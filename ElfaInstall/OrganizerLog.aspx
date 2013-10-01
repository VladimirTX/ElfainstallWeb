<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrganizerLog.aspx.cs" Inherits="ElfaInstall.OrganizerLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" Width="1080px">
    <center>
        <table style=" width:950;">
            <tr>
                <td style="height: 20px; text-align:center">
                    <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666">Open Orders Log</asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlGread" Visible="true">
            <table>
                <tr>
                    <td style="width: 1000px; border-style:double; border-width:2px; border-color:Black;" valign="top">
                        <asp:GridView ID="grdLog" runat="server" DataSourceID="sdsLog"  AutoGenerateColumns="False"  
                            Width="1000px" CellPadding="0" HorizontalAlign="Center" 
                            GridLines="Vertical" CssClass="gridData" 
                            ShowFooter="True" BackColor="White" AllowPaging="True" AllowSorting="True" 
                            EnableModelValidation="True" onpageindexchanged="GrdLogPageIndexChanged" 
                            onrowdatabound="GrdOrdersRowDataBound" 
                            onsorted="GrdLogSorted"  PageSize="16" 
                            onselectedindexchanged="GrdLogSelectedIndexChanged">
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
                                <asp:BoundField DataField="Fees" HeaderText="Fees" 
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
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total" 
                                    DataFormatString="{0:$0.00}" HtmlEncode="False">
                                    <ItemStyle CssClass="gridcellright" Width="60px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Ins. Deduct">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="checkBox4" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbDeduct" runat="server" Checked='<%# Eval("Deduct") %>' Enabled="true" 
                                            AutoPostBack="true" OnCheckedChanged="cbDeduct_OnCheckedChanged"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" CausesValidation="False" 
                                    ShowSelectButton="True" ShowCancelButton="False" SelectText="Close" >
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:CommandField>
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
                <tr>
                    <td align="center">
                        <br />
                        <asp:Panel ID="pnlEdit" runat="server" Width="500px" Visible="false">
                            <table style="width:500px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width:200px;" class="tdmain">Order #</td> 
                                    <td style="width:300px;" class="tdmain">
                                        <asp:Label ID="lblOrderNumber" runat="server" Text="Order #"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmain">Customer:</td>
                                    <td class="tdmain">
                                        <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblPhone" runat="server" Text="Phone #"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><hr /></td>
                                </tr>
                                <tr>
                                    <td class="tdmain">Fees</td>
                                    <td align="left" class="tdmain">
                                        <asp:Label ID="lblFees" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmain">Other</td>
                                    <td  align="left" class="tdmain">
                                        <asp:Label ID="lblOther" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmain">Adjustment</td>
                                    <td  align="left" class="tdmain">
                                        <asp:Label ID="lblAdjustment" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmain">Total</td>
                                    <td  align="left" class="tdmain">
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdmain" colspan="2">
                                        <span class="tdmain">Invoice Notes:</span><br />
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblComments" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr><td colspan="2"><hr /></td></tr>
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
                                <tr><td colspan="2"><hr /></td></tr>
                                <tr>
                                    <td align="center"><br />
                                        <asp:Button ID="btnSave" runat="server" Text=" Save " 
                                            CssClass="btnsubmit" Width="80px" onclick="BtnSaveClick" />
                                    <br />&nbsp;
                                </td>
                                    <td align="center" style="width: 250px"><br />
                                        <asp:Button ID="btnClose" 
                                            runat="server" Text="Cancel" CssClass="btnmenu"  
                                            Width="80px" onclick="BtnCloseClick"/>
                                        <br />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:SqlDataSource runat="server" ID="sdsLog" />
        <asp:HiddenField runat="server" ID="hfOrganizerID" />
    </center>
    </asp:Panel>
</asp:Content>
