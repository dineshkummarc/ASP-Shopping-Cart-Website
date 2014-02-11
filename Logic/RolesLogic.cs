using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data;

namespace Logic
{
    public enum RoleUpdate
    {
        Successful,
        SameRole,
        LockedRole
    }

    public enum RoleDelete
    {
        Successful,
        HasUsers,
        LockedRole
    }

    public class RolesLogic
    {
        /// <summary>
        /// Retrieves All Roles
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type Role</returns>
        public IQueryable<Role> RetrieveAllRoles()
        {
            try
            {
                return new RolesRepository().RetrieveAllRoles();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Role By ID
        /// Level: Logic
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        /// <returns>An object of type Role</returns>
        public Role RetrieveRoleByID(int RoleID)
        {
            try
            {
                return new RolesRepository().RetrieveRoleByID(RoleID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a Role
        /// Level: Logic
        /// </summary>
        /// <param name="Role">The Role</param>
        /// <returns>True if Successful, False if Not</returns>
        public bool AddRole(string Role)
        {
            try
            {
                RolesRepository myRepository = new RolesRepository();

                if (!myRepository.RoleExists(Role))
                {
                    Common.Role myRole = new Role();

                    myRole.Role1 = Role;

                    myRepository.AddRole(myRole);

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
        /// Level: Logic
        /// </summary>
        /// <param name="RoleID">The Role ID</param>
        /// <param name="Role">The Role</param>
        /// <returns>RoleUpdate Enum</returns>
        public RoleUpdate UpdateRole(int RoleID, string Role)
        {
            try
            {
                RolesRepository myRepository = new RolesRepository();

                if (myRepository.RoleIsAdministratorOrUser(RoleID))
                {
                    return RoleUpdate.LockedRole;
                }
                else if (myRepository.RoleExists(Role))
                {
                    return RoleUpdate.SameRole;
                }
                else
                {
                    myRepository.UpdateRole(RoleID, Role);
                    return RoleUpdate.Successful;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a Role
        /// Level: Logic
        /// </summary>
        /// <param name="RoleID">The RoleID</param>
        /// <returns>RoleDeleteEnum</returns>
        public RoleDelete DeleteRole(int RoleID)
        {
            try
            {
                RolesRepository myRepository = new RolesRepository();

                if (myRepository.RoleIsAdministratorOrUser(RoleID))
                {
                    return RoleDelete.LockedRole;
                }
                else if (myRepository.RoleHasUsers(RoleID))
                {
                    return RoleDelete.HasUsers;
                }
                else
                {
                    myRepository.DeleteRole(RoleID);
                    return RoleDelete.Successful;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

    }
}
