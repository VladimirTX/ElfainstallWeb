<%@ Page Title="Open Orders" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="ElfaInstall.Orders" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style=" width:1000;">
    <tr>
        <td class="tdheader" style="height: 20px">
            <asp:Label runat="server" ID="lblHeader">Installation Orders List</asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 1000px" valign="top">
            <asp:GridView runat="Server" ID="grdOrders" Width="1000px" 
                DataSourceID="sdsOrders" AutoGenerateColumns="False" 
                OnRowDataBound="GrdOrdersRowDataBound" CellPadding="0" HorizontalAlign="Center" 
                GridLines="Vertical" CssClass="gridData" ShowFooter="True" BackColor="White" 
                AllowPaging="True" AllowSorting="True" OnPageIndexChanged="GrdOrdersPageIndexChanged" 
                OnSorted="GrdOrdersSorted" PageSize="20" Font-Size="Small" 
                EnableModelValidation="True">
                <Columns>
<%--                    <asp:TemplateField HeaderText="Calls">
                        <EditItemTemplate>
                            <asp:CheckBox ID="checkBox1" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbCall1" runat="server" Checked='<%# Eval("Call1") %>' Enabled="False" Width="8pt" />
                            <asp:CheckBox ID="cbCall2" runat="server" Checked='<%# Eval("Call2") %>' Enabled="False" Width="8pt" />
                            <asp:CheckBox ID="cbCall3" runat="server" Checked='<%# Eval("Call3") %>' Enabled="False" Width="8pt" />
                        </ItemTemplate>
                        <ItemStyle Width="90px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="OrderID">
                    <ItemStyle Font-Size="XX-Small" Width="5px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date"  
                        DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="OrderDate" >
                        <ItemStyle CssClass="gridcellleft" Width="75px" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="OrderID" 
                        DataNavigateUrlFormatString="OrderDetails.aspx?OrderID={0}"
                        DataTextField="OrderNumb" HeaderText="Order #" NavigateUrl="~/OrderDetails.aspx" 
                        SortExpression="OrderNumb" >
                        <ItemStyle CssClass="gridcellright" Width="70px" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Customer" DataField="Customer" >
                        <ItemStyle Width="160px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Delivery" Visible="True" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StoreCode" HeaderText="Store" SortExpression="StoreCode" >
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorName" HeaderText="24 hr/Vendor Assignment" SortExpression="VendorName" >
                        <ItemStyle Width="170px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Image Reviewed">
                        <EditItemTemplate>
                            <asp:CheckBox ID="checkBox4" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbImage" runat="server" Checked='<%# Eval("imgView") %>' AutoPostBack="true" 
                                Enabled="true" OnCheckedChanged="cbImage_OnCheckedChanged" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Conf. with Cust.">
                        <EditItemTemplate>
                            <asp:CheckBox ID="checkBox2" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbConf" runat="server" Checked='<%# Eval("Approved") %>' Enabled="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust. Response">
                        <EditItemTemplate>
                            <asp:CheckBox ID="checkBox3" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbResponse" runat="server" Checked='<%# Eval("response") %>' Enabled="false" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ins. Deduct">
                        <EditItemTemplate>
                            <asp:CheckBox ID="checkBox4" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbDeduct" runat="server" Checked='<%# Eval("Deduct") %>' Enabled="true" 
                                AutoPostBack="true" OnCheckedChanged="cbDeduct_OnCheckedChanged"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="OrderPrice" HeaderText="Total Invoice" 
                        DataFormatString="{0:$0.00}" HtmlEncode="False">
                    <ItemStyle Width="60px" CssClass="gridcellright"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="InstallDate" HeaderText="48 hr/ Sched. Install. Date" 
                    DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="InstallDate" >
                        <ItemStyle CssClass="gridcellleft" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InstallTime" HeaderText="Install. Time" DataFormatString="{0:t}" HtmlEncode="False" >
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Promo" HeaderText="Event" SortExpression="Promo">
                        <ItemStyle Font-Bold="True" Font-Size="Small" ForeColor="Red" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Options" HeaderText="Opt." SortExpression="Options">
                    </asp:BoundField>
                    <asp:BoundField DataField="isTax" SortExpression="isTax">
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#C4C2C2" Font-Bold="True" ForeColor="#0000C0" HorizontalAlign="Center" Height="30px" />
                <PagerSettings Mode="NumericFirstLast" />
                <AlternatingRowStyle BackColor="LightGray" />
                <RowStyle Height="25px" />
                <PagerStyle HorizontalAlign="Center" />
             </asp:GridView>
             <br />
             <center>
             <asp:HyperLink ID="hlSchedule" runat="server" Target="_blank" Font-Bold="True" Visible="False">Installation Schedule</asp:HyperLink>
             <asp:SqlDataSource runat="Server" ID="sdsOrders" />
            <asp:HiddenField ID="hfParameter" runat="server" />
            <asp:HiddenField ID="hfVendor" runat="server" />
            </center>
        </td>
    </tr>
    <tr>
        <td align="center" style=" font-size:smaller">
            * - Bold blue rows - orders with delivery.
        </td>
    </tr>
</table>
</asp:Content>
