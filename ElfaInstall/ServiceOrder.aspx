<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="ServiceOrder.aspx.cs" Inherits="ElfaInstall.ServiceOrder" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <br />
        <table width="700px">
            <tr>
                <td style="width:150px;font-size:larger" align="right" class="tdheadersmall">
                    New Order
                </td>
                <td style="width:200px" class="tdmain" align="left">
                    Type:&nbsp;&nbsp;
                    <asp:DropDownList runat="server" ID="ddlOrderType" 
                        onselectedindexchanged="DdlOrderTypeSelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem Value="S">Service Order</asp:ListItem>
                        <asp:ListItem Value="G">Gift Certificate</asp:ListItem>
                        <asp:ListItem Value="X">Extra Order</asp:ListItem>
                        <asp:ListItem Value="U">Uninstall Order</asp:ListItem>
                        <asp:ListItem Value="R">Refund</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:350px" class="tdmain" align="left">
                    <asp:Label ID="lblReason" runat="server" Visible="false">Reason:</asp:Label>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlReason" runat="server" Visible="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdmainRed" style="font-size:larger; text-align:center" colspan="3"><br />Customer:</td>
            </tr>
        </table>
        <table width="560px">
        <tr>
            <td align="center">
                <table  cellpadding="0" cellspacing="0" width="560px" border="0">
                    <tr>
                        <td class="tdmain">First Name&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtfName" runat="server" Width="100px" MaxLength="25"></asp:TextBox></td>
                        <td class="tdmain">MI&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtMi" runat="server" Width="25px" MaxLength="1"></asp:TextBox></td>
                        <td class="tdmain">Last Name&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtlName" runat="server" Width="130px" MaxLength="25"></asp:TextBox></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="560px">
                    <tr>
                        <td style="width:90px" class="tdmain">Address1&nbsp;</td>
                        <td style="width:160px" align="left">
                            <asp:TextBox ID="txtAddr1" runat="server" Width="160px" MaxLength="50"></asp:TextBox></td>
                        <td style="width:90px" class="tdmain">Address2&nbsp;</td>
                        <td style="width:100px" align="left">
                            <asp:TextBox ID="txtAddr2" runat="server" Width="105px" MaxLength="20"></asp:TextBox></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="560px">
                    <tr>
                        <td class="tdmain" style="width:90px">City</td>
                        <td align="left">
                            <asp:TextBox ID="txtCity" runat="server" Width="100px" MaxLength="25"></asp:TextBox></td>
                        <td class="tdmain">State&nbsp;</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlStates" runat="server" Width="120px">
                                <asp:ListItem Selected="True"></asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="tdmain">Zip&nbsp;</td>
                        <td align="left">
                            <asp:TextBox ID="txtZip" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmain">Phones: &nbsp;&nbsp;&nbsp;Home&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtPhone1" runat="server" Width="100px" MaxLength="15"></asp:TextBox></td>
                        <td  class="tdmain">Other&nbsp;</td>
                        <td align="left"><asp:TextBox ID="txtPhone2" runat="server" Width="90px" MaxLength="15"></asp:TextBox></td>
                    </tr>
                    <tr><td colspan="6"><hr /></td></tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2" class="tdmainRed">Store:<span style="color:Red"> *</span></td>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlStores" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr><td colspan="6"><hr /></td></tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2" class="tdmainRed">Vendor:</td>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlVendors" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr><td colspan="6"><hr /></td></tr>
                </table>
                <table width="560px">
                    <tr>
                        <td style="width:130px" class="tdmainRed">Service Date</td>
                        <td style="width:160px" align="left">
                            <telerik:RadDatePicker ID="rdpEventDate" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width:140px" class="tdmainRed">
                            Install Price </td>
                        <td style="width:110px">
                            <asp:TextBox ID="txtPrice" runat="server" Width="80px" Text="0.00"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmainRed" colspan="4" valign="top">
                            Old Comments:&nbsp;
                            <asp:Label ID="lblComments" runat="server" Width="400px" CssClass="lblsize12a" BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmainRed" colspan="4" valign="top">
                            New Comments:
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="color:Red; text-align:center; width:500px">* required field</div>
            </td>
        </tr>
    </table>
        <asp:RangeValidator ID="valStore" runat="server" ControlToValidate="ddlStores"
            Display="None" ErrorMessage="Select Store" MaximumValue="999" MinimumValue="1"
            Type="Integer" ValidationGroup="Order"></asp:RangeValidator>
            &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="valType" runat="server" 
            ControlToValidate="ddlOrderType" Display="None" 
            ErrorMessage="Select Order Type" ValidationGroup="Order"></asp:RequiredFieldValidator>
    <table width="450px">
        <tr>
            <td align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnsubmit" Width="120px" ValidationGroup="Order" OnClick="BtnSubmitClick" />
            </td>
            <td align="center">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnmenu" Width="80px" OnClick="BtnCancelClick" />
            </td>
        </tr>
    </table>
        <asp:ValidationSummary ID="valSumm" runat="server" ShowMessageBox="True" ShowSummary="False"
            ValidationGroup="Order" />
        <asp:HiddenField ID="hfOrderNumb" runat="server" />
        <asp:HiddenField ID="hfCustID" runat="server" />
        <asp:HiddenField ID="hfOption" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
