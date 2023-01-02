<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SAP_Stream_Master_Edit.aspx.cs" Inherits="SAP_Stream_Master_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    function openModalDivOverride() {
        $('#divforexport').modal({
            backdrop: 'static'
        })

        $('#divforexport').modal('show');


    }

    function NumberandCharOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95)
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    function openModalDelete() {
        $('#DivDelete').modal({
            backdrop: 'static'
        })

        $('#DivDelete').modal('show');
    }
      

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                   Edit SAP Master<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" Visible="false" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="btnTopSearch" Text="Back" OnClick="btnTopSearch_Click" />
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
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Divsion </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:TextBox placeholder="Stream Code" Width="215px" ID="txtStreamCode" runat="server"
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
                                                                <asp:Label runat="server" ID="Label2">Stream Name </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Name" Width="215px" ID="txtStreamName" runat="server"
                                                                    MaxLength="1000" />
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
            
            <div id="DivAddPanel" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr runat="server" id="row1">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label1" CssClass="red">Divison </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="divisionname" Width="240px" ID="ddlDivisionedit" runat="server"/>
                                                         <asp:TextBox placeholder="divisioncodee" Width="240px" ID="Ddldivisioncode" runat="server" Visible="false"/>
                                                          <%--  <asp:DropDownList runat="server" ID="ddlDivisionedit" Width="240px" data-placeholder="ddlDivisionedit"
                                                                CssClass="chzn-select"   />
                                                              <asp:DropDownList runat="server" ID="Ddldivisioncode" Width="240px" data-placeholder="Ddldivisioncode"
                                                                CssClass="chzn-select" Visible="false"  />--%>
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
                                                        <asp:TextBox placeholder="acad year" Width="240px" ID="ddlAcadYearedit" runat="server"/>
                                                            <%--<asp:DropDownList runat="server" ID="ddlAcadYearedit" Width="240px" data-placeholder="Select Academic Year"
                                                                CssClass="chzn-select"  />--%>
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
                                                        <asp:TextBox placeholder="Course Name" Width="240px" ID="ddlClassRoomCourse" runat="server"
                                                                MaxLength="1000" />
                                                         <asp:TextBox placeholder="Course1 Name" Width="240px" ID="Ddlcoursecode" runat="server"
                                                                MaxLength="1000" Visible="false" />
                                                          <asp:TextBox placeholder="Course1 Name" Width="240px" ID="ddlstreamcode" runat="server"
                                                                 Visible="false" />
                                                           <%-- <asp:DropDownList runat="server" ID="ddlClassRoomCourse" Width="240px" data-placeholder="Select ClassRoom Course"
                                                                CssClass="chzn-select" />
                                                                 <asp:DropDownList runat="server" ID="Ddlcoursecode" Width="240px" data-placeholder="Select ClassRoom Course"
                                                                CssClass="chzn-select" Visible="false" />--%>
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
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Stream Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Product Name" Width="240px" ID="txtProductName" runat="server"
                                                                MaxLength="1000" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7" CssClass="red"> Stream Description</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Description" ID="txtDescription" Width="240px" runat="server"
                                                                MaxLength="1000" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left" >
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" Visible="false" ID="Label11" CssClass="red"> Fees Zone</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60;">
                                                        <asp:TextBox placeholder="Feezone" Width="240px" ID="ddlFeesZone" runat="server"
                                                                MaxLength="1000" Visible="false" />
                                                          <%--  <asp:DropDownList runat="server" Visible="false" ID="ddlFeesZone" Width="240px" data-placeholder="Select Fees Zone"
                                                                CssClass="chzn-select">--%>
                                                            
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
                                                            <input readonly="readonly" runat="server"
                                                               style="width:240px" id="txtCoursePeriod"  placeholder="Period" data-placement="bottom" data-original-title="Date Range" />
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
                                                            <input readonly="readonly" runat="server"
                                                               style="width:240px" id="txtAdmissionPeriod" placeholder="Period" data-placement="bottom" data-original-title="Date Range" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label15" CssClass="red">Center(s) </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                          <asp:ListBox runat="server" Enabled="false" ID="ddlCenter" data-placeholder="Select Center(s)" Width="240px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" ></asp:ListBox>
                                                             <asp:ListBox runat="server" ID="ddlcentercode" data-placeholder="Select Center(s)" Visible="false" Width="240px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true" ></asp:ListBox>
                                                              
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        <div id="divSubject" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="widget-header widget-header-small header-color-dark">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label14" runat="server"></asp:Label>
                                        <asp:Label ID="Label16" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <asp:DataList ID="dlSubjects" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                                AutoPostBack="true" Visible="True" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">
                                                                Subject Group Code
                                                            </th>
                                                            <th style="text-align: left;">
                                                            Subject Group Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="true" OnCheckedChanged="chkCheck_CheckedChanged" />
                                                            <span class="lbl"></span></td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblSubgrCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblSubgrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divsubjects" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="widget-header widget-header-small header-color-dark">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label22" runat="server"></asp:Label>
                                        <asp:Label ID="Label23" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <asp:DataList ID="dlSubjects1" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            <%-- <asp:CheckBox ID="chkAll" runat="server" Checked="True" Visible="true" Enabled="false" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">--%>
                                                            Subject Code </th>
                                                            <th style="text-align: left;">
                                                                Subject Name
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Subject Grup Code
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Subject Date
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Subject Price
                                                            </th>
                                                            <th style="text-align: left;">
                                                                CRF Value
                                                            </th>
                                                            <th style="text-align: left;">
                                                                CRF %
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Total Course Fee
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--       <span style=" width: 2%; text-align: left" >
                                                            <asp:CheckBox ID="chkCheck" runat="server" Checked="True" Visible="true" Enabled="false"  />
                                                            </span>
                                                            </td>
                                                            <td style="width: 8%; text-align: left">--%>
                                                            <asp:Label ID="lblSubCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lblSubName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left">
                                                                <asp:Label ID="lblsubgrup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subgrup")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:TextBox  runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                 Text='<%#DataBinder.Eval(Container.DataItem,"sdate")%>' Width="85%"
                                                                    id="txtsubjdate" placeholder="sdate" data-placement="bottom" data-original-title="Date Range"
                                                                   />
                                                            </td>
                                                            <td style="width: 15%; text-align: left;">
                                                                <asp:TextBox ID="txtprise" runat="server" OnTextChanged="txtprise_Changed" AutoPostBack="true"
                                                                    Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Prices")%>' Width="85%" />
                                                            </td>
                                                            <td style="width: 10%; text-align: center;">
                                                                <asp:TextBox ID="txtCRF" runat="server" OnTextChanged="txtCRF_Changed" AutoPostBack="true"
                                                                    Text='<%#DataBinder.Eval(Container.DataItem,"CRF")%>' Width="85%" onkeypress="return NumberOnly()" />
                                                            </td>
                                                            <td style="width: 10%; text-align: center;">
                                                                <asp:TextBox ID="TxtCRFvalue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF_Value")%>'
                                                                    Width="85%" onkeypress="return NumberOnly()" />
                                                            </td>
                                                            <td style="width: 15%; text-align: center;">
                                                                <asp:TextBox ID="txtTotal" runat="server" AutoPostBack="true" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Course_Fees")%>'
                                                                    Width="85%" onkeypress="return NumberOnly()" />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <%--<div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                                        <!--Button Area -->
                                                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                                                     
                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                                            Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                                          <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                    </div>--%>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divRegst" runat="server" visible="false">
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
                                                    <asp:DataList ID="dsregst" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" Visible="false" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">
                                                                Material Code
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Voucher Desc
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Voucher Type
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Sdate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Edate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Amount/%
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<span style=" width: 2%; text-align: left" >--%>
                                                            <asp:CheckBox ID="chkCheck" runat="server" />
                                                            <span class="lbl"></span></td>
                                                            <td style="width: 10%; text-align: left">
                                                                <asp:Label ID="lblRegcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                            </td>
                                                            <td style="width: 25%; text-align: left">
                                                                <asp:Label ID="lblVouchrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_name")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left">
                                                                <asp:Label ID="lalvoutype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                                                            </td>
                                                            <td style="width: 15%; text-align: left">
                                                                <input  class="span2.5 date-picker" id="txtPeriod1" runat="server"
                                                                  type="text"   />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:TextBox ID="Txtedate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Enddate")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: center;">
                                                                <asp:TextBox ID="txtAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'
                                                                    Width="85%" />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                                        <!--Button Area -->
                                                        <asp:Label runat="server" ID="Label26" Text="" ForeColor="Red" />
                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button1" runat="server"
                                                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveEdit_Click" />
                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button2" runat="server"
                                                            Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button3" Visible="true"
                                                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                    </div>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                      
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal fade" id="divforexport" style="left: 50% !important; top: 10% !important;
                    display: none; width: 40%" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Select Master
                                    <asp:Label ID="Lblpk" runat="server" Visible="false" />
                                </h4>
                                <asp:CheckBox ID="chkAllHidden" runat="server" Visible="False" />
                            </div>
                            <div class="modal-body">
                                <!--Controls Area -->
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 100%;" colspan="3">
                                            <asp:DataList ID="dladditem" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                Width="100%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" />
                                                    <span class="lbl"></span></th>
                                                    <th>
                                                        <b>Master File Name</b>
                                                    </th>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="True" />
                                                    <span class="lbl"></span></td>
                                                    <td>
                                                        <asp:Label ID="lblMastertable1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"name")%>' />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <!--Button Area -->
                                <asp:Label runat="server" ID="Label44" Text="" Visible="false" />
                                <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnadditem" ToolTip="OK"
                                    runat="server" Text="OK" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="Button5" ToolTip="Cancel" runat="server" Text="Cancel" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      
        
        
        
        
        <!--/row-->
    </div>
</asp:Content>

