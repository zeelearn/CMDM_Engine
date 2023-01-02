<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Manage_DifficultyLevelParameter.aspx.cs" Inherits="Manage_DifficultyLevelParameter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Difficulty Level Parameter <span class="divider"></span>
                </h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="New" OnClick="BtnAdd_Click" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-block alert-success" id="Msg_Success" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                    <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="DivSearchPanel" runat="server">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5>
                                    Search Options
                                </h5>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="lblparametername">Parameter Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtsearchparamaetrname" ToolTip="Concept Name" Width="205px" />
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </tr>
                                    </div>
                                    <div class="widget-main alert-block alert-info" style="text-align: center;">
                                        <!--Button Area -->
                                        <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                            Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                        <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                            runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                            ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                                        <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnSearch" />
                        <asp:PostBackTrigger ControlID="BtnClearSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" ForeColor="White" runat="server"
                                        Text="Export" onclick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <%--<table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label10">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivision_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label11">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            
                        </td>
                    </tr>
                </table>--%>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Parameter Name</b> </th>
                        <th>
                            Parameter Short Description
                        </th>
                        <th>
                            Parameter Long Description
                        </th>
                        <th align="left" style="width: 15%">
                            Status
                        </th>
                        <th style="text-align: center">
                            Action
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Parameter_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Parameter_Short_Description")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Parameter_Long_Description")%>' />
                        </td>
                        <td>
                            <asp:Label CssClass='<%# Convert.ToInt32( Eval("is_active")) == 1 ? "label label-success":"label label-warning"  %>'
                                runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "is_active") == 1 ? "Active" : "Inactive" %>  
                            </asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                data-rel="tooltip" CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"record_id")%>'
                                ToolTip="Edit" data-placement="left"></asp:LinkButton>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Parameter Name</b> </th>
                        <th>
                            Parameter Short Description
                        </th>
                        <th>
                            Parameter Long Description
                        </th>
                        <th>
                            Status
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Parameter_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCentre1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Parameter_Short_Description")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStandard1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Parameter_Long_Description")%>' />
                        </td>
                        <td>
                            <asp:Label CssClass='<%# Convert.ToInt32( Eval("is_active")) == 1 ? "label label-success":"label label-warning"  %>'
                                runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "is_active") == 1 ? "Active" : "Inactive"%>
                            </asp:Label>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivEditPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Edit Difficulty Level
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbleditparametername" CssClass="red">Parameter Name</asp:Label>
                                                                <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txteditparametername" ToolTip="Parameter Name" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbleditparametershordesc"  CssClass="red">Parameter Short Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txteditparametershordesc" ToolTip="Parameter Short Description"
                                                                   TextMode="MultiLine"  Width="205px"  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbleditparameterlongdesc" CssClass="red">Parameter Long Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txteditparameterlongdesc" ToolTip="Parameter Long Description"
                                                                  TextMode="MultiLine"  Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label7" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="Chkactiveedit" name="switch-field-2" type="checkbox" class="ace-switch ace-switch-2"
                                                                        checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="Label8" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnedit_save" runat="server"
                                Text="Save" ValidationGroup="UcValidate" onclick="btnedit_save_Click"  />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnedit_cose" Visible="true"
                                runat="server" Text="Close" onclick="btnedit_cose_Click"  />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivAddPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Create New Difficulty Level
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbladdparamatername" CssClass="red">Parameter Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtaddparamatername" ToolTip="Parameter Name" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbladdparamatershortdesc" CssClass="red">Parameter Short Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtaddparamatershortdesc" ToolTip="Parameter Short Description"
                                                                    TextMode="MultiLine" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbladdparamaterlongdesc" CssClass="red">Parameter Long Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtaddparamaterlongdesc" ToolTip="Parameter Long Description"
                                                                    TextMode="MultiLine" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label38" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="chkActive" name="switch-field-2" type="checkbox" class="ace-switch ace-switch-2"
                                                                        checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                                Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveAdd_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" onclick="BtnCloseAdd_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
