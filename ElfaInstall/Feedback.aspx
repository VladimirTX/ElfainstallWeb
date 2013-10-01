<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="ElfaInstall.Feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <table style=" width:650px;" cellpadding="0" cellspacing="3">
            <tr>
                <td style="height: 20px; text-align:center" colspan="6">
                    <asp:Label runat="server" ID="lblHeader" Font-Bold="True" Height="40px" Font-Size="14pt" ForeColor="#666666"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 75px; text-align: left;">
                    Customer:</td>
                <td style="width: 190px; text-align: left;">
                    <asp:Label ID="lblCustomer" runat="server" Text="Label"></asp:Label></td>
                <td style="width: 65px; text-align: right;">
                    Order #:</td>
                <td style="width: 70px; text-align: left;">
                    <asp:Label ID="lblOrder" runat="server" Text="lblOrder"></asp:Label></td>
                <td style="width: 90px; text-align: right">
                    Inst.
                    Date:</td>
                <td style="width: 110px; text-align: left;">
                    <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="2">
                    Elfa Product Amount:
                    <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label></td>
                <td style="text-align: right;" colspan="2">
                    Installation Charge:
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="lblCharge" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Delivery:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblDelivery" runat="server" Text="Label"></asp:Label></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="width: 200px; text-align: left;">
                    Overall Installation Time:</td>
                <td style="width: 120px">
                    <asp:TextBox ID="txtInstH" runat="server" Width="50px"></asp:TextBox>&nbsp; Hrs</td>
                <td style="width: 120px">
                    <asp:TextBox ID="txtInstM" runat="server" Width="50px"></asp:TextBox>
                    Min</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Demolition:</td>
                <td>
                    <asp:TextBox ID="txtDemolH" runat="server" Width="50px"></asp:TextBox>
                    Hrs</td>
                <td>
                    <asp:TextBox ID="txtDemolM" runat="server" Width="50px"></asp:TextBox>
                    Min</td>
            </tr>
        </table>
        <br />
        <table style=" width:600px;" cellpadding="0" cellspacing="3">
            <tr>
                <td style="text-align: left; width:200px">
                    Missing product:
                    <asp:DropDownList ID="ddlMissing" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlMissingSelectedIndexChanged">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                <td style="text-align: left; width:200px" valign="middle">
                    Damaged product:
                    <asp:DropDownList ID="ddlDamaged" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlDamagedSelectedIndexChanged">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width:200px" valign="middle">
                    Design flaw:
                    <asp:DropDownList ID="ddlSlow" runat="server">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Installer Comments:<br />
                    <span style="text-align: left; font-size:x-small;">
                    (common, like design flaw)
                    </span>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="350px" MaxLength="250"></asp:TextBox></td>
            </tr>
                
        </table>
<%--            <asp:Panel runat="server" ID="pnlFeedBack" HorizontalAlign="center">
        <center>
        <table>
            <tr>
                <td align="center">
                    <br />
                    <asp:Button ID="btnSaveFeedback" runat="server" Text="Save Feedback" CssClass="btnsubmit" OnClick="btnSaveFeedback_Click" />
                </td>
            </tr>
        </table>
        </center>
        </asp:Panel>--%>
        <asp:Panel runat="server" ID="pnlMissing" Visible="False">
            <center>
            <br />
            <table style=" width:600px;" cellpadding="0" cellspacing="3">
                <tr>
                    <td align="center" colspan="2">
                        <strong>
                        Missing Product Details:</strong></td>
                </tr>
                <tr>
                    <td style="width:250px">Specific Product Missing</td>
                    <td style="width:350px">
                        <asp:TextBox ID="txtMissing" runat="server" TextMode="MultiLine" Width="340px" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Resolution</td>
                    <td>
                        <asp:TextBox ID="txtMResolution" runat="server" TextMode="MultiLine" Width="340px" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>EIS Employee Contact</td>
                    <td>
                            <asp:TextBox ID="txtMEmployee" runat="server" Width="340px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Was Product Delivered by EIS?</td>
                    <td>
                        <asp:DropDownList ID="ddlMDelivered" runat="server">
                            <asp:ListItem Value="N"></asp:ListItem>
                            <asp:ListItem Value="Y"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Was Product Paid for by Customer?</td>
                    <td>
                        <asp:DropDownList ID="ddlMPaid" runat="server">
                            <asp:ListItem Value="N"></asp:ListItem>
                            <asp:ListItem Value="Y"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDamaged" Visible="False">
            <center>
            <br />
            <table style=" width:600px;" cellpadding="0" cellspacing="3">
                <tr>
                    <td align="center" colspan="2">
                        <strong>
                        Damaged Product Details:</strong></td>
                </tr>
                <tr>
                    <td style="width:250px">Specific Product Damaged</td>
                    <td style="width:350px">
                        <asp:TextBox ID="txtDamaged" runat="server" TextMode="MultiLine" Width="340px" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Resolution</td>
                    <td>
                        <asp:TextBox ID="txtDResolution" runat="server" TextMode="MultiLine" Width="340px" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>EIS Employee Contact</td>
                    <td>
                            <asp:TextBox ID="txtDEmployee" runat="server" Width="340px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Was Product Delivered by EIS?</td>
                    <td>
                        <asp:DropDownList ID="ddlDDelivered" runat="server">
                            <asp:ListItem Value="N"></asp:ListItem>
                            <asp:ListItem Value="Y"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Was Product Paid for by Customer?</td>
                    <td>
                        <asp:DropDownList ID="ddlDPaid" runat="server">
                            <asp:ListItem Value="N"></asp:ListItem>
                            <asp:ListItem Value="Y"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel>
        <table>
            <tr>
                <td align="center">
                    <br />
                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save Feedback Info" CssClass="btnsubmit" OnClick="BtnSaveDetailsClick" />
                    <br />&nbsp;
                </td>
            </tr>
        </table>
    </center>
</asp:Panel>
</asp:Content>
