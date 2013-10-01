<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Report_Created.aspx.cs" Inherits="ElfaInstall.Reports.ReportCreated" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />
        Created Orders</center></div>
    <br /><br />
    <table width="740">
        <tr>
            <td style="width: 140px;" class="tdmainR">Date Range</td>
            <td style="width: 200px;" class="tdmainR">From:&nbsp;&nbsp;
                <telerik:RadDatePicker ID="rdpDateStart" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td style="width: 200px;" class="tdmainR">To:&nbsp;&nbsp;
                <telerik:RadDatePicker ID="rdpDateEnd" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
    </table>
    <br />
    <table style=" width:740;">
        <tr>
            <td style="width:280px;" class="tdmainR">Completed Orders By</td>
            <td style="width:160px;" align="right">
                <asp:DropDownList ID="ddlSelection" runat="server" ValidationGroup="Completed">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Store</asp:ListItem>
                    <asp:ListItem>Market</asp:ListItem>
                    <asp:ListItem>Vendor</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:300px;" align="center">
                <asp:Button ID="btnCompleted" runat="server" CssClass="btnmenu" Text=" Continue " ValidationGroup="Completed" PostBackUrl="~/Reports/ShowCreated.aspx" />
            </td>
        </tr>
    </table>
    <hr style="color:#666666; height:2; margin-left:5px" />
    <asp:RequiredFieldValidator ID="valDateStart" runat="server" ControlToValidate="rdpDateStart"
                Display="None" ErrorMessage="Select FROM Date" ValidationGroup="Completed"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valDateEnd" runat="server" ControlToValidate="rdpDateEnd"
                Display="None" ErrorMessage="Select TO Date" ValidationGroup="Completed"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
                ValidationGroup="Completed" />
            <asp:RequiredFieldValidator ID="valSelect" runat="server" ControlToValidate="ddlSelection"
                Display="None" ErrorMessage="Select BY criteria" ValidationGroup="Completed"></asp:RequiredFieldValidator>
    </center>
</asp:Panel>
</asp:Content>
