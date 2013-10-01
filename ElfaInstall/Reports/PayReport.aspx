<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayReport.aspx.cs" Inherits="ElfaInstall.Reports.PayReport" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pay Report</title>
</head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager1" Runat="server"></telerik:RadScriptManager>
    <div>
    <center>
        <br />
<div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />Weekly Log Pay Report</center></div>
        <br /><br />
        <table width="740">
            <tr>
                <td style="width: 140px;" class="tdmainR">Date Range</td>
                <td style="width: 300px;" class="tdmainR">From:&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="rdpStart" Runat="server" AutoPostBack="True" 
                        onselecteddatechanged="RdpStartSelectedDateChanged">
                    </telerik:RadDatePicker>
                </td>
                <td style="width: 300px;" class="tdmainR">To:&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="rdpEnd" Runat="server" AutoPostBack="True" 
                        onselecteddatechanged="RdpEndSelectedDateChanged">
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style=" width:740;">
            <tr>
                <td style="width:740px;"  align="center">
                    <asp:Button ID="btnCompleted" runat="server" CssClass="btnmenu" Text=" Continue " ValidationGroup="Completed" OnClick="BtnCompletedClick" />
                </td>
            </tr>
            <tr><td style="width:740px;"  align="center">
                <asp:Label runat="server" ID="lblResult" Visible="false"></asp:Label>
            </td></tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
