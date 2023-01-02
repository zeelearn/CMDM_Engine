<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Role" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalAuthorise() {
            $('#DivAuthorise').modal({
                backdrop: 'static'
            })

            $('#DivAuthorise').modal('show');
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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
                    Role<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
              <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="New" OnClick="BtnAdd_Click" />
                <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click"/>
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
            <asp:UpdatePanel ID="UpdatePanelsrch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                                                <td class="span4" style="text-align: left; width: 361px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12" >Role Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <asp:TextBox runat="server" ID="txtSearchRole" ToolTip="Role Name" type="text" Width="205px" />
                                                            
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </td>
                                                <td class="span4" style="text-align: left; width: 319px;">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label40" runat="server" >Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="chzn-select" Width ="215px">
                                                                    <asp:ListItem>Active</asp:ListItem>
                                                                    <asp:ListItem>Inactive</asp:ListItem>
                                                                </asp:DropDownList>
                                                                </td>
                                                        </tr>
                                                    </table>
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
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch" OnClick="BtnSearch_Click"
                                    Text="Search" ToolTip="Search" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate> 
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelresultpanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" onitemcommand="dlGridDisplay_ItemCommand" >
                    <HeaderTemplate>
                   
                        <b>Role ID</b> </th>
                        <th style="width: 60%; text-align: center;">
                            Role Name
                        </th>
                        <th style="width: 15%; text-align: center;">
                            Status
                        </th>              
                        <th style="width: 18%; text-align: center;" >
                        Action
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate> 
                                                  
                            <asp:Label ID="lblroleid" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"Role_Code")%>' />
                        </td>
                        <td style="width: 5%; text-align: center;">
                            <asp:Label ID="lblrolename" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Role_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'
                                    CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>'  />
                        </td>
                                               
                        <td style="width: 30px; text-align: center;">
                            <div class="inline position-relative">                                                           

                                <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Role_Code")%>' runat="server"
                                        CommandName="comEdit" Height="25px" />
                                        
                                <asp:LinkButton ID="LnkUser" ToolTip="User" class="btn-small btn-primary icon-user"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Role_Code")%>' runat="server"
                                        CommandName="comUser" Height="25px" />
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:DataList>

                <asp:DataList ID="DataList2" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false"  >
                    <HeaderTemplate>
                   
                        <b>Role ID</b> </th>
                        <th style="width: 60%; text-align: center;">
                            Role Name
                        </th>
                        <th style="width: 15%; text-align: center;">
                            Status
                        </th>              
                        
                    </HeaderTemplate>
                    <ItemTemplate> 
                                                  
                            <asp:Label ID="lblroleid" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"Role_Code")%>' />
                        </td>
                        <td style="width: 5%; text-align: center;">
                            <asp:Label ID="lblrolename" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Role_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />
                        </td>
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
            </ContentTemplate> 
            </asp:UpdatePanel>
        </div>
         <asp:UpdatePanel ID="UpdatePanelAddPnl" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create New Role" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left; width: 525px;">
                                                <table cellpadding="0" style="border-style: none; width: 82%;" 
                                                    class="table-hover">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label23">Role Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtAddRoleName" runat="server" MaxLength="100" 
                                                                ToolTip="Mid Name" type="text" ValidationGroup="UcValidate" Width="205px" />
                                                            
                                                            <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label28" runat="server">Status</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                            <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                    checked="checked" />
                                                            <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left; width: 525px;">
                                                &nbsp;</td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left" colspan="3">
                                                <div class="span12">
                                                    <div class="widget-box">
                                                        <div class="widget-header">
                                                            <h5 class="smaller">
                                                                Select Menu
                                                            </h5>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main ">
                                                            
                                                                <asp:DataList ID="dlMenu_Add" runat="server" 
                                                                    CssClass="table table-striped table-bordered table-hover" Width="97%">
                                                                    <HeaderTemplate>
                                                                        
                                                                <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChanged" />
                                                    <span class="lbl"></span>
                                                                        </th>
                                                                        <th align="left" style="width: 35%">
                                                                            Application Name
                                                                        </th>
                                                                        <th align="left" style="width: 25%">
                                                                            Menu Type
                                                                        </th>
                                                                        <th align="left" style="width: 40%">
                                                                            Menu Name
                                                                        </th>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCheck" runat="server" />
                                                    <span class="lbl"></span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblAppName" runat="server" 
                                                                                Text='<%#DataBinder.Eval(Container.DataItem,"System_Description")%>' />
                                                                                <asp:Label ID="lblMenuCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Menu_Code")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMenuType" runat="server" 
                                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Menu_Type")%>' />
                                                                            
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMenuName" runat="server" 
                                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Menu_Name")%>' />
                                                                           
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                 
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <!--/span-->
                                        <!--/span-->
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                            Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelAssignUser" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <div id="DivAssignUser" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="Label5" runat="server" Text="Assign Users" />
                    </h5>
                    <asp:Label ID="lblroleid_User" runat="server" Visible="False" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left; width: 388px;">
                                                <table cellpadding="0" style="border-style: none; width: 87%;" 
                                                    class="table-hover">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label7">Role Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtRoleName_User" runat="server" MaxLength="100" 
                                                                ToolTip="Mid Name" type="text" ValidationGroup="UcValidate" Width="205px" 
                                                                ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left; width: 388px;">
                                                &nbsp;</td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left" colspan="3">
                                                <div class="span12">
                                                    <div class="widget-box">
                                                        <div class="widget-header">
                                                            <h5 class="smaller">
                                                                Select Menu
                                                            </h5>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main">
                                                            
                                                                <asp:DataList ID="DataList1" runat="server" 
                                                                    CssClass="table table-striped table-bordered table-hover" Width="97%">
                                                                    <HeaderTemplate>
                                                                            
                                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                                    <span class="lbl"></span>

                                                                        </th>
                                                                        <th align="left" style="width: 50%">
                                                                            User ID
                                                                        </th>
                                                                        <th align="left" style="width: 50%">
                                                                            User Name
                                                                        </th>
                                                                        
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                       <asp:CheckBox ID="CHKUser" runat="server" />
                                                    <span class="lbl"></span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblDL_Activity" runat="server" 
                                                                                Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />
                                                                            <asp:Label ID="lblusercode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" 
                                                                                Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                                                                            
                                                                        </td>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <!--/span-->
                                        <!--/span-->
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label10" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave_User" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_User_Click" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                            ID="BtnSaveEdit_User" runat="server"
                            Text="Save" ValidationGroup="UcValidate" Visible="false" 
                            OnClick="BtnSaveEdit_User_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button3" Visible="true"
                            runat="server" Text="Close" OnClick="Button3_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;button>
                             <h4 class="modal-title">
                        Block Classroom
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to block classroom
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />. Do you want to Continue ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>

