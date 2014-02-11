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
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtCurrentUserType.Attributes.Add("readonly", "readonly");
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}