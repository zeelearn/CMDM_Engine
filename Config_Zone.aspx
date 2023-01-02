<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Config_Zone.aspx.cs" Inherits="Config_Zone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script type="text/javascript">

     function CharacterOnly() {
         var AsciiValue = event.keyCode
         if ((AsciiValue >= 33 && AsciiValue <= 64) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 91 && AsciiValue <= 96) || (AsciiValue >= 123 && AsciiValue <= 126))
             event.returnValue = false;
         else
             event.returnValue = true;
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
                <h4 class="blue">
                    Zone<span class="divider"></span></h4>
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
                                                                <asp:Label ID="Label7" runat="server" CssClass="red">Division</asp:Label>
&nbsp;</td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                               <asp:DropDownList runat="server" ID="ddlsearchdivision" Width="215px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"/> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label2">Zone Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtsearchzone" runat="server" Width="205px" onkeypress="return CharacterOnly(event);"></asp:TextBox>
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
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:DataList ID="ddlBoard" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="ddlBoard_ItemCommand" >
                        <HeaderTemplate>
                        <b>Zone Code</b>
                        </th>
                        <th>
                            Zone Name</th>
                            <th style="width: 30%; text-align: center">
                                Division
                            </th>
                            <th style="width: 10%; text-align: center">
                                Status
                            </th>
                            <th style="width: 10%; text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label ID="Label1"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Zone_Code")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBoardCode"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Zone_name")%>' />
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:Label ID="lblBoardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Division_ShortDesc")%>' />
                            </td>
                            <td class='hidden-480' style="width: 10%; text-align: left">
                                <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="linkActive">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                            <td style="width: 10%; text-align: center">                                
                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign" data-rel="tooltip"
                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Zone_Code")%>'
                                                ToolTip="Edit" data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="false" >
                        <HeaderTemplate><b>Zone Code</b>
                        </th>
                        <th>
                            Zone Name</th>
                            <th style="width: 30%; text-align: center">
                                Division
                            </th>
                            <th style="width: 10%; text-align: center">
                                Status
                            </th>
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label ID="Label1"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Zone_Code")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBoardCode"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Zone_name")%>' />
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:Label ID="lblBoardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Division_ShortDesc")%>' />
                            </td>
                            <td class='hidden-480' style="width: 10%; text-align: left">
                               <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'/>
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
                                                                <asp:Label runat="server" ID="lblzonename" CssClass="red">Zone Name</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtzonename" runat="server" Width="205px" onkeypress="return CharacterOnly(event);"></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label8" CssClass="red">Division</asp:Label>
                                                                                                                              
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                     />                                                                
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
                                                                <asp:Label runat="server" ID="Label6">Is Active</asp:Label>
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
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                                    Text="Save" ToolTip="Save" OnClick="btnSave_Click" />
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                    Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnClear_Add" Visible="true"
                                    runat="server" Text="Close" OnClick="btnClear_Add_Click" />
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

