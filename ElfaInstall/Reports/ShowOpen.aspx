<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowOpen.aspx.cs" Inherits="ElfaInstall.Reports.ShowOpen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orders List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <center>
        <asp:table id="tblTop" runat="server" CellSpacing="0" Width="800px" CellPadding="0">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" Font-Bold="True" Font-Size="10pt">
                    <asp:Label runat="Server" ID="lblHeader"> Orders from </asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblDate">09/16/2006</asp:Label>&nbsp;&nbsp;for&nbsp;
                    <asp:Label runat="Server" ID="lblTarget"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="trEmpty" Visible="False">
                <asp:TableCell HorizontalAlign="Center" Font-Bold="true" Font-Size="18" ForeColor="Red">
                    <asp:Label runat="Server" ID="lblEmpty"><br /><br />Your Selection return no result.</asp:Label>
                    <br /><br />
                    <asp:Button runat="Server" ID="btnReturn" Text="Return" PostBackUrl="~/Reports/SummaryReport.aspx" Font-Bold="true" Font-Size="12" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:table>
        <asp:Table ID="tblMain" runat="server" CellPadding="0" CellSpacing="0" Width="800px" Font-Size="8pt">
        </asp:Table>
        <asp:Table ID="tblTotal" runat="Server" CellPadding="5" CellSpacing="5" Width="760" Font-Size="10pt">
            <asp:TableRow>
                <asp:TableCell Width="620" HorizontalAlign="Left" Font-Bold="True">Total for Report:&nbsp;&nbsp;
                    <asp:Label runat="Server" ID="lblTotOrders" Font-Bold="True">N</asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="60" HorizontalAlign="Right">
                    <asp:Label runat="Server"  ID="lblTotPurchase" Font-Bold="True">P</asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="60" HorizontalAlign="Right">
                    <asp:Label runat="Server" ID="lblTotInstall" Font-Bold="True">I</asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20">&nbsp;</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </center>
    </div>
    </form>
</body>
</html>
