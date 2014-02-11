using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class TopTenView
    {
        public Guid ProductID { get; set;  }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int QuantitySold { get; set; }
    }
}
