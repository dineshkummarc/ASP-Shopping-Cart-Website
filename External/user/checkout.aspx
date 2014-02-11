<%@ Page Title="the Great Supermarket | Checkout" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="External.user.checkout" %>
<asp:Content ID="CheckoutHead" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/checkout.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="CheckoutContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <h1>Checkout</h1>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" CssClass="ValidatorSummary" Text="&nbsp;" ClientIDMode="Static"></asp:Label>
</div>
    

<table id="ShoppingCartItems">
</table>

<table>
<tr>
<td><asp:Label ID="lblCartTotal" CssClass="GridViewHeader" runat="server" Text="Total =" ClientIDMode="Static"></asp:Label></td>
<td><asp:Label ID="txtCartTotal" CssClass="MiniFontBlue" runat="server" Text="" ClientIDMode="Static"></asp:Label></td>
</tr>
    
</table>

<br />
<br />

<table>
    <tr>
        <td>
            <asp:Label ID="lblCreditCard" CssClass="MiniFontGrey" runat="server" Text="Credit Card Number*" ClientIDMode="Static"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCreditCard" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnCheckout" runat="server" Text="Check Out" ClientIDMode="Static" />
        </td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Label ID="lblCreditCardError" CssClass="ValidatorSummary" runat="server" Text="&nbsp;" ClientIDMode="Static"></asp:Label>
        </td>
        <td></td>
    </tr>
</table>








</asp:Content>
