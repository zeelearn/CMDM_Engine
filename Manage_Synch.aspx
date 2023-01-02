<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeFile="Manage_Synch.aspx.cs" Inherits="Manage_Synch" %>

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
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Manage Synchronisation<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true"
                runat="server" ID="BtnShowSearchPanel" Text="Search" 
                onclick="BtnShowSearchPanel_Click" />
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
                            <asp:Label runat="server" ID="lblColumn" Visible="False"></asp:Label>
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
                                                    <table cellpadding="0" style="border-style: none; width: 139%;" 
                                                        class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label13" runat="server" CssClass="red">Database Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlDatabaseName" runat="server" 
                                                                    AutoPostBack="True" CssClass="chzn-select" 
                                                                    data-placeholder="Select Database Name" Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 118%;" 
                                                        class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                &nbsp;</td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                &nbsp;</td>
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
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" 
                                    onclick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
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
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>                
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" onitemcommand="dlGridDisplay_ItemCommand" >
                    <HeaderTemplate>
                        <b>Table No</b> </th>
                        <th style="width: 30%; text-align: center;">
                            Table Name
                        </th>
                        <th style="width: 20%; text-align: center;">
                            Pending Records
                        </th>
                        <th style="width: 20%; text-align: center;">
                            Last Updated On
                        </th>
                                                                
                        <th style="width: 20%; text-align: center;" >
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>   
                    
                            <asp:Label ID="lblMenuCode" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"TableCode")%>' />
                        </td>
                        <td style="width: 30%; text-align: left;">
                            <asp:Label ID="lblMenuName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TableName")%>' />
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label ID="lblMenuCss" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PendingRecordsCount")%>' />
                        </td>
                        <td style="width: 20%; text-align: left;">
                            <asp:Label ID="lblMenuParentcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LastUpdatedOn")%>' />
                        </td>
                                          
                        <td style="width: 20%; text-align: center;">
                            <div class="inline position-relative">                                                           
                            
                                <asp:LinkButton ID="lnkSync" ToolTip="Sync" class="btn-small btn-primary icon-cogs"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"TableCode")%>' runat="server"
                                        CommandName="comSync" Height="25px" />&nbsp;
                                
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false" >
                    <HeaderTemplate>
                        <b>Table No</b> </th>
                        <th style="width: 30%; text-align: center;">
                            Table Name
                        </th>
                        <th style="width: 20%; text-align: center;">
                            Pending Records
                        </th>
                        <th style="width: 20%; text-align: center;">
                            Last Updated On
                        </th>
                        
                    </HeaderTemplate>
                    <ItemTemplate>   
                    
                            <asp:Label ID="lblMenuCode" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"TableCode")%>' />
                        </td>
                        <td style="width: 30%; text-align: left;">
                            <asp:Label ID="lblMenuName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TableName")%>' />
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label ID="lblMenuCss" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PendingRecordsCount")%>' />
                        </td>
                        <td style="width: 20%; text-align: left;">
                            <asp:Label ID="lblMenuParentcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LastUpdatedOn")%>' />
                        </td>
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <%--<div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                              Menu Details
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
                                                    <table cellpadding="0" class="table-hover" 
                                                        style="border-style: none; width: 142%;">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label32" runat="server" CssClass="red">Application Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlApplicationName" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Parent Mneu Name" 
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" 
                                                        style="border-style: none; width: 142%;">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label31" runat="server" CssClass="red">Menu Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtMenuName" runat="server" Text="" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" 
                                                        style="border-style: none; width: 138%;">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label30" runat="server" CssClass="red">Menu Css</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtMenuCss" runat="server" Text="" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                                        <tr>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" 
                                                                    style="border-style: none; width: 100%;">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label29" runat="server" CssClass="red">Menu Link</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtMenuLink" runat="server" Text="" Width="205px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label28" runat="server">Menu Discription</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtMenuDiscription" runat="server" Text="" Width="205px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label27" runat="server" CssClass="red">Menu Type</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:DropDownList ID="ddlMenuType" runat="server" AutoPostBack="true" 
                                                                                CssClass="chzn-select" 
                                                                                onselectedindexchanged="ddlMenuType_SelectedIndexChanged" Width="215px">
                                                                                <asp:ListItem Value="0">Select Menu Type</asp:ListItem>
                                                                                <asp:ListItem Value="1">Parent</asp:ListItem>
                                                                                <asp:ListItem Value="2">Child</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label1" runat="server">Parent Menu Name</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:DropDownList ID="ddlParentMenuName" runat="server" AutoPostBack="True" 
                                                                                CssClass="chzn-select" data-placeholder="Select Parent Mneu Name" 
                                                                                Width="215px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" 
                                                                    style="border-style: none; width: 100%;">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label25" runat="server" CssClass="red">Order No</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtOrderNo" runat="server" Text="" Width="205px" onkeypress="return NumberOnly()"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label24" runat="server">Is Active</asp:Label>
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
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                </table>
                                                            </td>
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
                                Text="Save" ValidationGroup="UcValidate" onclick="BtnSaveAdd_Click"/>
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                Text="Save" ValidationGroup="UcValidate" onclick="BtnSaveEdit_Click" 
                                Visible="False" />
                            
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" onclick="BtnCloseAdd_Click" />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>--%>
            <%--<div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Edit Course Details
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
                                                              
                                                                <asp:Label ID="Label9" runat="server" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">                               
                                                                
                                                                                <asp:DropDownList runat="server" ID="ddlDivisionEdit" Width="142px" data-placeholder="Select Division Name"
                                                                                CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label16" runat="server" CssClass="red">Category </asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCCEdit" Width="142px" data-placeholder="Select Course Category"
                                                                                CssClass="chzn-select" AutoPostBack="True" />                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label17" runat="server" CssClass="red">Board </asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBoardEdit" Width="142px" data-placeholder="Select Board Name"
                                                                                CssClass="chzn-select" AutoPostBack="True" />
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                                        <tr>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label18" runat="server" CssClass="red">Medium </asp:Label>                                                                           
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:DropDownList runat="server" ID="ddlMediumEdit" Width="142px" data-placeholder="Select Medium Name"
                                                                                CssClass="chzn-select" AutoPostBack="True" />                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label24" runat="server" CssClass="red">Course Name</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCourseName" runat="server" Text="" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label25" runat="server">Display Name</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCourseDisplayName" runat="server" Text="" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>                                                           
                                                        </tr>
                                                        <tr>
                                                             <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label26" runat="server">Short Name</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCourseShortName" runat="server" Text="" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label27" runat="server">Description</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCourseDesc" runat="server" Text="" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label28" runat="server">Sequence No</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCourseSequenceNo" runat="server" Text="" Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>                                              
                                                            
                                                        </tr>    
                                                        <tr>
                                                             <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label29" runat="server">Is Active</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <label>
                                                                            <input runat="server" id="chkEditActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                                            checked="checked" />
                                                                            <span class="lbl"></span>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                             <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                </table>
                                                            </td>
                                                             <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                </table>
                                                            </td>
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
                                Text="Save" ValidationGroup="UcValidate" onclick="btnEditSave_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnEditClose"
                                Visible="true" runat="server" Text="Close" onclick="btnEditClose_Click"  />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>--%>  

        </div>
    </div>
     <!--/row-->
           
</asp:Content>
