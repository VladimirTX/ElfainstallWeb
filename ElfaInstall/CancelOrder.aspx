<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="CancelOrder.aspx.cs" Inherits="ElfaInstall.CancelOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style="width: 900px;" cellpadding="0" cellspacing="2">
        <tr>
            <td colspan="2" style=" height:100;" align="center">
                <img src="images/Top_logo1.GIF" alt="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="600" cellpadding="10" cellspacing="10">
        <tr>
            <td  colspan="2"  align="Center" style="font-weight:bold; font-size:large">
                <asp:Label runat="Server" ID="Label1">Are you sure you want to cancel Order #&nbsp;</asp:Label>
                <asp:Label runat="Server" ID="lblOrderNumb"></asp:Label>&nbsp;?
                <br />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                If you put cancellation reason below and click "Yes", request will be send to Administrator
            </td>
        </tr>
        <tr>
            <td colspan="2" align="Center">
                <center>
                Put reason here:
                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="338px" MaxLength="250"></asp:TextBox>
                </center>
            </td>
        </tr>
        <tr>
            <td align="Center"><asp:Button ID="btnYes" runat="server" CssClass="btnmenu" Text="    YES    " OnClick="BtnYesClick" /></td>
            <td align="Center"><asp:Button ID="btnNo" runat="server" CssClass="btnmenu" Text="    NO    " CausesValidation="false" OnClick="BtnNoClick" /></td>
        </tr>
    </table>
    &nbsp;<asp:RequiredFieldValidator ID="valReason" runat="server" ControlToValidate="txtReason"
        Display="None" ErrorMessage="Put Reason of Canceling the Order"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </center>
</asp:Panel>
</asp:Content>
