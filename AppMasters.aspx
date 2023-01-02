<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AppMasters.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Modify Master<span class="divider"></span></h4>
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
                    <div class="widget-header widget-hea1der-small">
                        <h4 class="lighter">Modify Master Form </h4> <h4 class="lighter" style="float:right"><span> &nbsp;</span></h4>
                                                                          
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="widget-main no-padding">
                                <div class="content">
                                    <table class="table table-striped table-bordered table-advance table-hover">
                                        <thead>
                                            <tr>
                                                  <th style="width: 247px">Master Name</th>
                                                  <th style="width: 235px">Action</th>
                                              </tr>
                                        </thead>
                                        <tbody>
                                              <tr>
                                                  <td style="width: 247px">Document Master</td>
                                                  <td>
                                                <asp:Button ID="btnDocReferenceMasrer" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnDocReferenceMasrer_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Status Master</td>
                                                  <td>
                                                <asp:Button ID="btnStatusMaster" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="Button1_Click"  />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Address Type</td>
                                                  <td>
                                                <asp:Button ID="btnAddressType" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnAddressType_Click"   />
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="width: 247px">Vendor Type</td>
                                                  <td>
                                                <asp:Button ID="btnVendorTypeMaster" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnVendorTypeMaster_Click" />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Vendor Master</td>
                                                  <td>
                                                <asp:Button ID="btnVendorInfo" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnVendorMaster_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <%--<tr>
                                                  <td style="width: 247px">VendorAddress.aspx</td>
                                                  <td>
                                                <asp:Button ID="btnNendorAddress" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnNendorAddress_Click"   />
                                                  </td>
                                              </tr>
                                           --%>
                                              <tr>
                                                  <td style="width: 247px">Designation Master</td>
                                                  <td>
                                                <asp:Button ID="btnDesignation_Master" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnDesignation_Master_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Assign Responsible Person To Division</td>
                                                  <td>
                                                <asp:Button ID="btnDivision_Responsible_person" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnDivision_Responsible_person_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Premises Type</td>
                                                  <td>
                                                <asp:Button ID="btnPremises_Type" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnPremises_Type_Click"   />
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="width: 247px">Agreement Type</td>
                                                  <td>
                                                <asp:Button ID="btnAgreement_Type_Master" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnAgreement_Type_Master_Click"   />
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="width: 247px">Period Master</td>
                                                  <td>
                                                <asp:Button ID="btnPeriod_Master" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnPeriod_Master_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Payment Type</td>
                                                  <td>
                                                <asp:Button ID="btnPayment_Type_Master" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnPayment_Type_Master_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Terms Of Payment</td>
                                                  <td>
                                                <asp:Button ID="btnTerms_Of_Payment" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnTerms_Of_Payment_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Assign Permission Id To Center</td>
                                                  <td>
                                                <asp:Button ID="btnCentre_Master" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnCentre_Master_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Power Backup Type</td>
                                                  <td>
                                                <asp:Button ID="btnPower_Backup_Type" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnPower_Backup_Type_Click"   />
                                                  </td>
                                              </tr>
                                           
                                              <tr>
                                                  <td style="width: 247px">Property Document Type</td>
                                                  <td>
                                                <asp:Button ID="btnProperty_Document_Type" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnProperty_Document_Type_Click"   />
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="width: 247px">Payment Mode</td>
                                                  <td>
                                                      <asp:Button ID="btnPaymentMode" runat="server" Text="View/Modify" CssClass="btn btn-mini" OnClick="btnPaymentMode_Click"/></td>
                                              </tr>
                                           
                                        </tbody>
                                    </table>
                       
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    
</asp:Content>

