using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Logic;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for role
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class role : System.Web.Services.WebService
    {
        /// <summary>
        /// Adds a New Role
        /// Level: External
        /// </summary>
        /// <param name="Role">Role Name</param>
        /// <returns>True if Added. False if Not.</returns>
        [WebMethod]
        public bool AddRole(string Role)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myRole = Role.Trim();

                    return new RolesLogic().AddRole(myRole);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Role
        /// Level: External
        /// </summary>
        /// <param name="RoleID">Role ID</param>
        /// <param name="Role">Role Name</param>
        /// <returns>Error Message</returns>
        [WebMethod]
        public string UpdateRole(string RoleID, string Role)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int myRoleID = Convert.ToInt32(RoleID.Trim());
                    string myRole = Role.Trim();

                    RoleUpdate myResult = new RolesLogic().UpdateRole(myRoleID, myRole);

                    if (myResult == RoleUpdate.LockedRole)
                    {
                        return "Role is Locked and Cannot be Modified";
                    }
                    else if (myResult == RoleUpdate.SameRole)
                    {
                        return "Role Already Exists";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
