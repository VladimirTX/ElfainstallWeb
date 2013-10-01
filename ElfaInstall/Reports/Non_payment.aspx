<%@ Page Title="Non-payment Report" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Non_payment.aspx.cs" Inherits="ElfaInstall.Reports.Non_payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:760;">
        <tr>
            <td class="tdheader" style="height: 20px; text-align:center" align="center">
                <asp:Label runat="server" ID="lblHeader">Non-payment orders</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 760px" valign="top">
                <asp:GridView runat="Server" ID="grdOrders" Width="760px"
                    DataSourceID="sdsOrders" AutoGenerateColumns="False" 
                    CellPadding="0" HorizontalAlign="Center" 
                    GridLines="Vertical" CssClass="gridData" ShowFooter="True" BackColor="White" 
                    AllowPaging="True" PageSize="15">
                    <Columns>
                        <asp:HyperLinkField DataTextField="OrderNumb" HeaderText="Order #" 
                            DataNavigateUrlFields="OrderID" NavigateUrl="../OrderDetails.aspx"  
                            DataNavigateUrlFormatString="../OrderDetails.aspx?OrderID={0}" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer Name" />
                        <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                            DataFormatString="{0:MM/dd/yy}" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrderPrice" HeaderText="Total Due" 
                            DataFormatString="{0:$0.00}" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VendorName" HeaderText="Vendor" />
                    </Columns>
                    <HeaderStyle BackColor="#C4C2C2" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="#BFC7A4" />
                    <RowStyle Height="25px" />
                    <PagerStyle HorizontalAlign="Center" />
                </asp:GridView>
                <asp:SqlDataSource runat="Server" ID="sdsOrders" />
                <center><br />
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/Reports/NonPayments.aspx" Font-Bold="True">View in Excel</asp:HyperLink>
                    <br />&nbsp;
                </center>
            </td>
        </tr>
    </table>
    </center>
</asp:Panel>
</asp:Content>
