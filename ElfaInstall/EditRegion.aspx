<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="EditRegion.aspx.cs" Inherits="ElfaInstall.EditRegion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
    <table style=" width:500;" cellpadding="2"  class="tblBorder">
        <tr>
            <td colspan="2" class="tdheader" style="height: 20px">
                <asp:Label runat="Server" ID="lblHeader">Region Info</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:140px" class="tdmainR">Region Admin:</td>
            <td style="width:360px" align="left"><asp:TextBox ID="txtName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">City:</td>
            <td align="left">
                <table>
                    <tr>
                        <td style="width:120px" align="left"><asp:TextBox ID="txtCity" runat="server" Width="100px"></asp:TextBox></td>
                        <td style="width:240px" class="tdmain">State<asp:DropDownList ID="ddlStates" runat="server"></asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdmainR">Email:</td>
            <td align="left">
                <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Comments(show):</td>
            <td align="left"><asp:TextBox ID="txtComments" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdmainR">Is Regional:</td>
            <td class="tdmain">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbYes" runat="server" Text="Yes" GroupName="Regional" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbNo" runat="server" Text="No" GroupName="Regional" />
            </td>
        </tr>
        <tr style="height:40px"><td colspan="2" align="center">
            <asp:Button ID="btnSave" runat="server" CssClass="btnsmall" Text="Save Info" OnClick="BtnSaveClick" /></td></tr>
    </table>
    <br />
    <table style=" width:400px;" cellpadding="5"  class="tblBorder">
        <tr><td style="text-align:center; font-weight:bold" class="style1">Assignet Stores:</td></tr>
        <tr><td style="text-align:center; font-size:small">(Click on Store ID to Remove it from the List)</td></tr>
        <tr><td><asp:Label runat="server" ID="lblStores"></asp:Label></td></tr>
    </table>
    <table style=" width:400px;" cellpadding="5">
        <tr><td align="center">
            <asp:Button ID="btnAddStore" runat="server" CssClass="btnsmall" Text="Assign Additional Store" OnClick="BtnAddStoreClick" />
        </td></tr>
    </table>
    <asp:Panel runat="server" ID="pnlStores" Visible="false">
    <asp:GridView ID="grvStores" runat="server" DataSourceID="sdsStores" 
        CellPadding="0" DataMember="DefaultView" HorizontalAlign="Center" Width="410px" 
        AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" BackColor="White" 
        PageSize="16" AllowSorting="True" OnPageIndexChanged="GrvStoresPageIndexChanged" 
        OnSorted="GrvStoresSorted" Font-Bold="True" OnSelectedIndexChanged="GrvStoresSelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="StoreID">
                <ItemStyle Width="1px" Font-Size="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="StoreCode" HeaderText="Store Code" SortExpression="StoreCode" >
                <ItemStyle Width="100px" CssClass="gridDataC" />
            </asp:BoundField>
            <asp:BoundField DataField="StoreName" HeaderText="Store Name" SortExpression="StoreName" >
                <ItemStyle Width="200px" CssClass="gridDataL" />
            </asp:BoundField>
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" >
                <ItemStyle Width="50px" CssClass="gridDataC" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" SelectText="Add" ShowSelectButton="True" />
        </Columns>
        <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#0000C0" 
            HorizontalAlign="Center" Height="30px" />
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#BFC7A4" />
        <RowStyle Height="25px" />
    </asp:GridView>
    <table style=" width:920px;" cellpadding="5">
        <tr>
            <td align="center">
                <asp:Button ID="btnClose" runat="server"  CssClass="btnsmall" Text="Close" 
                    onclick="BtnCloseClick" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <br />&nbsp;
    <asp:SqlDataSource ID="sdsStores" runat="server" />
    </center>
</asp:Panel>
</asp:Content>
