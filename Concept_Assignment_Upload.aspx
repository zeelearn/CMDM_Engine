<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Concept_Assignment_Upload.aspx.cs" Inherits="Concept_Assignment_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function downloadselection_show() {
            $('#downloadselection').modal({
                backdrop: 'static'
            })

            $('#downloadselection').modal('show');

        };

        function downloadselection_hide() {
            $('#downloadselection').modal({
                backdrop: 'static'
            })

            $('#downloadselection').modal('hide');

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
            <li>
                <h4 class="blue">Concepts Assignment Upload<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtndwnldTemplate"
                Width="150px" Text="Download Template" onserverclick="btndownload_ServerClick" OnClick="BtndwnldTemplate_Click1" />

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



            <div id="DivNew_Upload1" visible="true" runat="server">
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
                                                                    <asp:Label ID="lbluploadcourse" runat="server" CssClass="red">Course</asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                    <asp:DropDownList runat="server" ID="ddluploadcourse" Width="215px" data-placeholder="Select Course"
                                                                        CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddluploadcourse_SelectedIndexChanged" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="span4" style="text-align: left">
                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                    <asp:Label ID="lbluploadsubject" runat="server" CssClass="red">Subject </asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                    <asp:DropDownList runat="server" ID="ddluploadsubject" Width="215px" data-placeholder="Select Subject"
                                                                        CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddluploadsubject_SelectedIndexChanged" />
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
                                            <%--</td>
                                        <td class="span4" style="text-align: left">
                                        </td>
                                        <td class="span4" style="text-align: left">
                                        </td>
                                    </tr>
                                </table>--%>
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
                            <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                <tr>
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
                                    <td class="span4" style="text-align: left"></td>
                                </tr>
                            </table>

                            <asp:DataList ID="datalist_NewUploads1" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" Visible="True">
                                <HeaderTemplate>
                                    <b>Chapter_Name  </b></th>
                        <th>Chapter_Code
                        </th>
                                    <th>Concept_Name
                                    <th align="left">Status
                                    </th>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:Label ID="lblchaptername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                    </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblchaptercode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>' />
                        </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblconceptname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Concept_Name")%>' />
                                    </td>
                                    <td style="text-align: left;">
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
                        <asp:PostBackTrigger ControlID="btnsaveexcel" />
                        <asp:PostBackTrigger ControlID="btnClose" />
                        <asp:PostBackTrigger ControlID="Btnimport" />
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>








<%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>

                <div class="modal fade" id="downloadselection" style="left: 47% !important; top: 20% !important; width: auto; height: auto; display: none;"
                    role="dialog" aria-labelledby="myModalLabel" aria-hidden="false">
                    <div class="modal-dialog modal-small ">

                        <div class="alert alert-danger" id="divcertiErr" visible="false" runat="server">
                            <button class="close" data-close="alert">
                            </button>
                            <strong>
                                <asp:Label ID="lbldownloadErrormsg" runat="server" Visible="false"></asp:Label></strong>
                        </div>
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title blue">Download Critera</h4>
                            </div>
                            <div class="modal-body">
                                <div style="height: 200px" data-always-visible="1" data-rail-visible1="1">
                                    <div class="row-fluid">
                                        <div class="span12">
                                            <div class="table-responsive">
                                                <table class="table table-striped table-bordered table-advance table-hover" width="100%">
                                                    <tr>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbldowloadcourse" runat="server" CssClass="red">Course</asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddldownloadcourse" Width="215px" data-placeholder="Select Course"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddldownloadcourse_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbldownloadsubject" runat="server" CssClass="red">Subject </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddldownloadsubject" Width="215px" data-placeholder="Select Subject"
                                                                            CssClass="chzn-select" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                </table>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="well" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" runat="server" ID="Btndownload"
                                    Text="Ok" ToolTip="Ok" ValidationGroup="Grplead2" OnClick="Btndownload_Click" />
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" ID="btncancel"
                                    Visible="true" runat="server" Text="Cancel" />
                                <asp:ValidationSummary ID="ValidationSummary9" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="Val12" runat="server" />
                            </div>
                        </div>
                        <!-- /.modal-content -->

                    </div>
                </div>
            <%--</ContentTemplate>
            <Triggers>

                <%-- <asp:PostBackTrigger ControlID="btncancel" />
                <asp:PostBackTrigger ControlID="Btndownload" />
            </Triggers>
        </asp:UpdatePanel>--%>
</asp:Content>

