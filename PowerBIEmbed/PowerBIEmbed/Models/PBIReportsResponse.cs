using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerBIEmbed.Models
{
    public class PBIReportsResponse
    {
        public IEnumerable<PowerBiReport> value { get; set; }
    }
}