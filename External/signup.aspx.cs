using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Globalization;
using Logic;

namespace External
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    MaintainScrollPositionOnPostBack = true;

                    if (!Page.IsPostBack)
                    {
                        calExtender.EndDate = DateTime.Now.AddYears(-18);
                        calExtender.SelectedDate = DateTime.Now.AddYears(-18);

                        txtDateOfBirth.Attributes.Add("readonly", "readonly");

                        ddlCountry.DataSource = new UsersLogic().RetrieveAllCountries();
                        ddlCountry.DataTextField = "Country1";
                        ddlCountry.DataValueField = "Id";
                        ddlCountry.DataBind();
                        ddlCountry.Items.Insert(0, new ListItem("Select", "0"));
                    }
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
                    //Retrieving Control Values
                    string Name = txtName.Text.Trim();
                    string Surname = txtSurname.Text.Trim();

                    DateTime DateOfBirth = new DateTimeParser().ParseDate(txtDateOfBirth.Text.Trim().Replace('/', '-'));

                    string Address = txtAddressLine1.Text.Trim().Replace("|", String.Empty) + "|"
                        + txtAddressLine2.Text.Trim().Replace("|", String.Empty);

                    string Town = txtTown.Text.Trim();
                    string PostCode = txtPostCode.Text.Trim();
                    string Country = ddlCountry.SelectedItem.Text;
                    string Username = txtUsername.Text.Trim();
                    string Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "MD5");
                    string ConfirmPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtConfirmPassword.Text.Trim(), "MD5");
                    string Email = txtEmail.Text.Trim();

                    switch (new UsersLogic().FinalizeSignUp(Name, Surname, DateOfBirth, Address, Email, Town, PostCode, Country, Username, Password))
                    {
                        case UserSignup.UsernameAndEmailExist:
                            lblServerSideError.Text = "Email Already Exists</br>Username Already Exists";
                            break;

                        case UserSignup.EmailExists:
                            lblServerSideError.Text = "Email Already Exists";
                            break;

                        case UserSignup.UsernameExists:
                            lblServerSideError.Text = "Username Already Exists";
                            break;

                        case UserSignup.Successful:
                            Response.Redirect("~/login.aspx");
                            break;
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