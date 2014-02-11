using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Common;

namespace Logic
{
    public class ShoppingCartLogic
    {
        /// <summary>
        /// Retrieves all Shopping Cart Items
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <returns>A collection of type ShoppingCart</returns>
        public IQueryable<ShoppingCart> RetrieveAllShoppingCartItems(Guid UserID)
        {
            try
            {
                return new ShoppingCartRepository(false).RetrieveAllShoppingCartItems(UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds an item to the cart
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="Quantity">The Quantity</param>
        public void AddToCart(Guid UserID, Guid ProductID, int Quantity)
        {
            try
            {
                ShoppingCartRepository myRepository = new ShoppingCartRepository(false);

                if (myRepository.ItemExists(ProductID, UserID))
                {
                    myRepository.ChangeQuantity(ProductID, UserID, Quantity);
                }
                else
                {
                    ShoppingCart myShoppingCartItem = new ShoppingCart();

                    myShoppingCartItem.UserFK = UserID;
                    myShoppingCartItem.ProductFK = ProductID;
                    myShoppingCartItem.Quantity = Quantity;

                    myRepository.AddToCart(myShoppingCartItem);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Shopping Cart Item by ID
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>An object of type shopping cart</returns>
        public ShoppingCart RetrieveShoppingCartItemByID(Guid UserID, Guid ProductID)
        {
            try
            {
                return new ShoppingCartRepository(false).RetrieveShoppingCartItemById(ProductID, UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes shopping cart item from cart
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The User ID</param>
        /// <param name="ProductID">The Product ID</param>
        public void RemoveFromCart(Guid UserID, Guid ProductID)
        {
            try
            {
                new ShoppingCartRepository(false).RemoveFromCart(ProductID, UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Empties a Users Shopping Cart
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The UserID</param>
        public void EmptyCart(Guid UserID)
        {
            try
            {
                new ShoppingCartRepository(false).EmptyCart(UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Total Price for Cart
        /// Level: Logic
        /// </summary>
        /// <param name="UserID">The UserID</param>
        /// <returns>The Price</returns>
        public double RetrieveCartTotal(Guid UserID)
        {
            try
            {
                return new ShoppingCartRepository(false).RetrieveCartTotal(UserID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
