using System;

namespace DK.OM.Bhav.Models
{
    public class BSEDownloadCSVType
    {
        public int SC_CODE { get; set; }
        public string SC_NAME { get; set; }
        public string SC_GROUP { get; set; }
        public string SC_TYPE { get; set; }
        public double OPEN { get; set; }
        public double HIGH { get; set; }
        public double LOW { get; set; }
        public double CLOSE { get; set; }
        public double LAST { get; set; }
        public double PREVCLOSE { get; set; }
        public int NO_TRADES { get; set; }
        public int NO_OF_SHRS { get; set; }
        public string NET_TURNOV { get; set; }
        public string TDCLOINDI { get; set; }
    }
}
