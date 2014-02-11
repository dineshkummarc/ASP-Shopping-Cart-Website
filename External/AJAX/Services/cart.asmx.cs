using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common;
using Logic;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for cart
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class cart : System.Web.Services.WebService
    {
        /// <summary>
        /// Loads all Shopping Cart Items
        /// Level: External
        /// </summary>
        /// <returns></returns>
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
                    List<Product> myProductList = new List<Product>();

                    foreach (ShoppingCart myShoppingCartItem in myShoppingCartItems)
                    {
                        myProductList.Add(new ProductsLogic().RetrieveProductByID(myShoppingCartItem.ProductFK.ToString()));
                    }

                    Product[] myProducts = myProductList.ToArray();

                    if (myProducts.Length > 0)
                    {
                        for (int i = 1; i <= (myProducts.Length + 4 / 4); i++)
                        {
                            int Counter = i * 4;

                            HTML += "<tr>";

                            for (int j = (Counter - 3); j <= Counter; j++)
                            {
                                HTML += "<td>";

                                HTML += TDContents(myProducts[j - 1]);

                                HTML += "</td>";

                                if (j == myProducts.Length)
                                {
                                    goto LoopEnd;
                                }
                            }

                            HTML += "</tr>";
                        }
                    }

                LoopEnd:

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
        /// Generates TD Contents for Each Product
        /// Level: External
        /// </summary>
        /// <param name="myProduct">The Product to Display</param>
        /// <returns>HTML</returns>
        private string TDContents(Product myProduct)//ProductsView myCurrentProduct
        {
            try
            {
                string Name = myProduct.Name;
                string ImageURL = VirtualPathUtility.ToAbsolute(myProduct.ImageURL);
                string ProductID = myProduct.Id.ToString();
                string ProductLink = "/user/viewproduct.aspx?id=" + new Encryption().Encrypt(myProduct.Id.ToString());

                User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myLoggedUser.UserTypeFK, myProduct.Id);
                ShoppingCart myShoppingCartItem = new ShoppingCartLogic().RetrieveShoppingCartItemByID(myLoggedUser.Id, myProduct.Id);

                string Price = "";

                if (myPriceType != null)
                {
                    Price = "€" + myPriceType.Price.ToString("F");
                    double? NewPrice = 0;

                    if ((myPriceType.DiscountDateFrom != null) && (myPriceType.DiscountDateTo != null) && (myPriceType.DiscountPercentage != null))
                    {
                        if ((DateTime.Now >= myPriceType.DiscountDateFrom) && (DateTime.Now <= myPriceType.DiscountDateTo))
                        {
                            NewPrice = myPriceType.Price - ((myPriceType.DiscountPercentage / 100) * myPriceType.Price);

                            string myDisplayedNewPrice = Convert.ToDouble(NewPrice).ToString("F");

                            Price = myPriceType.DiscountPercentage + "% Off : €" + myDisplayedNewPrice;
                        }
                    }
                }

                //string Price = new ProductsLogic().RetrieveProductPrice(_UserType, myProduct.Id).ToString("F");
                string ButtonHTML = "<input type=\"image\" class=\"ProductButton\" alt=\"\" ProductID=\"" + ProductID + "\" ClickAction=\"Add\" src=\"/images/Add.jpg\" onclick=\"return false;\" />" +
                                    "<div class=\"ProductButtonSpacer\"></div>" +
                                    "<input type=\"image\" class=\"ProductButton\" alt=\"\" ProductID=\"" + ProductID + "\" ClickAction=\"Remove\" src=\"/images/Remove.jpg\" onclick=\"return false;\"/>";
                string OutOfStock = "No Stock &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                string LowOnStock = "Low Stock &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                string TextBox = "<div class=\"MiniFontGrey\"><div style=\"padding-top: 5px; float: left;\">Quantity&nbsp;&nbsp;</div><input type=\"text\" value=\"" + myShoppingCartItem.Quantity + "\" name=\"" + ProductID + "\" class=\"CatalogTextBox\"></div>" +
                                 "<br />";

                if (myProduct.StockQuantity == 0)
                {
                    ButtonHTML = "";
                    LowOnStock = "";
                    TextBox = "<div style=\"height: 27px; width: 1px\"></div>";
                }
                else if (myProduct.StockQuantity <= myProduct.ReorderLevel)
                {
                    OutOfStock = "";
                }
                else
                {
                    LowOnStock = "";
                    OutOfStock = "";
                }

                string HTML = "<div>" +
                                  "<div class=\"ProductContent\">" +
                                      "<img class=\"ProductImage\" alt=\"\" src=\"" + ImageURL + "\" />" +
                                      "<br />" +
                                      "<a href=\"" + ProductLink + "\" target=\"_blank\" class=\"MiniFontGrey\">" + Name + "</a><br />" +
                                   "<div class=\"MiniFontBlue\">" + OutOfStock + LowOnStock + Price + "</div>" +
                                   "<br />" +
                                   TextBox +
                               "</div>" +
                               "<div class=\"ProductButtons\">" +
                                    ButtonHTML +
                               "</div>";

                return HTML;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
