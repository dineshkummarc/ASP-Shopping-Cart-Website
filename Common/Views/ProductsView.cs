using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class ProductsView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQty { get; set; }
        public string ImageURL { get; set; }
        public bool Status { get; set; }
        public int CategoryFK { get; set; }
        public double VatRate { get; set; }
        public int? ReorderLevel { get; set; }
        public int SupplierFK { get; set; }
    }
}
