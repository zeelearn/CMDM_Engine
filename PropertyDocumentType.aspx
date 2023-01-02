<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PropertyDocumentType.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Property Document Type<span class="divider"></span></h4>
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
                        <h4 class="lighter">Property Document Type Form </h4> <h4 class="lighter" style="float:right"></h4>
                                                                          
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="widget-main no-padding">
                                <div class="content">
                                    <table class="table table-striped table-bordered table-advance table-hover">
                                        <tbody>
                                            <tr>
                                                <td style="width:27%"> <span class="red">Document Id</span></td>                                                
                                                <td style="width:90%">
                                                    <asp:TextBox ID="txtPropertyDocID" runat="server" MaxLength="3" Width="205px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtPropertyDocID" ControlToValidate="txtPropertyDocID" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="display:none">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width:27%"> <span class="red">Document Description</span></td>                                                
                                                <td style="width:90%">
                                                    <asp:TextBox ID="txtPropertyDocDescription" runat="server" MaxLength="40" Width="205px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtPropertyDocDescription" ControlToValidate="txtPropertyDocDescription" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="display:none">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width:27%">Is Active</td>
                                                <td style="width:90%">
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
                                        <asp:Button ID="btnPropertyDocumentTypeSave" runat="server" Text="Save" CssClass="btn btn-success btn-app btn-mini radius-4" Width="15%" OnClick="btnPropertyDocumentTypeSave_Click" />
                                        <asp:Button ID="btnPropertyDocumentTypeUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-app btn-mini  radius-4" Width="15%" OnClick="btnPropertyDocumentTypeUpdate_Click" Visible="false" />
                                         <asp:Button ID="btnPropertyDocumentTypeCancel" runat="server" Text="Cancel" CssClass="btn btn-app btn-danger btn-primary btn-mini radius-4" Width="15%" CausesValidation="false" Visible="false" OnClick="btnPropertyDocumentTypeCancel_Click" />
                                        <asp:Button ID="btnPropertyDocumentTypeBack" runat="server" Text="Back" CssClass="btn btn-app btn-primary btn-mini radius-4" Width="15%" OnClick="btnPropertyDocumentTypeBack_Click"  CausesValidation="false"/>
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
                                    <asp:GridView ID="PropertyDocumentTypeGridView" runat="server" DataKeyNames="Property_Doc_ID"  ShowHeaderWhenEmpty="true" 
                                            CssClass="table1 table-striped table-bordered table-hover" OnRowCommand="PropertyDocumentTypeGridView_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Property Document Type ID" DataField="Property_Doc_ID" />
                                                <asp:BoundField HeaderText="Document Type Description" DataField="Property_Doc_Description" />
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="btnPropertyDocumentTypeInActive" runat="server" Text='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>' CommandName='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>' CommandArgument='<%# Eval("Property_Doc_ID") %>'
                                                            CssClass='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "btn btn-mini btn-success" : "btn btn-mini btn-danger1" %>'  CausesValidation="false"></asp:LinkButton>--%>
                                                        <asp:Label ID="lblPropertyDocumentTypeInActive" runat="server" Text='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>'
                                                             CssClass= '<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ?  "lbl-success" : "lbl-danger" %>'
                                                            ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPropertyDocumentTypeEdit" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Eval("Property_Doc_ID") + "@" + Eval("Property_Doc_Description")+"@"+Eval("Is_Active") %>'
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

