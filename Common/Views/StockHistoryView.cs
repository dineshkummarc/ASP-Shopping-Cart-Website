using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class StockHistoryView
    {
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public int QuantityIncreased { get; set; }
        public int QuantityDecreases { get; set; }
        public string AffectedBy { get; set; }

    }
}
