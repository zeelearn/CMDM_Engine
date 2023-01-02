<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Faculty_lecture_count_SSC.aspx.cs" Inherits="Faculty_lecture_count_SSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">


    function openModalDelete() {
        $('#DivDelete').modal({
            backdrop: 'static'
        })

        $('#DivDelete').modal('show');
    }

    function NumberandCharOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 45 || (AsciiValue >= 48 && AsciiValue <= 57) || AsciiValue == 95 || AsciiValue == 32)
            event.returnValue = true;
        else
            event.returnValue = false;
    } 

    </script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Faculty lecture Count Monthly <span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="btnTopSearch" Text="Search" OnClick="btnTopSearch_Click" />
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
                                                                <asp:Label runat="server" ID="Label2" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlLMSProduct" data-placeholder="Select LMS Product"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlLMSProduct_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29">Period</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker" id="Daterange"
                                                                        placeholder="Date Search" data-placement="bottom" data-original-title="Date Range"/>
                                                                                                                     
                                                            </td>
                                                        </tr>
                                                    </table>
                                                   
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                 <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label25" >Faculty Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlfaculty" data-placeholder="Select" CssClass="chzn-select"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                               <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Center Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenter"  data-placeholder="Select Center(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                                                                   
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                 
                                                </td>
                                            </tr>

                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
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
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                <tr>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label6">Division</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblDivision" Text="" CssClass="blue" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label7">Academic Year</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblAcademicYear" Text="" CssClass="blue" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label8">Course</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblCourse" Text="" CssClass="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label23">LMS Product</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblLMSProduct" Width="250px" CssClass="blue" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label11" CssClass="red">Faculty Name</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblfaculty" Width="250px" CssClass="blue" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                    </td>
                                </tr>
                            </table>
                            <asp:DataList ID="dlfaculty" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b>Faculty Name</b> </th>
                                    <th style="text-align: center">
                                        Subject Name
                                    </th>
                                    <th style="text-align: center">
                                        Center Name
                                    </th>
                                    <th style="text-align: center">
                                        Course Name
                                    </th>
                                    <th style="text-align: center">
                                        Horizon Name
                                    </th>
                                    <th style="text-align: center">
                                        Lecture Type Name
                                    </th>
                                
                                    <th style="text-align: center">
                                    Toltal Lecture Count
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lbsubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name" )%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblcentername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"centername")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblcourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblproduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblchap" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblsched" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureCount")%>' />
                                    </td>
                                
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" Visible="false">
                                <HeaderTemplate>
                                    <b>Faculty Name</b> </th>
                                    <th style="text-align: center">
                                        Subject Name
                                    </th>
                                    <th style="text-align: center">
                                        Center Name
                                    </th>
                                    <th style="text-align: center">
                                        Course Name
                                    </th>
                                    <th style="text-align: center">
                                        Horizon Name
                                    </th>
                                    <th style="text-align: center">
                                        Lecture Type Name
                                    </th>
                                
                                    <th style="text-align: center">
                                    Toltal Lecture Count
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lbsubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name" )%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblcentername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"centername")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblcourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblproduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblchap" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_Name")%>' />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblsched" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureCount")%>' />
                                    </td>
                                
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

