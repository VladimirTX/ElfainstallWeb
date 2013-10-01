<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenOrders.ascx.cs" Inherits="ElfaInstall.Reports.OpenOrders" %>
<div style=" page-break-after:always; text-align:center;">
    <asp:DataGrid ID="dgrOrders" runat="Server"   
         AutoGenerateColumns="False" Width="860px" 
         CellPadding="3" BackColor="White" BorderColor="Black" Font-Bold="True" 
         BorderWidth="1px" 
        CellSpacing="1" HorizontalAlign="Center" Font-Size="8pt">
            <Columns>
                <asp:BoundColumn DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:d}" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="55px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OrderNumb" HeaderText="Order #">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Customer" HeaderText="Customer">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="StoreName" HeaderText="Store">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MarketName" HeaderText="Market">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Vendor" HeaderText="Vendor">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PurchasePrice" DataFormatString="{0:c}" HeaderText="elfa Amount">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Actual" DataFormatString="{0:c}" HeaderText="Sale Amount">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OrderPrice" DataFormatString="{0:c}" HeaderText="Inv. Amount">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="InstallDate" HeaderText="Ins. Schedule" DataFormatString="{0:d}" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="55px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Days" HeaderText="# of days open">
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                    <HeaderStyle Width="20px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Delivery" HeaderText="Delivery">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="White" Font-Bold="True" />
            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Left" Font-Size="8pt" />
        </asp:DataGrid>
        <span style="width:760px; text-align:right">Page: 
            <asp:Label runat="Server" ID="lblPage">3</asp:Label>
        </span>
</div>