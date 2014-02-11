<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewproduct.aspx.cs" Inherits="External.viewproduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/viewproduct.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <br />
    <asp:Label ID="txtPageTitle" CssClass="PageTitle" runat="server" Text=""></asp:Label>
    <br />
    <br />
</div>
    <asp:HiddenField ID="hdnProductID" runat="server" ClientIDMode="Static" />

            <div style="float: left; padding-left: 60px;"> 
                <div class="ProductContent">

                    <asp:Image ID="imgProduct" CssClass="ProductImage" runat="server" />

                    <br /> 
                    <asp:Label ID="lblProductName" CssClass="MiniFontGrey" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblPrice" CssClass="MiniFontBlue" runat="server" Text="price"></asp:Label>
                    <br />
                    <div class="MiniFontGrey"><div style="padding-top: 5px; float: left;">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity&nbsp;&nbsp;"></asp:Label></div>
                        <asp:TextBox ID="txtQuantity" CssClass="CatalogTextBox" runat="server" 
                            ClientIDMode="Static"></asp:TextBox></div>&nbsp;<br />
                    </div>
                     <div class="ProductButtons">
                        
                            <% if (_StockAvailable == true)
                               { %>
                        
                               <input type="image" class="ProductButton" alt=""  ProductID=""   ClickAction="Add" src="/images/Add.jpg" onclick="return false;" />
                                <div class="ProductButtonSpacer"></div>
                                <input type="image" class="ProductButton" alt="" ProductID=""  ClickAction="Remove" src="/images/Remove.jpg" onclick="return false;"/>
                                <% } %>
                  </div>
              </div>
              <div style="float: left">
                  <asp:Label ID="lblDescription" CssClass="MiniFontGrey" runat="server" Text=""></asp:Label>
              </div>

</asp:Content>
