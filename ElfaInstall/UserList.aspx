<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="ElfaInstall.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <asp:Panel runat="server" ID="pnlList">
    <table style=" width:920;">
        <tr>
            <td align="center" class="tdheader" style="height: 21px">
                User List
            </td>
        </tr>
        <tr>
            <td style="width: 920px; text-align:center;" valign="top">
                <asp:GridView runat="Server" ID="grdUsers"
                    DataSourceID="sdsUsers" AutoGenerateColumns="False" 
                    CellPadding="0" HorizontalAlign="Center" 
                    Font-Bold="True" Font-Size="Small" BackColor="White" GridLines="Vertical" 
                    AllowPaging="True" OnPageIndexChanged="GrdUsersPageIndexChanged" PageSize="20" 
                    EnableModelValidation="True" 
                    onselectedindexchanged="GrdUsersSelectedIndexChanged" AllowSorting="True" 
                    onsorted="GrdUsersSorted">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="2px" Font-Size="XX-Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" 
                            SortExpression="UserName" >
                            <ItemStyle CssClass="gridcellleft" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" 
                            SortExpression="Status" >
                            <ItemStyle CssClass="gridcellleft" Width="60px" />
                        </asp:BoundField>
<%--                        <asp:BoundField DataField="Vendor" HeaderText="Vendor Location" >
                            <ItemStyle CssClass="gridcellleft" Width="130px" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="userPhone" HeaderText="Phone" >
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="userEmail" HeaderText="Email" >
                        <ItemStyle CssClass="gridcellleft" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alllogins" DataFormatString="{0:d}" HeaderText="Last Logins" >
                            <ItemStyle CssClass="gridcellleft" Width="240px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="active" HeaderText="Active" ReadOnly="True" 
                            SortExpression="active">
                        <ControlStyle Font-Bold="True" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:CheckBoxField>
<%--                        <asp:HyperLinkField DataNavigateUrlFields="UserID" 
                            DataNavigateUrlFormatString="ResetPassword.aspx?UserID={0}" 
                            HeaderText="Reset Password" NavigateUrl="~/ResetPassword.aspx" Text="Reset">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:HyperLinkField>--%>
<%--                        <asp:CommandField ButtonType="Button" SelectText="Submit" ShowSelectButton="True" HeaderText="Activate/Deact. User">
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:CommandField>--%>
                        <asp:CommandField HeaderText="Edit User" ButtonType="Button" SelectText="Edit" ShowSelectButton="True" >
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:CommandField>
                    </Columns>
                    <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" HorizontalAlign="Center" Height="30px" />
                    <AlternatingRowStyle BackColor="#BFC7A4" />
                    <RowStyle Height="25px" />
                    <PagerStyle HorizontalAlign="Center" />
                </asp:GridView>
                <asp:SqlDataSource runat="Server" ID="sdsUsers">
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="btnAddUser" runat="server" Text="Add New User" CssClass="btnsubmit" PostBackUrl="~/NewUser.aspx" />
                <br /><br />
<%--                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="~/UserReport.aspx"
                    Target="_blank">User Report</asp:HyperLink>--%>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlEdit" Visible="false">
        <table width="400px">
            <tr align="center">
                <td colspan="2" style=" font-size:14pt; font-weight:bold">Edit User Info:</td>
            </tr>
            <tr>
                <td class="tdmain">User Name</td>
                <td align="left"><asp:Label runat="server" ID="lblUserName">User Name</asp:Label></td>
            </tr>
            <tr>
                <td class="tdmain">Status</td>
                <td align="left"><asp:Label runat="server" ID="lblStatus">Admin</asp:Label></td>
            </tr>
            <tr>
                <td class="tdmain">Login</td>
                <td align="left"><asp:TextBox ID="txtLogin" runat="server" Width="190px" 
                        MaxLength="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain">Password</td>
                <td align="left">
                    <asp:Button runat="server" ID="btnReser" Text="Reset Password" 
                        Width="150px" onclick="BtnReserClick" /></td>
            </tr>
            <tr>
                <td class="tdmain">Active</td>
                <td align="left"><asp:CheckBox runat="server" ID="chkActive" /></td>
            </tr>
            <tr>
                <td class="tdmain">Phone</td>
                <td align="left"><asp:TextBox ID="txtPhone" runat="server" Width="190px" 
                        MaxLength="30"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain">Email</td>
                <td align="left"><asp:TextBox ID="txtEmail" runat="server" Width="190px" 
                        MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"></td></tr>
            <tr>
                <td align="right"><asp:Button runat="server" ID="btnSave" Text="Save"  CssClass="btnsubmit" 
                        onclick="BtnSaveClick" Width="80px" /></td>
                <td align="center"><asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btnmenu" 
                        onclick="BtnCancelClick" Width="80px" /></td>
            </tr>
            <tr><td colspan="2"></td></tr>
        </table>
    </asp:Panel>
    </center>
</asp:Panel>
</asp:Content>
