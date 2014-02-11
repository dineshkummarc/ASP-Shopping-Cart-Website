using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data
{
    public class UserTypesRepository : ConnectionClass
    {
        public UserTypesRepository()
            : base()
        {
        }

        public UserTypesRepository(bool isAdmin)
            : base()
        {
        }

        //Retrieves all UserTypes
        public IQueryable<UserType> RetrieveAllUserTypes()
        {
            try
            {
                return Entities.UserTypes.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        //Checks if UserType already exists
        public bool UserTypeExists(string UserType)
        {
            try
            {
                if (Entities.UserTypes.SingleOrDefault(t => t.Type == UserType) != null)
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

        //Adds UserType
        public void AddUserType(UserType UserType)
        {
            try
            {
                Entities.AddToUserTypes(UserType);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        //Checks if UserType is bound to any Users
        public bool UserTypeHasUsers(int UserTypeID)
        {
            try
            {
                if (Entities.Users.SingleOrDefault(u => u.UserTypeFK == UserTypeID) != null)
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

        //Retrieves UserType by ID
        public UserType RetrieveUserTypeByID(int UserTypeID)
        {
            try
            {
                return Entities.UserTypes.SingleOrDefault(t => t.Id == UserTypeID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        public UserType RetrieveUserTypeByName(string UserType)
        {
            return Entities.UserTypes.SingleOrDefault(t => t.Type == UserType);
        }

        //Updates a UserType
        public void UpdateUserType(int UserTypeID, string UserType)
        {
            try
            {
                UserType myUserType = RetrieveUserTypeByID(UserTypeID);

                myUserType.Type = UserType;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        //Deletes a UserType
        public void DeleteUserType(int UserTypeID)
        {
            try
            {
                UserType myUserType = RetrieveUserTypeByID(UserTypeID);

                Entities.DeleteObject(myUserType);

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        //Check if usertype is retailer or wholesaler
        public bool UserTypeIsRetailerOrWholesaler(int UserTypeID)
        {
            try
            {
                if ((Entities.UserTypes.SingleOrDefault(t => t.Id == UserTypeID).Type == "Retailer") ||
                    (Entities.UserTypes.SingleOrDefault(t => t.Id == UserTypeID).Type == "Wholesaler"))
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

        public string RetrieveUsersUserType(string Email)
        {
            try
            {
                User myUser = Entities.Users.SingleOrDefault(u => u.Email == Email);

                if (myUser != null)
                {
                    return myUser.UserType.Type;
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
