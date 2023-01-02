<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Manage_Partner_Rates.aspx.cs" Inherits="Manage_Partner_Rates" %>

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

        function NumberOnly1() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 45 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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
                    Manage Partner Rates<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
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
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Acad Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" ToolTip="Acad Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Activity Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlActivityType" Width="215px" ToolTip="Activity Type"
                                                                    data-placeholder="Select Activity Type" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlActivityType_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">Partner</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlPartner" Width="215px" ToolTip="Partner"
                                                                    data-placeholder="Select Partner" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span8" colspan="2" style="text-align: left">
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
                                        <asp:Label runat="server" ID="Label2">Acad Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label3">Activity Type</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblActivityType_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label8">Partner</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblPartner_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span8" style="text-align: left" colspan="2">
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" OnItemDataBound="dlGridDisplay_ItemDataBound">
                            <HeaderTemplate>
                                <b>Partner Name</b> 
                                </th>
                                <th style="width: 8%; text-align: center;">
                                    Acad Year
                                </th>
                                 <th style="width: 12%; text-align: center;">
                                    Period
                                </th>
                                <th style="width: 14%; text-align: center;">
                                    Course
                                </th>
                                <th style="width: 12%; text-align: center;">
                                    Subject
                                </th>
                                <th style="width: 12%; text-align: center;">
                                    Payment Mode
                                </th>
                                <th style="width: 8%; text-align: center;">
                                    Rate
                                </th>
                                <th style="width: 8%; text-align: center;">
                                    Action
                                </th>
                                <th style="width: 8%; text-align: center; vertical-align: middle;">
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPartnerRateCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerRateCode")%>'></asp:Label>
                                <asp:Label runat="server" ID="lblPartnerName" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>'></asp:Label>
                                <asp:Label runat="server" ID="lblPartnerCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerCode")%>'></asp:Label>
                                </td>
                                <td style="width: 8%;">
                                    <asp:Label runat="server" ID="lblAcadYear" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"AcadYear")%>'></asp:Label>
                                </td>
                                <td style="width: 12%;">
                                    <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                        visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'
                                        value='<%#DataBinder.Eval(Container.DataItem,"RatePeriod")%>'
                                        id="txtPartnerRatePeriod" placeholder="Partner Rate Period" data-placement="bottom"
                                        data-original-title="Date Range" />
                                    <asp:Label ID="lblDLRatePeriod" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RatePeriodDesc")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 14%;">
                                    <asp:DropDownList runat="server" ID="ddlCourse" Width="185px" ToolTip="Course" data-placeholder="Select Course"
                                        CssClass="chzn-select" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
                                    <asp:Label ID="lblDLCourseName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    <asp:Label runat="server" ID="lblDLCourseCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCode")%>'></asp:Label>
                                </td>
                                <td style="width: 12%;">
                                    <asp:DropDownList runat="server" ID="ddlSubject" Width="150px" ToolTip="Subject"
                                        data-placeholder="Select Subject" CssClass="chzn-select" Visible="false" />
                                    <asp:Label ID="lblDLSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectName")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    <asp:Label runat="server" ID="lblDLSubjectCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectCode")%>'></asp:Label>
                                </td>
                                <td style="width: 12%;">
                                    <asp:DropDownList runat="server" ID="ddlPaymentMode" Width="150px" ToolTip="PaymentMode"
                                        data-placeholder="Select PaymentMode" CssClass="chzn-select" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                    <asp:Label ID="lblDLPaymentMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentMode")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    <asp:Label runat="server" ID="lblDLPaymentModeCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentModeCode")%>'></asp:Label>
                                </td>
                                <td style="width: 8%;">
                                    <asp:TextBox ID="txtPartnerRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerRate")%>' 
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' 
                                        onkeypress="return NumberOnly1()" Width="100px" >
                                    </asp:TextBox>
                                    <asp:Label ID="lblDLPartnerRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerRate")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />                                    
                                </td>
                                <td style="width: 8%; text-align: center;">
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PartnerRateCode")%>'
                                        runat="server" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>'
                                        CommandName="Edit" Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PartnerRateCode")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 8%; text-align: center; vertical-align: middle;">
                                    <a id="lbl_DLError" runat="server" title="Error" data-rel="tooltip" href="#">
                                        <asp:Panel ID="icon_Error" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:DataList ID="dlGridExport" Visible="false" runat="server" HorizontalAlign="Left"
                            CssClass="table table-striped table-bordered table-hover" Width="100%">
                            <HeaderTemplate>
                                <b>Partner Name</b>
                                <th>
                                    Acad Year
                                </th>
                                <th align="center" style="width: 20%; text-align: center">
                                    Period
                                </th>
                                <th style="width: 20%; text-align: left">
                                    Course
                                </th>
                                <th style="width: 20%; text-align: left">
                                    Subject
                                </th>
                                <th align="center" style="width: 20%; text-align: center">
                                    Payment Mode
                                </th>
                                
                                <th align="center" style="width: 20%; text-align: center">
                                    Rate
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPartnerName" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAcadYear" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AcadYear")%>' />
                                </td>
                                <td style="width: 20%; text-align: center">
                                    <asp:Label ID="lblPeriod" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RatePeriodDesc")%>' />
                                </td>
                                <td style="width: 20%; text-align: left">
                                    <asp:Label ID="lblCourse_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                                </td>
                                <td style="width: 20%; text-align: left">
                                    <asp:Label ID="lblSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectName")%>' />
                                </td>
                                <td style="width: 20%; text-align: center">
                                    <asp:Label ID="lblPaymentMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PaymentMode")%>' />
                                </td>                                
                                <td style="width: 20%; text-align: center">
                                    <asp:Label ID="lblPartnerRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerRate")%>' />
                                
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="dlGridDisplay" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>
