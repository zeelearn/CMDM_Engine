<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Manage_User_HR.aspx.cs" Inherits="Manage_User_HR" %>

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
            if ((AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberandCharOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95 || (AsciiValue >= 48 && AsciiValue <= 57))
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
                <h4 class="blue">User<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="New" OnClick="BtnAdd_Click" />
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
                                                <td class="span4" style="text-align: left; width: 473px;">
                                                    <table cellpadding="0" style="border-style: none; width: 70%;"
                                                        class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12">User Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtUserName" ToolTip="Faculty Name" type="text"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left; width: 340px;">
                                                    <table cellpadding="0" class="table-hover"
                                                        style="border-style: none; width: 99%;">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 54%;">
                                                                <asp:Label ID="Label35" runat="server">Employee ID</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtUsercode" runat="server" ToolTip="Faculty Name"
                                                                    type="text" Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left"></td>
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>User Code</b> </th>
                        <th align="left" style="width: 15%; text-align: left;">RFID Card No
                        </th>
                        <th align="left" style="width: 15%; text-align: left;">User Display Name
                        </th>
                        <th align="left" style="width: 15%; text-align: left;">Department
                        </th>
                        <th align="left" style="width: 15%; text-align: left;">Designation
                        </th>
                        <th align="left" style="width: 15%; text-align: center;">Status
                        </th>
                        <th style="width: 80px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblusercode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RFID")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Department_Name")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Designation_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>' />


                        </td>
                        <td style="width: 80px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' runat="server"
                                CommandName="Edit" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>User Code</b> </th>
                        <th align="left" style="width: 20%; text-align: left;">RFID Card No
                        </th>
                        <th align="left" style="width: 20%; text-align: left;">User Display Name
                        </th>
                        <th align="left" style="width: 15%; text-align: left;">Department
                        </th>
                        <th align="left" style="width: 15%; text-align: left;">Designation
                        </th>
                        <th align="left" style="width: 15%; text-align: center;">Status
                        </th>

                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblusercode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RFID")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Department_Name")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Designation_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />


                        </td>

                    </ItemTemplate>
                </asp:DataList>

            </div>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server" Text="Create New User" />
                        </h5>
                        <asp:Label runat="server" ID="lblPkey" Visible="false"></asp:Label>
                        <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 100%;"
                                                        class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" class="red" ID="Label23">User Display Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtuser_Display_Name" runat="server"
                                                                    ToolTip="User Display Name" type="text" MaxLength="100"
                                                                    ValidationGroup="UcValidate" Width="205px" onkeypress="return NumberandCharOnly(event);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">Email Id</td>
                                                            <td style="border-style: none; text-align: left; width: 60%; margin-left: 40px;">
                                                                <asp:TextBox runat="server" ID="txtUserEmailId" ToolTip="Email ID" type="text"
                                                                    Width="205px" ValidationGroup="UcValidate" MaxLength="100" />

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19" class="red">Employee Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtUserEmpCode" ToolTip="Employee ID" type="text"
                                                                    Width="205px" ValidationGroup="UcValidate" MaxLength="7" onkeypress="return NumberandCharOnly(event);"
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
                                                                <asp:Label runat="server" ID="lbldob" class="red">DOB</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <input readonly="readonly" class="span2.5 date-picker" id="txtdob" runat="server"
                                                                    type="text" data-date-format="dd M yyyy" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="lblgender" class="red">Gender</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlgender" Width="220px" data-placeholder="Select Gender"
                                                                    CssClass="chzn-select" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">


                                                    <table cellpadding="0" style="border-style: none; width: 100%;"
                                                        class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="lblemprfid" class="red">EMP Rfid.</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtemprfid" ToolTip="EMP Rfid" type="text"
                                                                    Width="205px" ValidationGroup="UcValidate" MaxLength="6" />

                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 100%;"
                                                        class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label11" class="red">Mobile No.</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtHandPhone1_Add" ToolTip="Mobile No" type="text"
                                                                    Width="205px" ValidationGroup="UcValidate" MaxLength="10" onkeypress="return NumberOnly(event);" onCopy="return false" onDrag="return false" onDrop="return false" onPaste="return false" autocomplete="off" />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Special Characters not allowed in Mobile No Field  !!"
                                                                    ValidationExpression="([0-9]|[+]|[ ])*" ControlToValidate="txtHandPhone1_Add"
                                                                    ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;" class="span2">
                                                                <asp:Label runat="server" ID="Label24">Is Active</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;" class="span4">
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
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="lbldepartment" class="red">Department</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddldepartment" ToolTip="Department" data-placeholder="Select Department"
                                                                    Width="220px" CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label runat="server" ID="lbldesignation" class="red">Designation</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddldesignation_Add" ToolTip="Designation" data-placeholder="Select Designation"
                                                                    Width="220px" CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left"></td>
                                                <td class="span4" style="text-align: left">

                                                </td>
                                            </tr>

                                        </table>


                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
            </div>
            </div>
</asp:Content>

