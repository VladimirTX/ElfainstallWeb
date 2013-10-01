<%@ Page Title="Installation Log" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="InstallLog.aspx.cs" Inherits="ElfaInstall.InstallLog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" Width="1080px">
    <center>
        <table style=" width:950;">
            <tr>
                <td style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666"> Installation Log of completed jobs</asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlGread" Visible="true">
        <table>
            <tr>
                <td style="width: 950px; border-style:double; border-width:2px; border-color:Black;" valign="top">
                    <asp:GridView ID="grdLog" runat="server" DataSourceID="sdsLog"  AutoGenerateColumns="False" 
                        CellPadding="0" HorizontalAlign="Center" GridLines="Vertical" CssClass="gridData" 
                        ShowFooter="True" BackColor="White" AllowPaging="True" AllowSorting="True" 
                        OnPageIndexChanged="GrdLogPageIndexChanged" 
                        OnSelectedIndexChanged="GrdLogSelectedIndexChanged" 
                        OnSorted="GrdLogSorted" PageSize="16" EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="OrderID">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="OrderID" DataTextField="OrderNumb" 
                                DataNavigateUrlFormatString="OrderDetails.aspx?OrderID={0}" HeaderText="Order #" 
                                NavigateUrl="~/OrderDetails.aspx" SortExpression="OrderNumb" >
                                <ItemStyle CssClass="gridcellright" Width="70px" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="lName" HeaderText="Customer" SortExpression="lName">
                                <ItemStyle CssClass="gridcellleft" Width="70px" />
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
                            <asp:BoundField DataField="MiscPrice" HeaderText="Add Paint." 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipPrice" HeaderText="Other" 
                                DataFormatString="{0:$0.00}" HtmlEncode="False">
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parking" HeaderText="Parking" 
                                DataFormatString="{0:$0.00}" >
                                <ItemStyle CssClass="gridcellright" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PromoPrice" HeaderText="Adjust." 
                                DataFormatString="{0:$0.00}" >
                                <ItemStyle CssClass="gridcellright" Width="70px" ForeColor="#990000" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tax" HeaderText="Tax" 
                                DataFormatString="{0:$0.00}" >
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
                                    <asp:CheckBox ID="cbPayd" runat="server" AutoPostBack="true" OnCheckedChanged="cbPayd_OnCheckedChanged" Checked='<%# Eval("Payd") %>' Enabled="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PayType" HeaderText="Pay Type" 
                                SortExpression="PayType">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Add Fee" SortExpression="AddFee">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="checkBox2" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbAddFee" runat="server" Checked='<%# Eval("AddFee") %>' Enabled="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>
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
                            <asp:BoundField DataField="Promo">
                                <ItemStyle Font-Bold="True" Font-Size="Smaller" ForeColor="Red" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Options" HeaderText="Opt." 
                                SortExpression="Options" />
                            <asp:CommandField ButtonType="Button" SelectText="Edit" ShowSelectButton="True">
                                <ItemStyle Width="60px" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" HorizontalAlign="Center" Height="30px" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <AlternatingRowStyle BackColor="LightGray" />
                        <RowStyle Height="25px" />
                        <PagerStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="tdtitle" style="border-style:double; border-width:2px; border-color:Black;">
                    Selection Filter:&nbsp;&nbsp;Vendors &nbsp;
                    <asp:DropDownList ID="ddlVendors" runat="server">
                    </asp:DropDownList>
                    &nbsp; OR&nbsp;&nbsp;Order #&nbsp; 
                    <asp:TextBox ID="txtOrderNumb" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Button ID="btnFilter" runat="server" Text="Select" OnClick="BtnFilterClick" /></td>
            </tr>
        </table>
        </asp:Panel>
        <table>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlEdit" runat="server" Width="550px" BorderStyle="Solid" BorderColor="Black" BorderWidth="2" Visible="false">
                        <br />
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td colspan="2" align="center" class="tdtitle">
                                    Order # 
                                    &nbsp;&nbsp;<asp:Label runat="Server" ID="lblOrderNumb"></asp:Label><br />
                                    Installer:&nbsp;&nbsp;
                                    <asp:Label runat="Server" ID="lblVendor"></asp:Label><br /> 
                                    Customer: &nbsp;&nbsp;
                                    <asp:Label runat="Server" ID="lblCustomer"></asp:Label><br />
                                    Elfa price: &nbsp;&nbsp;
                                    <asp:Label ID="lblElfaPrice" runat="server" Text="Elfa"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="tdtitle"><hr /></td>
                            </tr>
                            <tr>
                                <td style="width:200px;"  class="tdmain">Add Demo</td>
                                <td style="width:300px;" align="left" class="tdmain">
                                    $<asp:TextBox ID="txtDemoPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Add Miles</td>
                                <td align="left" class="tdmain">
                                    $<asp:TextBox ID="txtMilesPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Add Painting</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtMiscPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Parking/Toll</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtParking" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Adjustment</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtPromoPrice" runat="server" Width="80px"></asp:TextBox>
                                    <asp:DropDownList ID="ddlReasons" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdmain">Other</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtTipPrice" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Tax</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtTax" runat="server" Width="80px"></asp:TextBox>
                                    <asp:Button ID="btnTax" runat="server" CssClass="btnsmall" 
                                        Text="Recalculate Tax" Visible="false" onclick="BtnTaxClick" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdmain">Invoice total</td>
                                <td align="left">
                                    &nbsp;
                                    <asp:Label runat="Server" ID="lblInvoice" Font-Bold="True">$0.00</asp:Label>
                                </td>
                            </tr>
<%--                            <tr>
                                <td class="tdmain">Payment Received</td>
                                <td align="left">
                                    &nbsp;
                                    <asp:CheckBox ID="chkPayment" runat="server" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="tdmain">Pay Type</td>
                                <td  align="left"  class="td12">
                                    <asp:RadioButton ID="rbSQ" runat="server" GroupName="Payment" Text="Square" /><br />
                                    &nbsp;<asp:RadioButton ID="rbCC" runat="server" GroupName="Payment" Text="Credit Card" /><br />
                                    &nbsp;<asp:RadioButton ID="rbCheck" runat="server" GroupName="Payment" Text="Check" />
                                    &nbsp;#<asp:TextBox runat="server" ID="txtChkNumb" Width="100px"></asp:TextBox><br />
                                    &nbsp;<asp:RadioButton ID="rbNC" runat="server" GroupName="Payment" Text="N/C" /><br />
                                    &nbsp;<asp:RadioButton ID="rbInv" runat="server" GroupName="Payment" Text="Invoice" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="tdmain">Vendor %</td>
                                <td>
                                    &nbsp; &nbsp;
                                    <asp:DropDownList ID="ddlProcent" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlProcentSelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Select %</asp:ListItem>
                                        <asp:ListItem Value="0.55">55 %</asp:ListItem>
                                        <asp:ListItem Value="0.60">60 %</asp:ListItem>
                                        <asp:ListItem Value="0.65">65 %</asp:ListItem>
                                        <asp:ListItem Value="0.70">70 %</asp:ListItem>
                                        <asp:ListItem Value="0.75">75 %</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdmain">Vendor Paym. Due</td>
                                <td  align="left" class="tdmain">
                                    $<asp:TextBox ID="txtVendorDue" runat="server" Width="80px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="tdmain">Vendor Paid Date</td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;
                                    <telerik:RadDatePicker ID="rdpVendorDate" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td align="center"><br /><asp:Button ID="btnSave" runat="server" Text=" Save " CssClass="btnmenu" OnClick="BtnSaveClick" Height="30px" Width="80px" />
                                    <br />&nbsp;
                                </td>
                                <td align="center"><br /><asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btnmenu" OnClick="BtnCloseClick" Height="30px" Width="80px"/>
                                    <br />&nbsp;
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td align="center" class="tdtitle">Additional Vendor for this Order</td>
                            </tr>
                        </table>
                        <asp:Panel runat="server" ID="pnlAdd1">
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlVendorAdd" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td  class="tdmain">
                                    %<asp:DropDownList ID="ddlVendProc" runat="server">
                                        <asp:ListItem Value="0"></asp:ListItem>
                                        <asp:ListItem Value="0.05">5 %</asp:ListItem>
                                        <asp:ListItem Value="0.1">10 %</asp:ListItem>
                                        <asp:ListItem Value="0.15">15 %</asp:ListItem>
                                    </asp:DropDownList>
                                    OR
                                    $<asp:TextBox runat="server" ID="txtVendAmt" Width="60px"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnVadd" runat="server" CssClass="btnmenu" Text=" Add " OnClick="BtnVaddClick" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAdd2">
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td  class="tdtitle">
                                    <asp:Label ID="lblVendorName" runat="server" Text="Vendor"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;<asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <hr />
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td align="center" class="tdtitle">Additional Organizer for this Order</td>
                            </tr>
                        </table>
                        <asp:Panel runat="server" ID="pnlAdd3">
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlOrganizerAdd" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td  class="tdmain">
                                    %<asp:DropDownList ID="ddlOrgProc" runat="server">
                                        <asp:ListItem Value="0"></asp:ListItem>
                                        <asp:ListItem Value="0.05">5 %</asp:ListItem>
                                        <asp:ListItem Value="0.1">10 %</asp:ListItem>
                                        <asp:ListItem Value="0.15">15 %</asp:ListItem>
                                    </asp:DropDownList>
                                    OR
                                    $<asp:TextBox runat="server" ID="txtOrgAmt" Width="60px"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnOadd" runat="server" CssClass="btnmenu" Text=" Add " onclick="BtnOaddClick" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAdd4">
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td  class="tdtitle">
                                    <asp:Label ID="lblOrganizerName" runat="server" Text="Organizer"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;<asp:Label ID="lblOamount" runat="server" Text="Amount"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <hr />
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td align="center" class="tdtitle">Additional Payment for <asp:Label runat="Server" ID="lblVendor1"></asp:Label></td>
                            </tr>
                        </table>
                        <asp:Panel runat="server" ID="pnlAdd5">
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td style="width:130px;" >&nbsp;&nbsp;</td>
                                <td  class="tdmain">
                                    %<asp:DropDownList ID="ddlAddpayProc" runat="server">
                                        <asp:ListItem Value="0"></asp:ListItem>
                                        <asp:ListItem Value="0.05">5 %</asp:ListItem>
                                        <asp:ListItem Value="0.1">10 %</asp:ListItem>
                                        <asp:ListItem Value="0.15">15 %</asp:ListItem>
                                    </asp:DropDownList>
                                    OR
                                    $<asp:TextBox runat="server" ID="txtAddpayAmt" Width="60px"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnPadd" runat="server" CssClass="btnmenu" Text=" Add " 
                                        onclick="BtnPaddClick" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAdd6">
                        <table style="width:550px;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td  class="tdtitle">
                                    <asp:Label ID="lblLabel1" runat="server" Text="Additional Amount:"></asp:Label>
                                    &nbsp; &nbsp; &nbsp;<asp:Label ID="lblAddAmount" runat="server" Text="Amount"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                        &nbsp;
                    </asp:Panel>
                </td>
            </tr>
        </table>
<%--        <table style="width: 900px;" cellpadding="0" cellspacing="2">
            <tr>
                <td align="center">
                    <asp:Button ID="btnReport" runat="server" CssClass="btnmenu" Text="Non-payment orders" PostBackUrl="~/Reports/Non_payment.aspx" />
                </td>
            </tr>
        </table>--%>
        &nbsp;
        <asp:SqlDataSource runat="Server" ID="sdsLog" />
    </center>
</asp:Panel>
</asp:Content>
