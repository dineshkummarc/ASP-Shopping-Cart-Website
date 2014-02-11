using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data;

namespace Logic
{
    public class PriceTypesLogic
    {
        /// <summary>
        /// Retrieves a price type by ID
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeFK">The UserType ID</param>
        /// <param name="ProductFK">The Product ID</param>
        /// <returns>An object of type UserTypeProduct</returns>
        public UserTypeProduct RetrievePriceTypeByID(int UserTypeFK, Guid ProductFK)
        {
            try
            {
                return new PriceTypesRepository(false).RetrievePriceTypeByID(UserTypeFK, ProductFK);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a new price type
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeFK">The UserTypeID</param>
        /// <param name="ProductFK">The Product ID</param>
        /// <param name="Price">The Price</param>
        /// <param name="DiscountBegins">Discount Start Date</param>
        /// <param name="DiscountEnds">Discount End Date</param>
        /// <param name="DiscountPercent">Discount Percentage</param>
        public void AddPriceType(int UserTypeFK, Guid ProductFK, double Price,
            DateTime? DiscountBegins, DateTime? DiscountEnds, double? DiscountPercent)
        {
            try
            {
                UserTypeProduct myPriceType = new UserTypeProduct();

                myPriceType.UserTypeFK = UserTypeFK;
                myPriceType.ProductFK = ProductFK;
                myPriceType.Price = Price;
                myPriceType.DiscountDateFrom = DiscountBegins;
                myPriceType.DiscountDateTo = DiscountEnds;
                myPriceType.DiscountPercentage = DiscountPercent;

                new PriceTypesRepository().AddPriceType(myPriceType);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Specific Price Type
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeFK">The UserType ID</param>
        /// <param name="ProductFK">The Product ID</param>
        /// <param name="Price">The Price</param>
        /// <param name="DiscountBegins">The Discount Start Date</param>
        /// <param name="DiscountEnds">The Discount End Date</param>
        /// <param name="DiscountPercent">The Discount Percentage</param>
        public void UpdatePriceType(int UserTypeFK, Guid ProductFK, double Price,
            DateTime? DiscountBegins, DateTime? DiscountEnds, double? DiscountPercent)
        {
            try
            {
                new PriceTypesRepository().UpdatePriceType(UserTypeFK, ProductFK, Price, DiscountBegins,
                    DiscountEnds, DiscountPercent);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes a Price Type
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeFK">The UserType ID</param>
        /// <param name="ProductFK">The Product ID</param>
        public void RemoveSpecificPriceType(int UserTypeFK, Guid ProductFK)
        {
            try
            {
                new PriceTypesRepository().RemoveSpecificPriceType(UserTypeFK, ProductFK);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
