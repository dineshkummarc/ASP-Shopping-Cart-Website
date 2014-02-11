using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.Common;
using System.Configuration;

namespace Data
{
    public class ConnectionClass
    {
        /// <summary>
        /// A constructor used to create a new entity object.
        /// Level: Data
        /// </summary>
        public ConnectionClass()
        {
            try
            {
                _Entities = new TheGreatSupermarketEntities();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// A constructor used to create a new entity object.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public ConnectionClass(bool isAdmin)
        {
            try
            {
                if (isAdmin == false)
                {
                    _Entities = new TheGreatSupermarketEntities(ConfigurationManager.ConnectionStrings["TheGreatSupermarketEntitiesForUser"].ConnectionString);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        private TheGreatSupermarketEntities _Entities;
        public TheGreatSupermarketEntities Entities
        {
            get { return _Entities; }
            set { _Entities = value; }
        }
    }
}
