<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="NewOrder.aspx.cs" Inherits="ElfaInstall.NewOrder" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table class="tablemain" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="width:180px" class="tdmainRed">Store</td>
                <td style="width:260px" align="left">
                    <asp:Label runat="Server" ID="lblStore" Font-Bold="True" Font-Italic="True">Miami</asp:Label>
                </td>
                <td rowspan="6" colspan="4" style="width:320;vertical-align :top; text-align:left;">
                    <table>
                        <tr>
                            <td align="left" class="tdmain">
                                <asp:Label runat="Server" ID="lblSaleDate" Font-Bold="True">Sale Date:</asp:Label>
                            </td>
                            <td align="right">
                                <telerik:RadDatePicker ID="rdpEventDate" runat="server">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td class="tdmain" style="width: 192px">
                                &nbsp;&nbsp;&nbsp;<asp:Label runat="Server" ID="lblDelivery">Delivery Option</asp:Label>
                            </td>
                            <td align="left" style="width: 119px"><asp:CheckBox ID="cb1" runat="server" AutoPostBack="True" OnCheckedChanged="Cb1CheckedChanged" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdmain">Store Order #</td>
                <td align="left">
                    <asp:TextBox ID="txtOrderNumb" runat="server" Width="200px" MaxLength="14"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain" style="height: 27px">Planner</td>
                <td align="left" style="height: 27px">
                    <asp:TextBox ID="txtPlanner" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain">&nbsp;</td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="tdmainRed" style="font-size:larger;">Customer:</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <table  cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdmain">First Name&nbsp;</td>
                            <td align="left"><asp:TextBox ID="txtfName" runat="server" Width="80px" MaxLength="25"></asp:TextBox></td>
                            <td class="tdmain">MI&nbsp;</td>
                            <td align="left"><asp:TextBox ID="txtMi" runat="server" Width="25px" MaxLength="1"></asp:TextBox></td>
                            <td class="tdmain">Last Name&nbsp;</td>
                            <td align="left"><asp:TextBox ID="txtlName" runat="server" Width="110px" MaxLength="25"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:90px" class="tdmain">Address1&nbsp;</td>
                            <td style="width:160px" align="left">
                                <asp:TextBox ID="txtAddr1" runat="server" Width="170px" MaxLength="50"></asp:TextBox></td>
                            <td style="width:90px" class="tdmain">Address2&nbsp;</td>
                            <td style="width:100px" align="left">
                                <asp:TextBox ID="txtAddr2" runat="server" Width="105px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdmain">City&nbsp;</td>
                            <td align="left">
                                <asp:TextBox ID="txtCity" runat="server" Width="130px" MaxLength="25"></asp:TextBox></td>
                            <td class="tdmain">State&nbsp;</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlStates" runat="server" Width="130px">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="tdmain">Zip&nbsp;</td>
                            <td align="left">
                                <asp:TextBox ID="txtZip" runat="server" Width="45px" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdmain">Phones: &nbsp;&nbsp;&nbsp;Home&nbsp;</td>
                            <td align="left"><asp:TextBox ID="txtPhone1" runat="server" Width="120px" MaxLength="15"></asp:TextBox></td>
                            <td  class="tdmain">Other&nbsp;</td>
                            <td align="left"><asp:TextBox ID="txtPhone2" runat="server" Width="120px" MaxLength="15"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td  class="tdmain">Email</td>
                            <td align="left" colspan="3"><asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td style="width:30px"></td>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" style="border:1px; border-style:solid; border-color:#556b2f; height:50px;">
                        <tr>
                            <td class="tdmain">Install Preference</td>
                            <td>
                                <asp:DropDownList ID="ddlPref" runat="server">
                                    <asp:ListItem Selected="True">ASAP</asp:ListItem>
                                    <asp:ListItem>1 week</asp:ListItem>
                                    <asp:ListItem>2 weeks</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table> 
                </td>
            </tr>
        </table>
        <br />
        <table class="tablemain">
            <tr><td class="tdmainRed">Current solution description</td></tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtSolution" runat="server" Width="740px" Rows="2" TextMode="MultiLine" Font-Size="12pt"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tablemain">
            <tr><td class="tdmainRed" style="font-size:larger; width: 440px;">Installation Charges:</td></tr>
            <tr>
                <td class="tdmain" style="width: 440px">Non-Sale Retail Purchase Price</td>
                <td align="left">
                    $<asp:TextBox ID="txtPurchasePrice" runat="server" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain" style="width: 440px">Actual Purchase Price</td>
                <td align="left">
                    $<asp:TextBox ID="txtActual" runat="server" AutoPostBack="True" OnTextChanged="TxtActualTextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain" style="width: 440px">
                    <asp:Label runat="Server" ID="lblProc">30</asp:Label>% Basic Installation Charge (1 hr demo included)</td>
                <td align="left">
                    $<asp:TextBox ID="txtBasicInst" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain" style="width: 440px">
                    Delivery Option</td>
                <td align="left">
                    $<asp:TextBox ID="txtDeliveryPrice" runat="server" AutoPostBack="True" 
                        ontextchanged="TxtDeliveryPriceTextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain" style="width: 440px">
                    Addition Demo Required ($65.00/hr)</td>
                <td align="left">
                    $<asp:TextBox ID="txtDemoPrice" runat="server" AutoPostBack="True" OnTextChanged="TxtDemoPriceTextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdmain" style="font-size:larger; width: 440px;">Total Installation Charges</td>
                <td align="left">
                    $<asp:TextBox ID="txtOrderPrice" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <table class="tablemain">
            <tr><td class="tdmainRed">Additional Comments</td></tr>
            <tr>
                <td align="center"><asp:TextBox ID="txtComments" runat="server" Font-Size="12pt" Rows="2" TextMode="MultiLine" Width="740px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <table class="tablemain">
            <tr>
                <td align="center" width="400">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnsubmit" Text="Save" 
                        Width="120px" OnClick="BtnSave2Click" />
                </td>
                <td align="center" width="360">
                    <asp:Button ID="btnReturn" runat="server" CssClass="btnmenu" Text="Return" 
                        CausesValidation="False" PostBackUrl="~/Orders.aspx"/>
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="valOrderNumb" runat="server" ControlToValidate="txtOrderNumb"
            Display="None" ErrorMessage="Store Order Number is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valDate" runat="server" ControlToValidate="rdpEventDate"
            Display="None" ErrorMessage="Sale Date is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valPlanner" runat="server" ControlToValidate="txtPlanner"
            Display="None" ErrorMessage="Planner Name is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valFname" runat="server" ControlToValidate="txtfName"
            Display="None" ErrorMessage="Customer First Name is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valLname" runat="server" ControlToValidate="txtlName"
            Display="None" ErrorMessage="Customer Last Name is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valAddress" runat="server" ControlToValidate="txtAddr1"
            Display="None" ErrorMessage="Customer Address is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valCity" runat="server" ControlToValidate="txtCity"
            Display="None" ErrorMessage="Customer City is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valState" runat="server" ControlToValidate="ddlStates"
            Display="None" ErrorMessage="Customer State is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valPhone" runat="server" ControlToValidate="txtPhone1"
            Display="None" ErrorMessage="Customer Phone is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valPrice" runat="server" ControlToValidate="txtPurchasePrice"
            Display="None" ErrorMessage="Retail Price is Missing"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valActual" runat="server" ControlToValidate="txtActual"
            Display="None" ErrorMessage="Actual Price is Missing"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </center>
    &nbsp;
</asp:Panel>
</asp:Content>
