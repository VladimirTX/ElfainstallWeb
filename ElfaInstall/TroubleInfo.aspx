<%@ Page Title="Trouble Log" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="TroubleInfo.aspx.cs" Inherits="ElfaInstall.TroubleInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:760; text-align:center">
            <tr>
                <td class="tdheader" style="height: 20px; text-align:center" colspan="2">
                    <asp:Label runat="server" ID="lblHeader">Trouble Log</asp:Label><br />
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="width: 760px" valign="top" colspan="2">
                    <asp:Label runat="server" ID="Label1" CssClass="tdtitle">Problem description for Order # </asp:Label>
                    <asp:Label runat="server" ID="lblOrder" CssClass="tdtitle">12345</asp:Label>
                    <br />
                </td>
            </tr>
        </table>
        <table style=" width:760; text-align:center">
            <tr>
                <td style="width:160px" class="td14R" >Customer:</td>
                <td style="width:300px" class="td14"><asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                <td style="width:200px" class="td14R">Store:</td>
                <td style="width:100px" class="td14"><asp:Label ID="lblStore" runat="server" Text="Store"></asp:Label></td>
            </tr>
            <tr>
                <td class="td14R">Installer:</td>
                <td class="td14"><asp:Label ID="lblInstaller" runat="server" Text="Installer"></asp:Label></td>
                <td class="td14R">Delivered by eis:</td>
                <td class="td14"><asp:Label ID="lblDelivered" runat="server" Text="Yes"></asp:Label></td>
            </tr>
            <tr>
                <td class="td14R">Installation Date:</td>
                <td class="td14"><asp:Label ID="lblInstDate" runat="server"></asp:Label></td> 
                <td class="td14R">Installed:</td>
                <td class="td14"><asp:Label ID="lblInstalled" runat="server" Text="Yes"></asp:Label></td>
            </tr>
        </table>
        <asp:Panel ID="pnlMissing" runat="server" Visible="false">
            <center>
            <table style="width:760; text-align:center">
                <tr><td colspan="4"><hr /></td></tr>
                <tr>
                    <td style="width:160px" class="td14R">Missing Product:</td>
                    <td style="width:300px" class="td14" valign="top">
                        Details:<br />
                        <asp:Label ID="lblmDetails" runat="server" Width="295px">Details</asp:Label>
                    </td>
                    <td style="width:300px" class="td14" valign="top">
                        Resolution:<br />
                        <asp:Label ID="lblmResolution" runat="server" Width="295px">Resolution</asp:Label>
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel>
        <asp:Panel ID="pnlDamaged" runat="server" Visible="false" HorizontalAlign="Center">
            <center>
            <table style="width:760; text-align:center">
                <tr><td colspan="4"><hr /></td></tr>
                <tr>
                    <td style="width:160px" class="td14R">Damaged Product:</td>
                    <td style="width:300px" class="td14" valign="top">
                        Details:<br />
                        <asp:Label ID="lbldDetails" runat="server" Width="295px">Details</asp:Label>
                    </td>
                    <td style="width:300px" class="td14" valign="top">
                        Resolution:<br />
                        <asp:Label ID="lbldResolution" runat="server" Width="295px">Resolution</asp:Label>
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel>
        <asp:Panel ID="pnlFlaw" runat="server" Visible="false">
            <table style="width:760; text-align:center">
                <tr><td colspan="2"><hr /></td></tr>
                <tr>
                    <td style="width:160px" class="td14R">Design Flaw:</td>
                    <td style="width:600px" class="td14">
                        <asp:Label ID="lblFlaw" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table style="width:760; text-align:center">
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td style="width:160px" class="td14R">Problem Description:</td>
                <td style="width:590px" align="left">
                    <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="80px" Width="580px"><telerik:RadPane ID="RadPane2" runat="server" Scrolling="Y" Width="580px"><asp:Label ID="lblTroubleData" runat="server" CssClass="lblsize14"></asp:Label></telerik:RadPane></telerik:RadSplitter>
                </td>
            </tr>
            <tr>
                <td class="td14R">Add Problem Description:</td>
                <td class="td14">
                    <asp:TextBox ID="txtTrouble" runat="server" TextMode="MultiLine" Rows="2" 
                        Width="580px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table width="450px">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnsubmit" 
                        Width="120px" onclick="BtnSaveClick" />
                </td>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnmenu" 
                        Width="120px" onclick="BtnCancelClick" />
                </td>
            </tr>
        </table>
        <br />
    </center>
</asp:Panel>
</asp:Content>
