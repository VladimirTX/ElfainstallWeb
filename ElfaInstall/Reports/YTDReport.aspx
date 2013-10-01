<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YTDReport.aspx.cs" Inherits="ElfaInstall.Reports.YTDReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>YTD by Store</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <table style="width: 770px" cellpadding="0" cellspacing="1">
            <tr><td colspan="9" align="center">
                <span style="font-size: 16pt"><strong>YTD report by Store<br />&nbsp;
                </strong></span></td></tr>
            <tr style="font-weight: bold; height: 30px">
                <td style="width: 110px">
                </td>
                <td style="width: 70px">
                </td>
                <td colspan="3" style="text-align: center; border-bottom: thin double;">
                    Current Year Orders</td>
                <td colspan="4" style="text-align: center; border-left: thin solid; border-bottom: thin double;">
                    Previous Year Orders</td>
            </tr>
            <tr style="font-weight:bold; height:30px">
                <td style="width: 110px; text-align: center;">
                    Store</td>
                <td style="width: 70px"></td>
                <td style="width: 85px; text-align: right;">
                    Open</td>
                <td style="width: 85px; text-align:right">
                    Installed</td>
                <td style="width: 85px; text-align:right">
                    Received</td>
                <td style="width: 100; text-align:right;">
                    Installed</td>
                <td style="width:65px; text-align:right">
                    %&nbsp;&nbsp;</td>
                <td style="width: 100px; text-align:right">Received</td>
                <td style="width:65px; text-align:right">%&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:Table runat="Server" ID="tblMain" Width="770" CellPadding="0" CellSpacing="0" HorizontalAlign="Center"></asp:Table>
        <table style="width: 770px; text-align:center;" cellpadding="0" cellspacing="1">
            <tr><td colspan="9"><hr style="width:768px" /></td></tr>
            <tr>
                <td style="width: 110px; text-align:left"><strong>Total:</strong></td>
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
                    <asp:Label runat="Server" ID="lblLastQty1"></asp:Label>
                </td>
                <td style="width:65px; text-align:right">
                    <asp:Label runat="Server" ID="lblQtyProc1"></asp:Label>
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
                <td style="text-align:right">
                    <asp:Label runat="Server" ID="lblLastAmt1"></asp:Label>
                </td>
                <td style="text-align:right">
                    <asp:Label runat="Server" ID="lblAmtProc1"></asp:Label>
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
                <td style="text-align:right">
                    <asp:Label runat="Server" ID="lblLastInv1"></asp:Label>
                </td>
                <td style="text-align:right">
                    <asp:Label runat="Server" ID="lblInvProc1"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
