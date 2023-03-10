<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Lesson_Plan.aspx.cs" Inherits="Lession_Plan_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalAssignProduct() {
            $('#DivAssignProduct').modal({
                backdrop: 'static'
            })

            $('#DivAssignProduct').modal('show');

        };

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57) || AsciiValue == 32 || AsciiValue == 45 || AsciiValue == 46 || AsciiValue == 95)
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="#">Home</a><span class="divider"><i class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">Lesson Plan<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <!-- -->
            <!-- PAGE CONTENT BEGINS HERE -->
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
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" ToolTip="Course"
                                                                    data-placeholder="Select Standard" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSubject" Width="215px" ToolTip="Subject"
                                                                    data-placeholder="Select Subject" CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
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
                                                    <asp:Label runat="server" ID="Label2">Course</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label3">Subject</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblSubject_Result" class="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblPkey" runat="server" Visible="false"></asp:Label>
                            <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                                <HeaderTemplate>
                                    <b>Lessonplan Code</b>
                                    </th>
                                <th>Lesson Plan Name</th>
                                    <th align="left" style="width: 25%">Chapter Name
                                    </th>
                                    <th style="width: 10%; text-align: center;">Module Count
                                    </th>

                                    <th style="width: 10%; text-align: center;">Last Lecture
                                    </th>
                                    <th style="width: 10%; text-align: center">Status
                                    </th>
                                    <th style="width: 10%; text-align: center;">
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label30" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanCode")%>' />
                                    </td>
                                <td>
                                    <asp:Label ID="lblDLChapterShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanName")%>' />
                                </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ModuleCount")%>' />
                                    </td>

                                    <td style="text-align: center;">
                                        <asp:Label ID="lblEOC" runat="server" Text='<%# Convert.ToBoolean( Eval("EOC")) == true ? "Yes":"No"  %>' />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>'
                                            runat="server" ID="Label31"> 
                              <%#(int)DataBinder.Eval(Container.DataItem, "IsActive") == 1 ? "Active" : "Inactive"%>                         
                                        </asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LessonPlanCode")%>' runat="server"
                                            CommandName="Edit" Height="25px" />
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" Visible="false">
                                <HeaderTemplate>
                                    <b>Lesson Plan Code</b>
                                    </th>
                                <th>Lesson Plan Name </th>
                                    <th align="left" style="width: 25%">Chapter Name
                                    </th>
                                    <th style="width: 10%; text-align: center;">Module Name
                                    </th>

                                    <th style="width: 10%; text-align: center;">Last Lecture
                                    </th>
                                    <th style="width: 10%; text-align: center">Status
                                    </th>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3012" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanCode")%>' />
                                    </td>
                                <td>
                                    <asp:Label ID="lblDLChapterShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanName")%>' />
                                </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Name")%>' />
                                    </td>

                                    <td style="text-align: center;">
                                        <asp:Label ID="lblEOC" runat="server" Text='<%# Convert.ToBoolean( Eval("EOC")) == true ? "Yes":"No"  %>' />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />
                                    </td>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivAdd" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label runat="server" ID="lblHeader" Text="Create New Lesson Plan"></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label7">Division</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblDivision_Add" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label4">Course</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCourse_Add" class="blue"></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label5">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblSubject_Add" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left"></td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label8" CssClass="red">Lesson Plan Name</asp:Label>
                                                                <span class="help-button ace-popover"
                                                                    data-trigger="hover" data-placement="right" data-content="Lesson Detail(s)"
                                                                    title="Lesson Detail(s)">?</span>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtLessonPlanName_Add" MaxLength="100" Width="245px" onkeypress="return NumberandCharOnly(event);"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label9">Display Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtDisplayName_Add" MaxLength="100" Width="245px"></asp:TextBox>
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
                                                                <asp:Label runat="server" ID="Label11">Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtDescription_Add" MaxLength="500" Width="245px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Chapter</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlChapter_Add" ToolTip="Chapter" data-placeholder="Select Chapter"
                                                                    Width="255px" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlChapter_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="tdtpoic" class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lbltopic_add">Topic</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:ListBox runat="server" ID="lstboxtopic_add" data-placeholder="Select Topic(s)" Width="255px"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstboxtopic_add_SelectedIndexChanged"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td runat="server" id="tdsubtopic" class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblsubtopic_add">Sub Topic</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:ListBox runat="server" ID="lstboxsubtopic_add" data-placeholder="Select Sub Topic(s)" Width="255px"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstboxsubtopic_add_SelectedIndexChanged"></asp:ListBox>
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
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Module</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:ListBox runat="server" ID="ddlModuleAdd" data-placeholder="Select Module(s)" Width="255px"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlModuleAdd_SelectedIndexChanged"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;"></td>
                                                            <td class="span4" style="border-style: none; text-align: left;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label13">Is Last Lecture</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <label>
                                                                    <input runat="server" id="chkIsLastLecture" name="switch-field-1" type="checkbox"
                                                                        class="ace-switch ace-switch-2" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label29">Status</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <label>
                                                                    <input runat="server" id="chkIsActiveHeader" name="switch-field-1" type="checkbox"
                                                                        class="ace-switch ace-switch-2" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class=" " style="text-align: right;">
                                            <!--Button Area -->
                                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnEdit" runat="server"
                                                Text="Edit Lesson Plan" Width="120px" OnClick="btnEdit_Click" Visible="false" />
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12" runat="server" visible="false">
                                                <div class="widget-box">
                                                    <div class="widget-header">
                                                        <h5 class="smaller">Assign Product Content
                                                        </h5>
                                                        <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnAssignProduct"
                                                            Text="Add" OnClick="btnAssignProduct_Click" />
                                                    </div>
                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <asp:DataList ID="dlAssign_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                Width="100%" OnItemCommand="dlAssign_Add_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <b>Module</b> </th>
                                                            <th style="width: 10%; text-align: left">Product Content Name
                                                            </th>
                                                                    <th style="width: 10%; text-align: left">Display Name
                                                                    </th>
                                                                    <th style="width: 10%; text-align: left">Content Location
                                                                    </th>
                                                                    <th style="width: 10%; text-align: left">Content Type
                                                                    </th>
                                                                    <th style="width: 10%; text-align: left">Test
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">No.of Question
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">Duration (in Min)
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">Status
                                                                    </th>
                                                                    <th style="width: 10%; text-align: center">
                                                                    Action
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModule" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Name")%>' />
                                                                    </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label runat="server" ID="lblProductContentName" Text='<%#DataBinder.Eval(Container.DataItem,"ProductContentName")%>'></asp:Label>
                                                            </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblProductContentDisplay" Text='<%#DataBinder.Eval(Container.DataItem,"ProductContentDisplayName")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblLocation" Text='<%#DataBinder.Eval(Container.DataItem,"LocationName")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblContentType" Text='<%#DataBinder.Eval(Container.DataItem,"ContentName")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblTest" Text='<%#DataBinder.Eval(Container.DataItem,"TestName")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Label runat="server" ID="lblNoofQuestion"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Label runat="server" ID="lblDuration"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Label CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>'
                                                                            runat="server" ID="Label31"> 
                                                                   <%#(int)DataBinder.Eval(Container.DataItem, "IsActive") == 1 ? "Active" : "Inactive"%>                         
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Pkey")%>' runat="server"
                                                                            CommandName="Edit" Height="25px" />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                <!--Button Area -->
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
            </div>
        </div>
        <!--/row-->
        <div class="modal fade" id="DivAssignProduct" style="left: 40% !important; top: 5% !important; display: none; width: 900px; height: 500px"
            role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">Assign Product Content
                        </h4>
                        <asp:Label runat="server" ID="lblProductPkey" Visible="false"></asp:Label>
                    </div>
                    <div class="alert alert-error" id="divErrorProduct" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblErrorProduct" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                    <div class="modal-body" style="overflow-y: visible !important;">
                        <!--Controls Area -->
                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left" colspan="2">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 15%;">
                                                <asp:Label runat="server" ID="Label18" CssClass="red">Module</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:DropDownList ID="ddlModule" runat="server" ToolTip="Module" data-placeholder="Select Module"
                                                    CssClass="chzn-select" Width="215px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label20" CssClass="red">Product Content Name</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:TextBox ID="txtProductContentName" runat="server" Text="" Width="205px" MaxLength="100" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label22">Content Display Name</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:TextBox ID="txtProductContentDisplayName" runat="server" Text="" Width="205px"
                                                    MaxLength="100" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label27">Content Description</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:TextBox ID="txtDescription" runat="server" Text="" Width="205px" MaxLength="500" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label25" CssClass="red">Version ID</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:TextBox runat="server" ID="txtVersionID" Width="205px" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label24" CssClass="red">Content Location</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:DropDownList ID="ddlLocation" runat="server" ToolTip="Location" data-placeholder="Select Content Location"
                                                    CssClass="chzn-select" Width="215px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label26" CssClass="red">Content Type</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:DropDownList ID="ddlContentType" runat="server" ToolTip="Content Type" data-placeholder="Select Content Type"
                                                    CssClass="chzn-select" Width="215px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label28">Test</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:DropDownList ID="ddlTest" runat="server" Width="205px" ToolTip="Test" data-placeholder="Select Test"
                                                    CssClass="chzn-select" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label19">No.of Question</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:TextBox ID="txtNoQuestion" runat="server" Text="" Width="205px" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label21">Duration (in Min)</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:TextBox ID="txtDuration" runat="server" Text="" Width="205px" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label23">Status</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <label>
                                                    <input runat="server" id="chkIsActivePContant" name="switch-field-1" type="checkbox"
                                                        class="ace-switch ace-switch-2" />
                                                    <span class="lbl"></span>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <!--Button Area -->
                        <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnSave" ToolTip="Save"
                            runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                            ID="btnCancel" ToolTip="Cancel" runat="server" Text="Cancel" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
    <!--/#page-content-->
</asp:Content>
