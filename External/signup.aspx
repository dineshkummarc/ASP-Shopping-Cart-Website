<%@ Page Title="the Great Supermarket | Sign Up" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="External.signup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceholder" runat="server">
    
<div id="PageTitleContainer">
    <br />
    <h1>Sign Up</h1>
    <br />
    <br />
</div>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div>

    <div id="SignupErrorMessages">
        <asp:Label ID="lblServerSideError" CssClass="ValidatorSummary" runat="server" Text=""></asp:Label>
        <asp:ValidationSummary ID="valSummary" CssClass="ValidatorSummary" runat="server" DisplayMode="List" ShowMessageBox="False" />
    </div>

    <table class="CenterTable">
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" CssClass="MiniFontGrey" Text="Name*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" CssClass="TextBox" runat="server"></asp:TextBox>
               
            </td>
            <td> 
                <asp:RequiredFieldValidator ID="valName" runat="server" 
                    ErrorMessage="Insert a Valid Name" ControlToValidate="txtName" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
           </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSurname" runat="server" CssClass="MiniFontGrey" Text="Surname*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSurname" CssClass="TextBox" runat="server"></asp:TextBox>
              
            </td>
             <td>  <asp:RequiredFieldValidator ID="valSurname" runat="server" 
                    ErrorMessage="Insert a Valid Surname" ControlToValidate="txtSurname" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
             </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblCalendar" runat="server" CssClass="MiniFontGrey" Text="Date of Birth*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateOfBirth" CssClass="TextBox" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="calExtender" runat="server" DefaultView="Years" PopupPosition="BottomRight" TargetControlID="txtDateOfBirth">
                </asp:CalendarExtender>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="valDateOfBirth" runat="server" 
                    ErrorMessage="Select a Valid Date of Birth" ControlToValidate="txtDateOfBirth" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" CssClass="MiniFontGrey" Text="Email*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" CssClass="TextBox" runat="server"></asp:TextBox>
               
            </td>
            <td> 
                <asp:RequiredFieldValidator ID="valEmail" runat="server" 
                    ErrorMessage="Insert a Valid Email" ControlToValidate="txtEmail" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator
                        ID="valEmailValid" runat="server" CssClass="InlineValidator" ErrorMessage="Insert a Valid Email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="*" ControlToValidate="txtEmail" SetFocusOnError="True"></asp:RegularExpressionValidator>
           </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAddress" runat="server" CssClass="MiniFontGrey" Text="Street Address *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine1" CssClass="TextBox" runat="server"></asp:TextBox>
                
            </td>
             <td><asp:RequiredFieldValidator ID="valAddressLine1" runat="server" 
                    ErrorMessage="Fill in Street Address: Line 1" ControlToValidate="txtAddressLine1" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAddressLine2" runat="server" CssClass="MiniFontGrey" Text="*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine2" CssClass="TextBox" runat="server"></asp:TextBox>
                
            </td>
             <td><asp:RequiredFieldValidator ID="valAddressLine2" runat="server" 
                    ErrorMessage="Fill in Street Address: Line 2" ControlToValidate="txtAddressLine2" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTown" runat="server" CssClass="MiniFontGrey" Text="Town*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTown" CssClass="TextBox" runat="server"></asp:TextBox>
                
            </td>
             <td><asp:RequiredFieldValidator ID="valTown" runat="server" 
                    ErrorMessage="Insert a Valid Town" ControlToValidate="txtTown" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblPostCode" runat="server" CssClass="MiniFontGrey" Text="Postcode*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPostCode" CssClass="TextBox" runat="server"></asp:TextBox>
                
            </td>
             <td><asp:RequiredFieldValidator ID="valPostCode" runat="server" 
                    ErrorMessage="Insert a Valid Postcode" ControlToValidate="txtPostCode" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCountry" runat="server" CssClass="MiniFontGrey" Text="Country*"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCountry" CssClass="DropDownList" runat="server">
                </asp:DropDownList>
                
            </td>
             <td>
             
                 <asp:CompareValidator ID="ddlCountryCompare" runat="server" ErrorMessage="Select a Valid Country" ValueToCompare="0" ControlToValidate="ddlCountry" Operator="GreaterThan" CssClass="InlineValidator" Text="*" SetFocusOnError="True"></asp:CompareValidator>
             
             
             </td>
        </tr>
       
        <tr>
            <td>
                <br />
            </td>
            <td>
                <br />
            </td>
             <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUsername" runat="server" CssClass="MiniFontGrey" Text="Username*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUsername" CssClass="TextBox" runat="server"></asp:TextBox>
                
            </td>
             <td><asp:RequiredFieldValidator ID="valUsername" runat="server" 
                    ErrorMessage="Insert a Valid Username" ControlToValidate="txtUsername" 
                    Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPassword" runat="server" CssClass="MiniFontGrey" Text="Password*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" CssClass="TextBox" runat="server" TextMode="Password"></asp:TextBox>
                
            </td>
             <td>
                <asp:RequiredFieldValidator ID="valPassword" runat="server" 
                    ErrorMessage="Insert a Valid Password" Display="Dynamic" 
                    ControlToValidate="txtPassword" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ControlToValidate="txtPassword" CssClass="InlineValidator" ID="valPassLength" runat="server" ErrorMessage="Password Length Must be 6 +" ValidationExpression=".{5}." Text="*"></asp:RegularExpressionValidator>
             </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfirmPassword" runat="server" CssClass="MiniFontGrey" Text="Confirm Password*"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" CssClass="TextBox" runat="server" TextMode="Password"></asp:TextBox>
                
            </td>
             <td>
             <asp:RequiredFieldValidator ID="valReqValPassword" runat="server" 
                    ErrorMessage="Insert a Valid Comparison Password" Display="Dynamic" 
                    ControlToValidate="txtConfirmPassword" CssClass="InlineValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
             
             <asp:CompareValidator ID="valComparePassword" runat="server" 
                    ErrorMessage="Passwords Must Match" ControlToCompare="txtConfirmPassword" 
                    ControlToValidate="txtPassword" Display="Dynamic" CssClass="InlineValidator" SetFocusOnError="False">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td></td>
            <td>
                    <div style="text-align:right">
                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                    </div>

            </td>
             <td></td>
        </tr>
    </table>

    </div>
</asp:Content>
