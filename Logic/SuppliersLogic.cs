using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Views;
using Data;

namespace Logic
{
    public class SuppliersLogic
    {
        /// <summary>
        /// Retrieves all Suppliers
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type SuppliersView</returns>
        public IQueryable<SuppliersView> RetrieveAllSuppliers()
        {
            try
            {
                return new SuppliersRepository().RetrieveAllSuppliers();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all Countries
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type Country</returns>
        public IQueryable<Country> RetrieveAllCountries()
        {
            try
            {
                return new SuppliersRepository(false).RetrieveAllCountries();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a supplier by id
        /// Level: Logic
        /// </summary>
        /// <param name="ID">The Supplier ID</param>
        /// <returns>An object of type supplier</returns>
        public Supplier RetrieveSupplierByID(int ID)
        {
            try
            {
                return new SuppliersRepository().RetrieveSupplierByID(ID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a Supplier
        /// Level: Logic
        /// </summary>
        /// <param name="Supplier">The Supplier Name</param>
        /// <param name="Email">The Supplier Email</param>
        /// <param name="Postcode">The Postcode</param>
        /// <param name="StreetAddress">The Street Address</param>
        /// <param name="Town">The Town</param>
        /// <param name="Country">The Country</param>
        public void AddSupplier(string Supplier, string Email, string Postcode, string StreetAddress,
            string Town, string Country)
        {
            try
            {
                SuppliersRepository myRepository = new SuppliersRepository();

                Supplier mySupplier = new Supplier();

                mySupplier.Supplier1 = Supplier;
                mySupplier.Email = Email;
                mySupplier.Postcode = Postcode;
                mySupplier.StreetAddress = StreetAddress;

                Town myTown = myRepository.RetrieveTown(Town, Country);

                //If Town Exists
                if (myTown != null)
                {
                    //Assigning Existent Town to Supplier
                    mySupplier.Town = myTown;
                }
                else
                {
                    //Instanciating New Town
                    myTown = new Town();
                    myTown.Town1 = Town;
                    myTown.Country = myRepository.RetrieveCountry(Country);

                    //Assigning New Town to Supplier
                    mySupplier.Town = myTown;
                }

                myRepository.AddSupplier(mySupplier);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Supplier
        /// Level: Logic
        /// </summary>
        /// <param name="ID">The Supplier ID</param>
        /// <param name="Supplier">The Supplier Name</param>
        /// <param name="Email">The Email</param>
        /// <param name="Postcode">The Postcode</param>
        /// <param name="StreetAddress">The Street Address</param>
        /// <param name="Town">The Town</param>
        /// <param name="Country">The Country</param>
        public void UpdateSupplier(int ID, string Supplier, string Email, string Postcode, string StreetAddress,
            string Town, string Country)
        {
            try
            {
                SuppliersRepository myRepository = new SuppliersRepository();

                SuppliersView mySupplier = new SuppliersView();

                mySupplier.Id = ID;
                mySupplier.Supplier = Supplier;
                mySupplier.Email = Email;
                mySupplier.Postcode = Postcode;
                mySupplier.StreetAddress = StreetAddress;
                mySupplier.Town = Town;
                mySupplier.Country = Country;

                myRepository.UpdateSupplier(mySupplier);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a Supplier
        /// Level: Logic
        /// </summary>
        /// <param name="ID">The Supplier ID</param>
        /// <returns>True if Successful, False if Not</returns>
        public bool DeleteSupplier(int ID)
        {
            try
            {
                SuppliersRepository myRepository = new SuppliersRepository();

                if ((!myRepository.SupplierHasOrders(ID)) & (!myRepository.SupplierHasProducts(ID)))
                {
                    myRepository.DeleteSupplier(ID);
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
    }
}
