<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="TroubleLog.aspx.cs" Inherits="ElfaInstall.TroubleLog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="msgBox" TagPrefix="cc1" Namespace="BunnyBear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:760; text-align:center">
            <tr>
                <td class="tdheader" style="height: 20px; text-align:center" colspan="2">
                    <asp:Label runat="server" ID="lblHeader">Call Log</asp:Label><br />
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="width: 760px" valign="top" colspan="2">
                    <asp:Label runat="server" ID="Label1" CssClass="tdtitle">Input problem description for Order # </asp:Label>
                    <asp:Label runat="server" ID="lblOrder" CssClass="tdtitle">12345</asp:Label>
                    <br /><br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="width:700px" cellpadding ="5px">
                        <tr>
                            <td style="width:300px" class="td14R">Customer Name:</td>
                            <td style="width:400px" class="td14">
                                <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td14R">Store:</td>
                            <td class="td14">
                                StoreID:&nbsp;<asp:Label ID="lblStoreID" runat="server" Text="Store"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblStoreNumb" runat="server" Text="Store #"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td class="td14R">Store Order #:</td>
                            <td class="td14">
                                <asp:Label ID="lblStoreOrder" runat="server" Text="Order"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td class="td14R">Installer Name, Phone:</td>
                            <td class="td14">
                                <asp:Label ID="lblInstaller" runat="server" Text="Installer"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblInstPhone" runat="server" Text="phone"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td class="td14R">Delivered by eis:</td>
                            <td class="td14">
                                <asp:Label ID="lblDelivered" runat="server" Text="Yes"></asp:Label>
                            </td>
                        </tr> 
                        <tr><td colspan="2"><hr /></td></tr>
                        <tr>
                            <td class="td14">
                                Missing Product:<br />
                                <asp:DropDownList ID="ddlMissing" runat="server">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>One bundle missing from order</asp:ListItem>
                                    <asp:ListItem>Backorders on the order</asp:ListItem>
                                    <asp:ListItem>On receipt, not received</asp:ListItem>
                                    <asp:ListItem>On design, not on product list</asp:ListItem>
                                    <asp:ListItem>Not on design</asp:ListItem>
                                    <asp:ListItem>Owned quantity</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td14">
                                <asp:Label ID="lblMissing" runat="server" Width="350px" ></asp:Label><br />
                                <asp:TextBox ID="txtMissing" runat="server" TextMode="MultiLine" Rows="2" 
                                    Width="350px" MaxLength="240"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr><td colspan="2"><hr /></td></tr>
                        <tr>
                            <td class="td14">
                                Damaged Product:<br />
                                <asp:DropDownList ID="ddlDamaged" runat="server">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>Installer mis-cut</asp:ListItem>
                                    <asp:ListItem>Delivery company</asp:ListItem>
                                    <asp:ListItem>Defective merchandise</asp:ListItem>
                                    <asp:ListItem>Damaged product</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td14">
                                <asp:Label ID="lblDamaged" runat="server" Width="350px"></asp:Label><br />
                                <asp:TextBox ID="txtDamaged" runat="server" TextMode="MultiLine" Rows="2" 
                                    Width="350px" MaxLength="240"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr><td colspan="2"><hr /></td></tr>
                        <tr>
                            <td class="td14">
                                Change to Design :<br />
                                <asp:DropDownList ID="ddlChange" runat="server">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>Customer mis-measured</asp:ListItem>
                                    <asp:ListItem>Design would not work</asp:ListItem>
                                    <asp:ListItem>Customer/Installer changed the design onsite</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <span style="font-weight:normal; font-size:small">(Include Space and Wall)</span>
                            </td>
                            <td class="td14">
                                <asp:Label ID="lblFlaw" runat="server" Width="350px"></asp:Label><br />
                                <asp:TextBox ID="txtFlaw" runat="server" TextMode="MultiLine" Rows="2" 
                                    Width="350px" MaxLength="485"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr><td colspan="2"><hr /></td></tr>
                        <tr>
                            <td class="td14">
                                Trip back to Store:<br />
                                <asp:DropDownList ID="ddlTrip" runat="server">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>Installer will go</asp:ListItem>
                                    <asp:ListItem>Customer coming to store to pick up</asp:ListItem>
                                    <asp:ListItem>CSD to ship (mail)</asp:ListItem>
                                    <asp:ListItem>Manhatten Logistics</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td14">
                                <asp:Label ID="lblTrip" runat="server" Width="350px"></asp:Label><br />
                                <asp:TextBox ID="txtTrip" runat="server" Width="350px" MaxLength="240"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr><td colspan="2"><hr /></td></tr>
                        <tr>
                            <td class="td14">
                                Contact at Store:<br />
                                <asp:DropDownList ID="ddlContact" runat="server">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>CSD to ship (mail)</asp:ListItem>
                                    <asp:ListItem>Manhatten Logistics</asp:ListItem>
                                    <asp:ListItem>This email serves as your communication</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td14">
                                <asp:Label ID="lblContact" runat="server" Width="350px"></asp:Label><br />
                                <asp:TextBox ID="txtContact" runat="server" Width="350px" MaxLength="240"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td colspan="2"><hr /></td></tr>
                        <tr>
                            <td class="td14">
                                Reason Code:<br />
                                <asp:DropDownList ID="ddlReasons" runat="server"></asp:DropDownList>
                            </td>
                            <td class="td14">
                                Install Date:&nbsp;&nbsp;&nbsp;
                                <telerik:RadDatePicker ID="rdpInstallDate" runat="server">
                                </telerik:RadDatePicker>
                            </td>
                        </tr> 
                    </table>
                </td>
            </tr>
            <tr><td></td></tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td style="width: 380px">
                    <asp:Button ID="btsSave" runat="server" CssClass="btnmenu" Width="160" Text="Save / Send Email" OnClick="BtsSaveClick" ValidationGroup="TroubleLog" />
                </td>
                <td style="width: 380px">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btnmenu" Width="140" Text="Cancel" OnClick="BtnCancelClick" />
                </td>
            </tr>
<%--            <tr>
                <td colspan="2">
                    <br /><br />
                    <asp:HyperLink ID="hlLog" runat="server" CssClass="lblsize12" Target="_blank" NavigateUrl="~/TroubleLogReport.aspx">View Trouble Log Report</asp:HyperLink>
                </td>
            </tr>--%>
        </table>
<%--    <asp:RequiredFieldValidator ID="valTroubleLog" runat="server" ControlToValidate="txtDescription"
        Display="None" ErrorMessage="Description can not be empty!" ValidationGroup="TroubleLog"></asp:RequiredFieldValidator>--%>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="TroubleLog" />
    </center>
</asp:Panel>
<cc1:msgBox runat="server" ID="msgBox1" />
</asp:Content>
