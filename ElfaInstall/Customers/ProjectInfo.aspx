<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectInfo.aspx.cs" Inherits="ElfaInstall.Customers.ProjectInfo" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Information</title>
    <link href="../Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
    <center>
        <br />
        <asp:Image ID="imgHeader" runat="server" ImageUrl="~/images/Master_logo.gif" />
        <br />
        <table class="tblRed" width="955">
            <tr>
                <td class="tdheader"> Product/project Information</td>
            </tr>
            <tr>
                <td class="lblsize14">
                    <center>
                    Welcome – Please complete the check boxes in each section and submit your installalition confirmation - Thanks
                    </center>
                </td>
            </tr>
            <tr><td><hr style="color:#c43b40" /></td></tr>
            <tr>
                <td class="td14Bold">&nbsp;&nbsp;&nbsp;&nbsp;Customer Information</td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="td14" style="width:20%" valign="middle">Customer Name:</td>
                            <td>
                                <asp:Panel ID="pnlNameShow" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCustomerNameShow" runat="server" CssClass="lblsize14"></asp:Label>
                                            </td>
                                            <td style="width:25%">
                                                <telerik:RadButton ID="btnEditName" runat="server" Skin="Office2007" 
                                                    Text="Edit Customer Name" onclick="BtnEditNameClick" Width="150px"></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlNameEdit" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCustomerFirstName" runat="server" Width="150px" 
                                                    MaxLength="24"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomerMi" runat="server" Width="25px" MaxLength="1"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomerLastName" runat="server" Width="150px" 
                                                    MaxLength="24"></asp:TextBox>
                                            </td>
                                            <td style="width:25%">
                                                <telerik:RadButton ID="btnSaveName" runat="server" Skin="Office2007" 
                                                    Text="Save" onclick="BtnSaveNameClick" Width="70px"></telerik:RadButton>
                                                &nbsp;
                                                <telerik:RadButton ID="btnNameCancel" runat="server" Skin="Telerik" 
                                                    Text="Cancel" Width="70px" onclick="BtnNameCancelClick"></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="td14" style="width:20%" valign="middle">Installation Address:</td>
                            <td>
                                <asp:Panel ID="pnlAddressShow" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAddressShow" runat="server" CssClass="lblsize14"></asp:Label>
                                            </td>
                                            <td style="width:25%">
                                                <telerik:RadButton ID="btnEditAddress" runat="server" Skin="Office2007" 
                                                    Text="Edit Installation Address" Width="150px" 
                                                    onclick="BtnEditAddressClick"></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlAddressEdit" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAddress1" runat="server" MaxLength="50" Width="250px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:TextBox ID="txtAddress2" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="width:25%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCity" runat="server" MaxLength="25" Width="200px"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlStates" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtZip" runat="server" MaxLength="10" Width="75px"></asp:TextBox>
                                            </td>
                                            <td style="width:25%">
                                                <telerik:RadButton ID="btnSaveAddress" runat="server" Skin="Office2007" 
                                                    Text="Save" Width="70px" onclick="BtnSaveAddressClick"></telerik:RadButton>
                                                &nbsp;
                                                <telerik:RadButton ID="btnCancelAddress" runat="server" Skin="Telerik" 
                                                    Text="Cancel" Width="70px" onclick="BtnCancelAddressClick"></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="td14" style="width:20%" valign="middle">Best Contact Number:</td>
                            <td rowspan="2">
                                <asp:Panel ID="pnlContactChow" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblContactNumber" runat="server" CssClass="lblsize14"></asp:Label>
                                            </td>
                                            <td style="width:25%" rowspan="2">
                                                <telerik:RadButton ID="btnEditContactNumbers" runat="server" Skin="Office2007" 
                                                    Text="Edit Contact Numbers" Width="150px" 
                                                    onclick="BtnEditContactNumbersClick"></telerik:RadButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="18">
                                                <asp:Label ID="lblAlternateNumber" runat="server" CssClass="lblsize14"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlContactEdit" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtContactNumber" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="width:25%" rowspan="2">
                                                <telerik:RadButton ID="btnSaveContactNumber" runat="server" Skin="Office2007" 
                                                    Text="Save" Width="70px" onclick="BtnSaveContactNumberClick"></telerik:RadButton>
                                                &nbsp;
                                                <telerik:RadButton ID="btnCancelContactNumber" runat="server" Skin="Telerik" 
                                                    Text="Cancel" Width="70px" onclick="BtnCancelContactNumberClick"></telerik:RadButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <asp:TextBox ID="txtAlternativeNumber" runat="server" MaxLength="15" 
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="td14" style="width:20%" valign="middle">Alternate Number:</td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <br />
                                <asp:CheckBox ID="cbCustomerInfo" runat="server" /> 
                                <asp:Label ID="lblInfo1" runat="server" CssClass="lblsize14">The above information is correct or has been correctly edited and saved.</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlDelivery" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr><td><hr style="color:#c43b40" /></td></tr>
                                        <tr>
                                            <td class="td14Bold">&nbsp;&nbsp;&nbsp;&nbsp;Product Delivery</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rbDeliveryYes" runat="server" GroupName="Delivery" />
                                                <asp:Label ID="Label1" runat="server" CssClass="lblsize14">EIS is delivering my product. I understand there is an additional $80.00 charge.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rbDeliveryNo" runat="server" GroupName="Delivery" />
                                                <asp:Label ID="Label2" runat="server" CssClass="lblsize14">I have arranged to have the product here by install date myself.</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr><td><hr style="color:#c43b40" /></td></tr>
                        <tr>
                            <td class="td14Bold">&nbsp;&nbsp;&nbsp;&nbsp;Basic Demolition / Additional Demolition / Additional Services          </tr>
                        <tr>
                            <td class="td14">
                                <span style="color:Black">Basic demolition</span> is included in the installation service price. 
                                Basic demolition is defined as: EIS will remove up to 2 shelves and hanging rods per wall that receives new elfa product. 
                                EIS will repair, patch, and paint these areas of the walls that are affected by demolition. 
                                EIS will us a flat white paint product to paint unless customer supplies a custom paint that they want. 
                                The great majority of projects fit in this category.
                            </td>
                        </tr>
                        <tr>
                            <td class="td14">
                                <span style="color:Black">Additional Demolition</span> is required on a small percentage of projects and EIS charges a rate of $65 per hour to handle work. 
                                It is very rare that any additional demo ever takes more than a couple of hours. Examples of additional demo include: 
                                Removal of any shelves over and above 2 on a particular wall, removal of a column of wooden cubby hole style shelves on a wall, 
                                customer would like the entire closet or area repainted, and customer would like walls not receiving new elfa repaired, patched, 
                                and painted. Lastly, walls made out of plaster will occasionally damage badly during demolition. If this happens the repair 
                                will fall under additional demo.
                            </td>
                        </tr>
                        <tr>
                            <td class="td14">
                                <br />Please indicate which you believe pertains to this project.<br />
                                <asp:RadioButton ID="rbDemolition1" runat="server" GroupName="Demolition" />
                                <asp:Label ID="Label3" runat="server" CssClass="lblsize14">Basic demolition only.</asp:Label>
                                <br />
                                <asp:RadioButton ID="rbDemolition2" runat="server" GroupName="Demolition" />
                                <asp:Label ID="Label4" runat="server" CssClass="lblsize14">I think I have additional demolition – please call so I can describe to you.</asp:Label>
                                <br />
                                <asp:RadioButton ID="rbDemolition3" runat="server" GroupName="Demolition" />
                                <asp:Label ID="Label5" runat="server" CssClass="lblsize14">My walls are bare – There is no demolition at all.</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr><td><hr style="color:#c43b40" /></td></tr>
                        <tr>
                            <td class="td14Bold">&nbsp;&nbsp;&nbsp;&nbsp;Mileage fees</td>
                        </tr>
                        <tr>
                            <td class="td14">
                                Mileage fees are included in installation price for all jobs located within a 20 mile radius from store. 
                                If installation location is outside the 20 mile radius, a one way, onetime $3 dollar per mile charge for all miles 
                                past the 20 mile radius will be assessed. (ex. Installation is 35 miles away from store – mileage charge = 35mi - 20mi x 3 = $45)
                            </td>
                        </tr>
                        <tr>
                            <td class="td14">
                                <br />
                                <asp:RadioButton ID="rbMilage1" runat="server" GroupName="Milage" />
                                <asp:Label ID="Label6" runat="server" CssClass="lblsize14">My installation is within 20 miles from store.</asp:Label>
                                <br />
                                <asp:RadioButton ID="rbMilage2" runat="server" GroupName="Milage" />
                                 <asp:Label ID="Label7" runat="server" CssClass="lblsize14">Install is located more than 20 miles away from store – I understand the additional mileage fee.</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr><td><hr style="color:#c43b40" /></td></tr>
                        <tr>
                            <td class="td14Bold">&nbsp;&nbsp;&nbsp;&nbsp;Other Considerations</td>
                        </tr>
                        <tr>
                            <td class="td14">
                                Some properties such as high-rises or condominiums have Home Owners Associations and/or Management companies that dictate the times work can take place, 
                                require proof of insurance certificates, or other job related considerations. Please check appropriate box
                            </td>
                        </tr>
                        <tr>
                            <td class="td14">
                                <br />
                                <asp:RadioButton ID="rbOther1" runat="server" GroupName="Other" />
                                <asp:Label ID="Label8" runat="server" CssClass="lblsize14">There are no issues of this nature on my project.</asp:Label>
                                <br />
                                <asp:RadioButton ID="rbOther2" runat="server" GroupName="Other" />
                                <asp:Label ID="Label9" runat="server" CssClass="lblsize14">EIS will need to address with appropriate association or superintendant.</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td14">
                                <br />In some cases, installer will have to incur parking fees and/or tolls in order to perform installation. 
                                Customer agrees to reimburse install at time of payment if these are necessary.
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr><td><hr style="color:#c43b40" /></td></tr>
                        <tr>
                            <td class="td14Bold">&nbsp;&nbsp;&nbsp;&nbsp;Installation Fee</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbInstallFee" runat="server" /> 
                                <asp:Label ID="Label10" runat="server" CssClass="lblsize14">I understand my installation estimate is $</asp:Label> 
                                <asp:Label ID="lblPrice" runat="server" CssClass="lblmoneya">125.60</asp:Label>
                                <asp:Label ID="Label12" runat="server" CssClass="lblsize14">&nbsp;and that the invoice will be adjusted appropriately on-site if needed. </asp:Label>
                                <br /><br />
                                <asp:Label ID="Label11" runat="server" CssClass="lblsize14">EIS accepts check and major credit cards as payment.</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr><td colspan="2"><hr style="color:#c43b40" /></td></tr>
                        <tr>
                            <td class="td14Bold" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;Installation Date & Time</td>
                        </tr>
                        <tr>
                            <td class="td14" colspan="2">
                                I understand that I will need to prepare the spaces for installation services. 
                                All contents of the space will be removed from work area before installer arrives. 
                                <br />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="td14Bold" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;Your Installation is scheduled for</td>
                        </tr>
                        <tr>
                            <td class="tdheadersmall" colspan="2">
                                <asp:Label ID="lblInstallDate" runat="server">06/12/2011 10:00 AM</asp:Label> 
                                 - Installation time estimate
                            </td>
                        </tr>
                        <tr>
                            <td class="td14" colspan="2">
                                <br />
                                <asp:RadioButton ID="rbInstallDate1" runat="server" GroupName="InstallDate" />
                                <asp:Label ID="Label13" runat="server" CssClass="lblsize14">Yes – that is correct.</asp:Label>
                                <br />
                                <asp:RadioButton ID="rbInstallDate2" runat="server" GroupName="InstallDate" />
                                <asp:Label ID="Label14" runat="server" CssClass="lblsize14">No – That is not correct – please call.</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td14" colspan="2">
                                <br />Please allow a 1 hour window of time before or after install time for traffic and other considerations.
                                <br />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="td14" valign="top" width="30%">
                                Additional information you may have: 
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAdditional" runat="server" TextMode="MultiLine" 
                                    Width="650px" Rows="2" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr><td><hr style="color:#c43b40" /></td></tr>
                        <tr>
                            <td class="td14">
                                Thanks You – <br />
                                We truly look forward to providing you an exceptional installation experience. Please feel free to call with any special circumstances, 
                                issue, or questions. We can be reached during normal business hours at 1-888-202-7622. <br /><br />
                                Your Elfa Installation Services team.
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <telerik:RadButton ID="btnSubmit" runat="server" Text="Confirm Your Installation" 
                                    Font-Size="Medium" Skin="Forest" onclick="BtnSubmitClick" Height="30px">
                                </telerik:RadButton>
                                <br />
                                <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                <br />&nbsp;
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfOrderID" runat="server" />
    </center>
    </div>
<%--    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>--%>
<%--    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>--%>
    </form>
    
</body>
</html>
