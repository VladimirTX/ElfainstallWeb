<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrganizerPay.aspx.cs" Inherits="ElfaInstall.OrganizerPay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" Width="1080px">
    <center>
        <table style=" width:1000px">
            <tr>
                <td style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666">Completed Orders Log</asp:Label>
                </td>
            </tr>
            <tr>
                    <td style="width: 1000px; border-style:double; border-width:2px; border-color:Black;" valign="top">
                        <asp:GridView ID="grdLog" runat="server" DataSourceID="sdsLog"  AutoGenerateColumns="False"  
                            Width="1000px" CellPadding="0" HorizontalAlign="Center" 
                            GridLines="Vertical" CssClass="gridData" 
                            ShowFooter="True" BackColor="White" AllowPaging="True" AllowSorting="True" 
                            EnableModelValidation="True" onpageindexchanged="GrdLogPageIndexChanged" 
                            onrowdatabound="GrdOrdersRowDataBound" 
                            onsorted="GrdLogSorted"  PageSize="16">
                            <Columns>
                                <asp:BoundField DataField="OrderID">
                                    <ItemStyle HorizontalAlign="Center" Width="2px" Font-Size="1pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="viz">
                                    <ItemStyle HorizontalAlign="Center" Width="2px" Font-Size="1pt" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="OrderID" DataTextField="OrderNumb" 
                                    DataNavigateUrlFormatString="OrderDetails.aspx?OrderID={0}" HeaderText="Order #" 
                                    NavigateUrl="~/OrderDetails.aspx" SortExpression="OrderNumb" >
                                    <ItemStyle CssClass="gridcellright" Width="70px" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" 
                                    DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="OrderDate">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                                    DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="InstallDate">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer">
                                    <ItemStyle CssClass="gridcellleft" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Organizer" HeaderText="Organizer" SortExpression="Organizer">
                                    <ItemStyle CssClass="gridcellleft" Width="90px" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="Fees" HeaderText="Fees" 
                                    DataFormatString="{0:$0.00}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Other" HeaderText="Other" 
                                    DataFormatString="{0:$0.00}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Adjustment" HeaderText="Adjust." 
                                    DataFormatString="{0:$0.00}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Total" HeaderText="Invoice Amt." 
                                    DataFormatString="{0:$0.00}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Ins. Deduct">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="checkBox4" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbDeduct" runat="server" Checked='<%# Eval("Deduct") %>' Enabled="false"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PayType" HeaderText="Pay Type" 
                                    SortExpression="PayType">
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Payment Received">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="checkBox1" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbPayd" runat="server" AutoPostBack="true" OnCheckedChanged="cbPayd_OnCheckedChanged" Checked='<%# Eval("Payd") %>' Enabled="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="VendorDue" HeaderText="Vendor $ Due" 
                                    DataFormatString="{0:$0.00}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VendorDate" HeaderText="Vendor Paid Date" 
                                    DataFormatString="{0:MM/dd/yy}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellleft" Width="60px" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="Comments" HeaderText="Invoice Notes">
                                    <ItemStyle Width="400px" />
                                </asp:BoundField>--%>
                            </Columns>
                            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" HorizontalAlign="Center" Height="30px" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <AlternatingRowStyle BackColor="LightGray" />
                            <RowStyle Height="25px" />
                            <PagerStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
        </table>
        <asp:SqlDataSource runat="server" ID="sdsLog" />
        <asp:HiddenField runat="server" ID="hfOrganizerID" />
    </center>
    </asp:Panel>
</asp:Content>
