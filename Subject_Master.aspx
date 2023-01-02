<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Subject_Master.aspx.cs" Inherits="Subject_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">Subject Master Upload<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtndwnldTemplate"
                Width="150px" Text="Download Template" OnClick="BtndwnldTemplate_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
        </div>
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

            <div id="DivNew_Upload_1" visible="true" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="DivNew_Upload" visible="false" runat="server">
                            <div class="widget-box">
                                <div class="widget-header widget-header-small header-color-dark">
                                    <h5>New Upload
                            <%-- <asp:Label ID="lblPkey" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserType" runat="server" Text="" Visible="false"></asp:Label>--%>
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
                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                    <asp:Label runat="server" ID="lbldivision" CssClass="red">Divison </asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                    <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                        CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="span4" style="text-align: left">
                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                    <asp:Label runat="server" ID="lblcourse" CssClass="red">Course Name</asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                    <asp:DropDownList runat="server" ID="ddlCourseName" Width="215px" data-placeholder="Select Course"
                                                                        CssClass="chzn-select" AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="span4" style="text-align: left">

                                                        <table cellpadding="0" style="border-style: none; width: 100%;" class="table-hover"
                                                            runat="server" id="Table4">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                    <asp:Label ID="lblselectfile" runat="server" CssClass="red">Select File</asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 100%;">
                                                                    <asp:FileUpload ID="uploadfile" runat="server" size="22" Width="220" />
                                                                    <br />
                                                                    <asp:Label ID="lblfilepath" runat="server" Visible="False"></asp:Label>
                                                                    <asp:Label ID="lblfilename" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>


                                            </table>
                                            <div runat="server" class="widget-main alert-block alert-info" id="Divuploadbtn"
                                                style="text-align: center;">
                                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnUpload"
                                                    Text="Upload" ToolTip="Upload" ValidationGroup="UcValidateSearch" OnClick="btnUpload_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="New_UploadGrid" runat="server" visible="false">

                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                <tr>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lbldivisionresult" class="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label3">Course Name</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblcourseresult" class="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left"></td>
                                </tr>


                            </table>



                            <asp:DataList ID="datalist_NewUploads1" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" Visible="True">
                                <HeaderTemplate>
                                    <b>Subject Display Name</b>


                                    <th runat="server" visible="false" style="text-align: center;">Subject
                                    </th>

                                    <th runat="server" visible="true" style="width: 14%; text-align: center;">Chapter Display Name
                                    </th>
                                    <th runat="server" visible="false" style="text-align: center;">Chapter
                                    </th>

                                    <th runat="server" visible="true" style="width: 14%; text-align: center;">Topic Display-Name
                                    </th>
                                    <th runat="server" visible="false" style="text-align: center;">Topic
                                    </th>

                                    <th runat="server" visible="true" style="width: 14%; text-align: center;">SubTopic Display Name
                                    </th>
                                    <th runat="server" visible="false" style="text-align: center;">SubTopic
                                    </th>

                                    <th runat="server" visible="true" style="width: 14%; text-align: center;">Module Display Name
                                    </th>
                                    <th runat="server" visible="false" style="text-align: center;">Module
                                    </th>

                                    <th runat="server" visible="true" style="width: 16%; text-align: center;">Lession Plan Name Display Name
                                    </th>
                                    <th runat="server" visible="false" style="text-align: center;">Lession Plan Name
                                    </th>
                                    <th style="width: 14%; text-align: center;">Status
                                    </th>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:Label ID="lblsubjectdisplayname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Display_Name")%>' />
                                    </td>
                                    <td runat="server" visible="false" style="text-align: Center;">
                                        <asp:Label ID="lblsubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                                    </td>

                                    <td style="text-align: Center;">
                                        <asp:Label ID="lblchapterdisplayname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Display_Name")%>' />
                                    </td>
                                    <td runat="server" visible="false" style="text-align: Center;">
                                        <asp:Label ID="lblchapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter")%>' />
                                    </td>

                                    <td style="text-align: Center;">
                                        <asp:Label ID="lbltopicdisplayname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Display_Name")%>' />
                                    </td>
                                    <td runat="server" visible="false" style="text-align: Center;">
                                        <asp:Label ID="lbltopic" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic")%>' />
                                    </td>

                                    <td style="text-align: center;">
                                        <asp:Label ID="lblsubtopicdisplayname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic_Display_Name")%>' />
                                    </td>
                                    <td runat="server" visible="false" style="text-align: center;">
                                        <asp:Label ID="lblsubtopic" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubTopic")%>' />
                                    </td>

                                    <td style="text-align: Center;">
                                        <asp:Label ID="lblmoduledisplayname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module_Display_Name")%>' />
                                    </td>
                                    <td runat="server" visible="false" style="text-align: Center;">
                                        <asp:Label ID="lblmodule" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module")%>' />
                                    </td>

                                    <td style="text-align: Center;">
                                        <asp:Label ID="lbllessionplannamedisplayname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlan_Display_Name")%>' />
                                    </td>
                                    <td runat="server" visible="false" style="text-align: Center;">
                                        <asp:Label ID="lbllessionplanname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lesson_Plan_Name")%>' />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="labelSTATUS" runat="server" Text=""></asp:Label>
                                    </td>
                                </ItemTemplate>
                            </asp:DataList>

                            <div runat="server" class="widget-main alert-block alert-info" id="Divbtnimport"
                                style="text-align: center;">
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnsaveexcel" runat="server" visibe="false"
                                    Text="Save" ValidationGroup="UcValidate" OnClick="btnsaveexcel_Click" />
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Btnimport" runat="server"
                                    Text="Import" ValidationGroup="UcValidate" OnClick="Btnimport_Click" />
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnClose"
                                    Text="Close" ToolTip="Close" ValidationGroup="UcValidateSearch" OnClick="btnClose_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>


                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />
                        <asp:PostBackTrigger ControlID="btnClose" />
                        <asp:PostBackTrigger ControlID="Btnimport" />
                        <asp:PostBackTrigger ControlID="btnsaveexcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
</asp:Content>

