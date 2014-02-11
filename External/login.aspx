<%@ Page Title="the Great Supermarket | Login" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="External.login" %>
<asp:Content ID="LoginHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <h1>Login</h1>
    <br />
    <br />
</div>

<div id="CenterLogin">

    <div id="LoginErrorMessages">
        <asp:Label ID="lblServerSideError" CssClass="ValidatorSummary" runat="server" Text=""></asp:Label>
        <asp:ValidationSummary ID="valSummary" CssClass="ValidatorSummary" runat="server" DisplayMode="List" />
    </div>

    <table style="margin:auto">
        <tr>
            <td>
                <asp:Label ID="lblUsername" runat="server" CssClass="MiniFontGrey" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUsername" CssClass="TextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="valUsername" runat="server" CssClass="InlineValidator" ErrorMessage="Insert a Valid Username" ControlToValidate="txtUsername" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPassword" runat="server" CssClass="MiniFontGrey" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" CssClass="TextBox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="valPassword" runat="server" CssClass="InlineValidator" ErrorMessage="Insert a Valid Password" ControlToValidate="txtPassword" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
            </td>
            
        </tr>
        <tr>
            <td></td>
            <td>
                <div style="text-align: right">
                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                </div>
            </td>
            <td></td>
        </tr>
    </table>
</div>

</asp:Content>
