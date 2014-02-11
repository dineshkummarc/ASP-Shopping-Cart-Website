<%@ Page Title="the Great Supermarket | User Type" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="usertype.aspx.cs" Inherits="External.administrator.usertype" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../AJAX/JQuery/usertype.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">
    <asp:ScriptManager ID="UserTypeScriptManager" runat="server">
    </asp:ScriptManager>

<asp:UpdatePanel ID="GridViewPanel" runat="server">
    <ContentTemplate>
<div id="PageTitleContainer">
    <br />
    <h1>User Type Control Panel</h1>
    <br />
    <br />
    <asp:Label ID="lblServerSideError" CssClass="ValidatorSummary" runat="server" Text="&nbsp;"></asp:Label>
</div>

        <asp:GridView ID="gvUserTypes" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" CssClass="CenterTable" GridLines="None" ShowHeaderWhenEmpty="True" 
            UseAccessibleHeader="False" DataKeyNames="Id" 
            onselectedindexchanged="gvUserTypes_SelectedIndexChanged" 
            onrowdeleting="gvUserTypes_RowDeleting">
            <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="Type" HeaderText="User Type" />
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
            <HeaderStyle CssClass="GridViewHeader" />
            <RowStyle CssClass="GridViewTuple" />
            <SelectedRowStyle CssClass="GridViewSelected" />
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
    </Triggers>
</asp:UpdatePanel>
    <br />
    <br />
    <br />

    <asp:Button ID="btnRefresh" runat="server" Text="" ClientIDMode="Static" 
    CausesValidation="False" onclick="btnRefresh_Click" CssClass="ButtonInvisible" />

<asp:UpdatePanel ID="FieldPanel" runat="server" ChildrenAsTriggers="False" 
        UpdateMode="Conditional">
    <ContentTemplate>
    <div style="float: left;padding-left: 230px;">
        <asp:HiddenField ID="hdnID" runat="server" ClientIDMode="Static" />

        <table>
            <tr>
                <td>
                    <asp:Label ID="lblUserType" CssClass="MiniFontGrey" runat="server" Text="User Type*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserType" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="reqValUserType" CssClass="InlineValidator" runat="server" ErrorMessage="Insert a Valid User Type" Text="*" ControlToValidate="txtUserType"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="ButtonRight">
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" 
                            onclick="btnClear_Click" />
                        <asp:Button ID="btnAdd" OnClientClick="" CausesValidation="false" 
                            runat="server" Text="Add" ClientIDMode="Static" />
                        <asp:Button ID="btnUpdate" Visible="false" runat="server" Text="Update" 
                            ClientIDMode="Static" />
                    </div>
                </td>
                <td></td>
            </tr>
        </table>
        </div>

        
        <div class="BottomValidationSummary">
                    <asp:Label ID="lblServerSideErrorBottom" CssClass="ValidatorSummary" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" CssClass="ValidatorSummary" />
                </div>
     
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnClear" />
        <asp:AsyncPostBackTrigger ControlID="gvUserTypes" EventName="RowDeleting" />
        <asp:AsyncPostBackTrigger ControlID="gvUserTypes" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>