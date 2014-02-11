using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Logic;

namespace External.administrator
{
    public partial class role : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    gvRoles.DataSource = new RolesLogic().RetrieveAllRoles();
                    gvRoles.DataBind();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Refresh Button is Called by AJAX
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp";
                lblServerSideErrorBottom.Text = "";
                gvRoles.DataSource = new RolesLogic().RetrieveAllRoles();
                gvRoles.DataBind();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Role Grid View Selected Index is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp;";
                lblServerSideErrorBottom.Text = "";
                btnAdd.Visible = false;
                btnUpdate.Visible = true;

                Role myRole = new RolesLogic().RetrieveRoleByID(Convert.ToInt32(gvRoles.SelectedValue));

                hdnID.Value = myRole.Id.ToString();
                txtRole.Text = myRole.Role1;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Role Grid View Item is Being Deleted
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp;";

                lblServerSideErrorBottom.Text = "";

                RoleDelete myResult = new RolesLogic().DeleteRole(Convert.ToInt32(e.Keys[0]));

                if (myResult == RoleDelete.LockedRole)
                {
                    //Cant be deleted
                    lblServerSideError.Text = "Role is Locked and Cannot be Deleted";
                }
                else if (myResult == RoleDelete.HasUsers)
                {
                    //bound to users
                    lblServerSideError.Text = "Role is Bound to One or More Users and Cannot be Deleted";
                }
                else
                {
                    gvRoles.SelectedIndex = -1;
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    txtRole.Text = "";
                    hdnID.Value = "";
                    gvRoles.DataSource = new RolesLogic().RetrieveAllRoles();
                    gvRoles.DataBind();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Role Clear Button is Pressed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp;";
                gvRoles.SelectedIndex = -1;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                txtRole.Text = "";
                lblServerSideErrorBottom.Text = "";
                hdnID.Value = "";
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}