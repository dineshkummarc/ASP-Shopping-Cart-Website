using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common;
using Logic;

namespace External.AJAX.Services
{
    /// <summary>
    /// Summary description for allocate
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class allocate : System.Web.Services.WebService
    {
        /// <summary>
        /// Retrieves a Users Usertype
        /// Level: External
        /// </summary>
        /// <param name="Email">The Users Email Address</param>
        /// <returns>The Users UserType</returns>
        [WebMethod]
        public string RetrieveUserType(string Email)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myEmail = Email.Trim();

                    return new UserTypesLogic().RetrieveUsersUserType(myEmail);
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

        /// <summary>
        /// Retrieves a Users Possible User Types
        /// Level: External
        /// </summary>
        /// <param name="UserType">The Current User Type</param>
        /// <returns>The Users Possible User Types</returns>
        [WebMethod]
        public string RetrievePossibleTypes(string UserType)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myUserType = UserType.Trim();

                    List<UserType> myUserTypes = new UserTypesLogic().RetrieveAllUserTypes().ToList();
                    string HTML = "";

                    foreach (UserType myType in myUserTypes)
                    {
                        if (myType.Type != myUserType)
                        {
                            HTML += "<option value=\"" + myType.Id + "\">" + myType.Type + "</option>";
                        }
                    }

                    return HTML;
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

        /// <summary>
        /// Allocates a User Type
        /// Level: External
        /// </summary>
        /// <param name="Email">User Email</param>
        /// <param name="UserTypeID">The UserType's ID</param>
        [WebMethod]
        public void AllocateType(string Email, string UserTypeID)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myEmail = Email.Trim();
                    int myUserTypeID = Convert.ToInt32(UserTypeID);

                    new UsersLogic().AllocateNewUserType(myEmail, myUserTypeID);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Populates Roles
        /// Level: External
        /// </summary>
        /// <param name="Email">The User Email</param>
        /// <returns>A List of Current Roles and Available Roles</returns>
        [WebMethod]
        public string[] PopulateRoles(string Email)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myEmail = Email.Trim();

                    List<Role> myCurrentRoles = new UsersLogic().RetrieveUserRolesByEmail(myEmail).ToList();
                    List<Role> AllRoles = new RolesLogic().RetrieveAllRoles().ToList();

                    string CurrentRoleHTML = "";
                    string AvailableRoleHTML = "";

                    foreach (Role myRole in myCurrentRoles)
                    {
                        CurrentRoleHTML += "<option value=\"" + myRole.Id + "\">" + myRole.Role1 + "</option>";
                    }

                    foreach (Role myRole in myCurrentRoles)
                    {
                        Role ToRemove = AllRoles.SingleOrDefault(r => r.Role1 == myRole.Role1);
                        AllRoles.Remove(ToRemove);
                    }

                    foreach (Role myRole in AllRoles)
                    {
                        AvailableRoleHTML += "<option value=\"" + myRole.Id + "\">" + myRole.Role1 + "</option>";
                    }

                    return new string[2] { CurrentRoleHTML, AvailableRoleHTML };
                }
                else
                {
                    return new string[] { "" };
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// DeAllocates a Role
        /// Level: External
        /// </summary>
        /// <param name="Email">The User Email</param>
        /// <param name="Role">The User Role</param>
        /// <returns>Error Message</returns>
        [WebMethod]
        public string DeAllocateRole(string Email, string Role)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myEmail = Email.Trim();
                    int myRole = Convert.ToInt32(Role.Trim());

                    UserDeAllocate myResult = new UsersLogic().DeAllocateRole(myEmail, myRole);

                    if (myResult == UserDeAllocate.UserIsAdmin)
                    {
                        return "Role Cannot Be De Allocated : Same Permission";
                    }
                    else if (myResult == UserDeAllocate.OnlyUser)
                    {
                        return "User Must Either Be an Administrator, a User, or Both";
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

        /// <summary>
        /// Allocates a Role
        /// Level: External
        /// </summary>
        /// <param name="Email">The User Email</param>
        /// <param name="Role">The User Role</param>
        [WebMethod]
        public void AllocateRole(string Email, string Role)
        {
            try
            {
                if (Context.User.IsInRole("Administrator"))
                {
                    string myEmail = Email.Trim();
                    string myRole = Role.Trim();

                    new UsersLogic().AllocateRole(myEmail, myRole);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
