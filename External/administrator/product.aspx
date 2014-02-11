<%@ Page Title="the Great Supermarket | Product" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="External.administrator.product" %>
<asp:Content ID="ProductHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ProductContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div id="PageTitleContainer">
    <br />
    <h1>Product Control Panel</h1>
    <br />
    <br />
    <% if (LoadProducts == false)
       { %>
           <asp:Label ID="lblServerSideError" CssClass="ValidatorSummary" runat="server" Text="&nbsp;"></asp:Label>
    <% } %>
     <asp:Label ID="lblErrorDisplay" CssClass="ValidatorSummary" runat="server" Text="&nbsp;"></asp:Label>
    <br />
    <br />
</div>
    
    <%if (LoadProducts == true)
      { %>

    <asp:GridView ID="gvProducts" runat="server" BorderStyle="None" 
                GridLines="None" UseAccessibleHeader="False" 
                AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
                CssClass="CenterTable" CellPadding="4" DataKeyNames="Id" 
        onrowdeleting="gvProducts_RowDeleting" 
        onselectedindexchanged="gvProducts_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Product" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="StockQty" HeaderText="Stock Quantity" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="Re-Order Level" />
            <asp:BoundField DataField="Status" HeaderText="Active" />
            <asp:BoundField DataField="VatRate" HeaderText="Vat Rate" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
        </Columns>
        <HeaderStyle CssClass="GridViewHeader" />
        <RowStyle CssClass="GridViewTuple" />
        <SelectedRowStyle CssClass="GridViewSelected" />
    </asp:GridView>
    <br />
    <br />
    <% } %>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnClear" />
    </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>

    
    <%if (LoadProducts == true)
      { %>
<div>
<table style="float: left; padding-left: 50px">
    <tr> 
        <td>
            <asp:Label ID="lblName" CssClass="MiniFontGrey" runat="server" Text="Product Name*"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtName" CssClass="TextBox" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="reqValName" CssClass="InlineValidator" 
                ControlToValidate="txtName" runat="server" 
                ErrorMessage="Insert a Valid Name">*</asp:RequiredFieldValidator>
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDescription" CssClass="MiniFontGrey" runat="server" Text="Product Description*"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDescription" CssClass="MultilineTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqValDesc" CssClass="InlineValidator" 
                ControlToValidate="txtDescription" runat="server" 
                ErrorMessage="Insert a Valid Description">*</asp:RequiredFieldValidator>
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblImage" CssClass="MiniFontGrey" runat="server" Text="Image*"></asp:Label>
        </td>
        <td>
            <asp:FileUpload ID="fuImage" runat="server" Width="250px" Height="25px" />
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqValImage" CssClass="InlineValidator" ControlToValidate="fuImage" runat="server" ErrorMessage="Select a Valid Image">*</asp:RequiredFieldValidator>
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblVatRate" CssClass="MiniFontGrey" runat="server" Text="Vatrate*"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtVatRate" CssClass="TextBox" runat="server"></asp:TextBox>
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqValVatRate" CssClass="InlineValidator" ControlToValidate="txtVatRate" runat="server" ErrorMessage="Insert a Valid Vat Rate">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rngVatRate" runat="server" ErrorMessage="Insert a Valid Vat Rate : [ 0.1 - 100 ]" Type="Double" CssClass="InlineValidator" MaximumValue="100" MinimumValue="0.1" ControlToValidate="txtVatRate">*</asp:RangeValidator>
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblRedorderLevel" CssClass="MiniFontGrey" runat="server" Text="Reorder Level*"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtReorderLevel" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        </td>
        <td>
        <asp:RequiredFieldValidator ID="reqValReorder" CssClass="InlineValidator" ControlToValidate="txtReorderLevel" runat="server" ErrorMessage="Insert a Valid Reorder Level">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rngValidatorReorder" CssClass="InlineValidator" runat="server" ErrorMessage="Insert a Valid Reorder Level : [ 0 - 200 ]" Type="Integer" MinimumValue="0" MaximumValue="200" ControlToValidate="txtReorderLevel" Text="*"></asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblStatus" CssClass="MiniFontGrey" runat="server" Text="Status*"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlStatus" CssClass="DropDownList" runat="server">
            </asp:DropDownList>
        </td>
        <td>
        
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCategory" CssClass="MiniFontGrey" runat="server" Text="Category*"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" CssClass="DropDownList" runat="server">
            </asp:DropDownList>
        </td>
        <td>
        
                 <asp:CompareValidator ID="ddlCategoryCompare" runat="server" ErrorMessage="Select a Valid Category" ValueToCompare="0" ControlToValidate="ddlCategory" Operator="GreaterThan" CssClass="InlineValidator" Text="*"></asp:CompareValidator>
             
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSupplier" CssClass="MiniFontGrey" runat="server" Text="Supplier*"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSupplier" CssClass="DropDownList" runat="server">
            </asp:DropDownList>
        </td>
        <td>
        
                 <asp:CompareValidator ID="ddlSupplierCompare" runat="server" ErrorMessage="Select a Valid Supplier" ValueToCompare="0" ControlToValidate="ddlSupplier" Operator="GreaterThan" CssClass="InlineValidator" Text="*"></asp:CompareValidator>
             
        </td>    
    </tr>
    <tr>
        <td>
            
        </td>
        <td>
            <div class="ButtonRight">
                <asp:Button ID="btnClear" runat="server" Text="Clear" 
                    onclick="btnClear_Click" CausesValidation="False" />
                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" 
                    onclick="btnUpdate_Click" />
            </div>
        </td>
        <td>

        </td>    
    </tr>

</table>
</div>

<div style="float: left; padding-top: 3px; padding-left: 10px;">
    <asp:Image ID="imgProduct" CssClass="ProductImage" runat="server" 
        Visible="False" />

</div>

<div style="float: left; padding-left: 10px;">
    <asp:ValidationSummary ID="valSummary" CssClass="ValidatorSummary" runat="server" DisplayMode="List" />
    <asp:Label ID="lblImageError" CssClass="ValidatorSummary" runat="server" Text=""></asp:Label>
</div>
<% } %>
</ContentTemplate>


  <Triggers>    
        <asp:AsyncPostBackTrigger ControlID="btnClear" />
        <asp:PostBackTrigger ControlID="btnAdd" />
        <asp:PostBackTrigger ControlID="btnUpdate" />
        <asp:AsyncPostBackTrigger ControlID="gvProducts" EventName="RowDeleting" />
        <asp:AsyncPostBackTrigger ControlID="gvProducts" EventName="SelectedIndexChanged" />
    </Triggers>
 

</asp:UpdatePanel>

</asp:Content>
