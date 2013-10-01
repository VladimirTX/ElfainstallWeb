<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrganizerOrder.aspx.cs" Inherits="ElfaInstall.OrganizerOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <br />
        <table width="560px">
            <tr>
                <td class="tdheadersmall" style="font-size:larger; " align="center">Organizer Order </td>
                <tr>
                    <td class="tdmainRed" style="font-size:larger; text-align:center; height:40px">
                        Customer:</td>
                </tr>
                <tr>
                    <td align="center">
                        <table border="0" cellpadding="0" cellspacing="0" width="560px">
                            <tr>
                                <td class="tdmain">
                                    First Name&nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtfName" runat="server" MaxLength="25" Width="100px"></asp:TextBox>
                                </td>
                                <td class="tdmain">
                                    MI&nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtMi" runat="server" MaxLength="1" Width="25px"></asp:TextBox>
                                </td>
                                <td class="tdmain">
                                    Last Name&nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtlName" runat="server" MaxLength="25" Width="130px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" width="560px">
                            <tr>
                                <td class="tdmain" style="width:90px">
                                    Address1&nbsp;</td>
                                <td align="left" style="width:160px">
                                    <asp:TextBox ID="txtAddr1" runat="server" MaxLength="50" Width="160px"></asp:TextBox>
                                </td>
                                <td class="tdmain" style="width:90px">
                                    Address2&nbsp;</td>
                                <td align="left" style="width:100px">
                                    <asp:TextBox ID="txtAddr2" runat="server" MaxLength="20" Width="105px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" width="560px">
                            <tr>
                                <td class="tdmain" style="width:90px">
                                    City</td>
                                <td align="left">
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="25" Width="100px"></asp:TextBox>
                                </td>
                                <td class="tdmain">
                                    State&nbsp;</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStates" runat="server" Width="120px">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="tdmain">
                                    Zip&nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtZip" runat="server" MaxLength="10" Width="75px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdmain">
                                    Phones: &nbsp;&nbsp;&nbsp;Home&nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtPhone1" runat="server" MaxLength="15" Width="100px"></asp:TextBox>
                                </td>
                                <td class="tdmain">
                                    Other&nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtPhone2" runat="server" MaxLength="15" Width="90px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td class="tdmainRed" colspan="2">
                                    Store:<span style="color:Red"> *</span></td>
                                <td align="left" colspan="3">
                                    <asp:DropDownList ID="ddlStores" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td class="tdmainRed" colspan="2">
                                    Organizer:<span style="color:Red"> *</span></td>
                                <td align="left" colspan="3">
                                    <asp:DropDownList ID="ddlOrganizers" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table width="560px">
                            <tr>
                                <td class="tdmainRed" style="width:130px">
                                    Service Date</td>
                                <td align="left" style="width:160px">
                                    <telerik:RadDatePicker ID="rdpEventDate" runat="server">
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
                        <div style="color:Red; text-align:center; width:500px">
                            * required field</div>
                        <table width="450px">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnsubmit" 
                                        onclick="BtnSubmitClick" Text="Submit" ValidationGroup="Order" Width="120px" />
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btnmenu" 
                                        onclick="BtnCancelClick" Text="Cancel" Width="80px" />
                                </td>
                            </tr>
                        </table>
                        <asp:RangeValidator ID="valStore" runat="server" ControlToValidate="ddlStores" 
                            Display="None" ErrorMessage="Select Store" MaximumValue="999" MinimumValue="1" 
                            Type="Integer" ValidationGroup="Order"></asp:RangeValidator>
                        &nbsp;&nbsp;&nbsp;
                        <asp:RangeValidator ID="valOrganizer" runat="server" 
                            ControlToValidate="ddlOrganizers" Display="None" 
                            ErrorMessage="Select Organizer" MaximumValue="999" MinimumValue="1" 
                            Type="Integer" ValidationGroup="Order"></asp:RangeValidator>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="valDate" runat="server" 
                            ControlToValidate="rdpEventDate" Display="None" 
                            ErrorMessage="Select Service Date" InitialValue=" " ValidationGroup="Order"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" 
                            ShowSummary="False" ValidationGroup="Order" />
                    </td>
                </tr>
            </tr>
        </table>
    </center>
</asp:Panel>
</asp:Content>
