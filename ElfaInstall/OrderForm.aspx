<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderForm.aspx.cs" Inherits="ElfaInstall.OrderForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>elfa Installation Service</title>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#E63034" background="images/bac_elfa_short.jpg" leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
    <center>
        <p>&nbsp;</p>
        <p>&nbsp;</p>
        <table width="610" height="600" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td colspan="3" height="100" valign="top"><img src="images/elfa_masthead.jpg" width="610" height="100" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="2" style="color:#CC3232; font-weight:bold; font-family:Arial; font-size:large; text-align:left">
                    Service Order Form
                </td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td width="360" class="td12">Select Store where order was purchased:<span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span></td>
                <td width="220" align="left">
                    <asp:DropDownList ID="ddlStores" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">Order confirmation number:</td>
                <td align="left">
                    <asp:TextBox ID="txtOrderNumb" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">Name used on order at store:<span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span></td>
                <td align="left">
                    <asp:TextBox ID="txtCustomer" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">Address of install:<span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span></td>
                <td align="left">
                    <asp:TextBox ID="txtAddress" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">City:<span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span></td>
                <td align="left">
                    <asp:TextBox ID="txtCity" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">State, Zip:<span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span></td>
                <td align="left">
                    <asp:DropDownList ID="ddlStates" runat="server">
                    </asp:DropDownList>
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtZip" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">Contact phone number 1:<span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span></td>
                <td align="left">
                    <asp:TextBox ID="txtPhone1" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">Contact phone number 2:</td>
                <td align="left">
                    <asp:TextBox ID="txtPhone2" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12">Additional Comments:</td>
                <td align="left" style="font-size: 12pt">
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="225px" Rows="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="tdtitlesmall" colspan="2" align="center">
                    <span style="color: #ff0033;font-size: 14pt; font-weight:bold"> *</span> - required field.
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
                <td align="center">
                    <br />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/buttom_Submit.jpg" OnClick="ImageButton1_Click" />
                    <br />&nbsp;
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="valStore" runat="server" ErrorMessage="Please Select Store where order was purchased" ControlToValidate="ddlStores" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valName" runat="server" ErrorMessage="Please provide Name used on order at store" ControlToValidate="txtCustomer" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valAddress" runat="server" ErrorMessage="Please provide Address of install" ControlToValidate="txtAddress" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valCity" runat="server" ErrorMessage="Please provide City of install" ControlToValidate="txtCity" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valState" runat="server" ErrorMessage="Please provide State of install" ControlToValidate="ddlStates" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valZip" runat="server" ErrorMessage="Please provide ZIP of install" ControlToValidate="txtZip" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valPhone" runat="server" ErrorMessage="Please provide Contact phone number" ControlToValidate="txtPhone1" Display="None"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="false" ShowMessageBox="true" />
    </center>
    </div>
    </form>
</body>
</html>
