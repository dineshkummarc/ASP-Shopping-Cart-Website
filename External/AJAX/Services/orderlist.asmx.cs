using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Logic;
using Common;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for orderlist
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class orderlist : System.Web.Services.WebService
    {
        /// <summary>
        /// Loads all Order Products
        /// Level: External
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <returns>HTML</returns>
        [WebMethod]
        public string LoadProducts(string OrderID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string HTML = "";

                    Guid myOrderID = Guid.Parse(OrderID.Trim());

                    List<OrderProduct> myProductItems = new OrdersLogic().RetrieveItemsByOrderID(myOrderID).ToList();

                    HTML += "<table>";

                    int Counter = 0;

                    foreach (OrderProduct myProductItem in myProductItems)
                    {
                        HTML += "<tr class=\"GridViewTuple\">";

                        HTML += "<td>";
                        HTML += new ProductsLogic().RetrieveProductByID(myProductItem.ProductFK.ToString()).Name;
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<div style=\"padding-top: 4px; float: left;\">x&nbsp;&nbsp;</div><input class=\"CatalogTextBox\" id=\"" + myProductItem.ProductFK + "\" Use=\"Quantity\" type=\"text\" value=\"" + myProductItem.Quantity.ToString() + "\">";
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<input type=\"button\" ProductID=\"" + myProductItem.ProductFK + "\" Use=\"Update\" value=\"Update\">";
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<input type=\"button\" ProductID=\"" + myProductItem.ProductFK + "\" Use=\"Remove\" value=\"Remove\">";
                        HTML += "</td>";

                        HTML += "<td>";
                        HTML += "<div Use=\"ErrorDiv\" ProductID=\"" + myProductItem.ProductFK + "\" class=\"MiniFontBlue\">Not Enough Stock</div>";
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
        /// Updates an Order Item
        /// Level: External
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="OrderType">The Type of Order</param>
        /// <param name="Quantity">The Quantity</param>
        /// <returns>If true Update Successful. If false Update Unsuccessful.</returns>
        [WebMethod]
        public bool UpdateItem(string OrderID, string ProductID, string OrderType, string Quantity)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    Guid myOrderID = Guid.Parse(OrderID);
                    Guid myProductID = Guid.Parse(ProductID);
                    int myQuantity = Convert.ToInt32(Quantity);

                    bool UpdateSuccessful = false;

                    if (OrderType.Trim() == "User")
                    {
                        UpdateSuccessful = new OrdersLogic().UpdateUserOrderItem(myOrderID, myProductID, myQuantity);

                        return UpdateSuccessful;
                    }
                    else if (OrderType.Trim() == "Supplier")
                    {
                        UpdateSuccessful = new OrdersLogic().UpdateSupplierOrderItem(myOrderID, myProductID, myQuantity);

                        return UpdateSuccessful;
                    }

                    return UpdateSuccessful;
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
        /// Deletes an Order Item
        /// Level: External
        /// </summary>
        /// <param name="OrderID">The Order ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="OrderType">The Order Type</param>
        /// <returns>If remove successful return true. Else return false.</returns>
        [WebMethod]
        public bool RemoveItem(string OrderID, string ProductID, string OrderType)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    Guid myOrderID = Guid.Parse(OrderID);
                    Guid myProductID = Guid.Parse(ProductID);

                    bool UpdateSuccessful = false;

                    if (OrderType.Trim() == "User")
                    {
                        new OrdersLogic().RemoveUserOrderItem(myOrderID, myProductID);

                        return true;
                    }
                    else if (OrderType.Trim() == "Supplier")
                    {
                        UpdateSuccessful = new OrdersLogic().RemoveSupplierOrderItem(myOrderID, myProductID);

                        return UpdateSuccessful;
                    }

                    return UpdateSuccessful;
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
