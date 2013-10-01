<%@ Page Title="Vendor List" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="VendorList.aspx.cs" Inherits="ElfaInstall.VendorList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:660;">
        <tr>
            <td class="tdheader" style="height: 20px">
                Vendor List
            </td>
        </tr>
        <tr>
            <td style="width: 660px" valign="top" align="center">
                <asp:GridView ID="grvVendors" runat="Server" DataSourceID="sdsVendors"
                    CellPadding="0" DataMember="DefaultView" HorizontalAlign="Center" Width="600px" 
                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
                    ShowFooter="True" BackColor="White" OnPageIndexChanged="GrvVendorsPageIndexChanged" 
                    OnSorted="GrvVendorsSorted" CssClass="gridData" Font-Bold="True" 
                    PageSize="15" Font-Size="Small" EnableModelValidation="True">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="VendorID" 
                            DataNavigateUrlFormatString="EditVendor.aspx?VendorID={0}" 
                            DataTextField="VendorID" 
                            HeaderText="Installer ID" SortExpression="VendorID" >
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="VendorName" HeaderText="Installer Name" SortExpression="VendorName" >
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" >
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" >
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="active" HeaderText="Active" SortExpression="active" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Added" DataFormatString="{0:d}" HeaderText="Added">
                        <ItemStyle HorizontalAlign="Right" Width="60px" CssClass="gridcellright" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" 
                        HorizontalAlign="Center" Height="30px" />
                    <AlternatingRowStyle BackColor="#BFC7A4" />
                    <RowStyle Height="25px" />
                    <PagerStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:TextBox runat="server" ID="txtSearch" Width="80px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnSearch" Text="Search" 
                    onclick="BtnSearchClick" />
            </td>
        </tr>
        <tr><td style="width: 600px" class="td12">Click on Installer ID to Edit Vendor Info</td></tr>
        <tr><td align="center">
            <br /><asp:Button ID="btnAddNew" runat="server" CssClass="btnsubmit" Text="Add New" OnClick="BtnAddNewClick" /><br />&nbsp;
        </td></tr>
    </table>
    <asp:SqlDataSource ID="sdsVendors" runat="Server">
    </asp:SqlDataSource>
</center>
</asp:Panel>
</asp:Content>
