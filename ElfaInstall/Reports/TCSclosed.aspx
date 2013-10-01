<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TCSclosed.aspx.cs" Inherits="ElfaInstall.Reports.TCSclosed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closed Orders</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:10px;">
    <center>
        <asp:table id="tblTop" runat="server" CellSpacing="0" Width="760px" CellPadding="0">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" Font-Bold="True" Font-Size="10" Height="30" VerticalAlign="Top">
                    <asp:Label runat="Server" ID="lblHeader" Font-Size="Large">Completed Orders Comparative Analysis</asp:Label><br />
                    <asp:Label runat="Server" ID="lblDate">(09/01/2006)</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    
                </asp:TableCell>
            </asp:TableRow>
        </asp:table>
        <br />
        <asp:GridView runat="Server" ID="grdStores" Width="760px" 
            DataSourceID="sdsOrders" AutoGenerateColumns="False" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
            Font-Size="10px" ForeColor="Black">
            <Columns>
                <asp:BoundField DataField="StoreName" HeaderText="Store">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Total" HeaderText="Total number of installs">
                    <ItemStyle HorizontalAlign="Center" Width="75px"  />
                </asp:BoundField>
                <asp:BoundField DataField="Price1" HeaderText="# of install from $0-$1000">
                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                </asp:BoundField>
                <asp:BoundField DataField="Price2" HeaderText="# of install from $1001-$2000">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="Price3" HeaderText="# of install from $2001-$3000">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="Price4" HeaderText="# of install from $3001 and Up">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="Total Amount" 
                    DataFormatString="{0:$0,000.00}" HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="140px" />
                    <HeaderStyle BackColor="#E0E0E0" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    </center>
    </div>
    </form>
</body>
</html>
