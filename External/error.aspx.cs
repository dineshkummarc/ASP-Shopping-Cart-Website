using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace External
{
    public partial class error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string myOccurance = Request.QueryString[0].ToString();

                lblServerSideErrorTop.Text = "Oops! An unexpected error occured at: " + myOccurance + ". Please try again later!";
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}