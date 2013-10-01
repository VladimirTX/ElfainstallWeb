<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedbackReport.aspx.cs" Inherits="ElfaInstall.Reports.FeedbackReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Installer Feedback Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:10px;">
    <center>
        <span style="font-size: 14pt; height:50pt"><strong>Installer Feedback Report</strong></span>
        <asp:GridView runat="Server" ID="grdFeedback" Width="820px" 
            DataSourceID="sdsReport" AutoGenerateColumns="False" BorderColor="Black" 
            BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
            Font-Size="12px" ForeColor="Black" AllowPaging="True" PageSize="20" 
            AllowSorting="True" EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="OrderNumb" HeaderText="Order #" />
                <asp:BoundField DataField="InstallDate" DataFormatString="{0:d}" 
                    HeaderText="Inst. Date" HtmlEncode="False" SortExpression="InstallDate" />
                <asp:BoundField DataField="VendorName" HeaderText="Installer" 
                    SortExpression="VendorName" />
                <asp:BoundField DataField="PurchasePrice" DataFormatString="{0:c}" HeaderText="Product Amt." HtmlEncode="False" />
                <asp:BoundField DataField="OrderPrice" DataFormatString="{0:c}" HeaderText="Inst. Price" HtmlEncode="False" />
                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                <asp:BoundField DataField="Installation" HeaderText="Installation" />
                <asp:BoundField DataField="Demolition" HeaderText="Demolition" />
                <asp:BoundField DataField="IfMissing" HeaderText="Missing Product" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IfDamaged" HeaderText="Damaged Product" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="txtComments" HeaderText="Installer Comments" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            
            
            </asp:GridView>
        <asp:SqlDataSource runat="Server" ID="sdsReport" />
    </center>
    </div>
    </form>
</body>
</html>
