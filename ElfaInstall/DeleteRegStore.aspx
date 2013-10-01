<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="DeleteRegStore.aspx.cs" Inherits="ElfaInstall.DeleteRegStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <br /><br />
        <table style="width: 400px; border-left-color: navy; border-bottom-color: navy; border-top-style: double; border-top-color: navy; border-right-style: double; border-left-style: double; border-right-color: navy; border-bottom-style: double;">
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Do you want to remove store" Font-Bold="True" Font-Size="Larger"></asp:Label><br />
                    <asp:Label ID="lblMember" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"
                        Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="height:2px; color:Navy" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px" align="center">
                    <asp:Button ID="btnYes" runat="server" CssClass="btnmenu" Text="Yes" Width="100px" OnClick="BtnYesClick" />
                    <br />&nbsp;
                </td>
                <td style="width: 200px" align="center">
                    <asp:Button ID="btnNo" runat="server" CssClass="btnmenu" Text="No" Width="100px" OnClick="BtnNoClick" />
                    <br />&nbsp;
                </td>
            </tr>
        </table>
    </center>
</asp:Panel>
</asp:Content>
