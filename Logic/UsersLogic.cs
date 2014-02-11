using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Common;
using System.Data.Common;
using System.Web;

namespace Logic
{
    public enum UserSignup
    {
        Successful,
        UsernameExists,
        EmailExists,
        UsernameAndEmailExist
    }

    public enum UserDeAllocate
    {
        UserIsAdmin,
        OnlyUser,
        Successful
    }

    public class UsersLogic
    {
        /// <summary>
        /// Retrieves all Countries
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type Country</returns>
        public IQueryable<Country> RetrieveAllCountries()
        {
            try
            {
                return new UsersRepository(false).RetrieveAllCountries();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Finalises user signup
        /// Level: Logic
        /// </summary>
        /// <param name="Name">The Name</param>
        /// <param name="Surname">The Surname</param>
        /// <param name="DateOfBirth">The Date of Birth</param>
        /// <param name="Address">The Address</param>
        /// <param name="Email">The Email</param>
        /// <param name="Town">The Town</param>
        /// <param name="Postcode">The Postcode</param>
        /// <param name="Country">The Country</param>
        /// <param name="Username">The Username</param>
        /// <param name="Password">The Password</param>
        /// <returns>UserSignupEnum</returns>
        public UserSignup FinalizeSignUp(string Name, string Surname, DateTime DateOfBirth, string Address, string Email, string Town,
            string Postcode, string Country, string Username, string Password)
        {
            try
            {
                UsersRepository myRepository = new UsersRepository(false);

                try
                {
                    //Checking if Username Exists
                    if ((myRepository.UsernameExists(Username)) && (myRepository.EmailExists(Email)))
                    {
                        return UserSignup.UsernameAndEmailExist;
                    }
                    else if (myRepository.UsernameExists(Username))
                    {
                        return UserSignup.UsernameExists;
                    }
                    else if (myRepository.EmailExists(Email))
                    {
                        return UserSignup.EmailExists;
                    }
                    else
                    {
                        //Instanciating User
                        User myUser = new User();

                        myUser.Id = Guid.NewGuid();
                        myUser.Name = Name;
                        myUser.Surname = Surname;
                        myUser.DateOfBirth = DateOfBirth;
                        myUser.StreetAddress = Address;
                        myUser.Postcode = Postcode;
                        myUser.Email = Email;

                        //Retrieving UserType
                        myUser.UserType = myRepository.RetrieveUserTypeByName("Retailer");

                        //Retrieving Possible Same Town
                        Town myTown = myRepository.RetrieveTown(Town, Country);

                        //If Town Exists
                        if (myTown != null)
                        {
                            //Assigning Existent Town to User
                            myUser.Town = myTown;
                        }
                        else
                        {
                            //Instanciating New Town
                            myTown = new Town();
                            myTown.Town1 = Town;
                            myTown.Country = myRepository.RetrieveCountry(Country);

                            //Assigning New Town to User
                            myUser.Town = myTown;
                        }

                        //Instanciating User Details
                        UserDetail myUserDetails = new UserDetail();

                        myUserDetails.Id = Guid.NewGuid();
                        myUserDetails.Username = Username;
                        myUserDetails.Password = Password;

                        //Assigning User Details to User
                        myUser.UserDetail = myUserDetails;

                        //Assigning User Role to User
                        myUser.Roles.Add(myRepository.RetrieveRoleByName("User"));

                        //Saving Changes
                        myRepository.FinalizeSignUp(myUser);

                        //Returning Successful
                        return UserSignup.Successful;
                    }
                }
                catch (Exception Exception)
                {
                    //Throwing Exception
                    throw Exception;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Verifies Whether a User Can Log In
        /// Level: Logic
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <param name="Password">The Password</param>
        /// <returns>True if User can login, False if User Cannot</returns>
        public bool Login(string Username, string Password)
        {
            try
            {
                if (new UsersRepository(false).Login(Username, Password) != null)
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
        /// Retrieves User Roles By Username
        /// Level: Logic
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <returns>A collection of type Role</returns>
        public IQueryable<Role> RetrieveUserRoles(string Username)
        {
            try
            {
                return new UsersRepository(false).RetrieveUserRoles(Username);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User By Username
        /// Level: Logic
        /// </summary>
        /// <param name="Username">The Username</param>
        /// <returns>An object of type User</returns>
        public User RetrieveUserByUsername(string Username)
        {
            try
            {
                return new UsersRepository(false).RetrieveUserByUsername(Username);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Allocates a new usertype to a user
        /// Level: Logic
        /// </summary>
        /// <param name="Email">The Users Email</param>
        /// <param name="UserTypeID">The UserTypeID</param>
        public void AllocateNewUserType(string Email, int UserTypeID)
        {
            try
            {
                new UsersRepository().AllocateNewUserType(Email, UserTypeID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Allocates a role to a User
        /// Level: Logic
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <param name="Role">The Role</param>
        public void AllocateRole(string Email, string Role)
        {
            try
            {
                new UsersRepository().AllocateRole(Email, Role);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// De Allocates a Role from a User
        /// Level: Logic
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <param name="RoleID">The RoleID</param>
        /// <returns></returns>
        public UserDeAllocate DeAllocateRole(string Email, int RoleID)
        {
            try
            {
                RolesRepository myRolesRepository = new RolesRepository();

                Role myRole = myRolesRepository.RetrieveRoleByID(RoleID);

                //check if deallocation being done on admin
                if (myRolesRepository.RetrieveRoleByName("Administrator") != myRole)
                {
                    if (myRolesRepository.RetrieveRoleByName("User") != myRole)
                    {
                        new UsersRepository().DeAllocateRole(Email, myRole);
                        return UserDeAllocate.Successful;
                    }
                    else
                    {
                        UsersRepository myUsersRepository = new UsersRepository();

                        if (myUsersRepository.UserIsAdmin(Email))
                        {
                            new UsersRepository().DeAllocateRole(Email, myRole);
                            return UserDeAllocate.Successful;
                        }
                        else
                        {
                            return UserDeAllocate.OnlyUser;
                        }
                    }
                }
                else
                {
                    return UserDeAllocate.UserIsAdmin;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all userRoles By Email
        /// Level: Logic
        /// </summary>
        /// <param name="Email">The Email</param>
        /// <returns>A collection of type Role</returns>
        public IQueryable<Role> RetrieveUserRolesByEmail(string Email)
        {
            try
            {
                return new UsersRepository().RetrieveUserRolesByEmail(Email);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a User By ID
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <returns>An object of type User</returns>
        public User RetrieveUserByID(Guid UserID)
        {
            try
            {
                return new UsersRepository().RetrieveUserById(UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Inserts A credit cart Number
        /// Level: Logic
        /// </summary>
        /// <param name="CreditCard">The Credit Cart Number</param>
        /// <param name="UserID">The User ID</param>
        public void InsertCreditCardNumber(string CreditCard, Guid UserID)
        {
            try
            {
                new UsersRepository(false).InsertCreditCardNumber(CreditCard, UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
