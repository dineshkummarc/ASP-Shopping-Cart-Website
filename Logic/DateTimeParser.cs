using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class DateTimeParser
    {
        /// <summary>
        /// Parses a String to Date Time
        /// Level: Logic
        /// </summary>
        /// <param name="Date">The Date</param>
        /// <returns>A DateTime Object</returns>
        public DateTime ParseDate(string Date)
        {
            string[] mySplitString = Date.Split('-');

            int myMonth = Convert.ToInt32(mySplitString[0]);
            int myDay = Convert.ToInt32(mySplitString[1]);
            int myYear = Convert.ToInt32(mySplitString[2]);

            return new DateTime(myYear, myMonth, myDay);
        }

        /// <summary>
        /// Parses a Date Time Object to String
        /// Level: Logic
        /// </summary>
        /// <param name="Date">The DateTime Object</param>
        /// <returns>The Date</returns>
        public string ParseDateToString(DateTime? Date)
        {
            if (Date != null)
            {
                return Date.Value.Month + "/" + Date.Value.Day + "/" + Date.Value.Year;
            }
            else
            {
                return null;
            }
        }
    }
}
