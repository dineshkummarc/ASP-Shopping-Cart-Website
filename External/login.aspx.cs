using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Logic;
using Common;

namespace External
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/default.aspx");
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    string Username = txtUsername.Text.Trim();
                    string Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "MD5");

                    if (new UsersLogic().Login(Username, Password))
                    {
                        FormsAuthentication.RedirectFromLoginPage(Username, true);
                    }
                    else
                    {
                        lblServerSideError.Text = "Incorrect Username or Password";
                    }
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}