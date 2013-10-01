<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotPickedUp.aspx.cs" Inherits="ElfaInstall.Reports.NotPickedUp" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orders Not Picked Up</title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
        </telerik:RadScriptManager>
    <div style="text-align: center; font-size:14px;">
    <center>
        <span style="font-size: 14pt; height:50pt"><strong>Not Picked Up Orders</strong></span><br />
        <br />
        <table width="700px" style=" font-size: larger">
            <tr>
                <td style="width:250px" align="right">Installation date range From:</td>
                <td style="width:150px" align="left">
                    <telerik:RadDatePicker ID="rdpStart" Runat="server" Width="140px">
                    </telerik:RadDatePicker>
                </td>
                <td style="width:30px" align="right">To:</td>
                <td style="width:150px" align="left">
                    <telerik:RadDatePicker ID="rdpEnd" Runat="server" Width="140px">
                    </telerik:RadDatePicker>
                </td>
                <td style="width:150px" align="left">
                    <asp:Button ID="btnSelect" runat="server" Text="Select" Height="22px" 
                        onclick="Button1Click" Width="80px" />
                </td>
            </tr>
        </table>
        <asp:GridView runat="server" ID="grdNotPickedUp" DataSourceID="sdsNotPickedUp" 
            AutoGenerateColumns="False" CellPadding="3" Width="700px" 
            AllowPaging="True" BackColor="Transparent" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" PageSize="30" AllowSorting="True" 
            EnableModelValidation="True" CellSpacing="3" 
            onpageindexchanged="GrdNotPickedUpPageIndexChanged" 
            onsorted="GrdNotPickedUpSorted">
            <Columns>
                <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" 
                    SortExpression="OrderDate" DataFormatString="{0:d}">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="StoreCode" HeaderText="Store" 
                    SortExpression="StoreCode">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer">
                <ItemStyle HorizontalAlign="Left" Width="170px" />
                </asp:BoundField>
                <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                    SortExpression="InstallDate" DataFormatString="{0:d}">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="VendorName" HeaderText="Vendor" 
                    SortExpression="VendorName">
                <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource runat="Server" ID="sdsNotPickedUp"/>
    </center>
    </div>
    </form>
</body>
</html>
