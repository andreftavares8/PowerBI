using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerBIEmbed.Models
{
    public class PowerBiReport
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty("embedUrl")]
        public string EmbedUrl { get; set; }

        [JsonProperty("datasetId")]
        public string DatasetId { get; set; }
    }
}