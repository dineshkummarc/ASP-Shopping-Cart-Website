<%@ Page Title="the Great Supermarket | Top Ten" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="topten.aspx.cs" Inherits="External.administrator.topten" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/topten.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">
<div id="PageTitleContainer">
    <br />
    <h1>Top Ten - Products</h1>
    <br />
    <br />
</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="padding-left: 240px">
    <table>
        
        <tr>
        <td><asp:Label ID="lblStartDate" CssClass="MiniFontGrey" runat="server" Text="Start Date*" ClientIDMode="Static"></asp:Label></td>
        <td><asp:TextBox ID="txtStartDate" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        <asp:CalendarExtender
            ID="CalendarExtender1" TargetControlID="txtStartDate" runat="server">
        </asp:CalendarExtender></td>
        </tr>
        <tr>
        <td><asp:Label ID="lblEndDate" CssClass="MiniFontGrey" runat="server" Text="End Date*" ClientIDMode="Static"></asp:Label></td>
        <td> <asp:TextBox ID="txtEndDate" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        <asp:CalendarExtender
            ID="CalendarExtender2" TargetControlID="txtEndDate" runat="server">
        </asp:CalendarExtender></td>
        </tr>
        <tr>
        <td></td>
        <td><div class="ButtonRight"><asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return false;" ClientIDMode="Static" /></div></td>
        </tr>
       
    </table>
    </div>

<div style="width:inherit; text-align:center">
    <br />
    <br />
    <asp:Label ID="Label1" CssClass="PageTitle" runat="server" Text="Top Ten - Clients"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Button ID="btnPrintClients" runat="server" OnClientClick="return false;" Text="Print" ClientIDMode="Static" />
</div>

</asp:Content>

