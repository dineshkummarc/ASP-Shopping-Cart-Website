using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class SuppliersView
    {
        public int Id { get; set; }
        public string Supplier { get; set; }
        public string Email { get; set; }
        public string Postcode { get; set; }
        public string StreetAddress { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
    }
}
