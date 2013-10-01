<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="EditVendor.aspx.cs" Inherits="ElfaInstall.EditVendor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
&nbsp;<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:480;" cellpadding="2"  class="tblBorder">
        <tr>
            <td colspan="2" class="tdheader" style="height: 20px">
                <asp:Label runat="Server" ID="lblHeader">Vendor Info</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:155px" class="tdmainR">Vendor Name:</td>
            <td style="width:320px" align="left"><asp:TextBox ID="txtName" runat="server" Width="260px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Address:</td>
            <td  align="left"><asp:TextBox ID="txtAddress" runat="server" Width="260px" 
                    MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">City:</td>
            <td  align="left"><asp:TextBox ID="txtCity" runat="server" Width="260px" 
                    MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">State:</td>
            <td align="left">
                <table>
                    <tr>
                        <td style="width:150px" class="tdmain"><asp:DropDownList ID="ddlStates" runat="server"></asp:DropDownList></td>
                        <td style="width:120px" align="left">Zip: <asp:TextBox ID="txtZip" runat="server" Width="70px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Location:</td>
            <td align="left"><asp:TextBox ID="txtLocation" runat="server" Width="260px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Email 1:</td>
            <td align="left">
                <asp:TextBox ID="txtEmail1" runat="server" Width="260px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Delivery:</td>
            <td align="left"><asp:TextBox ID="txtEmail2" runat="server" Width="260px" 
                    MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Phones:</td>
            <td align="left" >
                <table style="width:320;">
                    <tr>
                        <td style="width:100;" class="tdmainR">Phone</td>
                        <td style="width:220;" align="left"><asp:TextBox ID="txtPphone" runat="server" Width="185px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmainR">Office</td>
                        <td align="left"><asp:TextBox ID="txtOphone" runat="server" Width="185px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmainR">Cell</td>
                        <td align="left"><asp:TextBox ID="txtCphone" runat="server" Width="185px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmainR">Home</td>
                        <td align="left"><asp:TextBox ID="txtHphone" runat="server" Width="185px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdmainR">Fax</td>
                        <td align="left"><asp:TextBox ID="txtFphone" runat="server" Width="185px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Date Added</td>
            <td align="left">
                <telerik:RadDatePicker runat="server" ID="dpAdded">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Contractor #</td>
            <td align="left">
                <asp:TextBox ID="txtContractor" runat="server" Width="150px" MaxLength="20" />
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Payment %</td>
            <td align="left"><asp:TextBox ID="txtProcent" runat="server" Width="80px" 
                    MaxLength="6" /></td>
        </tr>
        <tr>
            <td class="tdmainR" style="height: 28px">Comments:</td>
            <td align="left" style="height: 28px"><asp:TextBox ID="txtComments" runat="server" 
                    Width="300px" MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR" style="height: 28px">
                Active <asp:CheckBox ID="cbActive" runat="server" />
            </td>
            <td class="tdmain" align="center">
                Insurance deductible <asp:CheckBox ID="cbDeduct" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Payment Goes To:</td>
            <td align="left">
                <asp:DropDownList ID="ddlVendors" runat="server"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <table  style=" width:480;" cellpadding="2"  class="tblBorder" >
        <tr>
            <td class="tdmainR" valign="top" width="140">Team Members:</td>
            <td align="left" width="210" valign="top">
                <asp:Label runat="Server" ID="lblMembers" CssClass="lblsize14"></asp:Label>
            </td>
            <td align="center" valign="top" width="120">
                <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="BtnAddMemberClick" CssClass="btnsmall" />
            </td>
        </tr>
        <tr>
            <asp:Panel runat="Server" ID="pnlMember" Visible="False">
                <td class="tdmainR">Installer Name:</td>
                <td align="left">
                    <asp:TextBox ID="txtNewMember" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnSaveMember" runat="server" Text=" Save " OnClick="BtnSaveMemberClick" CssClass="btnsmall" />
                </td>
            </asp:Panel>
        </tr>
    </table>
    <table  style=" width:480;" >
        <tr>
            <td style="width:160px; height:60px;" align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="btnsubmit" Text="Save Info" OnClick="BtnSaveClick" /></td>
            <td style="width:160px; height:60px;" align="center">
                <asp:Button ID="btnDelete" runat="server" CssClass="btnmenu" Text="Delete Info" OnClick="BtnDeleteClick" CausesValidation="False" /></td>
            <td style="width:160px; height:60px;" align="center">
                <asp:Button ID="btnCancel" runat="server" CssClass="btnmenu" Text="Return" PostBackUrl="~/VendorList.aspx" CausesValidation="False" /></td>
        </tr>
<%--        <tr>
            <td colspan="3" align="center" height="25">
                <asp:HyperLink ID="hlSchedule" runat="server" Target="_blank" Font-Bold="True">Installation Schedule</asp:HyperLink></td>
        </tr>--%>
    </table>
    <asp:RequiredFieldValidator ID="valName" runat="server" ControlToValidate="txtName"
        Display="None" ErrorMessage="Installer Name Missing!"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valCity" runat="server" ControlToValidate="txtCity"
        Display="None" ErrorMessage="Installer City Missing!"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valState" runat="server" ControlToValidate="ddlStates"
        Display="None" ErrorMessage="Installer State Missing!" InitialValue='""'></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail1"
        Display="None" ErrorMessage="Installer Email Missing!"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </center>
</asp:Panel>
</asp:Content>
