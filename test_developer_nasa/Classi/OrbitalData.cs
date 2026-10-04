using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace test_developer_nasa.Classi
{
    [ReadOnly(true)]
    public class OrbitalData
    {

        
        public int AsteroidId { get; set; }
        public int OrbitId { get; set; }
        public double? AphelionDistance { get; set; }
        public double? AscendingNodeLongitude { get; set; }
        public int? DataArcInDays { get; set; }
        public double? Eccentricity { get; set; }
        public double? EpochOsculation { get; set; }
        public string? Equinox { get; set; }
        public DateTime? FirstObservationDate { get; set; }
        public double? Inclination { get; set; }
        public double? JupiterTisserandInvariant { get; set; }
        public DateTime? LastObservationDate { get; set; }
        public double? MeanAnomaly { get; set; }
        public double? MeanMotion { get; set; }
        public double? MinimumOrbitIntersection { get; set; }
        public int? ObservationsUsed { get; set; }
        public DateTimeOffset? OrbitDeterminationDate { get; set; }
        public int? OrbitUncertainty { get; set; }
        public double? OrbitalPeriod { get; set; }
        public double? PerihelionArgument { get; set; }
        public double? PerihelionDistance { get; set; }
        public double? PerihelionTime { get; set; }
        public double? SemiMajorAxis { get; set; }
        [Browsable(false)]
        public OrbitClass OrbitClass { get; set; }
        public OrbitalData(JsonElement json,int IDasteroide, OrbitClass orbitClass)
        {
            this.AsteroidId = IDasteroide;
            this.OrbitClass = orbitClass;
            this.OrbitId = int.Parse(json.GetProperty("orbit_id").GetString() ?? string.Empty);
            this.AphelionDistance = double.Parse(json.GetProperty("aphelion_distance").GetString() ?? string.Empty);
            this.AscendingNodeLongitude = double.Parse(json.GetProperty("ascending_node_longitude").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.DataArcInDays = json.GetProperty("data_arc_in_days").GetInt32();
            this.Eccentricity = double.Parse(json.GetProperty("eccentricity").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.EpochOsculation = double.Parse(json.GetProperty("epoch_osculation").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.Equinox = json.GetProperty("equinox").GetString();
            this.FirstObservationDate = DateTime.ParseExact(json.GetProperty("first_observation_date").GetString() ?? string.Empty, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            this.Inclination = double.Parse(json.GetProperty("inclination").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.JupiterTisserandInvariant = double.Parse(json.GetProperty("jupiter_tisserand_invariant").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.LastObservationDate = DateTime.ParseExact(json.GetProperty("last_observation_date").GetString() ?? string.Empty, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            this.MeanAnomaly = double.Parse(json.GetProperty("mean_anomaly").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.MeanMotion = double.Parse(json.GetProperty("mean_motion").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.MinimumOrbitIntersection = double.Parse(json.GetProperty("minimum_orbit_intersection").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.ObservationsUsed = json.GetProperty("observations_used").GetInt32(); 
            this.OrbitDeterminationDate = DateTimeOffset.ParseExact(json.GetProperty("orbit_determination_date").GetString() ?? string.Empty, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            this.OrbitUncertainty = int.Parse(json.GetProperty("orbit_uncertainty").GetString() ?? string.Empty);
            this.OrbitalPeriod = double.Parse(json.GetProperty("orbital_period").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.PerihelionArgument = double.Parse(json.GetProperty("perihelion_argument").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.PerihelionDistance = double.Parse(json.GetProperty("perihelion_distance").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.PerihelionTime = double.Parse(json.GetProperty("perihelion_time").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.SemiMajorAxis = double.Parse(json.GetProperty("semi_major_axis").GetString() ?? string.Empty, CultureInfo.InvariantCulture);
        }
        public OrbitalData(SqlDataReader reader, OrbitClass orbitClass)
        {
            this.AsteroidId = reader.GetInt32(reader.GetOrdinal("asteroid_id"));
            this.OrbitId = reader.GetInt32(reader.GetOrdinal("orbit_id"));
            this.AphelionDistance = reader.GetDouble(reader.GetOrdinal("aphelion_distance"));
            this.AscendingNodeLongitude = reader.GetDouble(reader.GetOrdinal("ascending_node_longitude"));
            this.DataArcInDays = reader.GetInt32(reader.GetOrdinal("data_arc_in_days"));
            this.Eccentricity = reader.GetDouble(reader.GetOrdinal("eccentricity"));
            this.EpochOsculation = reader.GetDouble(reader.GetOrdinal("epoch_osculation"));
            this.Equinox = reader.GetString(reader.GetOrdinal("equinox"));
            this.FirstObservationDate = reader.GetDateTime(reader.GetOrdinal("first_observation_date"));
            this.Inclination = reader.GetDouble(reader.GetOrdinal("inclination"));
            this.JupiterTisserandInvariant = reader.GetDouble(reader.GetOrdinal("jupiter_tisserand_invariant"));
            this.LastObservationDate =reader.GetDateTime(reader.GetOrdinal("last_observation_date"));
            this.MeanAnomaly = reader.GetDouble(reader.GetOrdinal("mean_anomaly"));
            this.MeanMotion =reader.GetDouble(reader.GetOrdinal("mean_motion"));
            this.MinimumOrbitIntersection = reader.GetDouble(reader.GetOrdinal("minimum_orbit_intersection"));
            this.ObservationsUsed = reader.GetInt32(reader.GetOrdinal("observations_used"));
            this.OrbitDeterminationDate = reader.GetDateTimeOffset(reader.GetOrdinal("orbit_determination_date"));
            this.OrbitUncertainty = reader.GetInt16(reader.GetOrdinal("orbit_uncertainty"));
            this.OrbitalPeriod = reader.GetDouble(reader.GetOrdinal("orbital_period"));
            this.PerihelionArgument = reader.GetDouble(reader.GetOrdinal("perihelion_argument"));
            this.PerihelionDistance = reader.GetDouble(reader.GetOrdinal("perihelion_distance"));
            this.PerihelionTime = reader.GetDouble(reader.GetOrdinal("perihelion_time"));
            this.SemiMajorAxis = reader.GetDouble(reader.GetOrdinal("semi_major_axis"));
            this.OrbitClass = orbitClass;
        }
    }
}
