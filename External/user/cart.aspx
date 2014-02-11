<%@ Page Title="the Great Supermarket | Cart" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="External.user.cart" %>
<asp:Content ID="CartHead" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/cart.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="CartContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <h1>Your Shopping Cart</h1>
    <br />
    <br />
</div>

<table id="ShoppingCart" class="CenterTable" cellpadding="4">
</table>

</asp:Content>
