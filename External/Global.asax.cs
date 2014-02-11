using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Principal;
using Common;
using Logic;
using Common.Views;

namespace External
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["SupplierOrder"] = null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                IQueryable<Role> myRoles = new UsersLogic().RetrieveUserRoles(Context.User.Identity.Name);

                List<string> myRoleList = new List<string>();

                foreach(Role myRole in myRoles)
                {
                    myRoleList.Add(myRole.Role1);
                }

                string[] myRolesArray = myRoleList.ToArray();

                GenericPrincipal myPrincipal = new GenericPrincipal(Context.User.Identity, myRolesArray);
                Context.User = myPrincipal;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["SupplierOrder"] = null;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}