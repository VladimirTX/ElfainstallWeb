<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="RegionInfo.aspx.cs" Inherits="ElfaInstall.RegionInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain" Width="950px">
    <center>
        <br /><br />
        <table style=" width:500;">
            <tr>
                <td class="tdheader" style="height: 20px; color:Black" colspan="3">
                    Hello <asp:Label runat="server" ID="lblHeader">Region</asp:Label>!
                </td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td class="tdheadersmall" style="color:Black" colspan="3">You have some orders which require attention:</td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td  style="width:350px" class="td18Bold" colspan="2">Total # of orders in log:</td>
                <td style="width:75px" class="td18Bold"><asp:Label ID="lblTotal" runat="server">55</asp:Label></td>
                <td style="width:75px">&nbsp;</td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                
            </tr>
            <tr>
                <td style="width:50px">&nbsp;</td>
                <td class="td14Bold">Confirmed Orders:</td>
                <td class="td14Bold"><asp:Label ID="lblConfirmed" runat="server">55</asp:Label></td>
                <td class="td14Bold"><asp:Label ID="lblConfirmedP" runat="server">55</asp:Label>%</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12a">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;By Phone:</td>
                <td class="td12a"><asp:Label ID="lblByPhone" runat="server">55</asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td12a">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;By Survey:</td>
                <td class="td12a"><asp:Label ID="lblBySurvey" runat="server">55</asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="td14Bold">Unconfirmed Orders:</td>
                <td class="td14Bold"><asp:Label ID="lblUnconfirmed" runat="server">55</asp:Label></td>
                <td class="td14Bold"><asp:Label ID="lblUnconfirmedP" runat="server">55</asp:Label>%</td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td class="td18Bold" colspan="2">No Contact with customer:</td>
                <td class="td18Bold"><asp:Label ID="lblNoContact" runat="server">55</asp:Label></td>
                <td class="td18Bold"><asp:Label ID="lblNoContactP" runat="server">55</asp:Label>%</td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td class="td18Bold" colspan="2">Unscheduled Orders:</td>
                <td class="td18Bold"><asp:Label ID="lblUnscheduled" runat="server">55</asp:Label></td>
                <td class="td18Bold"><asp:Label ID="lblUnscheduledP" runat="server">55</asp:Label>%</td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <asp:Panel ID="pnlNotScheduled" runat="server">
                <tr>
                    <td>&nbsp;</td>
                    <td class="td14Bold">Under Construction:</td>
                    <td class="td14Bold"><asp:Label ID="lblUnderConstruction" runat="server">55</asp:Label></td>
<%--                    <td class="td14Bold"><asp:Label ID="lblUnderConstructionP" runat="server">55</asp:Label>%</td>--%>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="td14Bold">Backorders:</td>
                    <td class="td14Bold"><asp:Label ID="lblBackorders" runat="server">55</asp:Label></td>
<%--                    <td class="td14Bold"><asp:Label ID="lblBackordersP" runat="server">55</asp:Label>%</td>--%>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="td14Bold">Customer Rescheduling:</td>
                    <td class="td14Bold"><asp:Label ID="lblRescheduling" runat="server">55</asp:Label></td>
<%--                    <td class="td14Bold"><asp:Label ID="lblReschedulingP" runat="server">55</asp:Label>%</td>--%>
                    <td>&nbsp;</td>
                </tr>
                <tr><td>&nbsp;</td></tr>
            </asp:Panel>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td colspan="3" align="center">
                    <telerik:RadButton runat="server" ID="btnContinue" Text=" Continue "
                        Width="140px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        onclick="BtnContinueClick">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
        </table>
        <asp:HiddenField ID="hfRegionID" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
