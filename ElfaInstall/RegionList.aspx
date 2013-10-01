<%@ Page Title="Region List" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="RegionList.aspx.cs" Inherits="ElfaInstall.RegionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:760;">
        <tr>
            <td class="tdheader" style="height: 20px">
                Regions List
            </td>
        </tr>
        <tr>
            <td style="width: 740px" valign="top" align="center">
                <asp:Table runat="Server" ID="tblRegions" Width="680" CellPadding="5" CellSpacing="0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small">
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell1" Width="100px" runat="server" CssClass="tdtitle" BorderWidth="1">RegionID</asp:TableCell>
                        <asp:TableCell ID="TableCell2" Width="240px" runat="server" CssClass="tdtitle" BorderWidth="1">Region Admin</asp:TableCell>
                        <asp:TableCell ID="TableCell3" Width="440px" runat="server" CssClass="tdtitle" BorderWidth="1">Store List</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
    <br />&nbsp;Click on Region ID to Edit Region Info<br />&nbsp;
    </center>
</asp:Panel>
</asp:Content>
