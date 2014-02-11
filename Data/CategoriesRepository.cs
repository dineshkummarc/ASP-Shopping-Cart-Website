using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using Common;

namespace Data
{
    public class CategoriesRepository : ConnectionClass
    {
        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        public CategoriesRepository()
            : base()
        {
        }

        /// <summary>
        /// Calling ConnectionClass base constructor.
        /// Level: Data
        /// </summary>
        /// <param name="isAdmin">Determines whether the user is or is not an Administrator to use and alternate connection string.</param>
        public CategoriesRepository(bool isAdmin)
            : base(isAdmin)
        {
        }

        /// <summary>
        /// Retrieves all parent categories.
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type category. Where each category is a parent.</returns>
        public IQueryable<Category> RetrieveParentCategories()
        {
            try
            {
                var list = from c in Entities.Categories
                           where c.CategoryFK == null
                           select c;

                return list.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all child categories.
        /// Level: Data
        /// </summary>
        /// <returns>A collection of type category. Where each category is a child.</returns>
        public IQueryable<Category> RetrieveAllChildCategories()
        {
            return Entities.Categories.Where(c => c.CategoryFK != null);
        }

        /// <summary>
        /// Retrieves child categories depending on their parent.
        /// Level: Data
        /// </summary>
        /// <param name="ParentID">The child categories Parent ID.</param>
        /// <returns>A collection of type category.</returns>
        public IQueryable<Category> RetrieveChildCategories(int ParentID)
        {
            try
            {
                var list = from c in Entities.Categories
                           where c.CategoryFK == ParentID
                           select c;

                return list.AsQueryable();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves a category based on it's ID.
        /// Level: Data
        /// </summary>
        /// <param name="CategoryID">The categories ID.</param>
        /// <returns>An object of type Category.</returns>
        public Category RetrieveCategoryByID(int CategoryID)
        {
            try
            {
                return Entities.Categories.SingleOrDefault(c => c.Id == CategoryID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Assigns a category as a child category.
        /// Level: Data
        /// </summary>
        /// <param name="CategoryID">The categories ID.</param>
        /// <param name="Category">The categories Name.</param>
        /// <param name="ImageURL">The categories Image URL.</param>
        /// <param name="ParentID">The categories Parent ID.</param>
        public void AssignCategoryAsChild(int CategoryID, string Category, string ImageURL, int ParentID)
        {
            try
            {
                Category myCategory = RetrieveCategoryByID(CategoryID);

                myCategory.Category1 = Category;

                if (ImageURL != null)
                {
                    myCategory.ImageURL = ImageURL;
                }

                myCategory.CategoryFK = ParentID;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a Parent Category has any Child Categories.
        /// Level: Data
        /// </summary>
        /// <param name="CategoryID">The categories ID.</param>
        /// <returns>False if no sub categories exist. True if sub categories exist.</returns>
        public bool CheckForSubCategories(int CategoryID)
        {
            try
            {
                var list = from c in Entities.Categories
                           where c.CategoryFK == CategoryID
                           select c;

                if (list.Count() == 0)
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
        /// Checks if a category with the same name already exists.
        /// Level: Data
        /// </summary>
        /// <param name="Category">The categories Name.</param>
        /// <returns>False if no categories exist. True if categories exist.</returns>
        public bool CategoryExists(string Category)
        {
            try
            {
                if (Entities.Categories.SingleOrDefault(c => c.Category1 == Category) != null)
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
        /// Checks if a parent category with the same name already exists.
        /// Level: Data
        /// </summary>
        /// <param name="Category">The categories Name.</param>
        /// <returns>False if no categories exist. True if categories exist.</returns>
        public bool ParentCategoryExists(string Category)
        {
            var list = from c in Entities.Categories
                       where c.Category1 == Category &&
                             c.CategoryFK == null
                       select c;

            if (list.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if a child category under a particular parent category already exists.
        /// Level: Data
        /// </summary>
        /// <param name="Category">The categories Name.</param>
        /// <param name="CategoryFK">The categories Parent ID.</param>
        /// <returns>False if no categories exist. True if categories exist.</returns>
        public bool ChildCategoryExists(string Category, int CategoryFK)
        {
            var list = from c in Entities.Categories
                       where c.Category1 == Category &&
                             c.CategoryFK == CategoryFK
                       select c;

            if (list.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Assigns a category as a parent category.
        /// Level: Data
        /// </summary>
        /// <param name="CategoryID">The categories ID.</param>
        /// <param name="Category">The categories Name.</param>
        /// <param name="ImageURL">The categories Image URL.</param>
        public void AssignCategoryAsParent(int CategoryID, string Category, string ImageURL)
        {
            try
            {
                Category myCategory = RetrieveCategoryByID(CategoryID);

                myCategory.Category1 = Category;

                if (ImageURL != null)
                {
                    myCategory.ImageURL = ImageURL;
                }

                myCategory.CategoryFK = null;

                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Adds a new category.
        /// Level: Data
        /// </summary>
        /// <param name="myCategory">The category object.</param>
        public void AddCategory(Category myCategory)
        {
            try
            {
                Entities.AddToCategories(myCategory);
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a category.
        /// Level: Data
        /// </summary>
        /// <param name="CategoryID">The categories ID.</param>
        public void DeleteCategory(int CategoryID)
        {
            try
            {
                Entities.DeleteObject(RetrieveCategoryByID(CategoryID));
                Entities.SaveChanges();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Checks if a category has products assigned to it.
        /// Level: Data
        /// </summary>
        /// <param name="CategoryID">The categories ID.</param>
        /// <returns>False if no products exist. True if products exist.</returns>
        public bool CheckForAssignedProducts(int CategoryID)
        {
            try
            {
                if (Entities.Products.Where(p => p.CategoryFK == CategoryID).Count() > 0)
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
