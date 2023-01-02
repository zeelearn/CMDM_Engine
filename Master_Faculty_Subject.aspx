﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeFile="Master_Faculty_Subject.aspx.cs" Inherits="Master_Faculty_Subject" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Color_Changed(sender) {

            sender.get_element().value = "#" + sender.get_selectedColor();

        }


        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 45 || (AsciiValue >= 48 && AsciiValue <= 57))
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
                    Teacher Subject Assignment <span class="divider"></span>
                </h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" Visible="false" runat="server"
                ID="btndwntemplete" Width="150px" Text="Download Template" OnClick="btndwntemplete_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4"  runat="server" ID="btnAdd"
                Text="ADD" OnClick="btnAdd_Click" />
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
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                                    CssClass="chzn-select" />
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
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
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
                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label12">Division</asp:Label>
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
                                        <asp:Label runat="server" ID="Label14">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAced_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label13">Course</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlFaculty" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlFaculty_ItemCommand">
                    <HeaderTemplate>
                        <b>Teacher Name</b> </th>
                        <th style="text-align: left">
                            Facluty Code
                        </th>
                        <th style="text-align: left">
                            Teacher Color Code
                        </th>
                        <th style="text-align: center">
                            Subject
                        </th>
                        <th style="text-align: center">
                            Short Name
                        </th>
                        <th style="text-align: center">
                            Rate
                        </th>
                        <th style="text-align: center">
                            Action
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFaculty" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                         <td style="text-align: center">
                            <asp:Label ID="Lblfacultycode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                        </td>
                        <td style="text-align: center" align="center">
                            <div style="height: 20px; width: 20px; background-color: <%#DataBinder.Eval(Container.DataItem,"ColorCode")%>">
                            </div>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblPaymentRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentRate")%>' />
                        </td>
                        <td style="text-align: center">
                            <div class="inline position-relative">
                                <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                    <i class="icon-cog icon-only"></i>
                                </button>
                                <ul class="dropdown-menu dropdown-icon-only dropdown-light pull-right dropdown-caret dropdown-close">
                                    <li>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="comEdit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                            class="tooltip-success" data-rel="tooltip" title="Edit" data-placement="left"><span
                                                    class="green"><i class="icon-edit"></i></span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkDelete" runat="server" class="tooltip-error" data-rel="tooltip"
                                            CommandName="comDelete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                            ToolTip="Delete" data-placement="left"><span class="red"><i class="icon-trash"></i></span></asp:LinkButton></li>
                                </ul>
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Teacher Name</b> </th>
                        <th style="text-align: left">
                            Facluty Code
                        </th>
                        <th style="text-align: left">
                            Teacher Color Code
                        </th>
                        <th style="text-align: center">
                            Subject
                        </th>
                        <th style="text-align: center">
                            Short Name
                        </th>
                        <th style="text-align: center">
                            Rate
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFaculty" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Lblfacultycode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                        </td>
                        <td style="text-align: center" align="center">
                            <div style="height: 20px; width: 20px; background-color: <%#DataBinder.Eval(Container.DataItem,"ColorCode")%>">
                            </div>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblPaymentRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentRate")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div runat="server" id="DivAddFacultySubject" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label runat="server" ID="lblHeaderFacultySubject" Text="Add Teacher Subject Assignment"></asp:Label>
                            <asp:Label runat="server" ID="lblPkey" Text="" Visible="false"></asp:Label>
                        </h5>
                    </div>
                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label4" CssClass="red">Division</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:DropDownList runat="server" ID="ddlDivisionAdd" Width="215px" data-placeholder="Select Division"
                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionAdd_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label5" CssClass="red">Academic Year</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:DropDownList runat="server" ID="ddlAcademicYearAdd" Width="215px" data-placeholder="Select Academic Year"
                                                CssClass="chzn-select" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label6" CssClass="red">Course</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:DropDownList runat="server" ID="ddlCourseAdd" Width="215px" data-placeholder="Select Course"
                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseAdd_SelectedIndexChanged" />
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
                                            <asp:Label runat="server" ID="Label8" CssClass="red">Subject</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:DropDownList runat="server" ID="ddlSubject" Width="215px" data-placeholder="Select Subject"
                                                CssClass="chzn-select" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label7" CssClass="red">Teacher Name</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:DropDownList runat="server" ID="ddlFaculty" Width="215px" data-placeholder="Select Teacher"
                                                CssClass="chzn-select" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label9">Color</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:TextBox ID="txtColor" runat="server" Width="205px" />
                                            <br />
                                            <div id="preview" style="width: 30px; height: 30px; border: 1px solid #000; margin: 0 3px;
                                                float: left" runat="server">
                                            </div>
                                            <a href="#">
                                                <asp:Image ID="btnImg" runat="server" AlternateText="Choose Color" ImageUrl="~/Images/images.jpg"
                                                    Width="20px" Height="20px" /></a>
                                            <cc1:ColorPickerExtender ID="ColorPicker1" runat="server" TargetControlID="txtColor"
                                                SampleControlID="preview" PopupButtonID="btnImg" PopupPosition="Right" OnClientColorSelectionChanged="Color_Changed" />
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
                                            <asp:Label runat="server" ID="Label10">Payment Rate</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:TextBox runat="server" ID="txtPaymentRate" Width="205px" data-placeholder="Payment Rate" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label11" CssClass="red">Short Name</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:TextBox runat="server" ID="txtShortName" Width="201px" data-placeholder="Short Name"
                                                onkeypress="return NumberandCharOnly(event);" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none; width: 100%;" class="table-hover"
                                    runat="server" id="Table4">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label ID="lblselectfile" runat="server" CssClass="red">Select File To Upload</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 100%;">
                                            <asp:FileUpload ID="uploadfile" runat="server" size="30" Width="220" />
                                            <br />
                                            <asp:Label ID="lblfilepath" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblfilename" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:DataList ID="datalist_NewUploads1" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" Visible="True">
                                <HeaderTemplate>
                                    <b>Divisioncode</b></th>
                                    <th>Acadyear</th>
                                    <th>Course</th>
                                    <th>Subject Name</th>
                                    <th>Subject Code</th>
                                     <th>Teacher code</th>
                                     <th>Color</th>
                                     <th>Payment Rate</th>
                                     <th>Short Name</th>
                                    <th align="left">Status
                                    </th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"disvision")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblacad" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"acadyear")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblcourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"course")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblsubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Subject Name]")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblsubcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Subject Code]")%>' />
                                    </td>
                                     <td style="text-align: left;">
                                        <asp:Label ID="lblteachcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Teacher code]")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblcolor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Color]")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblpayment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Payment Rate]")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblshtname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Short Name]")%>' />
                                    </td>

                                    <td style="text-align: left;">
                                        <asp:Label ID="labelSTATUS" runat="server" Text=""></asp:Label>
                                </ItemTemplate>

                            </asp:DataList>
                    
                        <!--Button Area -->
                        <div runat="server" class="widget-main alert-block alert-info" id="Divbtnimport"
                                style="text-align: center;">
                        <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnsaveexcel" runat="server" visibe="false"
                                    Text="Save" ValidationGroup="UcValidate" OnClick="btnsaveexcel_Click" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnupload" runat="server"
                            Text="Upload" ValidationGroup="UcValidate" OnClick="btnUpload_Click" />
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
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Delete Teacher Subject Assignment
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to delete this Teacher Subject Assignment. Do you want to Continue
                    ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" OnClick="btnDelete_Yes_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
