<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeletedOrders.aspx.cs" Inherits="ElfaInstall.Reports.DeletedOrders" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deleted Orders</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:12px;">
    <center>
        <span style="font-size: 14pt; height:50pt"><strong>Deleted Orders</strong></span><br />
        <br />
        <asp:GridView runat="server" ID="grdDeleted" DataSourceID="sdsDeleted" 
            AutoGenerateColumns="False" CellPadding="3" Width="950px" 
            AllowPaging="True" BackColor="Transparent" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" PageSize="30" AllowSorting="True" 
            EnableModelValidation="True">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="OrderID" DataNavigateUrlFormatString="OrderInfo.aspx?OrderID={0}"
                    DataTextField="OrderNumb" HeaderText="Order #" NavigateUrl="OrderInfo.aspx" Target="_blank" SortExpression="OrderNumb" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" 
                    DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="OrderDate" >
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                    SortExpression="InstallDate">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderPrice" DataFormatString="{0:c}" HeaderText="Price"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="55px" />
                </asp:BoundField>
                <asp:BoundField DataField="sCurrent" HeaderText="Last Status">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Cusomer" HeaderText="Customer" 
                    SortExpression="Cusomer">
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName">
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
<%--                <asp:BoundField DataField="RegionName" HeaderText="Region" 
                    SortExpression="RegionName" >
                <ItemStyle Width="100px" />
                </asp:BoundField>--%>
                <asp:BoundField DataField="Comments" HeaderText="Comments">
                    <ItemStyle Width="260px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DateDeleted" DataFormatString="{0:MM/dd/yy}" 
                    HeaderText="Date Deleted" HtmlEncode="False" SortExpression="DateDeleted">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="DeletedBy" HeaderText="Deleted By" 
                    SortExpression="DeletedBy">
                <ItemStyle Width="50px" />
                </asp:BoundField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BorderColor="#404040" BorderStyle="Solid" BorderWidth="1px" />
        </asp:GridView>
        <asp:SqlDataSource runat="Server" ID="sdsDeleted" ConnectionString="<%$ ConnectionStrings:CommConnection %>" ProviderName="<%$ ConnectionStrings:CommConnection.ProviderName %>"/>
    </center>
    </div>
    </form>
</body>
</html>
