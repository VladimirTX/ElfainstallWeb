<%@ Page Title="Region Orders Search" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="RegionSearch.aspx.cs" Inherits="ElfaInstall.RegionSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" Width="600px" BorderColor="Black" BorderWidth="1" BorderStyle="Solid">
    <center>
    <table style="width: 500px;" cellpadding="0" cellspacing="2">
        <tr>
            <td class="tdheadersmall" colspan="3"><br /> Select Search Criteria </td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
         <tr>
            <td></td>
            <td  align="left"  class="td14" colspan="2">
                <asp:RadioButton ID="rbOpen1" runat="server" GroupName="Open" 
                    Text=" Open Orders" Checked="True" /><br />
                &nbsp;<asp:RadioButton ID="rbOpen2" runat="server" GroupName="Open" Text=" Installed Orders" /><br />
                &nbsp;<asp:RadioButton ID="rbOpen3" runat="server" GroupName="Open" 
                    Text=" Both" />
            </td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td  style="width: 150px;" class="td14">Order #</td>
            <td  style="width: 200px;" align="left">
                <asp:TextBox ID="txtOrderNumb" runat="server" Width="135" MaxLength="15"></asp:TextBox></td>
            <td  style="width: 100px;" align="left"><asp:Button ID="btnOrderNumb" 
                    runat="server" Text="Search" CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" 
                    ValidationGroup="Order" onclick="BtnOrderNumbClick" /></td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td class="td14">Store</td>
            <td align="left">
                <asp:DropDownList ID="ddlStore" runat="server" Width="140px">
                </asp:DropDownList></td>
            <td align="Left"><asp:Button ID="btnStore" runat="server" Text="Search" 
                    CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" ValidationGroup="Store" 
                    onclick="BtnStoreClick" /></td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td class="td14">Vendor</td>
            <td align="left">
                <asp:DropDownList ID="ddlVendors" runat="server" Width="140px">
                </asp:DropDownList></td>
            <td align="left"><asp:Button ID="btnVendor" runat="server" Text="Search" 
                    CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" ValidationGroup="Vendor" 
                    onclick="BtnVendorClick" /></td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td class="td14">Market</td>
            <td align="left">
                <asp:DropDownList ID="ddlMarket" runat="server" Width="140px">
                </asp:DropDownList>
            </td>
            <td align="left"><asp:Button ID="btnMarket" runat="server" Text="Search" 
                    CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" ValidationGroup="Market" 
                    onclick="BtnMarketClick" /></td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td class="td14" valign="middle">Customer Name</td>
            <td align="left">
                <table width="150" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width:40px;"  class="td12">First</td>
                        <td style="width:110px;" align="right"><asp:TextBox ID="txtFirst" runat="server" 
                                Width="100px" MaxLength="10"></asp:TextBox></td>
                    </tr>
                    <tr><td align="center" colspan="2" class="td12">and / or</td></tr>
                    <tr>
                        <td class="td12">Last</td>
                        <td align="right"><asp:TextBox ID="txtLast" runat="server" Width="100px" 
                                MaxLength="10"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="middle"><asp:Button ID="btnName" runat="server" 
                    Text="Search" CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" 
                    onclick="BtnNameClick" /></td>
        </tr>
        <tr><td colspan="3" style="height: 35px"><hr /></td></tr>
        <tr>
            <td class="td14" valign="middle">Date Range</td>
            <td align="left">
                <table width="150" cellpadding="0" cellspacing="1">
                    <tr>
                        <td style="width:30px;"  class="td12">From: </td>
                        <td style="width:30px;" align="left">
                            <telerik:RadDatePicker ID="rdpDate1" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="td12">To: </td>
                        <td align="left">
                            <telerik:RadDatePicker ID="rdpDate2" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="middle"><asp:Button ID="btnDates" runat="server" 
                    Text="Search" CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" 
                    ValidationGroup="Date" onclick="BtnDatesClick" /></td>
        </tr>
        <tr><td colspan="3"><hr /></td></tr>
        <tr>
            <td class="td14">Order Option</td>
            <td align="left">
                <asp:DropDownList ID="ddlOption" runat="server">
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem Value="ATH">At Home</asp:ListItem>
                    <asp:ListItem Value="ATH-AT">ATH-AT</asp:ListItem>
                    <asp:ListItem Value="ATH-CG">ATH-CG</asp:ListItem>
                    <asp:ListItem Value="ATH-MB">ATH-MB</asp:ListItem>
                    <asp:ListItem Value="B2B">Business to Business</asp:ListItem>
                    <asp:ListItem Value="CAL">Call In</asp:ListItem>
<%--                    <asp:ListItem Value="COM">Commercial</asp:ListItem>--%>
                    <asp:ListItem Value="EPCP">Elfa Preferred Customer</asp:ListItem>
                    <asp:ListItem Value="EDS">Ellen DeGeneres Show</asp:ListItem>
                    <asp:ListItem Value="FFE">Fall for elfa</asp:ListItem>
                    <asp:ListItem Value="JFU">Just for You</asp:ListItem>
                    <asp:ListItem Value="MID">Man in Desert</asp:ListItem>
                    <asp:ListItem Value="PRM">PR</asp:ListItem>
                    <asp:ListItem Value="STC">Store Comp</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left"><asp:Button ID="btnOptions" runat="server" Text="Search" 
                    CssClass="btnmenu" PostBackUrl="~/OrdersR.aspx" ValidationGroup="Market" 
                    onclick="BtnOptionsClick" /></td>
        </tr>
        <tr><td colspan="3" style="height: 35px"><hr />
            <asp:HiddenField ID="hfParameter" runat="server" />
            <asp:HiddenField ID="hfVendor" runat="server" />
            <asp:HiddenField ID="hfRegionID" runat="server" />
        </td>
        </tr>
    </table>
    </center>
    </asp:Panel>
</asp:Content>
