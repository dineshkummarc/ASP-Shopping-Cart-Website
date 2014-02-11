<%@ Page Title="the Great Supermarket | Stock History" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="stockhistory.aspx.cs" Inherits="External.administrator.stockhistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div id="PageTitleContainer">
    <br />
    <h1>Stock History</h1>
    <br />
    <br /> 
    <asp:DropDownList ID="ddlProduct" CssClass="DropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlProduct_SelectedIndexChanged">
    </asp:DropDownList>
</div>
   


    <br />

    <asp:GridView ID="gvStockHistory" runat="server" BorderStyle="None" 
                GridLines="None" UseAccessibleHeader="False" 
    ShowHeaderWhenEmpty="True" CssClass="CenterTable" CellPadding="4" 
    AutoGenerateColumns="False">
    <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
        <Columns>
            <asp:BoundField DataField="Date" HeaderText="Date and Time Effected" />
            <asp:BoundField DataField="Product" HeaderText="Product" />
            <asp:BoundField DataField="QuantityIncreased" HeaderText="Increased By" />
            <asp:BoundField DataField="QuantityDecreases" HeaderText="Decreased By" />
            <asp:BoundField DataField="AffectedBy" HeaderText="Effected By" />
        </Columns>
    <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle CssClass="GridViewTuple" />
                <SelectedRowStyle CssClass="GridViewSelected" />

    </asp:GridView>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
