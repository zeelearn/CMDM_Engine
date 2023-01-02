<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Config_Message_Setup.aspx.cs" Inherits="Config_Division" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">

   
    function NumberandCharOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57))
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    function CharOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 45 || AsciiValue == 32)
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
                <h4 class="blue">
                    Message Setup<span class="divider"></span></h4>
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
                                                    <table cellpadding="0" style="border-style: none; width: 122%;" 
                                                        class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 28%;">
                                                                <asp:Label runat="server" ID="Label2">Division Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:DropDownList ID="ddlDivision_SR" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Division Name" Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label17" runat="server">Message Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlMessageName_SR" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Division Name" Width="215px" />
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
                    </div>
                <asp:DataList ID="dlDivision" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlDivision_ItemCommand">
                        <HeaderTemplate>
                            <b>Message Name</b> </th>
                            <th style="text-align: left">
                                Division Name
                            </th>
                            <th style="text-align: left">
                                 Send Type
                            </th>
                            <th style="text-align: left">
                                 Message Type
                            </th>
                            <th style="text-align: left">
                                Message Template
                            </th>
                            <th style="text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblBoardCode"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Message_Name")%>' />
                            </td>
                            <td style="text-align: left;width:18%">
                                <asp:Label ID="lblBoardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Division_ShortDesc")%>' />
                            </td>
                            <td style="text-align: left;width:10%" >
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Send_Type")%>' />
                            </td>
                            <td class='hidden-480' style="width:10%">
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Message_Type")%>' />
                            </td>
                            <td class='hidden-480' style="text-align: left;width:44%">
                                <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Message_Template")%>' />
                            </td>
                            <td style="text-align: center">                                
                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign" data-rel="tooltip"
                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"pk")%>'
                                                ToolTip="Edit" data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                
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
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">Division</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlDivision_Add" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Division Name" Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label8" CssClass="red">Message Type</asp:Label>
                                                                                                                              
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlMessageType" runat="server" 
                                                                    CssClass="chzn-select" data-placeholder="Message Type" Width="215px" 
                                                                    AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlMessageType_SelectedIndexChanged" >
                                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                    <asp:ListItem>SMS</asp:ListItem>
                                                                    <asp:ListItem>Mail</asp:ListItem>
                                                                </asp:DropDownList>
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
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Message</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlMessage" runat="server" CssClass="chzn-select" 
                                                                    data-placeholder="Message" Width="215px" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlMessage_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label14" CssClass="red" >Sending Type</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlSendingType" runat="server" CssClass="chzn-select" 
                                                                    data-placeholder="Sending Type" Width="215px" >
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem>Auto</asp:ListItem>
                                                                    <asp:ListItem>Manual</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label15" runat="server">Keywords</asp:Label>
                                                </td>
                                                <td class="span4" style="text-align: left; width :830px">
                                                    <asp:Label ID="Label16" runat="server">Template</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                                    <asp:DataList ID="DataList2" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="94%" onitemcommand="DataList2_ItemCommand" >
                        <HeaderTemplate>
                            <b>Template Keyword</b> </th>
                            <th style="text-align: left">
                                Discription
                            </th>
                            <th style="text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Label ID="lblBoardCode"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Template_Keyword")%>' />

                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblBoardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"description")%>' />
                            </td>
                            <td style="text-align: center">                                
                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign" data-rel="tooltip"
                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Template_Keyword")%>'
                                                data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    </ContentTemplate> </asp:UpdatePanel> </td>
                                                <td class="span4" style="text-align: left">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                                    <%--<asp:TextBox ID="TextBox1" runat="server" Height="172px" Width="627px"></asp:TextBox>--%>
                                                    <textarea id="TextArea1" rows="2" runat ="server" class="autosize-transition span12"></textarea>

                                                    </ContentTemplate></asp:UpdatePanel>
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
        <asp:Label ID="lblslotid" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbldelCode" runat="server" Visible="false"></asp:Label>
        <!--/row-->
    </div>
</asp:Content>

