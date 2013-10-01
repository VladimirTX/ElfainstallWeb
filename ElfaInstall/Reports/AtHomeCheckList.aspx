<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AtHomeCheckList.aspx.cs" Inherits="ElfaInstall.Reports.AtHomeCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check List</title>
    <link href="../Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <asp:Label ID="lblHeader" runat="server" Font-Size="X-Large" Font-Bold="true">At Home Order # 0123456</asp:Label>
        <table width="640px" frame="box" style="border: medium solid #000000" cellspacing="3">
            <tr>
                <td style="width:110px" class="td12">Customer:</td>
                <td style="width:210px" class="td12a">
                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                </td>
                <td style="width:120px" class="td12">Product Amount:</td>
                <td style="width:80px" class="td12a">
                    <asp:Label ID="lblProductAmt" runat="server" CssClass="lblsize12a"></asp:Label>
                </td>
                <td style="width:60px" class="td12">Pickup 1:</td>
                <td style="width:60px" class="td12a">
                    <asp:Label ID="lblPickUp" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  class="td12">Organizer:</td>
                <td class="td12a">
                    <asp:Label ID="lblSpecialist" runat="server"></asp:Label>
                </td>
                <td class="td12">Install Amount:</td>
                <td class="td12a">
                    <asp:Label ID="lblInstallAmt" runat="server"  CssClass="lblsize12a"></asp:Label>
                </td>
                <td class="td12">Pickup 2:</td>
                <td class="td12a">
                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Date Start:</td>
                <td class="td12a">
                    <asp:Label ID="lblInstallDateStart" runat="server"></asp:Label>
                </td>
                <td class="td12">Installer #1:</td>
                <td class="td12a" colspan="3">
                    <asp:Label ID="lblInstaller1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Date Finish:</td>
                <td class="td12a">
                    <asp:Label ID="lblInstallDateEnd" runat="server"></asp:Label>
                </td>
                <td class="td12">Installer #2:</td>
                <td class="td12a" colspan="3">
                    <asp:Label ID="lblInstaller2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Staging:&nbsp; <asp:Label ID="lblStaging" runat="server" ForeColor="Black">N</asp:Label>
                </td>
                <td class="td12a">
                    Date of Stage: 
                    <asp:Label ID="lblStagingDate" runat="server"></asp:Label>
                </td>
                <td class="td12">
                    Styling:&nbsp; <asp:Label ID="lblStyling" runat="server" ForeColor="Black">N</asp:Label>
                </td>
                <td class="td12a" colspan="3">
                    Date of Style: 
                    <asp:Label ID="lblStylingDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Special<br /> &nbsp;Requests:</td>
                <td class="td12a" colspan="5">
<%--                    <asp:TextBox ID="txtSpecial" runat="server" TextMode="MultiLine" Rows="2" 
                        MaxLength="500" Width="500px"></asp:TextBox>--%>
                    <asp:Label ID="lblSpecial" runat="server" Width="500px" BorderColor="#999999" 
                        BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Comments<br /> &nbsp;to Installer:</td>
                <td class="td12a" colspan="5">
<%--                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="2" 
                        ReadOnly="True" MaxLength="1500" Width="500px"></asp:TextBox>--%>
                    <asp:Label ID="lblComments" runat="server" Width="500px" BorderColor="#999999" 
                        BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                </td>
            </tr>
        </table>
         <asp:Table ID="tblSpaces" runat="server" Width="640px" CellPadding="0" CellSpacing="0"></asp:Table>
        <br />&nbsp;
    </center>
    </div>
    </form>
</body>
</html>
