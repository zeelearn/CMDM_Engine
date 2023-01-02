<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Cloud_Post_Student_Details_LMS.aspx.cs" Inherits="Clous_Post_Student_Details_LMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">LMS CLoud Posting<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
                  <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click"/>
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
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="lblcontactstatus" runat="server" CssClass="red">Critera</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlcriteria" Width="14%" data-placeholder="Select"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                                <asp:Label ID="lblprimarykey" Visible="false" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click"  />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
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
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" ForeColor="White" runat="server" Visible="false"
                                        Text="Export"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" >
                        <HeaderTemplate>
                            <b>User Code </b></th>
                            <th>
                                SBEntryCode
                            </th>
                            <th>
                                SPID
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Product Code
                            </th>
                            <th>
                                Product Name
                            </th>
                            <th>
                                Subscrition Type
                            </th>
                            
                            <th>
                                Transaction Id
                            </th>

                            <th>
                                Date
                            </th>
                            
                            <th style="text-align: center">
                                Action
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblusercode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UserCode")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Sbentrycode")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SPID")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name")%>' />
                            </td>

                            <td>
                                <asp:Label ID="lblproductcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblproducname" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                                <asp:Label ID="lblproducttype" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ProductType")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblsubscritiontype" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SubscriptionType")%>' />
                                <asp:Label ID="lblplannane" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Plan_Name")%>' />
                            </td>
                            
                            <td>
                                <asp:Label ID="lbltransactionid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TransactionId")%>' />
                            </td>

                            <td>
                                <asp:Label ID="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>' />
                            </td>
                            
                            <td style="text-align: center">
                                  <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                    data-rel="tooltip" CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                    ToolTip="Post Data" data-placement="left"></asp:LinkButton>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    
                </div>
                <%--<table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label10">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivision_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label11">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            
                        </td>
                    </tr>
                </table>--%>
            </div>

</asp:Content>

