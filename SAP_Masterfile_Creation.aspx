<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    EnableEventValidation="FALSE" CodeFile="SAP_Masterfile_Creation.aspx.cs" Inherits="SAP_Masterfile_Creation" %>

<script runat="server">


  
</script>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
</asp:Content>--%>
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

        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }
      

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
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
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="btnTopSearch" Text="Search" OnClick="btnTopSearch_Click" />
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
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Divsion </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division Name"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19"> Stream Code</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Code" Width="215px" ID="txtStreamCode" runat="server"
                                                                    MaxLength="1000" />
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
                                                                <asp:Label runat="server" ID="Label2">Stream Name </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Stream Name" Width="215px" ID="txtStreamName" runat="server"
                                                                    MaxLength="1000" />
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
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <%--<td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlClassRoomProduct" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlClassRoomProduct_ItemCommand">
                    <HeaderTemplate>
                        <b>Stream Code</b> </th>
                        <th style="text-align: left">
                            Stream Name
                        </th>
                        <th style="text-align: left">
                            Course Period
                        </th>
                        <th style="text-align: left">
                            Admission Period
                        </th>
                        <%--           <th style="text-align: center">
                            Centre Count
                        </th>--%>
                        <th style="text-align: center">
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStreamCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Code")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Name")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Period")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Admission_Period")%>' />
                        </td>
                        <td style="text-align: center">
                            <asp:LinkButton ID="lblEdit" runat="server" class="btn-small btn-primary icon-info-sign"
                                CommandName='comDiscount' ForeColor="White" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                ToolTip="Add Discount" data-placement="left"></asp:LinkButton>
                            <asp:LinkButton ID="lblSubGroup" runat="server" class="btn-small btn-primary icon-download-alt"
                                CommandName='comSubGroup' ForeColor="White" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                ToolTip="Download To Execl" data-placement="left"></asp:LinkButton>
                            <asp:LinkButton ID="Lbledite" runat="server" class="btn-small btn-primary icon-folder-open"
                                CommandName='comaddcenter' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                ToolTip="Add Center" data-placement="left"></asp:LinkButton>
                            <asp:LinkButton ID="lbleditstream" runat="server" class="btn-small btn-primary icon-edit"
                                CommandName='Comeditstream' ForeColor="White" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                ToolTip="Edit Stream" data-placement="left"></asp:LinkButton>
                            <asp:LinkButton ID="Comview" runat="server" class="btn-small btn-primary icon-eye-open"
                                CommandName='comviewclick' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                ToolTip="View" data-placement="left"></asp:LinkButton>
                            <asp:LinkButton ID="comdelete" runat="server" class="btn-small btn-danger   icon-ban-circle"
                                CommandName='comdeleteclick' ForeColor="White" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'
                                ToolTip="Delete" data-placement="left"></asp:LinkButton>
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Material Type</b> </th>
                        <th style="text-align: left">
                            Serial #
                        </th>
                        <th style="text-align: left">
                            Short Description (BD1)
                        </th>
                        <th style="text-align: left">
                            Material Code
                        </th>
                        <th style="text-align: center">
                            Link
                        </th>
                        <th style="text-align: center">
                            Base UOM
                        </th>
                        <th style="text-align: center">
                            Center
                        </th>
                        <th style="text-align: center">
                            Material Group
                        </th>
                        <th style="text-align: center">
                            General Item Category Group
                        </th>
                        <th style="text-align: center">
                            Tax relevance Indicator
                        </th>
                        <th style="text-align: center">
                            Account Assignment Group
                        </th>
                        <th style="text-align: center">
                            Item Category Group
                        </th>
                        <th style="text-align: center">
                            Material Group 1
                        </th>
                        <th style="text-align: center">
                            Material Statistics Group
                        </th>
                        <th style="text-align: center">
                            Batch Management
                        </th>
                        <th style="text-align: center">
                            Profit Center
                        </th>
                        <th style="text-align: center">
                        Batch No
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Type")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Serial_No")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Short_Description")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Link")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Base_UOM")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Group")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label28" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"General_tem_Category_Group")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label29" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Tax_Relevance_Indicator")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label30" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Account_Assignment_Group")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label31" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Item_Category_Group")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label32" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Group_1")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Statistics_Group")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label34" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Management")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Profit_Center")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label36" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_No")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Material Code of material type ZCRS</b> </th>
                        <th style="text-align: left">
                            Course Description
                        </th>
                        <th style="text-align: left">
                            Batch
                        </th>
                        <th style="text-align: left">
                            Center
                        </th>
                        <th style="text-align: center">
                            Course Start Date
                        </th>
                        <th style="text-align: center">
                            Course End Date
                        </th>
                        <th style="text-align: center">
                            Stream Description
                        </th>
                        <th style="text-align: center">
                            Stream Long Description
                        </th>
                        <th style="text-align: center">
                            Admission Start Date
                        </th>
                        <th style="text-align: center">
                            Admission End Date
                        </th>
                        <th style="text-align: center">
                            Academic Year Start Date
                        </th>
                        <th style="text-align: center">
                            Academic Year End Date
                        </th>
                        <th style="text-align: center">
                            Academic Year String
                        </th>
                        <th style="text-align: center">
                            Material of material type ZSUB
                        </th>
                        <th style="text-align: center">
                            Subject Description
                        </th>
                        <th style="text-align: center">
                            Subject Start Date
                        </th>
                        <th style="text-align: center">
                        Subject End Date
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code_ZCRS")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Desc")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Crs_Start_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label37" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Crs_End_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label38" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Description")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label39" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Stream_Long_Description")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label40" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Adm_Start_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label41" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Adm_End_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label42" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year_S_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label43" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year_E_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label45" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label46" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_type_ZSUB")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label47" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Description")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label48" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Start_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label49" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_End_Date")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport2" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Material of material type ZCRS</b> </th>
                        <th style="text-align: left">
                            Batch Desc
                        </th>
                        <th style="text-align: left">
                            Center
                        </th>
                        <th style="text-align: left">
                            Batch
                        </th>
                        <th style="text-align: center">
                            Material of material type ZSGR
                        </th>
                        <th style="text-align: center">
                            Material of material type ZSUB
                        </th>
                        <th style="text-align: center">
                            Voucher Type (ZP01)
                        </th>
                        <th style="text-align: center">
                            Subject Prices
                        </th>
                        <th style="text-align: center">
                            CRF %
                        </th>
                        <th style="text-align: center">
                            CRF Value
                        </th>
                        <th style="text-align: center">
                        Total Course Fees
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_of_material_type_ZCRS")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Desc")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_of_material_type_ZSGR")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label50" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_of_material_type_ZSUB")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label51" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label52" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Prices")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label53" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label54" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF_Value")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label55" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Course_Fees")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport3" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Voucher Type </b></th>
                        <th style="text-align: left">
                            Division Code
                        </th>
                        <th style="text-align: left">
                            Material for Registration fee
                        </th>
                        <th style="text-align: left">
                            Material of material type ZCRS
                        </th>
                        <th style="text-align: center">
                            Batch
                        </th>
                        <th style="text-align: center">
                            Validity Start Date (dd.mm.yyyy)
                        </th>
                        <th style="text-align: center">
                            Validity End Date (dd.mm.yyyy)
                        </th>
                        <th style="text-align: center">
                            Registration Fee
                        </th>
                        <th style="text-align: center">
                        Plan Code
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Code")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_for_registration")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_type_ZCRS")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label56" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_Start_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label57" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_End_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label58" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Registration_Fee")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label59" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PlanCode")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport4" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Voucher / Condition Type</b> </th>
                        <th style="text-align: left">
                            Material of material type ZCRS
                        </th>
                        <th style="text-align: left">
                            Course Batch Number
                        </th>
                        <th style="text-align: left">
                            Subject Group
                        </th>
                        <th style="text-align: center">
                            Customer Group
                        </th>
                        <th style="text-align: center">
                            Pay Plan
                        </th>
                        <th style="text-align: center">
                            Discount Validity Start Date (dd.mm.yyyy)
                        </th>
                        <th style="text-align: center">
                            Discount Validity End Date (dd.mm.yyyy)
                        </th>
                        <th style="text-align: center">
                            Discount Value
                        </th>
                        <th style="text-align: center">
                        Plan Code
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Condition_Type")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_of_material_type_ZCRS")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Batch_Number")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Customer_Group")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pay_Plan")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label60" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount_Validity_Start_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label61" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount_Validity_End_Date")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label62" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount_Value")%>' />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label63" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Plan_Code")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivAddPanel" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">
                                    <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                                    <asp:Label ID="lblPKey" runat="server" Visible="false"></asp:Label>
                                </h5>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main">
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr runat="server" id="row1">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label1" CssClass="red">Divison </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlDivisionAdd" Width="240px" data-placeholder="Select Division"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionAdd_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label20" CssClass="red">Academic Year </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlAcadYearAdd" Width="240px" data-placeholder="Select Academic Year"
                                                                CssClass="chzn-select" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label21" CssClass="red">Classroom Course </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlClassRoomCourse" Width="240px" data-placeholder="Select ClassRoom Course"
                                                                CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlClassRoomCourse_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="row2">
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6" CssClass="red">Stream Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Product Name" Width="240px" ID="txtProductName" runat="server"
                                                                MaxLength="1000" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7" CssClass="red"> Stream Description</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox placeholder="Description" ID="txtDescription" Width="240px" runat="server"
                                                                MaxLength="1000" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label11" CssClass="red"> Fees Zone</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60;">
                                                            <asp:DropDownList runat="server" ID="ddlFeesZone" Width="240px" data-placeholder="Select Fees Zone"
                                                                CssClass="chzn-select" AutoPostBack="True">
                                                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                <asp:ListItem Value="R1">R1</asp:ListItem>
                                                                <asp:ListItem Value="E1">E1</asp:ListItem>
                                                                <asp:ListItem Value="E2">E2</asp:ListItem>
                                                                <asp:ListItem Value="E3">E3</asp:ListItem>
                                                                <asp:ListItem Value="E4">E4</asp:ListItem>
                                                                <asp:ListItem Value="E5">E5</asp:ListItem>
                                                                <asp:ListItem Value="E6">E6</asp:ListItem>
                                                                <asp:ListItem Value="E7">E7</asp:ListItem>
                                                                <asp:ListItem Value="E8">E8</asp:ListItem>
                                                                <asp:ListItem Value="E9">E9</asp:ListItem>
                                                                <asp:ListItem Value="E!">E!</asp:ListItem>
                                                                <asp:ListItem Value="E@">E@</asp:ListItem>
                                                                <asp:ListItem Value="E#">E#</asp:ListItem>
                                                                <asp:ListItem Value="E$">E$</asp:ListItem>
                                                                <asp:ListItem Value="E%">E%</asp:ListItem>
                                                                <asp:ListItem Value="E^">E^</asp:ListItem>
                                                                <asp:ListItem Value="E&">E&</asp:ListItem>
                                                                <asp:ListItem Value="E*">E*</asp:ListItem>
                                                                <asp:ListItem Value="E(">E(</asp:ListItem>
                                                                <asp:ListItem Value="E)">E)</asp:ListItem>
                                                                <asp:ListItem Value="E-">E-</asp:ListItem>
                                                                <asp:ListItem Value="E+">E+</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                            <asp:Label runat="server" ID="Label12" CssClass="red">Course Period</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                width="240px" id="txtCoursePeriod" placeholder="Period" data-placement="bottom"
                                                                data-original-title="Date Range" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13" CssClass="red">Admission Period</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                width="240px" id="txtAdmissionPeriod" placeholder="Period" data-placement="bottom"
                                                                data-original-title="Date Range" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label15" CssClass="red">Center(s) </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:ListBox runat="server" ID="ddlCenter" data-placeholder="Select Center(s)" Width="240px"
                                                                CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="divSubject" runat="server" visible="false">
                                <div class="widget-box">
                                    <div class="widget-header widget-header-small header-color-dark">
                                        <h5 class="modal-title">
                                            <asp:Label ID="Label14" runat="server"></asp:Label>
                                            <asp:Label ID="Label16" runat="server" Visible="true"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div class="widget-body-inner">
                                        <div class="widget-main">
                                            <td class="span12" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <asp:DataList ID="dlSubjects" CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" />
                                                                <span class="lbl"></span></th>
                                                                <th style="text-align: left;">
                                                                    Subject Group Code
                                                                </th>
                                                                <th style="text-align: left;">
                                                                Subject Group Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="true" OnCheckedChanged="chkCheck_CheckedChanged" />
                                                                <span class="lbl"></span></td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblSubgrCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="lblSubgrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                            </td>
                                            </tr> </table> </td>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divsubjects" runat="server" visible="false">
                                <div class="widget-box">
                                    <div class="widget-header widget-header-small header-color-dark">
                                        <h5 class="modal-title">
                                            <asp:Label ID="Label22" runat="server"></asp:Label>
                                            <asp:Label ID="Label23" runat="server" Visible="true"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div class="widget-body-inner">
                                        <div class="widget-main">
                                            <td class="span12" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <asp:DataList ID="dlSubjects1" CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%">
                                                            <HeaderTemplate>
                                                                <%-- <asp:CheckBox ID="chkAll" runat="server" Checked="True" Visible="true" Enabled="false" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">--%>
                                                                Subject Code </th>
                                                                <th style="text-align: left;">
                                                                    Subject Name
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Subject Grup Code
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Subject Date
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Subject Price
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    CRF Value
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    CRF %
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Total Course Fee
                                                                </th>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--       <span style=" width: 2%; text-align: left" >
                                                            <asp:CheckBox ID="chkCheck" runat="server" Checked="True" Visible="true" Enabled="false"  />
                                                            </span>
                                                            </td>
                                                            <td style="width: 8%; text-align: left">--%>
                                                                <asp:Label ID="lblSubCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                                </td>
                                                                <td style="width: 20%; text-align: left">
                                                                    <asp:Label ID="lblSubName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                                </td>
                                                                <td style="width: 10%; text-align: left">
                                                                    <asp:Label ID="lblsubgrup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subgrup")%>' />
                                                                </td>
                                                                <td style="width: 20%; text-align: left">
                                                                    <asp:TextBox runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                        Text='<%#DataBinder.Eval(Container.DataItem,"sdate")%>' Width="85%" ID="txtsubjdate"
                                                                        placeholder="sdate" data-placement="bottom" data-original-title="Date Range" />
                                                                </td>
                                                                <td style="width: 15%; text-align: left;">
                                                                    <asp:TextBox ID="txtprise" runat="server" OnTextChanged="txtprise_Changed" AutoPostBack="true"
                                                                        Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Prices")%>' Width="85%" />
                                                                </td>
                                                                <td style="width: 10%; text-align: center;">
                                                                    <asp:TextBox ID="txtCRF" runat="server" OnTextChanged="txtCRF_Changed" AutoPostBack="true"
                                                                        Text='<%#DataBinder.Eval(Container.DataItem,"CRF")%>' Width="85%" onkeypress="return NumberOnly()" />
                                                                </td>
                                                                <td style="width: 10%; text-align: center;">
                                                                    <asp:TextBox ID="TxtCRFvalue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF_Value")%>'
                                                                        Width="85%" onkeypress="return NumberOnly()" />
                                                                </td>
                                                                <td style="width: 15%; text-align: center;">
                                                                    <asp:TextBox ID="txtTotal" runat="server" AutoPostBack="true" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Course_Fees")%>'
                                                                        Width="85%" onkeypress="return NumberOnly()" />
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        <%--<div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                                        <!--Button Area -->
                                                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                                                     
                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                                            Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                                          <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                    </div>--%>
                                            </td>
                                            </tr> </table> </td>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divRegst" runat="server" visible="false">
                                <div class="widget-box">
                                    <div class="widget-header widget-header-small header-color-dark">
                                        <h5 class="modal-title">
                                            <asp:Label ID="Label24" runat="server"></asp:Label>
                                            <asp:Label ID="Label25" runat="server" Visible="true"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div class="widget-body-inner">
                                        <div class="widget-main">
                                            <td class="span12" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <asp:DataList ID="dsregst" CssClass="table table-striped table-bordered table-hover"
                                                            runat="server" Width="100%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" Visible="false" />
                                                                <span class="lbl"></span></th>
                                                                <th style="text-align: left;">
                                                                    Material Code
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Voucher Desc
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Voucher Type
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Validity Sdate
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Validity Edate
                                                                </th>
                                                                <th style="text-align: left;">
                                                                    Amount/%
                                                                </th>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%--<span style=" width: 2%; text-align: left" >--%>
                                                                <asp:CheckBox ID="chkCheck" runat="server" />
                                                                <span class="lbl"></span></td>
                                                                <td style="width: 10%; text-align: left">
                                                                    <asp:Label ID="lblRegcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                                </td>
                                                                <td style="width: 25%; text-align: left">
                                                                    <asp:Label ID="lblVouchrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_name")%>' />
                                                                </td>
                                                                <td style="width: 10%; text-align: left">
                                                                    <asp:Label ID="lalvoutype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                                                                </td>
                                                                <td style="width: 15%; text-align: left">
                                                                    <input readonly="readonly" class="span2.5 date-picker" id="txtPeriod1" runat="server"
                                                                        type="text" />
                                                                </td>
                                                                <td style="width: 10%; text-align: left;">
                                                                    <asp:TextBox ID="Txtedate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Enddate")%>' />
                                                                </td>
                                                                <td style="width: 20%; text-align: center;">
                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'
                                                                        Width="85%" />
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                                            <!--Button Area -->
                                                            <asp:Label runat="server" ID="Label26" Text="" ForeColor="Red" />
                                                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button1" runat="server"
                                                                Text="Save" ValidationGroup="UcValidate" OnClick="BtnRegSave_Click" />
                                                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button2" runat="server"
                                                                Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button3" Visible="true"
                                                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                        </div>
                                            </td>
                                            </tr> </table> </td>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal fade" id="divforexport" style="left: 50% !important; top: 10% !important;
                    display: none; width: 40%" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Select Master
                                    <asp:Label ID="Lblpk" runat="server" Visible="false" />
                                </h4>
                                <asp:CheckBox ID="chkAllHidden" runat="server" Visible="False" />
                            </div>
                            <div class="modal-body">
                                <!--Controls Area -->
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 100%;" colspan="3">
                                            <asp:DataList ID="dladditem" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                Width="100%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" />
                                                    <span class="lbl"></span></th>
                                                    <th>
                                                        <b>Master File Name</b>
                                                    </th>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="True" />
                                                    <span class="lbl"></span></td>
                                                    <td>
                                                        <asp:Label ID="lblMastertable1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"name")%>' />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <!--Button Area -->
                                <asp:Label runat="server" ID="Label44" Text="" Visible="false" />
                                <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnadditem" ToolTip="OK"
                                    runat="server" Text="OK" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="Button5" ToolTip="Cancel" runat="server" Text="Cancel" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="Divedit" runat="server" visible="false">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="widget-box">
                        <div class="table-header">
                            <h5 class="modal-title">
                                <asp:Label ID="Label64" runat="server"></asp:Label>
                                <asp:Label ID="Label65" runat="server" Visible="false"></asp:Label>
                            </h5>
                        </div>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                    <tr runat="server" id="Tr1">
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label66" CssClass="red">Divison </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="divisionname" runat="server" />
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="Txtdiv" runat="server"
                                                            Visible="false" />
                                                        <%--<asp:DropDownList runat="server" ID="ddldivision1" Width="240px" data-placeholder="Select Division"
                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionAdd_SelectedIndexChanged" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label67" CssClass="red">Academic Year </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="acadyear" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label68" CssClass="red">Classroom Course </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="Classroom" runat="server"
                                                            Visible="false" />
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="Txtclassroom1" runat="server" />
                                                        <%--  <asp:DropDownList runat="server" ID="ddlclasroom" Width="275px" data-placeholder="Select ClassRoom Course"
                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlClassRoomCourse_SelectedIndexChanged" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr2">
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label69" CssClass="red">Product Name</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="txtstream" runat="server"
                                                            MaxLength="8000" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label70" CssClass="red">Stream Code</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Description" ID="txtbatch" Width="225px" runat="server"
                                                            MaxLength="1000" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label71" CssClass="red">Center(s) </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:ListBox runat="server" ID="ddlCenter1" data-placeholder="Select Center(s)" Width="225px"
                                                            CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="divsubedit" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="table-header">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label72" runat="server"></asp:Label>
                                        <asp:Label ID="Label73" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <asp:DataList ID="Subedite1" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            Subject Group Code </th>
                                                            <th style="text-align: left; width: 80%;">
                                                            Subject Group Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtSubgrCode" runat="server" Style="width: 20%;" Text='<%#DataBinder.Eval(Container.DataItem,"Material_of_material_type_ZSGR")%>' />
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="txtSubgrName" runat="server" Style="width: 80%;" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divsubjectsedit" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="table-header">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label74" runat="server"></asp:Label>
                                        <asp:Label ID="Label75" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <asp:DataList ID="subjectedit" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            Subject Code </th>
                                                            <th style="text-align: left;">
                                                                Subject Name
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Subject Grup Code
                                                            </th>
                                                            <th style="text-align: left;">
                                                                S Date
                                                            </th>
                                                            <th style="text-align: left;">
                                                                E Date
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Subject Prise
                                                            </th>
                                                            <th style="text-align: left;">
                                                                CRF Value
                                                            </th>
                                                            <th style="text-align: left;">
                                                                CRF %
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Total Course Fee
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubCode1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lblSubName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: left">
                                                                <asp:Label ID="lblsubgrup1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subgrup")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left">
                                                                <asp:Label ID="Lblsdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Sdate")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left">
                                                                <asp:Label ID="Lbledte" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Edate")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:Label ID="Lblsubfee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Prices")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: center;">
                                                                <asp:Label ID="Lblcrf" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: center;">
                                                                <asp:Label ID="Lblcrfvalue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF_Value")%>' />
                                                            </td>
                                                            <td style="width: 15%; text-align: center;">
                                                                <asp:TextBox ID="txtTotal" runat="server" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Course_Fees")%>'
                                                                    Width="85%" />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="diveditreg" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="table-header">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label77" runat="server"></asp:Label>
                                        <asp:Label ID="Label78" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <p>
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="Lberror" runat="server" Text="" Visible="false" CssClass="red"></asp:Label>
                                                    </p>
                                                    <asp:DataList ID="registration"  Visible="true" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            Material Code </th>
                                                            <th style="text-align: left;">
                                                                Voucher Desc
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Voucher Type
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Sdate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Edate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Course
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Batch
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Amount/%
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRegcode" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                            </td>
                                                            <td style="width: 25%; text-align: left">
                                                                <asp:Label ID="lblVouchrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left">
                                                                <asp:Label ID="lalvoutype" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left">
                                                                <asp:Label ID="lblregSdate" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_Start_Date")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:Label ID="Txtedate" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_End_Date")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:Label ID="CRS"  Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_type_ZCRS")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:Label ID="Txtbatch" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: center;">
                                                                <asp:Label ID="txtregAmount"  Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Registration_Fee")%>'
                                                                    Width="85%" />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="diveditrobomate" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="table-header">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label76" runat="server"></asp:Label>
                                        <asp:Label ID="Label79" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <p>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lbldlerror" runat="server" Text="" Visible="false" CssClass="red"></asp:Label>
                                                </p>
                                                <tr>
                                                    <asp:DataList ID="robomate" Visible="true" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            Material Code </th>
                                                            <th style="text-align: left;">
                                                                Voucher Desc
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Voucher Type
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Sdate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Edate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Course
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Batch
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Amount/%
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRegcode" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                            </td>
                                                            <td style="width: 25%; text-align: left">
                                                                <asp:Label ID="lblVouchrName" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_name")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left">
                                                                <asp:Label ID="lalvoutype" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left">
                                                                <asp:Label ID="lblregSdate" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_Start_Date")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:Label ID="Txtedate" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Validity_End_Date")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:Label ID="CRS" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_type_ZCRS")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:Label ID="Txtbatch" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                                                            </td>
                                                            <td style="width: 20%; text-align: center;">
                                                                <asp:Label ID="txtregAmount" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Robomate_fee")%>'
                                                                    Width="85%" />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divDiscount" runat="server" visible="false">
                            <div class="widget-box">
                                <div class="table-header">
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label88" runat="server"></asp:Label>
                                        <asp:Label ID="Label89" runat="server" Visible="true"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-body-inner">
                                    <div class="widget-main">
                                        <td class="span12" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <p>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lberor" runat="server" Text="" Visible="false" CssClass="red"></asp:Label>
                                                </p>
                                                <tr>
                                                    <asp:DataList ID="dsdiscount" Visible="true" CssClass="table table-striped table-bordered table-hover"
                                                        runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            Subject Grp Name </th>
                                                            <th style="text-align: left;">
                                                                Voucher Type
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Pay Plan
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Sdate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Validity Edate
                                                            </th>
                                                            <th style="text-align: left;">
                                                                Discount %
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRegcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                            </td>
                                                            <td style="width: 25%; text-align: left">
                                                                <asp:Label ID="lblVouchrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Condition_Type")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left">
                                                                <asp:Label ID="lalplan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pay_Plan")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left">
                                                                <asp:Label ID="lblDSdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SDATE")%>' />
                                                            </td>
                                                            <td style="width: 10%; text-align: left;">
                                                                <asp:Label ID="lblDEdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EDATE")%>' />
                                                            </td>
                                                            <td style="width: 8%; text-align: left;">
                                                                <asp:Label ID="Disp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount_Value")%>' />
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                        </td>
                                        </tr> </table> </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="widget-main alert-block alert-info" style="text-align: center;">
                <!--Button Area -->
                <%--    <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="Saveedite"
                                    Text="Save" ToolTip="Save" OnClick="BtnSvaeedite_Click" />--%>
                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="clossedit" Visible="true"
                    runat="server" Text="Cancel" OnClick="BtnclosseditClick" />
            </div>
        </div>
        <div id="Divcenteredit" runat="server" visible="false">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="widget-box">
                        <div class="table-header">
                            <h5 class="modal-title">
                                <asp:Label ID="Label80" runat="server"></asp:Label>
                                <asp:Label ID="Label81" runat="server" Visible="false"></asp:Label>
                            </h5>
                        </div>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                    <tr runat="server" id="Tr3">
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label82" CssClass="red">Divison </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="divisionname1" runat="server"
                                                            Visible="false" />
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="Txtdiv1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label83" CssClass="red">Academic Year </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="acad year" Width="225px" ID="acadyear1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label84" CssClass="red">Classroom Course </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Product Name" Visible="false" Width="225px" ID="Classroom1"
                                                            runat="server" />
                                                        <asp:TextBox placeholder="Product Name" Width="225px" ID="Txtcoursename1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr4">
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label85" CssClass="red">Product Name</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="stream Name" Width="225px" ID="txtstream1" runat="server"
                                                            MaxLength="8000" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label86" CssClass="red">Stream Code</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:TextBox placeholder="Description" ID="txtbatch1" Width="225px" runat="server"
                                                            MaxLength="1000" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label87" CssClass="red">Center(s) </asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:ListBox runat="server" ID="ddlCenteredite" data-placeholder="Select Center(s)"
                                                            Width="225px" CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true">
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="divsubeite1" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">
                                    <asp:Label ID="Label90" runat="server"></asp:Label>
                                    <asp:Label ID="Label91" runat="server" Visible="true"></asp:Label>
                                </h5>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main">
                                    <td class="span12" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <asp:DataList ID="Dasubedit1" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%">
                                                    <HeaderTemplate>
                                                        <%-- <asp:CheckBox ID="chkAll" runat="server" Checked="True" Visible="true" Enabled="false" />
                                                            <span class="lbl"></span></th>
                                                            <th style="text-align: left;">--%>
                                                        Subject Code </th>
                                                        <th style="text-align: left;">
                                                            Subject Name
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Subject Grup Code
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Subject Date
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Subject Price
                                                        </th>
                                                        <th style="text-align: left;">
                                                            CRF Value
                                                        </th>
                                                        <th style="text-align: left;">
                                                            CRF %
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Total Course Fee
                                                        </th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--       <span style=" width: 2%; text-align: left" >
                                                            <asp:CheckBox ID="chkCheck" runat="server" Checked="True" Visible="true" Enabled="false"  />
                                                            </span>
                                                            </td>
                                                            <td style="width: 8%; text-align: left">--%>
                                                        <asp:Label ID="lblSubCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                        </td>
                                                        <td style="width: 20%; text-align: left">
                                                            <asp:Label ID="lblSubName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Name")%>' />
                                                        </td>
                                                        <td style="width: 10%; text-align: left">
                                                            <asp:Label ID="lblsubgrup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"subgrup")%>' />
                                                        </td>
                                                        <td style="width: 20%; text-align: left">
                                                            <asp:TextBox runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"sdate")%>' Width="85%" ID="txtsubjdate"
                                                                placeholder="sdate" data-placement="bottom" data-original-title="Date Range" />
                                                        </td>
                                                        <td style="width: 15%; text-align: left;">
                                                            <asp:TextBox ID="txtprise" runat="server" OnTextChanged="txtprise_Changed" AutoPostBack="true"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Prices")%>' Width="85%" />
                                                        </td>
                                                        <td style="width: 10%; text-align: center;">
                                                            <asp:TextBox ID="txtCRF" runat="server" OnTextChanged="txtCRF_Changed" AutoPostBack="true"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"CRF")%>' Width="85%" onkeypress="return NumberOnly()" />
                                                        </td>
                                                        <td style="width: 10%; text-align: center;">
                                                            <asp:TextBox ID="TxtCRFvalue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CRF_Value")%>'
                                                                Width="85%" onkeypress="return NumberOnly()" />
                                                        </td>
                                                        <td style="width: 15%; text-align: center;">
                                                            <asp:TextBox ID="txtTotal" runat="server" AutoPostBack="true" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Course_Fees")%>'
                                                                Width="85%" onkeypress="return NumberOnly()" />
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <%--<div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                                                        <!--Button Area -->
                                                        <asp:Label runat="server" ID="Label92" Text="" ForeColor="Red" />
                                                     
                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button4" runat="server"
                                                            Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                                                          <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                                    </div>--%>
                                    </td>
                                    </tr> </table> </td>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divregedite1" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">
                                    <asp:Label ID="Label92" runat="server"></asp:Label>
                                    <asp:Label ID="Label93" runat="server" Visible="true"></asp:Label>
                                </h5>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main">
                                    <td class="span12" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <asp:DataList ID="Daregedite1" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%">
                                                    <HeaderTemplate>
                                                        <%-- <asp:CheckBox ID="chkAll" runat="server" Visible="false" />
                                                                <span class="lbl"></span></th>--%>
                                                        <th style="text-align: left;">
                                                            Material Code
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Voucher Desc
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Voucher Type
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Validity Sdate
                                                        </th>
                                                        <%-- <th style="text-align: left;">
                                                                    Validity Edate
                                                                </th>--%>
                                                        <th style="text-align: left;">
                                                            Amount/%
                                                        </th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<span style=" width: 2%; text-align: left" >--%>
                                                        <%--<asp:CheckBox ID="chkCheck" runat="server" />
                                                                <span class="lbl"></span></td>--%>
                                                        <td style="width: 20%; text-align: left">
                                                            <asp:Label ID="lblRegcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                        </td>
                                                        <td style="width: 25%; text-align: left">
                                                            <asp:Label ID="lblVouchrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_name")%>' />
                                                        </td>
                                                        <td style="width: 10%; text-align: left">
                                                            <asp:Label ID="lalvoutype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                                                        </td>
                                                        <td style="width: 25%; text-align: left">
                                                            <asp:TextBox runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Vsdate")%>' Width="85%" ID="txtPeriod1"
                                                                placeholder="Vsdate" data-placement="bottom" data-original-title="Date Range" />
                                                        </td>
                                                        <%--<td style="width: 10%; text-align: left;">
                                                                    <asp:TextBox ID="Txtedate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Enddate")%>' />
                                                                </td>--%>
                                                        <td style="width: 20%; text-align: center;">
                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'
                                                                Width="85%" />
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                    </td>
                                    </tr> </table> </td>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divrobomate1" runat="server" visible="false">
                        <div class="widget-box">
                            <div class="widget-header widget-header-small header-color-dark">
                                <h5 class="modal-title">
                                    <asp:Label ID="Label94" runat="server"></asp:Label>
                                    <asp:Label ID="Label95" runat="server" Visible="true"></asp:Label>
                                </h5>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main">
                                    <td class="span12" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <asp:DataList ID="Darobomate1" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%">
                                                    <HeaderTemplate>
                                                        <%--<asp:CheckBox ID="chkAll" runat="server" Visible="false" />
                                                                <span class="lbl"></span></th>--%>
                                                        <th style="text-align: left;">
                                                            Material Code
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Voucher Desc
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Voucher Type
                                                        </th>
                                                        <th style="text-align: left;">
                                                            Validity Sdate
                                                        </th>
                                                        <%-- <th style="text-align: left;">
                                                                    Validity Edate
                                                                </th>--%>
                                                        <th style="text-align: left;">
                                                            Amount/%
                                                        </th>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<span style=" width: 2%; text-align: left" >--%>
                                                        <%--<asp:CheckBox ID="chkCheck" runat="server" />
                                                                <span class="lbl"></span></td>--%>
                                                        <td style="width: 20%; text-align: left">
                                                            <asp:Label ID="lblRobocode1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Material_Code")%>' />
                                                        </td>
                                                        <td style="width: 25%; text-align: left">
                                                            <asp:Label ID="lblVouchrName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_name")%>' />
                                                        </td>
                                                        <td style="width: 10%; text-align: left">
                                                            <asp:Label ID="lalrobvoutype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Voucher_Type")%>' />
                                                        </td>
                                                        <td style="width: 25%; text-align: left">
                                                            <asp:TextBox runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Validity_Start_Date")%>' Width="85%"
                                                                ID="txtPeriod1" placeholder="Validity_Start_Date" data-placement="bottom" data-original-title="Date Range" />
                                                        </td>
                                                        <%-- <td style="width: 10%; text-align: left;">
                                                                    <asp:TextBox ID="Txtedate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Enddate")%>' />
                                                                </td>--%>
                                                        <td style="width: 20%; text-align: center;">
                                                            <asp:TextBox ID="txtroboAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'
                                                                Width="85%" />
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                    </td>
                                    </tr> </table> </td>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="widget-main alert-block alert-info" style="text-align: center;">
                <!--Button Area -->
                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="Saveedite"
                    Text="Save" ToolTip="Save" OnClick="BtnSvaeedite_Click" />
                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="Cancelledite" Visible="true"
                    runat="server" Text="Cancel" OnClick="BtnclosseditClick" />
            </div>
        </div>
        <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
            display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">
                            Delete Stream Details
                        </h4>
                    </div>
                    <div class="modal-body">
                        <!--Controls Area -->
                        <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="txtDeleteItemName"
                            Text="You are about to delete the stream. 
                                Note: This is a irreversible process.Are you sure you want to continue..." />
                        <center />
                    </div>
                    <div class="modal-footer">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                        <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                            ToolTip="Yes" runat="server" Text="Yes" OnClick="btnDelete_Yes_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                            ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!--/row-->
    </div>
</asp:Content>
