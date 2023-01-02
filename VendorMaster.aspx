<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="VendorMaster" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Vendor Master<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search" class="middle">
            <asp:Label ID="lblReportPeriod" runat="server"></asp:Label>
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <div class="row-fluid">
            
            <div class="clearfix">
            </div>
            <div class="row-fluid">
                <div class="widget-box">
                    <div class="widget-header1 widget-hea1der-small">
                        <h4 class="lighter">Vendor Master Form </h4> <h4 class="lighter" style="float:right"></h4>
                                                                          
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="widget-main no-padding">
                                <div class="content">
                                    <table class="table table-striped table-bordered table-advance table-hover">
                                        <tbody>
                                            <tr>
                                                <td style="width:20%"> <span class="red">Type Of Vendor</span></td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:DropDownList ID="ddlTypeOfVendor" runat="server" OnSelectedIndexChanged="ddlTypeOfVendor_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlTypeOfVendor" InitialValue="--Select--" ControlToValidate="ddlTypeOfVendor" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>
                                                 <td style="width:20%"> <span class="red">Name Of Company</span></td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtNameOfCompany" runat="server" Width="205px" MaxLength="40"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtNameOfCompany" ControlToValidate="txtNameOfCompany" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Vendor Code</td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtVendorCode" runat="server" Width="205px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtVendorCode" ControlToValidate="txtVendorCode" ErrorMessage="#" runat="server" CssClass="red" Enabled="false"  ></asp:RequiredFieldValidator>
                                                </td>
                                                 <td style="width:20%"> <span class="red">Address Id</span></td> 
                                                <td class="width-30%">
                                                    <asp:DropDownList ID="ddlAddressId" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlAddressId" InitialValue="--Select--" ControlToValidate="ddlAddressId" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%"> <span class="red">Company Code</span></td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:DropDownList ID="ddlCompanyCode" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlCompanyCode" InitialValue="--Select--" ControlToValidate="ddlCompanyCode" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>
                                                 <td style="width:20%">Floor Number</td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtFloorNo" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%"> <span class="red">Name 1</span></td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtName1" runat="server" Width="205px" MaxLength="35" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtName1" ControlToValidate="txtName1" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>
                                                 <td style="width:20%">Room Number</td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtRoomNo" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Name 2</td>                                                
                                                <td class="width-30%" style="width: 235px" >
                                                    <asp:TextBox ID="txtName2" runat="server" Width="205px" ></asp:TextBox>
                                                </td>
                                                 <td style="width:20%"> <span class="red">Country</span></td>  
                                                <td class="width-30%">
                                                    <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" >
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RFVddlCountry" InitialValue="--Select--" ControlToValidate="ddlCountry" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>                                              
                                            </tr>
                                            <tr>
                                                <td style="width:20%;">Name 3</td>                                                
                                                <td class="width-30%" width: 235px" >
                                                    <asp:TextBox ID="txtName3" runat="server" Width="205px" ></asp:TextBox>
                                                </td>
                                                 <td style="width:20%"> <span class="red">State</span></td> 
                                                <td class="width-30%">
                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlState" InitialValue="--Select--" ControlToValidate="ddlState" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Name 4</td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtName4" runat="server" Width="205px" ></asp:TextBox>
                                                </td>
                                                 <td style="width:20%"> <span class="red">City</span></td>
                                                 <td class="width-30%">
                                                    <asp:DropDownList ID="ddlCity" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlCity" InitialValue="--Select--" ControlToValidate="ddlCity" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                 </td>                                                
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Telephone Number</td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtTelephoneNo" runat="server" Width="205px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator runat="server" ID="retxtTelephoneNo" ControlToValidate="txtTelephoneNo" ValidationExpression="^[0-9]{1}[0-9]{5,11}$" ErrorMessage="X" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                 <td style="width:20%">Postal Code </td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="6" Width="205px"></asp:TextBox>
                                                   <asp:RegularExpressionValidator runat="server" ID="retxtPostalCode" ControlToValidate="txtPostalCode" ValidationExpression="^[0-9]{6}$" ErrorMessage="Enter Valid postal code" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>                                               
                                            </tr>
                                             <tr>
                                                <td style="width:20%"><span class="red">Pan Number</span></td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtPanNo" runat="server" Width="205px" MaxLength="40"></asp:TextBox>
                                                    <asp:RegularExpressionValidator runat="server" ID="retxtPanNo" ValidationExpression="^[a-zA-Z]{5}\d{4}[a-zA-Z]{1}$" ControlToValidate="txtPanNo" ErrorMessage="X" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                 <td style="width:20%">House Number</td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtHouseNo" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Extension</td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtExtension" runat="server" Width="205px"></asp:TextBox>
                                                </td>
                                                 <td style="width:20%">Street 1</td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtStreet1" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%"> <span class="red">Email Address</span></td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtEmail" runat="server" Width="205px" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtEmail" ControlToValidate="txtEmail" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="retxtEmail"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ControlToValidate="txtEmail" ErrorMessage="X" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                 <td style="width:20%">Street 2</td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtStreet2" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Mobile Number 1</td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtMobNo1" runat="server" MaxLength="10" Width="205px"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="retxtMobNo1" ValidationExpression="^[789][0-9]{9}$" ControlToValidate="txtMobNo1" ErrorMessage="X" runat="server" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                 <td style="width:20%">Street 3</td>  
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtStreet3" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                              
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Mobile Number 2</td>                                                
                                                <td class="width-30%" style="width: 235px">
                                                    <asp:TextBox ID="txtMobNo2" runat="server" MaxLength="10" Width="205px"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="retxtMobNo2" ValidationExpression="^[789][0-9]{9}$" ControlToValidate="txtMobNo2" ErrorMessage="X" runat="server" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                 <td style="width:20%">Street 4</td> 
                                                <td class="width-30%">
                                                    <asp:TextBox ID="txtStreet4" runat="server" Width="205px"></asp:TextBox>
                                                </td>                                               
                                            </tr>
                                            <tr>
                                                <td style="width:20%">Is Active</td>
                                                <td class="width-30%" style="width:235px">
                                                    <label>
                                                        <input runat="server" id="chkActive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                           checked="checked" />
                                                         <span class="lbl"></span>
                                                      </label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                                        <asp:Button ID="btnVendorInfoSave" runat="server" Text="Save" CssClass="btn btn-success btn-app btn-mini radius-4" Width="15%" OnClick="btnVendorInfoSave_Click" />
                                        <asp:Button ID="btnVendorInfoUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-app btn-mini  radius-4" Width="15%" OnClick="btnVendorInfoUpdate_Click" Visible="false" />
                                        <asp:Button ID="btnAddNewAddress" runat="server" Text="Add Another Address" CssClass="btn btn-success btn-app btn-mini radius-4" Width="15%" OnClick="btnAddNewAddress_Click"  />
                                        <asp:Button ID="btnVendorInfoCancel" runat="server" Text="Cancel" CssClass="btn btn-app btn-danger btn-primary btn-mini radius-4" Width="15%" OnClick="btnVendorInfoCancel_Click" CausesValidation="false" Visible="false" />
                                        <asp:Button ID="btnVendorInfoBack" runat="server" Text="Back" CssClass="btn btn-app btn-primary btn-mini radius-4" Width="15%" OnClick="btnVendorInfoBack_Click" CausesValidation="false" />
                                    </div>
                                    <div class="clearfix">
                                        &nbsp;
                                    </div>
                                    <div class="alert alert-error" id="diverror" visible="false" runat="server">
                                        <button type="button" class="close" data-dismiss="alert">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <strong><i class="icon-remove"></i></strong>
                                            <asp:Label ID="lblerrormsg" runat="server" Text="Label"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="alert alert-success" id="divsuccess" visible="false" runat="server">
									            <button type="button" class="close" data-dismiss="alert"><i class="icon-remove"></i></button>
									            <strong><i class="icon-ok"></i></strong>
									            <asp:Label ID="lblsuccessmsg" runat="server" Text="Label"></asp:Label>
								    </div>
                                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" CausesValidation="false" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                                    <asp:GridView ID="VendorInfoGridView" runat="server" DataKeyNames="Vendor_code"  ShowHeaderWhenEmpty="true" 
                                            CssClass="table1 table-striped table-bordered table-hover" OnRowCommand="VendorInfoGridView_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Type Of Vendor" DataField="Type_Of_Vendor" />
                                                <asp:BoundField HeaderText="Vendor Code" DataField="Vendor_code" />
                                                <asp:BoundField HeaderText="Company Code" DataField="Company_Code" />
                                                <asp:BoundField HeaderText="Name" DataField="NAME1" />
                                                <asp:BoundField HeaderText="PAN No" DataField="Pan_No" />
                                                <asp:BoundField HeaderText="Email ID" DataField="Email_Address" />
                                                <asp:BoundField HeaderText="Telephone No" DataField="Telephone_No"  ItemStyle-HorizontalAlign="Right"/>
                                                <asp:BoundField HeaderText="Mobile No" DataField="Mobile_No"  ItemStyle-HorizontalAlign="Right"/>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="btnVendorInfoInActive" runat="server" Text='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>' CommandName='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>' CommandArgument='<%# Eval("Vendor_code") %>'
                                                            CssClass='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "btn btn-mini btn-success" : "btn btn-mini btn-danger1" %>'  CausesValidation="false"></asp:LinkButton>--%>
                                                        <asp:Label ID="lblVendorInfoInActive" runat="server" Text='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>'
                                                             CssClass= '<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ?  "lbl-success" : "lbl-danger" %>'
                                                            ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnVendorInfoEdit" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Eval("Vendor_code") %>'
                                                            CssClass="btn btn-mini btn-info1"  CausesValidation="false" ></asp:LinkButton> 
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

