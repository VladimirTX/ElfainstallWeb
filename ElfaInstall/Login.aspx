<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ElfaInstall.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>elfa Login </title>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:center">
    <center>
        <img src="images/elfa_masthead.jpg" alt="" /><br /><br />
        <table style="width: 640px">
            <tr>
                <td style="width: 370px; text-align:left">
                    <table width="350">
                        <tr>
                            <td colspan="2" style="height: 45px">
                                <strong><span style="font-size: 16pt; color: #c43b40; font-family: Arial; text-indent:5pt">Please Log In Below:</span></strong>
                                <br />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px" class="tdmain">
                                User Name:
                            </td>
                            <td style="width: 200px" class="tdmain">
                                <asp:TextBox ID="txtUser" runat="server" Width="170"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdmain">
                                Password:
                            </td>
                            <td class="tdmain">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="170"></asp:TextBox>
                                <br />
                                <span style="font-size: 10pt">(Password is case-sensitive) </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblMessage" runat="Server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label><br />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="btnLogin" runat="server" CssClass="btnsubmit" Height="30" 
                                    Width="100" Text=" LOG IN " OnClick="btnLogin_Click"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        
                    </table>
                </td>
                <td style="width: 270px">
                    <img alt='""' src="images/closet_pic.jpg" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <img alt='""' src="images/bottom.gif" /></td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="valUser" runat="server" ControlToValidate="txtUser"
            Display="None" ErrorMessage="User Name required"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtPassword"
            Display="None" ErrorMessage="Password required"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </center>
    </div>
    </form>
</body>
</html>
