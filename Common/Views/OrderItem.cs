using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Views
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
