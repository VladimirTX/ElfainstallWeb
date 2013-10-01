<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="YTDSumm.aspx.cs" Inherits="ElfaInstall.Reports.YTDSumm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#666666;"><center><br />
        YTD Info:</center></div>
    <table style=" width:730; font-size:small;" cellpadding="5" cellspacing="0">
        <tr style="font-weight:bold; background-color:Silver">
            <td style="width:90px" align="right">&nbsp;</td>
            <td style="width:110px; font-size: 12pt;" align="right">Open Orders</td>
            <td style="width:110px; font-size: 12pt;" align="right">
                Orders Created</td>
            <td style="width:100px; font-size: 12pt;" align="right">
                <asp:Label runat="Server" ID="lblYear11">2000</asp:Label>&nbsp;YTD<br />
                Created
            </td>
            <td style="width:90px; font-size: 12pt;" align="right">
                % of <asp:Label runat="Server" ID="lblYear12">2000</asp:Label>&nbsp;YTD
            </td>
            <td style="width:110px; font-size: 12pt;" align="right">
                <asp:Label runat="Server" ID="lblYear21">2000</asp:Label>&nbsp;YTD Created&nbsp;</td>
            <td style=" width:100px; font-size: 12pt;" align="right"></td>
        </tr>
        <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">QTY</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenQty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblTotQty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblQTYlast"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblQTYproc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblQTYlast2"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="zzz"></asp:Label></td>
        </tr>
         <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">elfa Amt</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblTotAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblAMTlast"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblAMTproc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblAMTlast2"></asp:Label></td>
            <td align="right"></td>
        </tr>
        <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">Inv. Amt</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenInv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblTotInv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblINVlast"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblINVproc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblINVlast2"></asp:Label></td>
            <td align="right"></td>
        </tr>
        <tr><td colspan="7"></td></tr>
    </table>
    <table style=" width:730; font-size:small;" cellpadding="5" cellspacing="0">
        <tr style="font-weight:bold; background-color:Silver">
            <td style="width:90px" align="right">&nbsp;</td>
            <td style="width:110px; font-size: 12pt;" align="right">Open Orders</td>
            <td style="width:110px; font-size: 12pt;" align="right">
                Orders Installed</td>
            <td style="width:100px; font-size: 12pt;" align="right">
                <asp:Label runat="Server" ID="lblYear11a">2000</asp:Label>&nbsp;YTD<br />
                Installed&nbsp;</td>
            <td style="width:90px; font-size: 12pt;" align="right">
                % of <asp:Label runat="Server" ID="lblYear12a">2000</asp:Label>&nbsp;YTD
            </td>
            <td style="width:110px; font-size: 12pt;" align="right">
                <asp:Label runat="Server" ID="lblYear21a">2000</asp:Label>&nbsp;YTD Installed&nbsp;</td>
            <td style=" width:100px; font-size: 12pt;" align="right"></td>
        </tr>
        <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">QTY</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenQtya"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstQty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstLastQty"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblInstLastQtyProc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstLast2Qty"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="Label5"></asp:Label></td>
        </tr>
         <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">elfa Amt</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenAmta"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstLastAmt"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblInstLastAmtProc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstLast2Amt"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="Label11"></asp:Label></td>
        </tr>
        <tr>
            <td align="right" style="font-weight:bold; font-size: 12pt;">Inv. Amt</td>
            <td align="right"><asp:Label runat="server" ID="lblOpenInva"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstInv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstLastInv"></asp:Label></td>
            <td align="right"><asp:Label runat="Server" ID="lblInstLastInvProc"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="lblInstLast2Inv"></asp:Label></td>
            <td align="right"><asp:Label runat="server" ID="Label17"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="7">
                <br />
                <asp:HyperLink ID="hlByStore" runat="server" NavigateUrl="~/Reports/YTDReport.aspx" Target="_blank" Font-Bold="True" Font-Size="Larger">YTD Report by Store</asp:HyperLink>
            </td>
        </tr>
    </table>
    &nbsp;
    </center>
</asp:Panel>
</asp:Content>
