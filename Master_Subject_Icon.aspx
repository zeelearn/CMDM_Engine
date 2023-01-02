<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Master_Subject_Icon.aspx.cs" Inherits="Master_Subject_Icon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function openModalDelete() {
        $('#DivDelete').modal({
            backdrop: 'static'
        })

        $('#DivDelete').modal('show');
    };

    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 45 && AsciiValue <= 57))
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    function NumberandCharOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || AsciiValue == 32 || AsciiValue == 95 || AsciiValue == 45 || (AsciiValue >= 48 && AsciiValue <= 57))
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    </script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Subject Icon<span class="divider"></span></h4>
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
                                        Height="25px"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                               
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" OnSelectedIndexChanged="dlGridDisplay_SelectedIndexChanged">
                            <HeaderTemplate>
                            <b>Rec_No</b>
                            </th>
                            <th>
                                Subject Icon name </th>
                                <th align="left" style="width: 25%">
                                    Subject icon code
                                </th>
                                <th align="left" style="width: 22%">
                                    Subject Font
                                </th>
                       
                                <th style="width: 80px; text-align: center;">
                                Action
                                </th>
                                <th style="width: 30px; text-align: center; vertical-align: middle;">
                                
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label runat="server" ID="Label6" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Record_Number")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDLSubiconname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Name")%>'
                                    Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="75%" MaxLength="100" onkeypress="return NumberandCharOnly(event);"/>
                                <asp:Label ID="lblDLsubiconname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Name")%>'
                                    Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLsubicon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Code")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowChapterNameEditFlag") == 1 ? true : false%>' onkeypress="return NumberandCharOnly(event);" />
                                    <asp:Label ID="lblDLsubicon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Code")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />
                                    <asp:Label runat="server" ID="lblDLChapterCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Code")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLsubfont" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Font")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' Width="85%" Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"AllowChapterNameEditFlag") == 1 ? true : false%>'  />
                                    <asp:Label ID="lblDLsubfont" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Font")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' />                                    
                                </td>
  
                                <td style="text-align: center;">
                                    <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Record_Number")%>' runat="server"
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowEditButtonFlag") == 1 ? true : false%>' CommandName="Edit"
                                        Height="25px" />
                                    <asp:LinkButton ID="lnkDLSave" ToolTip="Save" class="btn-small btn-success icon-save"
                                        runat="server" CommandName="Save" Height="25px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Record_Number")%>'
                                        Visible='<%#(int)DataBinder.Eval(Container.DataItem,"ShowSaveButtonFlag") == 1 ? true : false%>' />
                                        </td>
                                        <td style="width: 30px; text-align: center; vertical-align: middle;">
                                    <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                    <asp:Panel id ="icon_Error" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel>
                                    </a>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                   
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" 
                    HorizontalAlign="Left"  CssClass="table table-striped table-bordered table-hover"
                     Width="100%">
                    <HeaderTemplate>
                    <b>Rec_No</b>
                    <th>
                        Subject Icon name</th>
                        <th  style="width: 20%;text-align:left">
                             Subject icon code
                        </th>
                        <th  style="width: 20%;text-align:left">
                            Subject Font
                        </th>
               
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:Label runat="server" ID="Label654" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Record_Number")%>'></asp:Label>
                    </td>
                           <td>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Name")%>' />
                        </td>
                        <td style="width: 20%;text-align:left" >
                             <asp:Label ID="lblCentre1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Icon_Code")%>' />
                        </td>
                        <td style="width: 20%;text-align:left" >
                             <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Font")%>' />
                        </td>
                   
                    </ItemTemplate>
                </asp:DataList>
                 
            </div>
        </div>
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>

