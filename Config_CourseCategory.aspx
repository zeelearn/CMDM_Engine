<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeFile="Config_CourseCategory.aspx.cs" Inherits="Config_CourseCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95 || AsciiValue == 45)
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
                    Course Category<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" onclick="BtnAdd_Click"  />
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
                                                    <table cellpadding="0" style="border-style: none; width: 122%;" 
                                                        class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6">Category Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtCourseCategoryName" Text="" Width="205px" onkeypress="return NumberandCharOnly(event);"></asp:TextBox>
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
                        <b>Course Category Code</b> </th>
                        <th align="left">
                            Course Category No
                        </th>
                        <th align="left">
                            Course Category Name
                        </th>
                        <th align="left">
                            Course Category Display Name
                        </th>
                        <th align="left">
                            Course Category Description
                        </th>
                        <th align="left">
                            IsActive
                        </th>                        
                        <th style="width: 100px; text-align: center;" >
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="lblCCId" runat="server" visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCateogryId")%>' />
                            <asp:Label ID="lblCCCode" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryCode")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblCCNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategorySequenceNo")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCCName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCCDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryDisplayName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCCDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryDescription")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' 
                            CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>'/>
                            
                        </td>                        
                        <td style="width: 100px; text-align: center;">
                            <div class="inline position-relative">             

                                        <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"CourseCateogryId")%>' runat="server"
                                            CommandName="comEdit"
                                            Height="25px" />
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" onitemcommand="dlGridDisplay_ItemCommand" Visible="false">
                    <HeaderTemplate>
                        <b>Course Category Code</b> </th>
                        <th align="left">
                            Course Category No
                        </th>
                        <th align="left">
                            Course Category Name
                        </th>
                        <th align="left">
                            Course Category Display Name
                        </th>
                        <th align="left">
                            Course Category Description
                        </th>
                        <th align="left">
                            IsActive
                        </th>                        
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="lblCCCode" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryCode")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblCCNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategorySequenceNo")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCCName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCCDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryDisplayName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCCDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CourseCategoryDescription")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'/>
                            
                        </td>                        
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                             Add Course Category Details
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
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label22" runat="server" CssClass="red">Category Name</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtAddCCName" runat="server" Text="" Width="80%" onkeypress="return NumberandCharOnly(event);"></asp:TextBox>                                                                                
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label1" runat="server">Display Name</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtAddCCDisplayName" runat="server" Text="" Width="80%" onkeypress="return NumberandCharOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label2" runat="server">Sequence No</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtAddCCSequenceNo" runat="server" Text="" Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
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
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label3" runat="server">Description</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtAddCCDescription" runat="server" Text="" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label4" runat="server">Is Active</asp:Label>
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
                            <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                                Text="Save" ValidationGroup="UcValidate" onclick="BtnSaveAdd_Click"/>
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" onclick="BtnCloseAdd_Click" />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Edit Course Category Details
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                    
                                                        <tr>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label5" runat="server" CssClass="red">Category Name</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCCName" runat="server" Text="" Width="80%" onkeypress="return NumberandCharOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label7" runat="server">Display Name</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCCDisplayName" runat="server" Text="" Width="80%" onkeypress="return NumberandCharOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label8" runat="server">Sequence No</asp:Label>
                                                                        </td>
                                                                        <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCCSequenceNo" runat="server" Text="" Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
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
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label9" runat="server">Description</asp:Label>
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtEditCCDescription" runat="server" Text="" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="span6" style="text-align: left">
                                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label ID="Label10" runat="server">Is Active</asp:Label>
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
            </div>  

        </div>
    </div>
     <!--/row-->
           
</asp:Content>
