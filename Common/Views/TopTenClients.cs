using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class TopTenClients
    {
        public Guid ClientID { get; set; }
        public string ClientName { get; set; }
        public int ProductCount { get; set; }
    }
}
