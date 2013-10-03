<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="ElfaInstall.EditOrder" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<%@ Register Assembly="msgBox" TagPrefix="cc1" Namespace="BunnyBear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <div style="font-size:22px; font-weight:bold; color:#666666;">
            <center>Edit Order Info&nbsp;&nbsp;
            <asp:Label ID="lblPromo" runat="server" Text="At Home" ForeColor="Red" Visible="false"></asp:Label>
            </center></div>
        <table width="760" border="0">
            <tr>
                <td style="width: 120px;" class="td12">Store Order #</td>
                <td style="width: 220px;" align="left"><asp:Label ID="lblOrderID" runat="server" Width="102px" CssClass="lblsize12"></asp:Label></td>
                <td style="width: 420px;" align="left" colspan="2" rowspan="3">
                    <asp:Panel ID="pnlRegular" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 100px;" class="td12">Installer</td>
                                <td style="width: 320px">
                                    <asp:Label ID = "lblInstaller" runat="server" CssClass="lblsize14" Text="Installer"></asp:Label>
                                    <asp:DropDownList ID="ddlInstallers" runat="server" Width="110px" 
                                        onselectedindexchanged="DdlInstallersSelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;&nbsp;
                                    <asp:Button ID="btnInstallers" runat="server" CssClass="btnsmall" Text="Reset" 
                                        Visible="False" onclick="BtnInstallersClick" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td12">Install. Date:</td>
                                <td align="left">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <telerik:RadDatePicker ID="rdpInstallDate" runat="server" 
                                                    onselecteddatechanged="RdpInstallDateSelectedDateChanged">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="left">
                                                &nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="btnReset" runat="server" CssClass="btnsmall" Text="Reset" OnClick="BtnResetClick" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td12">Install. Time:</td>
                                <td align="left" class="td12">
                                    Start:
                                    <telerik:RadTimePicker ID="rtpTimeStart" runat="server" Width="90px"
                                        TimeView-EndTime="19:00:00" TimeView-StartTime="08:00:00" 
                                        TimeView-Interval="02:00:00" 
                                        onselecteddatechanged="RtpTimeStartSelectedDateChanged">
                                    </telerik:RadTimePicker>
                                    &nbsp;&nbsp;
                                    End:
                                    <telerik:RadTimePicker ID="rtpTimeEnd" runat="server" Width="90px"
                                        TimeView-EndTime="21:00:00" TimeView-StartTime="10:00:00" 
                                        TimeView-Interval="02:00:00" 
                                        onselecteddatechanged="RtpTimeEndSelectedDateChanged">
                                    </telerik:RadTimePicker>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlAtHome" runat="server" Visible="false">
                        <table width="100%" cellpadding="0" cellspacing="5">
                            <tr valign="middle">
                                <td align="right">
                                    <telerik:RadButton runat="server" ID="btnViewATH" Text=" View At Home Check List "
                                        Width="180px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                                        Visible="false" onclick="BtnViewAthClick">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr valign="middle">
                                <td align="right">
                                    <telerik:RadButton runat="server" ID="btnPrint" Text=" Print Order Info "
                                        Width="175px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                                        AutoPostBack="False" ButtonType="LinkButton" CausesValidation="False" 
                                        Target="_blank" UseSubmitBehavior="False">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="td12">Store ID</td>
                <td align="left">
                    <asp:Label ID="lblStoreID" runat="server" CssClass="lblsize12" Width="50px"></asp:Label>
                    <asp:Button runat="server" ID="btnChange" Font-Size="Smaller" Height="20px" 
                        Text="Change Store" onclick="BtnChangeClick" />
                </td>
            </tr>
            <tr>
                <td class="td12">Sale Date</td>
                <td align="left">
                    <asp:Label ID="lblSaleDate" runat="server" CssClass="lblsize12" Text="" Width="118px"></asp:Label></td>
            </tr>
            <tr>
                <td class="td12" style="height: 3px; border-bottom-style:solid">
                    TCS Planner</td>
                <td align="left" style="height: 3px; border-bottom-style:solid">
                    <asp:Label runat="Server" ID="lblPlanner" CssClass="lblsize12" Width="212px"></asp:Label></td>
                <td class="td12" style="height: 3px; width:170px">Date confirmed</td>
                <td align="left" style="width: 250px; height: 3px;">
                    <asp:CheckBox ID="cbConfirmed" runat="server" Text=" " />
                    <asp:Label runat="Server" ID="lblConfirmed" Font-Size="X-Small">(By phone)
                    </asp:Label>
                    <asp:CheckBox ID="cbBySurvey" runat="server" Text=" " />
                    <asp:Label runat="Server" ID="lblBySurvey" Font-Size="X-Small">(By survey)
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Customer Name</td>
                <td align="left">
<%--                    <asp:Label runat="Server" ID="lblcName"  CssClass="lblsize12"></asp:Label>--%>
                    <asp:TextBox runat="server" ID="txtFname" Width="80px" Font-Size="10pt" 
                        MaxLength="25"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtLname" Width="110px" Font-Size="10pt" 
                        MaxLength="25"></asp:TextBox>
                </td>
                <td class="td12">Installation Completed</td>
                <td align="left" style="height: 9px;"><asp:CheckBox ID="cbCompleted" runat="server" Text=" " />
                    <asp:Label runat="Server" ID="lblWarning" Font-Size="X-Small" Visible="False" Width="200px">
                        (Make sure installation date is correct and Payment type selected)
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Address</td>
                <td align="left">
<%--                    <asp:Label runat="Server" ID="lblcAddress1"  CssClass="lblsize12"></asp:Label>
                    <asp:Label runat="Server" ID="lblcAddress2"  CssClass="lblsize12"></asp:Label>--%>
                    <asp:TextBox runat="server" ID="txtAddr1" Width="130px" Font-Size="10pt" 
                        MaxLength="50"></asp:TextBox> 
                    <asp:TextBox runat="server" ID="txtAddr2" Width="60px" Font-Size="10pt" 
                        MaxLength="20"></asp:TextBox> 
                </td>
                <td class="td12">Payment Type</td>
                <td align="left" class="td12">
                    <asp:DropDownList runat="server" ID="ddlPaymentType">
                        <asp:ListItem Value="0">_</asp:ListItem>
                        <asp:ListItem Value="4">Square</asp:ListItem>
                        <asp:ListItem Value="1">Credit Card</asp:ListItem>
                        <asp:ListItem Value="2">Check</asp:ListItem>
                        <asp:ListItem Value="3">N/C</asp:ListItem>
                        <asp:ListItem Value="5">Invoice</asp:ListItem>
                    </asp:DropDownList>
                   <%-- <telerik:RadDatePicker ID="rdpPayment" runat="server">
                    </telerik:RadDatePicker>--%>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    City, State, Zip</td>
                <td align="left">
<%--                    <asp:Label runat="Server" ID="lblcCity"  CssClass="lblsize12"></asp:Label>,&nbsp;
                    <asp:Label runat="Server" ID="lblcState"  CssClass="lblsize12"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcZip"  CssClass="lblsize12"></asp:Label>--%>
                    <asp:TextBox runat="server" ID="txtCity" Width="90px" Font-Size="10pt" MaxLength="25"></asp:TextBox>
                    <asp:DropDownList ID="ddlStates" runat="server" Width="50px" Font-Size="10pt">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="txtZip" Width="45px" Font-Size="10pt" MaxLength="10"></asp:TextBox>
                </td>
                <td class="td12">
                    Payment Received Date
<%--                    <asp:Label ID="lblClose" runat="server" Text="Follow-up call confirmed"></asp:Label>&nbsp;--%>
                </td>
                <td align="left">
                     <telerik:RadDatePicker ID="rdpPayment" runat="server">
                    </telerik:RadDatePicker>
<%--                    &nbsp;<asp:Button ID="btnClose" runat="server" CssClass="btnsmall" Text="Yes, Close Order" ValidationGroup="Closed" OnClick="BtnCloseClick" />--%>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Phone Number</td>
                <td class="td12">
                    Home:&nbsp;&nbsp;
                    <asp:TextBox ID="txtHphone" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                </td>
                <td class="td12">
                    <asp:Label ID="lblClose" runat="server" Text="Follow-up call confirmed"></asp:Label>&nbsp;
                </td>
                <td align="center" style="vertical-align:bottom">
                    &nbsp;<asp:Button ID="btnClose" runat="server" CssClass="btnsmall" Text="Yes, Close Order" ValidationGroup="Closed" OnClick="BtnCloseClick" />
<%--                    <asp:Button ID="btnCallLog" runat="server" CssClass="btnsmall" 
                        Text="Edit Call Log" onclick="BtnCallLogClick" />--%>
                </td>
            </tr>
            <tr>
                <td class="td12"></td>
                <td class="td12">
                    Other:&nbsp;&nbsp;
                    <asp:TextBox ID="txtPhone2" runat="server" MaxLength="15" Width="120px"></asp:TextBox></td>
                <td colspan="2" align="center" style="vertical-align:bottom">
                    <asp:Button ID="btnCallLog" runat="server" CssClass="btnsmall" 
                        Text="Edit Call Log" onclick="BtnCallLogClick" />
                </td>
            </tr>
        </table>
        <table width="760px">
            <tr>
                <td class="td12" width="160px">Email:</td>
                <td width="220px" colspan="3" align="left"><asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                <td class="td12" width="80px" rowspan="2">Office Notes:</td>
                <td width="302" rowspan="2" valign="top">
                    <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="80px" Width="360px">
                        <telerik:RadPane ID="RadPane2" runat="server" Width="360px" Scrolling="Y">
                            <asp:Label ID="lblOffice" runat="server" Height="80px" Width="340px" CssClass="lblsize12"></asp:Label>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </td>
            </tr>
            <tr>
                <td  class="td12" width="160">$<asp:Label runat="server" ID="lblDelivery">80.00</asp:Label> Delivery Option</td>
                <td width="60px" align="left"><asp:CheckBox ID="cbDelivery" runat="server" Checked="True" Text=" " Width="30px" /></td>
                <td class="td12" width="120px"><asp:Label ID="lblPickedUp" runat="server" Visible="false">Picked Up</asp:Label></td>
                <td width="40px" align="left"><asp:CheckBox ID="cbPickedUp" runat="server" Text=" " Width="30px" Visible="false"/></td>
            </tr>
<%--            <tr>
                <td class="td12">Scope of Demo:</td>
                <td align="left"><asp:Label ID="lblDemo" runat="server" Width="50px" CssClass="lblsize12"></asp:Label></td>
                <td class="td12">Demolition</td>
                <td align="left"><asp:CheckBox ID="cbDemolition" runat="server" Checked="True" Width="40px" Text=" " /></td>
            </tr>--%>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button runat="server" ID="btnSave1"  Text="  Save  " CssClass="btnsubmit" 
                        Width="140px" onclick="BtnSave1Click"/>
                </td>
                <td class="td12"><asp:Label runat="server" ID="lblAddNote" CssClass="lblsize12">Add Note</asp:Label>:</td>
                <td align="left">
                    <asp:TextBox ID="txtOffice" runat="server" Width="355px" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table width="740px">
            <tr>
                <td style="width:300px" class="td12Rb">Order Special Status:</td>
                <td style="width:200px" align="center">
                    <asp:DropDownList ID="ddlSpecialStatus" runat="server">
                        <asp:ListItem Selected="True" Value="0">_</asp:ListItem>
                        <asp:ListItem Value="1">Backorder until</asp:ListItem>
                        <asp:ListItem Value="2">Under Construction</asp:ListItem>
                        <asp:ListItem Value="3">Customer Rescheduling</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:200px">
                    <telerik:RadDatePicker ID="rdpSpecial" runat="server"></telerik:RadDatePicker>
                </td>
                <td style="width:100px">
                    <asp:Button CssClass="btnsmall" ID="btnSpecial" runat="server" Text=" Save " 
                        onclick="BtnSpecialClick" />
                </td>
            </tr>
        </table>
        &nbsp;<div style="width:740px; font-size:12px; font-weight:bold;">
            &nbsp;TCS Notes:</div>
        <table border="1" cellpadding="5" cellspacing="0" style="border-color:#666666;" 
            width="740">
            <tr>
                <td align="left">
                    <asp:TextBox ID="txtSolution" runat="server" ReadOnly="True" Rows="2" 
                        TextMode="MultiLine" Width="740"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div style="width:760px; font-size:14px; font-weight:bold; text-align:left;">
            &nbsp;&nbsp;Installation Charges:</div>
        <table width="760">
            <tr>
                <td class="td12" style="width: 320px">
                    Non-Sale Retail Purchase Price</td>
                <td align="right" style="width:120px;">
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="lblmoney"></asp:Label>
                </td>
                <td style="width:300px;">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btnsmall" 
                        OnClick="BtnUpdateClick" Text="Change Price" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    <%--                    <asp:Label runat="server" ID="lblProc">30</asp:Label>% --%>
                    Basic Installation Charge</td>
                <td class="lblmoney">
                    <%--                    $<asp:TextBox ID="txtInstallPrice" runat="Server" Width="80px" CssClass="txtmoney" AutoPostBack="True" OnTextChanged="txtInstallPrice_TextChanged"></asp:TextBox>--%>
                    $<telerik:RadNumericTextBox ID="rntInstallPrice" runat="server" 
                        AutoPostBack="True" ontextchanged="RntInstallPriceTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td align="center" style="width: 249px">
                    <%--<telerik:RadButton runat="server" ID="btnViewATH" Text=" View At Home Check List "
                        Width="180px" Font-Bold="True" Font-Size="10pt" Skin="Forest" 
                        Visible="false" onclick="BtnViewAthClick">
                    </telerik:RadButton>--%>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Delivery Option</td>
                <td class="lblmoney">
                    <%--                    $<asp:TextBox ID="txtDeliveryPrice" runat="Server" Width="80px" CssClass="txtmoney" AutoPostBack="True" OnTextChanged="txtDeliveryPrice_TextChanged"></asp:TextBox>--%>
                    $<telerik:RadNumericTextBox ID="rntDeliveryPrice" runat="server" 
                        AutoPostBack="true" ontextchanged="RntDeliveryPriceTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 249px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Additional Miles</td>
                <td class="lblmoney">
                    <%--                    $<asp:TextBox ID="txtAddMiles" runat="Server" Width="80px" CssClass="txtmoney" AutoPostBack="True" OnTextChanged="txtAddMiles_TextChanged"></asp:TextBox>--%>
                    $<telerik:RadNumericTextBox ID="rntAddMiles" runat="server" AutoPostBack="true" 
                        ontextchanged="RntAddMilesTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 249px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Addition Demo Required ($65.00/hr)</td>
                <td class="lblmoney">
                    <%--$<asp:TextBox ID="txtDemoPrice" runat="Server" Width="80px" CssClass="txtmoney" AutoPostBack="True" OnTextChanged="txtDemoPrice_TextChanged"></asp:TextBox>--%>
                    $<telerik:RadNumericTextBox ID="rntDemoPrice" runat="server" 
                        AutoPostBack="true" ontextchanged="RntDemoPriceTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 249px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Aditional Painting ($65.00/hr)</td>
                <td class="lblmoney">
                    <%--$<asp:TextBox ID="txtMisc" runat="Server" Width="80px" CssClass="txtmoney" AutoPostBack="True" OnTextChanged="txtMisc_TextChanged"></asp:TextBox>--%>
                    $<telerik:RadNumericTextBox ID="rntMisc" runat="server" AutoPostBack="true" 
                        ontextchanged="RntMiscTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 249px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Parking/Toll:</td>
                <td class="lblmoney">
                    <%--                    $ <asp:Label ID="lblParking" runat="server" CssClass="lblmoney"></asp:Label></td>--%>
                    $<telerik:RadNumericTextBox ID="rntParking" runat="server" AutoPostBack="true" 
                        ontextchanged="RntParkingTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 249px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Other:</td>
                <td class="lblmoney">
                    $<telerik:RadNumericTextBox ID="rntTip" runat="server" AutoPostBack="true" 
                        ontextchanged="RntTipTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 249px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Adjustment:</td>
                <td class="lblmoney">
                    $<telerik:RadNumericTextBox ID="rntDiscount" runat="server" AutoPostBack="true" 
                        ontextchanged="RntDiscTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td align="right" valign="top">
                    <asp:DropDownList ID="ddlReasons" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Tax:</td>
                <td class="lblmoney">
                    $<telerik:RadNumericTextBox ID="rntTax" runat="server" AutoPostBack="true" 
                        ontextchanged="RntTaxTextChanged" Width="80px">
                        <EnabledStyle Font-Bold="True" HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    <asp:Button ID="btnTax" runat="server" CssClass="btnsmall" 
                        onclick="BtnTaxClick" Text="Calculate Tax" Visible="false" />
                    &nbsp;
                    <asp:CheckBox ID="cbExempt" runat="server" AutoPostBack="True" 
                        CssClass="lblsize12" oncheckedchanged="CbExemptCheckedChanged" 
                        Text="Tax Exempt" Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Total Installation Charges:</td>
                <td align="right">
                    <asp:Label ID="lblTotalPrice" runat="server" CssClass="lblmoney"></asp:Label>
                </td>
                <td align="right" style="font-size:12px; font-weight:bold;">
                    Order Option:&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlOption" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DdlOptionSelectedIndexChanged">
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
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <div style="width:740px; font-size:12px; font-weight:bold;">
            &nbsp;Comments to Installer:</div>
        <table border="1" cellpadding="3" cellspacing="0" style="border-color:#666666;" 
            width="740">
            <tr>
                <td align="left">
                    <asp:TextBox ID="txtComments" runat="server" MaxLength="1500" Rows="3" 
                        TextMode="MultiLine" Width="740"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="width:740px; font-size:12px; font-weight:bold;">
            &nbsp;Invoice Comments:</div>
        <table border="1" cellpadding="3" cellspacing="0" style="border-color:#666666;" 
            width="740">
            <tr>
                <td align="left">
                    <asp:TextBox ID="txtInvoices" runat="server" MaxLength="300" Rows="2" 
                        TextMode="MultiLine" Width="740"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table style="width:740px;">
            <tr>
                <td style="text-align:center; width: 250px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="btnsubmit" 
                        OnClick="BtnSaveClick" Text="  Save  " Width="140px" />
                </td>
                <td style="text-align:center; width:250px">
                    <asp:Button ID="btnReturn" runat="server" CausesValidation="False" 
                        CssClass="btnmenu" onclick="BtnReturnClick" Text="  Return  " />
                </td>
                <td align="center">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btnsubmit" 
                        onclick="BtnCancelClick" Text=" Cancel Order " UseSubmitBehavior="False" 
                        Visible="False" Width="140px" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="btnmenu" 
                        OnClick="BtnDeleteClick" Text="Delete Order" Visible="False" />
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="!!" 
                        Visible="False" />
                </td>
                <td align="center">
                    <asp:Panel ID="pnlPrintAtHome" runat="server" Width="220px" Visible="false" HorizontalAlign="Center">
                        <telerik:RadButton runat="server" ID="btnInvoice" Text="Organizer Invoice" 
                            Width="160px" Font-Bold="True" Font-Size="12pt" Skin="Forest" 
                            onclick="BtnInvoiceClick">
                        </telerik:RadButton>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:RequiredFieldValidator ID="valPayment" runat="server" ControlToValidate="rdpPayment"
            Display="None" ErrorMessage="You can not close Order before Payment received"
            ValidationGroup="Closed"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Closed" />
        <asp:HiddenField ID="hfVendor" runat="server" />
        <asp:HiddenField ID="hfStoreID" runat="server" />
        <asp:HiddenField ID="hfPickedUp" runat="server" />
        <asp:HiddenField ID="hfInstaller" runat="server" />
        <asp:HiddenField ID="hfOrderID" runat="server" />
        <asp:HiddenField ID="hfStatus" runat="server" />
        <asp:HiddenField ID="hfUpdated" runat="server" />
        <asp:HiddenField ID="hfConfirmed" runat="server" />
        <asp:HiddenField ID="hfLogin" runat="server" />
        <asp:HiddenField ID="hfCheckNumb" runat="server" />
        <asp:HiddenField ID="hfInstDate" runat="server" />
        <asp:HiddenField ID="hfOption" runat="server" />
    <telerik:RadWindow ID="rwStorePopup" runat="server" VisibleOnPageLoad="false" Height="250px" 
            Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" 
            Behaviors="None" Title="Select Store" Skin="WebBlue">
            <ContentTemplate>
                <div style="padding: 20px">
                    <center><br />
                    <asp:DropDownList ID="ddlStores" runat="server"></asp:DropDownList>
                    <br /><br /><br /><br />
                    <asp:Button ID="btnStoreSend" runat="server" Text="Save" Width="100px" OnClick="BtnStoreSelectClick"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:Button ID="btnStoreCancel" runat="server" Text="Cancel" Width="100px" OnClick="BtnStoreCancelClick" />
                    </center>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="rwTaxExempt" runat="server" VisibleOnPageLoad="false" Height="220px" Width="500px" 
            Modal="true" Title="Tax Exempt"  Skin="WebBlue">
            <ContentTemplate>
                <div style="padding: 20px">
                    <center>
                    Select Tax Exempt Reason:<br /><br />
                    <asp:DropDownList ID="ddlExempts" runat="server"></asp:DropDownList>
                    <br /><br />
                    <asp:Button ID="btnExemptSave" runat="server" Text="Save" Width="100px" OnClick="BtnExemptSaveClick"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                <asp:Button ID="btnExemptCancel" runat="server" Text="Cancel" Width="100px" OnClick="BtnExemptCancelClick"/>
                    </center>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="rwCheckList" runat="server" VisibleOnPageLoad="false" Modal="true" 
            Title="Check List" Width="920px" Height="750px" Behaviors="Resize, Close" 
            KeepInScreenBounds="True">
        </telerik:RadWindow>
    </center>
</asp:Panel>
<asp:Panel runat="Server" ID="pnlCallLog" CssClass="panelMain" Visible="false">
    <center>
    <div style="text-align:center; font-size:22px; font-weight:bold; color:#666666;">Call Log</div>
    <table style="width:560px;">
        <asp:Panel runat="Server" ID="pnlCall1" Visible="False">
        <tr>
            <td style="width:150px;" class="tdmain">Call #1</td>
            <td style="width:400px;">
                <asp:DropDownList ID="ddlCall1" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Busy</asp:ListItem>
                    <asp:ListItem>Mess. left</asp:ListItem>
                    <asp:ListItem>Wrong #</asp:ListItem>
                    <asp:ListItem>Emailed</asp:ListItem>
                </asp:DropDownList>
                <telerik:RadDateTimePicker ID="rdpDate1" runat="server" Width="200">
                </telerik:RadDateTimePicker>
            </td>
        </tr>
        </asp:Panel>
        <asp:Panel runat="Server" ID="pnlCall2" Visible="False">
        <tr>
            <td style="width:150px;" class="tdmain">Call #2</td>
            <td style="width:400px;">
                <asp:DropDownList ID="ddlCall2" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Busy</asp:ListItem>
                    <asp:ListItem>Mess. left</asp:ListItem>
                    <asp:ListItem>Wrong #</asp:ListItem>
                    <asp:ListItem>Emailed</asp:ListItem>
                </asp:DropDownList>
                <telerik:RadDateTimePicker ID="rdpDate2" runat="server" Width="200">
                </telerik:RadDateTimePicker>
            </td>
        </tr>
        </asp:Panel>
        <asp:Panel runat="Server" ID="pnlCall3" Visible="False">
        <tr>
            <td style="width:150px;" class="tdmain">Call #3</td>
            <td style="width:400px;">
                <asp:DropDownList ID="ddlCall3" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Busy</asp:ListItem>
                    <asp:ListItem>Mess. left</asp:ListItem>
                    <asp:ListItem>Wrong #</asp:ListItem>
                    <asp:ListItem>Emailed</asp:ListItem>
                </asp:DropDownList>
                <telerik:RadDateTimePicker ID="rdpDate3" runat="server" Width="200">
                </telerik:RadDateTimePicker>
            </td>
        </tr>
        </asp:Panel>
        <tr><td style="height:20px"></td></tr>
        <tr>
            <td style="text-align:center;">
                <asp:Button ID="btnSaveCalls" runat="server" Text="  Save  " 
                    CssClass="btnsubmit" Width="120px" OnClick="BtnSaveCallsClick" />
            </td>
            <td style="text-align:center;">
                <asp:Button ID="btnReturnCalls" runat="server" CausesValidation="False" CssClass="btnmenu"
                    Text="  Return  " OnClick="BtnReturnCallsClick" /></td>
        </tr>
    </table>
        &nbsp; 
    </center>
    </asp:Panel>
    <cc1:msgBox runat="server" ID="msgBox1" />
</asp:Content>
