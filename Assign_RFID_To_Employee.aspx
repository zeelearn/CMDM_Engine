<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Assign_RFID_To_Employee.aspx.cs" Inherits="Assign_RFID_To_Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                <h4 class="blue">
                    Assign RFID To Employee <span class="divider"></span>
                </h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            
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
                                                <td class="span12" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Employee Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 80%;">
                                                                <asp:DropDownList runat="server" ID="ddlEmployeeTYpe" Width="215px" data-placeholder="Select Employee Type"
                                                                    CssClass="chzn-select"  >

                                                                    <asp:ListItem  Text="Select" Value="0"></asp:ListItem>
                                                                    <asp:ListItem  Text="Corporate Office Employee" Value="1"></asp:ListItem>
                                                                    <asp:ListItem  Text="Center Employee" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
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
                                </td>
                                <td style="text-align: right" class="span2">
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" onitemcommand="dlGridDisplay_ItemCommand" >
                    <HeaderTemplate>
                        <b>User Code</b> </th>
                        <th align="left">
                            User Name
                        </th>
                        
                        <th style="text-align: center;width:250px">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUser_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblUser_Display_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"User_Display_Name")%>' />
                        </td>
                        
                        <td style=" text-align: center;">
                            <asp:Label ID="lblEmp_RFID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emp_RFID")%>' Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"ShowLabel")) == "1" ? true : false%>' />
                            
                            
                              <asp:LinkButton ID="lnkEditInfo" ToolTip="Assign RFID" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"User_Code")%>' runat="server"
                                CommandName="Assign" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"Showbutton")) == "1" ? true : false%>'/>

                                <asp:TextBox ID="txtEmp_RFID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"USER_NAME")%>'
                                 Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"ShowTextBox")) == "1" ? true : false %>' Width="142px"  MaxLength="7"/>

                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                        ValidChars="" TargetControlID="txtEmp_RFID" />
                                        &nbsp; <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                       <asp:Panel id ="icon" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel></a>




                    </ItemTemplate>
                </asp:DataList>
                
            

            <div class="widget-main alert-block alert-success  alert- " style="text-align: center;"
                                    runat="server" id="divBottom">
                                    <!--Button Area -->
                                    <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                                        Text="Save" onclick="BtnSave_Click"   />
                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnClose" Visible="true"
                                        runat="server" Text="Close" onclick="BtnClose_Click"  />
                                   
                                </div>
                                </div>
        </div>
        
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>
