<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="RegionSelection.aspx.cs" Inherits="ElfaInstall.RegionSelection" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table width="500px">
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td>Select Region:</td>
                <td>
                    <asp:DropDownList ID="ddlRegions" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnRegion" runat="server" Text="Select" 
                        onclick="btnRegion_Click" Width="100px" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
