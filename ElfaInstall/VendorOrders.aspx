<%@ Page Title="Open Orders" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="VendorOrders.aspx.cs" Inherits="ElfaInstall.VendorOrders" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="msgBox" TagPrefix="cc1" Namespace="BunnyBear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:960;">
        <tr>
            <td style="height: 20px; text-align:center">
                <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 960px;" valign="top">
                <asp:GridView runat="Server" ID="grdOrders" Width="960px" 
                    DataSourceID="sdsOrders" AutoGenerateColumns="False" 
                    CellPadding="0" HorizontalAlign="Center" 
                    GridLines="Vertical" CssClass="gridData" ShowFooter="True" BackColor="White" 
                    AllowPaging="True" AllowSorting="True" OnPageIndexChanged="GrdOrdersPageIndexChanged" 
                    OnSorted="GrdOrdersSorted" PageSize="20" 
                    OnSelectedIndexChanged="GrdOrdersSelectedIndexChanged" Font-Size="Small" 
                    onrowdatabound="GrdOrdersRowDataBound" EnableModelValidation="True" 
                    BorderStyle="Double" BorderWidth="2px">
                    <Columns>
                        <asp:BoundField DataField="OrderID">
                            <ItemStyle HorizontalAlign="Center" Width="5px" Font-Size="1px" />
                            <HeaderStyle Width="5px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="OrderID,Installed" HeaderText="Order #" 
                            NavigateUrl="~/Vendor_Order.aspx" DataTextField="OrderNumb" 
                            DataNavigateUrlFormatString="Vendor_Order.aspx?OrderID={0}&Installed={1}">
                            <ItemStyle Width="70px" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date"  
                            DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" SortExpression="OrderDate" >
                            <ItemStyle CssClass="gridcellcenter" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreCode" HeaderText="Store" 
                            SortExpression="StoreCode" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer">
                            <ItemStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Delivery" Visible="True" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hPhone" HeaderText="Home Phone">
                                <ItemStyle Width="110px" />
                            </asp:BoundField>
                        <asp:TemplateField HeaderText="Calls">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBox1" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbCall1" runat="server" Checked='<%# Eval("Call1") %>' Enabled="False" Width="8pt" />
                                <asp:CheckBox ID="cbCall2" runat="server" Checked='<%# Eval("Call2") %>' Enabled="False" Width="8pt" />
                                <asp:CheckBox ID="cbCall3" runat="server" Checked='<%# Eval("Call3") %>' Enabled="False" Width="8pt" />
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conf. by Phone">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBox2" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbPhone" runat="server" Checked='<%# Eval("ByPhone") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conf. by Survey">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBox3" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSurvey" runat="server" Checked='<%# Eval("BySurvey") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
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
                        <asp:BoundField DataField="InstallDate" HeaderText="Install. Date" 
                            DataFormatString="{0:d}" HtmlEncode="False"  SortExpression="InstallDate">
                            <ItemStyle CssClass="gridcellcenter" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InstallTime" HeaderText="Install. Time"  DataFormatString="{0:t}&nbsp;">
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" CausesValidation="False" ShowSelectButton="True" ShowCancelButton="False" SelectText="Edit" />
                        <asp:BoundField DataField="Promo">
                            <ItemStyle Font-Bold="True" Font-Size="Small" ForeColor="Red" HorizontalAlign="Left" />
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
                <asp:Label runat="server" ID="lblDelivComm" style=" font-size:smaller">
                  * - Bold blue rows - orders with delivery.  
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlEdit" runat="server" Width="500px" BorderStyle="Solid" BorderColor="Black" BorderWidth="2" Visible="false">
                    <table style="width:500px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width:200px;" class="tdmain">Customer:</td>
                            <td style="width:300px;" class="tdmain">
                                <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblPhone" runat="server" Text="Phone #"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                        </tr>
<%--                        <tr>
                            <td style="width:200px;" class="tdmain">Phone:</td>
                            <td style="width:250px;" class="tdmain">
                                <asp:Label ID="lblPhone" runat="server" Text="Phone #"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdmain">Installation Charge</td>
                            <td align="left">
                                &nbsp;
                                <asp:Label runat="Server" ID="lblBase" Font-Bold="True">$0.00</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdmain">Delivery Option</td>
                            <td align="left">
                                &nbsp;
                                <asp:Label runat="Server" ID="lblDelivery" Font-Bold="True">$0.00</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px;"  class="tdmain">Add Demo</td>
                            <td style="width:200px;" align="left" class="tdmain">
                                $<asp:TextBox ID="txtDemoPrice" runat="server" Width="80px"></asp:TextBox></td>
                        </tr>
<%--                            <tr>
                            <td class="tdmain">Delivery Price</td>
                            <td align="left" class="tdmain">
                                $<asp:TextBox ID="txtDelivery" runat="server" Width="80px"></asp:TextBox></td>
                        </tr>--%>
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
                            <td class="tdmain">Other</td>
                            <td  align="left" class="tdmain">
                                $<asp:TextBox ID="txtTipPrice" runat="server" Width="80px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdmain">Tax</td>
                            <td  align="left" class="tdmain">
                                $<asp:TextBox ID="txtTax" runat="server" Width="80px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdmain">Invoice total</td>
                            <td align="left">
                                &nbsp;
                                <asp:Label runat="Server" ID="lblInvoice" Font-Bold="True">$0.00</asp:Label> 
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnUpdate" CssClass="btnsubmit" Text="Update" 
                                    onclick="BtnUpdateClick" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                        </tr>
                        <tr><td class="gridDataC" colspan="2" align="center">Call Log</td></tr>
                        <tr>
                            <td colspan="2">
                                <table style="width:500px;" cellpadding="0" cellspacing="0">
                                    <asp:Panel runat="Server" ID="pnlCall1" Visible="true">
                                        <tr>
                                            <td style="width:150px;" class="tdmain">Call #1</td>
                                            <td style="width:300px;">
                                                <asp:DropDownList ID="ddlCall1" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Busy</asp:ListItem>
                                                    <asp:ListItem>Mess. left</asp:ListItem>
                                                    <asp:ListItem>Wrong #</asp:ListItem>
                                                    <asp:ListItem>Emailed</asp:ListItem>
                                                </asp:DropDownList>
                                                <telerik:RadDateTimePicker ID="rdpDate1" runat="server" Width="200px">
                                                </telerik:RadDateTimePicker>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="Server" ID="pnlCall2" Visible="false">
                                        <tr>
                                            <td style="width:150px;" class="tdmain">Call #2</td>
                                            <td style="width:300px;">
                                                <asp:DropDownList ID="ddlCall2" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Busy</asp:ListItem>
                                                    <asp:ListItem>Mess. left</asp:ListItem>
                                                    <asp:ListItem>Wrong #</asp:ListItem>
                                                    <asp:ListItem>Emailed</asp:ListItem>
                                                </asp:DropDownList>
                                                <telerik:RadDateTimePicker ID="rdpDate2" runat="server" Width="200px">
                                                </telerik:RadDateTimePicker>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="Server" ID="pnlCall3" Visible="false">
                                        <tr>
                                            <td style="width:150px;" class="tdmain">Call #3</td>
                                            <td style="width:300px;">
                                                <asp:DropDownList ID="ddlCall3" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Busy</asp:ListItem>
                                                    <asp:ListItem>Mess. left</asp:ListItem>
                                                    <asp:ListItem>Wrong #</asp:ListItem>
                                                    <asp:ListItem>Emailed</asp:ListItem>
                                                </asp:DropDownList>
                                                <telerik:RadDateTimePicker ID="rdpDate3" runat="server" Width="200px">
                                                </telerik:RadDateTimePicker>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                        </tr>
                        <asp:Panel runat="server" ID="pnlInstallation" Visible="true">
                        <tr>
                            <td class="tdmain">Install. Date</td>
                            <td align="left">
                                <telerik:RadDatePicker ID="rdpInstDate" runat="server" 
                                    onselecteddatechanged="RdpInstDateSelectedDateChanged">
                                </telerik:RadDatePicker>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReset" runat="server" CssClass="btnsmall" Text="Reset" Visible="False" OnClick="BtnResetClick" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdmain">Install. Time</td>
                            <td class="tdmain" align="left" style="width: 250px">
                                From:<telerik:RadTimePicker ID="rtpStart" Runat="server" Width="80px" 
                                    TimeView-EndTime="19:00:00" TimeView-StartTime="08:00:00" 
                                    TimeView-Interval="02:00:00" 
                                    onselecteddatechanged="RtpStartSelectedDateChanged">
                                </telerik:RadTimePicker>
                                To:<telerik:RadTimePicker ID="rtpEnd" Runat="server" Width="80px" 
                                    TimeView-EndTime="21:00:00" TimeView-StartTime="10:00:00" 
                                    TimeView-Interval="02:00:00" onselecteddatechanged="RtpEndSelectedDateChanged">
                                </telerik:RadTimePicker>
                            </td>
                        </tr>
                        <tr><td colspan="2"><hr /></td></tr>
                        </asp:Panel>
                        <tr>
                            <td class="tdmain">Date confirmed</td>
                            <td align="left">
                                <asp:CheckBox ID="cbConfirmed" runat="server" Text=" " />
                                <asp:Label runat="Server" ID="lblConfirmed" Font-Size="X-Small">
                                    (By phone)
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="cbBySurvey" runat="server" Text=" " />
                                <asp:Label runat="Server" ID="lblBySurvey" Font-Size="X-Small">
                                    (By survey)
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  class="tdmain">
                                <asp:Label runat="server" ID="lblPickedUp" Visible="false">Product Picked-Up</asp:Label>
                            </td>
                            <td align="left" style="width: 250px">
                                <asp:CheckBox ID="cbPickedup" runat="server" Text=" " Visible="false"/></td>
                        </tr>
                        <tr>
                            <td class="tdmain">Installation Complete</td>
                            <td align="left" valign="top" style="width: 250px">
                                <asp:CheckBox ID="cbCompleted" runat="server" Text=" " />
                                <asp:Label runat="Server" ID="lblWarning" Font-Size="X-Small" Visible="False" Width="190px">
                                    Before checking this box make sure installation date is correct
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdmain">
                                <asp:Label runat="Server" ID="lblInstalledBy" Visible="False">Installed By:</asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:DropDownList ID="ddlInstalledBy" runat="server" Visible="false">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="tdmain">Pay Type</td>
                            <td  align="left"  class="td12">
                                <asp:RadioButton ID="rbSQ" runat="server" GroupName="Payment" Text="Square" /><br />
                                &nbsp;<asp:RadioButton ID="rbCC" runat="server" GroupName="Payment" Text="Credit Card" /><br />
                                &nbsp;<asp:RadioButton ID="rbCheck" runat="server" GroupName="Payment" Text="Check" />&nbsp;
                                #&nbsp;<asp:TextBox runat="server" ID="txtCheckNumb" MaxLength="15" Width="100px"></asp:TextBox><br />
                                &nbsp;<asp:RadioButton ID="rbNC" runat="server" GroupName="Payment" Text="N/C" /><br />
                                &nbsp;<asp:RadioButton ID="rbInv" runat="server" GroupName="Payment" Text="Invoice" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"   class="tdmain">
                                Comments about installation:<br />
                                <asp:TextBox ID="txtComments" runat="server" Width="490" TextMode="MultiLine" 
                                    Rows="3" MaxLength="1500" ontextchanged="txtComments_TextChanged"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2"   class="tdmain">
                                Comments to accounting:<br />
                                <asp:TextBox ID="txtAccounting" runat="server" Width="490" TextMode="MultiLine" 
                                    Rows="2" MaxLength="500"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center"><br /><asp:Button ID="btnSave" runat="server" Text=" Save " 
                                    CssClass="btnsubmit" OnClick="BtnSaveClick" Width="80px" />
                            <br />&nbsp;
                        </td>
                            <td align="center" style="width: 250px"><br /><asp:Button ID="btnClose" 
                                    runat="server" Text="Cancel" CssClass="btnmenu" OnClick="BtnCloseClick" 
                                    Width="80px"/>
                            <br />&nbsp;
                        </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource runat="Server" ID="sdsOrders" />
    <asp:HiddenField ID="hfMembers" runat="server" />
    <asp:HiddenField ID="hfUpdated" runat="server" />
    <asp:HiddenField ID="hfConfirmed" runat="server" />
    </center>
    &nbsp;
</asp:Panel>
<cc1:msgBox runat="server" ID="msgBox1" />
</asp:Content>
