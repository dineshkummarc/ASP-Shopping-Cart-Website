using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common;
using Common.Views;
using Logic;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for pricetype
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class pricetype : System.Web.Services.WebService
    {
        /// <summary>
        /// Populates User Types Dropdown
        /// Level: External
        /// </summary>
        /// <returns>HTML</returns>
        [WebMethod]
        public string PopulateUserTypes()
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    List<UserType> myUserTypes = new UserTypesLogic().RetrieveAllUserTypes().ToList();
                    string HTML = "<option value=\"0\"> Select </option>";

                    foreach (UserType myType in myUserTypes)
                    {
                        HTML += "<option value=\"" + myType.Id + "\">" + myType.Type + "</option>";
                    }

                    return HTML;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Populates Product Dropdown
        /// Level: External
        /// </summary>
        /// <returns>HTML</returns>
        [WebMethod]
        public string PopulateProducts()
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    List<ProductsView> myProducts = new ProductsLogic().RetrieveAllProducts().ToList();
                    string HTML = "<option value=\"0\"> Select </option>";

                    foreach (ProductsView myProduct in myProducts)
                    {
                        HTML += "<option value=\"" + myProduct.Id + "\">" + myProduct.Name + "</option>";
                    }

                    return HTML;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Populates Pricetype Fields
        /// Level: External
        /// </summary>
        /// <param name="UserTypeID"></param>
        /// <param name="ProductID"></param>
        /// <returns>String[] Containing Field Data</returns>
        [WebMethod]
        public string[] PopulateFields(string UserTypeID, string ProductID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    if ((UserTypeID == "0") || (ProductID == "0"))
                    {
                        return null;
                    }
                    else
                    {
                        int myUserTypeID = Convert.ToInt32(UserTypeID);
                        Guid myProductID = Guid.Parse(ProductID);

                        UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myUserTypeID, myProductID);

                        if (myPriceType != null)
                        {
                            string PriceTypePercentage;

                            if (myPriceType.DiscountPercentage == null)
                            {
                                PriceTypePercentage = null;
                            }
                            else
                            {
                                PriceTypePercentage = myPriceType.DiscountPercentage.ToString();
                            }

                            return new string[4] { myPriceType.Price.ToString(), new DateTimeParser().ParseDateToString(myPriceType.DiscountDateFrom), 
                    new DateTimeParser().ParseDateToString(myPriceType.DiscountDateTo), PriceTypePercentage };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return new string[] { "" };
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a Price Type
        /// Level: External
        /// </summary>
        /// <param name="UserTypeID">The UserType ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="Price">The Price</param>
        /// <param name="DiscountStart">The Discount Start Date</param>
        /// <param name="DiscountEnd">The Discount End Date</param>
        /// <param name="DiscountPercentage">The Discount Percentage</param>
        /// <returns>True if Added. False if not.</returns>
        [WebMethod]
        public bool AddPriceType(string UserTypeID, string ProductID, string Price, 
            string DiscountStart, string DiscountEnd, string DiscountPercentage)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int myUserTypeID = Convert.ToInt32(UserTypeID);
                    Guid myProductID = Guid.Parse(ProductID);
                    double myPrice = Convert.ToDouble(Price);

                    if ((DiscountStart != string.Empty) && (DiscountEnd != string.Empty) && (DiscountPercentage != string.Empty))
                    {
                        DateTime? myDiscountStart = new DateTimeParser().ParseDate(DiscountStart.Trim().Replace('/', '-'));
                        DateTime? myDiscountEnd = new DateTimeParser().ParseDate(DiscountEnd.Trim().Replace('/', '-'));
                        double? myDiscountPercentage = Convert.ToDouble(DiscountPercentage);

                        if (myDiscountStart < myDiscountEnd)
                        {
                            new PriceTypesLogic().AddPriceType(myUserTypeID, myProductID, myPrice, myDiscountStart, myDiscountEnd, myDiscountPercentage);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        new PriceTypesLogic().AddPriceType(myUserTypeID, myProductID, myPrice, null, null, null);
                        return true;
                    }
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
        /// Updates a Price Type
        /// Level: External
        /// </summary>
        /// <param name="UserTypeID">The User Type ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="Price">The Price</param>
        /// <param name="DiscountStart">The Discount Start Date</param>
        /// <param name="DiscountEnd">The Discount End Date</param>
        /// <param name="DiscountPercentage">The Discount Percentage</param>
        /// <returns>True if updated. False if not.</returns>
        [WebMethod]
        public bool UpdatePriceType(string UserTypeID, string ProductID, string Price,
            string DiscountStart, string DiscountEnd, string DiscountPercentage)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int myUserTypeID = Convert.ToInt32(UserTypeID);
                    Guid myProductID = Guid.Parse(ProductID);
                    double myPrice = Convert.ToDouble(Price);

                    if ((DiscountStart != string.Empty) && (DiscountEnd != string.Empty) && (DiscountPercentage != string.Empty))
                    {
                        DateTime? myDiscountStart = new DateTimeParser().ParseDate(DiscountStart.Trim().Replace('/', '-'));
                        DateTime? myDiscountEnd = new DateTimeParser().ParseDate(DiscountEnd.Trim().Replace('/', '-'));
                        double? myDiscountPercentage = Convert.ToDouble(DiscountPercentage);

                        if (myDiscountStart < myDiscountEnd)
                        {
                            new PriceTypesLogic().UpdatePriceType(myUserTypeID, myProductID, myPrice, myDiscountStart, myDiscountEnd, myDiscountPercentage);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        new PriceTypesLogic().UpdatePriceType(myUserTypeID, myProductID, myPrice, null, null, null);
                        return true;
                    }
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
        /// Deletes a Price Type
        /// Level: External
        /// </summary>
        /// <param name="UserTypeID">The User Type ID</param>
        /// <param name="ProductID">The Product ID</param>
        [WebMethod]
        public void DeletePriceType(string UserTypeID, string ProductID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int myUserTypeID = Convert.ToInt32(UserTypeID);
                    Guid myProductID = Guid.Parse(ProductID);

                    new PriceTypesLogic().RemoveSpecificPriceType(myUserTypeID, myProductID);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
