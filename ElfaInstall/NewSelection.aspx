<%@ Page Title="New Orders Menu" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="NewSelection.aspx.cs" Inherits="ElfaInstall.NewSelection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table class="tablemain" border="0">
            <tr>
                <td class="tdheadersmall"><br />Select Store and Order Type to Create New Installation Order<br />
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlStores" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlTypes" runat="server">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem Value="ATH">At Home</asp:ListItem>
                        <asp:ListItem Value="ATH-AT">ATH-AT</asp:ListItem>
                        <asp:ListItem Value="ATH-CG">ATH-CG</asp:ListItem>
                        <asp:ListItem Value="ATH-MB">ATH-MB</asp:ListItem>
                        <asp:ListItem Value="B2B">Business to Business</asp:ListItem>
                        <asp:ListItem Value="CAL">Call In</asp:ListItem>
<%--                        <asp:ListItem Value="COM">Commercial</asp:ListItem>--%>
                        <asp:ListItem Value="EPCP">Elfa Preferred Customer</asp:ListItem>
                        <asp:ListItem Value="EDS">Ellen DeGeneres Show</asp:ListItem>
                        <asp:ListItem Value="FFE">Fall for elfa</asp:ListItem>
                        <asp:ListItem Value="JFU">Just for You</asp:ListItem>
                        <asp:ListItem Value="MID">VIP</asp:ListItem>
                        <asp:ListItem Value="PRM">PR</asp:ListItem>
<%--                        <asp:ListItem Value="STC">Store Comp</asp:ListItem>--%>
                        <asp:ListItem Value="M">M-Order</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center"><br /><asp:Button ID="btnContinue" runat="server" CssClass="btnsubmit" Text=" Continue " OnClick="BtnContinueClick" /></td>
            </tr>
            <tr><td><br /><hr style="height:3pt; color:Black; border-width:medium" /></td></tr>
            <tr>
                <td class="tdheadersmall"><br />Select Vendor to Create Fulfillment Order<br />
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlVendors" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="center"><br /><asp:Button ID="btnDummyOrder" runat="server" CssClass="btnsubmit" Text=" Continue " OnClick="BtnDummyOrderClick"/></td>
            </tr>
            <tr><td><br /><hr style="height:3pt; color:Black; border-width:medium" /></td></tr>
            <tr>
                <td class="tdheadersmall"><br />Create a Special Order</td>
            </tr>
            <tr>
                <td align="center"><br /><asp:Button ID="btnServiceOrder" runat="server" CssClass="btnsubmit" Text=" Continue " PostBackUrl="~/ServiceOrder.aspx" />
                    <br />&nbsp;
                </td>
            </tr>
            <tr><td><br /><hr style="height:3pt; color:Black; border-width:medium" /></td></tr>
            <tr>
                <td class="tdheadersmall"><br />Create Stand Alone Organizer Order</td>
            </tr>
            <tr>
                <td align="center"><br /><asp:Button ID="btnOrganizerOrder" runat="server" CssClass="btnsubmit" Text=" Continue " PostBackUrl="~/OrganizerOrder.aspx" />
                    <br />&nbsp;
                </td>
            </tr>
        </table>
    </center>
</asp:Panel>
</asp:Content>
