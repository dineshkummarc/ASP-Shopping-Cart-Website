using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Logic;

namespace External
{
    public partial class viewproduct : System.Web.UI.Page
    {
        protected bool _StockAvailable = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string myProductID = null;

                try
                {
                    myProductID = new Encryption().Decrypt(Request.QueryString[0].ToString());
                }
                catch (Exception)
                {
                    Response.Redirect("~/pagenotfound.aspx");
                }

                Guid myProductGuid;

                if (Guid.TryParse(myProductID, out myProductGuid))
                {
                    Product myProduct = new ProductsLogic().RetrieveProductByID(myProductID);
                    User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                    UserTypeProduct myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myLoggedUser.UserTypeFK, myProduct.Id);

                    this.Title = "the Great Supermarket | " + myProduct.Name;
                    txtPageTitle.Text = "Viewing: " + myProduct.Name;

                    hdnProductID.Value = myProduct.Id.ToString();

                    imgProduct.ImageUrl = myProduct.ImageURL;
                    lblProductName.Text = myProduct.Name;
                    lblDescription.Text = myProduct.Description;

                    string Price = "";
                    string Stock = "";

                    if (myProduct.StockQuantity == 0)
                    {
                        Stock = "No Stock";
                        txtQuantity.Visible = false;
                        lblQuantity.Visible = false;
                        _StockAvailable = false;
                    }
                    else if (myProduct.StockQuantity <= myProduct.ReorderLevel)
                    {
                        Stock = "Low Stock";
                    }

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

                    lblPrice.Text = Stock + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Price;
                }
                else
                {
                    Response.Redirect("~/pagenotfound.aspx");
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}