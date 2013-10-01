<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Vendor_Order.aspx.cs" Inherits="ElfaInstall.VendorOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table width="740">
            <tr>
                <td style="width: 150px;" valign="top" class="td12">Store Order #</td>
                <td style="width: 230px;" valign="top" align="left">
                    <asp:Label ID="lblOrderID" runat="server" Width="102px" CssClass="lblsize12"></asp:Label>
                </td>
                <td style="width: 140px;" valign="top" class="td12">Installer</td>
                <td style="width: 210px;" valign="top" align="left">
                    <asp:Label ID="lblInstaller" runat="server" Width="157px" CssClass="lblsize12"></asp:Label></td>
            </tr>
            <tr>
                <td valign="top" class="td12">Store ID</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblStoreID" runat="server" CssClass="lblsize12" Width="118px"></asp:Label>
                </td>
                <td valign="top" class="td12">Install Date / Time</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblInstallDate" runat="server" CssClass="lblsize12"></asp:Label>&nbsp;/&nbsp;
                    <asp:Label ID="lblInstallTime" runat="Server" CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="td12">Sale Date</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblSaleDate" runat="server" CssClass="lblsize12" Text="" Width="118px"></asp:Label>
                </td>
                <td valign="top" class="td12">Install Preference</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblInstallPref" runat="server" CssClass="lblsize12" Text="" Width="159px"></asp:Label></td>
            </tr>
            <tr>
                <td valign="top" class="td12">
                    TCS Planner
                </td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblPlanner" CssClass="lblsize12" Width="212px"></asp:Label>
                </td>
                <td valign="top" class="td12">
                    Order Status
                </td>
                <td align="left" style="background-color:#BFC7A4; text-indent:5px; width: 197px;">
                    <asp:Label runat="Server" ID="lblStatus" CssClass="lblsize14" Width="200"></asp:Label>
                </td>
            </tr>
        </table>
    <br />
        <table width="740">
            <tr>
                <td style="width: 150px" class="td12">Customer Name</td>
                <td align="left" colspan="3">
                    <asp:Label runat="Server" ID="lblcName"  CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Address</td>
                <td align="left" colspan="3">
                    <asp:Label runat="Server" ID="lblcAddress1"  CssClass="lblsize12"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcAddress2"  CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">City, State, Zip</td>
                <td align="left" colspan="3">
                    <asp:Label runat="Server" ID="lblcCity"  CssClass="lblsize12"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcState"  CssClass="lblsize12"></asp:Label>,&nbsp;
                    <asp:Label runat="Server" ID="lblcZip"  CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Phone Number</td>
                <td align="left" colspan="3">
                    <table width="580" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 80px;" class="td12">Home:</td>
                            <td style="width: 150px;" align="left">
                                <asp:Label ID="lblHphone" runat="server" CssClass="lblsize12"></asp:Label>
                            </td>
                            <td style="width: 54px;" class="td12">Other:</td>
                            <td align="left" style="width: 302px;">
                                <asp:Label ID="lblPhone2" runat="server" CssClass="lblsize12" Width="120px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td12">Email</td>
                <td align="left" style="width:225px">
                    <asp:Label ID="lblEmail" runat="server" CssClass="lblsize12"></asp:Label>
                </td>
                <td class="td12" style="width:130px">
<%--                    Install.Notes:--%>
                </td>
                <td align="left" style="width:220px">
                    <%--<asp:Label ID="lblInstNotes" runat="server" CssClass="lblsize12"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td class="tdtitlesmall">
                    <asp:HyperLink ID="hlAddress" runat="server" 
                        NavigateUrl="https://maps.google.com/maps?q=" Target="_blank" >Click for Map</asp:HyperLink>
                </td>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
        <table width="740">
            <tr>
                <td style="width: 153px" class="td12">$<asp:Label runat="server" ID="lblDelivery">80.00</asp:Label> Delivery Option</td>
                <td style="width: 125px" align="left" >
                    <asp:CheckBox ID="cbDelivery" runat="server" Checked="True" Enabled="False" Text=" "/></td>
                <td class="td12" style="width: 79px">Demolition</td>
                <td style="width: 105px" align="left">
                    <asp:CheckBox ID="cbDemolition" runat="server" Checked="True" Enabled="False" Width="70px" Text=" " /></td>
                <td style="width: 99px" class="td12">Scope of Demo:</td>
                <td align="left">
                    <asp:Label ID="lblDemo" runat="server" Width="131px" CssClass="lblsize12"></asp:Label></td>
            </tr>
        </table>
        <br />
        <div style="width:740px; font-size:12px; font-weight:bold; color:#666666">&nbsp;TCS Notes:
            <asp:Label ID="lblPromo" runat="server" Text="Promo!" Font-Bold="true" ForeColor="red" Font-Size="Large" Visible="false"></asp:Label>
        </div>
        <table cellpadding="5" cellspacing="0" width="740" border="1" style="border-color:#666666;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblSolution" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <div style="width:760px; font-size:14px; font-weight:bold; text-align:left; color:#666666">&nbsp;&nbsp;Installation Charges:</div>
        <table width="740">
            <tr>
                <td class="td12" style="width: 370px">
                    Non-Sale Retail Purchase Price</td>
                <td align="right" style="width:120px;">
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="lblmoney"></asp:Label>
                </td>
                <td style="width:244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12"><asp:Label runat="server" ID="lblProc">30</asp:Label>% Basic Installation Charge</td>
                <td align="right">
                    <asp:Label ID="lblInstallPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Delivery Option</td>
                <td align="right">
                    <asp:Label ID="lblDeliveryPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Addition Demo Required ($65.00/hr)</td>
                <td align="right">
                    <asp:Label ID="lblDemoPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Additional Miles</td>
                <td align="right">
                    <asp:Label ID="lblMiles" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Additional Painting ($65.00/hr)</td>
                <td align="right">
                    <asp:Label ID="lblMisc" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Parking/Toll</td>
                <td align="right">
                    <asp:Label ID="lblParking" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Other/Tip</td>
                <td align="right">
                    <asp:Label ID="lblTips" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
             <tr>
                <td class="td12">
                    Adjustment</td>
                <td align="right">
                    <asp:Label ID="lblDiscount" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
             <tr>
                <td class="td12">
                    Tax</td>
                <td align="right">
                    <asp:Label ID="lblTax" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td14">
                    Total Installation Charges:</td>
                <td align="right">
                    <asp:Label ID="lblTotalPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
        </table>
        <br />
        <div style="width:740px; font-size:12px; font-weight:bold; color:#666666">&nbsp;Comments to Installer:</div>
        <table cellpadding="3" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblComments" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="width:740px; font-size:12px; font-weight:bold; color:#666666">&nbsp;Invoice Comments:</div>
        <table cellpadding="3" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblInvoice" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="width:740px;">
            <tr>
                <td align="center">
                    <asp:HyperLink ID="hlImages" runat="server" Target="_blank" Font-Bold="True">Order Images</asp:HyperLink>
                </td>
                <td align="center">
                    <asp:HyperLink ID="hlAtHome" runat="server" Target="_blank" Font-Bold="True" Visible="false">AtHome CheckList</asp:HyperLink>
                </td>
            </tr>
        </table>
        <br />
        <table style="width:740px;">
            <tr>
                <td style="width:33%; text-align:center">
                    <asp:HyperLink ID="lnkPrint" CssClass="btnsubmit" runat="server" Width="140px" Height="22px" Target="_blank" Font-Bold="True">Print Invoice</asp:HyperLink>
                </td>
                <td style="width:33%; text-align:center">
                    &nbsp;<asp:Button ID="btnReturn" runat="server" CssClass="btnmenu" OnClick="BtnReturnClick"
                        Text=" Return " />
                </td>
                <td style="width:33%; text-align:center">
                    &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btnsubmit" 
                        Text=" Cancel Order " UseSubmitBehavior="False" OnClick="BtnCancelClick" 
                        Enabled="False" Visible="False" />
                    <asp:Button ID="btnRequest" runat="server" CssClass="btnsubmit" 
                        Text="Request new plans" Visible="false" Width="160px" 
                        onclick="BtnRequestClick" />
                </td>
            </tr>
        </table>
        <telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false" Height="250px"
	                Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="None" Title="Send Plans Request">
	        <ContentTemplate>
	            <div style="padding: 20px">
	                You can use default comment or type yours.
	                <br /><br />
                    Comment: <asp:TextBox ID="txtComments" runat="server" Width="300px" MaxLength="300"></asp:TextBox><br /><br /><br /><br />
                    <center>
                    <asp:Button ID="btnOk" runat="server" Text="Email request" Width="150px" OnClick="BtnOkClick" />
                    &nbsp;&nbsp;&nbsp;
	                <asp:Button ID="btnNo" runat="server" Text="Cancel" Width="100px" OnClick="BtnNoClick" />
                    </center>
	            </div>
	        </ContentTemplate>
        </telerik:RadWindow>
        &nbsp;
    </center>
</asp:Panel>
</asp:Content>
