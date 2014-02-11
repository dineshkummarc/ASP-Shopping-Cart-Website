using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data
{
    public class ShoppingCartRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public ShoppingCartRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public ShoppingCartRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Retrieves all Shopping Cart Items for User
        /// Level: Data
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <returns>A collection of type ShoppingCart</returns>
        public IQueryable<ShoppingCart> RetrieveAllShoppingCartItems(Guid UserID)
        {
            try
            {
                User myUser = new UsersRepository().RetrieveUserById(UserID);

                IQueryable<UserTypeProduct> myUserTypeProducts = Entities.UserTypeProducts.Where(up => up.UserType.Id == myUser.UserTypeFK);
                IQueryable<ShoppingCart> myShoppingCartItems = null;

                myShoppingCartItems = Entities.ShoppingCarts.Where(sc => sc.UserFK == UserID && sc.Product.Status == true);

                List<ShoppingCart> myShoppingCartItemsList = myShoppingCartItems.ToList();

                foreach (ShoppingCart myCurrentItem in myShoppingCartItems)
                {
                    if (myUserTypeProducts.SingleOrDefault(t => t.Product.Id == myCurrentItem.ProductFK) == null)
                    {
                        myShoppingCartItemsList.Remove(myCurrentItem);
                    }
                }

                return myShoppingCartItemsList.AsQueryable();

                //return Entities.ShoppingCarts.Where(sc => sc.UserFK == UserID && sc.Product.Status == true);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a ShoppingCart Item
        /// Level: Data
        /// </summary>
        /// <param name="myShoppingCartItem">The ShoppingCart to Add</param>
        public void AddToCart(ShoppingCart myShoppingCartItem)
        {
            try
            {
                Entities.AddToShoppingCarts(myShoppingCartItem);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes a ShoppingCart Item
        /// Level: Data
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="UserID">The User ID</param>
        public void RemoveFromCart(Guid ProductID, Guid UserID)
        {
            try
            {
                ShoppingCart myShoppingCartItem = RetrieveShoppingCartItemById(ProductID, UserID);

                if (myShoppingCartItem != null)
                {
                    Entities.DeleteObject(myShoppingCartItem);
                    Entities.SaveChanges();
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if Shopping Cart Item Exists
        /// Level: Data
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="UserID">The User ID</param>
        /// <returns>Returns False if item does not exist. Returns true if item exists.</returns>
        public bool ItemExists(Guid ProductID, Guid UserID)
        {
            try
            {
                if (Entities.ShoppingCarts.SingleOrDefault(sc => sc.UserFK == UserID && sc.ProductFK == ProductID) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Shopping Cart Item By ID
        /// Level: Data
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="UserID">The User ID</param>
        /// <returns>An object of Type ShoppingCart</returns>
        public ShoppingCart RetrieveShoppingCartItemById(Guid ProductID, Guid UserID)
        {
            try
            {
                return Entities.ShoppingCarts.SingleOrDefault(sc => sc.ProductFK == ProductID && sc.UserFK == UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Changes a Shopping Cart Item's Quantity
        /// Level: Data
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="UserID">The User ID</param>
        /// <param name="NewQuantity">The Quantity</param>
        public void ChangeQuantity(Guid ProductID, Guid UserID, int NewQuantity)
        {
            try
            {
                ShoppingCart myShoppingCartItem = Entities.ShoppingCarts.SingleOrDefault(sc => sc.ProductFK == ProductID && sc.UserFK == UserID);

                myShoppingCartItem.Quantity = NewQuantity;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Empties a Users Shopping Cart
        /// Level: Data
        /// </summary>
        /// <param name="UserID">The User ID</param>
        public void EmptyCart(Guid UserID)
        {
            try
            {
                IQueryable<ShoppingCart> myUserCart = Entities.ShoppingCarts.Where(sc => sc.UserFK == UserID);

                foreach (ShoppingCart myCartItem in myUserCart)
                {
                    Entities.DeleteObject(myCartItem);
                }

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Users Shopping Cart Total
        /// Level: Data
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <returns>The Shopping Cart Total Price</returns>
        public double RetrieveCartTotal(Guid UserID)
        {
            try
            {
                IQueryable<ShoppingCart> myUserCart = RetrieveAllShoppingCartItems(UserID);

                User myUser = new UsersRepository().RetrieveUserById(UserID);

                double TotalPrice = 0;

                foreach (ShoppingCart myShoppingCartItem in myUserCart)
                {
                    UserTypeProduct myPriceType = new PriceTypesRepository().RetrievePriceTypeByID(myUser.UserTypeFK, myShoppingCartItem.ProductFK);

                    if ((DateTime.Now >= myPriceType.DiscountDateFrom) && (DateTime.Now <= myPriceType.DiscountDateTo))
                    {
                        TotalPrice += Convert.ToDouble(myPriceType.Price - ((myPriceType.DiscountPercentage / 100) * myPriceType.Price)) * myShoppingCartItem.Quantity;
                    }
                    else
                    {
                        TotalPrice += myPriceType.Price * myShoppingCartItem.Quantity;
                    }
                }

                return TotalPrice;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
