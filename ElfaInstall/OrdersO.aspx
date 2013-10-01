<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrdersO.aspx.cs" Inherits="ElfaInstall.OrdersO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain" Width="1010px">
    <center>
    <table style=" width:1000;">
        <tr>
            <td class="tdheader" style="height: 20px">
                <asp:Label runat="server" ID="lblHeader">Orders List</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 1050px" valign="top">
                <asp:GridView runat="Server" ID="grdOrders" Width="1000px" 
                    DataSourceID="sdsOrders" AutoGenerateColumns="False" 
                    OnRowDataBound="GrdOrdersRowDataBound" CellPadding="0" HorizontalAlign="Center" 
                    GridLines="Vertical" CssClass="gridData" ShowFooter="True" BackColor="White" 
                    AllowPaging="True" AllowSorting="True" OnPageIndexChanged="GrdOrdersPageIndexChanged" 
                    OnSorted="GrdOrdersSorted" PageSize="15" Font-Size="Small" 
                    EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date"  
                            DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" SortExpression="OrderDate" >
                            <ItemStyle CssClass="gridcellright" Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="OrderID" 
                            DataNavigateUrlFormatString="OrderDetails.aspx?OrderID={0}"
                            DataTextField="OrderNumb" HeaderText="Order #" NavigateUrl="~/OrderDetails.aspx" 
                            SortExpression="OrderNumb" >
                            <ItemStyle CssClass="gridcellright" Width="70px" />
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="Customer" DataField="Customer" SortExpression="Customer" >
                            <ItemStyle Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Delivery" Visible="True" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreCode" HeaderText="Store" SortExpression="StoreName" >
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VendorName" HeaderText="24 hr/Vendor Assignment" SortExpression="VendorName" >
                            <ItemStyle Width="140px" />
                        </asp:BoundField>
<%--                        <asp:TemplateField HeaderText="Calls">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbCall1" runat="server" Checked='<%# Eval("Call1") %>' Enabled="False" Width="8pt" />
                                <asp:CheckBox ID="cbCall2" runat="server" Checked='<%# Eval("Call2") %>' Enabled="False" Width="8pt" />
                                <asp:CheckBox ID="cbCall3" runat="server" Checked='<%# Eval("Call3") %>' Enabled="False" Width="8pt" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridcellcenter"/>
                            <ItemStyle Width="110px" CssClass="gridcellleft" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Conf. by Phone">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBox2" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbPhone" runat="server" Checked='<%# Eval("ByPhone") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conf. by Survey">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBox3" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSurvey" runat="server" Checked='<%# Eval("BySurvey") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="InstallDate" HeaderText="48 hr/ Sched. Install. Date" DataFormatString="{0:MM/dd/yyyy}" 
                            HtmlEncode="False" SortExpression="InstallDate" >
                            <ItemStyle CssClass="gridcellright" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InstallTime" HeaderText="Install. Time" DataFormatString="{0:t}" HtmlEncode="False">
                            <ItemStyle Width="80px" CssClass="gridcellright" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrderPrice" HeaderText="Invoice" 
                            DataFormatString="{0:$0.00}" HtmlEncode="False">
                            <ItemStyle CssClass="gridcellright" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Promo" HeaderText="Event" SortExpression="Promo">
                            <ItemStyle Font-Bold="True" Font-Size="Small" ForeColor="Red" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Options" HeaderText="Opt." SortExpression="Options">
                    </asp:BoundField>
                        <asp:BoundField DataField="isTax" HeaderText=" " SortExpression="isTax">
                        <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SpecialStatus" HeaderText="Spec. Status" 
                            SortExpression="SpecialStatus" >
                        <ItemStyle Width="50px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#C4C2C2" Font-Bold="True" ForeColor="#0000C0" HorizontalAlign="Center" Height="30px" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <AlternatingRowStyle BackColor="#BFC7A4" />
                    <RowStyle Height="25px" />
                    <PagerStyle HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:SqlDataSource runat="Server" ID="sdsOrders" />
                <asp:HiddenField ID="hfParameter" runat="server" />
                <asp:HiddenField ID="hfOrganizerID" runat="server" />
            </td>
        </tr>
    </table>
    <table style="width: 1000px; font-size:smaller;" cellpadding="0" cellspacing="2">
        <tr>
            <td align="center" colspan="5">
                * - Bold blue rows - orders with delivery.
            </td>
        </tr>
    </table>
    </center>
</asp:Panel>
</asp:Content>
