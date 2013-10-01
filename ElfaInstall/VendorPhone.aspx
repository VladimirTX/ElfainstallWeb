<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VendorPhone.aspx.cs" Inherits="ElfaInstall.VendorPhone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" font-size:x-large">
        <center>
            <br />
            Vendor: <asp:Label ID="lblVendorName" runat="server"></asp:Label>
            <br /><br />
            Phone: <asp:Label ID="lblVendorPhone" runat="server"></asp:Label>
            <br />
            Email: <asp:Label ID="lblVendorEmail" runat="server"></asp:Label>
        </center>
    </div>
    </form>
</body>
</html>
