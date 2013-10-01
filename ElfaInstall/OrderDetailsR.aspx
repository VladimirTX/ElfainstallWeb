<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrderDetailsR.aspx.cs" Inherits="ElfaInstall.OrderDetailsR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#556b2f;">
    <center>Order Information</center></div>
    <br />
    <table width="740">
        <tr>
            <td style="width: 155px;" valign="top" class="td12">Store Order #</td>
            <td style="width: 229px;" valign="top" align="Left">
                <asp:Label ID="lblOrderID" runat="server" Width="102px" CssClass="lblsize12"></asp:Label>
            </td>
            <td style="width: 152px;" valign="top" class="td12">Installer</td>
            <td style="width: 197px;" valign="top" align="Left">
                <asp:Label ID="lblInstaller" runat="server" Width="157px" CssClass="lblsize12"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" class="td12">Store ID</td>
            <td valign="top" align="Left">
                <asp:Label ID="lblStoreID" runat="server" CssClass="lblsize12" Width="118px"></asp:Label>
            </td>
            <td valign="top" class="td12">Install Date / Time</td>
            <td valign="top" align="Left">
                <asp:Label ID="lblInstallDate" runat="server" CssClass="lblsize12"></asp:Label>&nbsp;/
                &nbsp;<asp:Label ID="lblInstallTime" runat="Server" CssClass="lblsize12"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" class="td12">Sale Date</td>
            <td valign="top" align="Left">
                <asp:Label ID="lblSaleDate" runat="server" CssClass="lblsize12" Text="" Width="118px"></asp:Label>
            </td>
            <td valign="top" class="td12">Install Preference</td>
            <td style="text-indent:5px;" valign="top" align="Left">
                <asp:Label ID="lblInstallPref" runat="server" CssClass="lblsize12" Text="" Width="159px"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" class="td12">
                TCS Planner
            </td>
            <td align="Left">
                <asp:Label runat="Server" ID="lblPlanner" CssClass="lblsize12" Width="212px"></asp:Label>
            </td>
            <td valign="top" class="td12">
                Order Status
            </td>
            <td align="Left" style="background-color:#C4C2C2; text-indent:5px; width: 197px;">
                <asp:Label runat="Server" ID="lblStatus" CssClass="lblsize14" Width="200"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="740">
        <tr>
            <td style="width: 155px; height:15px;"></td><td style="width: 240px;"></td>
            <td style="width: 345px;" valign="top" align="left" rowspan="4">
                <asp:Label ID="lblCall1" runat="server" CssClass="lblsize12" Text=" "></asp:Label><br />
                <asp:Label ID="lblCall2" runat="server" CssClass="lblsize12" Text=" "></asp:Label><br />
                <asp:Label ID="lblCall3" runat="server" CssClass="lblsize12" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td12">Customer Name</td>
            <td align="Left">
                <asp:Label runat="Server" ID="lblcName"  CssClass="lblsize12"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td12">Address</td>
            <td align="Left">
                <asp:Label runat="Server" ID="lblcAddress1"  CssClass="lblsize12"></asp:Label>&nbsp;
                <asp:Label runat="Server" ID="lblcAddress2"  CssClass="lblsize12"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td12">City, State, Zip</td>
            <td align="Left">
                <asp:Label runat="Server" ID="lblcCity"  CssClass="lblsize12"></asp:Label>&nbsp;
                <asp:Label runat="Server" ID="lblcState"  CssClass="lblsize12"></asp:Label>,&nbsp;
                <asp:Label runat="Server" ID="lblcZip"  CssClass="lblsize12"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="740">
        <tr>
            <td style="width: 160px;" class="td12">Phone Number</td>
            <td align="Left" style="width: 565px;">
                <table width="580" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 74px; height: 19px;" class="td12">Home:</td>
                        <td style="width: 153px; height: 19px;" align="Left">
                            <asp:Label ID="lblHphone" runat="server" CssClass="lblsize12"></asp:Label>
                        </td>
                        <td style="width: 54px; height: 19px;" class="td12">Other:</td>
                        <td align="Left" style="width: 302px; height: 19px;">
                            <asp:Label ID="lblPhone2" runat="server" CssClass="lblsize12" Width="120px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 160px;" class="td12">Email</td>
            <td align="Left" style="width: 565px">
                <asp:Label ID="lblEmail" runat="server" CssClass="lblsize12"></asp:Label>
            </td>
        </tr>
    </table>
   <br />
    <table width="740">
        <tr>
            <td style="width: 153px" class="td12">$<asp:Label runat="server" ID="lblDelivery">80.00</asp:Label> Delivery Option</td>
            <td style="width: 125px" align="Left" >
                <asp:CheckBox ID="cbDelivery" runat="server" Checked="True" Enabled="False" /></td>
            <td class="td12" style="width: 79px">Demolition</td>
            <td style="width: 105px" align="Left">
                <asp:CheckBox ID="cbDemolition" runat="server" Checked="True" Enabled="False" Width="70px" Text=" " /></td>
            <td style="width: 99px" class="td12">Scope of Demo:</td>
            <td align="Left">
                <asp:Label ID="lblDemo" runat="server" Width="131px" CssClass="lblsize12"></asp:Label></td>
        </tr>
    </table>
    <br />
    <div style="width:740px; font-size:12px; font-weight:bold;">&nbsp;Current solution description:</div>
    <table cellpadding="5" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
        <tr>
            <td align="left">
                <asp:Label ID="lblSolution" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div style="width:760px; font-size:14px; font-weight:bold; text-align:left;">&nbsp;&nbsp;Installation Charges:</div>
    <table width="740">
        <tr>
            <td class="td12" style="width: 370px; height: 21px;">
                Non-Sale Retail Purchase Price</td>
            <td align="Right" style="width:120px; height: 21px;">
                <asp:Label ID="lblPurchasePrice" runat="server" CssClass="lblmoney"></asp:Label>
            </td>
            <td style="width:244px; height: 21px;">&nbsp;</td>
        </tr>
        <tr>
            <td class="td12"><asp:Label runat="server" ID="lblProc">30</asp:Label>% Basic Installation Charge</td>
            <td align="Right">
                <asp:Label ID="lblInstallPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
            <td style="width: 244px">&nbsp;</td>
        </tr>
        <tr>
            <td class="td12">
                Delivery Option</td>
            <td align="Right">
                <asp:Label ID="lblDeliveryPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
            <td style="width: 244px">&nbsp;</td>
        </tr>
        <tr>
            <td class="td12">
                Additional Miles</td>
            <td align="Right">
                <asp:Label ID="lblMilesPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
            <td style="width: 244px">&nbsp;</td>
        </tr>
        <tr>
            <td class="td12">
                Addition Demo Required ($65.00/hr)</td>
            <td align="Right">
                <asp:Label ID="lblDemoPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
            <td style="width: 244px">&nbsp;</td>
        </tr>
        <tr>
            <td class="td12">
                Misc Charges</td>
            <td align="Right">
                <asp:Label ID="lblMisc" runat="server" CssClass="lblmoney"></asp:Label></td>
            <td style="width: 244px">&nbsp;</td>
        </tr>
        <tr>
            <td class="td14">
                Total Installation Charges:</td>
            <td align="Right">
                <asp:Label ID="lblTotalPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
            <td style="width: 244px">&nbsp;</td>
        </tr>
    </table>
    <br />
    <div style="width:740px; font-size:12px; font-weight:bold;">&nbsp;Additional Comments:</div>
    <table cellpadding="5" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
        <tr>
            <td align="left">
                <asp:Label ID="lblComments" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:HyperLink ID="hlImages" runat="server" Target="_blank" Font-Bold="True">Order Images</asp:HyperLink>
<%--    <asp:Panel ID="pnlImages" runat="server" Width="740px" BorderStyle="Solid" BorderWidth="1" BorderColor="#bdb76b" Visible="false">
        <table style="width:740px;">
            <tr>
                <td style="width:300px; text-align:right; font-weight:bold; height: 21px;">Uploaded Images:&nbsp;</td>
                <td style="width:440px; text-align:left; height: 21px;">
                    <asp:Panel runat="Server" ID="pnlLinks" Font-Size="14px">
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>--%>
    <br /><br />
    <table style="width:740px;">
        <tr>
            <td style="width:200; text-align:center">
                <asp:Button ID="btnEdit" runat="server" Text="Edit Order Info" CssClass="btnsubmit" Width="150px" OnClick="btnEdit_Click" />
                </td>
            <td style="width:200; text-align:center">
                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btnsubmit" Width="150px" CausesValidation="False" UseSubmitBehavior="False" OnClick="btnPrint_Click" />&nbsp;
             </td>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnReturn" runat="server" CssClass="btnmenu" OnClick="btnReturn_Click"
                    Text=" Return " /></td>
        </tr>
    </table>
    <br />
    <asp:HiddenField ID="hfOrderID" runat="server" />
    </center>
</asp:Content>
