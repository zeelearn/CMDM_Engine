<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeFile="Config_Course.aspx.cs" Inherits="Config_Course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModalDivCOnfirmationCopyCourse() {
            $('#DivCOnfirmationCopyCourse').modal({
                backdrop: 'static'
            })

            $('#DivCOnfirmationCopyCourse').modal('show');
        }
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 43 || AsciiValue == 95 || AsciiValue == 45)
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Course<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnuploadviexcel"
                Width="150px" Text="Upload Via Excel" OnClick="btnuploadviexcel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true" runat="server"
                ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                        <h5>
                            Search Options
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
                                                                <asp:Label ID="Label13" runat="server" CssClass="red">Division Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivisionName" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 118%;" class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6">Course Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtCourseName" Text="" Width="205px"></asp:TextBox>
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                    <asp:Label runat="server" ID="lblCourse_Code" Visible="false" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" OnSelectedIndexChanged="dlGridDisplay_SelectedIndexChanged">
                    <HeaderTemplate>
                        <b>Course Code</b> </th>
                        <th style="width: 5%; text-align: center;">
                            Course No
                        </th>
                        <th style="width: 5%; text-align: left;">
                            Course Name
                        </th>
                        <th style="width: 5%; text-align: left;">
                            Display Name
                        </th>
                        <th align="left">
                            Short Name
                        </th>
                        <th align="left">
                            Description
                        </th>
                        <th align="left">
                            Course Category
                        </th>
                        <th align="left">
                            Board
                        </th>
                        <th align="left">
                            Medium
                        </th>
                        <th align="left">
                            Division
                        </th>
                        <th align="left">
                            Status
                        </th>
                        <th align="left">
                            Online Status
                        </th>
                        <th style="width: 50px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCourseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Code")%>' />
                        </td>
                        <td style="width: 5%; text-align: center;">
                            <asp:Label ID="lblCourseNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseSequenceNo")%>' />
                        </td>
                        <td style="width: 15%; text-align: left;">
                            <asp:Label ID="lblCourseName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                        <td style="width: 15%; text-align: left;">
                            <asp:Label ID="lblCourseDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Display_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Short_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Description")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseCatName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblBoardName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Board")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblMediumName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MediumName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseShortNameDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Division_ShortDesc")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'
                                CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label5" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("Is_Online")) == 1 ? "Online":"Offline"  %>'
                                CssClass='<%# Convert.ToInt32( Eval("Is_Online")) == 1 ? "label label-success":"label label-warning"  %>' />
                        </td>
                        <td style="width: 11%; text-align: center;">
                            <div class="inline position-relative">
                                <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Course_Code")%>' runat="server"
                                    CommandName="comEdit" Height="25px" />
                                <asp:LinkButton ID="lnkCopy" ToolTip="Copy" class="btn-small btn-primary icon-copy" Visible="false"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Course_Code")%>' runat="server"
                                    CommandName="comCopy" Height="25px" />
                                    <%-- streamcopy visible is commited  for PRD on 13062017:
                                      Visible='<%#Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Is_Active")) == 0 ? false : true%>'--%>
                                    <asp:LinkButton ID="LinkSub" ToolTip="Add Sub Icon" class="btn-small btn-primary icon-book"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Course_Code")%>' runat="server"
                                    CommandName="AddSubIcon" Height="25px" />
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Course Code</b> </th>
                        <th style="width: 5%; text-align: center;">
                            Course No
                        </th>
                        <th style="width: 15%; text-align: left;">
                            Course Name
                        </th>
                        <th style="width: 15%; text-align: left;">
                            Display Name
                        </th>
                        <th align="left">
                            Short Name
                        </th>
                        <th align="left">
                            Description
                        </th>
                        <th align="left">
                            Course Category
                        </th>
                        <th align="left">
                            Board
                        </th>
                        <th align="left">
                            Medium
                        </th>
                        <th align="left">
                            Division
                        </th>
                        <th align="left">
                            Status
                        </th>
                        <th align="left">
                            Online Status
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCourseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Code")%>' />
                        </td>
                        <td style="width: 5%; text-align: center;">
                            <asp:Label ID="lblCourseNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseSequenceNo")%>' />
                        </td>
                        <td style="width: 15%; text-align: left;">
                            <asp:Label ID="lblCourseName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                        </td>
                        <td style="width: 15%; text-align: left;">
                            <asp:Label ID="lblCourseDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Display_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Short_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Description")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseCatName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblBoardName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Board")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblMediumName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MediumName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCourseShortNameDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Division_ShortDesc")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label8" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("Is_Online")) == 1 ? "Online":"Offline"  %>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Add Course Details
                            <asp:Label runat="server" ID="lblPkey" Visible="false"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label7" runat="server" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivisionAdd" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label22" runat="server" CssClass="red">Category </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCCName" Width="215px" data-placeholder="Select Course Category"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label11" runat="server" CssClass="red">Board </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBoardName" Width="215px" data-placeholder="Select Board Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label ID="Label12" runat="server" CssClass="red">Medium </asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlMediumName" Width="215px" data-placeholder="Select Medium Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label14" runat="server" CssClass="red">Course Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtAddCourseName" runat="server" Text="" Width="205px" onkeypress="return NumberandCharOnly(event);"
                                                                    MaxLength="150"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label1" runat="server">Display Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtAddCourseDisplayName" runat="server" Text="" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label2" runat="server">Short Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtAddCourseShortName" runat="server" Text="" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label3" runat="server">Description</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtAddCourseDescription" runat="server" Text="" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label15" runat="server">Sequence No</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtCourseSequenceNo" runat="server" Text="" Width="205px" onkeypress="return NumberOnly()"></asp:TextBox>
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
                                                                <asp:Label ID="Label4" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                        checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="lblisonline" runat="server">Is Online</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="addchkisonline" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                        checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
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
                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Edit Course Details
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label9" runat="server" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivisionEdit" Width="142px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label16" runat="server" CssClass="red">Category </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCCEdit" Width="142px" data-placeholder="Select Course Category"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label17" runat="server" CssClass="red">Board </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBoardEdit" Width="142px" data-placeholder="Select Board Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label ID="Label18" runat="server" CssClass="red">Medium </asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlMediumEdit" Width="142px" data-placeholder="Select Medium Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label24" runat="server" CssClass="red">Course Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtEditCourseName" runat="server" Text="" Width="80%" onkeypress="return NumberandCharOnly(event);"
                                                                    MaxLength="150"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label25" runat="server">Display Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtEditCourseDisplayName" runat="server" Text="" Width="80%"></asp:TextBox>
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
                                                                <asp:Label ID="Label26" runat="server">Short Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtEditCourseShortName" runat="server" Text="" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label27" runat="server">Description</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtEditCourseDesc" runat="server" Text="" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label28" runat="server">Sequence No</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtEditCourseSequenceNo" runat="server" Text="" Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
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
                                                                <asp:Label ID="Label29" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="chkEditActiveFlag" name="switch-field-1" type="checkbox"
                                                                        class="ace-switch ace-switch-2" checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="lbleditisonline" runat="server">Is Online</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="editchkisonline" name="switch-field-1" type="checkbox"
                                                                        class="ace-switch ace-switch-2" checked="checked" />
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
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
                            <asp:Label runat="server" ID="Label55" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnEditSave" runat="server"
                                Text="Save" ValidationGroup="UcValidate" OnClick="btnEditSave_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnEditClose" Visible="true"
                                runat="server" Text="Close" OnClick="btnEditClose_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivCOnfirmationCopyCourse" style="left: 50% !important;
        top: 20% !important; display: none;" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
        
         <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Alert!!!
                    </h4>
                </div>
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
             <div class="alert alert-error" id="divpopuperror" visible="false" runat="server">
                <button type="button" class="close" data-dismiss="alert">
                    <i class="icon-remove"></i>
                </button>
                <p>
                    <strong><i class="icon-remove"></i>Error!</strong>
                    <asp:Label ID="lblpoperror" runat="server" Text="Label"></asp:Label>
                </p>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
           <div class="modal-body" style=" position: relative; max-height: 400px;  padding: 9px;overflow-y: auto; padding-left: 27px;">
                    <h5>
                        <asp:Label runat="server" ID="Label19"> You about the copy of this Course ?</asp:Label>
                    </h5>
                </div>
               <div class="modal-body" style=" position: relative; max-height: 400px;  padding: 9px;overflow-y: auto; padding-left: 27px;"> 
                    <!--Controls Area -->
                    <table cellpadding="2" style="border-style: none;">
                        <tr>
                            <td class="span8" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label ID="Label10" runat="server" CssClass="red">New Course Name</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:TextBox ID="txtCourse" runat="server" Text="" Width="205px" onkeypress="return NumberandCharOnly(event);"
                                                MaxLength="150"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left; border-style: none;">
                           
                            </td>
                        </tr>
                        <tr>
                            <td class="span8" style="text-align: left">
                              
                            </td>
                            <td class="span4" style="text-align: left; border-style: none;">
                           
                            </td>
                        </tr>
                         <tr>
                            <td class="span8" style="text-align: left">
                              
                            </td>
                            <td class="span4" style="text-align: left; border-style: none;">
                           
                            </td>
                        </tr>
                        <tr>
                            <td class="span8" style="text-align: left">
                              <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label ID="Label20" runat="server">Course Display Name</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                         <%--   <asp:TextBox ID="txtCourseDisplay" runat="server" Text="" Width="205px" onkeypress="return NumberandCharOnly(event);"
                                                MaxLength="150"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtCourseDisplay" Width="205px"  runat="server" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left; border-style: none;">
                            </td>
                        </tr>
                    </table>
                    </br>
              </div>
                <div>
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="Label53" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="Yes" ToolTip="Yes"
                        runat="server" Text="Yes" OnClick="Yes_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="Button6" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
