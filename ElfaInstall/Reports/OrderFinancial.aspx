<%@ Page Title="Financial Report" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrderFinancial.aspx.cs" Inherits="ElfaInstall.Reports.OrderFinancial" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="font-size:22px; font-weight:bold; color:#666666; text-align:center; width:1000px">Orders Financial Detais</div>
    <br />
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
<center>
    <table width="530">
        <tr>
            <td style="width: 280px;" class="tdmain">Installation Date Range:</td>
            <td style="width: 125px;" align="left">
                <telerik:RadDatePicker ID="rdpDate1" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td style="width: 125px;" align="left">
                <telerik:RadDatePicker ID="rdpDate2" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="tdmain">Order Status:</td>
            <td colspan="2" align="left">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Selected="True">[All]</asp:ListItem>
                    <asp:ListItem>Opened</asp:ListItem>
                    <asp:ListItem>Installed</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain">By State:</td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlStates" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain">By Store:</td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlStores" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain">By Installers:</td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlVendors" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain">By Order Type:</td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlOrderType" runat="server">
                    <asp:ListItem>[All]</asp:ListItem>
                    <asp:ListItem>Additional</asp:ListItem>
                    <asp:ListItem>At Home</asp:ListItem>
                    <asp:ListItem>Business to Business</asp:ListItem>
                    <asp:ListItem>Call In</asp:ListItem>
<%--                    <asp:ListItem>Commercial</asp:ListItem>--%>
                    <asp:ListItem>elfa Preferred Customer</asp:ListItem>
                    <asp:ListItem>Ellen DeGeneres Show</asp:ListItem>
                    <asp:ListItem>Fall for elfa</asp:ListItem>
                    <asp:ListItem>Just for You</asp:ListItem>
                    <asp:ListItem>Man in Desert</asp:ListItem>
                    <asp:ListItem>PR</asp:ListItem>
                    <asp:ListItem>Service</asp:ListItem>
                    <asp:ListItem>Store Comp</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain">By Adjustment Type:</td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlReasons" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdmain" colspan="2">Customer First OR Last Name:</td>
            <td align="left">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Button ID="btnShowReport" runat="server" CssClass="btnmenu" 
                    Text=" Continue " onclick="BtnShowReportClick" />
            </td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
    </table>
</center>
</asp:Panel>
<asp:Panel ID="pnlReport" runat="server" Width="1100px" Visible="false">
    <asp:GridView ID="grdOrders" runat="server" Width="1100px" 
                    AutoGenerateColumns="False" DataSourceID="sdsOrders" 
                    EmptyDataText="No records for your selection" EnableModelValidation="True" 
                    CellPadding="3" Font-Size="Smaller">
        <Columns> 
            <asp:BoundField DataField="OrderDate" DataFormatString="{0:MM/dd/yy}" 
                HeaderText="Order Date">
            <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="OrderNumb" HeaderText="Order #">
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="InstallDate" DataFormatString="{0:MM/dd/yy}" 
                HeaderText="Install Date">
            <ItemStyle HorizontalAlign="Right" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="Customer" HeaderText="Customer">
            <ItemStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="VendorName" HeaderText="Installer">
            <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="PurchasePrice" DataFormatString="{0:$0.00}" 
                HeaderText="Purchase Price">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="BaseInstallPrice" DataFormatString="{0:$0.00}" 
                HeaderText="Install Price">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="DeliveryPrice" DataFormatString="{0:$0.00}" 
                HeaderText="Delivery Price">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="DemoPrice" DataFormatString="{0:$0.00}" 
                HeaderText="Add Demo">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="MilesPrice" DataFormatString="{0:$0.00}" 
                HeaderText="Add Miles">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="PaintPrice" DataFormatString="{0:$0.00}" 
                HeaderText="Add Paint.">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="OtherPrice" DataFormatString="{0:$0.00}" 
                HeaderText="Other">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="Parking" DataFormatString="{0:$0.00}" 
                HeaderText="Parking">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="Adjustment" DataFormatString="{0:$0.00}" 
                HeaderText="Adjust.">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="Tax" DataFormatString="{0:$0.00}" 
                HeaderText="Tax">
            <ItemStyle HorizontalAlign="Right" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="Exempt" HeaderText="Tax Exempt">
            </asp:BoundField>
            <asp:BoundField DataField="Invoice" DataFormatString="{0:$0.00}" 
                HeaderText="Invoice">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="Installed" HeaderText="Installed">
            <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="PaymentDate" DataFormatString="{0:MM/dd/yy}" 
                HeaderText="Paym. Date">
            <ItemStyle HorizontalAlign="Right" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="PayType" HeaderText="Paym. Type">
            <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="VendorDue" DataFormatString="{0:$0.00}" 
                HeaderText="Vandor Due">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="VendorDate" DataFormatString="{0:MM/dd/yy}" 
                HeaderText="Vendor Pay">
            <ItemStyle HorizontalAlign="Right" Width="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="DiscountReason" HeaderText="Adjustment Type">
            <ItemStyle Width="120px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" />
    </asp:GridView>
    <center>
        <br />
<%--    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Reports/OrderFinExcel.aspx" Font-Bold="True">View in Excel</asp:HyperLink>--%>
        <asp:Button ID="bntExcel" runat="server" Text="View in Excel" PostBackUrl="~/Reports/OrderFinExcel.aspx" />
        <br />&nbsp;
    </center>
    <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    <asp:RequiredFieldValidator ID="valDate1" runat="server" ControlToValidate="rdpDate1" 
        ErrorMessage="Start Date is Missing" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valDate2" runat="server" ControlToValidate="rdpDate2" 
        ErrorMessage="End Date is Missing" Display="None"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    <asp:HiddenField ID="hfParam" runat="server" />
</asp:Panel>
</asp:Content>
