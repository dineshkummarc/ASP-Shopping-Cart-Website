using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Common;

namespace Logic
{
    public enum UserTypeUpdate
    {
        Successful,
        SameUserType,
        LockedUserType
    }

    public enum UserTypeDelete
    {
        Successful,
        HasUsers,
        LockedUserType
    }

    public class UserTypesLogic
    {
        /// <summary>
        /// Retrieves all UserTypes
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type UserType</returns>
        public IQueryable<UserType> RetrieveAllUserTypes()
        {
            try
            {
                return new UserTypesRepository().RetrieveAllUserTypes();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a UserType By ID
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeID">The UserTypeID</param>
        /// <returns>An object of type UserType</returns>
        public UserType RetrieveUserTypeByID(int UserTypeID)
        {
            try
            {
                return new UserTypesRepository().RetrieveUserTypeByID(UserTypeID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a New UserType
        /// Level: Logic
        /// </summary>
        /// <param name="UserType">The UserType</param>
        /// <returns>True if Successful, False if Not</returns>
        public bool AddUserType(string UserType)
        {
            try
            {
                UserTypesRepository myRepository = new UserTypesRepository();

                if (!myRepository.UserTypeExists(UserType))
                {
                    Common.UserType myUserType = new UserType();

                    myUserType.Type = UserType;

                    myRepository.AddUserType(myUserType);

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
        /// Updates a UserType
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeID">The UserTypeID</param>
        /// <param name="UserType">The UserType</param>
        /// <returns>UserTypeUpdate Enum</returns>
        public UserTypeUpdate UpdateUserType(int UserTypeID, string UserType)
        {
            try
            {
                UserTypesRepository myRepository = new UserTypesRepository();

                if (myRepository.UserTypeIsRetailerOrWholesaler(UserTypeID))
                {
                    return UserTypeUpdate.LockedUserType;
                }
                else if (myRepository.UserTypeExists(UserType))
                {
                    return UserTypeUpdate.SameUserType;
                }
                else
                {
                    myRepository.UpdateUserType(UserTypeID, UserType);
                    return UserTypeUpdate.Successful;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeID">The UserTypeID</param>
        /// <returns>UserTypeDelete Enum</returns>
        public UserTypeDelete DeleteUserType(int UserTypeID)
        {
            try
            {
                UserTypesRepository myRepository = new UserTypesRepository();

                if (myRepository.UserTypeIsRetailerOrWholesaler(UserTypeID))
                {
                    return UserTypeDelete.LockedUserType;
                }
                else if (myRepository.UserTypeHasUsers(UserTypeID))
                {
                    return UserTypeDelete.HasUsers;
                }
                else
                {
                    myRepository.DeleteUserType(UserTypeID);
                    return UserTypeDelete.Successful;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Users UserType
        /// Level: Logic
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <returns>A UserType String</returns>
        public string RetrieveUsersUserType(string Email)
        {
            try
            {
                return new UserTypesRepository().RetrieveUsersUserType(Email);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a UserTypeByName
        /// Level: Logic
        /// </summary>
        /// <param name="UserType">The UserType Name</param>
        /// <returns>A UserType Object</returns>
        public UserType RetrieveUserTypeByName(string UserType)
        {
            try
            {
                return new UserTypesRepository().RetrieveUserTypeByName(UserType);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
