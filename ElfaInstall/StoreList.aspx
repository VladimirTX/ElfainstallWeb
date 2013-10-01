<%@ Page Title="Store List" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="StoreList.aspx.cs" Inherits="ElfaInstall.StoreList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:760;">
        <tr>
            <td class="tdheader" style="height: 20px">
                Store List
            </td>
        </tr>
        <tr>
            <td style="width: 740px" valign="top" align="center">
                <asp:GridView ID="grvStores" runat="server" DataSourceID="sdsStores" 
                    CellPadding="0" DataMember="DefaultView" HorizontalAlign="Center" Width="740px" 
                    AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" BackColor="White" 
                    PageSize="16" AllowSorting="True" OnPageIndexChanged="GrvStoresPageIndexChanged" 
                    OnSorted="GrvStoresSorted" Font-Bold="True" Font-Size="Smaller">
                    <Columns>
                        <asp:HyperLinkField DataTextField="StoreNumb" HeaderText="Store #" 
                            DataNavigateUrlFields="StoreID" DataNavigateUrlFormatString="EditStore.aspx?StoreID={0}" 
                            NavigateUrl="~/EditStore.aspx" SortExpression="StoreNumb" >
                            <ItemStyle Width="100px" CssClass="gridDataC" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="StoreCode" HeaderText="Store Code" SortExpression="StoreCode" >
                            <ItemStyle Width="100px" CssClass="gridDataC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreName" HeaderText="Store Name" SortExpression="StoreName" >
                            <ItemStyle Width="220px" CssClass="gridDataL" Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MarketName" HeaderText="Market" SortExpression="MarketName" >
                            <ItemStyle Width="220px" CssClass="gridDataL" Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" >
                            <ItemStyle Width="100px" CssClass="gridDataC" Font-Size="Small" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" 
                        HorizontalAlign="Center" Height="30px" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="#BFC7A4" />
                    <RowStyle Height="25px" />
                </asp:GridView>
                <br />
            </td>
        </tr>
        <tr><td style="width: 740px" class="td12">Click on Store# to Edit Store Info</td></tr>
        <tr>
            <td style="width: 740px" valign="top" align="center">
                <center>
                    <asp:Button ID="btnAdd" runat="server" Text=" Add New " CssClass="btnmenu" OnClick="BtnAddClick" />
                </center>
            </td>
        </tr>
    </table>
    <br />&nbsp;
    <asp:SqlDataSource ID="sdsStores" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
