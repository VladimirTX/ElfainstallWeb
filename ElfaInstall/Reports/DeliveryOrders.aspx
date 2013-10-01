<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryOrders.aspx.cs" Inherits="ElfaInstall.Reports.DeliveryOrders" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div>
    <center>
        <table width="800px">
            <tr>
                <td align="center">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="X-Large" Font-Bold="true">Orders with Delivery</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="tdtitle">
                    Installed From&nbsp;&nbsp; 
                    <telerik:RadDatePicker ID="rdpFrom" Runat="server" Culture="en-US">
                    </telerik:RadDatePicker>
                    &nbsp;&nbsp;
                    To
                    <telerik:RadDatePicker ID="rdpTo" Runat="server">
                    </telerik:RadDatePicker>
                    <telerik:RadButton ID="rbShow" runat="server" Text="Show" Font-Bold="True" 
                        Skin="Telerik" Width="80px" onclick="RbShowClick">
                    </telerik:RadButton>
                    
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlReport" runat="server" HorizontalAlign="Center" Width="805px" Visible="false">
            <asp:GridView ID="grdOrders" runat="server" Width="800px" 
                AutoGenerateColumns="False" DataSourceID="sdsOrders" 
                EmptyDataText="No records for your selection" EnableModelValidation="True" 
                CellPadding="3" AllowSorting="True" onsorted="GrdOrdersSorted">
                <Columns>
                    <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InstallDate" HeaderText="Install.Date" 
                        DataFormatString="{0:d}" SortExpression="InstallDate">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Store" HeaderText="Store" SortExpression="Store">
                    <ItemStyle Width="75px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor">
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Customer" HeaderText="Customer">
                    <ItemStyle Width="225px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OrderPrice" HeaderText="Invoice Amt." DataFormatString="{0:$0.00}" >
                    <ItemStyle Width="100px" HorizontalAlign="Right"/>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </center>
    </div>
    <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    </form>
</body>
</html>
