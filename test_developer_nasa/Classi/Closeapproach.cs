using Microsoft.Data.SqlClient;
using System;
using System.Globalization;
using System.Text.Json;

namespace test_developer_nasa.Classi
{
    // Generated from table 'Closeapproach'
    public class CloseApproach
    {
        public int AsteroidId { get; set; }
        public DateTimeOffset CloseApproachDate { get; set; }
        public long? EpochDateCloseApproach { get; set; }
        public double? RelativeVelocityKmH { get; set; }
        public double? MissDistanceKm { get; set; }
        public string? OrbitingBody { get; set; }
        //costruttore per l'api
        public CloseApproach(JsonElement json, int idasteroide)
        {
            this.AsteroidId = idasteroide;
            this.CloseApproachDate = DateTimeOffset.ParseExact(
                json.GetProperty("close_approach_date_full").GetString() ?? string.Empty, "yyyy-MMM-dd HH:mm", CultureInfo.InvariantCulture);
            this.EpochDateCloseApproach = json.TryGetProperty("epoch_date_close_approach", out var epochProp) ? epochProp.GetInt64() : (long?)null;
            this.RelativeVelocityKmH = double.Parse(json.GetProperty("relative_velocity").GetProperty("kilometers_per_hour").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.MissDistanceKm = double.Parse(json.GetProperty("miss_distance").GetProperty("kilometers").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.OrbitingBody = json.GetProperty("orbiting_body").GetString();
        }
        public CloseApproach(SqlDataReader reader)
        {
            this.AsteroidId = reader.GetInt32(reader.GetOrdinal("AsteroidId"));
            this.CloseApproachDate = reader.GetDateTimeOffset(reader.GetOrdinal("CloseApproachDate"));
            this.EpochDateCloseApproach = reader.GetInt64(reader.GetOrdinal("EpochDateCloseApproach"));
            this.RelativeVelocityKmH = reader.GetDouble(reader.GetOrdinal("RelativeVelocityKmH"));
            this.MissDistanceKm = reader.GetDouble(reader.GetOrdinal("MissDistanceKm"));
            this.OrbitingBody = reader.GetString(reader.GetOrdinal("OrbitingBody"));
        }
       
    }
}
