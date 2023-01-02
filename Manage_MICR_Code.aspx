<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false"    CodeFile="Manage_MICR_Code.aspx.cs" Inherits="Manage_MICR_Code"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function NumberOnly() {
              var AsciiValue = event.keyCode
              if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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
            <li><i class="icon-home"></i><a href="Homepage.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>            
            <li>
                <h4 class="blue">
                    Bank MICR<span class="divider"></span></h4>
            </li>
        </ul>
         <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" 
                 ID="BtnAdd" Text="Add" onclick="BtnAdd_Click" />
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" 
                 onclick="BtnShowSearchPanel_Click" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->

        <div class="row-fluid">
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
                                <center><table cellpadding="6" class="table table-striped table-bordered table-condensed">
                                            
                                         <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label2">MICR No</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtMicrNo" runat="server" ></asp:TextBox>                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1">Bank Name</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtBankName" runat="server" ></asp:TextBox>                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                             
                                                 
                                            </tr>                                            
                                         
                                         
                                             </table></center>
                                       </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" onclick="BtnSearch_Click"  />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    
                                                                                                                                                                                                                                                                                                                                                                                                                                    <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
            <div id="DivResult" runat="server">
                        <div class="tab-content" style="border: 1px solid #DDDDDD">                            
                                <div id="ACountPendingandConfirm" class="table-content" >  
    
        <center>  <asp:Label ID="lblErrormsg" runat="server" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label></center>
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
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    </div>
        <div class="widget-main no-padding" style="height: 400px; Width: 100%; overflow-y: none; overflow-x: scroll;" id ="divBankDetail" runat ="server">
            
                <asp:GridView ID="grdBankDetails" runat="server" DataKeyNames="Micrno"   AutoGenerateColumns="False"  OnRowEditing="grdBankDetails_RowEditing" OnRowCancelingEdit="grdBankDetails_RowCancelingEdit" OnRowUpdating="grdBankDetails_RowUpdating" ShowFooter="True" OnRowCommand="grdBankDetails_RowCommand" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateEditButton="True">
                    <Columns>          

                        <asp:TemplateField HeaderText="MICR No">
                           <%-- <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="Addvalidation"  />                    
                            </FooterTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblmicrno" runat="server" Text='<%# Eval("Micrno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City Code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCityCode" Text='<%# Eval("citycode") %>' runat="server" ></asp:TextBox>
                            </EditItemTemplate>
                           <%-- <FooterTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>                                       
                            <asp:RequiredFieldValidator ID="RFV83" runat="server" ControlToValidate="TextBox1" ErrorMessage="*" ForeColor="Red" ValidationGroup="Addvalidation"></asp:RequiredFieldValidator>                                
                            </FooterTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblcitycode" runat="server" Text='<%# Eval("citycode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBankCode" runat="server" Text='<%# Eval("bankcode") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <%--<FooterTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV4" runat="server" ControlToValidate="TextBox2" ErrorMessage="*" ForeColor="Red" ValidationGroup="Addvalidation"></asp:RequiredFieldValidator>                                
                            </FooterTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblBankcode" runat="server" Text='<%# Eval("bankcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtbranchcode" runat="server" Text='<%# Eval("branchcode") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <%--<FooterTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" ForeColor="Red" ValidationGroup="Addvalidation"></asp:RequiredFieldValidator>                                
                            </FooterTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblbranchcode" runat="server" Text='<%# Eval("branchcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtbankname" runat="server" Text='<%# Eval("bankname") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <%--<FooterTemplate>
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="TextBox4" ErrorMessage="*" ForeColor="Red" ValidationGroup="Addvalidation"></asp:RequiredFieldValidator>                                
                            </FooterTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Branch">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtbankbranch" runat="server" Text='<%# Eval("bankbranch") %>'></asp:TextBox>
                            </EditItemTemplate>
                           <%-- <FooterTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="TextBox5" ErrorMessage="*" ForeColor="Red" ValidationGroup="Addvalidation"></asp:RequiredFieldValidator>                                
                            </FooterTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblbankbranch" runat="server" Text='<%# Eval("bankbranch") %>'></asp:Label>
                            </ItemTemplate>
                
                        </asp:TemplateField>
                    </Columns>
        
                </asp:GridView>
               
    </div>
                                    </div>
        </div>
        </div>

            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" Text="Create New MICR" runat="server"></asp:Label>
                            
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="2" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left; height: 42px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">City Code</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtCityCode" runat="server" Width="205px" MaxLength="3" onkeypress="return NumberOnly()" ></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                 <td class="span4" style="text-align: left; height: 42px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;" colspan="2">
                                                                <asp:Label runat="server" ID="Label13" class="red">Bank Code</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <asp:TextBox ID="txtBankCode" runat="server" Width="205px" MaxLength="3" onkeypress="return NumberOnly()" ></asp:TextBox>
                                                                                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left; height: 42px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Branch Code</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox ID="txtBranchCode" runat="server" Width="205px" MaxLength="3" onkeypress="return NumberOnly()" ></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>                                 
                                            <tr>
                                                
                                                 <td class="span4" style="text-align: left; height: 42px;">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;" colspan="2">
                                                                <asp:Label runat="server" ID="Label4" class="red" >Bank Name</asp:Label>
                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <asp:TextBox ID="txtBankNameAdd" runat="server" Width="205px"></asp:TextBox>
                                                                                                                                
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left; height: 42px;">
                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                                            <asp:Label runat="server" ID="Label5" CssClass="red">Branch Name</asp:Label>
                                                                
                                                                        </td>
                                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                                            <asp:TextBox ID="txtBranchName" runat="server" Width="205px"></asp:TextBox>
                                                                
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                             <td class="span4" style="text-align: left; height: 42px;">
                                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                    <tr>
                                                                        <td style="border-style: none; text-align: left; width: 40%;" colspan="2">
                                                               
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
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                                    Text="Save" ToolTip="Save" onclick="btnSave_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClear_Add" Visible="true"
                                    runat="server" Text="Close" onclick="btnClear_Add_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                </div>
    </div>
        </div>
    
</asp:Content>

