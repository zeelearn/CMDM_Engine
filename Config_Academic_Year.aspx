<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Config_Academic_Year.aspx.cs" Inherits="Config_Academic_Year" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }



        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 45 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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
                <h4 class="blue">
                    Academic Year <span class="divider"></span>
                </h4>
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
                                                                <asp:Label runat="server" ID="Label12">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtSearchAcademicYear" MaxLength="10" 
                                                                    Width="205px"></asp:TextBox>
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
                                    OnClick="BtnSearch_Click" Text="Search" ToolTip="Search" />
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
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                     <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" />
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
                        <b>Academic Year Code</b> </th>
                        <th align="left">
                            Academic Year
                        </th>
                        <th align="center" style="width: 10%; text-align: center">
                            Status
                        </th>
                        <th style="width: 80px; text-align: center;">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>' />
                        </td>
                        <td style="text-align: center"> 

                        
                         <asp:Label CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>'
                             runat="server" ID="Label4" > 
                              <%#(int)DataBinder.Eval(Container.DataItem, "Is_Active") == 1 ? "Active" : "Inactive"%>                         
                            </asp:Label>


                            

                        </td>
                        <td style="width: 80px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"
                                CommandName="Edit" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" ItemStyle-BackColor="Silver" HorizontalAlign="Left"
                    CssClass="table table-striped table-bordered table-hover" runat="server" Width="100%" >
                    <HeaderTemplate>
                        <b>Academic Year Code</b> </th>
                        <th align="left">
                            Academic Year
                        </th>
                        <th align="left" style="width: 10%">
                        Status
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create New Academic Year" />
                    </h5>
                    <asp:Label ID="lblAcademicYearPKey_Hidden" runat="server" Text="" Visible="false" />
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
                                                            <asp:Label runat="server" ID="Label2" CssClass="red">Academic Year Code</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtAcademicYearCode" MaxLength="9" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label1" CssClass="red">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtAcademicYearDec" MaxLength="9" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label28">Is Active</asp:Label>
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
                                    </table>
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
                        &times;</button>
                    <h4 class="modal-title">
                        Delete Academic Year
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to delete academic year
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
