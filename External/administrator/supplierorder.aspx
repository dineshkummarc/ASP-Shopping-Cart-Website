<%@ Page Title="the Great Supermarket | Supplier Order" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="supplierorder.aspx.cs" Inherits="External.administrator.supplierorder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/supplierorder.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <h1>Place Supplier Order</h1>
    <br />
    <br />
</div>

<div style="float: left; padding-left: 75px">

<table>

<tr>
    <td>
        <asp:Label ID="lblSupplier" CssClass="MiniFontGrey" runat="server" Text="Supplier*"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlSupplier" CssClass="DropDownList" runat="server" ClientIDMode="Static">
        </asp:DropDownList>
    </td>
</tr>

<tr>
    <td>
        <asp:Label ID="lblProduct" CssClass="MiniFontGrey" runat="server" Text="Product*" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlProduct" CssClass="DropDownList" runat="server" ClientIDMode="Static">
        </asp:DropDownList>
    </td>
</tr>

<tr>
    <td>
        <asp:Label ID="lblPrice" CssClass="MiniFontGrey" runat="server" Text="Price" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:Label ID="txtPrice" CssClass="MiniFontBlueLeft" runat="server" Text="€ 0" ClientIDMode="Static"></asp:Label>
    </td>
</tr>

<tr>
    <td>
        <asp:Label ID="lblQuantity" CssClass="MiniFontGrey" runat="server" Text="Quantity*" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtQuantity" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
    </td>
</tr>

<tr>
    <td>
       
    </td>
    <td>
        <div class="ButtonRight">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ClientIDMode="Static" />
            <asp:Button ID="btnSubmit" runat="server" Text="Place Order" ClientIDMode="Static" />
            <asp:Button ID="btnNew" runat="server" Text="Add to List" ClientIDMode="Static" />
        </div>
    </td>
</tr>

<tr>
    <td>
    </td>
    <td>
        <asp:Label ID="txtError" CssClass="ValidatorSummary" runat="server" Text="" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
    </td>
    
</tr>

</table>

</div>


<div style="float: left; padding-left: 30px">
    <asp:Table ID="tblOrderItems" runat="server" ClientIDMode="Static">
    </asp:Table>

    </div>

</asp:Content>
