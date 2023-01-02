<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeFile="Config_PaperCheckerRates.aspx.cs" Inherits="Config_PaperCheckerRates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.gridtext
{
    text-align:center !important;
}

</style>
<script type="text/javascript">


    function NumberandCommaOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || AsciiValue == 46)
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
                <h4 class="blue">
                    Paper Checker Rates<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label2" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1">Slab Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtSlab_Name_Sr" runat="server" data-placeholder="Payment Rate" 
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3">Active Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStatus" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" 
                                                                    >
                                                                    <asp:ListItem Value="2">All</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
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
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <asp:DataList ID="dlFaculty" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlFaculty_ItemCommand" >
                    <HeaderTemplate>
                        <b>Slab Name</b> </th>
                        
                        <th style="text-align: center">
                            Status
                        </th>
                        
                        <th style="text-align: center">
                            Action
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSlab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Name")%>' />
                        </td>
                        
                        <td style="text-align: center">
                            <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>' 
                             CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>'/>    
                        </td>
                        
                        <td style="text-align: center">
                            <div class="inline position-relative">               
                               
                                <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Slab_Code")%>' runat="server"
                                        CommandName="comEdit" Height="25px" />
                            </div>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Slab Name</b> </th>
                        
                        <th style="text-align: center">
                            Status
                        </th>
                       
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSlab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slab_Name")%>' />
                        </td>
                        
                        <td style="text-align: center">
                            <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>' 
                             CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>'/>    
                        </td>
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
                <div id="DivAddPanel" runat="server" visible="false">
                    <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Add Slab Details
                            <asp:Label ID="lblPkey" runat="server" Text="" Visible ="false" ></asp:Label>
                        </h5>
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
                                                        <asp:Label runat="server" ID="Label4" CssClass="red">Slab Name</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtSlab_Name_Add" runat="server" data-placeholder="Payment Rate" 
                                                                    Width="205px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                                    width="100%">
                                                                    <tr>
                                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">                                                                            
                                                                                <asp:Label runat="server" ID="Label35">Is Active</asp:Label>                                                                   
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
                                        <td class="span4" style="text-align: left">
                                            &nbsp;</td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="span4" style="text-align: left">
                                            &nbsp;</td>
                                        <td class="span4" style="text-align: left">
                                        </td>
                                        <td class="span4" style="text-align: left">
                                        </td>
                                    </tr>
                                </table>

                                <%--<asp:DataList ID="dlGridChapter" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <b>From Marks</b> </th>
                                        <th align="left" style="width: 30%">
                                            To Marks
                                        </th>
                                        <th style="width: 15%; text-align: center;">
                                            Rate/Paper
                                        </th>
                                        <th style="width: 20%; text-align: center;">
                                            Status
                                        </th>
                                        <th style="width: 20%; text-align: center;">
                                        Result
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"From_Marks")%>'
                                                Width="85%" onkeypress="return NumberandCharOnly(event);"/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"To_Marks")%>'
                                                Width="85%" onkeypress="return NumberandCharOnly(event);"/>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Rate")%>'
                                                Width="85%" onkeypress="return NumberandCharOnly(event);"/>
                                        </td>
                                        <td style="text-align: center;">
                                            <label>
                                        <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                        checked='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? true:false  %>' Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'/>
                                        <span class="lbl"></span>
                                    </label>                                    
                                    <asp:Label ID="lblDLStatus" runat="server" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>' />                                
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblResult" runat="server" Text="" />
                                    </ItemTemplate>
                                </asp:DataList>--%>
                                <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" 
                                    AutoGenerateColumns="false" 
                                    CssClass="table1 table-striped table-bordered table-hover" Width="100%">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" Visible="false"/>
            <asp:TemplateField HeaderText="From Marks">
                <ItemTemplate><center>
                    <asp:TextBox ID="TextBox1" runat="server" Width ="215px" onkeypress="return NumberandCommaOnly(event);" Text='<%#DataBinder.Eval(Container.DataItem,"Column1")%>'></asp:TextBox>
                    </center>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Marks">
                <ItemTemplate>
                <center>
                    <asp:TextBox ID="TextBox2" runat="server" Width ="215px" onkeypress="return NumberandCommaOnly(event);" Text='<%#DataBinder.Eval(Container.DataItem,"Column2")%>'></asp:TextBox>
                    </center>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rate/Paper">
                <ItemTemplate>
                <center>
                    <asp:TextBox ID="TextBox4" runat="server" Width ="215px" onkeypress="return NumberandCommaOnly(event);" Text='<%#DataBinder.Eval(Container.DataItem,"Column3")%>'></asp:TextBox>
                    </center>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                <center >
                      <label>
                         <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                         checked='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? true:false  %>' />
                         <span class="lbl"></span>
                      </label>
                      </center> 
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                       <asp:Label ID="lblResult" runat="server" Text="" />
                </ItemTemplate>
                
            </asp:TemplateField>
            </Columns>
        </asp:gridview>
                                
                                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;"
                                    runat="server" id="divBottom">
                                    <!--Button Area -->
                                    <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                                        Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveAdd_Click" />
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                        Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveEdit_Click" />
                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                        runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            
        </div>
        </div>
</asp:Content>
