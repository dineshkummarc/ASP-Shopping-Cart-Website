using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using Common;

namespace External.administrator
{
    public partial class stockhistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ddlProduct.DataSource = new ProductsLogic().RetrieveAllProducts();
                    ddlProduct.DataTextField = "Name";
                    ddlProduct.DataValueField = "Id";
                    ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, new ListItem("Select Product", "0"));

                    gvStockHistory.DataSource = new OrdersLogic().RetrieveStockHistory();
                    gvStockHistory.DataBind();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Drop Down List Selected in Stock History is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue != "0")
            {
                gvStockHistory.DataSource = new OrdersLogic().RetrieveStockHistoryForProduct(Guid.Parse(ddlProduct.SelectedValue.ToString()));
                gvStockHistory.DataBind();
            }
            else
            {
                gvStockHistory.DataSource = new OrdersLogic().RetrieveStockHistory();
                gvStockHistory.DataBind();
            }
        }
    }
}