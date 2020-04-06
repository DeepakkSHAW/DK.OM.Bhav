using System;
using System.Collections.Generic;
using System.Text;

namespace DK.OM.Bhav.Models
{
    public class NSEDownloadCSVType
    {
        public string SYMBOL { get; set; }
        public string SERIES { get; set; }
        public double OPEN { get; set; }
        public double HIGH { get; set; }
        public double LOW { get; set; }
        public double CLOSE { get; set; }
        public double LAST { get; set; }
        public double PREVCLOSE { get; set; }
        public long TOTTRDQTY { get; set; }
        public double TOTTRDVAL { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public long TOTALTRADES { get; set; }
        public string ISIN { get; set; }

    }
}
