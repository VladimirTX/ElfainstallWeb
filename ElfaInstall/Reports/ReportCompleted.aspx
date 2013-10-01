<%@ Page Title="Completed Orders" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="ReportCompleted.aspx.cs" Inherits="ElfaInstall.Reports.ReportCompleted" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <br />
        <div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />Completed Orders</center></div>
        <br /><br />
        <table width="740">
            <tr>
                <td style="width: 140px;" class="tdmainR">Date Range</td>
                <td style="width: 260px;" class="tdmainR">From:&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="rdpDateStart" runat="server" AutoPostBack="True">
                    </telerik:RadDatePicker>
                </td>
                <td style="width: 340px;" class="tdmainR">To:&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="rdpDateEnd" runat="server" AutoPostBack="True">
                </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
        <br />
        <table style=" width:740;">
            <tr>
                <td style="width:280px;" class="tdmainR">Completed Orders By</td>
                <td style="width:160px;" align="right">
                    <asp:DropDownList ID="ddlSelection" runat="server" ValidationGroup="Completed" 
                        AutoPostBack="True">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Store</asp:ListItem>
                        <asp:ListItem>Market</asp:ListItem>
                        <asp:ListItem>Vendor</asp:ListItem>
<%--                        <asp:ListItem>Regionals</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td style="width:300px;" align="center">
                    <asp:HyperLink ID="hlCompleted" runat="server" CssClass="btnmenu" 
                        ValidationGroup="Completed" Target="_blank" Width="120px"> Continue </asp:HyperLink>
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
