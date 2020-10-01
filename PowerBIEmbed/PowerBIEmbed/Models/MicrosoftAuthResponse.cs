using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerBIEmbed.Models
{
    public class MicrosoftAuthResponse
    {
        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("scope")]
        public string scope { get; set; }

        [JsonProperty("experies_in")]
        public int expires_in { get; set; }

        [JsonProperty("ext_experies_in")]
        public int ext_expires_in { get; set; }

        [JsonProperty("experies_on")]
        public int expires_on { get; set; }

        [JsonProperty("not_before")]
        public int not_before { get; set; }

        [JsonProperty("resource")]
        public string resource { get; set; }

        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }

        [JsonProperty("id_token")]
        public string id_token { get; set; }
    }
}