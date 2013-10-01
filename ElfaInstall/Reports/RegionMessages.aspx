<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="RegionMessages.aspx.cs" Inherits="ElfaInstall.Reports.RegionMessages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:800;">
            <tr>
                <td class="tdheadersmall" style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader">Region Open Orders</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 800px" valign="top">
                    <asp:GridView runat="Server" ID="grdMessages" Width="800px" 
                        DataSourceID="sdsMessages" AutoGenerateColumns="False" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Arial" 
                        Font-Size="12px" ForeColor="Black" EnableModelValidation="True" 
                        onrowdatabound="GrdMessagesRowDataBound" >
                        <Columns>
                            <asp:BoundField DataField="RegionName" HeaderText="Region" >
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" >
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ByPhone" HeaderText="Confirmed by Phone" >
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BySurvey" HeaderText="Confirmed by Survey">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnApproved" HeaderText="Unconfirmed" >
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NoContact" HeaderText="No Contact" >
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnScheduled" HeaderText="Unscheduled" >
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField>
                            <HeaderStyle BackColor="#CCCCCC" />
                            <ItemStyle BackColor="#CCCCCC" Width="2px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status1" HeaderText="Backorders">
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status2" HeaderText="Under Construction">
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status3" HeaderText="Customer Rescheduling">
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <RowStyle VerticalAlign="Top" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Total # of Orders in Report: 
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    Total Installation Fee:
                    <asp:Label ID="lblInvoice" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    Total Retail Amount of Open Orders:
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
        </table>
        <asp:SqlDataSource runat="Server" ID="sdsMessages" />
    </center>
</asp:Panel>
</asp:Content>
