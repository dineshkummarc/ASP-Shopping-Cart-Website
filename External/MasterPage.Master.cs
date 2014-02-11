using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Security;
using Common;
using Logic;

namespace External
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();

                if (!IsPostBack)
                {
                    if (Context.User.Identity.IsAuthenticated)
                    {
                        User myLoggedUser = new UsersLogic().RetrieveUserByUsername(Context.User.Identity.Name);
                        lblLoggedUser.Text = myLoggedUser.Name + " " + myLoggedUser.Surname;
                    }

                    txtSearch.Attributes.Add("DefaultText", "Search Store");
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        protected void lnkShoppingCart_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/user/cart.aspx");
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}