<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Adjustments.aspx.cs" Inherits="ElfaInstall.Reports.Adjustments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:900;">
            <tr>
                <td class="tdheadersmall" style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader">Adjustments</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 900px" valign="top">
                    <asp:GridView runat="Server" ID="grdMessages" Width="800px" 
                        DataSourceID="sdsAdjustments" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="5" Font-Names="Arial" 
                        Font-Size="12px" ForeColor="Black" EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:d}">
                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Invoice" HeaderText="Invoice" 
                                DataFormatString="{0:$0,000.00}" >
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Adjustment" HeaderText="Adjustment" 
                                DataFormatString="{0:$0,000.00}" >
                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DiscountReason" HeaderText="Adjustment Reason">
                            <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InstallDate" HeaderText="Install.Date" DataFormatString="{0:d}">
                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VendorName" HeaderText="Vendor Name">
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <RowStyle VerticalAlign="Top" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource runat="Server" ID="sdsAdjustments" />
    </center>
</asp:Panel>
</asp:Content>
