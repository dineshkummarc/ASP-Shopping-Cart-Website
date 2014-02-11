using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Common;
using Logic;

namespace External.administrator
{
    public partial class category : System.Web.UI.Page
    {
        enum CategoryShift
        {
            Nothing,
            ChildToChild,
            ChildToParent,
            ParentToChild,
            ParentToParent,
            ChildCategoryError,
            Error
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MaintainScrollPositionOnPostBack = true;

                if (!Page.IsPostBack)
                {
                    gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                    gvCategories.DataBind();

                    ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                    ddlParent.DataTextField = "Category1";
                    ddlParent.DataValueField = "Id";
                    ddlParent.DataBind();
                    ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));
                }                
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Categories GridView item is Deleted
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Category myCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(e.Keys[0]));

                string PreviousURL = myCategory.ImageURL;

                if (new CategoriesLogic().DeleteCategory(myCategory.Id) == false)
                {
                    //deletion error, has sub categories
                    lblServerSideErrorTop.Text = "Category has Existing Child Categories and Cannot be Deleted";
                }
                else
                {
                    File.Delete(Server.MapPath(PreviousURL));

                    gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                    gvCategories.DataBind();

                    gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                    gvSubCategories.DataBind();

                    lblServerSideErrorBottom.Text = "";
                    lblServerSideErrorTop.Text = "&nbsp;";
                    gvCategories.SelectedIndex = -1;
                    gvSubCategories.SelectedIndex = -1;
                    txtCategory.Text = "";
                    imgSelectedCategory.Visible = false;
                    btnUpdate.Visible = false;
                    ReqValImage.Visible = true;
                    btnAdd.Visible = true;

                    ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                    ddlParent.DataTextField = "Category1";
                    ddlParent.DataValueField = "Id";
                    ddlParent.DataBind();
                    ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Categories Grid View Selected Index is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvSubCategories.SelectedIndex = -1;
                ReqValImage.Visible = false;
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                lblServerSideErrorBottom.Text = "";
                lblServerSideErrorTop.Text = "&nbsp;";

                gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                gvSubCategories.DataBind();

                Category myCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvCategories.SelectedValue));
                txtCategory.Text = myCategory.Category1;

                imgSelectedCategory.Visible = true;

                imgSelectedCategory.ImageUrl = Page.ResolveClientUrl(myCategory.ImageURL);

                ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                ddlParent.DataTextField = "Category1";
                ddlParent.DataValueField = "Id";
                ddlParent.DataBind();
                ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));
                ddlParent.Items.Remove(new ListItem(myCategory.Category1, myCategory.Id.ToString()));

                ddlParent.SelectedValue = "0";
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Sub Categories Grid View Selected index is changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSubCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvCategories.SelectedIndex = -1;
                ReqValImage.Visible = false;
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                lblServerSideErrorBottom.Text = "";
                lblServerSideErrorTop.Text = "&nbsp;";

                Category myCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvSubCategories.SelectedValue));
                txtCategory.Text = myCategory.Category1;

                imgSelectedCategory.Visible = true;

                imgSelectedCategory.ImageUrl = Page.ResolveClientUrl(myCategory.ImageURL);

                ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                ddlParent.DataTextField = "Category1";
                ddlParent.DataValueField = "Id";
                ddlParent.DataBind();
                ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));

                ddlParent.SelectedValue = myCategory.CategoryFK.ToString();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Sub Categories Grid View Row is being deleted
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSubCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Category myCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(e.Keys[0]));

                string PreviousURL = myCategory.ImageURL;

                if (new CategoriesLogic().DeleteCategory(myCategory.Id) == false)
                {
                    lblServerSideErrorTop.Text = "Category is Bound to One or More Products and Cannot be Deleted";
                }
                else
                {
                    File.Delete(Server.MapPath(PreviousURL));

                    gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                    gvCategories.DataBind();

                    gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(myCategory.CategoryFK));
                    gvSubCategories.DataBind();

                    gvCategories.SelectedIndex = -1;
                    gvSubCategories.SelectedIndex = -1;

                    txtCategory.Text = "";
                    imgSelectedCategory.Visible = false;
                    btnUpdate.Visible = false;
                    ReqValImage.Visible = true;
                    btnAdd.Visible = true;
                    lblServerSideErrorBottom.Text = "";
                    lblServerSideErrorTop.Text = "&nbsp;";

                    ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                    ddlParent.DataTextField = "Category1";
                    ddlParent.DataValueField = "Id";
                    ddlParent.DataBind();
                    ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Uploads an Image for A Category
        /// Level: External
        /// </summary>
        /// <param name="myUploadControl">The Upload Control</param>
        /// <returns>Tuple Where string is the URL and UploadResult is an Enum</returns>
        private Tuple<string, UploadResult> UploadImage(FileUpload myUploadControl)
        {
            return new External.administrator.UploadImage().Upload(myUploadControl);
        }

        /// <summary>
        /// Occurs when the Add Button is Clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    Tuple<string, UploadResult> myTuple = UploadImage(fuImage);

                    if (myTuple.Item2 == UploadResult.Successful)
                    {
                        string Category = txtCategory.Text.Trim();
                        string ImageURL = myTuple.Item1;
                        int ParentID = Convert.ToInt32(ddlParent.SelectedValue);

                        if (new CategoriesLogic().AddCategory(Category, ImageURL, ParentID) == CategoryResult.Successful)
                        {
                            if (ParentID == 0)
                            {
                                gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                                gvCategories.DataBind();

                                gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                                gvSubCategories.DataBind();
                            }
                            else
                            {
                                gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                                gvCategories.DataBind();

                                gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(ParentID));
                                gvSubCategories.DataBind();
                            }

                            txtCategory.Text = "";
                            lblServerSideErrorBottom.Text = "";
                            lblServerSideErrorTop.Text = "&nbsp;";

                            ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                            ddlParent.DataTextField = "Category1";
                            ddlParent.DataValueField = "Id";
                            ddlParent.DataBind();
                            ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));

                            ddlParent.SelectedIndex = 0;
                        }
                        else
                        {
                            File.Delete(Server.MapPath(ImageURL));

                            lblServerSideErrorBottom.Text = "Category Already Exists at Specified Level";
                        }
                    }
                    else if (myTuple.Item2 == UploadResult.InvalidExtension)
                    {
                        lblServerSideErrorBottom.Text = "Invalid Image Extension";
                    }
                }
            }
            catch(Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Clear Button is clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                gvCategories.SelectedIndex = -1;
                gvSubCategories.SelectedIndex = -1;
                txtCategory.Text = "";
                imgSelectedCategory.Visible = false;
                btnUpdate.Visible = false;
                ReqValImage.Visible = true;
                btnAdd.Visible = true;
                lblServerSideErrorBottom.Text = "";
                lblServerSideErrorTop.Text = "&nbsp;";

                ddlParent.DataSource = new CategoriesLogic().RetrieveParentCategories();
                ddlParent.DataTextField = "Category1";
                ddlParent.DataValueField = "Id";
                ddlParent.DataBind();
                ddlParent.Items.Insert(0, new ListItem("Set: Parent", "0"));

                gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                gvSubCategories.DataBind();
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Update Button is Clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    lblServerSideErrorBottom.Text = "";
                    lblServerSideErrorTop.Text = "&nbsp;";

                    CategoryShift ShiftedAs = CategoryShift.Nothing;

                    if (gvCategories.SelectedIndex == -1)
                    {
                        //The Category is a Child

                        int CategoryID = Convert.ToInt32(gvSubCategories.SelectedValue);
                        string Category = txtCategory.Text.Trim();

                        if (ddlParent.SelectedIndex == 0)
                        {
                            //Set the Category as a Parent Category

                            Tuple<string, UploadResult> myTuple = UploadImage(fuImage);

                            if (myTuple.Item2 == UploadResult.Successful)
                            {
                                string PreviousURL = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvSubCategories.SelectedValue)).ImageURL;

                                if (new CategoriesLogic().AssignCategoryAsParent(CategoryID, Category, myTuple.Item1))
                                {
                                    File.Delete(Server.MapPath(PreviousURL));

                                    imgSelectedCategory.ImageUrl = Page.ResolveClientUrl(myTuple.Item1);

                                    ShiftedAs = CategoryShift.ChildToParent;
                                }
                                else
                                {
                                    File.Delete(Server.MapPath(myTuple.Item1));

                                    lblServerSideErrorTop.Text = "Category Already Exists or is Bound to One or More Products and Cannot be Elevated";

                                    ShiftedAs = CategoryShift.ChildCategoryError;
                                }
                            }
                            else if (myTuple.Item2 == UploadResult.NoImageFound)
                            {
                                if (new CategoriesLogic().AssignCategoryAsParent(CategoryID, Category, null))
                                {
                                    ShiftedAs = CategoryShift.ChildToParent;
                                }
                                else
                                {
                                    lblServerSideErrorTop.Text = "Category Already Exists or is Bound to One or More Products and Cannot be Elevated";

                                    ShiftedAs = CategoryShift.ChildCategoryError;
                                }
                            }
                            else
                            {
                                //inval extens
                                lblServerSideErrorBottom.Text = "Invalid Image Extension";
                                ShiftedAs = CategoryShift.Error;
                            }
                        }
                        else
                        {
                            //Set the Category as a Child Category

                            Tuple<string, UploadResult> myTuple = UploadImage(fuImage);

                            if (myTuple.Item2 == UploadResult.Successful)
                            {
                                string PreviousURL = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvSubCategories.SelectedValue)).ImageURL;

                                if (new CategoriesLogic().AssignCategoryAsChild(CategoryID, Category, myTuple.Item1, Convert.ToInt32(ddlParent.SelectedValue)))
                                {
                                    File.Delete(Server.MapPath(PreviousURL));
                                    imgSelectedCategory.ImageUrl = Page.ResolveClientUrl(myTuple.Item1);
                                    ShiftedAs = CategoryShift.ChildToChild;
                                }
                                else
                                {
                                    File.Delete(Server.MapPath(myTuple.Item1));
                                    lblServerSideErrorTop.Text = "Child Category Already Exists";
                                    ShiftedAs = CategoryShift.ChildCategoryError;
                                }
                            }
                            else if (myTuple.Item2 == UploadResult.NoImageFound)
                            {
                                if (new CategoriesLogic().AssignCategoryAsChild(CategoryID, Category, null, Convert.ToInt32(ddlParent.SelectedValue)))
                                {
                                    ShiftedAs = CategoryShift.ChildToChild;
                                }
                                else
                                {
                                    lblServerSideErrorTop.Text = "Child Category Already Exists";
                                    ShiftedAs = CategoryShift.ChildCategoryError;
                                }
                            }
                            else
                            {
                                //extension error
                                lblServerSideErrorBottom.Text = "Invalid Image Extension";
                                ShiftedAs = CategoryShift.Error;
                            }
                        }
                    }
                    else
                    {
                        //The Category is a Parent

                        int CategoryID = Convert.ToInt32(gvCategories.SelectedValue);
                        string Category = txtCategory.Text.Trim();

                        if (ddlParent.SelectedIndex == 0)
                        {
                            //Set the Category as a Parent Category

                            Tuple<string, UploadResult> myTuple = UploadImage(fuImage);

                            if (myTuple.Item2 == UploadResult.Successful)
                            {
                                string PreviousURL = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvCategories.SelectedValue)).ImageURL;

                                if (new CategoriesLogic().AssignCategoryAsParent(CategoryID, Category, myTuple.Item1))
                                {
                                    File.Delete(Server.MapPath(PreviousURL));

                                    imgSelectedCategory.ImageUrl = Page.ResolveClientUrl(myTuple.Item1);

                                    ShiftedAs = CategoryShift.ParentToParent;
                                }
                                else
                                {
                                    File.Delete(Server.MapPath(myTuple.Item1));

                                    lblServerSideErrorTop.Text = "Parent Category Already Exists";

                                    ShiftedAs = CategoryShift.Error;
                                }
                            }
                            else if (myTuple.Item2 == UploadResult.NoImageFound)
                            {
                                if (new CategoriesLogic().AssignCategoryAsParent(CategoryID, Category, null))
                                {
                                    ShiftedAs = CategoryShift.ParentToParent;
                                }
                                else
                                {
                                    lblServerSideErrorTop.Text = "Parent Category Already Exists";

                                    ShiftedAs = CategoryShift.Error;
                                }
                            }
                            else
                            {
                                //extension error
                                lblServerSideErrorBottom.Text = "Invalid Image Extension";
                                ShiftedAs = CategoryShift.Error;
                            }
                        }
                        else
                        {
                            //Set the Category as a Child Category

                            Tuple<string, UploadResult> myTuple = UploadImage(fuImage);
                            Category myCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvCategories.SelectedValue));
                            string PreviousURL = myCategory.ImageURL;
                            string PreviousCategory = myCategory.Category1;

                            if (myTuple.Item2 == UploadResult.Successful)
                            {
                                if (new CategoriesLogic().AssignCategoryAsChild(CategoryID, Category, myTuple.Item1, Convert.ToInt32(ddlParent.SelectedValue)) == false)
                                {
                                    //error cannot sublevel this category
                                    File.Delete(Server.MapPath(myTuple.Item1));
                                    lblServerSideErrorTop.Text = "Category Already Exists or has Existing Child Categories and Cannot be Sub Levelled";
                                    ShiftedAs = CategoryShift.Error;
                                    ddlParent.SelectedIndex = 0;
                                    txtCategory.Text = PreviousCategory;
                                }
                                else
                                {
                                    File.Delete(Server.MapPath(PreviousURL));
                                    ShiftedAs = CategoryShift.ParentToChild;
                                    imgSelectedCategory.ImageUrl = Page.ResolveClientUrl(myTuple.Item1);
                                }
                            }
                            else if (myTuple.Item2 == UploadResult.NoImageFound)
                            {
                                if (new CategoriesLogic().AssignCategoryAsChild(CategoryID, Category, null, Convert.ToInt32(ddlParent.SelectedValue)) == false)
                                {
                                    //error cannot sublevel this category
                                    lblServerSideErrorTop.Text = "Category Already Exists or has Existing Child Categories and Cannot be Sub Levelled";
                                    ShiftedAs = CategoryShift.Error;
                                    ddlParent.SelectedIndex = 0;
                                    txtCategory.Text = PreviousCategory;
                                }
                                else
                                {
                                    ShiftedAs = CategoryShift.ParentToChild;
                                }
                            }
                            else
                            {
                                //extension error
                                lblServerSideErrorBottom.Text = "Invalid Image Extension";
                                ShiftedAs = CategoryShift.Error;
                            }
                        }
                    }

                    //Setting Grid Views Depending on Update Action
                    if (ShiftedAs == CategoryShift.ChildToChild)
                    {
                        Category myParentCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(ddlParent.SelectedValue));

                        gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                        gvCategories.DataBind();

                        foreach (GridViewRow myRow in gvCategories.Rows)
                        {
                            if (gvCategories.DataKeys[myRow.RowIndex].Value.Equals(myParentCategory.Id))
                            {
                                gvCategories.SelectedIndex = myRow.RowIndex;
                                break;
                            }
                        }

                        Category myChildCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvSubCategories.SelectedValue));

                        gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                        gvSubCategories.DataBind();

                        foreach (GridViewRow myRow in gvSubCategories.Rows)
                        {
                            if (gvSubCategories.DataKeys[myRow.RowIndex].Value.Equals(myChildCategory.Id))
                            {
                                gvCategories.SelectedIndex = -1;
                                gvSubCategories.SelectedIndex = myRow.RowIndex;
                                break;
                            }
                        }
                    }
                    else if (ShiftedAs == CategoryShift.ChildToParent)
                    {
                        Category myParentCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvSubCategories.SelectedValue));

                        gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                        gvCategories.DataBind();

                        foreach (GridViewRow myRow in gvCategories.Rows)
                        {
                            if (gvCategories.DataKeys[myRow.RowIndex].Value.Equals(myParentCategory.Id))
                            {
                                gvCategories.SelectedIndex = myRow.RowIndex;
                                break;
                            }
                        }

                        gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                        gvSubCategories.DataBind();
                    }
                    else if (ShiftedAs == CategoryShift.ParentToChild)
                    {
                        Category myChildCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvCategories.SelectedValue));

                        gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(ddlParent.SelectedValue));
                        gvSubCategories.DataBind();

                        foreach (GridViewRow myRow in gvSubCategories.Rows)
                        {
                            if (gvSubCategories.DataKeys[myRow.RowIndex].Value.Equals(myChildCategory.Id))
                            {
                                gvSubCategories.SelectedIndex = myRow.RowIndex;
                                break;
                            }
                        }

                        gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                        gvCategories.DataBind();
                        gvCategories.SelectedIndex = -1;
                    }
                    else if ((ShiftedAs == CategoryShift.ParentToParent) || (ShiftedAs == CategoryShift.Error) || (ShiftedAs == CategoryShift.ChildCategoryError))
                    {
                        //if ((gvCategories.SelectedIndex == -1) && (gvSubCategories.SelectedIndex == -1))
                        //{ 
                        //    gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                        //    gvCategories.DataBind();

                        //    gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                        //    gvSubCategories.DataBind();
                        //}
                        //Error populating
                        if ((gvSubCategories.SelectedIndex == -1) && (gvCategories.SelectedIndex > -1))
                        {
                            Category myCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvCategories.SelectedValue));

                            gvCategories.DataSource = new CategoriesLogic().RetrieveParentCategories();
                            gvCategories.DataBind();

                            foreach (GridViewRow myRow in gvCategories.Rows)
                            {
                                if (gvCategories.DataKeys[myRow.RowIndex].Value.Equals(myCategory.Id))
                                {
                                    gvCategories.SelectedIndex = myRow.RowIndex;
                                    break;
                                }
                            }

                            gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(gvCategories.SelectedValue));
                            gvSubCategories.DataBind();

                            txtCategory.Text = myCategory.Category1;
                            ddlParent.SelectedValue = "0";
                            //dropdown??
                        }
                        else if ((gvCategories.SelectedIndex == -1) && (gvSubCategories.SelectedIndex > -1))
                        {
                            Category mySubCategory = new CategoriesLogic().RetrieveCategoryByID(Convert.ToInt32(gvSubCategories.SelectedValue));

                            gvSubCategories.DataSource = new CategoriesLogic().RetrieveChildCategories(Convert.ToInt32(mySubCategory.CategoryFK));
                            gvSubCategories.DataBind();

                            foreach (GridViewRow myRow in gvSubCategories.Rows)
                            {
                                if (gvSubCategories.DataKeys[myRow.RowIndex].Value.Equals(mySubCategory.Id))
                                {
                                    gvSubCategories.SelectedIndex = myRow.RowIndex;
                                    break;
                                }
                            }

                            txtCategory.Text = mySubCategory.Category1;
                            ddlParent.SelectedValue = mySubCategory.CategoryFK.ToString();
                            //dropdown??
                        }
                    }
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}