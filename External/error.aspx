<%@ Page Title="the Great Supermarket | Error" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="External.error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">
<div id="PageTitleContainer">
    <br />
    <h1>Error</h1>
    <br />
    <asp:Label ID="lblServerSideErrorTop" CssClass="ValidatorSummary" runat="server" Text="Oops! An unexpected error occured"></asp:Label>
</div>
</asp:Content>
