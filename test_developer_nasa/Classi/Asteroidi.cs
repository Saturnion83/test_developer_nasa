using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;


namespace test_developer_nasa.Classi
{
    // Generated from table 'Asteroidi'
    public class Asteroidi
    {
       /* public override string ToString()
        {
                return $"Asteroid: {Name}," +
                $" NeoReferenceId: {NeoReferenceId}," +
                $" AbsoluteMagnitudeH: {AbsoluteMagnitudeH}," +
                $" IsPotentiallyHazardous: {IsPotentiallyHazardous}," +
                $" IsSentryObject: {IsSentryObject}," +
                $" EstimatedDiameterMinKm: {EstimatedDiameterMinKm}," +
                $" EstimatedDiameterMaxKm: {EstimatedDiameterMaxKm}," +
                $" CloseApproaches Count: {CloseApproaches.Count}";
        }*/
        public string CBToString()
        {
            return $"{Id} - {Name}";
        }
        public int Id { get; set; }
        public int NeoReferenceId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NasaJplUrl { get; set; }
        public double AbsoluteMagnitudeH { get; set; }
        public bool IsPotentiallyHazardous { get; set; }
        public bool IsSentryObject { get; set; }
        public double EstimatedDiameterMinKm { get; set; }
        public double EstimatedDiameterMaxKm { get; set; }
        public Dictionary<DateTimeOffset, CloseApproach> CloseApproaches { get; set; } = new Dictionary<DateTimeOffset, CloseApproach>();
        public OrbitalData? OrbitData { get; set; }
        //costruttore per l'api
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
        //costuttore per il db
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
