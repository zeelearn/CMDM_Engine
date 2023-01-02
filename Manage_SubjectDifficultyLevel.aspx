<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Manage_SubjectDifficultyLevel.aspx.cs" Inherits="Manage_SubjectDifficultyLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 45 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95)
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
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Subject Difficulty Level<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
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
                                        Text="Export" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                        <HeaderTemplate>
                            <b>Board </b></th>
                            <th>
                                Course
                            </th>
                            <th>
                                Subject
                            </th>
                            <th>
                                Min Value
                            </th>
                            <th>
                                Max Value
                            </th>
                            <th align="left" style="width: 15%">
                                Status
                            </th>
                            <th style="text-align: center">
                                Action
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Board")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Min_Value")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Max_Value")%>' />
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
                    <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                        Visible="false" runat="server" Width="100%">
                        <HeaderTemplate>
                            <b>Board </b></th>
                            <th>
                                Course
                            </th>
                            <th>
                                Subject
                            </th>
                            <th>
                                Min Value
                            </th>
                            <th>
                                Max Value
                            </th>
                            <th align="left" style="width: 15%">
                                Status
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Board")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Min_Value")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Max_Value")%>' />
                            </td>
                            <td>
                                <asp:Label CssClass='<%# Convert.ToInt32( Eval("is_active")) == 1 ? "label label-success":"label label-warning"  %>'
                                    runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "is_active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
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
            </div>
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
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Board</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlboard" Width="215px" ToolTip="Board" data-placeholder="Select Board"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlboard_SelectedIndexChanged" />
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
                                                                    data-placeholder="Select Course" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="black">Subject</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSubject" Width="215px" ToolTip="Subject"
                                                                    data-placeholder="Select Subject" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" />
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
            <div id="DivAddPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                        </h5>
                        <h5 class="modal-title">
                            <asp:Label ID="lblprimarykey" Visible="false" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr runat="server" id="row1">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label2" CssClass="red">Board </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddladdboard" Width="215px" data-placeholder="Select Board"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddladdboard_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Course Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddladdCourseName" Width="215px" data-placeholder="Select Course"
                                                                CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddladdCourseName_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label7" runat="server" CssClass="red"> Subject Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList ID="ddladdsubjectname" runat="server" AutoPostBack="true" CssClass="chzn-select"
                                                                data-placeholder="Select Subject" Width="215px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="row2">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label11">Min Value</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Min Value" ID="txtaddminvalue" Width="205px" runat="server"
                                                                MaxLength="100" onkeypress="return NumberOnly(event);" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete=off  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label12">Max Value</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Max Value" ID="txtaddmaxvalue" Width="205px" runat="server"
                                                                MaxLength="100" onkeypress="return NumberOnly(event);" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete=off  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label16">Is Active</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkActive" checked="checked" name="switch-field-1" type="checkbox"
                                                                    class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="row3" visible="false">
                                            <td class="span4" style="text-align: left">
                                            </td>
                                            <td class="span4" style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="widget-main alert-block alert-info" style="text-align: center;">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                        Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveAdd_Click" />
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                        Visible="false" Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveEdit_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                        runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                </div>
            </div>
</asp:Content>
