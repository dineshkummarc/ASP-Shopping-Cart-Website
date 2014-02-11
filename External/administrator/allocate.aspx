<%@ Page Title="the Great Supermarket | Allocate" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="allocate.aspx.cs" Inherits="External.administrator.user" %>
<asp:Content ID="UserHead" ContentPlaceHolderID="head" runat="server">

    <script src="../AJAX/JQuery/allocate.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="UserContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <h1>Allocate Control Panel</h1>
    <br />
    <br />
</div>

<div style="padding-left: 100px">
<table cellpadding="4">

    <tr>
        <td>
            <asp:Label ID="lblEmail" CssClass="MiniFontGrey" runat="server" Text="Email*"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmail" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search" ClientIDMode="Static" CausesValidation="False" UseSubmitBehavior="True" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" ClientIDMode="Static" CausesValidation="False" UseSubmitBehavior="True" />
        </td>
        <td>
            <asp:Label ID="lblError" CssClass="ValidatorSummary" runat="server" Text="" ClientIDMode="Static"></asp:Label>
        </td>
     </tr>

     <tr>
         <td>
            <asp:Label ID="lblCurrentUserType" CssClass="MiniFontGrey" runat="server" Text="Current User Type" ClientIDMode="Static"></asp:Label>
         </td>
         <td>
            <asp:TextBox ID="txtCurrentUserType" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
         </td>
         <td>
             
         </td>
         <td></td>
     </tr>


     <tr>
        <td>
            <asp:Label ID="lblUserTypes" CssClass="MiniFontGrey" runat="server" Text="New User Type" ClientIDMode="Static"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlUserTypes" CssClass="DropDownList" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
        </td>
        <td>
        <asp:Button ID="btnUpdate" runat="server" Text="Allocate" ClientIDMode="Static" />
        </td>
        <td></td>
    </tr>

    <tr>
        <td>
            <asp:Label ID="lblDeAllocate" CssClass="MiniFontGrey" runat="server" Text="Current Roles" ClientIDMode="Static"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCurrentRoles" CssClass="DropDownList" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
        </td>
        <td>
        <asp:Button ID="btnDeAllocateRole" runat="server" Text="De Allocate" ClientIDMode="Static" />
         
        </td>
        <td><asp:Label ID="lblDeAllocateError" CssClass="ValidatorSummary" runat="server" Text="" ClientIDMode="Static"></asp:Label></td>
    </tr>

    <tr>
        <td>
            <asp:Label ID="lblAllocate" CssClass="MiniFontGrey" runat="server" Text="Available Roles" ClientIDMode="Static"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAvailableRoles" CssClass="DropDownList" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
        </td>
        <td>
        <asp:Button ID="btnAllocateRole" runat="server" Text="Allocate" ClientIDMode="Static" />
         
        </td>
        <td></td>
    </tr>


</table>
</div>

</asp:Content>
