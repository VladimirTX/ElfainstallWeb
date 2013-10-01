<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Accomodation.aspx.cs" Inherits="ElfaInstall.Reports.Accomodation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
<div style="font-size:22px; font-weight:bold; color:#666666; text-align:center; width:600px">Accomodation Report</div>
    <br />
    <table width="850">
        <tr>
            <td style="width: 250px;" class="td12cb" align="center">Pay Date Range:</td>
            <td style="width: 175px;" align="left">
                <telerik:RadDatePicker ID="rdpDate1" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td style="width: 175px;" align="left">
                <telerik:RadDatePicker ID="rdpDate2" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td align="center">
                <telerik:RadButton runat="server" ID="btnViewReport" Text=" View Report "
                    Width="120px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                    onclick="BtnViewReportClick">
                </telerik:RadButton>
            </td>
        </tr>
        <asp:Panel ID="pnlGrids" runat="server" Visible="false">
        <tr><td colspan="4" class="td14Bold">Service Orders</td></tr>
        <tr>
            <td colspan="4">
                <asp:GridView runat="Server" ID="grdService" Width="850px" 
                        DataSourceID="sdsService" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Arial" 
                        Font-Size="12px" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst. Date" HeaderText="Inst. Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pay Date" HeaderText="Pay Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer">
                        <ItemStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Installer" HeaderText="Installer">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" HeaderText="Reason">
                        <ItemStyle Width="325px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td colspan="4" class="td14Bold">Refunds</td></tr>
        <tr>
            <td colspan="4">
                <asp:GridView runat="Server" ID="grdRefunds" Width="850px" 
                        DataSourceID="sdsRefunds" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Arial" 
                        Font-Size="12px" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst. Date" HeaderText="Inst. Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pay Date" HeaderText="Pay Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer">
                        <ItemStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Installer" HeaderText="Installer">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" HeaderText="Reason">
                        <ItemStyle Width="325px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td colspan="4" class="td14Bold">Adjustments</td></tr>
        <tr>
            <td colspan="4">
                <asp:GridView runat="Server" ID="grdAdjustments" Width="850px" 
                        DataSourceID="sdsAdjustments" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Arial" 
                        Font-Size="12px" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst. Date" HeaderText="Inst. Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pay Date" HeaderText="Pay Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer">
                        <ItemStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Installer" HeaderText="Installer">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" HeaderText="Reason">
                        <ItemStyle Width="325px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td colspan="4" class="td14Bold">Trip to Store</td></tr>
        <tr>
            <td colspan="4">
                <asp:GridView runat="Server" ID="grdTrip" Width="850px" 
                        DataSourceID="sdsTrip" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Arial" 
                        Font-Size="12px" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst. Date" HeaderText="Inst. Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pay Date" HeaderText="Pay Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer">
                        <ItemStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Installer" HeaderText="Installer">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" HeaderText="Reason">
                        <ItemStyle Width="325px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td colspan="4" class="td14Bold">Fulfillment Orders</td></tr>
        <tr>
            <td colspan="4">
                <asp:GridView runat="Server" ID="grdFulfillment" Width="850px" 
                        DataSourceID="sdsFulfillment" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Arial" 
                        Font-Size="12px" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst. Date" HeaderText="Inst. Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pay Date" HeaderText="Pay Date">
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer">
                        <ItemStyle Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Installer" HeaderText="Installer">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" HeaderText="Reason">
                        <ItemStyle Width="325px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        </asp:Panel>
        <tr><td colspan="4">&nbsp;&nbsp;</td></tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="bntExcel" runat="server" Text="Excel Report" 
                    onclick="BntExcelClick"/>
                <br />&nbsp;
            </td>
        </tr>
    </table>
    <asp:SqlDataSource runat="Server" ID="sdsService" />
    <asp:SqlDataSource runat="Server" ID="sdsRefunds" />
    <asp:SqlDataSource runat="Server" ID="sdsAdjustments" />
    <asp:SqlDataSource runat="Server" ID="sdsTrip" />
    <asp:SqlDataSource runat="Server" ID="sdsFulFillment" />
    </center>
</asp:Content>
