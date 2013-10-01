<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TCSbyStore.aspx.cs" Inherits="ElfaInstall.Reports.TCSbyStore" %>
<%@ Reference Control="~/Reports/TCSstore.ascx"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TCS Comparative Analysis by Store</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <table style="width: 660px" cellpadding="0" cellspacing="0" align="center">
            <tr><td colspan="7" align="center">
                <span style="font-size: 12pt"><strong>TCS Comparative Analysis by Store  
                    <asp:Label ID="lblFiscal" runat="server" Text="(from "></asp:Label></strong></span></td></tr>
            <tr style="font-weight:bold; height:30px">
                <td style="width: 140px">
                    Store</td>
                <td style="width: 90px"></td>
                <td style="width: 90px">
                    Open Orders</td>
                <td style="width: 90px; text-align:center">
                    Installed</td>
                <td style="width: 90px; text-align:center">
                    Sold</td>
                <td style="width: 90px; text-align:center">
                    Prev. Year Installed</td>
                <td style="width:60px; text-align:center">%</td>
            </tr>
        </table>
        <asp:Table runat="Server" ID="tblMain" Width="660" CellPadding="0" CellSpacing="0" HorizontalAlign="Center"></asp:Table>
        <table style="width: 660px; text-align:center" cellpadding="0" cellspacing="0" align="center">
            <tr><td colspan="7"><hr style="width:660px" /></td></tr>
            <tr>
                <td style="width: 120px; text-align:left"><strong>Total:</strong></td>
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
    </center>
    </div>
    </form>
</body>
</html>
