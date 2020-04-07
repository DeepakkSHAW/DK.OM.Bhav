using System;
using System.Collections.Generic;
using System.Text;

namespace DK.OM.Bhav.Models
{
    public class BhavBSEStockPrices
    {
        public int Id { get; set; }
        public int BhavBSEStockId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Last { get; set; }
        public double PreClose { get; set; }
        public long Turnover { get; set; }
        public DateTime OnDate { get; set; } = DateTime.Now;

        //Navigation properties
        public IEnumerable<BhavBSEStocks> BhavStocks { get; set; }
    }
}
