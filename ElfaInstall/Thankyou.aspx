<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thankyou.aspx.cs" Inherits="ElfaInstall.Thankyou" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thank You</title>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#E63034" background="images/bac_elfa_short.jpg" leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
    <center>
        <p>&nbsp;</p>
        <p>&nbsp;</p>
        <table width="610" height="300" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td height="100" valign="top">
                    <img src="images/elfa_masthead.jpg" width="610" height="100" />
                 </td>
            </tr>
            <tr>
                <td align="center">
                    <p><font size="2" face="Arial, Helvetica, sans-serif"><b>
                    <%--You request has been sent to elfa<sup><font size="2">&reg;</font></sup> Installation--%>
                    You request has been sent to TCS Installation
                    Service.</b></font></p>
                    <p><font size="2" face="Arial, Helvetica, sans-serif"><b>Thank you.</b></font>
                    <p>&nbsp;</p>
                    <asp:Button ID="Button1" runat="server" Text="  OK  "
                        PostBackUrl="http://www.containerstore.com/" />
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
