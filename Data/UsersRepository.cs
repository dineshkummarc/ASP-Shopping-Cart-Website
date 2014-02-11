using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.Common;

namespace Data
{
    public class UsersRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public UsersRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public UsersRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Checks if Username Already Exists
        /// Level: Data
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <returns>True if Username Exists. False if Username Doesnt Exist.</returns>
        public bool UsernameExists(string Username)
        {
            try
            {
                if (Entities.UserDetails.SingleOrDefault(d => d.Username == Username) != null)
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
        /// Checks if Email Already Exists
        /// Level: Data
        /// </summary>
        /// <param name="Email">The E-Mail</param>
        /// <returns>True if Email Exists. False if Email Does Not Exist.</returns>
        public bool EmailExists(string Email)
        {
            try
            {
                if (Entities.Users.SingleOrDefault(u => u.Email == Email) != null)
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
        /// Retrieves a UserType By UserType Name
        /// Level: Data
        /// </summary>
        /// <param name="UserType">The User Type</param>
        /// <returns>An object of type UserType</returns>
        public UserType RetrieveUserTypeByName(string UserType)
        {
            try
            {
                return Entities.UserTypes.SingleOrDefault(t => t.Type == UserType);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User By ID
        /// Level: Data
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <returns>An object of type User</returns>
        public User RetrieveUserById(Guid UserID)
        {
            try
            {
                return Entities.Users.SingleOrDefault(u => u.Id == UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Role By Role Name
        /// Level: Data
        /// </summary>
        /// <param name="Role">The Role Name</param>
        /// <returns>An Object of type Role</returns>
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
        /// Retrieves a Country Given the Country Name
        /// Level: Data
        /// </summary>
        /// <param name="Country">The Country Name</param>
        /// <returns>An Object of type Country</returns>
        public Country RetrieveCountry(string Country)
        {
            try
            {
                return Entities.Countries.SingleOrDefault(c => c.Country1 == Country);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Town Given the Town Name and Country Name
        /// Level: Data
        /// </summary>
        /// <param name="Town">The Town Name</param>
        /// <param name="Country">The Country Name</param>
        /// <returns>An object of type Town</returns>
        public Town RetrieveTown(string Town, string Country)
        {
            try
            {
                return Entities.Towns.SingleOrDefault(t => t.Town1 == Town && t.Country.Country1 == Country);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Finalises User Signup
        /// Level: Data
        /// </summary>
        /// <param name="myUser">The User to Add</param>
        public void FinalizeSignUp(User myUser)
        {
            try
            {
                Entities.AddToUsers(myUser);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User By the Users Username and Password
        /// Level: Data
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <param name="Password">The Password</param>
        /// <returns>An object of type User</returns>
        public User Login(string Username, string Password)
        {
            try
            {
                return Entities.Users.SingleOrDefault(u => u.UserDetail.Username == Username && u.UserDetail.Password == Password);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Users Role By the Users Username
        /// Level: Data
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <returns>A collection of type Role</returns>
        public IQueryable<Role> RetrieveUserRoles(string Username)
        {
            try
            {
                return Entities.Users.SingleOrDefault(u => u.UserDetail.Username == Username).Roles.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Users Role By the Users Email
        /// Level: Data
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <returns>A collection of type Role</returns>
        public IQueryable<Role> RetrieveUserRolesByEmail(string Email)
        {
            try
            {
                return Entities.Users.SingleOrDefault(u => u.Email == Email).Roles.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User By the Users Username
        /// Level: Data
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <returns>An object of type User</returns>
        public User RetrieveUserByUsername(string Username)
        {
            try
            {
                return Entities.Users.SingleOrDefault(u => u.UserDetail.Username == Username);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all Countries
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type Country</returns>
        public IQueryable<Country> RetrieveAllCountries()
        {
            try
            {
                return Entities.Countries.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User By Email
        /// Level: Data
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <returns>A collection of type User</returns>
        public User RetrieveUserByEmail(string Email)
        {
            try
            {
                return Entities.Users.SingleOrDefault(u => u.Email == Email);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Allocates a new User Type to a User
        /// Level: Data
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <param name="UserTypeID">The User Type</param>
        public void AllocateNewUserType(string Email, int UserTypeID)
        {
            try
            {
                User myUser = RetrieveUserByEmail(Email);

                myUser.UserTypeFK = UserTypeID;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Allocates a Role to a User
        /// Level: Data
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <param name="Role">The Role</param>
        public void AllocateRole(string Email, string Role)
        {
            try
            {
                User myUser = RetrieveUserByEmail(Email);

                myUser.Roles.Add(RetrieveRoleByName(Role));

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deallocates a Role from a User
        /// Level: Data
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <param name="myRole">The Role</param>
        public void DeAllocateRole(string Email, Role myRole)
        {
            try
            {
                User myUser = RetrieveUserByEmail(Email);

                Role myRoleToRemove = myUser.Roles.SingleOrDefault(r => r.Role1 == myRole.Role1);
                myUser.Roles.Remove(myRoleToRemove);

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a User is an Administrator
        /// Level: Data
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <returns>True if a User is Administrator. False if User is not an Administrator.</returns>
        public bool UserIsAdmin(string Email)
        {
            try
            {
                if (Entities.Users.SingleOrDefault(u => u.Email == Email).Roles.Contains(RetrieveRoleByName("Administrator")))
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
        /// Inserts the Users Credit Card Number
        /// Level: Data
        /// </summary>
        /// <param name="CreditCard">The Credit Cart Number</param>
        /// <param name="UserID">The User ID</param>
        public void InsertCreditCardNumber(string CreditCard, Guid UserID)
        {
            try
            {
                User myUser = new UsersRepository().RetrieveUserById(UserID);
                UserDetail myUserDetail = Entities.UserDetails.SingleOrDefault(d => d.Id == myUser.UserDetailsFK);

                myUserDetail.CreditCardNumber = CreditCard;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
