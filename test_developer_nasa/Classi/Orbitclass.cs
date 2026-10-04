using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Drawing.Design;
using System.Text.Json;

namespace test_developer_nasa.Classi
{
    // Generated from table 'Orbitclass'
    [ReadOnly(true)]
    public class OrbitClass
    {
        public string? OrbitClassType { get; set; }
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string? OrbitClassDescription { get; set; }
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string? OrbitClassRange { get; set; }
        public OrbitClass(JsonElement json)
        {
            this.OrbitClassType = json.GetProperty("orbit_class_type").GetString() ?? string.Empty;
            this.OrbitClassDescription = json.GetProperty("orbit_class_description").GetString() ?? string.Empty;
            this.OrbitClassRange = json.GetProperty("orbit_class_range").GetString() ?? string.Empty;
        }
        public OrbitClass(SqlDataReader reader)
        {
            this.OrbitClassType = reader.GetString(reader.GetOrdinal("orbit_class_type"));
            this.OrbitClassDescription = reader.GetString(reader.GetOrdinal("orbit_class_description"));
            this.OrbitClassRange = reader.GetString(reader.GetOrdinal("orbit_class_range"));
        }
    }
}
