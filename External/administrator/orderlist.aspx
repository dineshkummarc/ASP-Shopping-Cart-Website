<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="orderlist.aspx.cs" Inherits="External.administrator.orderlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../AJAX/JQuery/orderlist.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<div id="PageTitleContainer">
    <br />
    <h1>Order Control Panel</h1>
    <br />
    <br />
</div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <table class="CenterTable">
        <tr>
            <td><asp:Label ID="lblUsername" runat="server" CssClass="MiniFontGrey" Text="Username*"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtUsername" CssClass="TextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnUserSearch"
                runat="server" Text="Search" onclick="btnUserSearch_Click" 
                    ClientIDMode="Static" />
            </td>
        </tr>
        <tr>
        
            <td><asp:Label ID="lblSupplier" runat="server" CssClass="MiniFontGrey" Text="Supplier*"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtSupplier" CssClass="TextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSupplierSearch"
                runat="server" Text="Search" onclick="btnSupplierSearch_Click" 
                    ClientIDMode="Static"  />
            </td>
        </tr>
    </table>
      
      <br />
      <br />
    
    <asp:GridView ID="gvUserOrders" runat="server" AutoGenerateColumns="False" BorderStyle="None" 
                GridLines="None" UseAccessibleHeader="False" CssClass="CenterTable" 
            CellPadding="4" DataKeyNames="Id" 
            onselectedindexchanged="gvUserOrders_SelectedIndexChanged">
                 <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Order ID" />
            <asp:BoundField DataField="OrderDate" HeaderText="Date Placed" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Status" />
            <asp:BoundField DataField="Username" HeaderText="User" />
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
        </Columns>
         <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle CssClass="GridViewTuple" />
                <SelectedRowStyle CssClass="GridViewSelected" />
    </asp:GridView>

     <asp:GridView ID="gvSupplierOrders" runat="server" AutoGenerateColumns="False" BorderStyle="None" 
                GridLines="None" UseAccessibleHeader="False" CssClass="CenterTable" 
            CellPadding="4" DataKeyNames="Id" 
            onselectedindexchanged="gvSupplierOrders_SelectedIndexChanged">
                 <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Order ID" />
            <asp:BoundField DataField="OrderDate" HeaderText="Date Placed" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Status" />
            <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
        </Columns>
         <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle CssClass="GridViewTuple" />
                <SelectedRowStyle CssClass="GridViewSelected" />
    </asp:GridView>
    
    <br />
    <br />

    <table class="CenterTable">
        <tr>
            <td><asp:Label ID="lblOrder" runat="server" CssClass="MiniFontGrey" Text="Order: "></asp:Label></td>
            <td>
                <asp:TextBox ID="txtUserID" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtSupplierID" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox></td>
            <td> 
                <asp:DropDownList ID="ddlOrderStatus" CssClass="DropDownList" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                    onclick="btnUpdate_Click" />
            </td>
        </tr>
    </table>

    

    <br />

    <table class="CenterTable">
    <tr><td>
    <asp:Button ID="btnEditContents" runat="server" OnClientClick="return false;" 
        Text="Edit Contents" ClientIDMode="Static" /></td>
    <td><asp:Button ID="btnPrintContents" runat="server" OnClientClick="return false;" 
        Text="Print Order" ClientIDMode="Static" /></td>
        </tr>
    </table>

    <br />
</ContentTemplate>
    </asp:UpdatePanel>
    <table id="OrderItems" class="CenterTable">
    </table>

</asp:Content>
