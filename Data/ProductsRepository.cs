using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Views;

namespace Data
{
    public class ProductsRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public ProductsRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public ProductsRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Retrieves products that have a wholesaler usertype currently bound and that match the current supplier
        /// Level: Data
        /// </summary>
        /// <param name="SupplierID">The Supplier ID</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayBySupplier(int SupplierID)
        {
            try
            {
                //return products that have a wholesaler usertype currently bound and that match the current supplier
                IQueryable<UserTypeProduct> myUserTypeProducts = Entities.UserTypeProducts.Where(up => up.UserType.Type == "Wholesaler" && up.Product.SupplierFK == SupplierID);
                IQueryable<Product> myProducts = Entities.Products.Where(p => p.SupplierFK == SupplierID && p.Status == true);

                List<Product> myProductsList = myProducts.ToList();

                foreach (Product myProduct in myProducts)
                {
                    if (myUserTypeProducts.SingleOrDefault(t => t.Product.Id == myProduct.Id) == null)
                    {
                        myProductsList.Remove(myProduct);
                    }
                }

                return myProductsList.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Products to Display to User
        /// Level: Data
        /// </summary>
        /// <param name="UserTypeID">User Type ID</param>
        /// <param name="ParentID">Parent ID</param>
        /// <param name="ChildCategoryID">Child ID</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayByUser(int UserTypeID, int? ParentID, int? ChildCategoryID)
        {
            try
            {
                IQueryable<UserTypeProduct> myUserTypeProducts = Entities.UserTypeProducts.Where(up => up.UserType.Id == UserTypeID);
                IQueryable<Product> myProducts = null;

                if ((ParentID == null) && (ChildCategoryID != null))
                {
                    myProducts = Entities.Products.Where(p => p.Status == true && p.CategoryFK == ChildCategoryID);
                }
                else if ((ParentID != null) && (ChildCategoryID == null))
                {
                    myProducts = Entities.Products.Where(p => p.Status == true && p.Category.CategoryFK == ParentID);
                }

                List<Product> myProductsList = myProducts.ToList();

                foreach (Product myProduct in myProducts)
                {
                    if (myUserTypeProducts.SingleOrDefault(t => t.Product.Id == myProduct.Id) == null)
                    {
                        myProductsList.Remove(myProduct);
                    }
                }

                return myProductsList.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Products to Be Displayed on User Search
        /// Level: Data
        /// </summary>
        /// <param name="UserTypeID">The User Type ID</param>
        /// <param name="SearchText">The Search Phrase</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayBySearch(int UserTypeID, string SearchText)
        {
            try
            {
                IQueryable<UserTypeProduct> myUserTypeProducts = Entities.UserTypeProducts.Where(up => up.UserType.Id == UserTypeID);
                IQueryable<Product> myProducts = null;

                myProducts = Entities.Products.Where(p => p.Status == true && p.Name.Contains(SearchText));
       
                List<Product> myProductsList = myProducts.ToList();

                foreach (Product myProduct in myProducts)
                {
                    if (myUserTypeProducts.SingleOrDefault(t => t.Product.Id == myProduct.Id) == null)
                    {
                        myProductsList.Remove(myProduct);
                    }
                }

                return myProductsList.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Products for Display By User Type
        /// Level: Data
        /// </summary>
        /// <param name="UserTypeID">The User Type ID</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayByUserType(int UserTypeID)
        {
            try
            {
                IQueryable<UserTypeProduct> myUserTypeProducts = Entities.UserTypeProducts.Where(up => up.UserType.Id == UserTypeID);
                IQueryable<Product> myProducts = null;

                myProducts = Entities.Products.Where(p => p.Status == true);

                List<Product> myProductsList = myProducts.ToList();

                foreach (Product myProduct in myProducts)
                {
                    if (myUserTypeProducts.SingleOrDefault(t => t.Product.Id == myProduct.Id) == null)
                    {
                        myProductsList.Remove(myProduct);
                    }
                }

                return myProductsList.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Product Price
        /// Level: Data
        /// </summary>
        /// <param name="UserType">The User Type ID</param>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>The Product Price</returns>
        public double RetrieveProductPrice(string UserType, Guid ProductID)
        {
            try
            {
                return Entities.UserTypeProducts.SingleOrDefault(ut => ut.Product.Id == ProductID && ut.UserType.Type == UserType).Price;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all Products
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type ProductsView</returns>
        public IQueryable<ProductsView> RetrieveAllProducts()
        {
            try
            {
                var list = from p in Entities.Products
                           join v in Entities.Vatrates
                           on p.VatrateFK equals v.Id
                           select new ProductsView
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Description = p.Description,
                               StockQty = p.StockQuantity,
                               ImageURL = p.ImageURL,
                               Status = p.Status,
                               CategoryFK = p.CategoryFK,
                               VatRate = v.Vatrate1,
                               ReorderLevel = p.ReorderLevel,
                               SupplierFK = p.SupplierFK
                           };

                return list.AsQueryable<ProductsView>();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Vatrate
        /// Level: Data
        /// </summary>
        /// <param name="VatRate">Vat Rate Value</param>
        /// <returns>VatRate Object</returns>
        public Vatrate RetrieveVatRate(double VatRate)
        {
            try
            {
                return Entities.Vatrates.SingleOrDefault(v => v.Vatrate1 == VatRate);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Product By ID
        /// Level: Data
        /// </summary>
        /// <param name="myProductID">The Product ID</param>
        /// <returns>An object of type Product</returns>
        public Product RetrieveProductByID(Guid myProductID)
        {
            try
            {
                return Entities.Products.SingleOrDefault(p => p.Id == myProductID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        
        /// <summary>
        /// Adds a Product
        /// Level: Data
        /// </summary>
        /// <param name="myProduct">The product to be Added</param>
        public void AddProduct(Product myProduct)
        {
            try
            {
                Entities.AddToProducts(myProduct);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        
        /// <summary>
        /// Updates a Product
        /// Level: Data
        /// </summary>
        /// <param name="myProduct">A Products view representing the product</param>
        public void UpdateProduct(ProductsView myProduct)
        {
            try
            {
                Product myOriginalProduct = RetrieveProductByID(myProduct.Id);

                myOriginalProduct.Name = myProduct.Name;
                myOriginalProduct.Description = myProduct.Description;

                if (myProduct.ImageURL != null)
                {
                    myOriginalProduct.ImageURL = myProduct.ImageURL;
                }

                myOriginalProduct.Status = myProduct.Status;
                myOriginalProduct.CategoryFK = myProduct.CategoryFK;
                myOriginalProduct.SupplierFK = myProduct.SupplierFK;
                myOriginalProduct.ReorderLevel = myProduct.ReorderLevel;

                Vatrate myVatRate = RetrieveVatRate(myProduct.VatRate);

                if (myVatRate != null)
                {
                    myOriginalProduct.Vatrate = myVatRate;
                }
                else
                {
                    myVatRate = new Vatrate();
                    myVatRate.Vatrate1 = myProduct.VatRate;
                    myOriginalProduct.Vatrate = myVatRate;
                }

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a Product
        /// Level: Data
        /// </summary>
        /// <param name="myProductID">The Product ID</param>
        public void DeleteProduct(Guid myProductID)
        {
            try
            {
                Entities.DeleteObject(RetrieveProductByID(myProductID));
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a Product is Listed in any Orders
        /// Level: Data
        /// </summary>
        /// <param name="myProductID">The Product ID</param>
        /// <returns>True if product has orders. False if product has no orders.</returns>
        public bool ProductHasOrders(Guid myProductID)
        {
            try
            {
                Product myProduct = RetrieveProductByID(myProductID);

                if (Entities.OrderProducts.Where(pop => pop.ProductFK == myProduct.Id).Count() > 0)
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
