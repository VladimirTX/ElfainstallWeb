<%@ Page Title="Installation Log" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="VendorInstall.aspx.cs" Inherits="ElfaInstall.VendorInstall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:960;">
            <tr>
                <td style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666"> Installation Log of completed jobs</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 960px; border-style:double; border-width:2px; border-color:Black;" valign="top">
                    <asp:GridView ID="grdLog" runat="server" DataSourceID="sdsLog"  AutoGenerateColumns="False" 
                        CellPadding="0" HorizontalAlign="Center" GridLines="Vertical" CssClass="gridData" 
                        ShowFooter="True" BackColor="White" AllowPaging="True" AllowSorting="True" 
                        OnPageIndexChanged="GrdLogPageIndexChanged" 
                        OnSelectedIndexChanged="GrdLogSelectedIndexChanged" 
                        OnSorted="GrdLogSorted" PageSize="16" Font-Size="Small" 
                        EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="OrderID" HeaderText="ID">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="OrderID,Installed" DataTextField="OrderNumb" 
                                DataNavigateUrlFormatString="Vendor_Order.aspx?OrderID={0}&Installed={1}" HeaderText="Order #" 
                                NavigateUrl="~/Vendor_Order.aspx" SortExpression="OrderNumb" >
                                <ItemStyle CssClass="gridcellright" Width="70px" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer">
                                <ItemStyle CssClass="gridcellleft" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                                DataFormatString="{0:MM/dd/yy}" HtmlEncode="False" SortExpression="InstallDate">
                                <ItemStyle CssClass="gridcellleft" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BaseInstallPrice" HeaderText="Install $" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeliveryPrice" HeaderText="Delivery $" 
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
                            <asp:TemplateField HeaderText="Ins. Deduct">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="checkBox4" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbDeduct" runat="server" Checked='<%# Eval("Deduct") %>' Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="VendorDue" HeaderText="Vendor $ Due" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VendorDate" HeaderText="Vendor Paid Date" 
                                DataFormatString="{0:MM/dd/yy}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellleft" Width="80px" />
                            </asp:BoundField>
<%--                            <asp:CommandField ButtonType="Button" SelectText="Edit" ShowSelectButton="True">
                                <ItemStyle Width="60px" />
                            </asp:CommandField>--%>
                            <asp:BoundField DataField="Promo">
                                <ItemStyle Font-Bold="True" ForeColor="Red" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Options" />
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
                <td align="center">
                    <asp:Panel ID="pnlEdit" runat="server" Width="400px" BorderStyle="Solid" BorderColor="Black" BorderWidth="2" Visible="false">
                        <br />
                        <table style="width:400px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td colspan="2" align="center" class="tdtitle">
                                    Customer: &nbsp;&nbsp;
                                    <asp:Label runat="Server" ID="lblCustomer"></asp:Label><br />
                                    Phone:&nbsp;&nbsp;
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label><br />
                                    Install.Date:&nbsp;&nbsp;
                                    <asp:Label ID="lblInstallDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="tdtitle" style="height: 18px"><hr /></td>
                            </tr>
                            <tr>
                                <td style="width:200px;"  class="tdmain">Add Demo</td>
                                <td style="width:200px;" align="left" class="tdmain">
                                    $<asp:TextBox ID="txtDemoPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Delivery Price</td>
                                <td align="left" class="tdmain">
                                    $<asp:TextBox ID="txtDelivery" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Add Miles</td>
                                <td align="left" class="tdmain">
                                    $<asp:TextBox ID="txtMilesPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Misc</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtMiscPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Tip</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtTipPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Invoice total</td>
                                <td align="left">
                                    &nbsp;
                                    <asp:Label runat="Server" ID="lblInvoice" Font-Bold="True">$0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdmain">Pay Type</td>
                                <td  align="left"  class="td12">
                                    <asp:RadioButton ID="rbSQ" runat="server" GroupName="Payment" Text="Square" /><br />
                                    &nbsp;<asp:RadioButton ID="rbCC" runat="server" GroupName="Payment" Text="Credit Card" /><br />
                                    &nbsp;<asp:RadioButton ID="rbCheck" runat="server" GroupName="Payment" Text="Check" /><br />
                                    &nbsp;<asp:RadioButton ID="rbNC" runat="server" GroupName="Payment" Text="N/C" /><br />
                                    &nbsp;<asp:RadioButton ID="rbInv" runat="server" GroupName="Payment" Text="Invoice" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdmain">Vendor Paym. Due</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtVendorDue" runat="server" Width="80px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="center"><br /><asp:Button ID="btnSave" runat="server" Text=" Save " 
                                        CssClass="btnsubmit" OnClick="BtnSaveClick" Width="80px" />
                                    <br />&nbsp;
                                </td>
                                <td align="center"><br /><asp:Button ID="btnClose" runat="server" Text="Close" 
                                        CssClass="btnmenu" OnClick="BtnCloseClick" Width="80px"/>
                                    <br />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:SqlDataSource runat="Server" ID="sdsLog" />
        <asp:HiddenField ID="hfVendorID" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
