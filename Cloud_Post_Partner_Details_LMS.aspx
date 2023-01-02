<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Cloud_Post_Partner_Details_LMS.aspx.cs" Inherits="Cloud_Post_Partner_Details_LMSaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>

            <li>
                <h4 class="blue">Partner Post LMS<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->

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
                                                                <asp:Label runat="server" ID="Label12">Faculty Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtPartnerName" ToolTip="Faculty Name" type="text"
                                                                    Width="205px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left"></td>
                                                <td class="span4" style="text-align: left"></td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Country</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCountry" ToolTip="Country"
                                                                    data-placeholder="Select Country" Width="215px" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
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
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
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
                                                                <asp:Label ID="Label41" runat="server">Division</asp:Label>
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
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" />
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
                                <td style="text-align: right" class="span2">&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Faculty Code</b> </th>
                        <th align="left" style="width: 15%; text-align: left;">Faculty Name
                        </th>
                        <th align="left" style="width: 10%; text-align: left;">Employee No.
                        </th>
                        <th align="left" style="width: 10%; text-align: left;">Hand Phone
                        </th>
                        <th align="left" style="width: 15%; text-align: left;">EMail Id
                        </th>
                        <th align="left" style="width: 10%; text-align: left;">Qualification
                        </th>
                        <th align="left" style="width: 10%; text-align: left;">Area
                        </th>
                        <th align="left" style="width: 10%; text-align: center;">Status
                        </th>

                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPartnerCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeNo")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Handphone1")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Qualification")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:Label runat="server" ID="linkActive" CssClass='<%# Eval("Status").ToString().Trim() == "Active" ? "label label-success":"label label-warning"  %>'>
                            
                              <%#DataBinder.Eval(Container.DataItem, "Status")%>                         
                            </asp:Label>
                    </ItemTemplate>
                </asp:DataList>

                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnpostlms" runat="server"
                        Text="Post LMS" ValidationGroup="UcValidate" Visible="true" OnClick="btnpostlms_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" Visible="true"
                        runat="server" Text="Close" OnClick="BtnClose_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                </div>
            </div>
</asp:Content>

