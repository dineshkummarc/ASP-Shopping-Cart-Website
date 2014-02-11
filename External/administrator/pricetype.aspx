<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pricetype.aspx.cs" Inherits="External.administrator.pricetype" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PriceTypeHead" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/pricetype.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="PriceTypeContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div id="PageTitleContainer">
    <br />
    <h1>Price Type Control Panel</h1>
    <br />
    <br />
</div>

<div style="padding-left: 165px; width: 390px; float: left;">
<table style="float: right">
<tr>
    <td>
        <asp:Label ID="lblProduct" runat="server" CssClass="MiniFontGrey" Text="Product"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlProduct" CssClass="DropDownList" runat="server" ClientIDMode="Static">
        </asp:DropDownList>
    </td>
    <td>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblUserType" runat="server" CssClass="MiniFontGrey" Text="User Type"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlUserType" CssClass="DropDownList" runat="server" ClientIDMode="Static">
        </asp:DropDownList>
    </td>
    
    <td>
    </td>
</tr>

<tr>
    <td>
        <asp:Label ID="lblPrice" runat="server" CssClass="MiniFontGrey" Text="Price*" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtPrice" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
    </td>
    
    <td>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDiscount" runat="server" CssClass="MiniFontGrey" Text="Add Discount" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:CheckBox ID="chkbxHasDiscount" runat="server" ClientIDMode="Static" />
    </td>
    
    <td>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDateStart" runat="server" CssClass="MiniFontGrey" 
            Text="Discount Starts*" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtDateStart" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        <asp:CalendarExtender ID="calExtDateStart"  runat="server" TargetControlID="txtDateStart" DefaultView="Years" PopupPosition="BottomRight">
        </asp:CalendarExtender>
    </td>
    
    <td>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDateEnd" runat="server" CssClass="MiniFontGrey" 
            Text="Discount Ends*" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtDateEnd" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        <asp:CalendarExtender ID="calExtDateEnd"  runat="server" TargetControlID="txtDateEnd" DefaultView="Years" PopupPosition="BottomRight">
        </asp:CalendarExtender>
    </td>
    
    <td>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblPercentage" runat="server" CssClass="MiniFontGrey" 
            Text="Discount Percentage*" ClientIDMode="Static"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtPercentage" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
    </td>
    
    <td>
    </td>
</tr>

<tr>
    <td></td>
    <td>
        <div class="ButtonRight">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" ClientIDMode="Static" />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ClientIDMode="Static" />
        </div>
    </td>
    
    <td>
        
    </td>
</tr>


</table>
</div>
<div style="float:left; padding-left: 20px">
    <asp:Label ID="txtError" CssClass="ValidatorSummary" runat="server" Text="" ClientIDMode="Static"></asp:Label>
    <asp:Label ID="txtDateError" CssClass="ValidatorSummary" runat="server" Text="" ClientIDMode="Static"></asp:Label>
</div>


</asp:Content>
