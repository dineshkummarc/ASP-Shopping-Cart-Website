using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using Common;

namespace External
{
    public partial class printorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    string myOrderID = Request.QueryString[0].ToString();
                    Guid myOrderGuid;

                    if (Guid.TryParse(myOrderID, out myOrderGuid))
                    {
                        Order myOrder = new OrdersLogic().RetrieveOrderByID(myOrderGuid);
                        IQueryable<OrderProduct> myOrderItems = new OrdersLogic().RetrieveItemsByOrderID(myOrderGuid);


                        bool HasAccess = false;

                        if(Context.User.IsInRole("Administrator"))
                        {
                            HasAccess = true;
                        }
                        else
                        {
                            if (myOrder.UserFK == new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name).Id)
                            {
                                HasAccess = true;
                            }
                        }

                        //User has access to the order
                        if (HasAccess)
                        {
                            string HTML = "<table style=\"font-family: Arial;\"  cellpadding=\"6\">";

                            HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "Order ID: ";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += myOrder.Id;
                                HTML += "</td>";
                            HTML += "</tr>";

                            
                            
                            if(myOrder.SupplierFK == null)
                            {
                                HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "Name: ";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += myOrder.User.Name + " " + myOrder.User.Surname;
                                HTML += "</td>";
                                HTML += "</tr>";

                                string[] Address = myOrder.User.StreetAddress.Split('|');

                                HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "Address: ";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += Address[0];
                                HTML += "</td>";
                                HTML += "</tr>";

                                HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += Address[1];
                                HTML += "</td>";
                                HTML += "</tr>";

                                HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "Town: ";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += myOrder.User.Town.Town1;
                                HTML += "</td>";
                                HTML += "</tr>";

                                HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "Country: ";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += myOrder.User.Town.Country.Country1;
                                HTML += "</td>";
                                HTML += "</tr>";
                            }
                            else
                            {
                                HTML += "<tr>";
                                HTML += "<td>";
                                HTML += "Supplier: ";
                                HTML += "</td>";
                                HTML += "<td>";
                                HTML += myOrder.Supplier.Supplier1;
                                HTML += "</td>";
                                HTML += "</tr>";
                            }
                            
                            
                            HTML += "<tr>";
                            HTML += "<td>";
                            HTML += "Status: ";
                            HTML += "</td>";
                            HTML += "<td>";
                            HTML += myOrder.OrderStatus.Status;
                            HTML += "</td>";


                            HTML += "</tr>";
                            HTML += "<tr>";
                            HTML += "<td>";
                            HTML += "Date Placed: ";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += myOrder.OrderDate;
                            HTML += "</td>";

                            HTML += "</tr>";

                            HTML += "</table>";

                            HTML += "<br/>";

                            HTML += "<table style=\"font-family: Arial;\"  cellpadding=\"6\">";

                            HTML += "<tr>";

                            HTML += "<td>";
                            HTML += "Product";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "Quantity";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "Price";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "VAT Rate";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "Discount (Incl.)";
                            HTML += "</td>";

                            HTML += "</tr>";

                            double TotalPrice = 0;
                            double TotalVat = 0;

                            foreach (OrderProduct myOrderItem in myOrderItems)
                            {
                                Product myProduct = new ProductsLogic().RetrieveProductByID(myOrderItem.ProductFK.ToString());

                                User myUser = null;
                                UserTypeProduct myPriceType = null;

                                if (myOrder.UserFK != null)
                                {
                                    myUser = new UsersLogic().RetrieveUserByID(Guid.Parse(myOrder.UserFK.ToString()));
                                    myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myUser.UserTypeFK, myProduct.Id);
                                }
                                else
                                {
                                    UserType myUserType = new UserTypesLogic().RetrieveUserTypeByName("Wholesaler");
                                    myPriceType = new PriceTypesLogic().RetrievePriceTypeByID(myUserType.Id, myProduct.Id);
                                }

                                HTML += "<tr>";

                                HTML += "<td>";
                                HTML += myProduct.Name;
                                HTML += "</td>";

                                HTML += "<td>";
                                HTML += " x " + myOrderItem.Quantity;
                                HTML += "</td>";

                                HTML += "<td>";
                                HTML += " at € " + myOrderItem.Price.ToString("F");
                                HTML += "</td>";

                                HTML += "<td>";
                                HTML += myProduct.Vatrate.Vatrate1 + "% VAT";
                                HTML += "</td>";

                                TotalPrice += myOrderItem.Price * myOrderItem.Quantity;
                                TotalVat += ((myProduct.Vatrate.Vatrate1 / 100) * (myOrderItem.Price * myOrderItem.Quantity));

                                HTML += "<td>";

                                if((myOrder.OrderDate >= myPriceType.DiscountDateFrom) && (myOrder.OrderDate <= myPriceType.DiscountDateTo))
                                {
                                    HTML += myPriceType.DiscountPercentage + "% Discount";
                                }

                                HTML += "</td>";

                                HTML += "</tr>";
                            }

                            HTML += "<tr>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "Subtotal : ";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "€ " + (TotalPrice - TotalVat).ToString("F");
                            HTML += "</td>";

                            HTML += "<tr>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "VAT : ";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "€ " + TotalVat.ToString("F");
                            HTML += "</td>";

                            HTML += "</tr>";
                            
                            HTML += "<tr>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "Total : ";
                            HTML += "</td>";

                            HTML += "<td>";
                            HTML += "€ " + TotalPrice.ToString("F");
                            HTML += "</td>";

                            HTML += "</tr>";
                            HTML += "</table>";

                            lblOutput.Text = HTML;
                        }
                        else
                        {
                            Response.Redirect("~/default.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/default.aspx");
                }

            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}