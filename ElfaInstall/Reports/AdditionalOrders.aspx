<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdditionalOrders.aspx.cs" Inherits="ElfaInstall.Reports.AdditionalOrders" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Additional Orders</title>
    <link href="../Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:12px;">
    <center>
        <table width="800px">
            <tr>
                <td align="center">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="X-Large" Font-Bold="true">Additional Orders</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="tdtitle">
                    Paid From 
                    <telerik:RadDatePicker ID="rdpFrom" runat="server">
                    </telerik:RadDatePicker>
                    To
                    <telerik:RadDatePicker ID="rdpTo" runat="server">
                    </telerik:RadDatePicker>
                    <telerik:RadButton ID="rbShow" runat="server" Text="Show" Font-Bold="True" 
                        Skin="Telerik" Width="80px" onclick="RbShowClick">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlReport" runat="server" HorizontalAlign="Center" Width="805px">
            <asp:GridView ID="grdOrders" runat="server" Width="800px" 
                AutoGenerateColumns="False" DataSourceID="sdsOrders" 
                EmptyDataText="No records for your selection" EnableModelValidation="True" 
                CellPadding="3">
                <Columns>
                    <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer">
                    <ItemStyle Width="140px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorName" HeaderText="Vendor">
                    <ItemStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InstallDate" DataFormatString="{0:d}" 
                        HeaderText="Instal.Date">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PayDate" DataFormatString="{0:d}" 
                        HeaderText="Paid Date">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Amount" DataFormatString="{0:$0.00}" 
                        HeaderText="Amount">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Comments" HeaderText="Comments">
                    <ItemStyle Width="270px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </center>
    </div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    </form>
</body>
</html>
