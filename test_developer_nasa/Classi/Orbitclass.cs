using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Text.Json;

namespace test_developer_nasa.Classi
{
    /// <summary>
    /// classe OrbitClass
    /// la classe implementa semplicemente la stessa stuttura del database
    /// </summary>
    [ReadOnly(true)]  //serve per dire alla property grid di non essere modificabile 
    public class OrbitClass
    {
        public string? OrbitClassType { get; set; }
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))] //permette di clicarci sopra alla multigrid per una visualzzazione del dato migliore
        public string? OrbitClassDescription { get; set; }
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))] //permette di clicarci sopra alla multigrid per una visualzzazione del dato migliore
        public string? OrbitClassRange { get; set; }
        /// <summary>
        /// costruttore della classe OrbitClass utilizzato suuccessivamente alla chiamata API
        /// </summary>
        /// <param name="json">json dell'oggetto ricevuto dall'api</param>
        public OrbitClass(JsonElement json)
        {
            this.OrbitClassType = json.GetProperty("orbit_class_type").GetString() ?? string.Empty;
            this.OrbitClassDescription = json.GetProperty("orbit_class_description").GetString() ?? string.Empty;
            this.OrbitClassRange = json.GetProperty("orbit_class_range").GetString() ?? string.Empty;
        }
        /// <summary>
        /// costruttore della classe OrbitClass utilizzato suuccessivamente alle chiamate del Database
        /// </summary>
        /// <param name="reader">oggetto ricevuto dal Database</param>
        public OrbitClass(SqlDataReader reader)
        {
            this.OrbitClassType = reader.GetString(reader.GetOrdinal("orbit_class_type"));
            this.OrbitClassDescription = reader.GetString(reader.GetOrdinal("orbit_class_description"));
            this.OrbitClassRange = reader.GetString(reader.GetOrdinal("orbit_class_range"));
        }
    }
}
