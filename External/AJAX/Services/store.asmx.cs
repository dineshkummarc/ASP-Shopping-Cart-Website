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
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class store : System.Web.Services.WebService
    {
        private string _UserType;

        /// <summary>
        /// Appends products to the store
        /// Level: External
        /// </summary>
        /// <param name="ParentID">The Parent ID</param>
        /// <param name="ChildID">The Child ID</param>
        /// <param name="SearchText">The Search Text</param>
        /// <param name="LoadAll">If Products Should All Be Loaded</param>
        /// <returns>HTML</returns>
        [WebMethod]
        public string AppendProducts(string ParentID, string ChildID, string SearchText, string LoadAll)
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    int myParentID = 0;
                    int myChildID = 0;

                    if ((ParentID.Trim() != "") && (ChildID.Trim() != ""))
                    {
                        myParentID = Convert.ToInt32(ParentID.Trim());
                        myChildID = Convert.ToInt32(ChildID.Trim());
                    }

                    string HTML = "";

                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                    _UserType = myLoggedUser.UserType.Type;

                    Product[] myProducts = null;

                    if (SearchText == "")
                    {
                        if (myChildID == 0)
                        {
                            //parent only
                            myProducts = new ProductsLogic().RetrieveProductsForDisplayByUser(myLoggedUser.UserTypeFK, myParentID, null).ToArray();
                        }
                        else
                        {
                            //child
                            myProducts = new ProductsLogic().RetrieveProductsForDisplayByUser(myLoggedUser.UserTypeFK, null, myChildID).ToArray();
                        }
                    }
                    else
                    {
                        if (LoadAll == "false")
                        {
                            myProducts = new ProductsLogic().RetrieveProductsForDisplayBySearch(myLoggedUser.UserTypeFK, SearchText).ToArray();
                        }
                    }

                    if (LoadAll == "true")
                    {
                        myProducts = new ProductsLogic().RetrieveProductsForDisplayByUserType(myLoggedUser.UserTypeFK).ToArray();
                    }

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
        /// Generates TD Contents for Product
        /// Level: External
        /// </summary>
        /// <param name="myProduct">The Product</param>
        /// <returns>HTML</returns>
        private string TDContents(Product myProduct) //ProductsView myCurrentProduct
        {
            try
            {
                string Name = myProduct.Name;
                string ImageURL = VirtualPathUtility.ToAbsolute(myProduct.ImageURL);
                string ProductID = myProduct.Id.ToString();
                string ProductLink = "/user/viewproduct.aspx?id=" + new Encryption().Encrypt(myProduct.Id.ToString());

                User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myLoggedUser.UserTypeFK, myProduct.Id);

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
                string TextBox = "<div class=\"MiniFontGrey\"><div style=\"padding-top: 5px; float: left;\">Quantity&nbsp;&nbsp;</div><input type=\"text\" name=\"" + ProductID + "\" class=\"CatalogTextBox\"></div>" +
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

        /// <summary>
        /// Retrieves Parent Categories for Drop Down
        /// Level: External
        /// </summary>
        /// <returns>HTML</returns>
        [WebMethod]
        public string RetrieveParentCategories()
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    List<Category> myCategories = new CategoriesLogic().RetrieveParentCategories().ToList();
                    string HTML = "";

                    foreach (Category myCategory in myCategories)
                    {
                        HTML += "<option value=\"" + myCategory.Id + "\">" + myCategory.Category1 + "</option>";
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
        /// Retrieves Child Categories for Drop Down
        /// Level: External
        /// </summary>
        /// <param name="ParentID">The Parent ID</param>
        /// <returns>HTML</returns>
        [WebMethod]
        public string RetrieveChildCategories(string ParentID)
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    List<Category> myCategories = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(ParentID)).ToList();
                    string HTML = "<option value=\"0\">Select</option>";

                    foreach (Category myCategory in myCategories)
                    {
                        HTML += "<option value=\"" + myCategory.Id + "\">" + myCategory.Category1 + "</option>";
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
        /// Adds an Item to the Cart
        /// Level: External
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="Quantity">The Quantity</param>
        [WebMethod]
        public void AddToCart(string ProductID, string Quantity)
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    Guid myProductID = Guid.Parse(ProductID.Trim());
                    int myQuantity = 0;

                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);

                    if (Quantity.Trim() != string.Empty)
                    {
                        myQuantity = Convert.ToInt32(Quantity.Trim());
                    }
                    else
                    {
                        myQuantity = 1;
                    }

                    new ShoppingCartLogic().AddToCart(myLoggedUser.Id, myProductID, myQuantity);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes an Item From the Cart
        /// Level: External
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        [WebMethod]
        public void RemoveFromCart(string ProductID)
        {
            try
            {
                if (Context.User.IsInRole("User"))
                {
                    Guid myProductID = Guid.Parse(ProductID.Trim());

                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);

                    new ShoppingCartLogic().RemoveFromCart(myLoggedUser.Id, myProductID);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
