<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Sap_Discount_Master.aspx.cs" Inherits="Sap_Discount_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    SAP Master<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <%--<asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add"  />--%>
          <%--  <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="btnTopSearch" Text="Search" 
                />--%>
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

            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Zcrs" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtCourse" runat="server" Width="205px" data-placeholder="Course"></asp:TextBox>
                                                                <%--<asp:DropDownList runat="server" ID="ddlServiceprovider1" Width="215px" ToolTip="Select"
                                                                    data-placeholder="Select" CssClass="chzn-select" AutoPostBack="True" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <asp:TextBox ID="txtbatch" runat="server" Width="205px" data-placeholder="batch"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Sub" CssClass="red">Subject Grp</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlSubgrp" Width="215px" ToolTip="Select"
                                                                    data-placeholder="Select" SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Condition Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="DDLPayplan" Width="215px" ToolTip="Select"
                                                                    data-placeholder="Select" CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label5" runat="server" CssClass="red">Discount Validity</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" style="width: 215px;"
                                                                    name="date-range-picker" id="txtValidity" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label2" runat="server" CssClass="red">Discount Value</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtdiscount" runat="server" Width="205px" data-placeholder="Plan"></asp:TextBox>
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
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Pay Plan</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="DDlplan" Width="215px" ToolTip="Select"
                                                                    data-placeholder="Select" CssClass="chzn-select" AutoPostBack="True" />
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
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" Visible="false" runat="server"
                                    ID="btnSave" Text="Save" ToolTip="Save" OnClick="btnSave_Click" />
                                   <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClear_Add" Visible="true"
                                    runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivDiscountHistory" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lbldiscounthistory_header" runat="server" Text="Discount History"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <p>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lbldlerror" runat="server" Text="" Visible="false" CssClass="red"></asp:Label>
                                </p>
                                <asp:DataList ID="DataList3" CssClass="table table-striped table-bordered table-hover"
                                    Visible="false" runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <b>Condition Type</b> </th>
                                        <th style="width: 15%; text-align: center">
                                             Material Type ZCRS
                                        </th>
                                        <th style="width: 15%; text-align: center">
                                            Course Batch
                                        </th>
                                        <th style="width: 15%; text-align: center">
                                            Subject Group
                                        </th>
               
                                        <th style="width: 15%; text-align: center">
                                            Pay Plan
                                        </th>
                                        <th style="width: 25%; text-align: center">
                                           Discount S/E Date
                                        </th>
                                        <th style="width: 15%; text-align: center">
                                            Discount Value
                                        </th>
                                                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="ddlCourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Condition_Type")%>' />
                                        </td>
                                        <td style="width: 15%; text-align: left">
                                            <asp:Label ID="Laabel3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_of_material_type_ZCRS")%>' />
                                        </td>
                                        <td style="width: 15%; text-align: left">
                                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Batch_Number")%>' />
                                        </td>
                                        <td style="width: 15%; text-align: left">
                                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_grup")%>' />
                                        </td>                         
                                        <td style="width: 15%; text-align: left">
                                            <asp:Label ID="Label48" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pay_Plan")%>' />
                                        </td>
                                        <td style="width: 30%; text-align: left">
                                            <asp:Label ID="Label49" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Period")%>' />
                                        </td>
                                        <td style="width: 15%; text-align: left">
                                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount_Value")%>' />
                                        </td>
     
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

</asp:Content>

