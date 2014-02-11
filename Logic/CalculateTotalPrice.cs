using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Views;

namespace Logic
{
    public class CalculateTotalPrice
    {
        /// <summary>
        /// Calculates Order Total Price
        /// Level: Logic
        /// </summary>
        /// <param name="myItemList"></param>
        /// <returns></returns>
        public double CalculateTotal(List<OrderItem> myItemList)
        {
            double myResult = 0;

            foreach(OrderItem myItem in myItemList)
            {
                myResult += (myItem.Quantity * myItem.Price);
            }

            return myResult;
        }
    }
}
