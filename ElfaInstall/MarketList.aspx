<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="MarketList.aspx.cs" Inherits="ElfaInstall.MarketList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:300;">
        <tr>
            <td class="tdheader" style="height: 20px" colspan="2">
                Market List
            </td>
        </tr>
        <tr>
            <td style="width:40px;">&nbsp;</td>
            <td style="width:300px" valign="top" align="center">
            <center>
                <asp:GridView ID="grvMarkets" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sdsMarkets" Width="400px" DataKeyNames="MarketID" 
                    BorderColor="DarkOliveGreen" BorderWidth="1px" CellPadding="0" ForeColor="Black" ShowHeader="False" DataMember="DefaultView" BorderStyle="Solid" Font-Size="Small">
                    <Columns>
                        <asp:BoundField DataField="MarketName" ShowHeader="False" >
                            <ItemStyle Width="260px" CssClass="gridcellleft" Height="25px" Font-Size="10pt" Font-Bold="True" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="False" ShowEditButton="True" >
                            <ItemStyle Width="140px" HorizontalAlign="Center" />
                            <ControlStyle CssClass="btnsmall" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="Tan" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="#BFC7A4" />
                    <EditRowStyle Width="100px" Wrap="False" />
                </asp:GridView>
            </center>
            </td>
        </tr>
        <tr>
            <td style="width:380px" align="center" colspan="2">
                <br /><asp:Button ID="btnAdd" runat="server" Text=" Add New" OnClick="BtnAddClick" CssClass="btnsmall" />
                <asp:Label runat="Server" ID="lblNewMarket" CssClass="lblsize12" Visible="false">Add Market Name</asp:Label>
                <asp:TextBox ID="txtNewMarket" runat="server" Visible="false" Width="100px"></asp:TextBox>&nbsp;
                <asp:Button ID="btnSave" runat="server" Text=" Save " Visible="false" OnClick="BtnSaveClick" ValidationGroup="New" CssClass="btnsmall"/>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="BtnCancelClick" Visible="False" CssClass="btnsmall" /><br />&nbsp;
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="sdsMarkets" runat="server" 
        SelectCommand="SELECT MarketID, MarketName FROM tblMarkets" 
        ConnectionString="<%$ ConnectionStrings:CommConnection %>" 
        ProviderName="<%$ ConnectionStrings:CommConnection.ProviderName %>" 
        UpdateCommand="UPDATE tblMarkets SET MarketName=@MarketName WHERE MarketID=@MarketID">
        <UpdateParameters>
            <asp:QueryStringParameter Name="@MarketName" />
            <asp:QueryStringParameter Name="@MarketID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:RequiredFieldValidator ID="valMarket" runat="server" ControlToValidate="txtNewMarket"
        Display="None" ErrorMessage="Market Name is Missing!" ValidationGroup="New"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="New" />
    </center>
</asp:Panel>
</asp:Content>
