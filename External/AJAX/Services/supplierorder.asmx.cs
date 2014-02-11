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
    /// Summary description for supplierorder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class supplierorder : System.Web.Services.WebService
    {
        /// <summary>
        /// Retrieves Shopping List Items
        /// Level: External
        /// </summary>
        /// <returns>HTML</returns>
        [WebMethod(EnableSession = true)]
        public string RetrieveItems()
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string HTML = "";

                    HTML += "<table id=\"Order\" cellpadding=\"4\">";

                    List<OrderItem> myList = (List<OrderItem>)Session["SupplierOrder"];

                    foreach (OrderItem myOrderItem in myList)
                    {
                        UserType myUserType = new UserTypesLogic().RetrieveUserTypeByName("Wholesaler");
                        UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myUserType.Id, myOrderItem.Id);

                        string PriceOutput = "";

                        if (myPriceType != null)
                        {
                            PriceOutput = myPriceType.Price.ToString("F");
                            double? NewPrice = 0;

                            if ((myPriceType.DiscountDateFrom != null) && (myPriceType.DiscountDateTo != null) && (myPriceType.DiscountPercentage != null))
                            {
                                if ((DateTime.Now >= myPriceType.DiscountDateFrom) && (DateTime.Now <= myPriceType.DiscountDateTo))
                                {
                                    NewPrice = myPriceType.Price - ((myPriceType.DiscountPercentage / 100) * myPriceType.Price);

                                    string myDisplayedNewPrice = Convert.ToDouble(NewPrice).ToString("F");

                                    PriceOutput = myDisplayedNewPrice + " : " + myPriceType.DiscountPercentage + " % Off";
                                }
                            }
                        }

                        HTML += "<tr class=\"GridViewTuple\">";

                        HTML += "<td>";
                        HTML += new ProductsLogic().RetrieveProductByID(myOrderItem.Id.ToString()).Name;
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "x " + myOrderItem.Quantity.ToString();
                        HTML += "</td>";

                        HTML += "<td> at </td>";

                        HTML += "<td>";
                        HTML += "€ " + PriceOutput;
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<input type=\"image\" class=\"ProductButton\" alt=\"\" productid=\"" + myOrderItem.Id.ToString() + "\" src=\"/images/Remove.jpg\" onclick=\"return false;\" />";
                        HTML += "</td>";

                        HTML += "</tr>";
                    }

                    double myTotalPrice = new CalculateTotalPrice().CalculateTotal(myList);

                    HTML += "<tr class=\"GridViewHeader\"><td></td><td>Total</td><td>=</td><td><div class=\"MiniFontBlueLeft\">€ " + myTotalPrice.ToString("F") + "<div></td></tr>";

                    HTML += "<table>";

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
        /// Retrieves Suppliers For Drop Down
        /// Level: External
        /// </summary>
        /// <returns>HTML</returns>
        [WebMethod]
        public string PopulateSuppliers()
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    List<SuppliersView> mySuppliers = new SuppliersLogic().RetrieveAllSuppliers().ToList();
                    string HTML = "<option value=\"0\">Select</option>";

                    foreach (SuppliersView mySupplier in mySuppliers)
                    {
                        HTML += "<option value=\"" + mySupplier.Id + "\">" + mySupplier.Supplier + "</option>";
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
        /// Populates Products For Supplier
        /// Level: External
        /// </summary>
        /// <param name="SupplierID">The Supplier ID</param>
        /// <returns>HTML</returns>
        [WebMethod(EnableSession = true)]
        public string PopulateProducts(string SupplierID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int mySupplierID = Convert.ToInt32(SupplierID);

                    List<OrderItem> myList = (List<OrderItem>)Session["SupplierOrder"];

                    List<Product> myProducts = new ProductsLogic().RetrieveProductsForDisplayBySupplier(mySupplierID).ToList();
                    string HTML = "<option value=\"0\">Select</option>";

                    foreach (Product myProduct in myProducts)
                    {
                        if (myList.SingleOrDefault(oi => oi.Id == myProduct.Id) == null)
                        {
                            HTML += "<option value=\"" + myProduct.Id + "\">" + myProduct.Name + "</option>";
                        }
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
        /// Retrieves the Current Product Price
        /// Level: External
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>Product Price</returns>
        [WebMethod]
        public string RetrieveProductPrice(string ProductID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    Guid myProductID = Guid.Parse(ProductID);

                    UserType myUserType = new UserTypesLogic().RetrieveUserTypeByName("Wholesaler");
                    UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myUserType.Id, myProductID);

                    string Price = "";

                    if (myPriceType != null)
                    {
                        Price = myPriceType.Price.ToString("F");
                        double? NewPrice = 0;

                        if ((myPriceType.DiscountDateFrom != null) && (myPriceType.DiscountDateTo != null) && (myPriceType.DiscountPercentage != null))
                        {
                            if ((DateTime.Now >= myPriceType.DiscountDateFrom) && (DateTime.Now <= myPriceType.DiscountDateTo))
                            {
                                NewPrice = myPriceType.Price - ((myPriceType.DiscountPercentage / 100) * myPriceType.Price);

                                string myDisplayedNewPrice = Convert.ToDouble(NewPrice).ToString("F");

                                Price = myDisplayedNewPrice + " : " + myPriceType.DiscountPercentage + " % Off";
                            }
                        }
                    }

                    return Price;
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
        /// Adds a new Item to Shopping List
        /// Level: External
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="Price">The Product Price</param>
        /// <param name="Quantity">The Quantity</param>
        [WebMethod(EnableSession = true)]
        public void AddItemToList(string ProductID, string Price, string Quantity)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    Guid myProductID = Guid.Parse(ProductID);
                    //double myPrice = Convert.ToDouble(Price.Replace('€', ' ').Trim());
                    int myQuantity = Convert.ToInt32(Quantity);

                    double myPrice = 0;

                    UserType myUserType = new UserTypesLogic().RetrieveUserTypeByName("Wholesaler");
                    UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myUserType.Id, myProductID);

                    if (myPriceType != null)
                    {
                        myPrice = myPriceType.Price;
                        double? NewPrice = 0;

                        if ((myPriceType.DiscountDateFrom != null) && (myPriceType.DiscountDateTo != null) && (myPriceType.DiscountPercentage != null))
                        {
                            if ((DateTime.Now >= myPriceType.DiscountDateFrom) && (DateTime.Now <= myPriceType.DiscountDateTo))
                            {
                                NewPrice = myPriceType.Price - ((myPriceType.DiscountPercentage / 100) * myPriceType.Price);
                                myPrice = Convert.ToDouble(NewPrice);
                            }
                        }
                    }

                    List<OrderItem> myList = (List<OrderItem>)Session["SupplierOrder"];

                    OrderItem myOrderItem = new OrderItem();

                    myOrderItem.Id = myProductID;
                    myOrderItem.Price = myPrice;
                    myOrderItem.Quantity = myQuantity;

                    myList.Add(myOrderItem);

                    Session["SupplierOrder"] = myList;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes an Item from the Shopping List
        /// Level: External
        /// </summary>
        /// <param name="ProductID">Product ID</param>
        [WebMethod(EnableSession = true)]
        public void RemoveItemFromList(string ProductID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    Guid myProductID = Guid.Parse(ProductID);

                    List<OrderItem> myList = (List<OrderItem>)Session["SupplierOrder"];

                    List<OrderItem> myNewList = new List<OrderItem>();

                    foreach (OrderItem myOrderItem in myList)
                    {
                        if (myOrderItem.Id != myProductID)
                        {
                            myNewList.Add(myOrderItem);
                        }
                    }

                    Session["SupplierOrder"] = myNewList;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Clears the Shopping List
        /// Level: External
        /// </summary>
        [WebMethod(EnableSession = true)]
        public void ClearList()
        {
            try
            {
                Session["SupplierOrder"] = new List<OrderItem>();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Places a Supplier Order
        /// Level: External
        /// </summary>
        /// <param name="SupplierID">The Supplier ID</param>
        [WebMethod(EnableSession = true)]
        public void PlaceOrder(string SupplierID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int mySupplierID = Convert.ToInt32(SupplierID);

                    List<OrderItem> myList = (List<OrderItem>)Session["SupplierOrder"];

                    new OrdersLogic().AddOrder(mySupplierID, null, null, myList);

                    Session["SupplierOrder"] = new List<OrderItem>();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
