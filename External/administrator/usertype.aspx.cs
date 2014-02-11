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
    public partial class usertype : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    gvUserTypes.DataSource = new UserTypesLogic().RetrieveAllUserTypes();
                    gvUserTypes.DataBind();
                }
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
                lblServerSideError.Text = "&nbsp";
                lblServerSideErrorBottom.Text = "";
                gvUserTypes.DataSource = new UserTypesLogic().RetrieveAllUserTypes();
                gvUserTypes.DataBind();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the UserTypes selected index is Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUserTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp";
                lblServerSideErrorBottom.Text = "";
                btnAdd.Visible = false;
                btnUpdate.Visible = true;

                UserType myUserType = new UserTypesLogic().RetrieveUserTypeByID(Convert.ToInt32(gvUserTypes.SelectedValue));

                hdnID.Value = myUserType.Id.ToString();
                txtUserType.Text = myUserType.Type;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the User Type Clear Button is Pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp";
                gvUserTypes.SelectedIndex = -1;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                txtUserType.Text = "";
                hdnID.Value = "";
                lblServerSideErrorBottom.Text = "";
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the User Types Grid View Selected item is deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUserTypes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblServerSideError.Text = "&nbsp";
                lblServerSideErrorBottom.Text = "";

                UserTypeDelete myResult = new UserTypesLogic().DeleteUserType(Convert.ToInt32(e.Keys[0]));

                if (myResult == UserTypeDelete.LockedUserType)
                {
                    lblServerSideError.Text = "User Type is Locked and Cannot be Deleted";
                }
                else if (myResult == UserTypeDelete.HasUsers)
                {
                    lblServerSideError.Text = "User Type is Bound to One or More Users and Cannot be Deleted";
                }
                else
                {   
                    gvUserTypes.SelectedIndex = -1;
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    txtUserType.Text = "";
                    hdnID.Value = "";
                    gvUserTypes.DataSource = new UserTypesLogic().RetrieveAllUserTypes();
                    gvUserTypes.DataBind();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}