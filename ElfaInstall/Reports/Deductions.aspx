<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deductions.aspx.cs" Inherits="ElfaInstall.Reports.Deductions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <div>
    <center>
        <table width="800px">
            <tr>
                <td align="center">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="X-Large" Font-Bold="true">Orders with Deduction</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="tdtitle">
                    Vendor Pay Date From&nbsp;&nbsp; 
                    <telerik:RadDatePicker ID="rdpFrom" Runat="server">
                    </telerik:RadDatePicker>
                    &nbsp;&nbsp;
                    To
                    <telerik:RadDatePicker ID="rdpTo" Runat="server">
                    </telerik:RadDatePicker>
                    
                </td>
                <td rowspan='2'>
                    <telerik:RadButton ID="rbShow" runat="server" Text="Show" Font-Bold="True" 
                        Skin="Telerik" Width="80px" onclick="RbShowClick">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="tdtitle">
                    Installer Selection:&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlVendors" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlReport" runat="server" HorizontalAlign="Center" Width="805px" Visible="false">
            <asp:GridView ID="grdOrders" runat="server" Width="800px" 
                AutoGenerateColumns="False" DataSourceID="sdsOrders" 
                EmptyDataText="No records for your selection" EnableModelValidation="True" 
                CellPadding="3" AllowSorting="True" 
                onsorted="GrdOrdersSorted">
                <Columns>
                    <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InstallDate" HeaderText="Install.Date" 
                        DataFormatString="{0:d}" SortExpression="InstallDate">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StoreCode" HeaderText="Store" SortExpression="StoreCode">
                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorName" HeaderText="Installer /Coordinator" SortExpression="VendorName" >
                        <ItemStyle Width="170px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Customer" HeaderText="Customer">
                        <ItemStyle Width="175px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OrderPrice" HeaderText="Invoice Amt." DataFormatString="{0:$0.00}" >
                        <ItemStyle Width="80px" HorizontalAlign="Right"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorDate" HeaderText="Pay Date" 
                        DataFormatString="{0:d}" SortExpression="VendorDate" >
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            Total orders for selected parameters:
            <asp:Label ID="lblTotal" runat="server"></asp:Label>
        </asp:Panel>
        <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    </center>
    </div>
    </form>
</body>
</html>
