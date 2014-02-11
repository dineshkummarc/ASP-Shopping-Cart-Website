using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class UserOrdersView
    {
        public Guid Id { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string Username { get; set; }
    }
}
