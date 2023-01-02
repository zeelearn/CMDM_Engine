<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OE_Configration.aspx.cs" Inherits="OE_Configration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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
                    OE Configuration<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
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
                                                                <asp:Label runat="server" ID="Label15" class="red">Config Name</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlConfigTable" Width="215px" ToolTip="Table"
                                                                    data-placeholder="Select Table" CssClass="chzn-select" AutoPostBack="True" />
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
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
               <asp:UpdatePanel ID="UpdatePanel_Add" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">                                    
                                       <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" Visible="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>      
                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr> 
                                <td class="span6" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label10">Config Name</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblConfigName_Result" class="blue"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>  
                                <td class="span6" style="text-align: left">
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
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" >
                            <HeaderTemplate>
                                <b> ID</b> </th> 
                                <th style="width: 50%; text-align: left;">
                                    Description
                                </th>                               
                                <th style="width: 12%; text-align: left; vertical-align: left;">
                                    Status                                
                                </th>
                                <th style="width: 12%; text-align: center;">
                                    Action
                                </th>
                                    <th style="width:12%; text-align: center; vertical-align: middle;">

                            </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:TextBox ID="txtDLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Enabled ="false"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="75%" MaxLength="100" />
                                    <asp:Label ID="lblDLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowEditFlag") == 1 ? true : false%>'  />
                                    <asp:Label ID="lblDLDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    
                                </td>
                          
                                <td style="width: 5%; text-align: left;">                                   
                                    <label>
                                        <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                        checked='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? true:false  %>' Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'/>
                                        <span class="lbl"></span>
                                    </label>                                    
                                    <asp:Label ID="lblDLStatus" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' />                                
                                </td>
                                <td style="text-align: center;">
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CommandName="Edit"
                                        Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 5%; text-align: center; vertical-align: middle;">
                                    <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                    <asp:Panel id ="icon_Error" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel>
                                    </a>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:DataList ID="dlGridDisplay1" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false" 
                            onitemcommand="dlGridDisplay1_ItemCommand" >
                            <HeaderTemplate>
                                <b> ID</b> </th>                                
                                <th style="width: 20%; text-align: left;">
                                    Short Description
                                </th> 
                                <th style="width: 30%; text-align: left;">
                                    Long Description
                                </th>                              
                                <th style="width: 12%; text-align: left; vertical-align: left;">
                                    Status                                
                                </th>
                                <th style="width: 12%; text-align: center;">
                                    Action
                                </th>
                                    <th style="width:12%; text-align: center; vertical-align: middle;">

                            </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:TextBox ID="txtDLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Enabled ="false"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="75%" MaxLength="100" />
                                    <asp:Label ID="lblDLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>                            
                                <td>
                                    <asp:TextBox ID="txtDLShortDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDesc")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowEditFlag") == 1 ? true : false%>'  />
                                    <asp:Label ID="lblDLShortDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDesc")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLLongDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LongDesc")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowEditFlag") == 1 ? true : false%>'  />
                                    <asp:Label ID="lblDLLongDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LongDesc")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    
                                </td>
                                <td style="width: 5%; text-align: left;">                                   
                                    <label>
                                        <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                        checked='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? true:false  %>' Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'/>
                                        <span class="lbl"></span>
                                    </label>                                    
                                    <asp:Label ID="lblDLStatus" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' />                                
                                </td>
                                <td style="text-align: center;">
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CommandName="Edit"
                                        Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 5%; text-align: center; vertical-align: middle;">
                                    <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                    <asp:Panel id ="icon_Error" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel>
                                    </a>
                            </ItemTemplate>
                        </asp:DataList>

                        <asp:DataList ID="dlGridDisplay2" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false" 
                            onitemcommand="dlGridDisplay2_ItemCommand" >
                            <HeaderTemplate>
                                <b> ID</b> </th>                                
                                <th style="width: 40%; text-align: left;">
                                    Description
                                </th> 
                                <th style="width: 12%; text-align: left;">
                                    Probability_Percent
                                </th>                              
                                <th style="width: 12%; text-align: left; vertical-align: left;">
                                    Status                                
                                </th>
                                <th style="width: 12%; text-align: center;">
                                    Action
                                </th>
                                    <th style="width:12%; text-align: center; vertical-align: middle;">

                            </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:TextBox ID="txtDLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Enabled ="false"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="75%" MaxLength="100" />
                                    <asp:Label ID="lblDLID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>                            
                                <td>
                                    <asp:TextBox ID="txtDLDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowEditFlag") == 1 ? true : false%>'  />
                                    <asp:Label ID="lblDLDesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLPerc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Probability_Percent")%>' onkeypress="return NumberOnly()"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowEditFlag") == 1 ? true : false%>'  MaxLength="3" />
                                    <asp:Label ID="lblDLPerc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Probability_Percent")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    
                                </td>
                                <td style="width: 5%; text-align: left;">                                   
                                    <label>
                                        <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                        checked='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? true:false  %>' Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>'/>
                                        <span class="lbl"></span>
                                    </label>                                    
                                    <asp:Label ID="lblDLStatus" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>' />                                
                                </td>
                                <td style="text-align: center;">
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CommandName="Edit"
                                        Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td style="width: 5%; text-align: center; vertical-align: middle;">
                                    <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                    <asp:Panel id ="icon_Error" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel>
                                    </a>
                            </ItemTemplate>
                        </asp:DataList>

                <asp:DataList ID="dlGridExport" Visible="false" runat="server" ItemStyle-BackColor="Silver"
                    HorizontalAlign="Left" HeaderStyle-BackColor="Gray" CssClass="table table-striped table-bordered table-hover"
                     Width="100%">
                    <HeaderTemplate>
                                <b> Topic Name</b> </th>                                
                                <th style="width: 15%; text-align: center;">
                                    DisplayName
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    Description
                                </th>
                                <th style="width: 30px; text-align: center; vertical-align: middle;">
                                    SequenceNo
                                </th>
                                <th style="width: 30px; text-align: center; vertical-align: middle;">
                                    Status
                                </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="lblTopic" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDisplayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_DisplayName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_Description")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblSeqNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Topic_SequenceNo")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>' />
                        
                    </ItemTemplate>
                </asp:DataList>

                </ContentTemplate>
                   <Triggers>
                       <asp:PostBackTrigger ControlID="dlGridDisplay" />
                       <asp:PostBackTrigger ControlID="dlGridDisplay1" />
                       <asp:PostBackTrigger ControlID="dlGridDisplay2" />
                   </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>

