<%@ Page Title="Store Info" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="EditStore.aspx.cs" Inherits="ElfaInstall.EditStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
     <table style=" width:760;" cellpadding="2">
        <tr>
            <td colspan="2" class="tdheader" style="height: 20px">
                <asp:Label runat="Server" ID="lblHeader">Store Info</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:290;" valign="top">
                <table style="width:300;" class="tblBorder">
                    <tr>
                        <td style="width:120px" class="tdmain">Store #</td>
                        <td style="width:170px" align="left"><asp:TextBox ID="txtStoreNumb" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Store Code</td>
                        <td align="left"><asp:TextBox ID="txtStoreCode" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Store Name</td>
                        <td align="left"><asp:TextBox ID="txtStoreName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Market</td>
                        <td align="left"><asp:DropDownList ID="ddlMarkets" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Address</td>
                        <td align="left"><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">City</td>
                        <td align="left"><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">State</td>
                        <td align="left"><asp:DropDownList ID="ddlStates" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Zip</td>
                        <td align="left"><asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Phone</td>
                        <td align="left"><asp:TextBox ID="txtStorePhone" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td style="width:450px;" valign="top">
                <table style="width:450;" class="tblBorder">
                    <tr>
                        <td style="width:170px" class="tdmain">General Manager</td>
                        <td style="width:270px" align="left"><asp:TextBox ID="txtGM" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">GM Email</td>
                        <td align="left"><asp:TextBox ID="txtGMemail" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">IC</td>
                        <td align="left"><asp:TextBox ID="txtIC" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">IC Email</td>
                        <td align="left"><asp:TextBox ID="txtICemail" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Manager Trainer</td>
                        <td align="left"><asp:TextBox ID="txtMT" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">MT Email</td>
                        <td align="left"><asp:TextBox ID="txtMTemail" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain"></td>
                        <td align="left"></td>
                    </tr>
                    <tr>
                        <td class="tdmain">Area Director</td>
                        <td align="left"><asp:TextBox ID="txtDirector" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmain">AD Email</td>
                        <td align="left"><asp:TextBox ID="txtADemail" runat="server" Width="240px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width:100%;" class="tblBorder">
                    <tr>
                        <td style="width:140;" class="tdmain">Inst. Charge %</td>
                        <td style="width:100px;" align="left">
                            <asp:TextBox ID="txtInstProc" runat="server" Width="60px" MaxLength="4"></asp:TextBox></td>
                        <td style="width:140;" class="tdmain">Minimum Inst. Price</td>
                        <td style="width:100px;" align="left">
                            <asp:TextBox ID="txtInstMin" runat="server" Width="70px" MaxLength="6"></asp:TextBox></td>
                        <td style="width:140;" class="tdmain">Delivery Price</td>
                        <td style="width:100px;" align="left">
                            <asp:TextBox ID="txtDelivery" runat="server" Width="60px" MaxLength="6"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style=" width:760;" cellpadding="5">
        <tr>
            <td align="center" style="width:33%;"><asp:Button ID="btnSave" runat="server" CssClass="btnsubmit" Text="Save Store Info" ValidationGroup="Save" OnClick="BtnSaveClick" /></td>
            <td align="center" style="width:33%;"><asp:Button ID="btnDelete" runat="server" CssClass="btnmenu" Text="Delete Store Info" OnClick="BtnDeleteClick" /></td>
            <td align="center" style="width:33%;"><asp:Button ID="btnReturn" runat="server" CssClass="btnmenu" Text="Return Without Save" OnClick="BtnReturnClick" /></td>
        </tr>
    </table>
    <br />
    <table style="width:700px;" class="tblBorder" cellpadding="10px">
        <tr><td colspan="2" align="center" class="tblBorder"><b>Vendors Assigned to this Store</b></td></tr>
        <tr>
            <td class="tdmain" style="width:350;">Assigned</td>
            <td class="tdmain" style="width:350;">Available</td>
        </tr>
        <tr>
            <td valign="middle" class="tblBorder">
                <asp:ListBox ID="lstAssigned" runat="server" Height="100px" 
                    OnSelectedIndexChanged="LstAssignedSelectedIndexChanged" Rows="5"></asp:ListBox>&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="BtnRemoveClick" />
            </td>
            <td valign="middle" class="tblBorder">
                <asp:ListBox ID="lstAvailable" runat="server" Height="100px" 
                    OnSelectedIndexChanged="LstAvailableSelectedIndexChanged" Rows="5"></asp:ListBox>&nbsp;
                <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="BtnAssignClick" />
            </td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="valStoreNumb" runat="server" ControlToValidate="txtStoreNumb"
        Display="None" ErrorMessage="Store Number Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valStoreCode" runat="server" ControlToValidate="txtStoreCode"
        Display="None" ErrorMessage="Store Code Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valStoreName" runat="server" ControlToValidate="txtStoreName"
        Display="None" ErrorMessage="Store Name Required" ValidationGroup="Save"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valMarket" runat="server" ControlToValidate="ddlMarkets"
        Display="None" ErrorMessage="Please Select Market" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator>&nbsp;
    <asp:RequiredFieldValidator ID="valInstProc" runat="server" ControlToValidate="txtInstProc"
        Display="None" ErrorMessage="Input Inst. Charge %" ValidationGroup="Save"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="valInstProc2" runat="server" ControlToValidate="txtInstProc"
        Display="None" ErrorMessage="Input % of Basic Install. Charge" MaximumValue="100"
        MinimumValue="5" Type="Double" ValidationGroup="Save"></asp:RangeValidator>
    <asp:RequiredFieldValidator ID="valMinPrice" runat="server" ControlToValidate="txtInstMin"
        Display="None" ErrorMessage="Input Min. Inst. Price" ValidationGroup="Save"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valDelivery" runat="server" ControlToValidate="txtDelivery"
        Display="None" ErrorMessage="Input Delivery Price (or 0)" ValidationGroup="Save"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
    </center>
</asp:Panel>
</asp:Content>
