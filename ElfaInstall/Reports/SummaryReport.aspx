<%@ Page Title="elfa® Reports" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="SummaryReport.aspx.cs" Inherits="ElfaInstall.Reports.SummaryReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />
    Open / Installed / Closed Orders</center></div>
    <br />
    <table width="740">
        <tr>
            <td style="width:130px;">
                <asp:RequiredFieldValidator ID="valOpStart" runat="server" ControlToValidate="rdpOpStart"
                    Display="None" ErrorMessage="Select FROM Date" ValidationGroup="Open"></asp:RequiredFieldValidator></td>
            <td align="center" class="tdmainR" style="width:470; border-right: Gray solid; border-top: Gray solid; border-left: Gray solid; border-bottom: Gray solid;">
                Select:&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbOpen" runat="server" Text="Open&nbsp;&nbsp;&nbsp;&nbsp;" Width="120px" GroupName="Select" Checked="True" />
                <asp:RadioButton ID="rbInstalled" runat="server" Text="Installed&nbsp;&nbsp;" Width="120px" GroupName="Select" />
                <asp:RadioButton ID="rbClosed" runat="server" Text="Closed&nbsp;" GroupName="Select"/></td>
            <td style="width:130px;">
                <asp:RequiredFieldValidator ID="valOpEnd" runat="server" ControlToValidate="rdpOpEnd"
                    Display="None" ErrorMessage="Select TO Date" ValidationGroup="Open"></asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <table width="740">
        <tr>
            <td style="width: 140px;" class="tdmainR">Date Range</td>
            <td style="width: 260px;" align="left">
                <telerik:RadDatePicker ID="rdpOpStart" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td style="width: 340px;" align="left">
                <telerik:RadDatePicker ID="rdpOpEnd" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr><td colspan="5"><hr style="color:#666666; height:2; margin-left:5px" /></td></tr>
    </table>
    <table style=" width:740;">
        <tr>
            <td style="width:180px;" class="tdmainR">By Store</td>
            <td style="width:220px;" align="right">
                <asp:DropDownList ID="ddlStores" runat="server" ValidationGroup="Store">
                </asp:DropDownList>
            </td>
            <td style="width:200px;" align="center">
                <asp:Button ID="btnStore" runat="server" CssClass="btnmenu" Text=" Continue " OnClick="BtnStoreClick" PostBackUrl="~/Reports/ShowOpen.aspx" ValidationGroup="Open"  />
            </td>
            <td style="width:140px" align="center">
<%--                <asp:Button ID="btnExStore" runat="server" CssClass="btnmenu" Text="Excel Rpt" OnClick="BtnStoreClick" PostBackUrl="~/Reports/XLSReport.aspx" ValidationGroup="Open"  />--%>
                
            </td>
        </tr>
        <tr><td colspan="4"><hr style="color:#666666; height:2; margin-left:5px" /></td></tr>
        <tr>
            <td class="tdmainR">By Market</td>
            <td align="right">
                <asp:DropDownList ID="ddlMarkets" runat="server" ValidationGroup="Market">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnMarket" runat="server" CssClass="btnmenu" Text=" Continue " OnClick="BtnMarketClick" PostBackUrl="~/Reports/ShowOpen.aspx" ValidationGroup="Open" />
            </td>
            <td align="center">
                <%--<asp:Button ID="btnExMarket" runat="server" CssClass="btnmenu" Text="Excel Rpt" OnClick="BtnMarketClick" PostBackUrl="~/Reports/XLSReport.aspx" ValidationGroup="Open" />--%>

            </td>
        </tr>
        <tr><td colspan="4"><hr style="color:#666666; height:2; margin-left:5px" /></td></tr>
        <tr>
            <td class="tdmainR">By Installer</td>
            <td align="right">
                <asp:DropDownList ID="ddlVendors" runat="server" ValidationGroup="Vendor">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnVendor" runat="server" CssClass="btnmenu" Text=" Continue " OnClick="BtnVendorClick" PostBackUrl="~/Reports/ShowOpen.aspx" ValidationGroup="Open" />
            </td>
            <td align="center">
                <%--<asp:Button ID="btnExVendor" runat="server" CssClass="btnmenu" Text="Excel Rpt" OnClick="BtnVendorClick" PostBackUrl="~/Reports/XLSReport.aspx" ValidationGroup="Open" />--%>

            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <hr style="color:#666666; height:2; margin-left:5px" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="valSumm2" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Open" />
    <asp:HiddenField ID="hfTarget" runat="server" />
    <asp:HiddenField ID="hfTargetName" runat="Server" />
    </center>
</asp:Panel>
</asp:Content>
