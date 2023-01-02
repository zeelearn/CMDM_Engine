<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="DocReferenceMaster.aspx.cs" Inherits="DocReferenceMaster" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">Document Reference Master<span class="divider"></span></h4>
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
                        <h4 class="lighter">Document Reference Form </h4>
                        <h4 class="lighter" style="float: right"></h4>

                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="widget-main no-padding">
                                <div class="content">
                                    <table class="table table-striped table-bordered table-advance table-hover">
                                        <thead>
                                        </thead>
                                        <tbody>

                                            <tr>
                                                <td style="width: 27%"><Span Class="red">Reference Id</Span></td>
                                                <td>
                                                    <asp:TextBox ID="txtReferenceId" runat="server" MaxLength="5" Width="205px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtReferenceId" ControlToValidate="txtReferenceId" ErrorMessage="#" runat="server" CssClass="red"  ></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="display: none">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 27%"><Span Class="red">Reference Document Description</Span></td>
                                                <td>
                                                    <asp:TextBox ID="txtReferenceDocumentDescription" runat="server" MaxLength="30" Width="205px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtReferenceDocumentDescription" ControlToValidate="txtReferenceDocumentDescription" ErrorMessage="#" runat="server" CssClass="red" ></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="display: none"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 27%"><Span Class="red">Number Range Count From</Span></td>
                                                <td>
                                                    <asp:TextBox ID="txtNoRangeCountFrom" runat="server" MaxLength="10"  Width="205px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtNoRangeCountFrom" ControlToValidate="txtNoRangeCountFrom" ErrorMessage="#" runat="server" CssClass="red" ></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REVtxtNoRangeCountFrom" ValidationExpression="^[0-9]+$" ControlToValidate="txtNoRangeCountFrom" ErrorMessage="Except only numeric values." runat="server" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                <td style="display: none"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 27%"><Span Class="red">Number Range Count To</Span></td>
                                                <td>
                                                    <asp:TextBox ID="txtNoRangeCountTo" runat="server" MaxLength="10" Width="205px" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtNoRangeCountTo" ControlToValidate="txtNoRangeCountTo" ErrorMessage="#" runat="server" CssClass="red" ></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REVtxtNoRangeCountTo" ValidationExpression="^[0-9]+$" ControlToValidate="txtNoRangeCountTo" ErrorMessage="Except only numeric values." runat="server" CssClass="red"></asp:RegularExpressionValidator>
                                                </td>
                                                <td style="display: none"></td>
                                            </tr>
                                            <tr>
                                                <td style="width:27%">Is Active</td>
                                                <td>
                                                    <label>
                                                        <input runat="server" id="chkActive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                           checked="checked" />
                                                         <span class="lbl"></span>
                                                      </label>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td style="width: 27%">&nbsp;Current Number Range</td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrentNumberRange" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="display: none">&nbsp;</td>
                                            </tr>

                                        </tbody>
                                    </table>
                                    <div class="clearfix">
                                        &nbsp;
                                    </div>
                                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                                        <asp:Button ID="btnDocReferenceMasterSave" runat="server" Text="Save" CssClass="btn btn-success btn-app btn-mini  radius-4" Width="15%" OnClick="btnDocReferenceMasterSave_Click" />
                                        <asp:Button ID="btnDocReferenceMasterUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-app btn-mini  radius-4" Width="15%" OnClick="btnDocReferenceMasterUpdate_Click" Visible="false" />
                                        <asp:Button ID="btnDocReferenceMasterCancel" runat="server" Text="Cancel" CssClass="btn btn-app btn-danger btn-primary btn-mini radius-4" Width="15%" CausesValidation="false" Visible="false" OnClick="btnDocReferenceMasterCancel_Click" />
                                        <asp:Button ID="BtnDocReferenceMasterBack" runat="server" Text="Back" CssClass="btn btn-app btn-primary btn-mini radius-4" Width="15%" OnClick="BtnDocReferenceMasterBack_Click" CausesValidation="false" />
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
                                    
                                    <div>
                                        <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount"  />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" CausesValidation="false" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                                        
                                        <asp:GridView ID="ReferenceDocGridView" runat="server" DataKeyNames="Ref_Id"  ShowHeaderWhenEmpty="True"
                                               CssClass="table1 table-striped table-bordered table-hover"
                                            OnRowCommand="ReferenceDocGridView_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Document ID" DataField="Ref_Id"  HeaderStyle-HorizontalAlign="Center">
                                                
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Document Description" DataField="Ref_Doc_Description" />
                                                <asp:BoundField HeaderText="Count From" DataField="Number_Range_Count_From"  ItemStyle-HorizontalAlign="Right"/>
                                                <asp:BoundField HeaderText="Count To" DataField="Number_Range_Count_To" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="btnDocReferenceMasterInActive"  runat="server" Text='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>' CommandName='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>' CommandArgument='<%# Eval("Ref_Id") %>'
                                                            CssClass='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "btn btn-mini btn-success" : "btn btn-mini btn-danger1" %>'  CausesValidation="false"></asp:LinkButton>--%>
                                                        <asp:Label ID="lblDocReferenceMasterInActive" runat="server" Text='<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ? "Active" : "InActive" %>'
                                                             CssClass= '<%# (Convert.ToInt32(Eval("Is_Active")) == 0) ?  "lbl-success" : "lbl-danger" %>'
                                                            ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDocReferenceMasterEdit" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Eval("Ref_Id") %>'
                                                            CssClass="btn btn-mini btn-info1"  CausesValidation="false" ></asp:LinkButton>
                                                        
                                                    <%--    <asp:LinkButton ID="btnDocReferenceMasterActive" runat="server" Text="Active" CommandName="Active" CommandArgument='<%# Eval("Ref_Id") %>'
                                                            CssClass="btn btn-mini btn-success"  CausesValidation="false" Visible="false"></asp:LinkButton>--%>
                                                        <%--<asp:Button ID="btnDocReferenceMasterEdit" runat="server" Text="" CssClass="btn btn-mini btn-info" Width="" OnClick="btnDocReferenceMasterEdit_Click">
                                                        </asp:Button>--%>
                                                        <%--<button class="btn btn-mini btn-info" title="Edit" id="btnDocReferenceMasterEdit" OnClick="btnDocReferenceMasterEdit_Click()" runat="server">
                                                            <i class="icon-edit"></i>
                                                        </button>--%>
                                                       <%-- <button class="btn btn-mini btn-danger" title="InActive" id="btnDocReferenceMasterInActive" OnClick="btnDocReferenceMasterInActive_Click" runat="server">
                                                            <i class="icon-trash"></i>
                                                        </button>--%>
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
    </div>
</asp:Content>

