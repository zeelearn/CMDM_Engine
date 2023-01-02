<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Manage_Password.aspx.cs" Inherits="Manage_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function openModalDelete() {
             $('#DivDelete').modal({
                 backdrop: 'static'
             })

             $('#DivDelete').modal('show');
         };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Change Password<span class="divider"></span></h4>
            </li>
        </ul>
        
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Change Password
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
                                                    
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none; width: 141%;" class="table-hover" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <fieldset>
                                                                Old Password:
                                                        <label>
                                                            <span class="block input-icon input-icon-right">
                                                            <asp:TextBox ID="txtoldPassword" runat="server" CssClass="span12" TextMode="Password"></asp:TextBox>
                                                                <i class="icon-lock"></i></span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="White" ControlToValidate="txtoldPassword"  runat="server"
                                                            ValidationGroup="uservalidation" SetFocusOnError="True" ErrorMessage="Enter Old Password" />
                                                        </label>

                                                        New Password:
                                                        <label>
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox ID="txtNewPassword1" runat="server" CssClass="span12" TextMode="Password"></asp:TextBox>
                                                                <i class="icon-lock"></i></span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="White" ControlToValidate="txtNewPassword1"  runat="server"
                                                            ValidationGroup="uservalidation" SetFocusOnError="True" ErrorMessage="Enter New Password" />
                                                        </label>

                                                        Retype Password:
                                                        <label>
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox ID="txtNewPassword2" runat="server" CssClass="span12" TextMode="Password"></asp:TextBox>
                                                                <i class="icon-lock"></i></span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="White" ControlToValidate="txtNewPassword2"  runat="server"
                                                            ValidationGroup="uservalidation" SetFocusOnError="True" ErrorMessage="Retype Password" />
                                                        </label>
                                                        
                                                    </fieldset>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnSubmit"
                                    Text="Submit" ToolTip="Search" ValidationGroup="UcValidateSearch" onclick="btnSubmit_Click" 
                                    />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClose" Visible="true"
                                    runat="server" Text="Close" onclick="btnClose_Click"  />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate> 
            </asp:UpdatePanel> 
            
        </div>
        
    </div>
    <!--/row-->
    
    <!--/#page-content-->
</asp:Content>

