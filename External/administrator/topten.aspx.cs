using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace External.administrator
{
    public partial class topten : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtStartDate.Attributes.Add("readonly", "readonly");
                txtEndDate.Attributes.Add("readonly", "readonly");
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}