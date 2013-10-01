<%@ Page Title="elfa® Reports" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="RepCompletedTCS.aspx.cs" Inherits="ElfaInstall.Reports.RepCompletedTCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />
        TCS Comparative Analysis Summary<br />&nbsp;</center></div>
    <table style=" width:740; font-size:small;" cellpadding="5" cellspacing="0">
        <tr style="font-weight:bold;">
            <td style="width:120px; font-size: 12pt;" align="right">Start Date:</td>
            <td style="width:160px; font-size: 12pt;" align="left">
                <asp:DropDownList runat="Server" ID="ddlStart" DataTextFormatString="{0:d}" 
                    AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="width:120px; font-size: 12pt;" align="right">End Date:</td>
            <td style="width:160px; font-size: 12pt;" align="left">&nbsp;&nbsp;
                <asp:DropDownList runat="Server" ID="ddlEnd" DataTextFormatString="{0:d}" 
                    AutoPostBack="True"></asp:DropDownList>
            </td>
            <td style="width: 180px;" align="center">
                <asp:HyperLink ID="hlCompleted" runat="server" CssClass="btnmenu"
                    Target="_blank" Width="120px">Show Report</asp:HyperLink>
            </td>
        </tr>
    </table>
    &nbsp;
    </center>
</asp:Panel>
</asp:Content>
