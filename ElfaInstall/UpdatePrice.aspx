<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="UpdatePrice.aspx.cs" Inherits="ElfaInstall.UpdatePrice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style="width:600;" cellpadding="10" cellspacing="10">
        <tr>
            <td colspan="2" align="center" style="font-size:22px; font-weight:bold; color:#556b2f;">
                Price Change for Order # 
                <asp:Label ID="lblOrderID" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td14" style="width:300;" align="right">Current Purchase Price</td>
            <td style="width:300;" class="tdmain">
                <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="td14" style="width:300;" align="right">New Purchase Price</td>
            <td style="width:300;" class="tdmain">
                $
                <telerik:RadNumericTextBox ID="rntPrice" runat="server" Width="80px" Font-Bold="true" Font-Size="Larger">
                    <NumberFormat DecimalDigits="2" DecimalSeparator="." />
                </telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="td14" style="width:300;" align="right">Current Installation Price %</td>
            <td style="width:300;" class="tdmain">
                $
                <telerik:RadNumericTextBox ID="rntProc" runat="server" Width="60px" Font-Bold="true" Font-Size="Larger" MaxValue="100" MinValue="0" MaxLength="6">
                    <NumberFormat DecimalDigits="2" DecimalSeparator="." />
                </telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" Text="   Save   " CssClass="btnsubmit" OnClick="BtnSaveClick" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="hfOrderID" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
