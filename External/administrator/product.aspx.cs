using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Logic;
using Common;
using Common.Views;

namespace External.administrator
{
    public partial class product : System.Web.UI.Page
    {
        protected bool LoadProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MaintainScrollPositionOnPostBack = true;

                LoadProducts = true;

                if (!Page.IsPostBack)
                {
                    IQueryable<Category> myCategories = new CategoriesLogic().RetrieveAllChildCategories();
                    IQueryable<SuppliersView> mySuppliers = new SuppliersLogic().RetrieveAllSuppliers();

                    if ((mySuppliers.Count() > 0) && (myCategories.Count() > 0))
                    {
                        gvProducts.DataSource = new ProductsLogic().RetrieveAllProducts();
                        gvProducts.DataBind();

                        ddlStatus.Items.Add(new ListItem("Active", "true"));
                        ddlStatus.Items.Add(new ListItem("Inactive", "false"));

                        ddlCategory.DataSource = myCategories;
                        ddlCategory.DataTextField = "Category1";
                        ddlCategory.DataValueField = "Id";
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, new ListItem("Select", "0"));

                        ddlSupplier.DataSource = mySuppliers;
                        ddlSupplier.DataTextField = "Supplier";
                        ddlSupplier.DataValueField = "Id";
                        ddlSupplier.DataBind();
                        ddlSupplier.Items.Insert(0, new ListItem("Select", "0"));

                        LoadProducts = true;
                    }
                    else
                    {
                        lblServerSideError.Text = "Suppliers and Categories must be added in order to add Products";

                        LoadProducts = false;
                    }
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when The Product Add Button is Clicked
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
                    lblImageError.Text = "";
                    lblErrorDisplay.Text = "&nbsp;";

                    Tuple<string, UploadResult> myResult = new UploadImage().Upload(fuImage);

                    if (myResult.Item2 == UploadResult.InvalidExtension)
                    {
                        lblImageError.Text = "Invalid Image Extension";
                    }
                    else if (myResult.Item2 == UploadResult.NoImageFound)
                    {
                        lblImageError.Text = "No Image Found";
                    }
                    else
                    {
                        string Name = txtName.Text;
                        string Description = txtDescription.Text;
                        bool Status = Convert.ToBoolean(ddlStatus.SelectedValue);
                        int CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                        int SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
                        double VatRate = Convert.ToDouble(txtVatRate.Text);
                        int ReorderLevel = Convert.ToInt32(txtReorderLevel.Text);
                        string ImageURL = myResult.Item1;

                        new ProductsLogic().AddProduct(Name, Status, Description, ImageURL, CategoryID, VatRate, SupplierID, ReorderLevel);

                        gvProducts.DataSource = new ProductsLogic().RetrieveAllProducts();
                        gvProducts.DataBind();

                        gvProducts.SelectedIndex = -1;
                        txtName.Text = "";
                        txtDescription.Text = "";
                        txtVatRate.Text = "";
                        txtReorderLevel.Text = "";
                        ddlStatus.SelectedValue = "true";
                        ddlSupplier.SelectedValue = "0";
                        ddlCategory.SelectedValue = "0";
                    }
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Product Update Button is Clicked
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
                    lblImageError.Text = "";
                    lblErrorDisplay.Text = "&nbsp;";

                    Tuple<string, UploadResult> myResult = new UploadImage().Upload(fuImage);
                    string PreviousURL = new ProductsLogic().RetrieveProductByID(gvProducts.SelectedValue.ToString()).ImageURL;

                    string ProductID = gvProducts.SelectedValue.ToString();
                    string Name = txtName.Text;
                    string Description = txtDescription.Text;
                    bool Status = Convert.ToBoolean(ddlStatus.SelectedValue);
                    int CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    int SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    int ReorderLevel = Convert.ToInt32(txtReorderLevel.Text);
                    double VatRate = Convert.ToDouble(txtVatRate.Text);
                    string ImageURL = null;

                    if (myResult.Item2 == UploadResult.InvalidExtension)
                    {
                        lblImageError.Text = "Invalid Image Extension";
                    }
                    else if (myResult.Item2 == UploadResult.NoImageFound)
                    {
                        new ProductsLogic().UpdateProduct(ProductID, Name, Description, ImageURL, Status, CategoryID, VatRate, SupplierID, ReorderLevel);

                        gvProducts.DataSource = new ProductsLogic().RetrieveAllProducts();
                        gvProducts.DataBind();
                    }
                    else
                    {
                        File.Delete(Server.MapPath(PreviousURL));

                        ImageURL = myResult.Item1;

                        new ProductsLogic().UpdateProduct(ProductID, Name, Description, ImageURL, Status, CategoryID, VatRate, SupplierID, ReorderLevel);

                        gvProducts.DataSource = new ProductsLogic().RetrieveAllProducts();
                        gvProducts.DataBind();

                        imgProduct.ImageUrl = Page.ResolveClientUrl(ImageURL);
                    }
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Product Clear Button is Clicked
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblImageError.Text = "";
                lblErrorDisplay.Text = "&nbsp;";
                gvProducts.SelectedIndex = -1;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                txtName.Text = "";
                txtDescription.Text = "";
                txtVatRate.Text = "";
                txtReorderLevel.Text = "";
                ddlStatus.SelectedValue = "true";
                imgProduct.Visible = false;
                ddlSupplier.SelectedValue = "0";
                ddlCategory.SelectedValue = "0";
                reqValImage.Visible = true;
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Products Grid View Selected Index is Changed
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblImageError.Text = "";
                lblErrorDisplay.Text = "&nbsp;";

                imgProduct.Visible = true;
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                reqValImage.Visible = false;

                Product myProduct = new ProductsLogic().RetrieveProductByID(gvProducts.SelectedValue.ToString());

                txtName.Text = myProduct.Name;
                txtDescription.Text = myProduct.Description;
                ddlStatus.SelectedValue = myProduct.Status.ToString().ToLower();
                ddlCategory.SelectedValue = myProduct.CategoryFK.ToString();
                ddlSupplier.SelectedValue = myProduct.SupplierFK.ToString();
                txtVatRate.Text = myProduct.Vatrate.Vatrate1.ToString();
                txtReorderLevel.Text = myProduct.ReorderLevel.ToString();
                imgProduct.ImageUrl = Page.ResolveClientUrl(myProduct.ImageURL);
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }

        /// <summary>
        /// Occurs when the Products Grid View Row is Deleting
        /// Level: External
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblImageError.Text = "";
                lblErrorDisplay.Text = "&nbsp;";

                Product myProduct = new ProductsLogic().RetrieveProductByID(e.Keys[0].ToString());

                string PreviousURL = myProduct.ImageURL;

                if (new ProductsLogic().DeleteProduct(myProduct.Id) == false)
                {
                    lblErrorDisplay.Text = "Product is Bound to One or More Orders and Cannot be Deleted";
                    //deletion error product has orders
                    //lblServerSideError.Text = "Category has Existing Child Categories and Cannot be Deleted";
                }
                else
                {
                    File.Delete(Server.MapPath(PreviousURL));

                    gvProducts.DataSource = new ProductsLogic().RetrieveAllProducts();
                    gvProducts.DataBind();

                    gvProducts.SelectedIndex = -1;
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    txtName.Text = "";
                    txtDescription.Text = "";
                    txtVatRate.Text = "";
                    txtReorderLevel.Text = "";
                    ddlStatus.SelectedValue = "true";
                    imgProduct.Visible = false;
                    ddlSupplier.SelectedValue = "0";
                    ddlCategory.SelectedValue = "0";
                    reqValImage.Visible = true;
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}