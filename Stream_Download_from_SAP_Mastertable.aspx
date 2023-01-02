<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Stream_Download_from_SAP_Mastertable.aspx.cs" Inherits="Stream_Download_from_SAP_Mastertable" %>

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
        function openModalDivOverride() {
            $('#divforexport').modal({
                backdrop: 'static'
            })

            $('#divforexport').modal('show');


        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95)
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }
      

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Stream Master Download From SAP<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button Width="100" class="btn  btn-app btn-success btn-mini radius-4  " runat="server"
                ID="BtnDownload" Text="Download" OnClick="BtnDownload_Click" />
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
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Divsion </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19" CssClass="red"> Stream Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Code" Width="215px" ID="txtStreamCode" runat="server"
                                                                    MaxLength="1000" />
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
                                                                <asp:Label runat="server" ID="Label2">Stream Name </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Name" Width="215px" ID="txtStreamName" runat="server"
                                                                    MaxLength="1000" />
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
                                <%--<td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlClassRoomProduct" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Stream Code</b> </th>
                        <th style="text-align: left">
                            Stream Name
                        </th>
                        <th style="text-align: left">
                            Course Period
                        </th>
                        <th style="text-align: left">
                            Center Name
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStreamCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Code")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Name")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Period")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CenterName")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivDowmloadPanel" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">
                                    <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                                    <asp:Label ID="lblPKey" runat="server" Visible="false"></asp:Label>
                                </h5>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main">
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr runat="server" id="row1">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label1" CssClass="red">Divison </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlDivisionAdd" Width="240px" data-placeholder="Select Division"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionAdd_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label4" CssClass="red">Divison </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="DropDownList1" Width="240px" data-placeholder="Select Division"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionAdd_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label15" CssClass="red">Center(s) </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:ListBox runat="server" ID="ddlCenter" data-placeholder="Select Center(s)" Width="240px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label26" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button1" runat="server"
                            Text="Download" ValidationGroup="UcValidate" OnClick="BtnSave_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button3" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                        </div>
                    </ContentTemplate>
                    
                   <%-- <Triggers>
                        <asp:PostBackTrigger ControlID="Button1" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal fade" id="divforexport" style="left: 50% !important; top: 10% !important;
                    display: none; width: 40%" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Select Master
                                    <asp:Label ID="Lblpk" runat="server" Visible="false" />
                                </h4>
                                <asp:CheckBox ID="chkAllHidden" runat="server" Visible="False" />
                            </div>
                            <div class="modal-body">
                                <!--Controls Area -->
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 100%;" colspan="3">
                                            <asp:DataList ID="dladditem" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                Width="100%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" />
                                                    <span class="lbl"></span></th>
                                                    <th>
                                                        <b>Master File Name</b>
                                                    </th>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="True" />
                                                    <span class="lbl"></span></td>
                                                    <td>
                                                        <asp:Label ID="lblMastertable1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"name")%>' />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <!--Button Area -->
                                <asp:Label runat="server" ID="Label44" Text="" Visible="false" />
                                <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnadditem" ToolTip="OK"
                                    runat="server" Text="OK" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="Button5" ToolTip="Cancel" runat="server" Text="Cancel" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
            display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">
                            Delete Stream Details
                        </h4>
                    </div>
                    <div class="modal-body">
                        <!--Controls Area -->
                        <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="txtDeleteItemName"
                            Text="You are about to delete the stream. 
                                Note: This is a irreversible process.Are you sure you want to continue..." />
                        <center />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!--/row-->
    </div>
</asp:Content>
