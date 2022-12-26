using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace RUPS_desktop
{

    public class Station
    {
        public string type { get; set; }
        public string id { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        [JsonProperty("marker-color")]
        public string markercolor { get; set; }

        [JsonProperty("marker-size")]
        public string markersize { get; set; }

        [JsonProperty("marker-symbol")]
        public string markersymbol { get; set; }

        [JsonProperty("marker-title")]
        public string markertitle { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string busroute { get; set; }
        public string URL { get; set; }
    }

  


}
