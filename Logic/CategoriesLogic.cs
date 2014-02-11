using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data;

namespace Logic
{
    public enum CategoryResult
    {
        Successful,
        Exists
    }

    public class CategoriesLogic
    {
        /// <summary>
        /// Retrieves all Parent Categories
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type Category</returns>
        public IQueryable<Category> RetrieveParentCategories()
        {
            try
            {
                return new CategoriesRepository(false).RetrieveParentCategories();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Retrieves all child categories
        /// Level: Logic
        /// </summary>
        /// <returns>A collection of type category</returns>
        public IQueryable<Category> RetrieveAllChildCategories()
        {
            return new CategoriesRepository(false).RetrieveAllChildCategories();
        }

        /// <summary>
        /// Retrieves all child categories by parent
        /// Level: Logic
        /// </summary>
        /// <param name="ParentID">The Parent ID</param>
        /// <returns>A collection of type category</returns>
        public IQueryable<Category> RetrieveChildCategories(int ParentID)
        {
            try
            {
                return new CategoriesRepository(false).RetrieveChildCategories(ParentID);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Assigns a Category as a Child Category
        /// Level: Logic
        /// </summary>
        /// <param name="CategoryID">The category id</param>
        /// <param name="Category">The category</param>
        /// <param name="ImageURL">The image url</param>
        /// <param name="ParentID">The parent id</param>
        /// <returns>If successful return true, else return false</returns>
        public bool AssignCategoryAsChild(int CategoryID, string Category, string ImageURL, int ParentID)
        {
            try
            {
                CategoriesRepository myRepository = new CategoriesRepository();

                Category myCategoryToUpdate = RetrieveCategoryByID(CategoryID);
                Category myCategoryToCompare = myRepository.Entities.Categories.SingleOrDefault(c => c.Category1 == Category);

                bool isSameLocation = false;

                if ((myCategoryToUpdate != null) && (myCategoryToCompare != null))
                {
                    if ((myCategoryToUpdate.Id == myCategoryToCompare.Id) && (myCategoryToUpdate.CategoryFK == myCategoryToUpdate.CategoryFK))
                    {
                        isSameLocation = true;
                    }
                }

                if (isSameLocation)
                {
                    if (!myRepository.CheckForSubCategories(CategoryID))
                    {
                        myRepository.AssignCategoryAsChild(CategoryID, Category, ImageURL, ParentID);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if ((!myRepository.CheckForSubCategories(CategoryID)) &&
                        (!myRepository.ChildCategoryExists(Category, ParentID)))
                    {
                        myRepository.AssignCategoryAsChild(CategoryID, Category, ImageURL, ParentID);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Assigns a category as a parent category
        /// Level: Logic
        /// </summary>
        /// <param name="CategoryID">The category id</param>
        /// <param name="Category">The category</param>
        /// <param name="ImageURL">The image url</param>
        /// <returns>True if successful, else false</returns>
        public bool AssignCategoryAsParent(int CategoryID, string Category, string ImageURL)
        {
            try
            {
                CategoriesRepository myRepository = new CategoriesRepository();

                if ((ImageURL == null) && (Category != null))
                {
                    if ((!myRepository.CheckForAssignedProducts(CategoryID)) &
                        (!myRepository.ParentCategoryExists(Category)))
                    {
                        myRepository.AssignCategoryAsParent(CategoryID, Category, ImageURL);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if ((ImageURL != null) && (Category != null))
                {
                    Category myCategoryToUpdate = RetrieveCategoryByID(CategoryID);
                    Category myCategoryToCompare = myRepository.Entities.Categories.SingleOrDefault(c => c.Category1 == Category);

                    if ((myCategoryToCompare != null) && (myCategoryToUpdate != null))
                    {
                        if (myCategoryToUpdate.Id == myCategoryToCompare.Id)
                        {
                            if (!myRepository.CheckForAssignedProducts(CategoryID))
                            {
                                myRepository.AssignCategoryAsParent(CategoryID, Category, ImageURL);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if ((!myRepository.CheckForAssignedProducts(CategoryID)) &
                            (!myRepository.ParentCategoryExists(Category)))
                            {
                                myRepository.AssignCategoryAsParent(CategoryID, Category, ImageURL);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if ((!myRepository.CheckForAssignedProducts(CategoryID)) &
                        (!myRepository.ParentCategoryExists(Category)))
                        {
                            myRepository.AssignCategoryAsParent(CategoryID, Category, ImageURL);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
        /// Adds a New Category
        /// Level: Logic
        /// </summary>
        /// <param name="Category">The category</param>
        /// <param name="ImageURL">The image url</param>
        /// <param name="CategoryFK">The Parent ID</param>
        /// <returns>Category Result Enum</returns>
        public CategoryResult AddCategory(string Category, string ImageURL, int CategoryFK)
        {
            try
            {
                CategoriesRepository myRepository = new CategoriesRepository();

                if (CategoryFK > 0)
                {
                    //is child

                    if(!myRepository.ChildCategoryExists(Category, CategoryFK))
                    {
                        Common.Category myCategory = new Category();

                        myCategory.Category1 = Category;
                        myCategory.ImageURL = ImageURL;

                        myCategory.CategoryFK = CategoryFK;

                        myRepository.AddCategory(myCategory);

                        return CategoryResult.Successful;
                    }
                    else
                    {
                        return CategoryResult.Exists;
                    }
                }
                else if (CategoryFK == 0)
                {
                    //is parent
                    if (!myRepository.ParentCategoryExists(Category))
                    {
                        Common.Category myCategory = new Category();

                        myCategory.Category1 = Category;
                        myCategory.ImageURL = ImageURL;

                        myCategory.CategoryFK = null;

                        myRepository.AddCategory(myCategory);

                        return CategoryResult.Successful;
                    }
                    else
                    {
                        return CategoryResult.Exists;
                    }
                }
                else
                {
                    return CategoryResult.Exists;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Deletes a category
        /// Level: Logic
        /// </summary>
        /// <param name="CategoryID">The category id</param>
        /// <returns>True if deleted, false if not deleted</returns>
        public bool DeleteCategory(int CategoryID)
        {   
            try
            {
                CategoriesRepository myRepository = new CategoriesRepository();
            
                if ((!myRepository.CheckForSubCategories(CategoryID)) &&
                    (!myRepository.CheckForAssignedProducts(CategoryID)))
                {
                    myRepository.DeleteCategory(CategoryID);
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
        /// Retrieves a category by id
        /// Level: Logic
        /// </summary>
        /// <param name="CategoryID">The category id</param>
        /// <returns>An object of type category</returns>
        public Category RetrieveCategoryByID(int CategoryID)
        {
            try
            {
                return new CategoriesRepository(false).RetrieveCategoryByID(CategoryID);
            }
            catch(Exception Exception)
            {
                throw Exception;
            }
        }

    }
}
