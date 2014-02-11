using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common.Views;
using Logic;

namespace External.AJAX.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class supplier : System.Web.Services.WebService
    {
        /// <summary>
        /// Adds a Supplier
        /// Level: External
        /// </summary>
        /// <param name="Supplier">The Supplier Name</param>
        /// <param name="Email">The Supplier Email</param>
        /// <param name="AddressLine1">The Address Part 1</param>
        /// <param name="AddressLine2">The Address Part 2</param>
        /// <param name="Town">The Town</param>
        /// <param name="Postcode">The Postcode</param>
        /// <param name="Country">The Country</param>
        [WebMethod]
        public void AddSupplier(string Supplier, string Email, string AddressLine1, string AddressLine2, string Town,
            string Postcode, string Country)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string mySupplier = Supplier.Trim();
                    string myEmail = Email.Trim();

                    string myAddress = AddressLine1.Trim().Replace("|", String.Empty) + "|"
                                  + AddressLine2.Trim().Replace("|", String.Empty);

                    string myTown = Town.Trim();
                    string myPostcode = Postcode.Trim();
                    string myCountry = Country.Trim();

                    new SuppliersLogic().AddSupplier(mySupplier, myEmail, myPostcode, myAddress, myTown, myCountry);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Supplier
        /// Level: External
        /// </summary>
        /// <param name="SupplierID">The Supplier ID</param>
        /// <param name="Supplier">The Supplier Name</param>
        /// <param name="Email">The Email</param>
        /// <param name="AddressLine1">The Address Part 1</param>
        /// <param name="AddressLine2">The Address Part 2</param>
        /// <param name="Town">The Town</param>
        /// <param name="Postcode">The Postcode</param>
        /// <param name="Country">The Country</param>
        [WebMethod]
        public void UpdateSupplier(string SupplierID, string Supplier, string Email, string AddressLine1, string AddressLine2, string Town,
            string Postcode, string Country)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int mySupplierID = Convert.ToInt32(SupplierID);
                    string mySupplier = Supplier.Trim();
                    string myEmail = Email.Trim();

                    string myAddress = AddressLine1.Trim().Replace("|", String.Empty) + "|"
                                + AddressLine2.Trim().Replace("|", String.Empty);

                    string myTown = Town.Trim();
                    string myPostcode = Postcode.Trim();
                    string myCountry = Country.Trim();

                    new SuppliersLogic().UpdateSupplier(mySupplierID, mySupplier, myEmail, myPostcode, myAddress, myTown, myCountry);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
