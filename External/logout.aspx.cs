using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace External
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Context.User != null)
                {
                    FormsAuthentication.SignOut();
                }

                Response.Redirect("~/default.aspx");
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}