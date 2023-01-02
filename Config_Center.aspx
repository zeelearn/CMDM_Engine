<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Config_Center.aspx.cs" Inherits="Config_Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
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
                <h4 class="blue">Manage Center<span class="divider"></span></h4>
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
                                                
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                             <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label14" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                                CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label2">Center Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:TextBox ID="txtsearchcentername" runat="server" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;"></td>
                                                            <td style="border-style: none; text-align: left; width: 60%;"></td>
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
                    <asp:DataList ID="dlcenter" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlcenter_ItemCommand" >
                        <HeaderTemplate>
                        <b>Center Code</b> </th>
                            <th style="width: 30%; text-align: center">Center Name
                            </th>
                            <th style="width: 20%; text-align: center">City
                            </th>
                            <th style="width: 20%; text-align: center">Location
                            </th>
                            <th style="width: 10%; text-align: center">Status
                            </th>
                            <th style="width: 10%; text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblBoardCode" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"source_center_code")%>' />
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:Label ID="lblBoardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"source_center_name")%>' />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"city_name")%>' />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"location_name")%>' />
                            </td>
                            <td class='hidden-480' style="width: 10%; text-align: left">
                                <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                            <td style="width: 10%; text-align: center">
                                <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign" data-rel="tooltip"
                                    CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"source_center_code")%>'
                                    ToolTip="Edit" data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlcenter_ItemCommand" Visible ="false" >
                        <HeaderTemplate>
                        <b>Center Code</b> </th>
                            <th style="width: 30%; text-align: center">Center Name
                            </th>
                            <th style="width: 20%; text-align: center">City
                            </th>
                            <th style="width: 20%; text-align: center">Location
                            </th>
                            <th style="width: 10%; text-align: center">Status
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblBoardCode" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"source_center_code")%>' />
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:Label ID="lblBoardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"source_center_name")%>' />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"city_name")%>' />
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"location_name")%>' />
                            </td>
                            <td class='hidden-480' style="width: 10%; text-align: left">
                                <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>' />
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
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Country</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCountry" Width="215px" ToolTip="Country"
                                                                    data-placeholder="Select Country" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" CssClass="red">State</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlState" Width="215px" ToolTip="State" data-placeholder="Select State"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">City</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCity" Width="215px" ToolTip="Standard" data-placeholder="Select City"
                                                                    CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Location</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddllocation" Width="215px" ToolTip="Country"
                                                                    data-placeholder="Select Location" CssClass="chzn-select" AutoPostBack="True"  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
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
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label41" runat="server" CssClass="red">Zone</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Zone" Width="215px" />
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
                                                                <asp:Label ID="Label40" runat="server" CssClass="red">Center Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtcentercode" runat="server" 
                                                                    onkeypress="return NumberOnly(event);" Width="205" data-placeholder="Center Code"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label39" runat="server" CssClass="red">Center Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtcentername" runat="server" 
                                                                     Width="205px" data-placeholder="Center Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label5" runat="server">Center short Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtcenshort" runat="server" 
                                                                   MaxLength="10" Width="205px" data-placeholder="Center Short Name"></asp:TextBox>
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
                                                                <asp:Label ID="Label9" runat="server" >Building</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtbuilding" runat="server"  Width="205" data-placeholder="Center Code"></asp:TextBox>
                                                             </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label10" runat="server" >Room</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtroom" runat="server"  Width="205px" data-placeholder="Center Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label11" runat="server">Floor</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtflr" runat="server"  Width="205px" data-placeholder="Center Short Name"></asp:TextBox>
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
                                                                <asp:Label ID="Label18" runat="server" CssClass="red">Address 1</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txadd1" runat="server" Width="205" data-placeholder="Address 1"></asp:TextBox>
                                                                                                                                  
                                                            </td>
                                                          
                                                        </tr>
                                                    </table>
                                                      
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label19" runat="server" >Address 2</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtadd2" runat="server" 
                                                                     Width="205px" data-placeholder="Address 2"></asp:TextBox>
                                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label20" runat="server">Address 3</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtadd3" runat="server" 
                                                                    Width="205px" data-placeholder="Address 3"></asp:TextBox>
                                                                    
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
                                                                <asp:Label ID="Label21" runat="server" CssClass="red">GSTIN NO</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="TXTGSTIN" runat="server" Width="205" data-placeholder="GSTIN NO"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                             <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label23" runat="server" CssClass="red">CIN NO</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="TXTCIN" runat="server" 
                                                                     Width="205px" data-placeholder="CIN NO"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                             <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label38" runat="server">Is Active</asp:Label>
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
                                               <%-- <td></td>
                                                <td></td>--%>

                                                
                                            </tr>
                                            
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div id="divtax" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="widget-header widget-header-small header-color-dark">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label24" runat="server"></asp:Label>
                                        <asp:Label ID="Label25" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <asp:DataList ID="dsTAX" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" Visible="false" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">
                                                                TAX Code
                                                            </th>
                                                            <th style="text-align: left;">
                                                                TAX Desc
                                                            </th>
                                                            <th style="text-align: left;">
                                                                TAX %
                                                            </th>
                                                            <th style="text-align: left;">
                                                               Validity Sdate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Edate
                                                            </th>
                                                            
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<span style=" width: 2%; text-align: left" >--%>
                                                            <asp:CheckBox ID="chkCheck" runat="server" />
                                                            <span class="lbl"></span></td>
                                                            <td style="width: 15%; text-align: left">
                                                                <asp:Label ID="lbltaxcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Taxcode")%>' />
                                                            </td>
                                                            <td style="width: 25%; text-align: left">
                                                                <asp:Label ID="lbltaxName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discription")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lbltaxvalue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TaxPercentage")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lalsadte" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SDate")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="laleadte" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EDate")%>' />
                                                            </td>
                                                         
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    
                                        <%--</td>--%>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>




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
            <div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="Label13" runat="server" Text="Edit Center Details"></asp:Label>
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
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label29" runat="server" CssClass="red">Country</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtCountry_edit" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>     
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label30" runat="server" CssClass="red">State</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtState_edit" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>   
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label31" runat="server" CssClass="red">City</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtCity_edit" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
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
                                                                <asp:Label ID="Label36" runat="server" CssClass="red">Location</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtLocation_edit" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>     
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label37" runat="server" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtDivision" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
                                                                <asp:TextBox ID="txtdivisioncode" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>   
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label45" runat="server" CssClass="red">Zone</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlZone_edit" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Zone" Width="215px" />
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
                                                                <asp:Label ID="Label44" runat="server" CssClass="red">Center Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txteditcentercode" runat="server" Enabled="false" 
                                                                    onkeypress="return NumberOnly(event);" Width="205"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label43" runat="server" CssClass="red">Center Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txteditcentername" runat="server" 
                                                                     Width="205px" data-placeholder="Center Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>    
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label6" runat="server">Center short Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtcenshrtname_edit" runat="server" 
                                                                    MaxLength="10" Width="205px" data-placeholder="Center Short Name"></asp:TextBox>
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
                                                                <asp:Label ID="Label33" runat="server" >Building</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditbuilding" runat="server" MaxLength="60" Width="205" data-placeholder="Center Code"></asp:TextBox>
                                                                 <asp:Label ID="Label34" runat="server" style="text-align: left;  width: 100%;"  CssClass="red">&nbsp;&nbsp;&nbsp;&nbsp; "Max Character Limit is 60 with space"</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label35" runat="server" >Room</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditroom" runat="server" MaxLength="20" Width="205px" data-placeholder="Center Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label46" runat="server">Floor</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditflr" runat="server" MaxLength="20" Width="205px" data-placeholder="Center Short Name"></asp:TextBox>
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
                                                                <asp:Label ID="Label47" runat="server" CssClass="red">Address 1</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditaddress1" runat="server" MaxLength="60" 
                                                                 Width="205" data-placeholder="Address 1"></asp:TextBox>
                                                                 <asp:Label ID="Label48" runat="server" style="text-align: left;  width: 100%;"  CssClass="red">&nbsp;&nbsp;&nbsp;&nbsp; "Max Character Limit is 60 with space"</asp:Label>
                                                                 
                                                            </td>
                                                          
                                                        </tr>
                                                    </table>
                                                      
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label49" runat="server" >Address 2</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditaddress2" runat="server" 
                                                                     Width="205px" data-placeholder="Address 2"></asp:TextBox>
                                                                      <asp:Label ID="Label50" runat="server" style="text-align: left;  width: 100%;"  CssClass="red">&nbsp;&nbsp;&nbsp;&nbsp; "Max Character Limit is 60 with space"</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label51" runat="server">Address 3</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditaddress3" runat="server" 
                                                                   MaxLength="10" Width="205px" data-placeholder="Address 3"></asp:TextBox>
                                                                    <asp:Label ID="Label52" runat="server" style="text-align: left;  width: 100%;"  CssClass="red">&nbsp;&nbsp;&nbsp;&nbsp; "Max Character Limit is 60 with space"</asp:Label>
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
                                                                <asp:Label ID="Label53" runat="server" CssClass="red">GSTIN NO</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditgstin" runat="server" Width="205" data-placeholder="GSTIN NO"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                             <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label54" runat="server" CssClass="red">CIN NO</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="Txteditcin" runat="server" 
                                                                     Width="205px" data-placeholder="CIN NO"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label42" runat="server">Is Active</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                <input runat="server" id="chactiveedit" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                                            checked="checked" />
                                                                <span class="lbl"></span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               
                                            </tr>
                                            
                                        </table>
                                        <div id="divtaxedit1" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="widget-header widget-header-small header-color-dark">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <asp:DataList ID="taxediet2" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" Visible="false" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">
                                                                TAX Code
                                                            </th>
                                                            <th style="text-align: left;">
                                                                TAX Desc
                                                            </th>
                                                            <th style="text-align: left;">
                                                                TAX %
                                                            </th>
                                                            <th style="text-align: left;">
                                                               Validity Sdate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Edate
                                                            </th>
                                                            
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<span style=" width: 2%; text-align: left" >--%>
                                                            <asp:CheckBox ID="chkCheck" runat="server" />
                                                            <span class="lbl"></span></td>
                                                            <td style="width: 15%; text-align: left">
                                                                <asp:Label ID="lbltaxcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Taxcode")%>' />
                                                            </td>
                                                            <td style="width: 25%; text-align: left">
                                                                <asp:Label ID="lbltaxName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discription")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lbltaxvalue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TaxPercentage")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lalsadte" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SDate")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="laleadte" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EDate")%>' />
                                                            </td>
                                                         
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    
                                        <%--</td>--%>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>

                                    </ContentTemplate>


                                </asp:UpdatePanel>
                                

                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                    Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClear_Edit" Visible="true"
                                    runat="server" Text="Close" OnClick="btnClear_Edit_Click"  />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblslotid" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbldelCode" runat="server" Visible="false"></asp:Label>
        <!--/row-->
    </div>
</asp:Content>


