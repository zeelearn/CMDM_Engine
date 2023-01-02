<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Config_Workflow.aspx.cs" Inherits="Config_Workflow" %>

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
                    WorkFlow<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
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
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15">User Name</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtUserName" runat="server" width="205px" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17">User Code</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtUserCode" runat="server" width="205px" ></asp:TextBox>
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
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
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
                        <b> User ID</b> </th>                                
                        <th style="width: 20%; text-align: left;">
                            User Code
                        </th>
                        <th style="width: 50%; text-align: left;">
                            User Name
                        </th>
                                
                        <th style="width: 5%; text-align: center;">
                            Action
                                
                    </HeaderTemplate>
                    <ItemTemplate>                                
                        <asp:Label ID="lblDLUserId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Name")%>'/>
                        </td>
                        <td>                                   
                            <asp:Label ID="lblDLUserCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />                                    
                        </td>
                        <td>                                    
                            <asp:Label ID="lblDLUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                        </td>
                                
                        <td style="width: 5%; text-align: center;">
                            <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' runat="server"
                                CommandName="Edit" Height="25px" />                                   
                                
                    </ItemTemplate>
                </asp:DataList> 
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false" >
                    <HeaderTemplate>
                        <b> User ID</b> </th>                                
                        <th style="width: 20%; text-align: left;">
                            User Code
                        </th>
                        <th style="width: 50%; text-align: left;">
                            User Name
                        </th>
                         
                    </HeaderTemplate>
                    <ItemTemplate>                                
                        <asp:Label ID="lblDLUserId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Name")%>'/>
                        </td>
                        <td>                                   
                            <asp:Label ID="lblDLUserCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />                                    
                        </td>
                        <td>                                    
                            <asp:Label ID="lblDLUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                        </td>
                          
                    </ItemTemplate>
                </asp:DataList>              
            </div>

            <div id="DivAddPanel" runat="server">                 
                 
                        <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label7">User Name</asp:Label>
                                                                
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblUserName_Result" class="blue"></asp:Label>
                                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label8" >User Code</asp:Label>                                                                                                                              
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblUserCode_Result" class="blue"></asp:Label>                                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                                           
                        </table>

                        <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                            <tr id="AddButtonRow" runat="server">
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>                                                           
                                            <td style="border-style: none; text-align: Right; width: 100%;">
                                                 <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                                                        Text="Add" onclick="BtnAdd_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                                
                            </tr>  
                             
                            <tr id="GridRequestRow" runat="server">
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>                                                           
                                            <td style="border-style: none; text-align: Center; width: 100%;">
                                                 
                                                    <asp:DataList ID="dlGridRequest" CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%" onitemcommand="dlGridRequest_ItemCommand" >
                                                            <HeaderTemplate>
                                                                <b> Request Type</b> </th>                                
                                                                <th style="width: 10%; text-align: left;">
                                                                    Level No
                                                                </th>
                                                                <th style="width: 50%; text-align: left;">
                                                                    Centre(s)
                                                                </th>                                
                                                                <th style="width: 10%; text-align: center;">
                                                                    Action
                                
                                                            </HeaderTemplate>
                                                            <ItemTemplate>                                
                                                                <asp:Label ID="lblDLRequestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request_Type")%>'/>
                                                                </td>
                                                                <td>                                   
                                                                    <asp:Label ID="lblDLLevel_No" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Level_No")%>' />                                    
                                                                </td>
                                                                <td>                                    
                                                                    <asp:Label ID="lblDLCentres" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                                                                </td>
                                
                                                                <td style="width: 10%; text-align: center;">
                                                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Request_Code")%>' runat="server"
                                                                        CommandName="Edit" Height="25px" /> 
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" class="btn-small btn-primary icon-trash"
                                                                        CommandName='comDelete' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Request_Code")%>'
                                                                        ToolTip="Delete" Visible = "false"></asp:LinkButton>                                  
                                
                                                            </ItemTemplate>
                                                        </asp:DataList>

                                            </td>
                                        </tr>
                                    </table>
                                </td>                                                
                            </tr> 
                            
                             <tr id="RequestAddRow" runat="server">
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>                                                           
                                            <td style="border-style: none; text-align: Center; width: 100%;">
                                                 <div id="divRequestAdd" runat="server">
                                                   <div class="widget-box">
                                                        <div class="widget-header widget-header-small header-color-dark">
                                                            <h5 class="modal-title">
                                                                <asp:Label ID="lblHeaderRq_Add" runat="server"></asp:Label>
                                                            </h5>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-body-inner">
                                                                <div class="widget-main">                                                                                
                                                                            <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                                                                                <tr>
                                                                                    <td class="span6" style="text-align: left">
                                                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                            <tr>
                                                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                                                    <asp:Label runat="server" ID="Label2" CssClass="red">Request Type</asp:Label>
                                                                
                                                                                                </td>
                                                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                                                    <asp:DropDownList runat="server" ID="ddlRequestType" Width="215px" ToolTip="Request Type"
                                                                                                                data-placeholder="Select Request Type" CssClass="chzn-select"  />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="span6" style="text-align: left">
                                                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                            <tr>
                                                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                                                    <asp:Label runat="server" ID="Label3" CssClass="red">Level No</asp:Label>
                                                                                                                              
                                                                                                </td>
                                                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                                                    <asp:TextBox ID="txtLevelNo" runat="server" Width="205px" onkeypress="return NumberOnly()"></asp:TextBox>                                                                
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>                                                            
                                                                                                
                                                                                <tr>
                                                                                    <td class="span6" style="text-align: left">
                                                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                            <tr>
                                                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                                                    <asp:Label runat="server" ID="Label1" CssClass="red">Centre(S)</asp:Label>
                                                                
                                                                                                </td>
                                                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                                                    <asp:ListBox runat="server" ID="ddlCentre_add"  data-placeholder="Select Centre(s)"
                                                                                                            CssClass="chzn-select" SelectionMode="Multiple"></asp:ListBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td class="span6" style="text-align: left">
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
                                                               </div>
                                                                <div class="widget-main alert-block alert-info" style="text-align: center;">
                                                                    <!--Button Area -->
                                                                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                                                                        Text="Save" ToolTip="Save" onclick="btnSave_Click" />
                                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                                                        Text="Save" ValidationGroup="UcValidate" Visible="false" 
                                                                        onclick="BtnSaveEdit_Click"/>
                                                                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClear_Add" Visible="true"
                                                                        runat="server" Text="Close" onclick="btnClear_Add_Click"/>
                                                               </div>
                                                           </div>
                                                        </div>
                                                    </div>
                        </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                                
                            </tr>                                  
                        </table> 

                        
                                                
            </div>

        </div>
     </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>

