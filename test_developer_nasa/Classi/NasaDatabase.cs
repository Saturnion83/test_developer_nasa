using Microsoft.Data.SqlClient;
using System.Text.Json;


namespace test_developer_nasa.Classi
{
    /// <summary>
    /// classe per la gestione delle chiamate api e salvataggio dei dati all'interno del DB
    /// </summary>
    
    public class NasaDatabase
    {

        /// <summary>
        /// metodo api close approach che prende in input startDate, endDate, connectionString, AsteroidList e nasaApiKey e restituisce una tupla con il numero di asteroidi aggiornati e il numero di close approach aggiornati
        /// </summary>
        /// <param name="startDate">data iniziale</param>
        /// <param name="endDate">data finale</param>
        /// <param name="connectionString">stringa di connessione al DB</param>
        /// <param name="AsteroidList">dizionario degli asteroidi in memoria</param>
        /// <param name="nasaApiKey">chiave API</param>
        /// <returns>int updatedAsteroid, int updatedCloseApproach che indicano quante righe asteroide e avvicinamente sono state aggiunte all'interno del database</returns>
        public static async Task<(int updatedAsteroid, int updatedCloseApproach)> CloseApproachApi(DateTime startDate, DateTime endDate, string connectionString, SortedDictionary<int, string> AsteroidList, string nasaApiKey)
        {
            int updatedAsteroid = 0;
            int updatedCloseApproach = 0;
            string apiUrl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={nasaApiKey}";
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string latestApiJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                // Ora latestApiJson contiene il JSON restituito dall'API
                using JsonDocument doc = JsonDocument.Parse(latestApiJson);
                var root = doc.RootElement;
                foreach (var data in root.GetProperty("near_earth_objects").EnumerateObject())
                {
                    foreach (var asteroide in data.Value.EnumerateArray())
                    {
                        var nuovoAsteroide = new Asteroidi(asteroide); 
                        SqlConnection connection = new SqlConnection(connectionString);
                        string query = @"
                            IF NOT EXISTS (SELECT 1 FROM Asteroidi a WHERE a.Id = @Id)
                            BEGIN
                                INSERT INTO Asteroidi (Id, NeoReferenceId, Name, NasaJplUrl, AbsoluteMagnitudeH, IsPotentiallyHazardous, IsSentryObject, EstimatedDiameterMinKm, EstimatedDiameterMaxKm)
                                VALUES (@Id, @NeoReferenceId, @Name, @NasaJplUrl, @AbsoluteMagnitudeH, @IsPotentiallyHazardous, @IsSentryObject, @EstimatedDiameterMinKm, @EstimatedDiameterMaxKm);
                            END";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@Id", nuovoAsteroide.Id);
                        cmd.Parameters.AddWithValue("@NeoReferenceId", nuovoAsteroide.NeoReferenceId);
                        cmd.Parameters.AddWithValue("@Name", nuovoAsteroide.Name);
                        cmd.Parameters.AddWithValue("@NasaJplUrl", nuovoAsteroide.NasaJplUrl);
                        cmd.Parameters.AddWithValue("@AbsoluteMagnitudeH", nuovoAsteroide.AbsoluteMagnitudeH);
                        cmd.Parameters.AddWithValue("@IsPotentiallyHazardous", nuovoAsteroide.IsPotentiallyHazardous);
                        cmd.Parameters.AddWithValue("@IsSentryObject", nuovoAsteroide.IsSentryObject);
                        cmd.Parameters.AddWithValue("@EstimatedDiameterMinKm", nuovoAsteroide.EstimatedDiameterMinKm);
                        cmd.Parameters.AddWithValue("@EstimatedDiameterMaxKm", nuovoAsteroide.EstimatedDiameterMaxKm);
                        connection.Open();
                        var rowsAffected = cmd.ExecuteNonQuery();
                        System.Diagnostics.Debug.WriteLine($"Asteroid Rows affected: {rowsAffected}");
                        if (rowsAffected > 0)
                        {
                            updatedAsteroid++;
                        }
                        connection.Close();
                        // Gestisci gli avvicinamenti
                        foreach (var approach in asteroide.GetProperty("close_approach_data").EnumerateArray())
                        {
                            var newcloseApproach = new CloseApproach(approach, nuovoAsteroide.Id);
                            connection = new SqlConnection(connectionString);
                            query = @"
                                IF NOT EXISTS (SELECT 1 FROM CloseApproach c WHERE c.AsteroidId = @AsteroidId AND c.CloseApproachDate = @CloseApproachDate)
                                BEGIN
                                    INSERT INTO CloseApproach (AsteroidId, CloseApproachDate, EpochDateCloseApproach, RelativeVelocityKmH, MissDistanceKm, OrbitingBody)
                                    VALUES (@AsteroidId, @CloseApproachDate, @EpochDateCloseApproach, @RelativeVelocityKmH, @MissDistanceKm, @OrbitingBody);
                                END";
                            cmd = new SqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@AsteroidId", newcloseApproach.AsteroidId);
                            cmd.Parameters.AddWithValue("@CloseApproachDate", newcloseApproach.CloseApproachDate);
                            cmd.Parameters.AddWithValue("@EpochDateCloseApproach", newcloseApproach.EpochDateCloseApproach);
                            cmd.Parameters.AddWithValue("@RelativeVelocityKmH",newcloseApproach.RelativeVelocityKmH);
                            cmd.Parameters.AddWithValue("@MissDistanceKm", newcloseApproach.MissDistanceKm );
                            cmd.Parameters.AddWithValue("@OrbitingBody", newcloseApproach.OrbitingBody );
                            connection.Open();
                            rowsAffected = cmd.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine($"CloseApproach Rows affected: {rowsAffected}");
                            if (rowsAffected > 0)
                            {
                                updatedCloseApproach++;
                            }
                            connection.Close();

                        }
                        // Aggiungi l'asteroide alla lista
                        AsteroidList.TryAdd(nuovoAsteroide.Id, nuovoAsteroide.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching data from NASA API: {ex.Message}");
                return (updatedAsteroid, updatedCloseApproach);
            }
            return (updatedAsteroid, updatedCloseApproach);
        }
       /// <summary>
       /// metodo per ottenere i range di date mancanti da una data iniziale a una data finale partendo da una lista di date mancanti
       /// </summary>
       /// <param name="missingDates">lista delle date mancanti nel databse</param>
       /// <returns>lista di range ottenuuta dalle date mancanti nel database</returns>
        public static List<Tuple<DateTime, DateTime>> Missingdaterange(List<DateTime> missingDates)
        {
            List<Tuple<DateTime, DateTime>> missingDateRanges = new List<Tuple<DateTime, DateTime>>();
            
            if (missingDates == null || missingDates.Count == 0)
                return missingDateRanges;

            DateTime rangeStart = missingDates[0];
            DateTime rangeEnd = missingDates[0];

            for (int i = 1; i < missingDates.Count; i++)
            {
                if (missingDates[i].Subtract(rangeEnd).TotalDays == 1)
                {
                    rangeEnd = missingDates[i];
                }
                else
                {
                    missingDateRanges.Add(new Tuple<DateTime, DateTime>(rangeStart, rangeEnd));
                    rangeStart = missingDates[i];
                    rangeEnd = missingDates[i];
                }
            }

            missingDateRanges.Add(new Tuple<DateTime, DateTime>(rangeStart, rangeEnd));
            return missingDateRanges;
        }
        /// <summary>
        /// Metodo per ottenere i dati di un asteroide specifico, già presente nel database, dall'API di NASA e salvarli nel database, il flag newasteroid e asteroidlist servono in caso si voglia salvare i dati dell'asteroide
        /// poiche esso è un asteroide nuovo, in caso non vengano passate si presuppone che l'asteroide esista già nel DB e vengano aggiornati i dati con l'orbita e i vari close apporach dell'ateroide
        /// </summary>
        /// <param name="asteroidId">id dell'asteroide da mandare all'api</param>
        /// <param name="connectionString">stringa di connessione</param>
        /// <param name="nasaApiKey">chiave API</param>
        /// <param name="newAsteroide">flag per aggiungere un nuovo asteroide al database</param>
        /// <param name="AsteroidList">dizionario per salvare l'asteroide nuuovo in memoria</param>
        /// <returns></returns>

        public static async Task AsteroidApi(int asteroidId, string connectionString, string nasaApiKey, bool newAsteroide=false, SortedDictionary<int, string>? AsteroidList = null)
        {
            string apiUrl=$"https://api.nasa.gov/neo/rest/v1/neo/{asteroidId}?api_key={nasaApiKey}";
            try
            {
                SqlConnection connection;
                string query;
                SqlCommand cmd;
                int rowsAffected;
                using var client = new HttpClient();
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string latestApiJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                // Ora latestApiJson contiene il JSON restituito dall'API
                using JsonDocument doc = JsonDocument.Parse(latestApiJson);
                var root = doc.RootElement;
                if (newAsteroide && AsteroidList != null)
                {
                    var nuovoAsteroide = new Asteroidi(root);
                   connection = new SqlConnection(connectionString);
                   query = @"
                            IF NOT EXISTS (SELECT 1 FROM Asteroidi a WHERE a.Id = @Id)
                            BEGIN
                                INSERT INTO Asteroidi (Id, NeoReferenceId, Name, NasaJplUrl, AbsoluteMagnitudeH, IsPotentiallyHazardous, IsSentryObject, EstimatedDiameterMinKm, EstimatedDiameterMaxKm)
                                VALUES (@Id, @NeoReferenceId, @Name, @NasaJplUrl, @AbsoluteMagnitudeH, @IsPotentiallyHazardous, @IsSentryObject, @EstimatedDiameterMinKm, @EstimatedDiameterMaxKm);
                            END";
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", nuovoAsteroide.Id);
                    cmd.Parameters.AddWithValue("@NeoReferenceId", nuovoAsteroide.NeoReferenceId);
                    cmd.Parameters.AddWithValue("@Name", nuovoAsteroide.Name);
                    cmd.Parameters.AddWithValue("@NasaJplUrl", nuovoAsteroide.NasaJplUrl);
                    cmd.Parameters.AddWithValue("@AbsoluteMagnitudeH", nuovoAsteroide.AbsoluteMagnitudeH);
                    cmd.Parameters.AddWithValue("@IsPotentiallyHazardous", nuovoAsteroide.IsPotentiallyHazardous);
                    cmd.Parameters.AddWithValue("@IsSentryObject", nuovoAsteroide.IsSentryObject);
                    cmd.Parameters.AddWithValue("@EstimatedDiameterMinKm", nuovoAsteroide.EstimatedDiameterMinKm);
                    cmd.Parameters.AddWithValue("@EstimatedDiameterMaxKm", nuovoAsteroide.EstimatedDiameterMaxKm);
                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine($"Asteroid Rows affected: {rowsAffected}");
                    connection.Close();
                    AsteroidList.TryAdd(nuovoAsteroide.Id, nuovoAsteroide.Name);
                }
                foreach (var approach in root.GetProperty("close_approach_data").EnumerateArray())
                {
                    var newcloseApproach = new CloseApproach(approach, asteroidId);
                    connection = new SqlConnection(connectionString);
                    query = @"
                                IF NOT EXISTS (SELECT 1 FROM CloseApproach c WHERE c.AsteroidId = @AsteroidId AND c.CloseApproachDate = @CloseApproachDate)
                                BEGIN
                                    INSERT INTO CloseApproach (AsteroidId, CloseApproachDate, EpochDateCloseApproach, RelativeVelocityKmH, MissDistanceKm, OrbitingBody)
                                    VALUES (@AsteroidId, @CloseApproachDate, @EpochDateCloseApproach, @RelativeVelocityKmH, @MissDistanceKm, @OrbitingBody);
                                END";
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@AsteroidId", newcloseApproach.AsteroidId);
                    cmd.Parameters.AddWithValue("@CloseApproachDate", newcloseApproach.CloseApproachDate);
                    cmd.Parameters.AddWithValue("@EpochDateCloseApproach", newcloseApproach.EpochDateCloseApproach);
                    cmd.Parameters.AddWithValue("@RelativeVelocityKmH", newcloseApproach.RelativeVelocityKmH);
                    cmd.Parameters.AddWithValue("@MissDistanceKm", newcloseApproach.MissDistanceKm);
                    cmd.Parameters.AddWithValue("@OrbitingBody", newcloseApproach.OrbitingBody);
                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine($"CloseApproach Rows affected: {rowsAffected}");
                    connection.Close();
                }
                var newOrbitClass = new OrbitClass(root.GetProperty("orbital_data").GetProperty("orbit_class"));
                connection = new SqlConnection(connectionString);
                query = @"
                            IF NOT EXISTS (SELECT 1 FROM OrbitClass o WHERE o.orbit_class_type = @OrbitClassType)
                            BEGIN
                                INSERT INTO OrbitClass (orbit_class_type, orbit_class_description, orbit_class_range)
                                VALUES (@OrbitClassType, @OrbitClassDescription, @OrbitClassRange);
                            END";
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@OrbitClassType", newOrbitClass.OrbitClassType);
                cmd.Parameters.AddWithValue("@OrbitClassDescription", newOrbitClass.OrbitClassDescription);
                cmd.Parameters.AddWithValue("@OrbitClassRange", newOrbitClass.OrbitClassRange);
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"OrbitClass Rows affected: {rowsAffected}");
                connection.Close();
                var newOrbitalData = new OrbitalData(root.GetProperty("orbital_data"), asteroidId, newOrbitClass);
                connection = new SqlConnection(connectionString);
                query = @"
                            IF NOT EXISTS (SELECT 1 FROM OrbitalData o WHERE o.asteroid_id = @AsteroidId)
                            BEGIN
                                INSERT INTO OrbitalData (asteroid_id, orbit_id, aphelion_distance, ascending_node_longitude, data_arc_in_days, eccentricity, epoch_osculation, equinox, first_observation_date, inclination, jupiter_tisserand_invariant, last_observation_date, mean_anomaly, mean_motion, minimum_orbit_intersection, observations_used, orbit_determination_date, orbit_uncertainty, orbital_period, perihelion_argument, perihelion_distance, perihelion_time, semi_major_axis, orbit_class)
                                VALUES (@AsteroidId, @OrbitId, @AphelionDistance, @AscendingNodeLongitude, @DataArcInDays, @Eccentricity, @EpochOsculation, @Equinox, @FirstObservationDate, @Inclination, @JupiterTisserandInvariant, @LastObservationDate, @MeanAnomaly, @MeanMotion, @MinimumOrbitIntersection, @ObservationsUsed, @OrbitDeterminationDate, @OrbitUncertainty, @OrbitalPeriod, @PerihelionArgument, @PerihelionDistance, @PerihelionTime, @SemiMajorAxis, @orbit_class);
                            END";
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@AsteroidId", newOrbitalData.AsteroidId);
                cmd.Parameters.AddWithValue("@OrbitId", newOrbitalData.OrbitId);
                cmd.Parameters.AddWithValue("@AphelionDistance", newOrbitalData.AphelionDistance);
                cmd.Parameters.AddWithValue("@AscendingNodeLongitude", newOrbitalData.AscendingNodeLongitude);
                cmd.Parameters.AddWithValue("@DataArcInDays", newOrbitalData.DataArcInDays);
                cmd.Parameters.AddWithValue("@Eccentricity", newOrbitalData.Eccentricity);
                cmd.Parameters.AddWithValue("@EpochOsculation", newOrbitalData.EpochOsculation);
                cmd.Parameters.AddWithValue("@Equinox", newOrbitalData.Equinox);
                cmd.Parameters.AddWithValue("@FirstObservationDate", newOrbitalData.FirstObservationDate);
                cmd.Parameters.AddWithValue("@Inclination", newOrbitalData.Inclination);
                cmd.Parameters.AddWithValue("@JupiterTisserandInvariant", newOrbitalData.JupiterTisserandInvariant);
                cmd.Parameters.AddWithValue("@LastObservationDate", newOrbitalData.LastObservationDate);
                cmd.Parameters.AddWithValue("@MeanAnomaly", newOrbitalData.MeanAnomaly);
                cmd.Parameters.AddWithValue("@MeanMotion", newOrbitalData.MeanMotion);
                cmd.Parameters.AddWithValue("@MinimumOrbitIntersection", newOrbitalData.MinimumOrbitIntersection);
                cmd.Parameters.AddWithValue("@ObservationsUsed", newOrbitalData.ObservationsUsed);
                cmd.Parameters.AddWithValue("@OrbitDeterminationDate", newOrbitalData.OrbitDeterminationDate);
                cmd.Parameters.AddWithValue("@OrbitUncertainty", newOrbitalData.OrbitUncertainty);
                cmd.Parameters.AddWithValue("@OrbitalPeriod", newOrbitalData.OrbitalPeriod);
                cmd.Parameters.AddWithValue("@PerihelionArgument", newOrbitalData.PerihelionArgument);
                cmd.Parameters.AddWithValue("@PerihelionDistance", newOrbitalData.PerihelionDistance);
                cmd.Parameters.AddWithValue("@PerihelionTime", newOrbitalData.PerihelionTime);
                cmd.Parameters.AddWithValue("@SemiMajorAxis", newOrbitalData.SemiMajorAxis);
                cmd.Parameters.AddWithValue("@orbit_class", newOrbitClass.OrbitClassType);
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"OrbitalData Rows affected: {rowsAffected}");
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching data from NASA API: {ex.Message}");
                return;
            }
        }

    }
}
