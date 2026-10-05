using Microsoft.Data.SqlClient;
using System.Text.Json;


namespace test_developer_nasa.Classi
{
    /// <summary>
    /// classe asteroidi
    /// la classe implementa semplicemente la stessa stuttura del database
    /// </summary>
    public class Asteroidi
    {
        public int Id { get; set; }
        public int NeoReferenceId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NasaJplUrl { get; set; }
        public double AbsoluteMagnitudeH { get; set; }
        public bool IsPotentiallyHazardous { get; set; }
        public bool IsSentryObject { get; set; }
        public double EstimatedDiameterMinKm { get; set; }
        public double EstimatedDiameterMaxKm { get; set; }
        /// <summary>
        /// dizionario per tenere traccia di tutti gli avvicinamenti dell'asteroide
        /// </summary>
        public Dictionary<DateTimeOffset, CloseApproach> CloseApproaches { get; set; } = new Dictionary<DateTimeOffset, CloseApproach>();
        /// <summary>
        /// dati dell'orbita, può essere che non sia assegnato poiche quando prendiamo i dati per gli avvicinamenti terrestri i dati specifici dell'orbita dell'asteroide
        /// non sono passati e vengono salvati successivamente ustilizzando l'api per ottenere i dati dell'asteroide
        /// </summary>
        public OrbitalData? OrbitData { get; set; }
        /// <summary>
        /// costruttore della classe Asteroidi utilizzato suuccessivamente alla chiamata API
        /// </summary>
        /// <param name="json">json dell'oggetto ricevuto dall'api</param>
        public Asteroidi(JsonElement json)
        {
            this.Id = int.Parse(json.GetProperty("id").GetString() ?? string.Empty);
            this.NeoReferenceId = int.Parse(json.GetProperty("neo_reference_id").GetString() ?? string.Empty);
            this.Name = json.GetProperty("name").GetString() ?? string.Empty;
            this.NasaJplUrl = json.GetProperty("nasa_jpl_url").GetString() ?? string.Empty;
            this.AbsoluteMagnitudeH = json.GetProperty("absolute_magnitude_h").GetDouble();
            this.IsPotentiallyHazardous = json.GetProperty("is_potentially_hazardous_asteroid").GetBoolean();
            this.IsSentryObject = json.GetProperty("is_sentry_object").GetBoolean();
            this.EstimatedDiameterMinKm = json.GetProperty("estimated_diameter").GetProperty("kilometers").GetProperty("estimated_diameter_min").GetDouble();
            this.EstimatedDiameterMaxKm = json.GetProperty("estimated_diameter").GetProperty("kilometers").GetProperty("estimated_diameter_max").GetDouble();
        }
        /// <summary>
        /// costruttore della classe Asteroidi utilizzato suuccessivamente alle chiamate del Database
        /// </summary>
        /// <param name="reader">oggetto ricevuto dal Database</param>
        public Asteroidi(SqlDataReader reader)
        {
            this.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            this.NeoReferenceId = reader.GetInt32(reader.GetOrdinal("NeoReferenceId"));
            this.Name = reader.GetString(reader.GetOrdinal("Name"));
            this.NasaJplUrl = reader.GetString(reader.GetOrdinal("NasaJplUrl"));
            this.AbsoluteMagnitudeH = reader.GetDouble(reader.GetOrdinal("AbsoluteMagnitudeH"));
            this.IsPotentiallyHazardous = reader.GetBoolean(reader.GetOrdinal("IsPotentiallyHazardous"));
            this.IsSentryObject = reader.GetBoolean(reader.GetOrdinal("IsSentryObject"));
            this.EstimatedDiameterMinKm = reader.GetDouble(reader.GetOrdinal("EstimatedDiameterMinKm"));
            this.EstimatedDiameterMaxKm = reader.GetDouble(reader.GetOrdinal("EstimatedDiameterMaxKm"));
        }
    }
}
