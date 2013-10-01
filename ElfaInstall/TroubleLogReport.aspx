<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TroubleLogReport.aspx.cs" Inherits="ElfaInstall.TroubleLogReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trouble Log Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:10px;">
    <center>
        <span style="font-size: 14pt; height:50pt"><strong>Trouble Log</strong></span>
        <br />&nbsp;
        <asp:GridView runat="Server" ID="grdOrders" Width="720px" 
            DataSourceID="sdsOrders" AutoGenerateColumns="False" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
            Font-Size="10px" ForeColor="Black">
            <Columns>
                <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderDate" DataFormatString="{0:d}" HeaderText="Order Date"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Problem Description">
                    <ItemStyle HorizontalAlign="Left" Width="490px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    </center>
    </div>
    </form>
</body>
</html>
