<%@ Page Title="" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="ElfaInstall.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlMain" runat="server" CssClass="panelMain">
    <center>
        <asp:Label runat="server" ID="lblTitle" Font-Bold="true" Font-Size="Larger">Ella Myszczynski</asp:Label>
        <br />
        <table width="740">
            <tr>
                <td align="center">
                    <asp:Calendar ID="cal1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                        CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px"
                        NextPrevFormat="ShortMonth" Width="680px" OnDayRender="Cal1DayRender" SelectionMode="None" ShowGridLines="True">
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                        <DayStyle BackColor="#C4C2C2" VerticalAlign="Top" Font-Size="Smaller" Font-Bold="True" />
                        <OtherMonthDayStyle ForeColor="Black" BackColor="#E0E0E0" Font-Size="Smaller" VerticalAlign="Top" />
                        <NextPrevStyle Font-Bold="True" Font-Size="10pt" ForeColor="White" />
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                        <TitleStyle BackColor="#A0A789" BorderStyle="None" Font-Bold="True" Font-Size="12pt"
                            ForeColor="White" Height="12pt" />
                    </asp:Calendar>
                </td>
            </tr>
        </table>
        &nbsp;
    </center>
</asp:Panel>
</asp:Content>
