<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="ElfaInstall.NewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table width="400px" cellpadding="3" cellspacing="3">
        <tr align="center">
            <td colspan="2" style=" font-size:14pt; font-weight:bold">Add New User Info:</td>
        </tr>
        <tr>
            <td style="width:400px" class="tdmain">User Name *</td>
            <td align="left"><asp:TextBox ID="txtName" runat="server" Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmain">User Status *</td>
            <td align="left">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" 
                    AutoPostBack="True">
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem>Admin</asp:ListItem>
                    <asp:ListItem>Region</asp:ListItem>
                    <asp:ListItem>Store</asp:ListItem>
                    <asp:ListItem>Vendor</asp:ListItem>
                    <asp:ListItem>Organizer</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain">Login *</td>
            <td align="left"><asp:TextBox ID="txtLogin" runat="server" Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmain">Password *</td>
            <td align="left"><asp:TextBox ID="txtPassword" runat="server" Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmain">Phone</td>
            <td align="left"><asp:TextBox ID="txtPhone" runat="server" Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmain">Email</td>
            <td align="left"><asp:TextBox ID="txtEmail" runat="server" Width="190px"></asp:TextBox></td>
        </tr>
    </table>
    <asp:Table ID="Table1" runat="server" Width="400px" CellPadding="0" CellSpacing="0" HorizontalAlign="Center">
        <asp:TableRow ID="rowStore" runat="server" Visible="false">
            <asp:TableCell ID="TableCell10" runat="server" CssClass="tdmain">Store Location *</asp:TableCell>
            <asp:TableCell ID="TableCell11" runat="server" HorizontalAlign="Left">
                <asp:DropDownList ID="ddlStore" runat="server" Width="180">
                    <asp:ListItem Text="" Value ="0"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowVendor" runat="server" Visible="false">
            <asp:TableCell ID="TableCell12" runat="server" CssClass="tdmain">Vendor Location *</asp:TableCell>
            <asp:TableCell ID="TableCell13" runat="server" HorizontalAlign="Left">
                    <asp:DropDownList ID="ddlVendor" runat="server" Width="180">
                    </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="rowOrganizer" runat="server" Visible="false">
            <asp:TableCell ID="TableCell14" runat="server" CssClass="tdmain">Organizer *</asp:TableCell>
            <asp:TableCell ID="TableCell15" runat="server" HorizontalAlign="Left">
                    <asp:DropDownList ID="ddlOrganizer" runat="server" Width="180">
                    </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" CssClass="td12cb"><br />* - mandatory fields</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow6" runat="Server" Height="50" VerticalAlign="Top">
            <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Center">
                <asp:Button runat="Server" ID="btnSave" CssClass="btnsubmit" Text=" Save Info " OnClick="BtnSaveClick" />
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Center">
                <asp:Button ID="btnReturn" runat="server" CssClass="btnmenu" Text="  Return  " CausesValidation="false" PostBackUrl="UserList.aspx" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
            <br />&nbsp;
            <asp:RequiredFieldValidator ID="valName" runat="server" ErrorMessage="User Name is Missing" ControlToValidate="txtName" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valStatus" runat="server" ErrorMessage="Plese Select User Status" Display="None" ControlToValidate="ddlStatus"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valLogin" runat="server" ErrorMessage="Login is Missing" Display="None" ControlToValidate="txtLogin"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valPassword" runat="server" ErrorMessage="Password is Missing" ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valStore" runat="server" ErrorMessage="Please Select Store" ControlToValidate="ddlStore" Display="None" InitialValue='0'></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
    </center>
</asp:Panel>
</asp:Content>
