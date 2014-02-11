using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using Common;
using Common.Views;

namespace External.administrator
{
    public partial class orderlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtSupplierID.Attributes.Add("readonly", "readonly");
                    txtUserID.Attributes.Add("readonly", "readonly");

                    ddlOrderStatus.DataSource = new OrdersLogic().RetrieveAllOrderStatuses();
                    ddlOrderStatus.DataTextField = "Status";
                    ddlOrderStatus.DataValueField = "Id";
                    ddlOrderStatus.DataBind();

                    ddlOrderStatus.Visible = false;
                    txtSupplierID.Visible = false;
                    txtUserID.Visible = false;
                    btnUpdate.Visible = false;
                    lblOrder.Visible = false;
                    btnPrintContents.Visible = false;
                    btnEditContents.Visible = false;

                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the User Search Button is Clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUserSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text.Trim() != "")
                {
                    gvUserOrders.SelectedIndex = -1;
                    ddlOrderStatus.Visible = false;
                    txtSupplierID.Text = "";
                    txtUserID.Text = "";
                    txtSupplierID.Visible = false;
                    txtUserID.Visible = false;
                    gvUserOrders.Visible = true;
                    gvSupplierOrders.Visible = false;
                    gvUserOrders.DataSource = new OrdersLogic().RetrieveOrdersByUsername(txtUsername.Text.Trim());
                    gvUserOrders.DataBind();
                    txtSupplier.Text = "";
                    btnUpdate.Visible = false;
                    lblOrder.Visible = false;

                    btnPrintContents.Visible = false;
                    btnEditContents.Visible = false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the supplier search button is Clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSupplierSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplier.Text.Trim() != "")
                {
                    gvSupplierOrders.SelectedIndex = -1;
                    ddlOrderStatus.Visible = false;
                    txtUserID.Text = "";
                    txtSupplierID.Text = "";
                    txtSupplierID.Visible = false;
                    txtUserID.Visible = false;
                    gvUserOrders.Visible = false;
                    gvSupplierOrders.Visible = true;
                    gvSupplierOrders.DataSource = new OrdersLogic().RetrieveOrdersBySupplier(txtSupplier.Text.Trim());
                    gvSupplierOrders.DataBind();
                    txtUsername.Text = "";
                    btnUpdate.Visible = false;
                    lblOrder.Visible = false;

                    btnPrintContents.Visible = false;
                    btnEditContents.Visible = false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Supplier Orders Grid View Selected Index is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSupplierOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSupplierID.Visible = true;
                txtSupplierID.Text = gvSupplierOrders.SelectedValue.ToString();

                Order myOrder = new OrdersLogic().RetrieveOrderByID(Guid.Parse(gvSupplierOrders.SelectedValue.ToString()));

                txtUserID.Text = "";
                ddlOrderStatus.Visible = true;
                ddlOrderStatus.SelectedValue = myOrder.OrderStatus.Id.ToString();
                btnUpdate.Visible = true;
                lblOrder.Visible = true;


                btnPrintContents.Visible = true;
                btnEditContents.Visible = true;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the User Orders Grid View Selected Index is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUserOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtUserID.Visible = true;
                txtUserID.Text = gvUserOrders.SelectedValue.ToString();

                Order myOrder = new OrdersLogic().RetrieveOrderByID(Guid.Parse(gvUserOrders.SelectedValue.ToString()));

                txtSupplierID.Text = "";
                ddlOrderStatus.Visible = true;
                ddlOrderStatus.SelectedValue = myOrder.OrderStatus.Id.ToString();
                btnUpdate.Visible = true;
                lblOrder.Visible = true;

                btnPrintContents.Visible = true;
                btnEditContents.Visible = true;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Order Update Button is Clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtSupplierID.Text == "") && (txtUserID.Text != ""))
                {
                    new OrdersLogic().UpdateOrderStatus(Guid.Parse(gvUserOrders.SelectedValue.ToString()), Convert.ToInt32(ddlOrderStatus.SelectedValue));
                    gvUserOrders.DataSource = new OrdersLogic().RetrieveOrdersByUsername(txtUsername.Text.Trim());
                    gvUserOrders.DataBind();
                }
                else if ((txtSupplierID.Text != "") && (txtUserID.Text == ""))
                {
                    new OrdersLogic().UpdateOrderStatus(Guid.Parse(gvSupplierOrders.SelectedValue.ToString()), Convert.ToInt32(ddlOrderStatus.SelectedValue));
                    gvSupplierOrders.DataSource = new OrdersLogic().RetrieveOrdersBySupplier(txtSupplier.Text.Trim());
                    gvSupplierOrders.DataBind();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}