using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data
{
    public class RolesRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public RolesRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public RolesRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Retrieves All Roles
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type Role</returns>
        public IQueryable<Role> RetrieveAllRoles()
        {
            try
            {
                return Entities.Roles.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if Role Already Exists
        /// Level: Data
        /// </summary>
        /// <param name="Role">The Role Name</param>
        /// <returns>True if Role Exists. False if Not Exists.</returns>
        public bool RoleExists(string Role)
        {
            try
            {
                if ((Entities.Roles.SingleOrDefault(r => r.Role1 == Role) != null))
                {
                    return true;
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
        /// Adds a New Role
        /// Level: Data
        /// </summary>
        /// <param name="Role">The Role to Add.</param>
        public void AddRole(Role Role)
        {
            try
            {
                Entities.AddToRoles(Role);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Role By ID
        /// Level: Data
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        /// <returns>An object of type Role</returns>
        public Role RetrieveRoleByID(int RoleID)
        {
            try
            {
                return Entities.Roles.SingleOrDefault(r => r.Id == RoleID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Role By Name
        /// Level: Data
        /// </summary>
        /// <param name="Role">The Role Name</param>
        /// <returns>An object of type Role</returns>
        public Role RetrieveRoleByName(string Role)
        {
            try
            {
                return Entities.Roles.SingleOrDefault(r => r.Role1 == Role);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if Role Has Users
        /// Level: Data
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        /// <returns>True if Role Has Users. False if Role has no users.</returns>
        public bool RoleHasUsers(int RoleID)
        {
            try
            {
                if (Entities.Roles.SingleOrDefault(r => r.Id == RoleID).Users.Count > 0)
                {
                    return true;
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
        /// Level: Data
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        /// <param name="Role">The Role Name</param>
        public void UpdateRole(int RoleID, string Role)
        {
            try
            {
                Common.Role myRole = RetrieveRoleByID(RoleID);

                myRole.Role1 = Role;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a Role
        /// Level: Data
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        public void DeleteRole(int RoleID)
        {
            try
            {
                Common.Role myRole = RetrieveRoleByID(RoleID);

                Entities.DeleteObject(myRole);

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a Role is the Administrator or User Role
        /// Level: Data
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        /// <returns>True if the role is Administrator or User. False if it is not.</returns>
        public bool RoleIsAdministratorOrUser(int RoleID)
        {
            try
            {
                if ((Entities.Roles.SingleOrDefault(r => r.Id == RoleID).Role1 == "Administrator") ||
                    (Entities.Roles.SingleOrDefault(r => r.Id == RoleID).Role1 == "User"))
                {
                    return true;
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
    }
}
