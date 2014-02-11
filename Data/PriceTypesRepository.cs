using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data
{
    public class PriceTypesRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public PriceTypesRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public PriceTypesRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Adds a Price Type
        /// Level: Data
        /// </summary>
        /// <param name="myPriceType">The Price Type to Add</param>
        public void AddPriceType(UserTypeProduct myPriceType)
        {
            try
            {
                Entities.AddToUserTypeProducts(myPriceType);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Price Type
        /// Level: Data
        /// </summary>
        /// <param name="UserTypeFK">The User Type ID</param>
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
                UserTypeProduct myPriceType = RetrievePriceTypeByID(UserTypeFK, ProductFK);

                myPriceType.Price = Price;
                myPriceType.DiscountDateFrom = DiscountBegins;
                myPriceType.DiscountDateTo = DiscountEnds;
                myPriceType.DiscountPercentage = DiscountPercent;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Price Type By ID
        /// Level: Data
        /// </summary>
        /// <param name="UserTypeFK">The User Type ID</param>
        /// <param name="ProductFK">The Product ID</param>
        /// <returns></returns>
        public UserTypeProduct RetrievePriceTypeByID(int UserTypeFK, Guid ProductFK)
        {
            try
            {
                return Entities.UserTypeProducts.SingleOrDefault(p => p.UserTypeFK == UserTypeFK && p.ProductFK == ProductFK);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Removes a Specific Price Type
        /// Level: Data
        /// </summary>
        /// <param name="UserTypeFK">The User Type ID</param>
        /// <param name="ProductFK">The Product ID</param>
        public void RemoveSpecificPriceType(int UserTypeFK, Guid ProductFK)
        {
            try
            {
                UserTypeProduct myPriceType = RetrievePriceTypeByID(UserTypeFK, ProductFK);

                Entities.DeleteObject(myPriceType);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}
