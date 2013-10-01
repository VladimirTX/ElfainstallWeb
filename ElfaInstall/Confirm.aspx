<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="ElfaInstall.Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table width="800px" cellpadding="10px" cellspacing="10px">
            <tr>
                <td colspan="2" style=" font-weight:bold; font-size:18; text-align:center; color:Red">
                    <asp:Label runat="Server" ID="Label1">Are you sure you want to delete<br /><br />Order #&nbsp;&nbsp;</asp:Label>
                    <asp:Label runat="Server" ID="lblOrderNumb"></asp:Label>&nbsp;?
                    <br />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <center>
                        Select the Reason:
                        <asp:DropDownList ID="ddlReason" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DdlReasonSelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>Cancelled by TCS</asp:ListItem>
                        <asp:ListItem>Self-installed</asp:ListItem>
                        <asp:ListItem>Contractor installed</asp:ListItem>
                        <asp:ListItem>Price</asp:ListItem>
                        <asp:ListItem>No Communication</asp:ListItem>
                        <asp:ListItem>Duplicate Order</asp:ListItem>
                        <asp:ListItem>Our Fault</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                    </center>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:TextBox ID="txtReason" runat="server" Width="600" MaxLength="250" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYes" runat="server" CssClass="btnmenu" OnClick="BtnYesClick" Text="    YES    " />
                </td>
                <td align="center">
                    <asp:Button ID="btnNo" runat="server" CssClass="btnmenu" OnClick="BtnNoClick" Text="    NO    " CausesValidation="false" />
                </td>
            </tr>
        </table>
    <asp:RequiredFieldValidator ID="valReason" runat="server" ControlToValidate="ddlReason"
        Display="None" ErrorMessage="Select Reason of Deleting the Order"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </center>
    </asp:Panel>
</asp:Content>
