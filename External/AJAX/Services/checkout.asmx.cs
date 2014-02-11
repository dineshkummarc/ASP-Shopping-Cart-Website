using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Logic;
using Common;
using Common.Views;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for checkout
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class checkout : System.Web.Services.WebService
    {
        /// <summary>
        /// Loads all Shopping Cart Items
        /// Level: External
        /// </summary>
        /// <returns>HTML</returns>
        [WebMethod]
        public string LoadShoppingCartItems()
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    string HTML = "";

                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                    List<ShoppingCart> myShoppingCartItems = new ShoppingCartLogic().RetrieveAllShoppingCartItems(myLoggedUser.Id).ToList();

                    HTML += "<table>";

                    int Counter = 0;

                    foreach (ShoppingCart myShoppingCartItem in myShoppingCartItems)
                    {
                        UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myLoggedUser.UserTypeFK, myShoppingCartItem.ProductFK);

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
                        HTML += new ProductsLogic().RetrieveProductByID(myShoppingCartItem.ProductFK.ToString()).Name;
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<div style=\"padding-top: 4px; float: left;\">x&nbsp;&nbsp;</div><input class=\"CatalogTextBox\" id=\"" + myShoppingCartItem.ProductFK + "\" Use=\"Quantity\" type=\"text\" value=\"" + myShoppingCartItem.Quantity.ToString() + "\">";
                        HTML += "</td>";

                        HTML += "<td> at </td>";

                        HTML += "<td>";
                        HTML += "€ " + PriceOutput;
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<div Use=\"ErrorDiv\" ProductID=\"" + myShoppingCartItem.ProductFK + "\" class=\"MiniFontBlue\">Not Enough Stock</div>";
                        HTML += "</td>";

                        HTML += "</tr>";

                        Counter++;
                    }

                    HTML += "</table>";

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
        /// Checks for Out of Stock Products
        /// Level: External
        /// </summary>
        /// <returns>Array of Violating ID's</returns>
        [WebMethod]
        public string[] CheckProductQuantities()
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                    List<ShoppingCart> myShoppingCartItems = new ShoppingCartLogic().RetrieveAllShoppingCartItems(myLoggedUser.Id).ToList();

                    List<string> ViolatingIDs = new List<string>();

                    foreach (ShoppingCart myShoppingCartItem in myShoppingCartItems)
                    {
                        if (!new OrdersLogic().HasSufficientQuantity(myShoppingCartItem.ProductFK, myShoppingCartItem.Quantity))
                        {
                            ViolatingIDs.Add(myShoppingCartItem.ProductFK.ToString());
                        }
                    }

                    return ViolatingIDs.ToArray();
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
        /// Completes the Checkout Process
        /// Level: External
        /// </summary>
        /// <param name="CreditCard">Users Credit Card Number</param>
        [WebMethod]
        public void CompleteCheckout(string CreditCard)
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                    List<ShoppingCart> myShoppingCartItems = new ShoppingCartLogic().RetrieveAllShoppingCartItems(myLoggedUser.Id).ToList();
                    List<OrderItem> myOrderItems = new List<OrderItem>();

                    foreach (ShoppingCart myShoppingCartItem in myShoppingCartItems)
                    {
                        UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myLoggedUser.UserTypeFK, myShoppingCartItem.ProductFK);

                        OrderItem myOrderItem = new OrderItem();

                        myOrderItem.Id = myShoppingCartItem.ProductFK;

                        double myPrice = 0;

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

                        myOrderItem.Price = myPrice;

                        myOrderItem.Quantity = myShoppingCartItem.Quantity;

                        myOrderItems.Add(myOrderItem);
                    }

                    //if (
                    new OrdersLogic().AddOrder(null, myLoggedUser.Id, CreditCard.Trim(), myOrderItems);
                    //{
                        //new UsersLogic().InsertCreditCardNumber(CreditCard.Trim(), myLoggedUser.Id);
                        //new ShoppingCartLogic().EmptyCart(myLoggedUser.Id);

                    //}
                 }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Total Price for Checkout Items
        /// Level: External
        /// </summary>
        /// <returns>Total Price</returns>
        [WebMethod]
        public string RetrieveCartTotal()
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                    return "€" + new ShoppingCartLogic().RetrieveCartTotal(myLoggedUser.Id).ToString("F");
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

        [WebMethod]
        public string RetrieveUserOrder()
        {
            try
            {
                return new OrdersLogic().RetrieveLastUserOrder(Context.User.Identity.Name).ToString();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
