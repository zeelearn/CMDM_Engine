<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="ManageSubject.aspx.cs" Inherits="ManageSubject" %>


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
                <h4 class="blue">Manage Subject<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="btnuploadviexcel"
                width="150px" Text="Upload Via Excel" OnClick="btnuploadviexcel_Click"  />
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
                                                                <asp:DropDownList runat="server" ID="ddlDivisionName" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionName_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Course Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlsearchcoursename" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19"> Subject Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Subject Name" Width="205px" ID="txtsearchsubjectname" runat="server"
                                                                    MaxLength="1000" />
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
                    <asp:DataList ID="dlSubject" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlSubject_ItemCommand">
                        <HeaderTemplate>
                            <b>Subject Code</b> </th>
                            
                            
                            <th style="text-align: left">Subject Name
                            </th>
                            <th style="text-align: left">Subject Display Name
                            </th>
                            <th style="text-align: left">Course Name
                            </th>
                            <th style="text-align: center">Status
                            </th>
                            <th style="text-align: center">
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_code")%>' />
                            </td>                           
                           
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subject_name")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subject_display_name")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"course_name")%>' />
                            </td>
                            <td class='hidden-480' style="text-align: center">
                               
                                 <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' runat="server" ID="Label5">                                
                                <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive" %>  
                                </asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                    CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"record_number")%>'
                                    ToolTip="Edit" data-placement="left"></asp:LinkButton>

                                    <asp:LinkButton ID="lblCopy" runat="server" class="btn-small btn-primary icon-folder-close"
                                    CommandName='comCopy' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"CopySubject")%>'
                                    ToolTip="Copy" data-placement="left"   Visible= '<%#(int)DataBinder.Eval(Container.DataItem, "IsReference") == 1  && (string)DataBinder.Eval(Container.DataItem, "ChapterCount") == "0"  && (string)DataBinder.Eval(Container.DataItem, "TopicCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "SubTopicCount") == "0"  && (string)DataBinder.Eval(Container.DataItem, "ModuleCount") == "0" && (string)DataBinder.Eval(Container.DataItem, "LessonCount") == "0" && (int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1? true : false %> '   ></asp:LinkButton>


                            </td>
                        </ItemTemplate>
                    </asp:DataList>

                    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="false" 
                    onselectedindexchanged="DataList1_SelectedIndexChanged" >
                        <HeaderTemplate>
                            <b>Subject Code</b> </th>
                            
                            
                            <th style="text-align: left">Subject Name
                            </th>
                            <th style="text-align: left">Subject Display Name
                            </th>
                            <th style="text-align: left">Course Name
                            </th>
                            <th style="text-align: center">Status
                            </th>
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_code")%>' />
                            </td>                           
                           
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subject_name")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subject_display_name")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"course_name")%>' />
                            </td>
                            <td class='hidden-480' style="text-align: center">
                               <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'/>
                            </td>
                            
                        </ItemTemplate>
                    </asp:DataList>
                    
                
            </div>
            <div id="DivAddPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
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
                                                            <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                                CssClass="chzn-select" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlDivision_SelectedIndexChanged" />
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
                                        <tr runat="server" id="row2">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Course Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCourseName" Width="215px" data-placeholder="Select Course"
                                                                CssClass="chzn-select" OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged"  AutoPostBack="true"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7" CssClass="red"> Subject Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Subject Name" ID="txtSubjectName" Width="205px" runat="server"
                                                                MaxLength="100" onkeypress="return NumberandCharOnly(event);"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label11"> Is Reference</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width:60;">                                                            
                                                            <label >
                                                                <asp:CheckBox ID="chkreference" runat="server" OnCheckedChanged="chkreference_CheckedChanged"
                                                                    AutoPostBack="true" Visible="false"/>
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="row3" visible="false">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label12">Reference Course Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlReferencecoursename" Width="215px" data-placeholder="Select Reference Course"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlReferencecoursename_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13">Reference Subject Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlreferencesubjectname" Width="215px" data-placeholder="Select Reference Subject"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlreferencesubjectname_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label14" CssClass="red">Subject Display Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Subject Display Name" ID="txtSubjectDisplayName" runat="server"
                                                                MaxLength="200" Width="205px" onkeypress="return NumberandCharOnly(event);"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label15">Subject Sequence No</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Subject Sequence No" ID="txtSubjectSequenceNo" runat="server"
                                                                MaxLength="100" Width="205px" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"  />
                                                            <span id="error" style="color: Red; display: none">Enter Number</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label16">Is Active</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkActive" checked="checked" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2" />
                                                                <span class="lbl"></span>
                                                            </label>
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
                                                            <asp:Label runat="server" ID="Label17" >Subject Icon Name </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlSubiconname" Width="215px" data-placeholder="Select"
                                                                                CssClass="chzn-select" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlSubiconname_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                               <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label2" > Sub Icon Colour Code</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Select" Enabled="false" ID="TxtIcon" runat="server" />
                                                                <%--MaxLength="2000" Width="205px" onkeypress="return NumberandCharOnly(event);"--%>
                                                        </td>
                                                    </tr>
                                                </table> 
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label4">Subject Icon Font</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Icon Font" Enabled="false" ID="Txtfont" runat="server"/>
                                                               <%-- MaxLength="100" Width="205px" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"  />
                                                            <span id="Span1" style="color: Red; display: none">Enter Number</span>--%>
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
            <asp:Label ID="lblslotid" runat="server" Visible="false"></asp:Label>
            
            </div>
            <!--/row-->
            </div>
</asp:Content>
