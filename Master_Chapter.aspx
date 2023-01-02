<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Master_Chapter.aspx.cs" Inherits="Master_Chapter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 45 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95 || AsciiValue == 45 || (AsciiValue >= 48 && AsciiValue <= 57))
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
                    Chapter<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click"/>
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
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" ToolTip="Course" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;"  >
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
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
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
                                <td class="span10">
                                    Total No of Records:                                    
                                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                      
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
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
                
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" OnSelectedIndexChanged="dlGridDisplay_SelectedIndexChanged">
                            <HeaderTemplate>
                            <b>Chapter Code</b>
                            </th>
                            <th>
                                Chapter Short Name </th>
                                <th align="left" style="width: 25%">
                                    Chapter Name
                                </th>
                                <th align="left" style="width: 22%">
                                    Chapter Display Name
                                </th>
                                <th style="width: 6%; text-align: center;">
                                    No. of Lectures
                                </th>
                                <th style="width: 6%; text-align: center;">
                                    Time in Min.
                                </th>
                                 <th style="width: 5%; text-align: center; vertical-align: middle;">
                                    Sequence No
                                </th>
                                <th style="width: 5%; text-align: left; vertical-align: left;">
                                    Status                                
                                </th>
                                <th style="width: 80px; text-align: center;">
                                Action
                                </th>
                                <th style="width: 30px; text-align: center; vertical-align: middle;">
                                
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label runat="server" ID="Label6" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDLChapterShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_ShortName")%>'
                                    Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="75%" MaxLength="100" onkeypress="return NumberandCharOnly(event);"/>
                                <asp:Label ID="lblDLChapterShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_ShortName")%>'
                                    Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLChapterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowChapterNameEditFlag") == 1 ? true : false%>' onkeypress="return NumberandCharOnly(event);" />
                                    <asp:Label ID="lblDLChapterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    <asp:Label runat="server" ID="lblDLChapterCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLChapterDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_DisplayName")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowChapterNameEditFlag") == 1 ? true : false%>'  />
                                    <asp:Label ID="lblDLChapterDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_DisplayName")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />                                    
                                </td>
                                <td style="width: 6%; text-align: center;">
                                    <asp:TextBox ID="txtDLLectureCnt" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lecture_Count")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" onkeypress="return NumberOnly(event);"/>
                                    <asp:Label ID="lblDLLectureCnt" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lecture_Count")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 6%; text-align: center;">
                                    <asp:TextBox ID="txtDLLectureMin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lecture_Duration")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" onkeypress="return NumberOnly(event);"/>
                                    <asp:Label ID="lblDLLectureMin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lecture_Duration")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 5%; text-align: center;">
                                    <asp:TextBox ID="txtDLChapter_SequenceNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ChapterSequenceNo")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" onkeypress="return NumberOnly(event);"/>
                                    <asp:Label ID="lblDLCHapter_SequenceNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ChapterSequenceNo")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 5%; text-align: left;">                                   
                                    <label>
                                        <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                        checked='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? true:false  %>' Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'/>
                                        <span class="lbl"></span>
                                    </label>                                    
                                    <asp:Label ID="lblDLStatus" runat="server" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>' />                                
                                </td>
                                <td style="text-align: center;">
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>' runat="server"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CommandName="Edit"
                                        Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                        </td>
                                        <td style="width: 30px; text-align: center; vertical-align: middle;">
                                    <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                    <asp:Panel id ="icon_Error" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel>
                                    </a>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                   
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" 
                    HorizontalAlign="Left"  CssClass="table table-striped table-bordered table-hover"
                     Width="100%">
                    <HeaderTemplate>
                    <b>Chapter Code</b>
                    <th>
                        Chapter Short name</th>
                        <th  style="width: 20%;text-align:left">
                            Chapter Name
                        </th>
                        <th  style="width: 20%;text-align:left">
                            Chapter Display Name
                        </th>
                        <th align="center" style="width: 20%;text-align:center">
                            No. of Lectures
                        </th>
                        <th align="center" style="width: 20%;text-align:center" >
                            Time in Min.
                            </th>
                                <th style="width: 5%; text-align: left; vertical-align: left;">
                                    Status  
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:Label runat="server" ID="Label654" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>'></asp:Label>
                    </td>
                           <td>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_ShortName")%>' />
                        </td>
                        <td style="width: 20%;text-align:left" >
                             <asp:Label ID="lblCentre1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                        </td>
                        <td style="width: 20%;text-align:left" >
                             <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_DisplayName")%>' />
                        </td>
                        <td style="width: 20%;text-align:center" >
                            <asp:Label ID="lblStandard1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lecture_Count")%>' />
                        </td>
                        <td style="width: 20%;text-align:center" >
                            <asp:Label ID="lblBatch1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lecture_Duration")%>' />
                            </td>
                            <td>
                            <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />
                        
                    </ItemTemplate>
                </asp:DataList>
                 
            </div>
        </div>
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>

