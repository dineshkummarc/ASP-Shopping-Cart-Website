<%@ Page Title="the Great Supermarket | 404" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pagenotfound.aspx.cs" Inherits="External.pagenotfound" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">
<div id="PageTitleContainer">
    <br />
    <h1>Error</h1>
    <br />
    <asp:Label ID="lblServerSideErrorTop" CssClass="ValidatorSummary" runat="server" Text="Oops! The page you requested does not exist!"></asp:Label>
</div>
</asp:Content>
