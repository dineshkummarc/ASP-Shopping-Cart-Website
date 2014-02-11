using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Views;
using Data;

namespace Logic
{
    public class ProductsLogic
    {
        /// <summary>
        /// Retrieves all Products
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type ProductsView</returns>
        public IQueryable<ProductsView> RetrieveAllProducts()
        {
            try
            {
                return new ProductsRepository(false).RetrieveAllProducts();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a Product By ID
        /// Level: Logic
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>An object of type Product</returns>
        public Product RetrieveProductByID(string ProductID)
        {
            try
            {
                return new ProductsRepository(false).RetrieveProductByID(Guid.Parse(ProductID));
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves products for display by supplier
        /// Level: Logic
        /// </summary>
        /// <param name="SupplierID">The Supplier ID</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayBySupplier(int SupplierID)
        {
            try
            {
                return new ProductsRepository().RetrieveProductsForDisplayBySupplier(SupplierID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Products for Display By User
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeID">The UserType ID</param>
        /// <param name="ParentCategoryID">The Parent CategoryID</param>
        /// <param name="ChildCategoryID">The ChildID</param>
        /// <returns>A collection of type product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayByUser(int UserTypeID, int? ParentCategoryID, int? ChildCategoryID)
        {
            try
            {
                return new ProductsRepository(false).RetrieveProductsForDisplayByUser(UserTypeID, ParentCategoryID, ChildCategoryID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves products for display when searching
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeID">The UserTypeID</param>
        /// <param name="SearchText">The Search Phrase</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayBySearch(int UserTypeID, string SearchText)
        {
            try
            {
                return new ProductsRepository(false).RetrieveProductsForDisplayBySearch(UserTypeID, SearchText);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a ProductPrice
        /// Level: Logic
        /// </summary>
        /// <param name="UserType">The User Type</param>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>The Price</returns>
        public double RetrieveProductPrice(string UserType, Guid ProductID)
        {
            try
            {
                return new ProductsRepository(false).RetrieveProductPrice(UserType, ProductID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves Products for Display By Usertype
        /// Level: Logic
        /// </summary>
        /// <param name="UserTypeID">The UserType ID</param>
        /// <returns>A collection of type Product</returns>
        public IQueryable<Product> RetrieveProductsForDisplayByUserType(int UserTypeID)
        {
            return new ProductsRepository(false).RetrieveProductsForDisplayByUserType(UserTypeID);
        }

        /// <summary>
        /// Adds A Product
        /// Level: Logic
        /// </summary>
        /// <param name="Name">Product Name</param>
        /// <param name="Status">Product Status</param>
        /// <param name="Description">Product Description</param>
        /// <param name="ImageURL">Product ImageURL</param>
        /// <param name="CategoryFK">The Category ID</param>
        /// <param name="VatRate">The VatRate</param>
        /// <param name="SupplierFK">The Supplier FK</param>
        /// <param name="ReorderLevel">The Reorder Level</param>
        public void AddProduct(string Name, bool Status, string Description, string ImageURL, int CategoryFK, 
            double VatRate, int SupplierFK, int ReorderLevel)
        {
            try
            {
                ProductsRepository myRepository = new ProductsRepository();

                Product myProduct = new Product();

                myProduct.Id = Guid.NewGuid();
                myProduct.Name = Name;
                myProduct.Description = Description;
                myProduct.StockQuantity = 0;
                myProduct.Status = Status;
                myProduct.ImageURL = ImageURL;
                myProduct.CategoryFK = CategoryFK;
                myProduct.SupplierFK = SupplierFK;
                myProduct.ReorderLevel = ReorderLevel;

                Vatrate myVatRate = myRepository.RetrieveVatRate(VatRate);

                if (myVatRate != null)
                {
                    myProduct.Vatrate = myVatRate;
                }
                else
                {
                    myVatRate = new Vatrate();
                    myVatRate.Vatrate1 = VatRate;
                    myProduct.Vatrate = myVatRate;
                }

                myRepository.AddProduct(myProduct);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Updates a Product
        /// Level: Logic
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <param name="Name">The Product Name</param>
        /// <param name="Description">The Product Description</param>
        /// <param name="ImageURL">The Product Image URL</param>
        /// <param name="Status">The Product Status</param>
        /// <param name="CategoryFK">The Category FK</param>
        /// <param name="VatRate">The VatRate</param>
        /// <param name="SupplierFK">The SupplierFK</param>
        /// <param name="ReorderLevel">The Reorder Level</param>
        public void UpdateProduct(string ProductID, string Name, string Description,
            string ImageURL, bool Status, int CategoryFK, double VatRate, int SupplierFK, int ReorderLevel)
        {
            try
            {
                ProductsRepository myRepository = new ProductsRepository();

                ProductsView myProduct = new ProductsView();

                myProduct.Id = Guid.Parse(ProductID);
                myProduct.Name = Name;
                myProduct.Description = Description;
                myProduct.ImageURL = ImageURL;
                myProduct.Status = Status;
                myProduct.CategoryFK = CategoryFK;
                myProduct.VatRate = VatRate;
                myProduct.SupplierFK = SupplierFK;
                myProduct.ReorderLevel = ReorderLevel;

                myRepository.UpdateProduct(myProduct);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
        
        /// <summary>
        /// Deletes a Product
        /// Level: Logic
        /// </summary>
        /// <param name="ProductID">The Product ID</param>
        /// <returns>True if successful, false if not.</returns>
        public bool DeleteProduct(Guid ProductID)
        {
            try
            {
                ProductsRepository myRepository = new ProductsRepository();

                if (!myRepository.ProductHasOrders(ProductID))
                {
                    myRepository.DeleteProduct(ProductID);
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
