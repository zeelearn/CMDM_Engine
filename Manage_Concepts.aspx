<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Manage_Concepts.aspx.cs" Inherits="Manage_Concepts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };


        function Chkactiveedit_onclick() {

        }

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
                <h4 class="blue">Concepts<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" Width="150px"  ID="btnnavigateexcel"
                Text="Upload Via Excel" OnClick="btnnavigateexcel_Click"  />
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
                                <h5>Search Options
                                </h5>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblsearchcourse" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlsearchcourse" Width="218px" ToolTip="Course"
                                                                    data-placeholder="Select Course" CssClass="chzn-select"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlsearchcourse_SelectedIndexChanged" />
                                                            </td>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        
                                                            <tr>
                                                                <td class="span2" style="border-style: none; text-align: left;">
                                                                    <asp:Label ID="lblsearchsubject" runat="server" CssClass="red">Subject</asp:Label>
                                                                </td>
                                                                <td class="span4" style="border-style: none; text-align: left;">
                                                                    <asp:DropDownList ID="ddlsearchsubject" runat="server" AutoPostBack="True"
                                                                        CssClass="chzn-select" data-placeholder="Select Subject" ToolTip="Subject" Width="218px" OnSelectedIndexChanged="ddlsearchsubject_SelectedIndexChanged" />
                                                                </td>
                                                        </table>
                                                    
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                                        
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12">Concept Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtsearchconceptname" ToolTip="Concept Name" Width="205px" />
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
                                            runat="server" Text="Clear" OnClick="BtnClearSearch_Click1" />
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
                                <td class="span10">Total No of Records:
                            <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" ForeColor="White" runat="server"
                                        Text="Export" OnClick="HLExport_Click2" />
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
                        <b>Course </b> </th>
                <th>Subject
                </th>
                        <th>Concept Name
                        </th>
                        <th>Concept Generic Name
                        </th>
                        <th>Module AV
                        </th>
                        <th align="left" style="width: 15%">Status
                        </th>
                        <th style="text-align: center">Action
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                <td>
                    <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                </td>
                        <td>
                            <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Concept_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Concept_Generic_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module_AV")%>' />
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
                    runat="server" Width="100%"
                    OnSelectedIndexChanged="dlGridExport_SelectedIndexChanged">
                    <HeaderTemplate>
                        <b>Course</b> </th>
                <th>Subject
                </th>
                        <th>Concept Name
                        </th>
                        <th>Concept Generic Name
                        </th>
                        <th>Module AV
                        </th>
                        <th>Status
                        </th>

                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                <td>
                    <asp:Label ID="lblCentre1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                </td>
                        <td>
                            <asp:Label ID="lblStandard1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Concept_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Concept_Generic_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module_AV")%>' />
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
                        <h5 class="modal-title">Edit Concept
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="lbleditcourse" CssClass="red">Course</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddleditcourse" Width="218px" ToolTip="Course"
                                                                data-placeholder="Select Course" CssClass="chzn-select"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddleditcourse_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label ID="lbleditsubject" runat="server" CssClass="red">Subject</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddleditsubject" Width="218px" ToolTip="Subject"
                                                                data-placeholder="Select Subject" CssClass="chzn-select"
                                                                AutoPostBack="True" />
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
                                                        <asp:Label runat="server" ID="Label1" CssClass="red">Concept Name</asp:Label>
                                                        <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                    </td>
                                                    <td class="span4" style="border-style: none; text-align: left;">
                                                        <asp:TextBox runat="server" ID="txteditconcepname" ToolTip="Concept Name" Width="205px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span6" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td class="span2" style="border-style: none; text-align: left;">
                                                        <asp:Label runat="server" ID="Label2">Concept Description</asp:Label>
                                                    </td>
                                                    <td class="span4" style="border-style: none; text-align: left;">
                                                        <asp:TextBox runat="server" ID="txteditconceptdescription" ToolTip="Concept Description"
                                                            Width="205px" />
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
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Concept Generic Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txteditconceptgenericname" ToolTip="Concept Generic Name"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label10" >Module AV</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txteditmoduleav" ToolTip="Module AV"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label7" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="Chkactiveedit" name="switch-field-2" type="checkbox" class="ace-switch ace-switch-2"
                                                                        checked="checked" onclick="return Chkactiveedit_onclick()" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    
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
                                Text="Save" ValidationGroup="UcValidate" OnClick="btnedit_save_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnedit_cose" Visible="true"
                                runat="server" Text="Close" OnClick="btnedit_cose_Click" />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivAddPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">Create New Concept
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
                                                                <asp:Label runat="server" ID="lbladdcourse" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddladdcourse" Width="218px" ToolTip="Course"
                                                                    data-placeholder="Select Course" CssClass="chzn-select"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddladdcourse_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label ID="lbladdsubject" runat="server" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddladdsubject" Width="218px" ToolTip="Subject"
                                                                    data-placeholder="Select Subject" CssClass="chzn-select"
                                                                    AutoPostBack="True" />
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
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Concept Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtconceptname" ToolTip="Concept Name" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label4">Concept Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtconceptdescription" ToolTip="Concept Description"
                                                                    Width="205px" />
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
                                                                <asp:Label runat="server" ID="Label5" CssClass="red">Concept Generic Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtconceptgenericname" ToolTip="Concept Generic Name"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label11" >Module AV</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtmoduleav" ToolTip="Module AV"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label ID="Label38" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <label>
                                                                    <input runat="server" id="chkActive" name="switch-field-2" type="checkbox" class="ace-switch ace-switch-2"
                                                                        checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    
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
                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
