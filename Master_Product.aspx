<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Master_Product.aspx.cs" Inherits="Master_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="topupdate" runat="server">
        <ContentTemplate>
            <div id="breadcrumbs" class="position-relative" style="height: 53px">
                <ul class="breadcrumb" style="height: 15px">
                    <li><i class="icon-home"></i><a href="#">Home</a><span class="divider"><i class="icon-angle-right"></i></span></li>
                    <li>
                        <h4 class="blue">Robomate+ <span class="divider"></span></h4>
                    </li>
                </ul>
                <div id="nav-search">
                    <!-- /btn-group -->
                    <asp:Button class="btn  btn-app btn-success btn-mini radius-4" Visible="true" runat="server"
                        ID="BtnAdd" Text="Add" OnClick="BtnAdd_Click" />
                    <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true" runat="server"
                        ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
                </div>
                <!--#nav-search-->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnAdd" />
            <asp:PostBackTrigger ControlID="BtnShowSearchPanel" />
        </Triggers>
    </asp:UpdatePanel>
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
            <asp:UpdatePanel ID="masterupdate" runat="server">
                <ContentTemplate>
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
                                                                        <asp:Label ID="lblsearchproducttype" runat="server" CssClass="red">Product Type </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlsearchproducttype" Width="215px" data-placeholder="Select Product Type"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlsearchproducttype_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="lblsearchproductsubtype" CssClass="red">Product Sub Type </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlsearchproductsubtype" Width="215px" data-placeholder="Select Product Sub Type"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label13" runat="server" CssClass="red">Division </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlDivisionName" Width="215px" data-placeholder="Select Division Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionName_SelectedIndexChanged" />
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
                                                                        <asp:Label runat="server" ID="Label6" CssClass="red">Course </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label7" runat="server" CssClass="red">Academic Year </asp:Label>
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
                                                                        <asp:Label ID="Label36" runat="server" >Product Code </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                         <asp:TextBox ID="txtProductcode" runat="server" Width="205px"></asp:TextBox> 
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
                    <div id="DivBeforeadd" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">Product Type
                                    <asp:Label runat="server" ID="Label20" Visible="false"></asp:Label>
                                </h5>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                    <tr>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lblproducttype" runat="server" CssClass="red">Product Type </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlproducttype" Width="14%" data-placeholder="Select Product Type"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlproducttype_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="lblsubtype" CssClass="red">Product Sub Type </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlproductsubtype" Width="14%" data-placeholder="Product Sub Type"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlproductsubtype_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left"></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                                        <td class="span10">Total No of Records:
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
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                            <HeaderTemplate>
                                <b>Product Code</b> </th>
                                <th align="left">Product Category Name
                                </th>
                                <th align="left">Product Name
                                </th>
                                <th align="left">Status
                                </th>
                                <th style="width: 100px; text-align: center;">
                                Action
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductCatname" runat="server" Text='<%# Convert.ToString( Eval("ProductCatCode")) == "LMS" ? "LMS":"NON LMS"  %>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'
                                        CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>' />
                                </td>
                                <td style="width: 100px; text-align: center;">
                                    <div class="inline position-relative">
                                        <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' runat="server"
                                            CommandName="comEdit" Height="25px" />
                                    </div>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false">
                            <HeaderTemplate>
                                <b>Product Code</b> </th>
                                <th align="left">Product Category Name
                                </th>
                                <th align="left">Product Name
                                </th>
                                <th align="left">Status
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductCatname" runat="server" Text='<%# Convert.ToString( Eval("ProductCatCode")) == "LMS" ? "LMS":"NON LMS"  %>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    <div id="DivResulPanel1" runat="server" class="dataTables_wrapper" visible="false">
                        <div class="widget-box">
                            <div class="table-header">
                                <table width="100%">
                                    <tr>
                                        <td class="span10">Total No of Records:
                                            <asp:Label runat="server" ID="lbltotalcountnew" Text="0" />
                                        </td>
                                        <td style="text-align: right" class="span2">
                                            <asp:LinkButton runat="server" ID="btnexportnew" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                                Height="25px" OnClick="btnexportnew_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:DataList ID="dlGridDisplaynew" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplaynew_ItemCommand">
                            <HeaderTemplate>
                                <b>Product Code</b> </th>
                                <th align="left">Product Category Name
                                </th>
                                <th align="left">Product Name
                                </th>
                                <th align="left">Status
                                </th>
                                <th style="width: 100px; text-align: center;">
                                Action
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductCatname" runat="server" Text='<%# Convert.ToString( Eval("ProductCatCode")) == "LMS" ? "LMS":"NON LMS"  %>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'
                                        CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>' />
                                </td>
                                <td style="width: 100px; text-align: center;">
                                    <div class="inline position-relative">
                                        <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' runat="server"
                                            CommandName="ComEdit" Height="25px" />
                                    </div>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:DataList ID="DataList3" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false">
                            <HeaderTemplate>
                                <b>Product Code</b> </th>
                                <th align="left">Product Category Name
                                </th>
                                <th align="left">Product Name
                                </th>
                                <th align="left">Status
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductCatname" runat="server" Text='<%# Convert.ToString( Eval("ProductCatCode")) == "LMS" ? "LMS":"NON LMS"  %>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblProductName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div id="DivAddPanel" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">Add Robomate+ Product Plan:Classroom
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
                                                                        <asp:Label ID="Label5" runat="server" CssClass="red">Division </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlDivisionAdd" Width="14%" data-placeholder="Select Division Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionAdd_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="Label9" CssClass="red">Course </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlCourseAdd" Width="14%" data-placeholder="Select Course Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseAdd_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label10" runat="server" CssClass="red">Academic Year </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlAcdYearAdd" Width="14%" data-placeholder="Select Academic Year"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
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
                                                                        <asp:Label ID="Label22" runat="server" CssClass="red">Course Category</asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlCCName" Width="14%" data-placeholder="Select Course Category"
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
                                                                        <asp:DropDownList runat="server" ID="ddlBoardName" Width="14%" data-placeholder="Select Board Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="red">Medium </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlMediumName" Width="14%" data-placeholder="Select Medium Name"
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
                                                                        <asp:Label ID="Label14" runat="server" CssClass="red">Class Room Product(s)</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:ListBox runat="server" ID="ddlAddClassRoomProduct" Width="14%" data-placeholder="Select Class Room Product(s)"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label25" runat="server" CssClass="red">Subject(s)</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:ListBox runat="server" ID="lstboxaddsubjects" Width="14%" data-placeholder="Select Subject(s)"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label1" runat="server" CssClass="red">Product Category </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlProductCat" Width="14%" data-placeholder="Select Product Category"
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
                                                                        <asp:Label ID="Label2" runat="server">SKU Code</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtAddSKUCode" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label3" runat="server" CssClass="red">Product Name</asp:Label>
                                                                        <span class="help-button ace-popover" data-trigger="hover" data-placement="right"
                                                                            data-content="Student taken admission for LMS or NON-LMS Product" title="Product(s)">?</span>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtAddName" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label23" runat="server">Description</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtAddDescription" runat="server" Text="" Width="76%"></asp:TextBox>
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
                                                                        <asp:Label ID="Label15" runat="server">Price</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtAddPrice" runat="server" Text="" Width="76%" onkeypress="return NumberOnly()"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label31" runat="server">Bucket Name</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtAddBucketName" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label30" runat="server" CssClass="red">Exam Month</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlAddExamMonth" Width="14%" data-placeholder="Select Exam Month"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <asp:Label ID="Label32" runat="server" CssClass="red">Exam Year</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlAddExamYear" Width="14%" data-placeholder="Select Exam Month"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                            <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                            <asp:ListItem Value="2027">2027</asp:ListItem>
                                                                            <asp:ListItem Value="2028">2028</asp:ListItem>
                                                                            <asp:ListItem Value="2029">2029</asp:ListItem>
                                                                            <asp:ListItem Value="2030">2030</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <i class="icon-calendar"></i>
                                                                        <asp:Label runat="server" ID="Label34" CssClass="red">Product Validity</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <input readonly="readonly" runat="server" class="id_date_range_picker_1 span10" name="date-range-picker"
                                                                            id="id_date_range_picker_1" width="14%" placeholder="Date Search" data-placement="bottom"
                                                                            data-original-title="Date Range" />
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="Label33">Is Available for Market</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <label>
                                                                            <input runat="server" id="chkMarketFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                                checked="checked" />
                                                                            <span class="lbl"></span>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>


                                                        <tr>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label runat="server" ID="Label35">Is Active</asp:Label>
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
                                                            <td class="span6" style="text-align: left"></td>
                                                            <td class="span6" style="text-align: left"></td>
                                                        </tr>

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
                    <div id="DivAddNew" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">Add Robomate+ Classroom
                                    <asp:Label runat="server" ID="Label21" Visible="false"></asp:Label>
                                </h5>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <asp:UpdatePanel ID="UpdatePanelAddNew" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                    <tr>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewdivision" runat="server" CssClass="red">Division </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewdivision" Width="14%" data-placeholder="Select Division Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddladdnewdivision_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="lbladdnewcourse" CssClass="red">Course </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewcourse" Width="14%" data-placeholder="Select Course Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddladdnewcourse_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewacadyear" runat="server" CssClass="red">Academic Year </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewacadyear" Width="14%" data-placeholder="Select Academic Year"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddladdnewacadyear_SelectedIndexChanged" />
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
                                                                        <asp:Label ID="lbladdnewcoursecategoary" runat="server" CssClass="red">Course Category</asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewcoursecategory" Width="14%" data-placeholder="Select Course Category"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddladdnewcoursecategory_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewboard" runat="server" CssClass="red">Board </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewboard" Width="14%" data-placeholder="Select Board Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewmedium" runat="server" CssClass="red">Medium </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewmedium" Width="14%" data-placeholder="Select Medium Name"
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
                                                                        <asp:Label ID="lblclassroomproduct" runat="server" CssClass="red">Class Room Product</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewclassroomproducts" Width="14%" data-placeholder="Select Class Room Product"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                       <%-- <asp:ListBox runat="server" ID="lstboxaddnewclassroomproducts" data-placeholder="Select Class Room Product(s)" Width="14%"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewsubjects" runat="server" CssClass="red">Subject(s)</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:ListBox runat="server" ID="lstboxaddnewsubjects" data-placeholder="Select Subject(s)" Width="14%"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewproductcategory" runat="server" CssClass="red">Product Category </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewproductcategory" Width="14%" data-placeholder="Select Product Category"
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
                                                                        <asp:Label ID="lbladdnewskucode" runat="server">SKU Code</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtaddnewskucode" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewproductname" runat="server" CssClass="red">Product Name</asp:Label>
                                                                        <span class="help-button ace-popover" data-trigger="hover" data-placement="right"
                                                                            data-content="Student taken admission for LMS or NON-LMS Product" title="Product(s)">?</span>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtaddnewproductname" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewsescription" runat="server">Description</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtaddnewdescription" runat="server" Text="" Width="76%"></asp:TextBox>
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
                                                                        <asp:Label ID="lbladdnewbucketname" runat="server">Bucket Name</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtaddnewbucketname" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewexammonth" runat="server" CssClass="red">Exam Month</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewexammonth" Width="14%" data-placeholder="Select Exam Month"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbladdnewexamyear" runat="server" CssClass="red">Exam Year</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddladdnewexamyear" Width="14%" data-placeholder="Select Exam Year"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                            <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                            <asp:ListItem Value="2027">2027</asp:ListItem>
                                                                            <asp:ListItem Value="2028">2028</asp:ListItem>
                                                                            <asp:ListItem Value="2029">2029</asp:ListItem>
                                                                            <asp:ListItem Value="2030">2030</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <i class="icon-calendar"></i>
                                                                        <asp:Label runat="server" ID="Label26" CssClass="red">Product Validity</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <input readonly="readonly" runat="server" class="id_date_range_picker_1 span10" name="date-range-picker"
                                                                            id="add_new_id_date_range_picker_1" width="14%" placeholder="Date Search" data-placement="bottom"
                                                                            data-original-title="Date Range" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="Label27">Is Available for Market</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <label>
                                                                            <input runat="server" id="add_new_chkMarketFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
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
                                                                        <asp:Label runat="server" ID="lbladdnewisactive">Is Active</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <label>
                                                                            <input runat="server" id="chkboxaddnewisactive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                                checked="checked" />
                                                                            <span class="lbl"></span>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>


                                                </table>

                                                <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                                                    <tr>
                                                        <td class="span6" style="text-align: left">

                                                            <asp:DataList ID="dlplanprice" CssClass="table table-striped table-bordered table-hover"
                                                                runat="server" Width="100%">
                                                                <HeaderTemplate>

                                                                    <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChanged" Visible="false" />
                                                                    <span class="lbl"></span></th>

                                                    <th style="text-align: center">Plan 
                                                    </th>
                                                                    <th style="text-align: center">Price
                                                                    </th>
                                                                    <th></th>


                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:CheckBox ID="chkCenter"
                                                                        runat="server" AutoPostBack="true"
                                                                        OnCheckedChanged="chkCenter_CheckedChanged" />
                                                                    <span class="lbl"></span>

                                                                    </td>
                                                                 <td style="text-align: center;">
                                                                     <asp:Label ID="lblplan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Plan_Name")%>' />
                                                                 </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:TextBox placeholder="0" ID="txtplanamt" Width="205px" runat="server" Enabled="false"
                                                                            MaxLength="100" onkeypress="return NumberOnly(event);" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete="off" />
                                                                    </td>
                                                                    <td style="text-align: center;">

                                                                        <asp:Label ID="lblplanid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Record_Id")%>' />
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:DataList>

                                                        </td>

                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                    <!--Button Area -->
                                    <asp:Label runat="server" ID="Label61" Text="" ForeColor="Red" />
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnaddnewsave" runat="server"
                                        Text="Save" ValidationGroup="UcValidate" OnClick="btnaddnewsave_Click" />

                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnaddnewclose" Visible="true"
                                        runat="server" Text="Close" OnClick="btnaddnewclose_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DivEditPanel" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">Edit Robomate+ Product Plan:Classroom
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
                                                                        <asp:Label ID="Label4" runat="server" CssClass="red">Division </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlDivisionEdit" Width="14%" data-placeholder="Select Division Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionEdit_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="Label8" CssClass="red">Course </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlCourseEdit" Width="14%" data-placeholder="Select Course Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseEdit_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label16" runat="server" CssClass="red">Academic Year </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlAcdYearEdit" Width="14%" data-placeholder="Select Academic Year"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
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
                                                                        <asp:Label ID="Label17" runat="server" CssClass="red">Course Category</asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlCCEdit" Width="14%" data-placeholder="Select Course Category"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label18" runat="server" CssClass="red">Board </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlBoardEdit" Width="14%" data-placeholder="Select Board Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label19" runat="server" CssClass="red">Medium </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlMediumEdit" Width="14%" data-placeholder="Select Medium Name"
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
                                                                        <asp:Label ID="Label37" runat="server" CssClass="red">Class Room Product(s)</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:ListBox runat="server" ID="ddlEditClassRoomProduct" data-placeholder="Select Class Room Product(s)" Width="14%"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditsubjects" runat="server" CssClass="red">Subject(s)</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:ListBox runat="server" ID="lstboxeditsubjects" data-placeholder="Select Subject(s)" Width="14%"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label38" runat="server" CssClass="red">Product Category </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlEditProductCat" Width="14%" data-placeholder="Select Product Category"
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
                                                                        <asp:Label ID="Label39" runat="server">SKU Code</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtEditSKUCode" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label40" runat="server" CssClass="red">Product Name</asp:Label>
                                                                        <span class="help-button ace-popover" data-trigger="hover" data-placement="right"
                                                                            data-content="Student taken admission for LMS or NON-LMS Product" title="Product(s)">?</span>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtEditName" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label41" runat="server">Description</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtEditDescription" runat="server" Text="" Width="76%"></asp:TextBox>
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
                                                                        <asp:Label ID="Label42" runat="server">Price</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtEditPrice" runat="server" Text="" Width="76%" onkeypress="return NumberOnly()"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label43" runat="server">Bucket Name</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txtEditBucketName" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="Label44" runat="server" CssClass="red">Exam Month</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlEditExamMonth" Width="14%" data-placeholder="Select Exam Month"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <asp:Label ID="Label45" runat="server" CssClass="red">Exam Year</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddlEditExamYear" Width="14%" data-placeholder="Select Exam Month"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                            <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                            <asp:ListItem Value="2027">2027</asp:ListItem>
                                                                            <asp:ListItem Value="2028">2028</asp:ListItem>
                                                                            <asp:ListItem Value="2029">2029</asp:ListItem>
                                                                            <asp:ListItem Value="2030">2030</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <i class="icon-calendar"></i>
                                                                        <asp:Label runat="server" ID="Label46" CssClass="red">Product Validity</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <input readonly="readonly" runat="server" class="id_date_range_picker_1 span10" name="date-range-picker"
                                                                            id="id_Edit_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                            data-original-title="Date Range" />
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="Label47">Is Available for Market</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <label>
                                                                            <input runat="server" id="chkEditMarketFlag" name="switch-field-1" type="checkbox"
                                                                                class="ace-switch ace-switch-2" checked="checked" />
                                                                            <span class="lbl"></span>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <tr>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label runat="server" ID="Label48">Is Active</asp:Label>
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
                                                            <td class="span6" style="text-align: left"></td>
                                                            <td class="span6" style="text-align: left"></td>
                                                        </tr>
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

                    <div id="DivEditPanelNew" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">Edit Robomate+ Classroom
                                    <asp:Label runat="server" ID="Label24" Visible="false"></asp:Label>
                                </h5>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                    <tr>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewdivision" runat="server" CssClass="red">Division </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewdivision" Width="14%" data-placeholder="Select Division Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddleditnewdivision_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="lbleditnewcourse" CssClass="red">Course </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewcourse" Width="14%" data-placeholder="Select Course Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddleditnewcourse_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewacadyear" runat="server" CssClass="red">Academic Year </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewacadyear" Width="14%" data-placeholder="Select Academic Year"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
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
                                                                        <asp:Label ID="lbleditnewcoursecategory" runat="server" CssClass="red">Course Category</asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewcoursecategory" Width="14%" data-placeholder="Select Course Category"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewboard" runat="server" CssClass="red">Board </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewboard" Width="14%" data-placeholder="Select Board Name"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span4" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewmedium" runat="server" CssClass="red">Medium </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewmedium" Width="14%" data-placeholder="Select Medium Name"
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
                                                                        <asp:Label ID="lbleditclassroomproducts" runat="server" CssClass="red">Class Room Product</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                          <asp:DropDownList runat="server" ID="ddleditnewclassroomproducts" Width="14%" data-placeholder="Select Class Room Product"
                                                                            CssClass="chzn-select" AutoPostBack="True" />
                                                                        <%--<asp:ListBox runat="server" ID="lstboxeditnewclassroomproducts" data-placeholder="Select Class Room Product(s)" Width="14%"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewsubjects" runat="server" CssClass="red">Subject(s)</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:ListBox runat="server" ID="lstboxeditnewsubjects" data-placeholder="Select Subject(s)" Width="14%"
                                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewproductcategory" runat="server" CssClass="red">Product Category </asp:Label>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewproductcategory" Width="14%" data-placeholder="Select Product Category"
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
                                                                        <asp:Label ID="lbleditnewskucode" runat="server">SKU Code</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txteditnewskucode" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewproductname" runat="server" CssClass="red">Product Name</asp:Label>
                                                                        <span class="help-button ace-popover" data-trigger="hover" data-placement="right"
                                                                            data-content="Student taken admission for LMS or NON-LMS Product" title="Product(s)">?</span>
                                                                    </td>
                                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txteditnewproductname" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewdescription" runat="server">Description</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txteditnewdescription" runat="server" Text="" Width="76%"></asp:TextBox>
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
                                                                        <asp:Label ID="lbleditnewbucketname" runat="server">Bucket Name</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:TextBox ID="txteditnewbucketname" runat="server" Text="" Width="76%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewexammonth" runat="server" CssClass="red">Exam Month</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewexammonth" Width="14%" data-placeholder="Select Exam Month"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label ID="lbleditnewexamyear" runat="server" CssClass="red">Exam Year</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <asp:DropDownList runat="server" ID="ddleditnewexamyear" Width="14%" data-placeholder="Select Exam Year"
                                                                            CssClass="chzn-select" AutoPostBack="True">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                            <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                            <asp:ListItem Value="2027">2027</asp:ListItem>
                                                                            <asp:ListItem Value="2028">2028</asp:ListItem>
                                                                            <asp:ListItem Value="2029">2029</asp:ListItem>
                                                                            <asp:ListItem Value="2030">2030</asp:ListItem>
                                                                        </asp:DropDownList>
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
                                                                        <i class="icon-calendar"></i>
                                                                        <asp:Label runat="server" ID="Label28" CssClass="red">Product Validity</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <input readonly="readonly" runat="server" class="id_date_range_picker_1 span10" name="date-range-picker"
                                                                            id="edit_new_id_Edit_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                            data-original-title="Date Range" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="span6" style="text-align: left">
                                                            <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                                <tr>
                                                                    <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                        <asp:Label runat="server" ID="Label29">Is Available for Market</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <label>
                                                                            <input runat="server" id="edit_new_chkEditMarketFlag" name="switch-field-1" type="checkbox"
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
                                                                        <asp:Label runat="server" ID="lbleditnewisactive">Is Active</asp:Label>
                                                                    </td>
                                                                    <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                        <label>
                                                                            <input runat="server" id="chkboxeditnewisactive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                                checked="checked" />
                                                                            <span class="lbl"></span>
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>


                                                </table>

                                                <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                                                    <tr>

                                                        <td class="span6" style="text-align: left">

                                                            <asp:DataList ID="dleditnewplanprice" CssClass="table table-striped table-bordered table-hover"
                                                                runat="server" Width="100%">
                                                                <HeaderTemplate>

                                                                    <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChangededit" Visible="false" />
                                                                    <span class="lbl"></span></th>

                                                    <th style="text-align: center">Plan 
                                                    </th>
                                                                    <th style="text-align: center">Price
                                                                    </th>
                                                                    <th></th>


                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:CheckBox ID="chkCenter"
                                                                        runat="server" AutoPostBack="true" OnCheckedChanged="chkCenter_CheckedChangededit" />
                                                                    <span class="lbl"></span>

                                                                    </td>
                                                                 <td style="text-align: center;">
                                                                     <asp:Label ID="lblplan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Plan_Name")%>' />
                                                                 </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:TextBox placeholder="0" ID="txtplanamt" Width="205px" runat="server" Enabled="false"
                                                                            MaxLength="100" onkeypress="return NumberOnly(event);" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete="off" />
                                                                    </td>
                                                                    <td style="text-align: center;">

                                                                        <asp:Label ID="lblplanid" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Record_Id")%>' />
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:DataList>

                                                        </td>

                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                    <!--Button Area -->
                                    <asp:Label runat="server" ID="Label59" Text="" ForeColor="Red" />
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btneditnewsave" runat="server"
                                        Text="Save" ValidationGroup="UcValidate" OnClick="btneditnewsave_Click" />

                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btneditnewclose" Visible="true"
                                        runat="server" Text="Close" OnClick="btneditnewclose_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary5" ShowSummary="false" DisplayMode="List"
                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    
                    <asp:PostBackTrigger ControlID="dlGridDisplaynew" />
                    <asp:PostBackTrigger ControlID="dlGridDisplay" />
                    
                    <asp:PostBackTrigger ControlID="HLExport" />
                     <asp:PostBackTrigger ControlID="BtnSearch" />
                    <asp:PostBackTrigger ControlID="BtnClearSearch" />
                    <asp:PostBackTrigger ControlID="btnexportnew" />
                    <asp:PostBackTrigger ControlID="BtnSaveAdd" />
                    <asp:PostBackTrigger ControlID="BtnCloseAdd" />
                    <asp:PostBackTrigger ControlID="btnaddnewsave" />
                    <asp:PostBackTrigger ControlID="btnaddnewclose" />
                    <asp:PostBackTrigger ControlID="btnEditSave" />
                    <asp:PostBackTrigger ControlID="btnEditClose" />
                    <asp:PostBackTrigger ControlID="btneditnewsave" />
                    <asp:PostBackTrigger ControlID="btneditnewclose" />
                    <asp:PostBackTrigger ControlID="btneditnewclose" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--/row-->
</asp:Content>
