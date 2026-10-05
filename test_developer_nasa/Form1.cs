using Microsoft.Data.SqlClient;
using ScottPlot;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using test_developer_nasa.Classi;
using System.Configuration;

namespace test_developer_nasa
{
    /// <summary>
    /// Form 1 cè l'interfaccia che contiene l'inter programma per l'utilizzo delle API nasa Asteroids - NeoWs
    /// </summary>
    public partial class Form1 : Form
    {
        Asteroidi? currentAsteroid; /// oggetto asteroide utilizzato per mantenere il dato mentre si naviga l'interfaccia
        private bool sortAscending = false; /// variabile per la gestione dell'ordinamento nelle datagridview
        private SortedDictionary<int, string> AsteroidList = new SortedDictionary<int, string>(); /// dizionario chiave nome per mantenere un lista di asteroidi, usato dentro la combobox
        private BindingList<CloseApproach> CloseApproachList = new BindingList<CloseApproach>(); /// binding list per l'aggiornamento della datagridview nella prima schermata
        private readonly string nasaApiKey = ConfigurationManager.AppSettings["NasaApiKey"] ?? "DEMO_KEY"; /// api key per acedere ai dati nasa situata nell'app.config
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DB\\NasaDatabase.mdf;Integrated Security=True"; //connection string al database interno
        ///<summary>inizializzazione della form e setup iniziale </summary>
        public Form1()
        {
            // Prova a leggere la chiave dal file di configurazione dell'eseguibile (MyApp.exe.config / App.config copiato in output)
            string? key = null;
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                key = config.AppSettings.Settings["NasaApiKey"]?.Value;
            }
            catch (ConfigurationErrorsException)
            {
                key = null;
            }
            // fallback a ConfigurationManager.AppSettings se necessario
            key ??= ConfigurationManager.AppSettings["NasaApiKey"];
            nasaApiKey = string.IsNullOrWhiteSpace(key) ? "DEMO_KEY" : key;

            if (nasaApiKey == "DEMO_KEY")
                MessageBox.Show($"API key non trovata, vai nel file test_developer_nasa.dll.config e inserisci la tua API key", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            InitializeComponent();
            DGVCloseAp.DataSource = CloseApproachList;
            DatePicI_ValueChanged(this, EventArgs.Empty);
            FillAsteroidDictionary();
            refreshCBAsteroid();
        }
        /// <summary>
        /// riempi la lista di asteoidi prendendo tutti gli asteroidi precedente salvati all'interno del DB
        /// </summary>
        private void FillAsteroidDictionary()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT Id, Name FROM Asteroidi";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                AsteroidList.TryAdd(reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")));
            }
            connection.Close();
            System.Diagnostics.Debug.WriteLine($"AsteroidList count: {AsteroidList.Count}");
        }

        /// <summary>
        /// bottone per prendere tutti gli avvicinamenti di oggetti alla terra in un periodo compreso tra una data inziale 
        /// ed una finale nel quale non possono differire da più di 7 giorni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGetCloseApproach_Click(object sender, EventArgs e)
        {
            btnGetCloseApproach.Enabled = false;
            DateTime startDate = DatePicI.Value;
            DateTime endDate = DatePicF.Value;
            //max 7 giorni di differenza tra start_date e end_date, altrimenti l'API restituisce un errore

            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"
            --tabella temporanea per contenere le date comprese tra start_date e end_date
            CREATE TABLE #rangedate (
            date DATETIMEOFFSET (7) PRIMARY KEY
            );

            -- Genera le date usando e le inserisce direttamente nella tabella temporanea
            WITH DateSequence AS (
            SELECT @Startdate AS CurrentDate, 0 AS Step
            UNION ALL
            SELECT DATEADD(DAY, 1, CurrentDate), Step + 1
            FROM DateSequence
            WHERE Step < @offset
            )
            INSERT INTO #rangedate (date)
            SELECT CurrentDate 
            FROM DateSequence;

            -- Creazione risultato con le date mancanti
            SELECT * FROM #rangedate WHERE date NOT IN (SELECT r.date FROM #rangedate r INNER JOIN closeapproach c ON CAST(r.date AS DATE) = CAST(c.closeapproachdate AS DATE) WHERE OrbitingBody = 'Earth');

            -- Eliminazione della tabella temporanea
            DROP TABLE #rangedate;";
            List<DateTime> missingDates = new List<DateTime>();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StartDate", startDate.Date);
            command.Parameters.AddWithValue("@offset", (endDate - startDate).Days);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime missingDate = reader.GetDateTimeOffset(0).DateTime;
                missingDates.Add(missingDate);
            }
            connection.Close();
            // se nel db ci sono range di dati mancanti tra la prima data e l'ultima aggiorna il DB con i dati delle date mancanti
            if (missingDates.Count > 0 || CBForceUpdate.Checked)
            {
                UpdateLabel.Visible = true;
                UpdateLabel.Text = "Download dati in corso...";
                //calcola l'intervallo di date da scaricare
                var missingdatarange = NasaDatabase.Missingdaterange(missingDates);
                List<Task<(int updatedAsteroid, int updatedCloseApproach)>> downloadTasks = new List<Task<(int updatedAsteroid, int updatedCloseApproach)>>();
                foreach (var missingrange in missingdatarange)
                {
                    var task = NasaDatabase.CloseApproachApi(missingrange.Item1, missingrange.Item2, connectionString, AsteroidList, nasaApiKey);
                    downloadTasks.Add(task);
                }
                var risultato = await Task.WhenAll(downloadTasks);
                int totalUpdatedAsteroids = risultato.Sum(r => r.updatedAsteroid);
                int totalUpdatedCloseApproaches = risultato.Sum(r => r.updatedCloseApproach);
                UpdateLabel.Text = $"aggiornati {totalUpdatedAsteroids} asteroidi\n e {totalUpdatedCloseApproaches} passaggi ravvicinati";
                refreshCBAsteroid();
            }
            else
            {
                UpdateLabel.Visible = true;
                UpdateLabel.Text = "Dati già presenti nel database";
            }
            updateDGVCloseApproach(startDate, endDate);
            updatePieHazard_Sentry(startDate, endDate);
            btnGetCloseApproach.Enabled = true;
        }
        /// <summary>
        /// aggiornamento dei grafici a torta hazard e sentry, mostrano quanti asteoidi degli avvicanemtni ottenuti sono potenzialmente pericoloso e sse sono sentry
        /// </summary>
        /// <param name="startDate">data iniziale</param>
        /// <param name="endDate">data finale</param>
        private void updatePieHazard_Sentry(DateTime startDate, DateTime endDate)
        {
            PieHazard.Plot.Clear();
            PieHazard.Visible = true;
            PieSentry.Plot.Clear();
            PieSentry.Visible = true;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT 
                    COUNT(CASE WHEN isPotentiallyHazardous = 1 THEN 1 END) AS Pericolosi,
                    COUNT(CASE WHEN isPotentiallyHazardous = 0 THEN 1 END) AS NonPericolosi,
                    COUNT(CASE WHEN isSentryObject = 1 THEN 1 END) AS Sentry,
                    COUNT(CASE WHEN isSentryObject = 0 THEN 1 END) AS NonSentry
                    FROM Asteroidi INNER JOIN CloseApproach ON Asteroidi.Id = CloseApproach.AsteroidId
                    WHERE CloseApproachDate BETWEEN @StartDate AND @EndDate
                    AND OrbitingBody = 'Earth';";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StartDate", startDate.Date);
            command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            int Pericolosi = 0;
            int nonPericolosi = 0;
            int Sentry = 0;
            int nonSentry = 0;
            while (reader.Read())
            {
                Pericolosi = reader.GetInt32(reader.GetOrdinal("Pericolosi"));
                nonPericolosi = reader.GetInt32(reader.GetOrdinal("NonPericolosi"));
                Sentry = reader.GetInt32(reader.GetOrdinal("Sentry"));
                nonSentry = reader.GetInt32(reader.GetOrdinal("NonSentry"));
            }
            connection.Close();
            PieSlice PericolosiSlice = new() { Value = Pericolosi, FillColor = Colors.Red };
            PieSlice NonPericolosiSlice = new() { Value = nonPericolosi, FillColor = Colors.Lime };
            PieSlice SentrySlice = new() { Value = Sentry, FillColor = Colors.Red };
            PieSlice NonSentrySlice = new() { Value = nonSentry, FillColor = Colors.Lime };
            for (int j = 0; j < 2; j++)
            {
                List<PieSlice> pieSlice;
                ScottPlot.Plottables.Pie pie;
                string[] labels = new string[2];
                if (j == 0)
                {
                    pieSlice = new List<PieSlice> { PericolosiSlice, NonPericolosiSlice };
                    pie = PieHazard.Plot.Add.Pie(pieSlice);
                    labels[0] = "Pericolosi";
                    labels[1] = "Non Pericolosi";

                }
                else
                {
                    pieSlice = new List<PieSlice> { SentrySlice, NonSentrySlice };
                    pie = PieSentry.Plot.Add.Pie(pieSlice);
                    labels[0] = "Sentry";
                    labels[1] = "Non Sentry";
                }
                // determine percentages for each slice
                double total = pie.Slices.Select(x => x.Value).Sum();
                double[] percentages = pie.Slices.Select(x => x.Value / total * 100).ToArray();
                // set each slice label to its percentage
                for (int i = 0; i < pie.Slices.Count; i++)
                {
                    if (pie.Slices[i].Value != 0)
                    {
                        pie.Slices[i].Label = $"{percentages[i]:0.0}%";
                        pie.Slices[i].LabelFontSize = 15;
                        pie.Slices[i].LabelBold = true;
                        pie.Slices[i].LabelFontColor = Colors.Black.WithAlpha(.5);
                    }
                    pie.Slices[i].LegendText = $"{labels[i]}: " +
                                              $"{pie.Slices[i].Value}";
                }
                // hide unnecessary plot components
                pie.Radius = 0.8;
                pie.SliceLabelDistance = 1.3;
            }
            PieHazard.Plot.Axes.SetLimits(-1.1, 2.5, -2.5, 1.4);
            PieHazard.Plot.Axes.Frameless();
            PieHazard.Plot.HideGrid();
            PieHazard.Plot.ShowLegend(Alignment.LowerRight);
            PieHazard.Refresh();
            PieSentry.Plot.Axes.SetLimits(-1.1, 2.5, -2.5, 1.4);
            PieSentry.Plot.Axes.Frameless();
            PieSentry.Plot.HideGrid();
            PieSentry.Plot.ShowLegend(Alignment.LowerRight);
            PieSentry.Refresh();

        }
        /// <summary>
        /// aggiornamento grafico torna per la distribuzione dei pianeti al quale l'asteroide passa vicino
        /// </summary>
        private void updatePieHorbitBody()
        {
            if (currentAsteroid == null)
            {
                PieOrbitingBody.Visible = false;
                return;
            }
            PieOrbitingBody.Plot.Clear();
            PieOrbitingBody.Visible = true;
            var OrbitingBodycount = currentAsteroid.CloseApproaches.Values.GroupBy(c => c.OrbitingBody).Select(g => new
            {
                OrbitingBody = g.Key,
                OrbitingBodyCount = g.Count()
            }).ToList();
            int numeroGeneriTotali = OrbitingBodycount.Count;
            ScottPlot.Plottables.Pie pie;
            List<PieSlice> pieSliceList = new List<PieSlice>();
            foreach (var item in OrbitingBodycount)
            {
                PieSlice Slice = new() { Value = item.OrbitingBodyCount, LegendText = $"{item.OrbitingBody}: {item.OrbitingBodyCount}" };
                pieSliceList.Add(Slice);
            }
            pie = PieOrbitingBody.Plot.Add.Pie(pieSliceList);
            // determine percentages for each slice
            double total = pie.Slices.Select(x => x.Value).Sum();
            double[] percentages = pie.Slices.Select(x => x.Value / total * 100).ToArray();
            // set each slice label to its percentage
            IPalette palette = new ScottPlot.Palettes.Category10();
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                if (pie.Slices[i].Value != 0)
                {
                    pie.Slices[i].Label = $"{percentages[i]:0.0}%";
                    pie.Slices[i].LabelFontSize = 15;
                    pie.Slices[i].LabelBold = true;
                    pie.Slices[i].LabelFontColor = Colors.Black.WithAlpha(.5);
                    pie.Slices[i].FillColor = palette.GetColor(i);
                }
            }
            // hide unnecessary plot components
            pie.Radius = 0.8;
            pie.SliceLabelDistance = 1.3;
            PieOrbitingBody.Plot.Axes.SetLimits(-1.1, 2.5, -2.5, 1.4);
            PieOrbitingBody.Plot.Axes.Frameless();
            PieOrbitingBody.Plot.HideGrid();
            PieOrbitingBody.Plot.ShowLegend(Alignment.LowerRight);
            PieOrbitingBody.Refresh();
        }
        /// <summary>
        /// aggiornamento della datagridview per mostrate gli avvicinamenti della terra comresi tra star e enddate
        /// </summary>
        /// <param name="startDate">data iniziale</param>
        /// <param name="endDate">data finale</param>
        private void updateDGVCloseApproach(DateTime startDate, DateTime endDate)
        {
            CloseApproachList.Clear();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"
                SELECT * from CloseApproach
                WHERE (CloseApproachDate BETWEEN @StartDate AND @EndDate)
                AND OrbitingBody = 'Earth'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StartDate", startDate.Date);
            command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var closeApproach = new CloseApproach(reader);
                CloseApproachList.Add(closeApproach);
            }
            connection.Close();
            approachLabel.Visible = true;
            approachLabel.Text = $"Trovati {CloseApproachList.Count} passaggi" +
                $" ravvicinati tra il\n{startDate.ToShortDateString()} e il {endDate.ToShortDateString()}";
        }
        ///<summary>evento di controllo per assicurare che endate-stardate non siamo maggiore di 7 giorni</summary>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicI_ValueChanged(object sender, EventArgs e)
        {
            DatePicF.MaxDate = DateTimePicker.MaxDateTime; // Imposta la data massima a DateTime.MaxValue
            DatePicF.MinDate = DateTimePicker.MinDateTime; // Imposta la data minima a DateTime.MinValue
            DatePicF.MinDate = DatePicI.Value;
            DatePicF.Value = DatePicI.Value;
            DatePicF.MaxDate = DatePicI.Value.AddDays(7); // Imposta la data massima a 7 giorni dopo la data iniziale
        }
        /// <summary>
        /// evento per la formattazione testuale della combobox asteroidi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBAsteroidi_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is KeyValuePair<int, string> asteroide)
            {
                // Imposta la stringa formattata desiderata
                e.Value = $"Id: {asteroide.Key}, Nome: {asteroide.Value}";
            }
        }
        /// <summary>
        /// switch automatico della custom tab control tramite checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckNuovoAsteroide_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckNuovoAsteroide.Checked)
            {
                TCAsteroidMode.SelectedIndex = 1;
            }
            else
            {
                TCAsteroidMode.SelectedIndex = 0;
            }
            CBAsteroidi.SelectedIndex = -1;
            currentAsteroid = null;
            TBNewAsteroid.Clear();
            UpdateAsteroidPage();
        }
        /// <summary>
        /// evento per la selezione dell'asteroide tramite combobox e sucessivo aggiornamento della pagina con richiesta di aggiornamento dati se necessario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CBAsteroidi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBAsteroidi.Enabled = false;
            if (CBAsteroidi.SelectedIndex == -1 || CBAsteroidi.SelectedItem == null || CBAsteroidi.Tag == null)
            {
                currentAsteroid = null;
                UpdateAsteroidPage();
                CBAsteroidi.Enabled = true;
                return;
            }
            var selectedAsteroid = (KeyValuePair<int, string>)CBAsteroidi.SelectedItem;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT asteroid_id FROM OrbitalData WHERE asteroid_id = @asteroidId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@asteroidId", selectedAsteroid.Key);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                connection.Close();
                LBAsteroidDownload.Visible = true;
                var task = NasaDatabase.AsteroidApi(selectedAsteroid.Key, connectionString, nasaApiKey);
                await task;
                LBAsteroidDownload.Visible = false;
            }
            else
            {
                connection.Close();
            }
            UpdateCurrentAsteroid(selectedAsteroid.Key);
            UpdateAsteroidPage();
            CBAsteroidi.Enabled = true;
        }
        /// <summary>
        /// aggiornamento grafico della pagina asteroid page 
        /// </summary>
        private void UpdateAsteroidPage()
        {
            updateDGVAsteroidClose();
            UpdateLabelsAsteroid();
            UpdatePropertyGrids();
            updatePieHorbitBody();
        }
        /// <summary>
        /// aggiornamento delle proprietà di orbita e classe orbita mostrare del current asteroid
        /// </summary>
        private void UpdatePropertyGrids()
        {
            if (currentAsteroid == null)
            {
                PGOrbitClass.SelectedObject = null;
                PGOrbitalData.SelectedObject = null;
                return;
            }
            PGOrbitalData.SelectedObject = currentAsteroid.OrbitData;
            PGOrbitClass.SelectedObject = currentAsteroid.OrbitData!.OrbitClass;
        }
        /// <summary>
        /// aggiornamento delle proprietà del current asteroid
        /// </summary>
        private void UpdateLabelsAsteroid()
        {
            if (currentAsteroid == null)
            {
                TBNeoRefernce.Clear();
                TBNasaUrl.Clear();
                TBHazardous.Clear();
                TBSentry.Clear();
                TBDiameterMin.Clear();
                TBDiameterMax.Clear();
                TBMagnitude.Clear();
                return;
            }
            TBNeoRefernce.Text = currentAsteroid.NeoReferenceId.ToString();
            TBNasaUrl.Text = currentAsteroid.NasaJplUrl;
            TBHazardous.Text = currentAsteroid.IsPotentiallyHazardous.ToString();
            TBSentry.Text = currentAsteroid.IsSentryObject.ToString();
            TBDiameterMin.Text = currentAsteroid.EstimatedDiameterMinKm.ToString();
            TBDiameterMax.Text = currentAsteroid.EstimatedDiameterMaxKm.ToString();
            TBMagnitude.Text = currentAsteroid.AbsoluteMagnitudeH.ToString();
        }
        /// <summary>
        /// aggiornamento della datagridview con tutti gli avvicinamenti registrati dal current asteroid
        /// </summary>
        private void updateDGVAsteroidClose()
        {
            DGVAsteroidClose.DataSource = null;
            if (currentAsteroid == null)
            {

                DGVAsteroidClose.Refresh();
                return;
            }
            DGVAsteroidClose.DataSource = currentAsteroid.CloseApproaches.Values.ToList();
            DGVAsteroidClose.Refresh();
        }
        /// <summary>
        /// richiesta dati dal DB e salvataggio del current asteroid in memoria
        /// </summary>
        /// <param name="asteroidId">id dell'asteroide da sttare come current</param>
        private void UpdateCurrentAsteroid(int asteroidId)
        {
            CloseApproach c;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"
                SELECT * FROM Asteroidi
                WHERE Id = @AsteroidId;
                SELECT * FROM CloseApproach
                WHERE AsteroidId = @AsteroidId;
                SELECT * FROM OrbitClass
                WHERE orbit_class_type = (SELECT orbit_class FROM OrbitalData WHERE Asteroid_Id = @AsteroidId)
                SELECT * FROM OrbitalData
                WHERE Asteroid_Id = @AsteroidId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AsteroidId", asteroidId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                connection.Close();
                CBAsteroidi.SelectedIndex = -1;
                return;
            }
            currentAsteroid = new Asteroidi(reader);
            reader.NextResult();
            while (reader.Read())
            {
                c = new CloseApproach(reader);
                currentAsteroid.CloseApproaches.TryAdd(c.CloseApproachDate, c);
            }
            reader.NextResult();
            reader.Read();
            OrbitClass oc = new OrbitClass(reader);
            reader.NextResult();
            reader.Read();
            currentAsteroid.OrbitData = new OrbitalData(reader, oc);
            connection.Close();
        }
        /// <summary>
        /// evento per l'apertura del browser sul link dato 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBNasaUrl_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = e.LinkText,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossibile aprire il link: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// inserimento di un nuovo asteroide nel DB dato un ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BTNewAsteroide_Click(object sender, EventArgs e)
        {
            TBNewAsteroid.Enabled = false;
            if (TBNewAsteroid.Text == "")
            {
                MessageBox.Show("Non hai inserito nessuno ID asteroide", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBNewAsteroid.Enabled = true;
                return;
            }
            if (!int.TryParse(TBNewAsteroid.Text, out int asteroidId))
            {
                MessageBox.Show("L'ID asteroide deve essere un numero intero", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TBNewAsteroid.Enabled = true;
                return;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"
                SELECT Id, Name FROM Asteroidi
                WHERE Id = @AsteroidId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AsteroidId", asteroidId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string asteroidName = reader.GetString(reader.GetOrdinal("Name"));
                connection.Close();
                MessageBox.Show("l'asteroide inserito è già presente", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CheckNuovoAsteroide.Checked = false;
                CBAsteroidi.SelectedIndex = CBAsteroidi.Items.IndexOf(new KeyValuePair<int, string>(asteroidId, asteroidName));
                TBNewAsteroid.Enabled = true;
                return;
            }
            connection.Close();
            int check = AsteroidList.Count();
            LBAsteroidDownload.Visible = true;
            var task = NasaDatabase.AsteroidApi(asteroidId, connectionString, nasaApiKey, true, AsteroidList);
            await task;
            LBAsteroidDownload.Visible = false;
            if (check == AsteroidList.Count())
            {
                MessageBox.Show("L'ID asteroide inserito non è valido / non è stato trovato un asteroid con quel'ID / errore nel contattare l'API", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TBNewAsteroid.Enabled = true;
                return;
            }
            refreshCBAsteroid();
            UpdateCurrentAsteroid(asteroidId);
            TBNewAsteroid.Enabled = true;
            Asteroidi? tmpCurrentAsteroid = currentAsteroid;
            CheckNuovoAsteroide.Checked = false;
            currentAsteroid = tmpCurrentAsteroid;
            CBAsteroidi.SelectedIndex = CBAsteroidi.Items.IndexOf(new KeyValuePair<int, string>(currentAsteroid!.Id, currentAsteroid.Name));
        }
        /// <summary>
        /// aggiornamento della combobox in caso di nuovi asteroidi aggiunti alla lista in memoria
        /// </summary>
        private void refreshCBAsteroid()
        {
            CBAsteroidi.Tag = null; //flag per disabilitare l'evento cbasteroid index change
            CBAsteroidi.DataSource = null;
            CBAsteroidi.DataSource = new BindingSource(AsteroidList, "");
            CBAsteroidi.SelectedIndex = -1;
            CBAsteroidi.Tag = 1; //flag per abilitare l'evento cbasteroid index change
        }
        /// <summary>
        /// evento per la gestione di ordinamento delle varie colonne sul click dell'header 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVCloseAp_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string nomeColonna = DGVCloseAp.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(nomeColonna))
                return;
            var lista = CloseApproachList.ToList();
            Func<CloseApproach, object> keySelector = x => x.GetType().GetProperty(nomeColonna)?.GetValue(x, null)!;


            List<CloseApproach> listaOrdinata;
            if (sortAscending)
                listaOrdinata = lista.OrderBy(keySelector).ToList();
            else
                listaOrdinata = lista.OrderByDescending(keySelector).ToList();

            CloseApproachList = new BindingList<CloseApproach>(listaOrdinata);
            DGVCloseAp.DataSource = CloseApproachList;
            sortAscending = !sortAscending;
        }
        /// <summary>
        /// evento per la gestione di ordinamento delle varie colonne sul click dell'header 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVAsteroidClose_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string nomeColonna = DGVAsteroidClose.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(nomeColonna))
                return;
            var lista = currentAsteroid!.CloseApproaches.Values.ToList();
            var propInfo = typeof(CloseApproach).GetProperty(nomeColonna);
            if (propInfo == null) return;

            List<CloseApproach> listaOrdinata;
            if (sortAscending)
                listaOrdinata = lista.OrderBy(x => propInfo.GetValue(x, null)).ToList();
            else
                listaOrdinata = lista.OrderByDescending(x => propInfo.GetValue(x, null)).ToList();

            DGVAsteroidClose.DataSource = new BindingList<CloseApproach>(listaOrdinata);
            sortAscending = !sortAscending;
        }
        /// <summary>
        /// evento per il trasferimento alla pagina asteroide associata all'asteroide selezionato dalla lista di avvicinamenti alla terra in un range di date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVCloseAp_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataGridViewRow row = DGVCloseAp.Rows[e.RowIndex];
            if (row.Cells["AsteroidId"].Value != null)
            {
                int DGVAsteroidID = Convert.ToInt32(row.Cells["AsteroidId"].Value);
                string? DGVAsteroidName = null;
                if (AsteroidList.TryGetValue(DGVAsteroidID, out DGVAsteroidName))
                {
                    CBAsteroidi.SelectedIndex = CBAsteroidi.Items.IndexOf(new KeyValuePair<int, string>(DGVAsteroidID, DGVAsteroidName));
                    tabControl1.SelectedIndex = 1;
                }
            }
        }
        /// <summary>
        /// bottone per lo svuotamento del DB e l'eliminazione di tutti i dati salvati in esso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTNCleanDB_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Attenzione, stai per eliminare tutti i dati salvati nel Database, sicuro di voler continuare?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"
                DELETE FROM Asteroidi;
                DELETE FROM OrbitClass";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            System.Diagnostics.Debug.WriteLine($"Total rows deleted: {rowsAffected}");
            currentAsteroid = null;
            AsteroidList.Clear();
            CloseApproachList.Clear();
            CBAsteroidi.Tag = null;
            CBAsteroidi.DataSource = null;
            CBAsteroidi.SelectedIndex = -1;
            CBAsteroidi.Tag = 1;
            PieHazard.Plot.Clear();
            PieHazard.Visible = false;
            PieSentry.Plot.Clear();
            PieSentry.Visible = false;
        }
    }
}
