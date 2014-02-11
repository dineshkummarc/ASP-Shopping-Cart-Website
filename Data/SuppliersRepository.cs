using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Views;

namespace Data
{
    public class SuppliersRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public SuppliersRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public SuppliersRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Retrieves all Suppliers
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type SuppliersView</returns>
        public IQueryable<SuppliersView> RetrieveAllSuppliers()
        {
            try
            {
                var list = from s in Entities.Suppliers
                           join t in Entities.Towns
                           on s.TownFK equals t.Id
                           join c in Entities.Countries
                           on t.CountryFK equals c.Id
                           select new SuppliersView
                           {
                               Id = s.Id,
                               Supplier = s.Supplier1,
                               Email = s.Email,
                               Postcode = s.Postcode,
                               StreetAddress = s.StreetAddress,
                               Town = t.Town1,
                               Country = c.Country1
                           };

                return list.AsQueryable<SuppliersView>();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Level: Data
        /// </summary>
        /// <param name="myID">The Supplier ID</param>
        /// <returns>An object of type Supplier</returns>
        public Supplier RetrieveSupplierByID(int myID)
        {
            try
            {
                return Entities.Suppliers.SingleOrDefault(s => s.Id == myID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Level: Data
        /// </summary>
        /// <param name="mySupplier">The Supplier to Add</param>
        public void AddSupplier(Supplier mySupplier)
        {
            try
            {
                Entities.AddToSuppliers(mySupplier);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Country Given the Country Name
        /// Level: Data
        /// </summary>
        /// <param name="Country">The Country Name</param>
        /// <returns>An object of type Country</returns>
        public Country RetrieveCountry(string Country)
        {
            try
            {
                return Entities.Countries.SingleOrDefault(c => c.Country1 == Country);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Town Given the Town Name and Country Name
        /// Level: Data
        /// </summary>
        /// <param name="Town">The Town Name</param>
        /// <param name="Country">The Country Name</param>
        /// <returns>An object of type Town</returns>
        public Town RetrieveTown(string Town, string Country)
        {
            try
            {
                return Entities.Towns.SingleOrDefault(t => t.Town1 == Town && t.Country.Country1 == Country);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Supplier
        /// Level: Data
        /// </summary>
        /// <param name="mySupplier">The Supplier to Be Updated</param>
        public void UpdateSupplier(SuppliersView mySupplier)
        {
            try
            {
                Supplier myOriginalSupplier = RetrieveSupplierByID(mySupplier.Id);

                myOriginalSupplier.Supplier1 = mySupplier.Supplier;
                myOriginalSupplier.Email = mySupplier.Email;
                myOriginalSupplier.StreetAddress = mySupplier.StreetAddress;
                myOriginalSupplier.Postcode = mySupplier.Postcode;

                Town myTown = RetrieveTown(mySupplier.Town, mySupplier.Country);

                //If Town Exists
                if (myTown != null)
                {
                    //Assigning Existent Town to Supplier
                    myOriginalSupplier.Town = myTown;
                }
                else
                {
                    //Instanciating New Town
                    myTown = new Town();
                    myTown.Town1 = mySupplier.Town;
                    myTown.Country = RetrieveCountry(mySupplier.Country);

                    //Assigning New Town to Supplier
                    myOriginalSupplier.Town = myTown;
                }

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all Countries
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type Country</returns>
        public IQueryable<Country> RetrieveAllCountries()
        {
            try
            {
                return Entities.Countries.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a Supplier has any Orders
        /// Level: Data
        /// </summary>
        /// <param name="ID">Supplier ID</param>
        /// <returns>True if supplier has orders. False if does not.</returns>
        public bool SupplierHasOrders(int ID)
        {
            try
            {
                if (Entities.Orders.Where(o => o.SupplierFK == ID).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a supplier has any products
        /// Level: Data
        /// </summary>
        /// <param name="ID">The supplier ID</param>
        /// <returns>True if supplier has products. False if not.</returns>
        public bool SupplierHasProducts(int ID)
        {
            try
            {
                if (Entities.Products.Where(p => p.SupplierFK == ID).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a Supplier
        /// Level: Data
        /// </summary>
        /// <param name="ID">The Supplier ID</param>
        public void DeleteSupplier(int ID)
        {
            try
            {
                Entities.DeleteObject(RetrieveSupplierByID(ID));
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
