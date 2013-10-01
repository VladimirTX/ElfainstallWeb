<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="ElfaInstall.OrderDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="msgBox" TagPrefix="cc1" Namespace="BunnyBear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <div style="font-size:22px; font-weight:bold; color:#556b2f; width:740px">
        <center>Order Information&nbsp;
            <asp:Label ID="lblPromo" runat="server" Text="At Home" ForeColor="Red" Visible="false"></asp:Label>
        </center>
    </div>
        <br />
        <table width="750">
            <tr>
                <td style="width: 130px;" valign="top" class="td12">Store Order #</td>
                <td style="width: 250px;" valign="top" align="left">
                    <asp:Label ID="lblOrderID" runat="server" Width="102px" CssClass="lblsize12"></asp:Label>
                </td>
                <td style="width: 100px;" valign="top" class="td12">Installer</td>
                <td style="width: 260px;" valign="top" align="left">
                    <a href="#"  onmouseout="dave.close()"   onmouseover="dave=window.open('VendorPhone.aspx','windname','width=400,height=280,nonononononotop=0,left=50'); return false;">
                        <asp:Label ID="lblInstaller" runat="server" CssClass="lblsize14" ForeColor="Red"></asp:Label>
                    </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnEmailVendor" Font-Size="Smaller" Height="20px" 
                        Text="Email" onclick="BtnEmailVendorClick" />
                </td>
            </tr>
            <tr>
                <td valign="top" class="td12">Store ID</td>
                <td valign="top" align="left">
                    <a href="#"  onmouseout="dave.close()"   onmouseover="dave=window.open('StorePhone.aspx','windname','width=300,height=200,nonononononotop=0,left=50'); return false;">
                        <asp:Label ID="lblStoreID" runat="server" CssClass="lblsize12" ForeColor="Red"></asp:Label>
                    </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnEmailStore" Font-Size="Smaller" Height="20px" 
                        Text="Email" onclick="BtnEmailStoreClick" Visible="false"/>
                </td>
                <td valign="top" class="td12">Install Start:</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblInstallDate" runat="server" CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="td12">Sale Date</td>
                <td valign="top" align="left">
                    <asp:Label ID="lblSaleDate" runat="server" CssClass="lblsize12" Text="" Width="118px"></asp:Label>
                </td>
                <td valign="top" class="td12">Install Finish:</td>
                <td valign="top" align="left" class="td12">
                    <asp:Label ID="lblTimeEnd" runat="server" CssClass="lblsize12" Text=" "></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="td12">
                    TCS Planner
                </td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblPlanner" CssClass="lblsize12" Width="212px"></asp:Label>
                </td>
                <td valign="top" class="td12">
                    Date Confirmed
                </td>
                <td align="left">
                    <asp:CheckBox ID="cbConfirmed" runat="server" Enabled="False" Text=" "/>
                    <asp:Label runat="Server" ID="lblConfirmed" Font-Size="X-Small">
                        (By phone)
                    </asp:Label>
                    <asp:CheckBox ID="cbBySurvey" runat="server" Enabled="False" Text=" "/>
                    <asp:Label runat="Server" ID="lblBySurvey" Font-Size="X-Small">
                        (By survey)
                    </asp:Label>
                </td>
            </tr>
        </table>
        <table width="750">
            <tr>
                <td style="width: 130px; height:15px;"></td>
                <td style="width: 240px;"></td>
                <td width="120"  class="td12" >Order Status</td>
                <td align="left" style="background-color:#C4C2C2; text-indent:5px; width: 230px;">
                    <asp:Label runat="Server" ID="lblStatus" CssClass="lblsize14" Width="200"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Customer Name</td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblcName"  CssClass="lblsize12"></asp:Label>
                </td>       
                <td style="width: 350px;" valign="top" align="left" rowspan="3" colspan="2">
                    <br />
                    <asp:Label ID="lblCall1" runat="server" CssClass="lblsize12" Text=" "></asp:Label><br />
                    <asp:Label ID="lblCall2" runat="server" CssClass="lblsize12" Text=" "></asp:Label><br />
                    <asp:Label ID="lblCall3" runat="server" CssClass="lblsize12" Text=" "></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">Address</td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblcAddress1"  CssClass="lblsize12"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcAddress2"  CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">City, State, Zip</td>
                <td align="left">
                    <asp:Label runat="Server" ID="lblcCity"  CssClass="lblsize12"></asp:Label>&nbsp;
                    <asp:Label runat="Server" ID="lblcState"  CssClass="lblsize12"></asp:Label>,&nbsp;
                    <asp:Label runat="Server" ID="lblcZip"  CssClass="lblsize12"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="740">
          <tr>
            <td width="150" class="td12" rowspan="2">Phone Number</td>
            <td width="75" class="td12">Home:</td>
            <td width="155" align="left"><asp:Label ID="lblHphone" runat="server" CssClass="lblsize12"></asp:Label></td>
            <td width="360" class="td14">Office Notes:</td>
          </tr>
          <tr>
            <td class="td12">Other:</td>
            <td align="left"><asp:Label ID="lblPhone2" runat="server" CssClass="lblsize12" Width="120px"></asp:Label></td>
            <td rowspan="3" align="left" valign="top">
                <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="80px" Width="360px">
                    <telerik:RadPane ID="RadPane2" runat="server" Width="360px" Scrolling="Y">
                        <asp:Label ID="lblOffice" runat="server" Height="50px" Width="340px" CssClass="lblsize12"></asp:Label>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </td>
          </tr>
            <tr>
            <td class="td12" align="center">
                <asp:Label ID="lblLabelEmail" runat="server">Email</asp:Label>
                <asp:Button runat="server" ID="btnEmailInvoice" Font-Size="Smaller" Height="20px" 
                        Text="Email Invoice" Visible="false" onclick="BtnEmailInvoiceClick"/>
            </td>
            <td colspan="2" align="left"><asp:Label ID="lblEmail" runat="server" CssClass="lblsize12"></asp:Label></td>
          </tr>
            <tr>
            <td class="tdtitlesmall" colspan="3">
                <asp:HyperLink ID="hlAddress" runat="server" NavigateURL="" Target="_blank">Click for Map</asp:HyperLink>
<%--                NavigateUrl="http://www.map-generator.net/map.php?name=Customer Address&address=1302%20Tippler%20Dr%2C%20Arlington%20TX&width=500&height=400&maptype=map&zoom=14&t=1289604015" onClick="return popup(this, 'notes')">Click for Map</asp:HyperLink>--%>
            </td>
          </tr>
            <tr>
            <td colspan="3">
              <table width="100%">
                <tr>
                  <td class="td12" width="42%">$<asp:Label runat="server" ID="lblDelivery">80.00</asp:Label> Delivery Option</td>
                  <td width="17%" align="left"><asp:CheckBox ID="cbDelivery" runat="server" Checked="True" Text=" " Enabled="False" /></td>
                  <td width="30%" class="td12"><asp:Label ID="lblPickedUp" runat="server" Visible="false">Picked Up</asp:Label></td>
                  <td width="11%" align="left"><asp:CheckBox ID="cbPickedUp" runat="server" Text=" " Enabled="False" Width="40px" Visible="false"/></td>
                </tr>
<%--                <tr>
                  <td class="td12">Scope of Demo:</td>
                  <td align="left"><asp:Label ID="lblDemo" runat="server" Width="50px" CssClass="lblsize12"></asp:Label></td>
                  <td class="td12">Demolition</td>
                  <td align="left"><asp:CheckBox ID="cbDemolition" runat="server" Checked="True" Enabled="False" Width="40px" Text=" " /></td>
                </tr>--%>
              </table>
            </td>
          </tr>
        </table>
        <br />
<%--        <div style="width:740px; font-size:12px; font-weight:bold;">&nbsp;TCS Notes:</div>--%>
        <asp:Label ID="Label1" runat="server" Font-Size="12px" Font-Bold="true" Width="100px">TCS Notes:</asp:Label>
        <asp:Label ID="lblSalePrice" runat="server" Font-Size="12px" Font-Bold="true" Width="640px"></asp:Label>
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
                <td align="right" style="width:120px; height: 21px;">
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="lblmoney"></asp:Label>
                </td>
                <td style="width:244px; height: 21px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12"><asp:Label runat="server" ID="lblProc" Visible="false">30</asp:Label> Basic Installation Charge</td>
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
                    Additional Miles</td>
                <td align="right">
                    <asp:Label ID="lblMilesPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Additional Demo Required ($65.00/hr)</td>
                <td align="right">
                    <asp:Label ID="lblDemoPrice" runat="server" CssClass="lblmoney"></asp:Label></td>
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
                    Other</td>
                <td align="right">
                    <asp:Label ID="lblTip" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px">&nbsp;</td>
            </tr>
            <tr>
                <td class="td12">
                    Adjustment</td>
                <td align="right">
                    <asp:Label ID="lblDiscount" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td align="center">
                    <asp:Label ID="lblReason" runat="server" CssClass="lblmoney"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td12">
                    Tax</td>
                <td align="right">
                    <asp:Label ID="lblTax" runat="server" CssClass="lblmoney"></asp:Label></td>
                <td style="width: 244px" align="center"><asp:Label ID="lblExempt" runat="server" CssClass="lblmoney" Visible="false">Tax Exempt</asp:Label></td>
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
        <div style="width:740px; font-size:12px; font-weight:bold;">&nbsp;Comments to Installer:</div>
        <table cellpadding="5" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblComments" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="width:740px; font-size:12px; font-weight:bold;">&nbsp;Invoice Comments:</div>
        <table cellpadding="5" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblInvoice" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="width:740px; font-size:12px; font-weight:bold;">&nbsp;Comments from Installer:</div>
        <table cellpadding="5" cellspacing="0" width="740" border="1" style="border-color:#bdb76b;">
            <tr>
                <td align="left">
                    <asp:Label ID="lblAccounting" runat="server" CssClass="lblsize12" Width="740px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:HyperLink ID="hlImages" runat="server" Target="_blank" Font-Bold="True">Order Images</asp:HyperLink>
        <br /><br />
        <table style="width:740px;">
            <tr>
                <td style="width:140; text-align:center">
                    <asp:Button ID="btnLog" runat="server" Text="Call Log" CssClass="btnsubmit" 
                        Width="120px" OnClick="BtnLogClick" Visible="False" /></td>
                <td style="width:160; text-align:center">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit Order Info" CssClass="btnsubmit" Width="140px" OnClick="BtnEditClick" />
                    </td>
                <td style="width:140; text-align:center">
                    <asp:HyperLink ID="lnkPrint" CssClass="btnsubmit" runat="server" Width="120px" Height="22px" Target="_blank" Font-Bold="True">Print Invoice</asp:HyperLink>
                 </td>
<%--                <td style="width:10; text-align:center;"></td>--%>
<%--               <td style="text-align:center">
                   <asp:Button ID="btnDelete" runat="server" CssClass="btnmenu" 
                       Text="Delete Order" OnClick="BtnDeleteClick" Visible="False" />
               </td> --%>
               <td style="text-align:center">
                    <asp:Button ID="btnService" runat="server" CssClass="btnsubmit" 
                        Text="Special Order" Visible="false" Width="160px" 
                        onclick="BtnServiceClick"/>
                    <asp:Button ID="btnRequest" runat="server" CssClass="btnsubmit" 
                        Text="Request new plans" Visible="false" Width="160px" 
                        onclick="BtnRequestClick" />
                    <telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false" Height="250px"
	                            Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="None" Title="Send Plans Request">
	                    <ContentTemplate>
	                        <div style="padding: 20px">
	                            You can use default comment or type yours.
	                            <br /><br />
                                Comment: <asp:TextBox ID="txtComments" runat="server" Width="300px" MaxLength="300"></asp:TextBox><br /><br /><br /><br />
                                <center>
                                <asp:Button ID="btnOk" runat="server" Text="Email request" Width="150px" OnClick="btnOk_Click" />
                                &nbsp;&nbsp;&nbsp;
	                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" OnClick="btnCancel_Click" />
                                </center>
	                        </div>
	                    </ContentTemplate>
                    </telerik:RadWindow>
                </td>
            </tr>
        </table>
        <telerik:RadWindow ID="rwStorePopup" runat="server" VisibleOnPageLoad="false" Height="250px" 
            Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" 
            Behaviors="None" Title="Send Email to Store" Skin="WebBlue">
            <ContentTemplate>
                <div style="padding: 20px">
                    Comments (optional):<br />
                    <asp:TextBox ID="txtStoreComments" runat="server" Width="400px" MaxLength="500" TextMode="MultiLine" Rows="2"></asp:TextBox><br /><br /><br /><br />
                    <center>
                    <asp:Button ID="btnStoreSend" runat="server" Text="Email to Manager" Width="150px" OnClick="BtnStoreSendClick"/>
                    &nbsp;&nbsp;&nbsp;
	                <asp:Button ID="btnStoreCancel" runat="server" Text="Cancel" Width="100px" OnClick="BtnStoreCancelClick" />
                    </center>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="rwVendorPopup" runat="server" VisibleOnPageLoad="false" Height="250px" 
            Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" 
            Behaviors="None" Title="Send Email to Vendor" Skin="WebBlue">
            <ContentTemplate>
                <div style="padding: 20px">
                    Comments (optional):<br />
                    <asp:TextBox ID="txtVendorComments" runat="server" Width="400px" MaxLength="500" Rows="2" TextMode="MultiLine"></asp:TextBox><br /><br /><br /><br />
                    <center>
                    <asp:Button ID="btnVendorSend" runat="server" Text="Email to Vendor" Width="150px" OnClick="BtnVendorSendClick"/>
                    &nbsp;&nbsp;&nbsp;
	                <asp:Button ID="btnVendorCancel" runat="server" Text="Cancel" Width="100px" OnClick="BtnVendorCancelClick" />
                    </center>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <br />
    </center>
    </asp:Panel>
    <asp:HiddenField ID="hfOrderID" runat="server" />
    <asp:HiddenField ID="hfStatus" runat="server" />
    <asp:HiddenField ID="hfConfirm" runat="server" />
    <cc1:msgBox runat="server" ID="msgBox1" />
</asp:Content>
