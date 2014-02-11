using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace External.administrator
{
    public partial class pricetype : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtDateStart.Attributes.Add("readonly", "readonly");
                txtDateEnd.Attributes.Add("readonly", "readonly");
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}