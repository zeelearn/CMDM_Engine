<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Master_Partner.aspx.cs" Inherits="Master_Partner" %>



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
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function NumberOnly1() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function CharacterOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 33 && AsciiValue <= 64) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 91 && AsciiValue <= 96) || (AsciiValue >= 123 && AsciiValue <= 126) || AsciiValue == 32)
                event.returnValue = false;
            else
                event.returnValue = true;
        } 
    </script>

  
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDOB.ClientID %>").datepicker();
            $("#<%=txtDOJ.ClientID %>").datepicker(); 
            });
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
                    Partner<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click"/>
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
                                                                <asp:Label runat="server" ID="Label12">Faculty Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtPartnerName" ToolTip="Faculty Name" type="text"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15" >Country</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCountry"  ToolTip="Country"
                                                                    data-placeholder="Select Country" Width="215px" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" >State</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label17" >City</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCity" Width="215px" ToolTip="Standard" data-placeholder="Select City"
                                                                    CssClass="chzn-select" AutoPostBack="False" />
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
                                                                <asp:Label ID="Label41" runat="server" >Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlDivision_Sr" runat="server" AutoPostBack="False" 
                                                                    CssClass="chzn-select" data-placeholder="Select City" ToolTip="Standard" 
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label34" runat="server">Hand Phone</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtHandPhone" runat="server" ToolTip="Hand Phone" type="text" 
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label40" runat="server">Active Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="False" 
                                                                    CssClass="chzn-select" data-placeholder="Select Status" ToolTip="Status" 
                                                                    Width="215px" />
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
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch" OnClick="BtnSearch_Click"
                                    Text="Search" ToolTip="Search" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
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
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Faculty Code</b> </th>
                        <th align="left" style="width: 15%;text-align: left;">
                           Faculty Name
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Employee No.
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Hand Phone
                        </th>
                        <th align="left" style="width: 15%;text-align: left;">
                            EMail Id
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Qualification
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                           State
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            City
                        </th>

                        <th align="left" style="width: 10%;text-align: left;">
                            Area
                        </th>
                        <th align="left" style="width: 10%;text-align: center;">
                            Status
                        </th>
                        <th style="width: 80px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="lblPartnerCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeNo")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Handphone1")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Qualification")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"State_Name")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label36" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"City_Name")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                        </td>
                        <td style="text-align:center">
                            <asp:Label  runat="server" ID="linkActive" CssClass='<%# Eval("Status").ToString().Trim() == "Active" ? "label label-success":"label label-warning"  %>' >
                            
                              <%#DataBinder.Eval(Container.DataItem, "Status")%>                         
                            </asp:Label>
                        </td>
                        <td style="width: 80px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' runat="server"
                                CommandName="Edit" />
                    </ItemTemplate>
                </asp:DataList>

                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false" >
                    <HeaderTemplate>
                        <b>Faculty Code</b> </th>
                        <th align="left" style="width: 10%;text-align: left;">
                           Faculty Name
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Employee No.
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Hand Phone
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            EMail Id
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Qualification
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                           State
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            City
                        </th>
                        <th align="left" style="width: 10%;text-align: left;">
                            Area
                        </th>
                        <th align="left" style="width: 10%;text-align: center;">
                            Status
                        </th>
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="lblPartnerCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeNo")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Handphone1")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Qualification")%>' />
                        </td>
                         <td style="text-align:left">
                            <asp:Label ID="Label36" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"State_Name")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label42" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"City_Name")%>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                        </td>
                        <td style="text-align:center">
                            <asp:Label Id="lblActive" runat="server" visible="True" Text='<%# Eval("Status").ToString().Trim() == "Active" ? "Active":"Inactive"  %>'/>
                        </td>
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create New Partner" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label23">First Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTitle_Add"  ToolTip="Title" data-placeholder="Select" Width="80px"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTitle_Add_SelectedIndexChanged"/>
                                                            <asp:DropDownList runat="server" ID="ddlTitle_Gender" Width="10px" Visible="false" />
                                                            <asp:TextBox runat="server" ID="txtFirstName_Add" ToolTip="First Name" type="text"
                                                               Width="120px" ValidationGroup="UcValidate" MaxLength="50" onkeypress="return CharacterOnly(event);"/>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidate" Display="None"
                                                                ControlToValidate="txtFirstName_Add" ErrorMessage="First Name can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special Characters not allowed in First Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtFirstName_Add" Display="None"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                            <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label10">Middle Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMiddleName_Add" ToolTip="Mid Name" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" onkeypress="return CharacterOnly(event);"/>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special Characters not allowed in Middle Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtMiddleName_Add" Display="None"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label19" class="red">Last Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtLastName_Add" ToolTip="Last Name" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" onkeypress="return CharacterOnly(event);"/>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Special Characters not allowed in Last Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtLastName_Add" Display="None"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
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
                                                            <asp:Label runat="server" class="red" ID="Label11">Hand Phone (1)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtHandPhone1_Add" ToolTip="Hand Phone (1)" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="10" onkeypress="return NumberOnly(event);"/>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UcValidate" Display="None"
                                                                ControlToValidate="txtHandPhone1_Add" ErrorMessage="Hand Phone (1) can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Special Characters not allowed in Hand Phone (1) !!"
                                                                ValidationExpression="([0-9]|[+]|[ ])*" ControlToValidate="txtHandPhone1_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label14">Hand Phone (2)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtHandPhone2_Add" ToolTip="Hand Phone (2)" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="10" onkeypress="return NumberOnly(event);" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Special Characters not allowed in Hand Phone (2) !!"
                                                                ValidationExpression="([0-9]|[+]|[ ])*" ControlToValidate="txtHandPhone2_Add" Display="None"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label26">Phone No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPhoneNo_Add" ToolTip="Phone No." type="text" Width="205px"
                                                                ValidationGroup="UcValidate" MaxLength="100" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                ErrorMessage="Special Characters not allowed in Phone Number !!" ValidationExpression="([0-9]|[+]|[ ])*"
                                                                ControlToValidate="txtPhoneNo_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
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
                                                            <asp:Label runat="server" ID="Label20">Email Id</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEmailId_Add" ToolTip="Email Id" type="text" Width="205px"
                                                                ValidationGroup="UcValidate" MaxLength="50" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label25">Gender</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlGender_Add" Width="215px" ToolTip="Gender"
                                                                data-placeholder="Select Gender" CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label30">Company</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCompany_Add"  ToolTip="Company" Width="215px"
                                                                data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblCompany_Add" Visible="False"></asp:Label>
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
                                                            <asp:Label runat="server" class="red" ID="Label3">Country</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCountry_Add" Width="215px" ToolTip="Country"
                                                                data-placeholder="Select Country" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label4">State</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlState_Add" Width="215px" ToolTip="State"
                                                                data-placeholder="Select State" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label5">City</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCity_Add" Width="215px" ToolTip="City" data-placeholder="Select City"
                                                                CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label1">Area Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtAreaName_Add" ToolTip="Area Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="205px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13">Street Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRoadName_Add" ToolTip="Street Name" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
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
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label2">Building Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtBuilding_Add" ToolTip="Building Name" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7">Flat No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRoomNo_Add" ToolTip="Flat No." type="text" Width="205px"
                                                                ValidationGroup="UcValidate" MaxLength="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label8">Pincode</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPincode_Add" ToolTip="Pincode" type="text" Width="205px"
                                                                ValidationGroup="UcValidate" MaxLength="6" onkeypress="return NumberOnly1(event);"/>
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
                                                            <i class="icon-calendar"></i>&nbsp;
                                                            <asp:Label runat="server" ID="Label29">Date of Birth</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span7 date-picker" id="txtDOB" runat="server" type="text"
                                                                        data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar red"></i>&nbsp;
                                                            <asp:Label runat="server" class="red" ID="Label31">Date of Joining</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span8 date-picker" id="txtDOJ" runat="server" type="text"
                                                                        data-date-format="dd M yyyy" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label32">Employee No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEmployeeNo_Add" ToolTip="Employee No." type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" onkeypress="return NumberOnly1(event);"/>
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
                                                            <asp:Label runat="server" ID="Label33">Qualification</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtQualification_Add" ToolTip="Qualification" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label27">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRemarks_Add" ToolTip="Remarks" type="text" Width="205px"
                                                                MaxLength="200" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label28">Active Partner</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                    checked="checked" />
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
                                                            <asp:Label runat="server" ID="Label6" >Account Number</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtAccountNumber_Add" ToolTip="Qualification" type="text"
                                                                Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label39" runat="server" >IFSC Code</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtIFSCCode_Add" runat="server" MaxLength="200" 
                                                                ToolTip="Remarks" type="text" Width="205px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label37" >Branch Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtBranchName_Add" runat="server" MaxLength="200" 
                                                                ToolTip="Remarks" type="text" Width="205px" />
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
                                                            <asp:Label runat="server" ID="Label38" >Pan Number</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPanNumber_Add" ToolTip="Qualification" type="text"
                                                               Width="205px" ValidationGroup="UcValidate" MaxLength="100" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                            <td class="span4" style="text-align: left">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <div class="span6">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Select Activity
                                                    </h5>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCapacity_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>Select</b> </th>
                                                                <th align="left" style="width: 90%">
                                                                Activity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDL_Select_Activity" runat="server" />
                                                                <span class="lbl"></span></td>
                                                                <td>
                                                                    <asp:Label ID="lblDL_Activity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_Name")%>' />
                                                                    <asp:Label ID="lblDL_Activity_Id" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_ID")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="span6">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Select Division
                                                    </h5>
                                                    <asp:CheckBox ID="chkCentreAllHidden_Sel" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCentre_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>Select</b></th>
                                                                <th align="left" style="width: 90%">
                                                                Division
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDL_Select_Centre" runat="server" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblDivisionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Code")%>'
                                                                    Visible="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                </ContentTemplate>
                               
                            </asp:UpdatePanel>
                        </div>
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
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;button>
                    <h4 class="modal-title">
                        Block Classroom
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to block classroom
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />. Do you want to Continue ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>

