using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.Views;
using Logic;

namespace External.administrator
{
    public partial class supplierorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["SupplierOrder"] = new List<OrderItem>();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}