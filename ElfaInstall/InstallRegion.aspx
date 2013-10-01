<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="InstallRegion.aspx.cs" Inherits="ElfaInstall.InstallRegion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain" Width="990px">
    <center>
        <table style=" width:950;">
            <tr>
                <td style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666"> Installation Log of completed jobs</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 950px; border-style:double; border-width:2px; border-color:Black;" valign="top">
                    <asp:GridView ID="grdLog" runat="server" DataSourceID="sdsLog"  AutoGenerateColumns="False" 
                        CellPadding="0" HorizontalAlign="Center" GridLines="Vertical" CssClass="gridData" 
                        ShowFooter="True" BackColor="White" AllowPaging="True" AllowSorting="True" 
                        OnPageIndexChanged="grdLog_PageIndexChanged" 
                        OnSorted="grdLog_Sorted" PageSize="16" Font-Size="Small" 
                        EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="OrderID" HeaderText="ID">
                                <ItemStyle HorizontalAlign="Center" Font-Size="XX-Small" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="OrderID" DataTextField="OrderNumb" 
                                DataNavigateUrlFormatString="OrderDetails.aspx?OrderID={0}" HeaderText="Order #" 
                                NavigateUrl="~/OrderDetails.aspx" SortExpression="OrderNumb" >
                                <ItemStyle CssClass="gridcellright" Width="70px" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="VendorName" HeaderText="Installer" SortExpression="VendorName">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lName" HeaderText="Customer" SortExpression="lName">
                                <ItemStyle CssClass="gridcellleft" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                                DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="InstallDate">
                                <ItemStyle CssClass="gridcellleft" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BaseInstallPrice" HeaderText="Install" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeliveryPrice" HeaderText="Delivery" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DemoPrice" HeaderText="Add Demo" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MilesPrice" HeaderText="Add Miles" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MiscPrice" HeaderText="Add Paint" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipPrice" HeaderText="Other" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parking" HeaderText="Parking" DataFormatString="{0:$0.00}" >
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tax" HeaderText="Tax" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderPrice" HeaderText="Total Invoice" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Payment Received">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="checkBox1" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbPayd" runat="server" Checked='<%# Eval("Payd") %>' Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PayType" HeaderText="Pay Type">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Promo" HeaderText="Event" SortExpression="Promo">
                                <ItemStyle Font-Bold="True" Font-Size="Smaller" ForeColor="Red" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Options" HeaderText="Opt." SortExpression="Options">
                            <ItemStyle Width="30px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" HorizontalAlign="Center" Height="30px" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <AlternatingRowStyle BackColor="#BFC7A4" />
                        <RowStyle Height="25px" />
                        <PagerStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="font-size:smaller"  align="center">
                    Search by Order # 
                    <asp:TextBox runat="server" ID="txtSearch" Width="120px"></asp:TextBox>
                    &nbsp;&nbsp;OR&nbsp;&nbsp;Vendor Name&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlVendors" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnSearch" Text="Search" 
                        onclick="BtnSearchClick" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource runat="Server" ID="sdsLog" />
        <asp:HiddenField ID="hfRegionID" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
