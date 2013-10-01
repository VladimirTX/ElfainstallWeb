<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YTDStore.ascx.cs" Inherits="ElfaInstall.Reports.YTDStore" %>
<table style="width: 770px; text-align:center" cellpadding="0" cellspacing="1">
    <tr><td colspan="9"><hr style="width:768px" /></td></tr>
    <tr>
        <td style="width: 110px; text-align:left" rowspan="2">
            <asp:Label runat="Server" ID="lblStore" Font-Bold="true"></asp:Label>
            &nbsp;</td>
        <td style="width: 70px; text-align:left">QTY</td>
        <td style="width: 85px; text-align:right">
            <asp:Label runat="Server" ID="lblOpenQty"></asp:Label>
        </td>
        <td style="width: 85px; text-align:right">
            <asp:Label runat="Server" ID="lblInstQty"></asp:Label>
        </td>
        <td style="width: 85px; text-align:right">
            <asp:Label runat="Server" ID="lblTotalQty"></asp:Label>
        </td>
        <td style="width: 100px; text-align:right">
            <asp:Label runat="Server" ID="lblLastQty"></asp:Label>
        </td>
        <td style="width:65px; text-align:right">
            <asp:Label runat="Server" ID="lblQtyProc"></asp:Label>
        </td>
        <td style="width: 100px; text-align:right">
            <asp:Label runat="Server" ID="lblTotalQty1"></asp:Label>
        </td>
        <td style="width:65px; text-align:right">
            <asp:Label runat="Server" ID="lblQtyProc1"></asp:Label>
        </td>
    </tr>
    <tr>
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
        <td align="right">
            <asp:Label runat="Server" ID="lblTotalAmt1">&nbsp;</asp:Label>
        </td>
        <td style="text-align:right">
            <asp:Label runat="Server" ID="lblAmtProc1">&nbsp;</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="left">Inv. Amt</td>
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
        <td align="right">
            <asp:Label runat="Server" ID="lblTotalInv1">&nbsp;</asp:Label>
        </td>
        <td style="text-align:right">
            <asp:Label runat="Server" ID="lblInvProc1">&nbsp;</asp:Label>
        </td>
    </tr>
</table>