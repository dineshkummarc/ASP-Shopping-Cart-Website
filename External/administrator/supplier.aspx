<%@ Page Title="the Great Supermarket | Supplier" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="supplier.aspx.cs" Inherits="External.user.administrator.supplier" %>
<asp:Content ID="SupplierHead" ContentPlaceHolderID="head" runat="server">

    <script src="../AJAX/JQuery/supplier.js" type="text/javascript"></script>  
</asp:Content>
<asp:Content ID="SupplierContent" ContentPlaceHolderID="MasterPlaceholder" runat="server">
   
    <asp:ScriptManager ID="SupplierScript" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="GridViewUpdatePanel" runat="server">
        <ContentTemplate>

    <div id="PageTitleContainer">
        <br />
        <h1>Supplier Control Panel</h1>
        <br />
        <asp:Label ID="lblServerSideError" CssClass="ValidatorSummary" runat="server" Text="&nbsp;"></asp:Label>
    </div>

    <br />

            <asp:GridView ID="gvSuppliers" runat="server" BorderStyle="None" 
                GridLines="None" UseAccessibleHeader="False" 
                AutoGenerateColumns="False" onrowdeleting="gvSuppliers_RowDeleting" 
                onselectedindexchanged="gvSuppliers_SelectedIndexChanged" 
                ShowHeaderWhenEmpty="True" CssClass="CenterTable" CellPadding="4" DataKeyNames="Id">
                    <AlternatingRowStyle CssClass="GridViewTupleAlternating" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="StreetAddress" HeaderText="Street Address" />
                        <asp:BoundField DataField="Postcode" HeaderText="Postcode" />
                        <asp:BoundField DataField="Town" HeaderText="Town" />
                        <asp:BoundField DataField="Country" HeaderText="Country" />
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" 
                            ControlStyle-Height="25" CausesValidation="False" >
                            <ControlStyle Height="25px"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                             ControlStyle-Height="25" CausesValidation="False" >
                             <ControlStyle Height="25px"></ControlStyle>
                        </asp:CommandField>
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

    <asp:Button ID="btnRefresh" runat="server" Text="" ClientIDMode="Static" 
    CausesValidation="False" onclick="btnRefresh_Click" CssClass="ButtonInvisible" />

    <br />
    <br />
    <br />

    <asp:UpdatePanel ID="FieldUpdatePanel" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
        <ContentTemplate> 

        

        <!-- outer div -->
            <div class="SupplierFieldsWrapper">

                <asp:HiddenField ID="hdnID" runat="server" ClientIDMode="Static" />
            
                <div class="SupplierFields">

                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblSupplier" CssClass="MiniFontGrey" runat="server" Text="Supplier*"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSupplier" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valReqSupplier" CssClass="InlineValidator" 
                                    runat="server" ErrorMessage="Insert a Valid Supplier" 
                                    ControlToValidate="txtSupplier" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEmail" CssClass="MiniFontGrey" runat="server" Text="Email*"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtEmail" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valReqEmail" CssClass="InlineValidator" runat="server" ErrorMessage="Insert a Valid Email" ControlToValidate="txtEmail" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="valEmailValid" runat="server" CssClass="InlineValidator" ErrorMessage="Insert a Valid Email" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="*" ControlToValidate="txtEmail" SetFocusOnError="False"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAddressLine1" CssClass="MiniFontGrey" runat="server" Text="Street Address*"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtAddressLine1" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valReqAddressLine1" CssClass="InlineValidator" 
                                    runat="server" ErrorMessage="Fill in Street Address: Line 1" 
                                    ControlToValidate="txtAddressLine1" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAddressLine2" CssClass="MiniFontGrey" runat="server" Text="*"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtAddressLine2" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valReqAddressLine2" CssClass="InlineValidator" 
                                    runat="server" ErrorMessage="Fill in Street Address: Line 2" 
                                    ControlToValidate="txtAddressLine2" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTown" CssClass="MiniFontGrey" runat="server" Text="Town*"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTown" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valReqTown" CssClass="InlineValidator" 
                                    runat="server" ErrorMessage="Insert a Valid Town" ControlToValidate="txtTown" 
                                    Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPostcode" CssClass="MiniFontGrey" runat="server" Text="Postcode*"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPostcode" CssClass="TextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valReqPostcode" CssClass="InlineValidator" 
                                    runat="server" ErrorMessage="Insert a Valid Postcode" 
                                    ControlToValidate="txtPostcode" Text="*" SetFocusOnError="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCountry" CssClass="MiniFontGrey" runat="server" Text="Country*"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" CssClass="DropDownList" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CompareValidator ID="ddlCountryCompare" runat="server" 
                                    ErrorMessage="Select a Valid Country" ValueToCompare="0" 
                                    ControlToValidate="ddlCountry" Operator="GreaterThan" 
                                    CssClass="InlineValidator" Text="*" SetFocusOnError="False" ClientIDMode="Inherit"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <div class="ButtonRight">
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CausesValidation="False" UseSubmitBehavior="False" ClientIDMode="Static" />
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" ClientIDMode="Static" CausesValidation="False" />
                                    <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update" ClientIDMode="Static" CausesValidation="False" />
                                </div>
                            </td>
                            <td></td>
                        </tr>
                    </table>

                </div>
                <div class="BottomValidationSummary">
                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" CssClass="ValidatorSummary" />
                </div>

            </div>
        
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="gvSuppliers" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="gvSuppliers" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
