<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOrgInvoice.aspx.cs" Inherits="ElfaInstall.Reports.PrintOrgInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Organization Invoice</title>
    <link href="../Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <table width="750" cellpadding="0">
            <tr>
                <td align="center" style="width:220px">
                    <img src="../images/AtHome.gif" width="200px" alt="logo"/>
                </td>
                <td style="width:300px">&nbsp;</td>
                <td align="left" style="width:230px;font-size:14px; font-weight:bold;">
                    &nbsp;The Container Store<br />
                    &nbsp;500 Freeport Pkwy Suite 100<br />
                    &nbsp;Dallas, TX 75019<br />
                </td>
            </tr>
            <tr>
                <td style="text-align:center" colspan="3">
                    <span style="font-size:16px; font-weight:bold; color:Black;  text-align:center">
                        <asp:Label ID="lblHeader" runat="server">ATHOME Organization Invoice</asp:Label>
                    </span>
                </td>
            </tr>
        </table>
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
            <tr>
                <td align="center">
                    <span style="font-size:16px; color:Black; font-family: Arial;  font-weight:bold;">Project Information</span>
                    <table width="740">
                        <tr style="height:20px">
                            <td style="width:150px" class="td12a">Store Order #:</td>
                            <td style="width:220px" class="td12a">
                                <asp:Label ID="lblOrderNumb" runat="server">060139877</asp:Label>
                            </td>
                            <td style="width:150px" class="td12a">Organizer:</td>
                            <td style="width:220px" class="td12a">
                                <asp:Label ID="lblOrganizer" runat="server">Alison Thompson</asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td12a">Store ID:</td>
                            <td class="td12a">
                                <asp:Label ID="lblStoreID" runat="server">FTW</asp:Label>
                            </td>
                            <td class="td12a">Start Date:</td>
                            <td class="td12a">
                                <asp:Label ID="lblStartDate" runat="server">2013-05-20 08:00:00</asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td12a">Sale Date:</td>
                            <td class="td12a">
                                <asp:Label ID="lblSaleDate" runat="server">2013-05-15</asp:Label>
                            </td>
                            <td class="td12a">Finish Date:</td>
                            <td class="td12a">
                                <asp:Label ID="lblFinishDate" runat="server">2013-05-20 17:00:00</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
            <tr>
                <td align="center">
                    <span style="font-size:16px; color:Black; font-family: Arial;  font-weight:bold;">Customer Information</span>
                    <table width="740">
                        <tr style="height:20px">
                            <td style="width:150px" class="td12a">Customer Name:</td>
                            <td style="width:220px" class="td12a">
                                <asp:Label ID="lblCustomer" runat="server">FTW</asp:Label>
                            </td>
                            <td style="width:150px" class="td12a">Phone Number: Home</td>
                            <td style="width:220px" class="td12a">
                                <asp:Label ID="lblPhoneHome" runat="server">FTW</asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td12a">Address:</td>
                            <td class="td12a">
                                <asp:Label ID="lblAddress" runat="server">FTW</asp:Label>
                            </td>
                            <td align="right"class="td12Rb">Cell&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td class="td12a">
                                <asp:Label ID="lblPhoneCell" runat="server">FTW</asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td12a">City, State, Zip</td>
                            <td class="td12a">
                                <asp:Label ID="lblCityState" runat="server">FTW</asp:Label>
                            </td>
                            <td class="td12a">Email:</td>
                            <td class="td12a">
                                <asp:Label ID="lblEmail" runat="server">FTW</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
            <tr>
                <td align="left">
                    <span style="font-size:16px; color:Black; font-family: Arial;  font-weight:bold;">Invoice Notes:</span>
                    <table width="740">
                        <tr>
                            <td class="td12a" colspan="4">
                                <asp:Label ID="lblComments" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
            <tr>
                <td align="center">
                    <span style="font-size:16px; color:Black; font-family: Arial;  font-weight:bold;">Invoice Amount</span>
                    <table width="740">
                        <tr style="height:20px">
                            <td style="width:150px" class="td12a">Organizing Fees:</td>
                            <td style="width:220px" class="td12a">
                                $<asp:Label ID="lblFees" runat="server" CssClass="lblmoneya"></asp:Label>
                            </td>
                            <td style="width:150px" class="td12a">Other:</td>
                            <td style="width:220px" class="td12a">
                                $<asp:Label ID="lblOther" runat="server" CssClass="lblmoneya"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td12a">Adjustment:</td>
                            <td class="td12a">
                                $<asp:Label ID="lblAdjustment" runat="server" CssClass="lblmoneya"></asp:Label>
                            </td>
                            <td class="td12a"> </td>
                            <td class="td12a"> </td>
                        </tr>
                        <tr style="height:20px">
                            <td align="right" class="td14Bold">Total:</td>
                            <td colspan="3"  class="td14Bold">
                                $<asp:Label ID="lblTotal" runat="server" CssClass="lblmoneya"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;
        <table width="750" style="border-color:Gray; border-style:solid; border-width:medium">
            <tr>
                <td align="center">
                    <span style="font-size:16px; color:Black; font-family: Arial;  font-weight:bold;">Payment Information</span>
                    <table width="740">
                        <tr style="height:25px">
                            <td style="width:150px" class="td12a">Check. Check #:</td>
                            <td style="width:220px" class="td12a"></td>
                            <td style="width:150px" class="td12a">Amount:</td>
                            <td style="width:220px" class="td12a"></td>
                        </tr>
                        <tr style="height:25px">
                            <td class="td12a">Credit Card:</td>
                            <td class="td12a"></td>
                            <td class="td12a">Billing Zip Code:</td>
                            <td class="td12a"></td>
                        </tr>
                        <tr style="height:25px">
                            <td class="td12a">Credit Card Type:</td>
                            <td class="td12a"></td>
                            <td class="td12a">Expiration:</td>
                            <td class="td12a"></td>
                        </tr>
                        <tr style="height:25px">
                            <td class="td14a">Signature:</td>
                            <td colspan="3"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfOrganizer" runat="server" />
    </center>
    </div>
    </form>
</body>
</html>
