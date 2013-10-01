<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ElfaInstall.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:740;" cellpadding="5" cellspacing="5">
        <tr>
            <td align="center" colspan="2" class="tdheadersmall">
                Reset Password for 
                <asp:Label runat="Server" ID="lblUser"></asp:Label><br />&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width:50%;" class="td14R">User Login:
            </td>
            <td style="width:50%" align="left" >
                <asp:Label runat="Server" ID="lblLogin" Font-Bold="True">Login</asp:Label></td>
        </tr>
        <tr>
            <td style="width:50%;" class="td14R">New Password:
            </td>
            <td style="width:50%" align="left" >
                <asp:TextBox ID="txtPass1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="td14R" style="width: 302px">Confirm Password:
            </td>
            <td align="left">
                <asp:TextBox ID="txtPass2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <br /><asp:Button ID="Button1" runat="server" Text=" Save " CssClass="btnmenu" OnClick="Button1_Click" ValidationGroup="Pass" /></td>
        </tr>
    </table>
        &nbsp;
        <asp:RequiredFieldValidator ID="valPass0" runat="server" ControlToValidate="txtPass1"
            Display="None" ErrorMessage="Password can not be empty!" ValidationGroup="Pass"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valPass1" runat="server" ControlToValidate="txtPass2"
            Display="None" ErrorMessage="Password can not be empty" ValidationGroup="Pass"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="valPass2" runat="server" ControlToCompare="txtPass1" ControlToValidate="txtPass2"
            Display="None" ErrorMessage="Passwords are not the same!" ValidationGroup="Pass"></asp:CompareValidator>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Pass" />
    </center>
</asp:Panel>
</asp:Content>
