<%@ Page Title="Error Page" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="ErrorMessage.aspx.cs" Inherits="ElfaInstall.ErrorMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table class="tablemain">
            <tr>
                <td class="tdheader" style="width: 660px"><br /> ERROR! <br />&nbsp;</td>
            </tr>
            <tr>
                <td align="center" style="width: 660px">
                    <asp:Label runat="Server" ID="lblError" ForeColor="Red" Font-Bold="True"> Error!</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 660px">
                    <br /><asp:Button ID="btnContinue" runat="server" CssClass="btnmenu" Text="Continue" OnClick="BtnContinueClick" />
                </td>
            </tr>
        </table>
        <br />&nbsp;
    </center>
</asp:Panel>
</asp:Content>
