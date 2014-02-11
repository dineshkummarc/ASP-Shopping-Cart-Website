using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using Common.Views;

namespace External.administrator
{
    public partial class printtopten : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string StartDate = null;
                string EndDate = null;
                string Users = null;

                if((Request.QueryString["sd"] != null) && (Request.QueryString["ed"] != null))
                {
                    StartDate = Request.QueryString["sd"].ToString();
                    EndDate = Request.QueryString["ed"].ToString();
                }
                else if (Request.QueryString["u"] != null)
                {
                    Users = Request.QueryString["u"].ToString();
                }
                else
                {
                    Response.Redirect("~/pagenotfound.aspx");
                }


                string HTML = "";

                if (Users == null)
                {
                    DateTime myStartDate = new DateTime();
                    DateTime myEndDate = new DateTime();

                    try
                    {
                        myStartDate = new DateTimeParser().ParseDate(StartDate.Replace('/', '-'));
                        myEndDate = new DateTimeParser().ParseDate(EndDate.Replace('/', '-'));
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/pagenotfound.aspx");
                    }

                    if (myStartDate > myEndDate)
                    {
                        DateTime myInitialStartDate = myStartDate;
                        DateTime myInitialEndDate = myEndDate;

                        myStartDate = myInitialEndDate;
                        myEndDate = myInitialStartDate;
                    }

                    IQueryable<TopTenView> myTopTenList = new OrdersLogic().RetrieveTopTen(myStartDate, myEndDate);

                    HTML = "<table style=\"font-family: Arial;\" cellpadding=\"6\">";

                    HTML += "<tr>";
                    HTML += "<td>";
                    HTML += "Start Date:";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += myStartDate.ToShortDateString();
                    HTML += "</td>";
                    HTML += "</tr>";

                    HTML += "<tr>";
                    HTML += "<td>";
                    HTML += "End Date:";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += myEndDate.ToShortDateString();
                    HTML += "</td>";
                    HTML += "</tr>";

                    HTML += "</table>";

                    HTML += "<br/>";
                    HTML += "<br/>";

                    HTML += "<table style=\"font-family: Arial;\" cellpadding=\"6\">";

                    HTML += "<tr>";
                    HTML += "<td>";
                    HTML += "Rank";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += "Product Name";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += "Product Description";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += "Quantity Bought";
                    HTML += "</td>";
                    HTML += "<tr>";

                    int Counter = 0;

                    foreach (TopTenView myTopTenItem in myTopTenList)
                    {
                        Counter++;

                        if (Counter > 10)
                        {
                            break;
                        }

                        HTML += "<tr>";

                        HTML += "<td>";
                        HTML += Counter;
                        HTML += "</td>";
                        HTML += "<td>";
                        HTML += myTopTenItem.ProductName;
                        HTML += "</td>";
                        HTML += "<td>";
                        HTML += myTopTenItem.ProductDescription;
                        HTML += "</td>";
                        HTML += "<td>";
                        HTML += myTopTenItem.QuantitySold;
                        HTML += "</td>";

                        HTML += "</tr>";
                    }

                    HTML += "</table>";

                }
                else
                {
                    IQueryable<TopTenClients> myTopTenList = new OrdersLogic().RetrieveTopTenClients();

                    HTML += "<table style=\"font-family: Arial;\" cellpadding=\"6\">";

                    HTML += "<tr>";
                    HTML += "<td>";
                    HTML += "Rank";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += "Client Name";
                    HTML += "</td>";
                    HTML += "<td>";
                    HTML += "Quantity Bought";
                    HTML += "</td>";
                    HTML += "<tr>";

                    int Counter = 0;

                    foreach (TopTenClients myTopTenItem in myTopTenList)
                    {
                        Counter++;

                        if (Counter > 10)
                        {
                            break;
                        }

                        HTML += "<tr>";

                        HTML += "<td>";
                        HTML += Counter;
                        HTML += "</td>";
                        HTML += "<td>";
                        HTML += myTopTenItem.ClientName;
                        HTML += "</td>";
                        HTML += "<td>";
                        HTML += myTopTenItem.ProductCount;
                        HTML += "</td>";

                        HTML += "</tr>";
                    }

                    HTML += "</table>";
                }
                
                lblOutput.Text = HTML;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}