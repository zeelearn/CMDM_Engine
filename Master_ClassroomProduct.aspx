<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="Master_ClassroomProduct.aspx.cs" Inherits="Master_ClassroomProduct" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 45 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95)
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
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">Class Room Products<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="btnTopSearch" Text="Search" OnClick="btnTopSearch_Click" />
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
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Divsion </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19"> Stream Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Code" Width="205px" ID="txtStreamCode" runat="server"
                                                                    MaxLength="1000" />
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
                                                                <asp:Label runat="server" ID="Label2" >Stream Name </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Name" Width="205px" ID="txtStreamName" runat="server"
                                                                    MaxLength="1000" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStatus" Width="215px" data-placeholder="Select Status"
                                                                    CssClass="chzn-select" >
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                                                </asp:DropDownList>
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
                                <td class="span10">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="btnExport_Click"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div></div>
                    <asp:DataList ID="dlClassRoomProduct" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" 
                    onitemcommand="dlClassRoomProduct_ItemCommand">
                        <HeaderTemplate>
                            <b>Stream Code</b> </th>
                            
                            
                            <th style="text-align: left">Stream Name
                            </th>
                            <th style="text-align: left">Course Period
                            </th>
                            <th style="text-align: left">Admission Period
                            </th>
                            <th style="text-align: center">Status
                            </th>
                            <th style="text-align: center">Centre Count
                            </th>
                            <th style="text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStreamCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Code")%>' />
                            </td>                           
                           
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Name")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Period")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Admission_Period")%>' />
                            </td>
                            <td class='hidden-480' style="text-align: center">
                               
                                 <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="Label5">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive"%>  
                                </asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Count")%>' />
                            </td>
                            <td style="text-align: center">
                                <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                    CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                    ToolTip="Edit" data-placement="left"></asp:LinkButton>

                                <asp:LinkButton ID="lblSubGroup" runat="server" class="btn-small btn-primary icon-folder-close"
                                    CommandName='comSubGroup' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                    ToolTip="Subject Group" data-placement="left" ></asp:LinkButton>

                                <asp:LinkButton ID="lblPricingItem" runat="server" class="btn-small btn-primary icon-leaf"
                                    CommandName='comPricingItem' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                    ToolTip="Pricing Item" data-placement="left"></asp:LinkButton>
                                    
                                <asp:LinkButton ID="lblPricingHeader" runat="server" class="btn-small btn-primary icon-comment"
                                    CommandName='comPricingHeader' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                    ToolTip="Pricing Header" data-placement="left"></asp:LinkButton>

                            </td>
                        </ItemTemplate>
                    </asp:DataList>

                    <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="false" >
                        <HeaderTemplate>                            
                            <b>Stream Code</b> </th>                           
                            
                            <th style="text-align: left">Stream Name
                            </th>
                            <th style="text-align: left">Course Period
                            </th>
                            <th style="text-align: left">Admission Period
                            </th>
                            <th style="text-align: center">Status
                            </th>
                            <th style="text-align: center">Centre Count                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_code")%>' />
                            </td>                           
                           
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_name")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Period")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Admission_Period")%>' />
                            </td>
                            <td class='hidden-480' style="text-align: center">
                               
                                 <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="Label5">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Count")%>' />
                            </td>                            
                        </ItemTemplate>
                    </asp:DataList>
                    
                
            </div>
            <div id="DivAddPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                            <asp:Label ID="lblPKey" runat="server" Visible="false"></asp:Label>
                        </h5>
                    </div>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr runat="server" id="row1">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label1" CssClass="red">Divison </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlDivisionAdd" Width="215px" data-placeholder="Select Division"
                                                                                CssClass="chzn-select" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlDivisionAdd_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                     <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label20" CssClass="red">Academic Year </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlAcadYearAdd" Width="215px" data-placeholder="Select Academic Year"
                                                                                CssClass="chzn-select" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label21" CssClass="red">Classroom Course </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlClassRoomCourse" Width="215px" data-placeholder="Select ClassRoom Course"
                                                                                CssClass="chzn-select" AutoPostBack="True"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="row2">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Product Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Product Name" Width="205px" ID="txtProductName" runat="server"
                                                                    MaxLength="1000" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7" > Description</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Description" ID="txtDescription" Width="205px" runat="server"
                                                                MaxLength="1000" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label11" CssClass="red"> Fees Zone</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width:60;">                                                            
                                                            <asp:DropDownList runat="server" ID="ddlFeesZone" Width="215px" data-placeholder="Select Fees Zone"
                                                                                CssClass="chzn-select" AutoPostBack="True">
                                                                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem Value="R1">R1</asp:ListItem>
                                                                    <asp:ListItem Value="R2">R2</asp:ListItem>
                                                                    <asp:ListItem Value="E1">E1</asp:ListItem>
                                                                    <asp:ListItem Value="E2">E2</asp:ListItem>
                                                                    <asp:ListItem Value="E3">E3</asp:ListItem>
                                                                    <asp:ListItem Value="E4">E4</asp:ListItem>
                                                                    <asp:ListItem Value="E5">E5</asp:ListItem>
                                                                    <asp:ListItem Value="E6">E6</asp:ListItem>
                                                                    <asp:ListItem Value="E7">E7</asp:ListItem>
                                                                    <asp:ListItem Value="E8">E8</asp:ListItem>
                                                                    <asp:ListItem Value="E9">E9</asp:ListItem>
                                                                    <asp:ListItem Value="E!">E!</asp:ListItem>
                                                                    <asp:ListItem Value="E@">E@</asp:ListItem>
                                                                    <asp:ListItem Value="E#">E#</asp:ListItem>
                                                                    <asp:ListItem Value="E$">E$</asp:ListItem>
                                                                    <asp:ListItem Value="E%">E%</asp:ListItem>
                                                                    <asp:ListItem Value="E^">E^</asp:ListItem>
                                                                    <asp:ListItem Value="E&">E&</asp:ListItem>
                                                                    <asp:ListItem Value="E*">E*</asp:ListItem>
                                                                    <asp:ListItem Value="E(">E(</asp:ListItem>
                                                                    <asp:ListItem Value="E)">E)</asp:ListItem>
                                                                    <asp:ListItem Value="E-">E-</asp:ListItem>
                                                                    <asp:ListItem Value="E+">E+</asp:ListItem>
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
                                                            <asp:Label runat="server" ID="Label12" CssClass="red">Course Period</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                             <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                                                id="txtCoursePeriod" placeholder="Period" data-placement="bottom"
                                                                                                data-original-title="Date Range" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13" CssClass="red">Admission Period</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                                                id="txtAdmissionPeriod" placeholder="Period" data-placement="bottom"
                                                                                                data-original-title="Date Range" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                             <td>
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label22">Product Code</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">                                                            
                                                            <asp:TextBox placeholder="Product Code" ID="txtProductCode" runat="server"
                                                                MaxLength="200" Width="205px" Enabled="false"/>
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
                                                            <asp:Label runat="server" ID="Label14" >Allow DP in Two Installment Receipts</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">  
                                                            <asp:CheckBox ID="chkAllowDP" runat="server" OnCheckedChanged="chkAllowDP_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" />
                                                                <span class="lbl"></span></th>                                                         
                                                            <%--<label>
                                                                <input runat="server" id="chkAllowDP"  name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                    runat ="server" checked="false" AutoPostBack="true" onchange="AllowDpCheckedChange" />
                                                                <span class="lbl"></span>
                                                            </label>   --%>                                                          
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;" runat="server" id="lblDate">
                                                            <asp:Label runat="server" ID="lblDate1" CssClass="red">Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;" runat="server" id="txtDPDate1">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span8 date-picker" id="txtDPDate" runat="server" type="text"
                                                                        data-date-format="dd M yyyy" />
                                                                </span>


                                                            </div>                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label16" >Set Limit On Maximum Cheque Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                           <asp:CheckBox ID="chkMaxChequeDate" runat="server" OnCheckedChanged="chkMaxChequeDate_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" />
                                                                <span class="lbl"></span></th> 

                                                            <%--<label>
                                                                <input runat="server" id="chkMaxChequeDate" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                    runat ="server" checked="false" />
                                                                <span class="lbl"></span>
                                                            </label> --%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%" >
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;" runat="server" id="lblCheckdate">
                                                            <asp:Label runat="server" ID="lblCheckdate1" CssClass="red">Date</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;" runat="server" id="txtMaxChequeDate1">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span8 date-picker" id="txtMaxChequeDate" runat="server" type="text"
                                                                        data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label24" >Maximum No Of Receipts in Full D.P.Mode </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Max No of Receipts" ID="txtMaxNoOfReceipts" runat="server"
                                                                MaxLength="200" Width="205px" onkeypress="return NumberOnly()"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label25" >Maximum No Of Days Between Two Cheques in Full DP Mode </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Max No of Days Between Two Cheques in Full DP Mode" ID="txtMaxNoOfDays" runat="server"
                                                                MaxLength="200" Width="205px" onkeypress="return NumberOnly()"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label15" CssClass="red" >Center(s) </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:ListBox runat="server" ID="ddlCenter"  data-placeholder="Select Center(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>                                                         
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>        
                <div class="widget-main alert-block alert-info" style="text-align: center;">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                        Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveAdd_Click" />
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server" Visible="false"
                        Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveEdit_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                        runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                </div>
            </div>
            
            <div id="DivSubjectGroup" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left" class="span10">
                                    Subject Group              
                                                                                     
                                </td>
                                <td style="text-align: right" class="span2">                                                    
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSubGrValiditySDate" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblSubGrValidityEDate" runat="server" Text=""></asp:Label> 
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                             <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                
                                                                <asp:Label runat="server" ID="Label26">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                
                                                                <asp:Label runat="server" ID="lblSubGroupDivision_Result" Text="MUM-SCI-ENG" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label27">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblSubGroupAcadYear_Result" Text="2014-2015" CssClass="blue" />
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
                                                                <asp:Label runat="server" ID="Label44">Stream Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblSubGroupStreamCode_Result" runat="server" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label28">Stream Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">      
                                                                
                                                                <asp:Label runat="server" ID="lblSubGroupStreamName_Result" CssClass="blue"></asp:Label>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                            </tr>                                            
                                        </table>

                                        <table width="100%">
                                            <tr>                                                
                                                <td style="text-align: right" class="span12">   
                                                    <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnAddSubjectGroup"
                                                        Text="Add" onclick="btnAddSubjectGroup_Click"/>                                                   
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        
                                        <div id="DivResultSubjectGroup" runat="server" class="row-fluid">
                                             <asp:DataList ID="dlSubjectGroup" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%" onitemcommand="dlSubjectGroup_ItemCommand">
                                                    <HeaderTemplate>
                                                        <b>Subject Group Code</b> </th>   
                            
                                                        <th style="text-align: left">Subject Group Name
                                                        </th>
                                                        <th style="text-align: left">Fees
                                                        </th>
                                                        <th style="text-align: left">Unit Of Measurement
                                                        </th>
                                                        <th style="text-align: left">Min Order Qty
                                                        </th>                                                       
                                                        <th style="text-align: center">
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubjectGroupCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectGroup_code")%>' />
                                                        </td>                           
                           
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectGroup_name")%>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Fees")%>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UOM")%>' />
                                                        </td>                                                        
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MinOrderQty")%>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                                                ToolTip="Edit" data-placement="left"></asp:LinkButton>                                                           
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                
                                        </div>

                                        
                                         <div id="DivAddSubjectGroup" runat="server" class="row-fluid">
                                             
                                                <div class="widget-box">
                                                    <div class="widget-header widget-header-small header-color-dark">
                                                        <h5 class="modal-title">
                                                            <asp:Label ID="lblSubGroupHeader_Add" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPKeySubGroup" runat="server" Visible="false"></asp:Label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-body-inner">
                                                        <div class="widget-main">                                                            
                                                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                                        <tr>
                                                                            <td class="span12" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label23" CssClass="red">Select Subject Group </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlSubjectGroupAdd" Width="215px" data-placeholder="Select Subject Group"
                                                                                                    CssClass="chzn-select" AutoPostBack="True" >                                                                                                
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            
                                                                        </tr>   
                                                                        <tr>
                                                                            <td class="span12" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label31" CssClass="red">Select Subjects </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DataList ID="dlSubjects" CssClass="table table-striped table-bordered table-hover"
                                                                                                runat="server" Width="100%">
                                                                                                <HeaderTemplate>
                                                                                                   <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                                                                            AutoPostBack="true" Visible="True" />
                                                                                                        <span class="lbl"></span></th>
                                                                                                    <th style="text-align: left">Subject Code
                                                                                                    </th>
                                                                                                    <th style="text-align: left">Subject Name                                                                                                   
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkCheck" runat="server"
                                                                                                        Checked="false" />
                                                                                                        <span class="lbl"></span>  
                                                                                                    </td>                                          
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblSubCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                                                                    </td>
                                                                                                    <td style="text-align: left">
                                                                                                        <asp:Label ID="lblSubName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                                                                    </td>                                                                                                    
                                                                                                </ItemTemplate>
                                                                                            </asp:DataList>

                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            
                                                                        </tr>                                                                        
                                                                    </table>
                                                                     <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                                        <tr>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label29" CssClass="red">Fees </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:TextBox placeholder="Fees" ID="txtFees" Width="205px" runat="server"
                                                                                                    MaxLength="1000" onkeypress="return NumberOnly()"/>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label30" CssClass="red">Unit Of Measurement </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlUOM" Width="215px" data-placeholder="Select Unit Of Measurement"
                                                                                                    CssClass="chzn-select" AutoPostBack="True" >
                                                                                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label32">Min Order Qty. </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:TextBox placeholder="Minimum Order Quantity" ID="txtMinOrderQty" Width="205px" runat="server"
                                                                                                    MaxLength="1000" onkeypress="return NumberOnly()"/>
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
                                                                                            <asp:Label runat="server" ID="Label40" CssClass="red">Validity Period</asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                                                id="txtValidityPeriod" placeholder="Period" data-placement="bottom"
                                                                                                data-original-title="Date Range"/>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                     </table>                                                               
                                                        </div>
                                                    </div>
                                                </div>        
                                                <div class="widget-main alert-block alert-info" style="text-align: center;">
                                                    <!--Button Area -->
                                                    <asp:Label runat="server" ID="Label47" Text="" ForeColor="Red" />
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                                                        ID="btnSubjectGroup_Save" runat="server"
                                                        Text="Save" ValidationGroup="UcValidate" 
                                                        onclick="btnSubjectGroup_Save_Click"/>
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                                                        ID="btnSubjectGroup_SaveEdit" runat="server" Visible="false"
                                                        Text="Save" ValidationGroup="UcValidate" 
                                                        onclick="btnSubjectGroup_SaveEdit_Click"/>
                                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                                                        ID="btnSubjectGroup_Close" Visible="true"
                                                        runat="server" Text="Close" onclick="btnSubjectGroup_Close_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                </div>
                                         
                                         </div>

                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>                                 
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: right;">
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                                                                ID="btnClose_SubGroupToResult" Visible="true"
                                                                runat="server" Text="Close" onclick="btnClose_SubGroupToResult_Click" />    
                        </div>
                    </div>
                </div>
            </div>
            
            <div id="DivItemLevelPricing" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left" class="span10">
                                    Item Level Pricing                                                                
                                </td>
                                <td style="text-align: right" class="span2">                                                   
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                             <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                
                                                                <asp:Label runat="server" ID="Label33">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                
                                                                <asp:Label runat="server" ID="lblItemLevelDivision_Result"  CssClass="blue" />
                                                                <asp:Label runat="server" ID="lblItemLevelDivisionCode_Result" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label35">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblItemLevelAcadYear_Result" Text="2014-2015" CssClass="blue" />
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
                                                                <asp:Label runat="server" ID="Label37">Stream Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblItemLevelStreamCode_Result" runat="server" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label39">Stream Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">      
                                                                
                                                                <asp:Label runat="server" ID="lblItemLevelStreamName_Result" CssClass="blue"></asp:Label>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                            </tr>                                            
                                        </table>

                                        <table width="100%">
                                            <tr>                                                
                                                <td style="text-align: right" class="span12">   
                                                    <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnAddItemLevelPric"
                                                        Text="Add" onclick="btnAddItemLevelPric_Click"/>                                                    
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <div id="DivResultItemLevel" runat="server" class="row-fluid">
                                             <asp:DataList ID="dlItemLevelPric" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%" onitemcommand="dlItemLevelPric_ItemCommand">
                                                    <HeaderTemplate>
                                                        <b>Subject Group Code</b> </th>   
                            
                                                        <th style="text-align: left">Subject Group 
                                                        </th>
                                                        <th style="text-align: left">Voucher Type Name
                                                        </th>
                                                        <th style="text-align: left">Voucher Type Code
                                                        </th>
                                                        <th style="text-align: left">Pay Plan
                                                        </th>
                                                        <th style="text-align: left">Voucher Amount
                                                        </th>                                                       
                                                        <th style="text-align: left">Validity Period
                                                        </th>
                                                        <th style="text-align: center">
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubjectGroupCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectGroup_code")%>' />
                                                        </td>                           
                           
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectGroup_name")%>' />
                                                        </td>
                                                         <td style="text-align: left">
                                                            <asp:Label ID="Label41" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Description")%>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_typeCode")%>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pay_Plan")%>' />
                                                        </td>                                                        
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Amount")%>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label34" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_Period")%>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                                                ToolTip="Edit" data-placement="left"></asp:LinkButton>                                                           
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>

                                        </div>

                                         <div id="DivAddItemLevel" runat="server" class="row-fluid">
                                            
                                                <div class="widget-box">
                                                    <div class="widget-header widget-header-small header-color-dark">
                                                        <h5 class="modal-title">
                                                            <asp:Label ID="lblItemLevelHeader_Add" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPKeyItemLevel" runat="server" Visible="false"></asp:Label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-body-inner">
                                                        <div class="widget-main">                                                           
                                                                    
                                                                     <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                                        <tr>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label45" CssClass="red">Subject Group </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlSubjectGroup" Width="215px" data-placeholder="Select Subject Group"
                                                                                                    CssClass="chzn-select" AutoPostBack="True" >                                                                                                
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label46" CssClass="red">Voucher Type </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlVoucherType" Width="215px" data-placeholder="Select Voucher Type"
                                                                                                    CssClass="chzn-select" AutoPostBack="True" >
                                                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                             </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label48" CssClass="red">Pay Plan </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlPayPlan" Width="215px" data-placeholder="Select Pay Plan"
                                                                                                    CssClass="chzn-select" AutoPostBack="True">
                                                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
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
                                                                                            <asp:Label runat="server" ID="Label36" CssClass="red">Voucher Amount </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:TextBox placeholder="Voucher Amount" ID="txtVoucherAmount" Width="205px" runat="server"
                                                                                                    MaxLength="1000" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label38" CssClass="red">Validity Period </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                                                id="txtItemLevelPeriod" placeholder="Period" data-placement="bottom"
                                                                                                data-original-title="Date Range" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                     </table>                                                               
                                                        </div>
                                                    </div>
                                                </div>        
                                                <div class="widget-main alert-block alert-info" style="text-align: center;">
                                                    <!--Button Area -->
                                                    <asp:Label runat="server" ID="Label49" Text="" ForeColor="Red" />
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                                                        ID="btnSaveItemLevel" runat="server"
                                                        Text="Save" ValidationGroup="UcValidate" onclick="btnSaveItemLevel_Click"/>
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                                                        ID="btnSaveEditItemLevel" runat="server" Visible="false"
                                                        Text="Save" ValidationGroup="UcValidate" 
                                                        onclick="btnSaveEditItemLevel_Click"/>
                                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                                                        ID="btnCloseItemLevel" Visible="true"
                                                        runat="server" Text="Close" onclick="btnCloseItemLevel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                </div>
                                         
            
                                         </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: right;">
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                                                ID="btnClose_ItemLevetToResult" Visible="true"
                                                runat="server" Text="Close" onclick="btnClose_ItemLevetToResult_Click"  />
                        </div>
                    </div>
                </div>
            </div>
            
            <div id="DivPricingHeader" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left" class="span10">
                                    Pricing Header                                                              
                                </td>
                                <td style="text-align: right" class="span2">                                                   
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                             <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                
                                                                <asp:Label runat="server" ID="Label42">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                
                                                                <asp:Label runat="server" ID="lblItemHeaderDivision_Result"  CssClass="blue" />
                                                                <asp:Label runat="server" ID="lblItemHeaderDivisionCode_Result" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label51">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblItemHeaderAcadYear_Result" Text="2014-2015" CssClass="blue" />
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
                                                                <asp:Label runat="server" ID="Label53">Stream Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblItemHeaderStreamCode_Result" runat="server" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label55">Stream Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">      
                                                                
                                                                <asp:Label runat="server" ID="lblItemHeaderStreamName_Result" CssClass="blue"></asp:Label>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                            </tr>                                            
                                        </table>

                                        <table width="100%">
                                            <tr>                                                
                                                <td style="text-align: right" class="span12">   
                                                    <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnAddItemHeaderPric"
                                                        Text="Add" onclick="btnAddItemHeaderPric_Click" />                                                    
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <div id="DivResultItemHeader" runat="server" class="row-fluid">
                                             <asp:DataList ID="dlItemHeaderPric" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%" 
                                                 onitemcommand="dlItemHeaderPric_ItemCommand" >
                                                    <HeaderTemplate>
                                                        <b>Voucher Type Code</b> </th>                                                                                       
                                                        <th style="text-align: left">Voucher Type Name
                                                        </th>    
                                                        <th style="text-align: left">Voucher Amount
                                                        </th>                                                       
                                                        <th style="text-align: left">Validity Period
                                                        </th>
                                                        <th style="text-align: center">
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVoucherTypeCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Typ")%>' />
                                                        </td>           
                                                         <td style="text-align: left">
                                                            <asp:Label ID="Label41" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Description")%>' />
                                                        </td>                           
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Amount")%>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label34" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_Period")%>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                                                ToolTip="Edit" data-placement="left"></asp:LinkButton>                                                           
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>

                                        </div>

                                         <div id="DivAddItemHeader" runat="server" class="row-fluid">
                                            
                                                <div class="widget-box">
                                                    <div class="widget-header widget-header-small header-color-dark">
                                                        <h5 class="modal-title">
                                                            <asp:Label ID="lblItemHeader_Add" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPKeyItemHeader" runat="server" Visible="false"></asp:Label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-body-inner">
                                                        <div class="widget-main">                                                           
                                                                    
                                                                     <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                                        <tr>
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label60" CssClass="red">Voucher Type </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlItemHeaderVoucherType" Width="215px" data-placeholder="Select Voucher Type"
                                                                                                    CssClass="chzn-select" AutoPostBack="True" >
                                                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                             </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>    
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label63" CssClass="red">Validity Period </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                                                id="txtItemHeaderValidityPeriod" placeholder="Period" data-placement="bottom"
                                                                                                data-original-title="Date Range" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>     
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label62" CssClass="red">Voucher Amount </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:TextBox placeholder="Voucher Amount" ID="txtItemHeaderVoucherAmount" Width="205px" runat="server"
                                                                                                    MaxLength="1000" />
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
                                                                                            <asp:Label runat="server" ID="Label43" CssClass="red"> Unit Of Measurement </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:DropDownList runat="server" ID="ddlItemPricingHeaderUOM" Width="215px" data-placeholder="Select UOM"
                                                                                                    CssClass="chzn-select" AutoPostBack="True" >
                                                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                             </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>    
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label50" >Min. Order Quantity </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:TextBox placeholder="Min. Order Quantity" ID="txtItemHeaderMinOrdQty" Width="205px" runat="server"
                                                                                                    MaxLength="1000" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>     
                                                                            <td class="span4" style="text-align: left">
                                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                                    <tr>
                                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                                            <asp:Label runat="server" ID="Label52" CssClass="red">Material ROB </asp:Label>
                                                                                        </td>
                                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                                            <asp:TextBox placeholder="Material ROB" ID="txtMaterialROB" Width="205px" runat="server"
                                                                                                    MaxLength="1000" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>                                                                       
                                                                        </tr>
                                                                     </table>                                                               
                                                        </div>
                                                    </div>
                                                </div>        
                                                <div class="widget-main alert-block alert-info" style="text-align: center;">
                                                    <!--Button Area -->
                                                    <asp:Label runat="server" ID="Label64" Text="" ForeColor="Red" />
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                                                        ID="btnSaveItemHeader" runat="server"
                                                        Text="Save" ValidationGroup="UcValidate" 
                                                        onclick="btnSaveItemHeader_Click" />
                                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                                                        ID="btnSaveEditItemHeader" runat="server" Visible="false"
                                                        Text="Save" ValidationGroup="UcValidate" 
                                                        onclick="btnSaveEditItemHeader_Click" />
                                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                                                        ID="btnCloseItemHeader" Visible="true"
                                                        runat="server" Text="Close" onclick="btnCloseItemHeader_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                </div>
                                         
            
                                         </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>                                
                            </div>
                        </div>
                        <div class="widget-main alert-block alert-success  alert- " style="text-align: right;">
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" 
                                                ID="btnClose_PricingHeaderToResult" Visible="true"
                                                runat="server" Text="Close" 
                                                onclick="btnClose_PricingHeaderToResult_Click" /> 
                        </div>
                    </div>
                </div>
            </div>
            

            </div>
            <!--/row-->
            </div>
</asp:Content>
