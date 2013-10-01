<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckList.aspx.cs" Inherits="ElfaInstall.CheckList" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check List</title>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Close() {
            GetRadWindow().close(); // Close the window
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
        }
    </script> 
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            color: Black;
            font-family: Arial, Helvetica, Sans-Serif;
            text-align: left;
            font-size: 12px;
            text-indent: 2pt;
            width: 220px;
        }
        .style2
        {
            width: 220px;
        }
        .style3
        {
            font-weight: bold;
            color: #666666;
            font-family: Arial, Helvetica, Sans-Serif;
            text-align: left;
            font-size: 12px;
            text-indent: 2pt;
            width: 220px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size:10pt;">
    <telerik:RadScriptManager runat="server" />
    <center>
        <div style="font-size:18px; font-weight:bold; color:#666666;">
            <center>At Home Order Info</center></div>
        <table width="820px" frame="box" style="border: medium solid #000000">
            <tr>
                <td style="width:110px" class="td12">Customer:</td>
                <td class="style1">
                    <asp:Label ID="lblCustomer" runat="server">Customer</asp:Label>
                </td>
                <td style="width:110px" class="td12">Product Amount:</td>
                <td style="width:80px" class="td12">$
                    <asp:Label ID="lblProductAmt" runat="server" CssClass="lblsize12a">756.30</asp:Label>
                </td>
                <td style="width:60px" class="td12">Pickup 1:</td>
                <td style="width:80px" align="left">
                    <asp:DropDownList ID="ddlPickUp" runat="server" CssClass="lblsize12a">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>ATX</asp:ListItem>
                        <asp:ListItem>FTW</asp:ListItem>
                        <asp:ListItem>FVW</asp:ListItem>
                        <asp:ListItem>GAL</asp:ListItem>
                        <asp:ListItem>NPK</asp:ListItem>
                        <asp:ListItem>SLK</asp:ListItem>
                        <asp:ListItem>STN</asp:ListItem>
                        <asp:ListItem>DC</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center" style="width:150px; border-left-style: solid; border-left-width: thin; border-left-color: #000000;" rowspan="2">
                    <telerik:RadButton runat="server" ID="btnAdd" Text=" Add New Space "
                        Width="140px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        onclick="BtnAddClick">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td class="td12">Organizer:</td>
                <td align="left" class="style2">
                    <asp:DropDownList ID="ddlSpecialist" runat="server" CssClass="lblsize12a"></asp:DropDownList>
                </td>
                <td class="td12">Install Amount:</td>
                <td class="td12">$
                    <asp:Label ID="lblInstallAmt" runat="server" CssClass="lblsize12a">224.80</asp:Label>
                </td>
                <td class="td12">Pickup 2:</td>
                <td align="left">
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="lblsize12a">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>ATX</asp:ListItem>
                        <asp:ListItem>FTW</asp:ListItem>
                        <asp:ListItem>FVW</asp:ListItem>
                        <asp:ListItem>GAL</asp:ListItem>
                        <asp:ListItem>NPK</asp:ListItem>
                        <asp:ListItem>SLK</asp:ListItem>
                        <asp:ListItem>STN</asp:ListItem>
                        <asp:ListItem>DC</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td12">Date Start:</td>
                <td align="left" class="style2">
                    <telerik:RadDateTimePicker ID="rdpInstallDateStart" runat="server" 
                        TimeView-EndTime="19:00:00" TimeView-StartTime="08:00:00" 
                        imeView-Interval="02:00:00" 
                        onselecteddatechanged="RdpInstallDateStartSelectedDateChanged">
                    </telerik:RadDateTimePicker>
                </td>
                <td class="td12" colspan="4">Installer #1:&nbsp;&nbsp;&nbsp;
<%--                </td>--%>
<%--                <td class="td12" colspan="3">--%>
                    <asp:DropDownList ID="ddlVendor1" runat="server" CssClass="lblsize12a"></asp:DropDownList>
                    Hrs.&nbsp;<asp:TextBox ID="txtProc1" runat="server" Width="20px" MaxLength="3" CssClass="lblsize12a"></asp:TextBox>
                </td>
                <td align="center" style="border-left-style: solid; border-left-width: thin; border-left-color: #000000;">
                    <telerik:RadButton runat="server" ID="btnClose" Text=" Save and Close "
                        Width="140px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        onclick="BtnCloseClick">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td class="td12">Date Finish:</td>
                <td align="left" class="style2">
                    <telerik:RadDateTimePicker ID="rdpInstallDateEnd" runat="server" 
                        TimeView-EndTime="19:00:00" TimeView-StartTime="08:00:00" 
                        imeView-Interval="02:00:00" 
                        onselecteddatechanged="RdpInstallDateEndSelectedDateChanged">
                    </telerik:RadDateTimePicker>
                </td>
                <td class="td12" colspan="4">Installer #2:&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlVendor2" runat="server" CssClass="lblsize12a"></asp:DropDownList>
                    Hrs.&nbsp;<asp:TextBox ID="txtProc2" runat="server" Width="20px" MaxLength="3" CssClass="lblsize12a"></asp:TextBox>
                </td>
                <td align="center" style="border-left-style: solid; border-left-width: thin; border-left-color: #000000;">
                    <telerik:RadButton runat="server" ID="btnPrint" Text=" Print Order Info "
                        Width="140px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        AutoPostBack="False" ButtonType="LinkButton" CausesValidation="False" 
                        Target="_blank" UseSubmitBehavior="False">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Staging:
                    <asp:DropDownList ID="ddlStaging" runat="server" AutoPostBack="True" CssClass="lblsize12a"
                        onselectedindexchanged="DdlStagingSelectedIndexChanged">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    Date o f Stage:
                    <telerik:RadDatePicker ID="rdpStagingDate" runat="server" Width="100px"></telerik:RadDatePicker>
                </td>
                <td class="td12">
                    Styling:
                    <asp:DropDownList ID="ddlStyling" runat="server" AutoPostBack="True" CssClass="lblsize12a" 
                        onselectedindexchanged="DdlStylingSelectedIndexChanged">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td12" colspan="3">
                    Date of Style:
                    <telerik:RadDatePicker ID="rdpStylingDate" runat="server" Width="100px"></telerik:RadDatePicker>
                </td>
                <td align="center" style="border-left-style: solid; border-left-width: thin; border-left-color: #000000;" rowspan="2" >
                    <asp:Label ID="lblLabel1" runat="server" CssClass="lblsize10">
                        Additional Services Quote:<br /></asp:Label>
                    $ <telerik:RadNumericTextBox ID="rntServices" runat="server" Width="80px" Height="20px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox><br /><br />
                    <asp:Label ID="lblLabel2" runat="server" CssClass="lblsize10">
                        Non-elfa product cost:<br /></asp:Label>
                    $ <telerik:RadNumericTextBox ID="rntNonElfa" runat="server" Width="80px" Height="20px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="td12">Special<br /> &nbsp;Requests:</td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtSpecial" runat="server" TextMode="MultiLine" Rows="2" 
                        MaxLength="500" Width="530px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td12">Comments<br /> &nbsp;to Installer:</td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="2" 
                        MaxLength="1500" Width="530px"></asp:TextBox>
                </td>
                <td class="td12" align="center" style="border-left-style: solid; border-left-width: thin; border-left-color: #000000;">
                    <asp:CheckBox ID="cbCompleted" runat="server" Text="Checklist completed" />
                </td>
            </tr>
        </table>
        <asp:Table ID="tblSpaces" runat="server" Width="820px"></asp:Table>
        <br />&nbsp;
        <asp:HiddenField ID="hfUpdated" runat="server" />
        <asp:HiddenField ID="hfVendor" runat="server" />
        <asp:HiddenField ID="hfCompleted" runat="server" />
        <asp:HiddenField ID="hfOrderNumber" runat="server" />
        <asp:HiddenField ID="hfAddService" runat="server" />
    </center>
    </div>
    </form>
</body>
</html>
