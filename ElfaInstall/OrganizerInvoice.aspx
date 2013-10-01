<%@ Page Title="Organization Invoice" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrganizerInvoice.aspx.cs" Inherits="ElfaInstall.OrganizerInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table width="750" cellpadding="0">
            <tr>
                <td align="center" style="width:220px">
                    <img src="images/AtHome.gif" width="200px" alt="logo"/>
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
                                <asp:Label ID="lblOrganizer" runat="server">Organizer Name</asp:Label>
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
                            <td class="td12b" colspan="4">
                                <asp:TextBox ID="txtComments" runat="server" Width="720px" MaxLength="500" 
                                    Rows="2" TextMode="MultiLine"></asp:TextBox>
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
                                $<telerik:RadNumericTextBox ID="rntFees" 
                                    runat="server" AutoPostBack="true" Width="80px" 
                                    ontextchanged="RntFeesTextChanged">
                                    <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="width:150px" class="td12a">Other:</td>
                            <td style="width:220px" class="td12a">
                                $<telerik:RadNumericTextBox ID="rntOther" 
                                    runat="server" AutoPostBack="true" Width="80px" 
                                    ontextchanged="RntOtherTextChanged">
                                    <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td12a">Adjustment:</td>
                            <td class="td12a">
                                $<telerik:RadNumericTextBox 
                                    ID="rntAdjustment" runat="server" AutoPostBack="true" Width="80px" 
                                    ontextchanged="RntAdjustmentTextChanged">
                                    <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td class="td12a"> </td>
                            <td class="td12a"> </td>
                        </tr>
                        <tr style="height:20px">
                            <td align="right" class="td14Bold">Total:</td>
                            <td colspan="3"  class="td14Bold">
                                $<telerik:RadNumericTextBox ID="rntTotal" runat="server" Width="80px">
                                    <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
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
        &nbsp;
        <table width="750" cellpadding="0">
            <tr>
                <td align="center" style="width:185px">
                    <telerik:RadButton runat="server" ID="btnSave" Text=" Save Invoice "
                        Width="120px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        onclick="BtnSaveClick">
                    </telerik:RadButton>
                </td>
                <td align="center" style="width:185px">
                    <telerik:RadButton runat="server" ID="btnPrint" Text=" Print Invoice "
                        Width="125px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        AutoPostBack="False" ButtonType="LinkButton" CausesValidation="False" 
                        Target="_blank" UseSubmitBehavior="False">
                    </telerik:RadButton>
                </td>
                <td align="center" style="width:185px">
                    <telerik:RadButton runat="server" ID="btnQuote" Text=" Quote Sheet "
                        Width="125px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        AutoPostBack="False" ButtonType="LinkButton" CausesValidation="False" 
                        UseSubmitBehavior="False">
                    </telerik:RadButton>
                </td>
                <td align="center" style="width:185px">
                    <telerik:RadButton runat="server" ID="btnSaveQuote" Text=" Save Quote "
                        Width="120px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        onclick="BtnSaveQuoteClick">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
        </table>
        <asp:HiddenField ID="hfOrganizer" runat="server" />
        <telerik:RadWindow ID="rwQuoteSave" runat="server" VisibleOnPageLoad="false" Height="250px" 
            Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" 
            Behaviors="None" Title="Select File to Save" Skin="WebBlue">
            <ContentTemplate>
                <center><br /><br />
                    <div style="text-align:center; font-size:14px; font-weight:bold; color:#666666;">
                        Click "Browse..." and select file you Edited/Saved.<br /><br />
                        Then click on " Save Quote " to save your changes on the Server.
                    </div>
                    <br /><br />
                    <asp:FileUpload ID="uplFile" runat="server"/>
                    &nbsp;
                    <asp:Button runat ="server" ID="btnSaveFile" Text=" Save Quote " OnClick="BtnSaveFileClick"/>
                    <br /><br /><br />
                    <asp:Button ID="btnSaveCancel" runat="server" Text="Cancel" Width="100px" 
                        OnClick="BtnSaveCancelClick"/>
                </center>
            </ContentTemplate>
        </telerik:RadWindow>
    </center>
</asp:Content>
