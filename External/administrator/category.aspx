<%@ Page Title="the Great Supermarket | Category" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="External.administrator.category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="CategoryHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="CategoryContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">

<asp:ScriptManager ID="CategoryScriptManager" runat="server">
</asp:ScriptManager>

    <asp:UpdatePanel ID="GridPanel" runat="server">
    <ContentTemplate>
<div id="PageTitleContainer">
    <br />
    <h1>Category Control Panel</h1>
    <br />
    <br />
    <asp:Label ID="lblServerSideErrorTop" CssClass="ValidatorSummary" runat="server" Text="&nbsp;"></asp:Label>
</div>

    <br />


    
        <div style="display: block">
        <div style="float: left; display: block;">
        <asp:GridView ID="gvCategories" runat="server" BorderStyle="None" 
        GridLines="None" CellPadding="4" UseAccessibleHeader="False" 
                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                onrowdeleting="gvCategories_RowDeleting" 
                onselectedindexchanged="gvCategories_SelectedIndexChanged" 
                DataKeyNames="Id">
            <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
            <Columns>
                <asp:BoundField DataField="Category1" HeaderText="Parent Category" />
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
            <HeaderStyle CssClass="GridViewHeader" />
            <RowStyle CssClass="GridViewTuple" />
            <SelectedRowStyle CssClass="GridViewSelected" />
        </asp:GridView>
        </div>

        <div style="float:right; display: block;">
        <asp:GridView ID="gvSubCategories" runat="server" BorderStyle="None" 
        GridLines="None" CellPadding="4" UseAccessibleHeader="False" 
                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                onrowdeleting="gvSubCategories_RowDeleting" 
                onselectedindexchanged="gvSubCategories_SelectedIndexChanged" 
                DataKeyNames="Id">
            <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
            <Columns>
                <asp:BoundField DataField="Category1" HeaderText="Child Category" />
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
            <HeaderStyle CssClass="GridViewHeader" />
            <RowStyle CssClass="GridViewTuple" />
            <SelectedRowStyle CssClass="GridViewSelected" />
        </asp:GridView>
        </div>
        </div>

    </ContentTemplate>

    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnClear" />
    </Triggers>
</asp:UpdatePanel>

<asp:UpdatePanel ID="FieldPanel" runat="server">
    <ContentTemplate>

    <div style="clear:both; display: block">

    <br />
    <br />
    <div style="float: left;">
        <table>
        

            <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" CssClass="MiniFontGrey" Text="Category*"></asp:Label>
                </td>
                <td>
                
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="TextBox">
                        </asp:TextBox>
                
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="reqValName" CssClass="InlineValidator" runat="server" ErrorMessage="Plase Insert a Valid Category" ControlToValidate="txtCategory">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUpload" runat="server" CssClass="MiniFontGrey" Text="Image*"></asp:Label>
                </td>
                <td>
             
                    <asp:FileUpload ID="fuImage" runat="server" Width="250px" Height="25px" />
                </td>
                <td>
            
                    <asp:RequiredFieldValidator ID="ReqValImage" CssClass="InlineValidator" runat="server" ErrorMessage="Please Browse for a Valid Image" ControlToValidate="fuImage" Text="*"></asp:RequiredFieldValidator>
                
                
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblParent" runat="server" CssClass="MiniFontGrey" Text="Parent Category*"></asp:Label>
                </td>
                <td>
            
                    <asp:DropDownList ID="ddlParent" CssClass="DropDownList" runat="server">
                    </asp:DropDownList>
                
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="ButtonRight">
                        <asp:Button ID="btnClear" runat="server" Text="Clear" 
                            onclick="btnClear_Click" CausesValidation="False" />
                    
                            <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                            <asp:Button ID="btnUpdate" Visible="false" runat="server" Text="Update" 
                            onclick="btnUpdate_Click" />
                      </div>   
                  
               
                </td>
                <td></td>
            </tr>
        </table>

         </div>   
       

        <div style="float:left; padding-top: 3px; padding-left: 10px;">
    
            <asp:Image ID="imgSelectedCategory" Visible="false" runat="server" 
                Width="100px" Height="100px" />
            
        </div>

        <div class="BottomValidationSummary">
            <asp:Label ID="lblServerSideErrorBottom" CssClass="ValidatorSummary" runat="server" Text=""></asp:Label>
            <asp:ValidationSummary ID="valSummary" CssClass="ValidatorSummary" runat="server" DisplayMode="List" />
        </div>

                
        </div>

    </ContentTemplate> 
                 
    <Triggers>    
        <asp:AsyncPostBackTrigger ControlID="btnClear" />
        <asp:PostBackTrigger ControlID="btnAdd" />
        <asp:PostBackTrigger ControlID="btnUpdate" />
        <asp:AsyncPostBackTrigger ControlID="gvCategories" EventName="RowDeleting" />
        <asp:AsyncPostBackTrigger ControlID="gvCategories" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="gvSubCategories" EventName="RowDeleting" />
        <asp:AsyncPostBackTrigger ControlID="gvSubCategories" EventName="SelectedIndexChanged" />
    </Triggers>
    
</asp:UpdatePanel>

 
</asp:Content>
