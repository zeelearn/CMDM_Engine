<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Master_RFID_Device.aspx.cs" Inherits="Master_RFID_Device" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="#">Home</a><span class="divider"><i class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    RFID Device<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
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
                        <h5 >
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
                                                            <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label2">Device Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:TextBox ID="txtDeviceSearch" runat="server" Width="205px"></asp:TextBox>                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1">Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStatus" Width="215px" data-placeholder="Select Satus"
                                                                                CssClass="chzn-select" AutoPostBack="True" > 
                                                                    <asp:ListItem Value="0">All</asp:ListItem>                                                                   
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">InActive</asp:ListItem>
                                                                    
                                                                </asp:DropDownList>
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
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    </div>
                <asp:DataList ID="dlDisplay" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" onitemcommand="dlDisplay_ItemCommand"  >
                        <HeaderTemplate>
                            <b>Device ID</b> </th>
                            <th style="text-align: left">
                                Device Name
                            </th>
                            <th style="text-align: left">
                                 Status
                            </th>                            
                            <th style="text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblDeviceCode"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Device_Code")%>' />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblDeviceName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Device_Name")%>' />
                            </td>                            
                            <td class='hidden-480' style="text-align: left">
                                <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="lblStatus">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive"%>  
                                </asp:Label>
                            </td>
                            <td style="text-align: center">                                
                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign" data-rel="tooltip"
                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Device_Code")%>'
                                                ToolTip="Edit" data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>

                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="false"  >
                        <HeaderTemplate>
                            <b>Device ID</b> </th>
                            <th style="text-align: left">
                                Device Name
                            </th>
                            <th style="text-align: left">
                                 Status
                            </th>                            
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblDeviceCode"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Device_Code")%>' />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblDeviceName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Device_Name")%>' />
                            </td>                            
                            <td class='hidden-480' style="text-align: left">
                                <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'/>                            
                            </td>
                           
                        </ItemTemplate>
                    </asp:DataList>
                
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
                                        <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left; height: 42px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">Device Name</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtDeviceName" runat="server" Width="205px"></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                 <td class="span4" style="text-align: left; height: 42px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;" colspan="2">
                                                                <asp:Label runat="server" ID="Label13" >Status</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <label>
                                                                    <input runat="server" id="chkActive" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
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
                                                <td class="span12" style="text-align: left">
                                                    
                                                    <asp:DataList ID="dlGridCentreDiv"  CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%" >
                                                            <HeaderTemplate>

                                                               <b>
                                                                 <asp:CheckBox ID="chkAllCentre" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllCentre_CheckedChanged" />
                                                                  <span class="lbl"></span></b></th>

                                                                  <th align="left">
                                                                Division</th>
                        
                                                                <th align="left" >
                                                                    Centre                                                                
                                                                
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkCentre" runat="server" />
                                                                <span class="lbl"></span></td>
                                                                 <td style="text-align: left;">

                                                                    <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DivisionName")%>' />
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CentreName")%>' />
                                                                        <asp:Label ID="lblCentreCode" runat="server" visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Code")%>' />                                                               
                                                                
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                                    Text="Save" ToolTip="Save" OnClick="btnSave_Click" />
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                    Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
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

