using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Logic;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for usertype
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class usertype : System.Web.Services.WebService
    {
        /// <summary>
        /// Adds a new UserType
        /// Level: External
        /// </summary>
        /// <param name="UserType">The User Type</param>
        /// <returns>True if Successful False if Not</returns>
        [WebMethod]
        public bool AddUserType(string UserType)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myUserType = UserType.Trim();

                    return new UserTypesLogic().AddUserType(myUserType);
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
        /// Updates a UserType
        /// Level: External
        /// </summary>
        /// <param name="UserTypeID">The User Type ID</param>
        /// <param name="UserType">The User Type</param>
        /// <returns>Error Message</returns>
        [WebMethod]
        public string UpdateUserType(string UserTypeID, string UserType)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    int myUserTypeID = Convert.ToInt32(UserTypeID.Trim());
                    string myUserType = UserType.Trim();

                    UserTypeUpdate myResult = new UserTypesLogic().UpdateUserType(myUserTypeID, myUserType);

                    if (myResult == UserTypeUpdate.LockedUserType)
                    {
                        return "User Type is Locked and Cannot be Modified";
                    }
                    else if (myResult == UserTypeUpdate.SameUserType)
                    {
                        return "User Type Already Exists";
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
