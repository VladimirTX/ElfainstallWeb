<%@ Page Title="Installation Schedule" Language="C#" MasterPageFile="~/MasterNew.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="ElfaInstall.Schedule" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table width="100%">
            <tr>
                <td style="width:40%; text-align:left">
                    <asp:Label ID="lblRegion" runat="server"></asp:Label>
                </td>
                <td style="width:60%; text-align:left">
                     Market:&nbsp;
                    <asp:DropDownList ID="ddlMarket" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="Button1" runat="server" Text="Select" />
                </td>
            </tr>
        </table>
        <telerik:RadScheduler ID="RadScheduler1" runat="server" 
            DataDescriptionField="Notes" DataEndField="EndTime" DataKeyField="ID" 
            DataSourceID="sdsAppointments" DataStartField="StartTime" 
            DataSubjectField="Subject" DayEndTime="19:00:00" 
            SelectedView="DayView" AppointmentStyleMode="Default" 
            GroupBy="Vendors" RowHeight="10px" 
            ShowAllDayRow="False" AllowDelete="False" Skin="Outlook">
            <ResourceTypes>
                <telerik:ResourceType DataSourceID="sdsVendors" ForeignKeyField="VendorID" 
                    KeyField="VendorID" Name="Vendors" TextField="VendorName" />
            </ResourceTypes>
            <MonthView MinimumRowHeight="8" VisibleAppointmentsPerDay="1" />
        </telerik:RadScheduler>
        <table width="1050px" border="1">
            <tr>
            <td align="center"><b>Not scheduled orders</b></td>
            <td align="center"><b>Not assigned orders</b></td>
        </tr>
        <tr>
            <td style="width:600px; vertical-align:top; text-align:center">
            <asp:Panel ID="pnlOpen" runat="server">
                <telerik:RadGrid ID="rgOpen" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    GridLines="None" BorderStyle="Solid" Width="600px" 
                    onitemcommand="RgOpenItemCommand">
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="OrderID" Width="600">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32" 
                                HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" 
                                UniqueName="OrderID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderNumb" HeaderText="Order #" 
                                SortExpression="OrderNumb" UniqueName="OrderNumb">
                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderDate" DataType="System.DateTime" 
                                HeaderText="Order Date" SortExpression="OrderDate" UniqueName="OrderDate" DataFormatString="{0:d}">
                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderPrice" DataType="System.Decimal" 
                                HeaderText="Price" SortExpression="OrderPrice" UniqueName="OrderPrice" DataFormatString="{0:C2}">
                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StoreCode" HeaderText="Store" 
                                SortExpression="StoreCode" UniqueName="StoreCode">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Customer" HeaderText="Customer" 
                                ReadOnly="True" SortExpression="Customer" UniqueName="Customer" >
                                <ItemStyle Width="160px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VendorName" HeaderText="Vendor"
                                ReadOnly="true" SortExpression="VendorName" UniqueName="VendorName">
                                <ItemStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn Text="Schedule" CommandName="Schedule" ButtonType="PushButton" UniqueName="btnSelect">
                                <ItemStyle Width="80px" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:Panel>
             <asp:Panel ID="pnlSchedule" runat="server" Visible="false">
                    <table width="500px" style=" text-align:left">
                        <tr>
                            <td align="center" colspan="4" style=" font-size:large; font-weight:bold">
                                Schedule Installation for Order #
                                <asp:Label ID="lblOrderNumb" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr><td colspan="4"><hr /></td></tr>
                        <tr>
                            <td width="120px">OrderDate:</td>
                            <td><asp:Label ID="lblOrderDate" runat="server">06/06/2010</asp:Label></td>
                            <td>Install. Price:</td>
                            <td><asp:Label ID="lblInstPrice" runat="server">$540.00</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Customer:</td>
                            <td><asp:Label ID="lblCustomer" runat="server">Nina Pupkin</asp:Label></td>
                            <td>Phone:</td>
                            <td><asp:Label ID="lblPhone" runat="server">(817) 123-4567</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Address:</td>
                            <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                            <td>City, State:</td>
                            <td><asp:Label ID="lblCity" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Description:</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDescription" runat="server" 
                                    TextMode="MultiLine" Width="350px" Height="30px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Comments to Installer:</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" 
                                    Width="350px" Height="30px"></asp:TextBox></td>
                        </tr>
                        <tr><td colspan="4"><hr /></td></tr>
                        <tr>
                            <td>Install. Date</td>
                            <td>
                                <telerik:RadDatePicker ID="rdInstall" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>Install. Start</td>
                            <td>
                                <telerik:RadTimePicker ID="rtStart" Runat="server" Width="80px" 
                                    TimeView-EndTime="19:00:00" TimeView-StartTime="08:00:00" TimeView-Interval="02:00:00">
                                </telerik:RadTimePicker>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>Install. End</td>
                            <td>
                                <telerik:RadTimePicker ID="rtEnd" Runat="server" Width="80px" 
                                    TimeView-EndTime="21:00:00" TimeView-StartTime="10:00:00" TimeView-Interval="02:00:00">
                                </telerik:RadTimePicker>
                            </td>
                        </tr>
                        <tr><td colspan="4"><hr /></td></tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                                    onclick="BtnSaveClick" ValidationGroup="Sched" />
                            </td>  
                            <td colspan="2" align="center">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" 
                                    onclick="BtnCancelClick" />
                            </td>             
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="width:600px; vertical-align:top; text-align:center">
                <asp:Panel ID="pnlNotAssigned" runat="server">
                <telerik:RadGrid ID="rgAssign" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    GridLines="None" BorderStyle="Solid" Width="600px" 
                        onitemcommand="RgAssignItemCommand">
                    <MasterTableView DataKeyNames="OrderID">
                    <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="OrderID" DataType="System.Int32" 
                                HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" 
                                UniqueName="OrderID" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderNumb" HeaderText="Order #" 
                                SortExpression="OrderNumb" UniqueName="OrderNumb">
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Customer" HeaderText="Customer" >
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderDate" DataFormatString="{0:d}" 
                                DataType="System.DateTime" HeaderText="Order Date" SortExpression="OrderDate" 
                                UniqueName="OrderDate">
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderPrice" DataFormatString="{0:c}" 
                                DataType="System.Decimal" HeaderText="Price" SortExpression="OrderPrice" 
                                UniqueName="OrderPrice">
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StoreCode" HeaderText="Store" 
                                SortExpression="StoreCode" UniqueName="StoreCode">
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:d}" 
                                HeaderText="Inst. Date" SortExpression="StartDate">
                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StartTime" HeaderText="Time"  DataFormatString="{0:t}"
                                SortExpression="StartTime" UniqueName="StartTime">
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </telerik:GridBoundColumn>
<%--                            <telerik:GridBoundColumn DataField="Duration" HeaderText="Dur." 
                                SortExpression="Duration" UniqueName="Duration">
                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridButtonColumn Text="Assign" CommandName="Assign" ButtonType="PushButton" UniqueName="btnAssign">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                </asp:Panel>
                <asp:Panel ID="pnlVendors" runat="server" Visible="false">
                    <table width="550px">
                        <tr>
                            <td align="center" colspan="4" style=" font-size:large; font-weight:bold">
                                Assign Vendor for Order #
                                <asp:Label ID="lblOrderNumb2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr><td colspan="4"><hr /></td></tr>
                        <tr>
                            <td width="120px">OrderDate:</td>
                            <td><asp:Label ID="lblOrderDate2" runat="server">06/06/2010</asp:Label></td>
                            <td>Install. Price:</td>
                            <td><asp:Label ID="lblInstPrice2" runat="server">$540.00</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Customer:</td>
                            <td><asp:Label ID="lblCustomer2" runat="server">Nina Pupkin</asp:Label></td>
                            <td>Phone:</td>
                            <td><asp:Label ID="lblPhone2" runat="server">(817) 123-4567</asp:Label></td>
                        </tr>
                        <tr>
                            <td>Address:</td>
                            <td><asp:Label ID="lblAddress2" runat="server"></asp:Label></td>
                            <td>City, State:</td>
                            <td><asp:Label ID="lblCity2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Description:</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDescription2" runat="server" 
                                    TextMode="MultiLine" Width="350px" Height="30px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Comments to Installer:</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtComments2" runat="server" TextMode="MultiLine" 
                                    Width="350px" Height="30px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td>Date:</td>
                                        <td><asp:Label ID="lblDate" runat="server" Text="06/06/2010"></asp:Label></td>
                                        <td>Time:</td>
                                        <td><asp:Label ID="lblTime" runat="server" Text="10:00 AM"></asp:Label></td>
                                        <td>Duration:</td>
                                        <td><asp:Label ID="lblDuration" runat="server" Text="4 h"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="4"><hr /></td></tr>
                        <tr>
                            <td colspan="2" align="center">Assign Vendor:</td>
                            <td colspan="2" align="left">
                                <asp:DropDownList ID="ddlVendors" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr><td colspan="4"><hr /></td></tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSelect" runat="server" Text="Save" Width="100px" 
                                    onclick="BtnSelectClick" />
                            </td>  
                            <td colspan="2" align="center">
                                <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Width="100px" 
                                    onclick="BtnCancel2Click" />
                            </td>             
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <br />
                <asp:Button ID="btnReturn" runat="server" Text="Return" Width="100px" 
                    PostBackUrl="~/RegionSelection.aspx" />
            </td>
        </tr>
        </table>
    </center>
<%--        <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
        </telerik:RadScriptManager>--%>
        <asp:SqlDataSource ID="sdsAppointments" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CommConnection %>" 
            InsertCommand="INSERT INTO [Appointments] ([AllDay], [Notes], [StartTime], [EndTime], [Subject], [VendorID]) VALUES (1, @Notes, @StartTime, @EndTime, @Subject, @VendorID)" 
            UpdateCommand="UPDATE [Appointments] SET [AllDay] = @AllDay, [Notes] = @Notes, [StartTime] = @StartTime, [EndTime] = @EndTime, [Subject] = @Subject, [VendorID] = @VendorID WHERE [ID] = @ID">
            <InsertParameters>
                <asp:Parameter Name="AllDay" Type="Boolean" />
                <asp:Parameter Name="Notes" Type="String" />
                <asp:Parameter Name="StartTime" Type="DateTime" />
                <asp:Parameter Name="EndTime" Type="DateTime" />
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="VendorID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="AllDay" Type="Boolean" />
                <asp:Parameter Name="Notes" Type="String" />
                <asp:Parameter Name="StartTime" Type="DateTime" />
                <asp:Parameter Name="EndTime" Type="DateTime" />
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="VendorID" Type="Int32" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsVendors" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CommConnection %>" > 
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsOpenOrders" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CommConnection %>" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsToAssign" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CommConnection %>" >
        </asp:SqlDataSource>
        <asp:HiddenField ID="hfOrderID" runat="server" />
        <asp:RequiredFieldValidator ID="valDate" runat="server" 
            ErrorMessage="Select Installation Date" ControlToValidate="rdInstall" 
        Display="None" ValidationGroup="Sched"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valStart" runat="server" 
            ControlToValidate="rtStart" Display="None" 
            ErrorMessage="Select Installation Start" ValidationGroup="Sched"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valEnd" runat="server" 
            ControlToValidate="rtEnd" Display="None" ErrorMessage="Select Installation End" 
            ValidationGroup="Sched"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="valSummSched" runat="server" ShowMessageBox="True" 
        ShowSummary="False" ValidationGroup="Sched" />
</asp:Content>
