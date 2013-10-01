<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="EditOrganizer.aspx.cs" Inherits="ElfaInstall.EditOrganizer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-indent: 5pt;
            text-align: right;
            font-size: 10pt;
            padding-right: 10px;
            font-weight: bold;
            height: 26px;
            color: #666666;
        }
        .style2
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:480;" cellpadding="2"  class="tblBorder">
        <tr>
            <td colspan="2" class="tdheader" style="height: 20px">
                <asp:Label runat="Server" ID="lblHeader">Organizer Info</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:155px" class="tdmainR">Organizer Name:</td>
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
            <td class="tdmainR">Email:</td>
            <td align="left">
                <asp:TextBox ID="txtEmail1" runat="server" Width="260px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Phone:</td>
            <td align="left">
                <asp:TextBox ID="txtPhone" runat="server" Width="260px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style1">Contractor #</td>
            <td align="left" class="style2">
                <asp:TextBox ID="txtContractor" runat="server" Width="150px" MaxLength="20" />
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Payment %</td>
            <td align="left">
<%--                <asp:TextBox ID="txtProcent" runat="server" Width="80px" MaxLength="6" />--%>
                <telerik:RadNumericTextBox ID="txtProcent" runat="server" AutoPostBack="true" 
                    Width="80px" MaxValue="100" MinValue="0">
                        <EnabledStyle HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
            </td>
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
    </table>
    <table  style=" width:480;" >
        <tr>
            <td style="width:160px; height:60px;" align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="btnsubmit" Text="Save Info" 
                    onclick="BtnSaveClick" /></td>
<%--            <td style="width:160px; height:60px;" align="center">
                <asp:Button ID="btnDelete" runat="server" CssClass="btnmenu" Text="Delete Info" CausesValidation="False" /></td>--%>
            <td style="width:160px; height:60px;" align="center">
                <asp:Button ID="btnCancel" runat="server" CssClass="btnmenu" Text="Return" 
                    PostBackUrl="~/OrganizerList.aspx" CausesValidation="False" /></td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="valName" runat="server" ControlToValidate="txtName"
        Display="None" ErrorMessage="Organizer Name Missing!"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valPhone" runat="server" ControlToValidate="txtPhone"
        Display="None" ErrorMessage="Organizer Phone Missing!"></asp:RequiredFieldValidator>
<%--    <asp:RequiredFieldValidator ID="valCity" runat="server" ControlToValidate="txtCity"
        Display="None" ErrorMessage="Organizer City Missing!"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valState" runat="server" ControlToValidate="ddlStates"
        Display="None" ErrorMessage="Organizer State Missing!" InitialValue='""'></asp:RequiredFieldValidator>--%>
    <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail1"
        Display="None" ErrorMessage="Organizer Email Missing!"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </center>
</asp:Panel>
</asp:Content>
