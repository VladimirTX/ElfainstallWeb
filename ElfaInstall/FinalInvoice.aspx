<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalInvoice.aspx.cs" Inherits="ElfaInstall.FinaleInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Invoice</title>
     <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <table width="750" cellpadding="0">
            <tr>
                <td align="center" style="width:220px">
                    <img src="images/TCSIstallation.jpg" width="200px" alt="logo"/>
                </td>
                <td style="width:300px">&nbsp;</td>
                <td align="left" style="width:230px;font-size:14px; font-weight:bold;">
                    &nbsp;500 Freeport Pkwy Suite 100<br />
                    &nbsp;Coppell, TX 75019<br />
                    &nbsp;1-888-202-7622
                </td>
            </tr>
<%--            <tr><td colspan="3"><hr style=" color:Black; height:4px" /></td></tr>--%>
            <tr>
                <td style="text-align:center" colspan="3">
                    <span style="font-size:26px; font-weight:bold; color:Black;  text-align:center">
                        <asp:Label ID="lblHeader" runat="server">Invoice</asp:Label>
                    </span>
                </td>
            </tr>
<%--            <tr><td colspan="3"><hr style=" color:Black; height:4px" /></td></tr>--%>
        </table>
<%--        <img alt='""' src="images/bottom.gif" width="760px"/>--%>
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
            <tr>
                <td align="center" style="width: 743px">
                    <span style="font-size:18px; color:Black; font-family: Arial;  font-weight:bold;">Customer Information</span>
        <br />
        <table width="740" style=" color:Black">
            <tr>
                <td valign="top" class="td14a">Store Order #</td>
                <td style="font-weight: bold; font-size: 12pt;" valign="top" align="left">
                    <asp:Label ID="lblOrderID" runat="server" Width="102px"></asp:Label>
                </td>
                <td valign="top" class="td14a">Installer</td>
                <td valign="top" align="left" class="style4">
                    <asp:Label ID="lblInstaller" runat="server" Width="157px" CssClass="lblsize12a"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 150px;" valign="middle" class="td12a">Store ID</td>
                <td style="width: 230px; font-weight: bold; font-size: 11pt;" valign="middle" align="Left">
                    <asp:Label ID="lblStoreID" runat="server" Width="118px" CssClass="lblsize12a"></asp:Label>
                </td>
                <td style="width: 150px;" valign="top" class="td12a">Install Date / Time</td>
                <td style="width: 210px;" valign="top" align="left">
                    <asp:Label ID="lblInstallDate" runat="server" CssClass="lblsize12a"></asp:Label>&nbsp;/&nbsp;
                    <asp:Label ID="lblInstallTime" runat="Server" CssClass="lblsize12a"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="td12a">Sale Date</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblSaleDate" runat="server" CssClass="lblsize12a" Text="" Width="118px"></asp:Label>
                </td>
                <td valign="top" class="td12a">
<%--                    Install Preference--%>
                    Order Status
                </td>
                <td valign="top" align="left">
<%--                    <asp:Label ID="lblInstallPref" runat="server" CssClass="lblsize12a" Text="" Width="159px"></asp:Label>--%>
                    <asp:Label runat="Server" ID="lblStatus" CssClass="lblsize14" Width="200"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="td12a">
                    TCS Planner
                </td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblPlanner" CssClass="lblsize12a" Width="212px"></asp:Label>
                </td>
                <td valign="top" class="td12a">
                    &nbsp;
<%--                    Order Status--%>
                </td>
                <td align="left" style="width: 197px;">
                    <%--<asp:Label runat="Server" ID="lblStatus" CssClass="lblsize14" Width="200"></asp:Label>--%>
                </td>
            </tr>
        </table>
            <hr />
        <table width="740">
            <tr>
                <td style="width: 150px" class="td12a">Customer Name</td>
                <td align="left" style="width: 230px; font-weight: bold; font-size: 12pt;">
                    <asp:Label runat="Server" ID="lblcName"></asp:Label>
                </td>
                <td align="left" 
                    style="border-width: thin; border-style: solid solid solid solid; width:360px" 
                    class="td12a" rowspan="3" valign="top"><asp:Label runat="Server" ID="lblInstNotes"  CssClass="lblsize12a"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12a">Address</td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblcAddress1"  CssClass="lblsize12a"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcAddress2"  CssClass="lblsize12a"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12a">City, State, Zip</td>
                <td align="left" colspan="1">
                    <asp:Label runat="Server" ID="lblcCity"  CssClass="lblsize12a"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcState"  CssClass="lblsize12a"></asp:Label>,&nbsp;
                    <asp:Label runat="Server" ID="lblcZip"  CssClass="lblsize12a"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12a">Phone Number</td>
                <td align="left" colspan="2">
                    <table width="580" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 74px; height: 19px;" class="td12a">Home:</td>
                            <td style="width: 153px;  font-weight: bold; font-size: 12pt;">
                                <asp:Label ID="lblHphone" runat="server"></asp:Label> 
                            </td>
                            <td style="width: 54px; height: 19px;" class="td12a">Other:</td>
                            <td align="left" style="width: 302px; height: 19px;">
                                &nbsp;
                                <asp:Label ID="lblPhone2" runat="server" CssClass="lblsize12a" Width="120px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td12a">Email</td>
                <td align="left" colspan="2">
                    <asp:Label ID="lblEmail" runat="server" CssClass="lblsize12a"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table width="740">
            <tr>
                <td style="width: 153px" class="td12a">$<asp:Label runat="server" ID="lblDelivery">80.00</asp:Label> Delivery Option</td>
                <td style="width: 125px" align="left" >
                    <asp:CheckBox ID="cbDelivery" runat="server" Checked="True" Enabled="False" /></td>
                <td class="td12a" style="width: 79px">
<%--                    Demolition--%>
                    &nbsp;
                </td>
                <td style="width: 105px" align="left">
                    &nbsp;
<%--                    <asp:CheckBox ID="cbDemolition" runat="server" Checked="True" Enabled="False" Width="70px" Text=" " />--%>
                </td>
                <td style="width: 99px" class="td12a">&nbsp;</td>
                <td align="left">&nbsp;</td>
            </tr>
        </table>
<%--        <br />--%>
        </td></tr></table>
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
        <tr>
        <td align="center" style="width: 743px">
<%--        <span style="font-size:18px; color:Black; font-family: Arial;  font-weight:bold;">Installation Charges</span>--%>
        <table width="740" cellspacing="0" cellpadding="2">
            <tr>
                <td class="td14a" style="width: 370px">
                    Non-Sale elfa<span style="font-size: 10pt; font-weight:bold;">®</span> Purchase Price&nbsp;-&nbsp;
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="lblmoneya"></asp:Label>
                </td>
                <td class="td14a" style="width:370px; text-align:right">
                    Actual elfa<span style="font-size: 10pt; font-weight:bold;">®</span> Purchase Price&nbsp;-&nbsp;
                    <asp:Label ID="lblActual" runat="server" CssClass="lblmoneya"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="740" cellspacing="0" cellpadding="2" style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid; border-top-color: gray; border-right-style: solid; border-left-style: solid; border-right-color: gray; border-bottom-style: solid;">
            <tr>
                <td class="td12a" style="width: 370px">Installation Charge 
                </td>
                <td align="right" style="width: 120px">
                    <asp:Label ID="lblInstallPrice" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12a">Delivery</td>
                <td align="right">
                    <asp:Label ID="lblDeliveryPrice" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td class="td12a" style="width: 244px;">&nbsp;&nbsp;
                    <asp:Label runat="server" ID="lblDelivOption"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12a">
                    Additional Services ($65.00/h)</td>
                <td align="right">
                    <asp:Label ID="lblDemoPrice" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12a">
                    Additional Mileage ($3.00/mile over 20 miles)</td>
                <td align="right">
                    <asp:Label ID="lblMilesPrice" runat="server" CssClass="lblmoneya"></asp:Label>
                </td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12a">
                    Additional Painting ($65.00/hr)</td>
                <td align="right">
                    <asp:Label ID="lblMiscPrice" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12a">
                    Parking Fee/Toll Charges</td>
                <td align="right">
                    <asp:Label ID="lblParking" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12a">
                    Adjustment:</td>
                <td align="right">
                    <asp:Label ID="lblDiscount" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12a">
                    Other</td>
                <td align="right">
                    <asp:Label ID="lblOther" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;">&nbsp;</td>
            </tr>
            <asp:Panel runat="server" ID="pnlTax" Visible="false">
            <tr>
                <td class="td12a">
                    Tax</td>
                <td align="right">
                    <asp:Label ID="lblTax" runat="server" CssClass="lblmoneya"></asp:Label></td>
                <td style="width: 244px;" class="td12a">
                    &nbsp;&nbsp;Tax rate: 
                    <asp:Label ID="lblTaxRate" runat="server">0.00</asp:Label>%
                </td>
            </tr>
            </asp:Panel>
        </table>
        <table width="740" cellspacing="0" cellpadding="2">
<%--            <tr>
                <td class="td12a" style="width: 370px">&nbsp;</td>
                <td align="right" style="width: 120px">&nbsp;</td>
                <td style="width: 244px">&nbsp;</td>
            </tr>--%>
            <tr>
                <td style="width: 370px">
 <%--                   Design Change Approved by:&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" CssClass="lblmoneya">___________</asp:Label>--%>
                    &nbsp;
                </td>
                <td class="td14a" style="text-align: right; width:120px">Total Charges:</td>
                <td style="width: 244px" align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotalPrice" runat="server" CssClass="lblmoneya">$ _____________</asp:Label>
                </td>
            </tr>
        </table>
        </td>
        </tr>
        </table>
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium"><tr><td align="center" style="width: 743px">
            <tr>
                <td>
                <span style="font-size:18px; color:Black; font-family: Arial;  font-weight:bold;">Payment</span>
                <table width="720" cellpadding="5" cellspacing="0" border="0">
                    <tr>
                        <td class="td14a" style="width:200; text-align:left">By Check:</td>
                        <td class="td14a" style="width:300; text-align:left">Check # _____________________________</td>
                        <td class="td14a" style="width:220; text-align:left">Amount $ ________________</td>
                    </tr>
                    <tr>
                        <td class="td14a" align="left">By Credit Card:</td>
                        <td class="td14a" align="left">Credit Card Type ______________________</td>
                        <td class="td14a" align="left">Billing Zip Code ___________</td>
                    </tr>
                    <tr>
                        <td class="td14a" align="left" colspan="2">Credit Card # __________________________________________</td>
                        <td class="td14a" align="left">Expiration ________________</td>
                    </tr>
                    <tr>
                        <td class="td14a" style="text-align:left; height:35pt" colspan="3" valign="bottom">Signature &nbsp; &nbsp; &nbsp;__________________________________________</td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
        </center>
    </div>
    </form>
</body>
</html>
