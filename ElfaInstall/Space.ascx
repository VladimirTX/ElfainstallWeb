<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Space.ascx.cs" Inherits="ElfaInstall.Space" %>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>

<table width="800px" cellpadding="1px" cellspacing="1px">
    <tr>
        <td style="width: 150px;" class="td14a">Space / Name</td>
        <td align="left" class="width: 250px;">
            <asp:TextBox ID="txtSpaceName" runat="server" Width="250px" MaxLength="50" Font-Bold="true"></asp:TextBox>
        </td>
        <td style="width: 90px;" class="td12a">Space ID #</td>
        <td style="width: 160px;" align="left">
            <asp:TextBox ID="txtSpaceID" runat="server" Width="100px" MaxLength="10" 
                Font-Bold="true"></asp:TextBox>
        </td>
        <td style="width: 140px; border-left-style: solid; border-left-width: thin; border-left-color: #000000; text-align:center; font-size:12px; font-weight: bold; color: Black;" 
            rowspan="4">
            Mark Space<br />as Deleted<br />
            <asp:CheckBox ID="cbDeleted" runat="server" Text="" AutoPostBack="True" 
                oncheckedchanged="CbDeletedCheckedChanged" />
        </td>
    </tr>
    <tr>
        <td class="td12">
            Texture&nbsp;&nbsp; 
            <asp:DropDownList ID="ddlTexture" runat="server" AutoPostBack="True" 
                onselectedindexchanged="DdlTextureSelectedIndexChanged">
                <asp:ListItem Value="True">Y</asp:ListItem>
                <asp:ListItem Value="False">N</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="td12" colspan="3">Description&nbsp;&nbsp;
            <asp:DropDownList ID="ddlDescription" runat="server" Width="200px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Orange Peel</asp:ListItem>
                <asp:ListItem>Crow’s Foot</asp:ListItem>
                <asp:ListItem>Knock Down</asp:ListItem>
                <asp:ListItem>Hand Texture</asp:ListItem>
            </asp:DropDownList>
        </td> 
    </tr>
    <tr>
        <td class="td12">Painting Option</td>
        <td align="left">
            <asp:DropDownList ID="ddlColors" runat="server">
            </asp:DropDownList>
        </td>
        <td class="td12" colspan="2">Color: 
            <asp:TextBox ID="txtColorName" runat="server" MaxLength="25" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="td12">Removal</td>
        <td class="td12a" colspan="3">
            <asp:TextBox ID="txtRemoval" runat="server" Width="460px" MaxLength="500" 
                Rows="1" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="td12">Non elfa items<br />&nbsp;to install/assemle</td>
        <td class="td12a" colspan="3"> 
            <asp:TextBox ID = "txtNonElfa" runat="server" Width="460px" MaxLength="200"></asp:TextBox>
        </td>
        <td rowspan="2" align="center" 
            style="border-left-style: solid; border-left-width: thin; border-left-color: #000000">
            <telerik:RadButton runat="server" ID="btnSave" Text=" Save "
                Width="100px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                onclick="BtnSaveClick">
            </telerik:RadButton>
        </td>
    </tr>
    <tr>
        <td class="td12">Space Details</td>
        <td class="td12a" colspan="3">
            <asp:TextBox ID="txtInstructions" runat="server" Width="460px" MaxLength="500" 
                TextMode="MultiLine" Rows="1"></asp:TextBox>
        </td>
    </tr>
    <tr><td colspan="5"><hr style="line-height: 3px; color:Black; font-weight:bold" /></td></tr>
</table>