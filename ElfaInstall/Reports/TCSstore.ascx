<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TCSstore.ascx.cs" Inherits="ElfaInstall.Reports.TCSstore" %>
<table style="width: 660px; text-align:center" cellpadding="0" cellspacing="0">
    <tr><td colspan="7"><hr style="width:660px" /></td></tr>
    <tr>
        <td style="width: 120px; text-align:left">
            <asp:Label runat="Server" ID="lblStore" Font-Bold="true"></asp:Label>
        </td>
        <td style="width: 90px; text-align:left">QTY</td>
        <td style="width: 90px; text-align:right">
            <asp:Label runat="Server" ID="lblOpenQty"></asp:Label>
        </td>
        <td style="width: 90px; text-align:right">
            <asp:Label runat="Server" ID="lblInstQty"></asp:Label>
        </td>
        <td style="width: 90px; text-align:right">
            <asp:Label runat="Server" ID="lblTotalQty"></asp:Label>
        </td>
        <td style="width: 90px; text-align:right">
            <asp:Label runat="Server" ID="lblLastQty"></asp:Label>
        </td>
        <td style="width:80px; text-align:right">
            <asp:Label runat="Server" ID="lblQtyProc"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="left">Elfa Amt</td>
        <td align="right">
            <asp:Label runat="Server" ID="lblOpenAmt"></asp:Label>
        </td>
        <td align="right">
            <asp:Label runat="Server" ID="lblInstAmt"></asp:Label>
        </td>
        <td align="right">
            <asp:Label runat="Server" ID="lblTotalAmt"></asp:Label>
        </td>
        <td align="right">
            <asp:Label runat="Server" ID="lblLastAmt"></asp:Label>
        </td>
        <td style="text-align:right">
            <asp:Label runat="Server" ID="lblAmtProc"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="left">Invoice Amt</td>
        <td align="right">
            <asp:Label runat="Server" ID="lblOpenInv"></asp:Label>
        </td>
        <td align="right">
            <asp:Label runat="Server" ID="lblInstInv"></asp:Label>
        </td>
        <td align="right">
            <asp:Label runat="Server" ID="lblTotalInv"></asp:Label>
        </td>
        <td align="right">
            <asp:Label runat="Server" ID="lblLastInv"></asp:Label>
        </td>
        <td style="text-align:right">
            <asp:Label runat="Server" ID="lblInvProc"></asp:Label>
        </td>
    </tr>
</table>