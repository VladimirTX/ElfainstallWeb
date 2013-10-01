<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowSpace.ascx.cs" Inherits="ElfaInstall.ShowSpace" %>
<table width="640px" cellpadding="1px" cellspacing="1px"  frame="box" 
    style="border: thin solid #000000">
    <tr>
        <td style="width: 180px;" class="td14a">Space / Name</td>
        <td class="td12a" style="width: 250px;">
            <asp:Label ID="lblSpaceName" runat="server"></asp:Label>
        </td>
        <td style="width: 60px;" class="td12a">ID #</td>
        <td style="width: 150px;" class="td12a">
            <asp:Label ID="lblSpaceID" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td12">
            Texture&nbsp;&nbsp; 
            <asp:Label ID="lblTexture" runat="server"></asp:Label>
        </td>
        <td class="td12" colspan="3">Description&nbsp;&nbsp;
            <asp:Label ID="lblDescription" runat="server" ForeColor="Black"></asp:Label>
        </td> 
    </tr>
    <tr>
        <td class="td12">Painting Option</td>
        <td class="td12a">
            <asp:Label ID="lblColor" runat="server"></asp:Label>
        </td>
        <td class="td12" colspan="2">Color: 
            <asp:Label CssClass="lblsize12a" ID="lblColorName" runat="server">Color Name</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td12">Removal</td>
        <td class="td12a" colspan="3">
<%--            <asp:TextBox ID="txtRemoval" runat="server" Width="440px" MaxLength="500" 
                ReadOnly="True" Rows="1" TextMode="MultiLine"></asp:TextBox>--%>
            <asp:Label ID="lblRemoval" runat="server" Width="440px" BorderColor="#999999" 
                BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td12">Non elfa items<br />&nbsp;to install/assemle</td>
        <td class="td12a" colspan="3">
<%--            <asp:TextBox ID="txtNonElfa" runat="server" Rows="1" 
                        ReadOnly="True" Width="440px"></asp:TextBox>--%>
            <asp:Label ID="lblNonElfa" runat="server" Width="440px" BorderColor="#999999" 
                BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td12">Space Details</td>
        <td class="td12a" colspan="3">
<%--            <asp:TextBox ID="txtInstructions" runat="server" Width="440px" MaxLength="500" 
                ReadOnly="True" TextMode="MultiLine" Rows="1"></asp:TextBox>--%>
            <asp:Label ID="lblInstructions" runat="server" Width="440px" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        </td>
    </tr>
</table>