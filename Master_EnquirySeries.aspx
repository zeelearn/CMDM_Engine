<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Master_EnquirySeries.aspx.cs" Inherits="Master_EnquirySeries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

//    function NumberOnly() {
//        var AsciiValue = event.keyCode
//        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
//            event.returnValue = true;
//        else
//            event.returnValue = false;
//    }

    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57))
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    function NumberandCharOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue==40 || AsciiValue==41 || AsciiValue==45)
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
            <li><i class="icon-home"></i><a href="#">Home</a><span class="divider"><i class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">Manage Enquiry Series<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search"
                OnClick="BtnShowSearchPanel_Click" />
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
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                             <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label14" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                                CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                             <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label2" CssClass="red">Acad Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" data-placeholder="Select Acad Year"
                                                                                CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                             <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label8" CssClass="red">Zone</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:DropDownList runat="server" ID="ddlZone" Width="215px" data-placeholder="Select Zone"
                                                                                CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlZone_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                             <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label9" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:DropDownList runat="server" ID="ddlCenter" Width="215px" data-placeholder="Select Center"
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
            <div id="DivResultPanel" runat="server" visible="false">

                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">Total No of Records:
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
                    <asp:DataList ID="dlDisplay" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlDisplay_ItemCommand" >
                        <HeaderTemplate>
                        <b>Center Name</b> </th>
                            <th style="width: 40%; text-align: center">Product
                            </th>
                            <th style="width: 20%; text-align: center">Number Range
                            </th>
                            <th style="width: 15%; text-align: center">Status
                            </th>
                            <th style="width: 10%; text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblCenterName" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"source_center_name")%>' />
                            </td>
                            <td style="width: 40%; text-align: left">
                                <asp:Label ID="lblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Product")%>' />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"NumberRange")%>' />
                            </td>
                            <td class='hidden-480' style="width: 15%; text-align: left">
                                <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                            <td style="width: 10%; text-align: center">
                                <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign" data-rel="tooltip"
                                    CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SerialCode")%>'
                                    ToolTip="Edit" data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible ="false" >
                        <HeaderTemplate>
                        <b>Center Name</b> </th>
                            <th style="width: 35%; text-align: center">Product
                            </th>
                            <th style="width: 20%; text-align: center">NumberRange
                            </th>
                            <th style="width: 20%; text-align: center">Status                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblCenterName" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"source_center_name")%>' />
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:Label ID="lblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Product")%>' />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"NumberRange")%>' />
                            </td>
                            <td class='hidden-480' style="width: 10%; text-align: left">
                                <asp:Label  runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>

            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                            <asp:Label ID="lblPKey" runat="server" Visible="false"></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivisionName" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlDivisionName_SelectedIndexChanged"  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Acad Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYearAdd" Width="215px" ToolTip="Acad Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" 
                                                                    AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlAcadYearAdd_SelectedIndexChanged"  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label41" runat="server" CssClass="red">Zone</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlZoneAdd" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Zone" Width="215px" 
                                                                    onselectedindexchanged="ddlZoneAdd_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label40" runat="server" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlCenterAdd" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Center" Width="215px" 
                                                                    onselectedindexchanged="ddlCenterAdd_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label39" runat="server" CssClass="red">Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlProduct_add"  data-placeholder="Select Product(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label38" runat="server" CssClass="red">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                <input runat="server" id="chkActive" name="switch-field-2" type="checkbox" class="ace-switch ace-switch-2"
                                                                                            checked="checked" />
                                                                <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label5" runat="server" CssClass="red">Start Number</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                 <asp:TextBox ID="txtStartNumber" runat="server" MaxLength="10"
                                                                         Width="205px" onkeypress="return NumberOnly(event);"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label7" runat="server" CssClass="red">End Number</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                 <asp:TextBox ID="txtEndNumber" runat="server" MaxLength="10"
                                                                         Width="205px" onkeypress="return NumberOnly(event);"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>

                                                
                                            </tr>
                                            
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" Visible="false" runat="server" ID="btnSave"
                                    Text="Save" ToolTip="Save" OnClick="btnSave_Click" />                                
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClear_Add" Visible="true"
                                    runat="server" Text="Close" OnClick="btnClear_Add_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        
        <!--/row-->
    </div>
</asp:Content>


