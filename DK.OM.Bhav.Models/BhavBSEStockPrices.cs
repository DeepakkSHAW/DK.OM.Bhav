using System;
using System.Collections.Generic;
using System.Text;

namespace DK.OM.Bhav.Models
{
    public class BhavBSEStockPrices
    {
        public int Id { get; set; }
        public int BhavStockId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Last { get; set; }
        public double PreClose { get; set; }
        public DateTime OnDate { get; private set; } = DateTime.Now;

        //Navigation properties
        public IEnumerable<BhavBSEStocks> BhavStocks { get; set; }
    }
}
