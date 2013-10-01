<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="ElfaInstall.Reports.OrderInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deleted Order Info</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <br />
        <strong><span style="font-size: 14pt">Deleted Order Info</span></strong><br />
        <br />
        <table width="600" border="1" cellpadding="5" cellspacing="0" style=" font-size:larger">
            <tr>
                <td style="width: 200px">Order #</td>
                <td style="width: 400px"><asp:Label runat="server" ID="lblOrder"></asp:Label></td>
            </tr>
            <tr>
                <td>Order Date</td>
                <td><asp:Label ID="lblDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Customer</td>
                <td><asp:Label ID="lblCustomer" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Address</td>
                <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Phone</td>
                <td><asp:Label ID="lblPhone" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Order Price</td>
                <td><asp:Label ID="lblPrice" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Inst. Price</td>
                <td><asp:Label ID="lblInstallPrice" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Vendor</td>
                <td><asp:Label ID="lblVendor" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Comments</td>
                <td><asp:Label ID="lblComments" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Date Deleted</td>
                <td><asp:Label ID="lblDelDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Deleted By</td>
                <td><asp:Label ID="lblDelBy" runat="server"></asp:Label></td>
            </tr>
        </table>
        <asp:HiddenField ID="hfOrderID" runat="server" />
    </center>
    </div>
    </form>
</body>
</html>
