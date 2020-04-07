using System;
using System.Collections.Generic;
using System.Text;

namespace DK.OM.Bhav.Models
{
    public class BhavBSEStocks
    {
        public int Id { get; set; }
        public string BSECode { get; set; }
        public string StockName { get; set; }
        //public string StockFullName { get; set; }
        //public string NSECode { get; set; }
        public string StockGroup { get; set; }
        public string StockType { get; set; }

        //only for audit purposes
        public DateTime inDate { get; private set; } = DateTime.Now;
        public DateTime updateDate { get; set; } = DateTime.Now;
    }
}
