using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Logic;

namespace External.user.administrator
{
    public partial class supplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    gvSuppliers.DataSource = new SuppliersLogic().RetrieveAllSuppliers();
                    gvSuppliers.DataBind();

                    ddlCountry.DataSource = new SuppliersLogic().RetrieveAllCountries();
                    ddlCountry.DataTextField = "Country1";
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Supplier Grid View Selected Index is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp;";

                btnAdd.Visible = false;
                btnUpdate.Visible = true;

                Supplier mySupplier = new SuppliersLogic().RetrieveSupplierByID(Convert.ToInt32(gvSuppliers.SelectedValue));

                hdnID.Value = mySupplier.Id.ToString();
                txtSupplier.Text = mySupplier.Supplier1;
                txtEmail.Text = mySupplier.Email;

                string[] mySplitAddress = mySupplier.StreetAddress.Split('|');

                txtAddressLine1.Text = mySplitAddress[0];
                txtAddressLine2.Text = mySplitAddress[1];

                txtTown.Text = mySupplier.Town.Town1;
                txtPostcode.Text = mySupplier.Postcode;
                ddlCountry.SelectedValue = mySupplier.Town.Country.Id.ToString();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Suppliers Grid View Row is Deleting
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSuppliers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp;";
                gvSuppliers.SelectedIndex = -1;

                txtSupplier.Text = "";
                txtEmail.Text = "";
                txtAddressLine1.Text = "";
                txtAddressLine2.Text = "";
                txtTown.Text = "";
                txtPostcode.Text = "";
                ddlCountry.SelectedValue = "0";

                if (new SuppliersLogic().DeleteSupplier(Convert.ToInt32(e.Keys[0])))
                {
                    gvSuppliers.DataSource = new SuppliersLogic().RetrieveAllSuppliers();
                    gvSuppliers.DataBind();

                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    lblServerSideError.Text = "Supplier is Linked to Existing Orders or Products and Cannot be Deleted";
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Suppliers Clear Button is Pressed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp;";

                txtSupplier.Text = "";
                txtEmail.Text = "";
                txtAddressLine1.Text = "";
                txtAddressLine2.Text = "";
                txtTown.Text = "";
                txtPostcode.Text = "";
                ddlCountry.SelectedValue = "0";

                gvSuppliers.SelectedIndex = -1;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Refresh Button is Called by AJAX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                gvSuppliers.DataSource = new SuppliersLogic().RetrieveAllSuppliers();
                gvSuppliers.DataBind();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}