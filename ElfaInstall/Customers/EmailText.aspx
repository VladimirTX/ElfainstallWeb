<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailText.aspx.cs" Inherits="ElfaInstall.Customers.EmailText" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <br /><br />
        <table width="600px">
            <tr>
                <td align="left">
                    Dear – Customers name – <br /><br />
                    Congratulations on your Elfa purchase from The Container Store!!!!  <br /><br />
                    We are excited about performing the installation services for you.  You can expect the same exceptional customer service 
                    from us as you receive from The Container Store. We will have you dancing in your new elfa space shortly.<br /><br />
                    Please click on the link below to start the process and confirm your installation date and time.<br /><br />
                    Your elfa Installation Services team is ready to serve.<br /><br />&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    Order ID&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox1" runat="server">62625</asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="Generate Link" onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:LinkButton ID="lbOrder" runat="server" Visible="false">Click here</asp:LinkButton>
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
