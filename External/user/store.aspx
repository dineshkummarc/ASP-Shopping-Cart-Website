<%@ Page Title="the Great Supermarket | Store" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="store.aspx.cs" Inherits="External.store" %>
<asp:Content ID="StoreHead" ContentPlaceHolderID="head" runat="server">

    <script src="../AJAX/JQuery/store.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="StoreContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">

    <div class="StoreSpacer"></div>

    <table id="tblSelectors" class="CenterTable">
        <tr>
            <td>
                <asp:Label ID="lblParent" CssClass="MiniFontGrey" runat="server" Text="Parent Category:" ClientIDMode="Static"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlParentCategories" CssClass="DropDownList" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblChild" CssClass="MiniFontGrey" runat="server" Text="Child Category:" ClientIDMode="Static"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlChildCategories" CssClass="DropDownList" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    
    <div class="StoreSpacer"></div>

    <table id="tblProducts" class="CenterTable" cellpadding="4">
    </table>

</asp:Content>
