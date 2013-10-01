<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="FulfillOrder.aspx.cs" Inherits="ElfaInstall.FakeOrder" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table  class="tablemain">
            <tr>
                <td align="center" class="tdheadersmall" style="font-size:larger">
                    Additional Order for Installer -  
                    <asp:Label ID="lblInstaller" runat="server" Text="Installer"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdmainRed" style="font-size:larger; text-align:center"><br />Customer:</td>
            </tr>
        </table>
        <table width="560px">
            <tr>
                <td align="Left">
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
                                <asp:DropDownList ID="ddlStates" runat="server" Width="110px">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="tdmain">Zip&nbsp;</td>
                            <td align="left">
                                <asp:TextBox ID="txtZip" runat="server" Width="45px" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                                <td class="tdmain">Phones: &nbsp;&nbsp;&nbsp;Home&nbsp;</td>
                                <td align="left"><asp:TextBox ID="txtPhone1" runat="server" Width="80px" MaxLength="15"></asp:TextBox></td>
                                <td  class="tdmain">Other&nbsp;</td>
                                <td align="left"><asp:TextBox ID="txtPhone2" runat="server" Width="90px" MaxLength="15"></asp:TextBox></td>
                            </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br /><br />
        <table width="500px">
            <tr>
                <td style="width:250px" class="tdmainRed">Reason</td>
                <td style="width:250px" align="left">
                    <asp:DropDownList ID="ddlReason" runat="server" 
                        onselectedindexchanged="DdlReasonSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <asp:Panel ID="pnlReason" runat="server" Visible="false">
            <tr>
                <td class="tdmainRed">Type a reason</td>
                <td align="left">
                    <asp:TextBox ID="txtReason" runat="server" Width="320px"></asp:TextBox>
                </td>
            </tr>
            </asp:Panel>
            <tr>
                <td class="tdmainRed">Install Date</td>
                <td align="left">
                    <telerik:RadDatePicker ID="rdpEventDate" runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="tdmainRed">Vendor $ Due</td>
                <td align="left">
                    <asp:TextBox ID="txtVendorDue" runat="server" Width="80px" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdmainRed">Vendor Paid Date</td>
                <td align="left">
                    <telerik:RadDatePicker ID="rdpPayDate" runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="tdmainRed" colspan="2" valign="top">
                    Comments:
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
            </tr>
        </table>
            <asp:RequiredFieldValidator ID="valReason" runat="server" 
            ControlToValidate="ddlReason" Display="None" ErrorMessage="Select a Reason" 
            ValidationGroup="Order" InitialValue="0"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="valVendorDue" runat="server" ControlToValidate="txtVendorDue"
                Display="None" ErrorMessage="Vendor Due value is incorrect" MaximumValue="99999"
                MinimumValue="1" ValidationGroup="Order" Type="Currency"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="valVendorAmt" runat="server" ControlToValidate="txtVendorDue"
                Display="None" ErrorMessage="Vendor Due Amount is Missing" ValidationGroup="Order"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valInstallDate" runat="server" ControlToValidate="rdpEventDate"
                Display="None" ErrorMessage="Install Date is Missing" ValidationGroup="Order"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="valPayDate" runat="server" ControlToValidate="rdpEventDate"
                Display="None" ErrorMessage="Paid Date is Missing" ValidationGroup="Order"></asp:RequiredFieldValidator>
        <br />
        <table width="450px">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnsubmit" Width="120px" OnClick="BtnSubmitClick" ValidationGroup="Order" />
                </td>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnmenu" Width="80px" OnClick="BtnCancelClick" />
                </td>
            </tr>
        </table>
            <asp:ValidationSummary ID="valSumm" runat="server" ShowMessageBox="True" ShowSummary="False"
                ValidationGroup="Order" />
    </center>
</asp:Panel>
</asp:Content>
