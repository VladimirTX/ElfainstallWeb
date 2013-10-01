<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportProduct.aspx.cs" Inherits="ElfaInstall.Reports.ReportProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missing/Damaged Product</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:10px;">
    <center>
        <table width="760px" cellpadding="5px" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlReport" runat="server" 
                        onselectedindexchanged="DdlReportSelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0" Text="">Select Report</asp:ListItem>
                        <asp:ListItem Value="1">Missing Product</asp:ListItem>
                        <asp:ListItem Value="2">Damaged Product</asp:ListItem>
                        <asp:ListItem Value="3">Design Flaw</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:table id="tblMissing" runat="server" CellSpacing="0" Width="760px" CellPadding="0" Visible="false">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" Font-Bold="True" Font-Size="12" Height="30" VerticalAlign="Top">
                    <asp:Label runat="Server" ID="lblHeader">Missing Product&nbsp;&nbsp;from </asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblDate1">09/01/2006</asp:Label>&nbsp;&nbsp;to&nbsp;
                    <asp:Label runat="Server" ID="lblDate2">09/16/2006</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:table>
        <asp:GridView runat="Server" ID="grdMissing" Width="760px" 
            DataSourceID="sdsMissing" AutoGenerateColumns="False" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
            Font-Size="12px" ForeColor="Black" Visible="false">
            <Columns>
                <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                <asp:BoundField DataField="InstallDate" HeaderText="Install.Date" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" Width="75px"  />
                </asp:BoundField>
                <asp:BoundField DataField="Details" HeaderText="Details">
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="Resolution" HeaderText="Resolution">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Employee" HeaderText="TCS Employee">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Delivered" HeaderText="Delivered by EIS">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
            <RowStyle VerticalAlign="Top" />
        </asp:GridView>
        <asp:table id="tblDamaged" runat="server" CellSpacing="0" Width="760px" CellPadding="0" Visible="false">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" Font-Bold="True" Font-Size="12" Height="30" VerticalAlign="Top">
                    <asp:Label runat="Server" ID="lblHeader1">Damaged Product&nbsp;&nbsp;from </asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblDate11">09/01/2006</asp:Label>&nbsp;&nbsp;to&nbsp;
                    <asp:Label runat="Server" ID="lblDate21">09/16/2006</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:table>
        <asp:GridView runat="Server" ID="grdDamaged" Width="760px" 
            DataSourceID="sdsDamaged" AutoGenerateColumns="False" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
            Font-Size="12px" ForeColor="Black" Visible="false">
            <Columns>
                <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                <asp:BoundField DataField="InstallDate" HeaderText="Install.Date" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" Width="75px"  />
                </asp:BoundField>
                <asp:BoundField DataField="Details" HeaderText="Details">
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="Resolution" HeaderText="Resolution">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Employee" HeaderText="TCS Employee">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Delivered" HeaderText="Delivered by EIS">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:table id="tblFlaw" runat="server" CellSpacing="0" Width="760px" CellPadding="0" Visible="false">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" Font-Bold="True" Font-Size="12" Height="30" VerticalAlign="Top">
                    <asp:Label runat="Server" ID="lblHeader2">Design Flaw&nbsp;&nbsp;from </asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblDate12">09/01/2006</asp:Label>&nbsp;&nbsp;to&nbsp;
                    <asp:Label runat="Server" ID="lblDate22">09/16/2006</asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:table>
        <asp:GridView runat="Server" ID="grdFlaw" Width="760px" 
            DataSourceID="sdsFlaw" AutoGenerateColumns="False" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
            Font-Size="12px" ForeColor="Black" Visible="false">
            <Columns>
                <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer" >
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="InstallDate" HeaderText="Install.Date" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" Width="75px"  />
                </asp:BoundField>
                <asp:BoundField DataField="Comments" HeaderText="Details">
                    <ItemStyle HorizontalAlign="Left"/>
                </asp:BoundField>
            </Columns>
            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:Label ID="lblEmpty" runat="server" Text="No Records" Font-Bold="true" Font-Size="Large" Visible="false"></asp:Label>
        <asp:SqlDataSource runat="Server" ID="sdsMissing" />
        <asp:SqlDataSource runat="Server" ID="sdsDamaged" />
        <asp:SqlDataSource runat="Server" ID="sdsFlaw" />
        &nbsp;
    </center>
    </div>
    </form>
</body>
</html>
