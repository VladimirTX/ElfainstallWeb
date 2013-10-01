<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceOrders.aspx.cs" Inherits="ElfaInstall.Reports.ServiceOrders" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Orders</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
        </telerik:RadScriptManager>
        <div style="text-align: center; font-size:14px;">
        <center>
            <span style="font-size: 16pt; height:50pt"><strong>Service Orders</strong>
            <br /><br /> 
            <label style="font-size: 14px">Order Created Date: </label>
            &nbsp;&nbsp;
            <telerik:RadDatePicker ID="rdpDate" Runat="server">
            </telerik:RadDatePicker>
            &nbsp;&nbsp;&nbsp;<telerik:RadButton ID="btnShow" runat="server" 
                Text="Show Report" Font-Bold="True" Skin="Telerik" 
                onclick="BtnShowClick">
            </telerik:RadButton>
            <br />
            </span><br />
            <asp:Panel ID="pnlReport" runat="server" HorizontalAlign="Center" Width="605px">
                <asp:GridView ID="grdOrders" runat="server" Width="600px" 
                    AutoGenerateColumns="False" DataSourceID="sdsOrders" 
                    EmptyDataText="No records for your selection" EnableModelValidation="True" 
                    CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="OrderNumb" HeaderText="Order #" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="City" HeaderText="City " >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VendorName" HeaderText="Vendor" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </center>
        </div>
        <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    </div>
    </form>
</body>
</html>
