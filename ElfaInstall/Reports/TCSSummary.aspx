<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="TCSSummary.aspx.cs" Inherits="ElfaInstall.Reports.TCSSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />
        TCS Comparative Analysis Summary<br />&nbsp;</center></div>
    <table style=" width:740; font-size:small;" cellpadding="5" cellspacing="0">
        <tr style="font-weight:bold;">
            <td style="width:120px; font-size: 12pt;" align="right">Start Date:</td>
            <td style="width:160px; font-size: 12pt;" align="left">
                <asp:DropDownList runat="Server" ID="ddlStart" DataTextFormatString="{0:d}"></asp:DropDownList>
            </td>
            <td style="width:120px; font-size: 12pt;" align="right">End Date:</td>
            <td style="width:160px; font-size: 12pt;" align="left">&nbsp;&nbsp;
                <asp:DropDownList runat="Server" ID="ddlEnd" DataTextFormatString="{0:d}"></asp:DropDownList>
            </td>
            <td style="width: 180px;" align="center">
                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="BtnCalculateClick"/>
            </td>
        </tr>
    </table>
    <hr style="color:#666666; height:2; margin-left:5px" />
    <%--<div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />
        YTD Info:</center></div>--%>
    <br />
    <table style=" width:740; font-size:small;" cellpadding="5" cellspacing="0">
        <tr style="font-weight:bold; background-color:Silver">
            <td style="width:100px" align="right">&nbsp;</td>
            <td style="width:110px; font-size: 12pt;" align="right">Open Orders</td>
            <td style=" width:100px; font-size: 12pt;" align="right">Installed</td>
            <td style="width:110px; font-size: 12pt;" align="right">
                Sold Orders</td>
            <td style="width:100px; font-size: 12pt;" align="right">Inst. in
                <asp:Label runat="Server" ID="lblYear11"> </asp:Label>
            </td>
            <td style="width:100px; font-size: 12pt;" align="right">
                % of <asp:Label runat="Server" ID="lblYear12"> </asp:Label>
            </td>
            <td style="width:100px; font-size: 12pt;" align="right">Inst. in
                <asp:Label runat="Server" ID="lbYear21"> </asp:Label>
            </td>
            <td style="width:20px;"></td>
        </tr>
        <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">QTY</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenQty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstQty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblTotQty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblQTYlast"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblQTYproc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblQTYlast2"></asp:Label></td>
            <td></td>
        </tr>
         <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">elfa Amt</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblTotAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblAMTlast"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblAMTproc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblAMTlast2"></asp:Label></td>
            <td></td>
        </tr>
        <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">Inv. Amt</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenInv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstInv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblTotInv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblINVlast"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblINVproc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblINVlast2"></asp:Label></td>
            <td></td>
        </tr>
    </table>
    <hr style="color:#666666; height:2; margin-left:5px" />
    <table style=" width:740; font-size:small;" cellpadding="5" cellspacing="0">
        <tr>
            <td align="center" width="720">
                <br />
                <asp:HyperLink ID="hlByStore" runat="server" Target="_blank" Font-Bold="True" Font-Size="Larger" Visible="False">TCS Comparative Analysis by Store</asp:HyperLink>
                <br />
                <asp:Label runat="Server" ID="lblByStore" Font-Bold="True" Font-Size="Larger" Visible="False">for selected date range</asp:Label>
            </td>
        </tr>
    </table>
    &nbsp;
    </center>
</asp:Panel>
</asp:Content>
