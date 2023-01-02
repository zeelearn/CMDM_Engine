using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;

public partial class Master_ClassroomProduct : System.Web.UI.Page
{
        #region Page Load
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    Page_Validation();               
                    FillDDL_Division();
                    FillDDL_AcadYear();
                    FillDDL_ClassRoomCourse();
                    FillDDL_UOM();
                    FillGrid_Subject();
                    FillDDL_SubjectGroup();
                    FillDDL_PayPlan();
                    FillDDL_VoucherType();
                    FillDDL_VoucherTypeINHeader();
                    ControlVisibility("Search");
                }
            }
        #endregion

        #region Methods

            private void Clear_ClassRoomProductAddPanel()
            {
                ddlDivisionAdd.SelectedIndex = 0;
                ddlAcadYearAdd.SelectedIndex = 0;
                ddlClassRoomCourse.SelectedIndex = 0;
                ddlCenter.Items.Clear();
                txtProductName.Text = "";
                txtDescription.Text = "";
                ddlFeesZone.SelectedIndex = 0;
                txtCoursePeriod.Value = "";
                txtAdmissionPeriod.Value = "";
                txtProductCode.Text = "";
                chkAllowDP.Checked = false;
                //tdDPDate.Visible = false;
                lblDate.Visible = false;
                txtDPDate1.Visible = false;
                txtDPDate.Value = "";
                chkMaxChequeDate.Checked = false;
                //tdMaxChequeDate.Visible = false;
                lblCheckdate.Visible = false;
                txtMaxChequeDate1.Visible = false;
                txtMaxChequeDate.Value = "";
                txtMaxNoOfReceipts.Text = "";
                txtMaxNoOfDays.Text = "";

            }

            private void Clear_ItemLevelPricingAddPanel()
            {
                ddlSubjectGroup.SelectedIndex = 0;
                ddlVoucherType.SelectedIndex = 0;
                ddlPayPlan.SelectedIndex = 0;
                txtVoucherAmount.Text = "";
                txtItemLevelPeriod.Value = "";                
            }

            private void Clear_ItemPricingHeaderAddPanel()
            {
                ddlItemHeaderVoucherType.SelectedIndex = 0;               
                txtItemHeaderVoucherAmount.Text = "";
                txtItemHeaderValidityPeriod.Value = "";
                ddlItemPricingHeaderUOM.SelectedIndex = 0;
                txtItemHeaderMinOrdQty.Text = "";
                txtMaterialROB.Text = "";
            }
            private void Clear_SubjectGroupAddPanel()
            {
                txtFees.Text = "";
                ddlSubjectGroupAdd.SelectedIndex = 0;
                ddlUOM.SelectedIndex = 0;
                txtMinOrderQty.Text = "";               
                //Set checked status of hidden check box to items in grid
                foreach (DataListItem dtlItem in dlSubjects.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                    chkitemck.Checked = false;
                }
            }

            

            private void FillDDL_Division()
            {

                try
                {

                    Clear_Error_Success_Box();
                    string Company_Code = "MT";
                    string DBname = "CDB";

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string UserID = cookie.Values["UserID"];
                    string UserName = cookie.Values["UserName"];

                    DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
                    BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
                    ddlDivision.Items.Insert(0, "Select");
                    ddlDivision.SelectedIndex = 0;

                    BindDDL(ddlDivisionAdd, dsDivision, "Division_Name", "Division_Code");
                    ddlDivisionAdd.Items.Insert(0, "Select");
                    ddlDivisionAdd.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_ItemPricingSubGroup()
            {

                try
                {

                    Clear_Error_Success_Box();
                    DataSet dsSubjectGroup = ProductController.Get_ClassroomProduct("", "", lblPKey.Text, "", "", "9");
                    BindDDL(ddlSubjectGroup, dsSubjectGroup, "Material_Name", "Material_Code");
                    ddlSubjectGroup.Items.Insert(0, "Select");
                    ddlSubjectGroup.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_Center()
            {

                try
                {                                      

                    DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_ForUserCreation(4, ddlDivisionAdd.SelectedValue);
                    if (dsCenter != null)
                    {
                        if (dsCenter.Tables.Count != 0)
                        {
                            BindListBox(ddlCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_AcadYear()
            {
                try
                {
                    DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
                    BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
                    ddlAcadYear.Items.Insert(0, "Select");
                    ddlAcadYear.SelectedIndex = 0;

                    BindDDL(ddlAcadYearAdd, dsAcadYear, "Description", "Id");
                    ddlAcadYearAdd.Items.Insert(0, "Select");
                    ddlAcadYearAdd.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_ClassRoomCourse()
            {
                try
                {
                    DataSet dsClassroomCourse = ProductController.Get_ClassRoomCourse("1");
                    BindDDL(ddlClassRoomCourse, dsClassroomCourse, "Material_Name", "Material_Code");
                    ddlClassRoomCourse.Items.Insert(0, "Select");
                    ddlClassRoomCourse.SelectedIndex = 0;
                                        
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillGrid_Subject()
            {
                try
                {
                    DataSet dsSubject = ProductController.Get_ClassRoomCourse("2");
                    dlSubjects.DataSource = dsSubject;
                    dlSubjects.DataBind();
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_SubjectGroup()
            {
                try
                {
                    DataSet dsClassroomCourse = ProductController.Get_ClassRoomCourse("3");
                    BindDDL(ddlSubjectGroup, dsClassroomCourse, "Material_Name", "Material_Code");
                    ddlSubjectGroup.Items.Insert(0, "Select");
                    ddlSubjectGroup.SelectedIndex = 0;

                    BindDDL(ddlSubjectGroupAdd, dsClassroomCourse, "Material_Name", "Material_Code");
                    ddlSubjectGroupAdd.Items.Insert(0, "Select");
                    ddlSubjectGroupAdd.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_PayPlan()
            {
                try
                {
                    DataSet dsPayPlan = ProductController.Get_ClassRoomCourse("5");
                    BindDDL(ddlPayPlan, dsPayPlan, "Pay_Plan_Description", "Pay_Plan_Code");
                    ddlPayPlan.Items.Insert(0, "Select");
                    ddlPayPlan.SelectedIndex = 0;
                    
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_VoucherType()
            {
                try
                {
                    DataSet dsVoucherType = ProductController.Get_ClassRoomCourse("6");
                    BindDDL(ddlVoucherType, dsVoucherType, "Pay_Plan_name", "Condition_Type");
                    ddlVoucherType.Items.Insert(0, "Select");
                    ddlVoucherType.SelectedIndex = 0;

                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_VoucherTypeINHeader()
            {
                try
                {
                    DataSet dsVoucherTypeHeader = ProductController.Get_ClassRoomCourse("7");
                    BindDDL(ddlItemHeaderVoucherType, dsVoucherTypeHeader, "Voucher_Description", "Voucher_Type");
                    ddlItemHeaderVoucherType.Items.Insert(0, "Select");
                    ddlItemHeaderVoucherType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }

            private void FillDDL_UOM()
            {

                try
                {
                    DataSet dsUOM = ProductController.Get_ClassRoomCourse("4");
                    BindDDL(ddlUOM, dsUOM, "UOM_Name", "UOM_Id");
                    ddlUOM.Items.Insert(0, "Select");
                    ddlUOM.SelectedIndex = 0;

                    BindDDL(ddlItemPricingHeaderUOM, dsUOM, "UOM_Name", "UOM_Id");
                    ddlItemPricingHeaderUOM.Items.Insert(0, "Select");
                    ddlItemPricingHeaderUOM.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ex.ToString();
                    UpdatePanelMsgBox.Update();
                    return;
                }
            }


            private void Fill_Grid()
            {
                Clear_Error_Success_Box();
                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_ClassroomProduct(ddlDivision.SelectedValue,ddlAcadYear.SelectedValue,txtStreamCode.Text.Trim(),txtStreamName.Text.Trim(),ddlStatus.SelectedValue,"1");
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {

                        dlClassRoomProduct.DataSource = dsGrid;
                        dlClassRoomProduct.DataBind();
                        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                        dlGridExport.DataSource = dsGrid;
                        dlGridExport.DataBind();
                    }
                    else
                    {
                        dlClassRoomProduct.DataSource = null;
                        dlClassRoomProduct.DataBind();
                        lbltotalcount.Text = "0";

                        dlGridExport.DataSource = null;
                        dlGridExport.DataBind();
                    }
                }
                else
                {
                    dlClassRoomProduct.DataSource = null;
                    dlClassRoomProduct.DataBind();
                    lbltotalcount.Text = "0";

                    dlGridExport.DataSource = null;
                    dlGridExport.DataBind();
                }

            }

            private void Fill_GridSubjetGroup()
            {

                lblSubGroupDivision_Result.Text = "";
                lblSubGroupAcadYear_Result.Text = "";
                lblSubGroupStreamCode_Result.Text = "";
                lblSubGroupStreamName_Result.Text = "";
                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_ClassroomProduct("","",lblPKey.Text,"","","3");//Subject Group Grid
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        dlSubjectGroup.DataSource = dsGrid.Tables[0];
                        dlSubjectGroup.DataBind();
                        if(dsGrid.Tables[1].Rows.Count > 0)
                        {
                            lblSubGroupDivision_Result.Text = dsGrid.Tables[1].Rows[0]["Division_Name"].ToString();
                            lblSubGroupAcadYear_Result.Text = dsGrid.Tables[1].Rows[0]["Acad_Year"].ToString();
                            lblSubGroupStreamCode_Result.Text = dsGrid.Tables[1].Rows[0]["Stream_Code"].ToString();
                            lblSubGroupStreamName_Result.Text = dsGrid.Tables[1].Rows[0]["Stream_Name"].ToString();                            
                        }

                    }
                    else
                    {
                        dlSubjectGroup.DataSource = null;
                        dlSubjectGroup.DataBind();
                    }
                }
                else
                {
                    dlSubjectGroup.DataSource = null;
                    dlSubjectGroup.DataBind();
                }
            }

            private void Fill_GridItemPricing()
            {
                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_ClassroomProduct("","",lblPKey.Text,"","","5");//Item Level Pricing Grid
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        dlItemLevelPric.DataSource = dsGrid.Tables[0];
                        dlItemLevelPric.DataBind();

                        if (dsGrid.Tables[1].Rows.Count > 0)
                        {
                            lblItemLevelDivision_Result.Text = dsGrid.Tables[1].Rows[0]["Division_Name"].ToString();
                            lblItemLevelDivisionCode_Result.Text = dsGrid.Tables[1].Rows[0]["Source_Division_Code"].ToString();
                            lblItemLevelAcadYear_Result.Text = dsGrid.Tables[1].Rows[0]["Acad_Year"].ToString();
                            lblItemLevelStreamCode_Result.Text = dsGrid.Tables[1].Rows[0]["Stream_Code"].ToString();
                            lblItemLevelStreamName_Result.Text = dsGrid.Tables[1].Rows[0]["Stream_Name"].ToString();
                        }
                    }
                    else
                    {
                        dlItemLevelPric.DataSource = null;
                        dlItemLevelPric.DataBind();
                    }
                }
                else
                {
                    dlItemLevelPric.DataSource = null;
                    dlItemLevelPric.DataBind();
                }
            }

            private void Fill_GridItemHeader()
            {
                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_ClassroomProduct("", "", lblPKey.Text, "", "", "7");//Item Pricing Header Grid
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        dlItemHeaderPric.DataSource = dsGrid.Tables[0];
                        dlItemHeaderPric.DataBind();

                        if (dsGrid.Tables[1].Rows.Count > 0)
                        {
                            lblItemHeaderDivision_Result.Text = dsGrid.Tables[1].Rows[0]["Division_Name"].ToString();
                            lblItemHeaderDivisionCode_Result.Text = dsGrid.Tables[1].Rows[0]["Source_Division_Code"].ToString();
                            lblItemHeaderAcadYear_Result.Text = dsGrid.Tables[1].Rows[0]["Acad_Year"].ToString();
                            lblItemHeaderStreamCode_Result.Text = dsGrid.Tables[1].Rows[0]["Stream_Code"].ToString();
                            lblItemHeaderStreamName_Result.Text = dsGrid.Tables[1].Rows[0]["Stream_Name"].ToString();
                        }
                    }
                    else
                    {
                        dlItemHeaderPric.DataSource = null;
                        dlItemHeaderPric.DataBind();
                    }
                }
                else
                {
                    dlItemHeaderPric.DataSource = null;
                    dlItemHeaderPric.DataBind();
                }
            }


            private void Page_Validation()
            {
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];

                string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo Info = new System.IO.FileInfo(Path);
                string pageName = Info.Name;

                int ResultId = 0;

                ResultId = ProductController.Chk_Page_Validation(pageName, UserID, "DB00");

                if (ResultId >= 1)
                {
                    //Allow
                }
                else
                {
                    Response.Redirect("~/Homepage.aspx", false);
                }

            }
        
            private void ControlVisibility(string Mode)
            {
                if (Mode == "Search")
                {
                    DivAddPanel.Visible = false;
                    DivSearchPanel.Visible = true;
                    DivResultPanel.Visible = false;
                    btnTopSearch.Visible = false;
                    BtnAdd.Visible = true;
                    DivSubjectGroup.Visible = false;
                    DivItemLevelPricing.Visible = false;
                    DivPricingHeader.Visible = false;
                }           
                else if (Mode == "Result")
                {
                    DivAddPanel.Visible = false;
                    DivSearchPanel.Visible = false;
                    btnTopSearch.Visible = false;
                    BtnAdd.Visible = true;
                    DivResultPanel.Visible = true;
                    btnTopSearch.Visible = true;
                    DivSubjectGroup.Visible = false;
                    DivItemLevelPricing.Visible = false;
                    DivPricingHeader.Visible = false;
                }
                else if (Mode == "Add")
                {
                    DivAddPanel.Visible = true;
                    DivSearchPanel.Visible = false;
                    btnTopSearch.Visible = true;
                    BtnAdd.Visible = false;
                    DivResultPanel.Visible = false;
                    DivSubjectGroup.Visible = false;
                    DivItemLevelPricing.Visible = false;
                    DivPricingHeader.Visible = false;
                }
                else if (Mode == "SubjectGroup")
                {
                    DivAddPanel.Visible = false;
                    DivSearchPanel.Visible = false;
                    btnTopSearch.Visible = true;
                    BtnAdd.Visible = false;
                    DivResultPanel.Visible = false;
                    DivSubjectGroup.Visible = true;
                    DivItemLevelPricing.Visible = false;
                    DivPricingHeader.Visible = false;
                }
                else if (Mode == "PricingItem")
                {
                    DivAddPanel.Visible = false;
                    DivSearchPanel.Visible = false;
                    btnTopSearch.Visible = true;
                    BtnAdd.Visible = false;
                    DivResultPanel.Visible = false;
                    DivSubjectGroup.Visible = false;
                    DivItemLevelPricing.Visible = true;
                    DivPricingHeader.Visible = false;
                }
                else if (Mode == "PricingHeader")
                {
                    DivAddPanel.Visible = false;
                    DivSearchPanel.Visible = false;
                    btnTopSearch.Visible = true;
                    BtnAdd.Visible = false;
                    DivResultPanel.Visible = false;
                    DivSubjectGroup.Visible = false;
                    DivItemLevelPricing.Visible = false;
                    DivPricingHeader.Visible = true;
                }
            }

            private void Show_Error_Success_Box(string BoxType, string Error_Code)
            {
                if (BoxType == "E")
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = ProductController.Raise_Error(Error_Code);
                    UpdatePanelMsgBox.Update();
                }
                else
                {
                    Msg_Success.Visible = true;
                    Msg_Error.Visible = false;
                    lblSuccess.Text = ProductController.Raise_Error(Error_Code);
                    UpdatePanelMsgBox.Update();
                }
            }
            private void Clear_Error_Success_Box()
            {
                Msg_Error.Visible = false;
                Msg_Success.Visible = false;
                lblSuccess.Text = "";
                lblerror.Text = "";
                UpdatePanelMsgBox.Update();
            }
            private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
            private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
        
        #endregion

        #region Events

            protected void BtnClearSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    ddlDivision.SelectedIndex = 0;
                    ddlAcadYear.SelectedIndex = 0;
                    txtStreamCode.Text = "";
                    txtStreamName.Text = "";
                    ddlStatus.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void BtnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (ddlDivision.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Division");
                        ddlDivision.Focus();
                        return;
                    }
                    if (ddlAcadYear.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Academic Year");
                        ddlAcadYear.Focus();
                        return;
                    }
                    if (ddlStatus.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Status");
                        ddlStatus.Focus();
                        return;
                    }


                    ControlVisibility("Result");
                    Fill_Grid();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void BtnAdd_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    ControlVisibility("Add");
                    lblHeader_Add.Text = "Add ClassRoom Product";

                    ddlDivisionAdd.Enabled = true;
                    ddlClassRoomCourse.Enabled = true;
                    ddlFeesZone.Enabled = true;
                    txtCoursePeriod.Disabled = false;

                    BtnSaveAdd.Visible = true;
                    BtnSaveEdit.Visible = false;
                    
                    Clear_ClassRoomProductAddPanel();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }

            }

            protected void btnTopSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    ControlVisibility("Search");
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void dlClassRoomProduct_ItemCommand(object source, DataListCommandEventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (e.CommandName == "comEdit")
                    {
                        Clear_Error_Success_Box();
                        ControlVisibility("Add");
                        Clear_ClassRoomProductAddPanel();
                        BtnSaveAdd.Visible = false;
                        BtnSaveEdit.Visible = true;
                        lblHeader_Add.Text = "Edit Classroom Product";
                        lblPKey.Text = e.CommandArgument.ToString();
                        DataSet dsGrid = new DataSet();
                        dsGrid = ProductController.Get_ClassroomProduct("","",lblPKey.Text,"","","2");
                        if (dsGrid.Tables[0].Rows.Count > 0)
                        {
                            ddlDivisionAdd.Enabled = false;
                            ddlClassRoomCourse.Enabled = false;
                            ddlFeesZone.Enabled = false;
                            txtCoursePeriod.Disabled = true;
                            
                            ddlDivisionAdd.SelectedValue = dsGrid.Tables[0].Rows[0]["Source_Division_Code"].ToString();
                            ddlDivisionAdd_SelectedIndexChanged(source, e);
                            ddlAcadYearAdd.SelectedValue = dsGrid.Tables[0].Rows[0]["Acad_Year"].ToString();
                            ddlClassRoomCourse.SelectedValue = dsGrid.Tables[0].Rows[0]["CourseCode"].ToString();
                            txtProductName.Text = dsGrid.Tables[0].Rows[0]["ProductName"].ToString();
                            txtDescription.Text = dsGrid.Tables[0].Rows[0]["ProductDesc"].ToString();
                            ddlFeesZone.SelectedValue = dsGrid.Tables[0].Rows[0]["FeeZone"].ToString();
                            txtCoursePeriod.Value = dsGrid.Tables[0].Rows[0]["Course_Period"].ToString();
                            txtAdmissionPeriod.Value = dsGrid.Tables[0].Rows[0]["Admission_Period"].ToString();
                            txtProductCode.Text = dsGrid.Tables[0].Rows[0]["ProductCode"].ToString();
                            //Allow DP in Multiple Receipt Checkbox
                            if (dsGrid.Tables[0].Rows[0]["AllowDPInTwoInstallmentFlag"].ToString() == "0")
                                chkAllowDP.Checked = false;
                            else if (dsGrid.Tables[0].Rows[0]["AllowDPInTwoInstallmentFlag"].ToString() == "1")
                            {
                                chkAllowDP.Checked = true;
                                //tdDPDate.Visible = true;
                                lblDate.Visible = true;
                                txtDPDate1.Visible = true;
                                txtDPDate.Value = dsGrid.Tables[0].Rows[0]["AllowDPInTwoInstallmentTillDate"].ToString();
                            }
                            //Set Limit On Maximum Cheque Date Checkbox
                            if (dsGrid.Tables[0].Rows[0]["AllowChequeTillDateFlag"].ToString() == "0")
                                chkMaxChequeDate.Checked = false;
                            else if (dsGrid.Tables[0].Rows[0]["AllowChequeTillDateFlag"].ToString() == "1")
                            {
                                chkMaxChequeDate.Checked = true;                                
                                //tdMaxChequeDate.Visible = false;
                                lblCheckdate.Visible = true;
                                txtMaxChequeDate1.Visible = true;
                                txtMaxChequeDate.Value = dsGrid.Tables[0].Rows[0]["AllowChequeTillDate"].ToString();
                            }
                            txtMaxNoOfReceipts.Text = dsGrid.Tables[0].Rows[0]["MaxNoOfChequesInFullDP"].ToString();
                            txtMaxNoOfDays.Text = dsGrid.Tables[0].Rows[0]["MaxDayGapInChequesInFullDP"].ToString();

                            //Fill Centers
                            //Fill selected Batches
                            if (dsGrid.Tables[1].Rows.Count > 0)
                            {
                                for (int cnt = 0; cnt <= dsGrid.Tables[1].Rows.Count - 1; cnt++)
                                {
                                    for (int rcnt = 0; rcnt <= ddlCenter.Items.Count - 1; rcnt++)
                                    {
                                        if (ddlCenter.Items[rcnt].Value == dsGrid.Tables[1].Rows[cnt]["Center_Code"].ToString())
                                        {
                                            ddlCenter.Items[rcnt].Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (e.CommandName == "comSubGroup")
                    {
                        ControlVisibility("SubjectGroup");                        
                        
                        lblPKey.Text = e.CommandArgument.ToString();
                        btnAddSubjectGroup.Visible = true;
                        DivResultSubjectGroup.Visible = true;
                        DivAddSubjectGroup.Visible = false;                           
                        Fill_GridSubjetGroup();                        
                    }
                    if (e.CommandName == "comPricingItem")
                    {
                        ControlVisibility("PricingItem");      

                        lblPKey.Text = e.CommandArgument.ToString();
                        btnAddItemLevelPric.Visible = true;
                        DivResultItemLevel.Visible = true;
                        DivAddItemLevel.Visible = false;
                        //ddlSubjectGroup.Items.Clear();
                        lblSubGrValiditySDate.Text = "";
                        lblSubGrValidityEDate.Text = "";
                        FillDDL_ItemPricingSubGroup();
                        Fill_GridItemPricing();

                        
                    }
                    if (e.CommandName == "comPricingHeader")
                    {
                        ControlVisibility("PricingHeader");

                        lblPKey.Text = e.CommandArgument.ToString();
                        btnAddItemHeaderPric.Visible = true;
                        DivResultItemHeader.Visible = true;
                        DivAddItemHeader.Visible = false;
                        Fill_GridItemHeader();
                    }
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void dlSubjectGroup_ItemCommand(object source, DataListCommandEventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (e.CommandName == "comEdit")
                    {
                        btnAddSubjectGroup.Visible = false;
                        DivResultSubjectGroup.Visible = false;
                        DivAddSubjectGroup.Visible = true;
                        Clear_SubjectGroupAddPanel();
                        lblSubGroupHeader_Add.Text = "Edit Subject Group";
                        btnSubjectGroup_Save.Visible = false;
                        btnSubjectGroup_SaveEdit.Visible = true;
                        lblPKeySubGroup.Text=e.CommandArgument.ToString();
                        //Edit
                        DataSet dsGrid = new DataSet();
                        dsGrid = ProductController.Get_ClassroomProduct("", "", lblPKeySubGroup.Text, "", "", "4");
                        if (dsGrid.Tables[0].Rows.Count > 0)
                        {
                            ddlSubjectGroupAdd.Enabled = false;                            
                            txtFees.Text = dsGrid.Tables[0].Rows[0]["Fees"].ToString();
                            try
                            {
                                ddlUOM.SelectedValue = dsGrid.Tables[0].Rows[0]["UOM"].ToString();
                            }
                            catch (Exception ex)
                            {
                                Show_Error_Success_Box("E", ex.ToString());
                            }
                            txtMinOrderQty.Text = dsGrid.Tables[0].Rows[0]["MinOrderQty"].ToString();

                            ddlSubjectGroupAdd.SelectedValue = dsGrid.Tables[0].Rows[0]["Sgr_Material"].ToString();
                            txtValidityPeriod.Value = dsGrid.Tables[0].Rows[0]["Validity_Period"].ToString();
                            //Fill Subject Grid
                            dlSubjects.DataSource = dsGrid.Tables[2];
                            dlSubjects.DataBind();
                            if (dsGrid.Tables[1].Rows.Count > 0)
                            {
                                //Fill Subject Grid                               
                                for (int cnt = 0; cnt <= dsGrid.Tables[1].Rows.Count - 1; cnt++)
                                {
                                    foreach (DataListItem dtlItem in dlSubjects.Items)
                                    {
                                        CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
                                        Label lblSubCode = (Label)dtlItem.FindControl("lblSubCode");
                                        if (Convert.ToString(lblSubCode.Text).Trim() == Convert.ToString(dsGrid.Tables[1].Rows[cnt]["Sub_Material"]).Trim())
                                        {
                                            chkCheck.Checked = true;
                                            break; 
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }


            protected void dlItemLevelPric_ItemCommand(object source, DataListCommandEventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (e.CommandName == "comEdit")
                    {
                        btnAddItemLevelPric.Visible = false;
                        DivResultItemLevel.Visible = false;
                        DivAddItemLevel.Visible = true;
                        Clear_ItemLevelPricingAddPanel();
                        lblItemLevelHeader_Add.Text = "Edit Item Level Price";
                        btnSaveItemLevel.Visible = false;
                        btnSaveEditItemLevel.Visible = true;                        
                        lblPKeyItemLevel.Text = e.CommandArgument.ToString();
                        //Edit
                        DataSet dsGrid = new DataSet();
                        dsGrid = ProductController.Get_ClassroomProduct("", "", lblPKeyItemLevel.Text, "", "", "6");
                        if (dsGrid.Tables[0].Rows.Count > 0)
                        {
                            ddlSubjectGroup.Enabled = false;
                            ddlSubjectGroup.SelectedValue = dsGrid.Tables[0].Rows[0]["Sgr_Material"].ToString();
                            ddlVoucherType.SelectedValue = dsGrid.Tables[0].Rows[0]["Voucher_type"].ToString();
                            ddlPayPlan.SelectedValue = dsGrid.Tables[0].Rows[0]["Pay_Plan"].ToString();
                            txtVoucherAmount.Text = dsGrid.Tables[0].Rows[0]["Voucher_amt"].ToString();
                            txtItemLevelPeriod.Value = dsGrid.Tables[0].Rows[0]["Validity_Period"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }


            protected void dlItemHeaderPric_ItemCommand(object source, DataListCommandEventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (e.CommandName == "comEdit")
                    {
                        btnAddItemHeaderPric.Visible = false;
                        DivResultItemHeader.Visible = false;
                        DivAddItemHeader.Visible = true;
                        Clear_ItemPricingHeaderAddPanel();
                        lblItemHeader_Add.Text = "Edit Item Level Price";
                        btnSaveItemHeader.Visible = false;
                        btnSaveEditItemHeader.Visible = true;
                        lblPKeyItemHeader.Text = e.CommandArgument.ToString();
                        //Edit
                        DataSet dsGrid = ProductController.Get_ClassroomProduct("", "", lblPKeyItemHeader.Text, "", "", "8");
                        if (dsGrid.Tables[0].Rows.Count > 0)
                        {
                            ddlItemHeaderVoucherType.SelectedValue = dsGrid.Tables[0].Rows[0]["Voucher_Typ"].ToString();
                            txtItemHeaderValidityPeriod.Value = dsGrid.Tables[0].Rows[0]["Validity_Period"].ToString();                            
                            txtItemHeaderVoucherAmount.Text = dsGrid.Tables[0].Rows[0]["Voucher_Amt"].ToString();
                            ddlItemPricingHeaderUOM.SelectedValue = dsGrid.Tables[0].Rows[0]["UOM"].ToString();
                            txtItemHeaderMinOrdQty.Text = dsGrid.Tables[0].Rows[0]["Min_Order_Qty"].ToString();
                            txtMaterialROB.Text = dsGrid.Tables[0].Rows[0]["Mat_ROB"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }

            }
            protected void BtnSaveAdd_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (ddlDivisionAdd.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Division");
                        ddlDivisionAdd.Focus();
                        return;
                    }
                    if (ddlAcadYearAdd.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Academic Year");
                        ddlAcadYearAdd.Focus();
                        return;
                    }
                    if (ddlClassRoomCourse.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select ClassRoom Course");
                        ddlClassRoomCourse.Focus();
                        return;
                    }
                    if (txtProductName.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Product name");
                        txtProductName.Focus();
                        return;
                    }
                    if (ddlFeesZone.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Fees Zone");
                        ddlFeesZone.Focus();
                        return;
                    }
                    if (txtCoursePeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Course Period");                        
                        return;
                    }
                    if (txtAdmissionPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Admission Period");                        
                        return;
                    }
                    if (chkAllowDP.Checked == true)
                    {
                        if (txtDPDate.Value == "")
                        {
                            Show_Error_Success_Box("E", "Select DP Date");
                            return;
                        }
                    }
                    if (chkMaxChequeDate.Checked == true)
                    {
                        if (txtMaxChequeDate.Value == "")
                        {
                            Show_Error_Success_Box("E", "Select Maximum Cheque Date");
                            return;
                        }
                    }

                    string CenterCode = "", CoursePeriod = "", Crs_SDate = "", Crs_EDate = "", AdmPeriod = "", Adm_SDate = "", Adm_EDate = "", AlowDpFlag = "0", DpDate = "", MaxChequeFlag = "0", ChequeDate = "", MaxNoOfChequesInFullDP = "", MaxDayGapInChequesInFullDP="";
                    for (int cnt = 0; cnt <= ddlCenter.Items.Count - 1; cnt++)
                    {
                        if (ddlCenter.Items[cnt].Selected == true)
                        {
                            CenterCode = CenterCode + ddlCenter.Items[cnt].Value + ",";
                        }
                    }
                    if (CenterCode == "")
                    {
                        Show_Error_Success_Box("E", "Atleast one Center should be selected");
                        ddlCenter.Focus();
                        return;
                    }

                    CenterCode = CenterCode.Substring(0, CenterCode.Length - 1);
                    CoursePeriod = txtCoursePeriod.Value;
                    Crs_SDate = CoursePeriod.Substring(0, 10);
                    Crs_EDate = CoursePeriod.Substring(13, 10);
                    AdmPeriod = txtAdmissionPeriod.Value;
                    Adm_SDate = AdmPeriod.Substring(0, 10);
                    Adm_EDate = AdmPeriod.Substring(13, 10);
                    if (chkAllowDP.Checked == true)
                    {
                        AlowDpFlag = "1";
                        DpDate = txtDPDate.Value;
                    }
                    if (chkMaxChequeDate.Checked == true)
                    {
                        MaxChequeFlag = "1";
                        ChequeDate = txtMaxChequeDate.Value;
                    }

                    MaxNoOfChequesInFullDP = txtMaxNoOfReceipts.Text.Trim();
                    MaxDayGapInChequesInFullDP = txtMaxNoOfDays.Text.Trim();

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];            

                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomProduct("", ddlAcadYearAdd.SelectedValue, ddlClassRoomCourse.SelectedValue, txtProductName.Text.Trim(), txtDescription.Text.Trim(), ddlFeesZone.SelectedValue, Crs_SDate, Crs_EDate, Adm_SDate, Adm_EDate, AlowDpFlag, DpDate, MaxChequeFlag, ChequeDate, MaxNoOfChequesInFullDP, MaxDayGapInChequesInFullDP, CenterCode, CreatedBy,"1");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record");
                        return;
                    }
                    else
                    {
                        ControlVisibility("Result");
                        Fill_Grid();
                        Show_Error_Success_Box("S", "0000");
                    }
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }
    
            protected void BtnCloseAdd_Click(object sender, EventArgs e)
            {
                try
                {
                    ControlVisibility("Result");
                    Clear_Error_Success_Box();

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }

            }
    
            protected void BtnSaveEdit_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    if (ddlDivisionAdd.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Division");
                        ddlDivisionAdd.Focus();
                        return;
                    }
                    if (ddlAcadYearAdd.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Academic Year");
                        ddlAcadYearAdd.Focus();
                        return;
                    }
                    if (ddlClassRoomCourse.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select ClassRoom Course");
                        ddlClassRoomCourse.Focus();
                        return;
                    }
                    if (txtProductName.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Product name");
                        txtProductName.Focus();
                        return;
                    }
                    if (ddlFeesZone.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Fees Zone");
                        ddlFeesZone.Focus();
                        return;
                    }
                    if (txtCoursePeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Course Period");
                        return;
                    }
                    if (txtAdmissionPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Admission Period");
                        return;
                    }
                    if (chkAllowDP.Checked == true)
                    {
                        if (txtDPDate.Value == "")
                        {
                            Show_Error_Success_Box("E", "Select DP Date");
                            return;
                        }
                    }
                    if (chkMaxChequeDate.Checked == true)
                    {
                        if (txtMaxChequeDate.Value == "")
                        {
                            Show_Error_Success_Box("E", "Select Maximum Cheque Date");
                            return;
                        }
                    }

                    string CenterCode = "", CoursePeriod = "", Crs_SDate = "", Crs_EDate = "", AdmPeriod = "", Adm_SDate = "", Adm_EDate = "", AlowDpFlag = "0", DpDate = "", MaxChequeFlag = "0", ChequeDate = "", MaxNoOfChequesInFullDP = "", MaxDayGapInChequesInFullDP = "";
                    for (int cnt = 0; cnt <= ddlCenter.Items.Count - 1; cnt++)
                    {
                        if (ddlCenter.Items[cnt].Selected == true)
                        {
                            CenterCode = CenterCode + ddlCenter.Items[cnt].Value + ",";
                        }
                    }
                    if (CenterCode == "")
                    {
                        Show_Error_Success_Box("E", "Atleast one Center should be selected");
                        ddlCenter.Focus();
                        return;
                    }

                    CenterCode = CenterCode.Substring(0, CenterCode.Length - 1);
                    CoursePeriod = txtCoursePeriod.Value;
                    Crs_SDate = CoursePeriod.Substring(0, 10);
                    Crs_EDate = CoursePeriod.Substring(13, 10);
                    AdmPeriod = txtAdmissionPeriod.Value;
                    Adm_SDate = AdmPeriod.Substring(0, 10);
                    Adm_EDate = AdmPeriod.Substring(13, 10);
                    if (chkAllowDP.Checked == true)
                    {
                        AlowDpFlag = "1";
                        DpDate = txtDPDate.Value;
                    }
                    if (chkMaxChequeDate.Checked == true)
                    {
                        MaxChequeFlag = "1";
                        ChequeDate = txtMaxChequeDate.Value;
                    }

                    MaxNoOfChequesInFullDP = txtMaxNoOfReceipts.Text.Trim();
                    MaxDayGapInChequesInFullDP = txtMaxNoOfDays.Text.Trim();

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                   

                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomProduct(lblPKey.Text, ddlAcadYearAdd.SelectedValue, ddlClassRoomCourse.SelectedValue, txtProductName.Text.Trim(), txtDescription.Text.Trim(), ddlFeesZone.SelectedValue, Crs_SDate, Crs_EDate, Adm_SDate, Adm_EDate, AlowDpFlag, DpDate, MaxChequeFlag, ChequeDate, MaxNoOfChequesInFullDP, MaxDayGapInChequesInFullDP, CenterCode, CreatedBy, "2");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record");
                        return;
                    }
                    else
                    {
                        ControlVisibility("Result");
                        Fill_Grid();
                        Show_Error_Success_Box("S", "0000");
                    }
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }

            }

            protected void btnExport_Click(object sender, EventArgs e)
            {
                Clear_Error_Success_Box();
                dlGridExport.Visible = true;

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                string filenamexls1 = "ClassRoomProducts_" + DateTime.Now + ".xls";
                Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");
                HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>ClassRoomProduct</b></TD></TR>");
                Response.Charset = "";
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
                //this.ClearControls(dladmissioncount)
                dlGridExport.RenderControl(oHtmlTextWriter1);
                Response.Write(oStringWriter1.ToString());
                Response.Flush();
                Response.End();

                dlGridExport.Visible = false;
            }

            protected void chkAll_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    CheckBox s = sender as CheckBox;
                    //Set checked status of hidden check box to items in grid
                    foreach (DataListItem dtlItem in dlSubjects.Items)
                    {
                        CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                        chkitemck.Checked = s.Checked;
                    }
                    Clear_Error_Success_Box();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void chkAllowDP_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    CheckBox s = sender as CheckBox;
                    //Set checked status of hidden check box to items in grid
                    //tdDPDate.Visible = s.Checked;
                    lblDate.Visible = s.Checked;                   
                    txtDPDate1.Visible = s.Checked;
                    
                    Clear_Error_Success_Box();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void chkMaxChequeDate_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                   
                    CheckBox s = sender as CheckBox;
                    //Set checked status of hidden check box to items in grid                                   
                    //tdMaxChequeDate.Visible = false;
                    lblCheckdate.Visible = s.Checked;
                    txtMaxChequeDate1.Visible = s.Checked;
                    Clear_Error_Success_Box();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnAddSubjectGroup_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    btnAddSubjectGroup.Visible = false;
                    btnSubjectGroup_Save.Visible = true;
                    btnSubjectGroup_SaveEdit.Visible = false;
                    ddlSubjectGroupAdd.Enabled = true;                    
                    DivResultSubjectGroup.Visible = false;
                    DivAddSubjectGroup.Visible = true;
                    Clear_SubjectGroupAddPanel();
                    lblSubGroupHeader_Add.Text = "Add New Subject Group";
                    FillGrid_Subject();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }
            protected void btnSubjectGroup_Close_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    btnAddSubjectGroup.Visible = true;
                    DivResultSubjectGroup.Visible = true;
                    DivAddSubjectGroup.Visible = false;
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnCloseItemLevel_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    btnAddItemLevelPric.Visible = true;
                    DivResultItemLevel.Visible = true;
                    DivAddItemLevel.Visible = false;
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnCloseItemHeader_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    btnAddItemHeaderPric.Visible = true;
                    DivResultItemHeader.Visible = true;
                    DivAddItemHeader.Visible = false;
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnSubjectGroup_Save_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    //btnAddSubjectGroup.Visible = true;
                    //DivResultSubjectGroup.Visible = true;
                    //DivAddSubjectGroup.Visible = false;

                    //Validation
                    if (ddlSubjectGroupAdd.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select SubjectGroup");
                        ddlSubjectGroupAdd.Focus();
                        return;
                    }
                    string SubjectCode = "",SubjectName="";
                    foreach (DataListItem dtlItem in dlSubjects.Items)
                    {
                        CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
                        Label lblSubCode = (Label)dtlItem.FindControl("lblSubCode");
                        Label lblSubName = (Label)dtlItem.FindControl("lblSubName");
                        if (chkCheck.Checked == true)
                        {
                            SubjectCode = SubjectCode + lblSubCode.Text + ",";
                            SubjectName=SubjectName+lblSubName.Text+",";
                        }
                    }
                    if (SubjectCode == "")
                    {
                        Show_Error_Success_Box("E", "Select Atleast One Subject");
                        return;
                    }
                    if (txtFees.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Fee");
                        txtFees.Focus();
                        return;
                    }
                    if (ddlUOM.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Unit Of Measurement(UOM)");
                        ddlUOM.Focus();
                        return;
                    }                    
                    if (txtValidityPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Validity Period");                        
                        return;
                    }
                    if (txtMinOrderQty.Text.Trim() == "")
                    {
                        txtMinOrderQty.Text = "0";
                    }
                    SubjectCode = SubjectCode.Substring(0, SubjectCode.Length - 1);
                    SubjectName = SubjectName.Substring(0, SubjectName.Length - 1);
                    string Validity_SDate = "", Validity_EDate = "", Validity_Period = "";
                    Validity_Period = txtValidityPeriod.Value;
                    Validity_SDate = Validity_Period.Substring(0, 10);
                    Validity_EDate = Validity_Period.Substring(13, 10);

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                   
                    //Save Data
                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomSubjectGroup(lblSubGroupStreamCode_Result.Text, ddlSubjectGroupAdd.SelectedValue, Validity_SDate, Validity_EDate, lblSubGroupStreamName_Result.Text, ddlSubjectGroupAdd.SelectedItem.ToString(), lblSubGroupAcadYear_Result.Text, txtFees.Text.Trim(), ddlUOM.SelectedValue, txtMinOrderQty.Text, SubjectCode, SubjectName,CreatedBy, "1");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Record Already Exist for this stream and Subjectgroup");
                        ddlSubjectGroupAdd.Focus();
                        return;
                    }
                    else if (ResultId == "-2")
                    {
                        Show_Error_Success_Box("E", "Validity Period is Out of Range");                        
                        return;
                    }
                    else
                    {                      
                        Fill_GridSubjetGroup();
                        Show_Error_Success_Box("S", "0000");
                        btnAddSubjectGroup.Visible = true;
                        DivResultSubjectGroup.Visible = true;
                        DivAddSubjectGroup.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnSubjectGroup_SaveEdit_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    //Validation
                    if (ddlSubjectGroupAdd.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select SubjectGroup");
                        ddlSubjectGroupAdd.Focus();
                        return;
                    }
                    string SubjectCode = "", SubjectName = "";
                    foreach (DataListItem dtlItem in dlSubjects.Items)
                    {
                        CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
                        Label lblSubCode = (Label)dtlItem.FindControl("lblSubCode");
                        Label lblSubName = (Label)dtlItem.FindControl("lblSubName");
                        if (chkCheck.Checked == true)
                        {
                            SubjectCode = SubjectCode + lblSubCode.Text + ",";
                            SubjectName = SubjectName + lblSubName.Text + ",";
                        }
                    }
                    if (SubjectCode == "")
                    {
                        Show_Error_Success_Box("E", "Select Atleast One Subject");
                        return;
                    }
                    if (txtFees.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Fee");
                        txtFees.Focus();
                        return;
                    }
                    if (ddlUOM.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Unit Of Measurement(UOM)");
                        ddlUOM.Focus();
                        return;
                    }
                    if (txtValidityPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Validity Period");
                        return;
                    }
                    if (txtMinOrderQty.Text.Trim() == "")
                    {
                        txtMinOrderQty.Text = "0";
                    }
                    SubjectCode = SubjectCode.Substring(0, SubjectCode.Length - 1);
                    SubjectName = SubjectName.Substring(0, SubjectName.Length - 1);
                    string Validity_SDate = "", Validity_EDate = "", Validity_Period = "";
                    Validity_Period = txtValidityPeriod.Value;
                    Validity_SDate = Validity_Period.Substring(0, 10);
                    Validity_EDate = Validity_Period.Substring(13, 10);

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                   
                    //Save Data
                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomSubjectGroup(lblSubGroupStreamCode_Result.Text, ddlSubjectGroupAdd.SelectedValue, Validity_SDate, Validity_EDate, lblSubGroupStreamName_Result.Text, ddlSubjectGroupAdd.SelectedItem.ToString(), lblSubGroupAcadYear_Result.Text, txtFees.Text.Trim(), ddlUOM.SelectedValue, txtMinOrderQty.Text, SubjectCode, SubjectName,CreatedBy, "2");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record");
                        return;
                    }
                    else if (ResultId == "-2")
                    {
                        Show_Error_Success_Box("E", "Validity Period Out Of Range");
                        return;
                    }
                    else
                    {
                        // ControlVisibility("Result");                        
                        Fill_GridSubjetGroup();
                        Show_Error_Success_Box("S", "0000");
                        btnAddSubjectGroup.Visible = true;
                        DivResultSubjectGroup.Visible = true;
                        DivAddSubjectGroup.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnClose_SubGroupToResult_Click(object sender, EventArgs e)
            {
                try
                {
                    ControlVisibility("Result");
                   
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnAddItemLevelPric_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    btnAddItemLevelPric.Visible = false;                    
                    DivResultItemLevel.Visible = false;
                    DivAddItemLevel.Visible = true;
                    ddlSubjectGroup.Enabled = true;
                    btnSaveItemLevel.Visible = true;
                    btnSaveEditItemLevel.Visible = false;
                    lblItemLevelHeader_Add.Text = "Create New Item Level";
                    Clear_ItemLevelPricingAddPanel();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnAddItemHeaderPric_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    btnAddItemHeaderPric.Visible = false;
                    DivResultItemHeader.Visible = false;
                    DivAddItemHeader.Visible = true;
                    //ddlSubjectGroup.Enabled = true;
                    btnSaveItemHeader.Visible = true;
                    btnSaveEditItemHeader.Visible = false;
                    lblItemHeader_Add.Text = "Create New Item Pricing Header";
                    Clear_ItemPricingHeaderAddPanel();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnSaveItemLevel_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    //Validation
                    if (ddlSubjectGroup.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select SubjectGroup");
                        ddlSubjectGroup.Focus();
                        return;
                    }
                    if (ddlVoucherType.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Voucher Type");
                        ddlVoucherType.Focus();
                        return;
                    }
                    if (ddlPayPlan.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Pay Plan");
                        ddlPayPlan.Focus();
                        return;
                    }
                    if (txtVoucherAmount.Text.Trim()=="")
                    {
                        Show_Error_Success_Box("E", "Enter Voucher Amount");
                        txtVoucherAmount.Focus();
                        return;
                    }
                    if (txtItemLevelPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Validity Period");                        
                        return;
                    }

                    string Validity_SDate = "", Validity_EDate = "", Validity_Period = "";
                    Validity_Period = txtItemLevelPeriod.Value;
                    Validity_SDate = Validity_Period.Substring(0, 10);
                    Validity_EDate = Validity_Period.Substring(13, 10);

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                   
                    //Save Data
                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomItemLevelPricing(lblItemLevelStreamCode_Result.Text, ddlSubjectGroup.SelectedValue, ddlVoucherType.SelectedValue, Validity_SDate, Validity_EDate, ddlPayPlan.SelectedValue, txtVoucherAmount.Text.Trim(), lblItemLevelDivisionCode_Result.Text, lblItemLevelStreamName_Result.Text,"", CreatedBy,"1");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record...!");
                        ddlSubjectGroup.Focus();
                        return;
                    }
                    else if (ResultId == "-2")
                    {
                        Show_Error_Success_Box("E", "Validity Period Out of Range..!");                        
                        return;
                    }
                    else
                    {
                        //ControlVisibility("Result");                        
                        Fill_GridItemPricing();
                        Show_Error_Success_Box("S", "0000");
                        btnAddItemLevelPric.Visible = true;
                        DivResultItemLevel.Visible = true;
                        DivAddItemLevel.Visible = false;
                    }


                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }
            protected void btnSaveEditItemLevel_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    //Validation
                    if (ddlSubjectGroup.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select SubjectGroup");
                        ddlSubjectGroup.Focus();
                        return;
                    }
                    if (ddlVoucherType.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Voucher Type");
                        ddlVoucherType.Focus();
                        return;
                    }
                    if (ddlPayPlan.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Pay Plan");
                        ddlPayPlan.Focus();
                        return;
                    }
                    if (txtVoucherAmount.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Voucher Amount");
                        txtVoucherAmount.Focus();
                        return;
                    }
                    if (txtItemLevelPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Validity Period");
                        return;
                    }

                    string Validity_SDate = "", Validity_EDate = "", Validity_Period = "";
                    Validity_Period = txtItemLevelPeriod.Value;
                    Validity_SDate = Validity_Period.Substring(0, 10);
                    Validity_EDate = Validity_Period.Substring(13, 10);


                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                   
                    //Save Data
                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomItemLevelPricing(lblItemLevelStreamCode_Result.Text, ddlSubjectGroup.SelectedValue, ddlVoucherType.SelectedValue, Validity_SDate, Validity_EDate, ddlPayPlan.SelectedValue, txtVoucherAmount.Text.Trim(), lblItemLevelDivisionCode_Result.Text, lblItemLevelStreamName_Result.Text,lblPKeyItemLevel.Text, CreatedBy,"2");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record");
                        ddlSubjectGroup.Focus();
                        return;
                    }
                    else if (ResultId == "-2")
                    {
                        Show_Error_Success_Box("E", "Validity Period Out of Range..!");
                        return;
                    }
                    else
                    {
                        // ControlVisibility("Result");                        
                        Fill_GridItemPricing();
                        Show_Error_Success_Box("S", "0000");
                        btnAddItemLevelPric.Visible = true;
                        DivResultItemLevel.Visible = true;
                        DivAddItemLevel.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnClose_ItemLevetToResult_Click(object sender, EventArgs e)
            {
                try
                {
                    ControlVisibility("Result");
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnSaveItemHeader_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    //Validation
                    if (ddlItemHeaderVoucherType.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Voucher Type");
                        ddlItemHeaderVoucherType.Focus();
                        return;
                    }
                    if (txtItemHeaderValidityPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Validity Period");                        
                        return;
                    }
                    if (txtItemHeaderVoucherAmount.Text == "")
                    {
                        Show_Error_Success_Box("E", "Enter Voucher Amount");
                        txtItemHeaderVoucherAmount.Focus();
                        return;
                    }
                    if (ddlItemPricingHeaderUOM.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Unit Of Measurement");
                        ddlItemPricingHeaderUOM.Focus();
                        return;
                    }
                    if (txtMaterialROB.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Material ROB");
                        txtMaterialROB.Focus();
                        return;
                    }
                    if (txtItemHeaderMinOrdQty.Text.Trim() == "")
                    {
                        txtItemHeaderMinOrdQty.Text = "0";
                    }
                    string Validity_SDate = "", Validity_EDate = "", Validity_Period = "";
                    Validity_Period = txtItemHeaderValidityPeriod.Value;
                    Validity_SDate = Validity_Period.Substring(0, 10);
                    Validity_EDate = Validity_Period.Substring(13, 10);

                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                    //Save Data
                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomItemPricingHeader(lblItemHeaderStreamCode_Result.Text, ddlItemHeaderVoucherType.SelectedValue, Validity_SDate, Validity_EDate, txtItemHeaderVoucherAmount.Text.Trim(),txtMaterialROB.Text.Trim(),ddlItemPricingHeaderUOM.SelectedValue,txtItemHeaderMinOrdQty.Text.Trim(), lblItemHeaderDivisionCode_Result.Text, "",CreatedBy,"1");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record");                        
                        return;
                    }
                    else if (ResultId == "-2")
                    {
                        Show_Error_Success_Box("E", "Validity Period Out of Range");
                        return;
                    }
                    else
                    {
                        // ControlVisibility("Result");                        
                        Fill_GridItemHeader();
                        Show_Error_Success_Box("S", "0000");
                        btnAddItemHeaderPric.Visible = true;
                        DivResultItemHeader.Visible = true;
                        DivAddItemHeader.Visible = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnSaveEditItemHeader_Click(object sender, EventArgs e)
            {
                try
                {
                    Clear_Error_Success_Box();
                    //Validation
                    if (ddlItemHeaderVoucherType.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Voucher Type");
                        ddlItemHeaderVoucherType.Focus();
                        return;
                    }
                    if (txtItemHeaderValidityPeriod.Value == "")
                    {
                        Show_Error_Success_Box("E", "Select Validity Period");
                        return;
                    }
                    if (txtItemHeaderVoucherAmount.Text == "")
                    {
                        Show_Error_Success_Box("E", "Enter Voucher Amount");
                        txtItemHeaderVoucherAmount.Focus();
                        return;
                    }
                    if (ddlItemPricingHeaderUOM.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Unit Of Measurement");
                        ddlItemPricingHeaderUOM.Focus();
                        return;
                    }
                    if (txtMaterialROB.Text.Trim() == "")
                    {
                        Show_Error_Success_Box("E", "Enter Material ROB");
                        txtMaterialROB.Focus();
                        return;
                    }
                    if (txtItemHeaderMinOrdQty.Text.Trim() == "")
                    {
                        txtItemHeaderMinOrdQty.Text = "0";
                    }
                    string Validity_SDate = "", Validity_EDate = "", Validity_Period = "";
                    Validity_Period = txtItemHeaderValidityPeriod.Value;
                    Validity_SDate = Validity_Period.Substring(0, 10);
                    Validity_EDate = Validity_Period.Substring(13, 10);


                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string CreatedBy = cookie.Values["UserID"];
                    //Save Data
                    string ResultId = "";

                    ResultId = ProductController.Insert_Update_ClassRoomItemPricingHeader(lblItemHeaderStreamCode_Result.Text, ddlItemHeaderVoucherType.SelectedValue, Validity_SDate, Validity_EDate, txtItemHeaderVoucherAmount.Text.Trim(), txtMaterialROB.Text.Trim(), ddlItemPricingHeaderUOM.SelectedValue, txtItemHeaderMinOrdQty.Text.Trim(), lblItemHeaderDivisionCode_Result.Text,lblPKeyItemHeader.Text ,CreatedBy, "2");
                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", "Duplicate Record");
                        return;
                    }
                    else if (ResultId == "-2")
                    {
                        Show_Error_Success_Box("E", "Validity Period Out of Range");
                        return;
                    }
                    else
                    {
                        // ControlVisibility("Result");                        
                        Fill_GridItemHeader();
                        Show_Error_Success_Box("S", "0000");
                        btnAddItemHeaderPric.Visible = true;
                        DivResultItemHeader.Visible = true;
                        DivAddItemHeader.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }

            protected void btnClose_PricingHeaderToResult_Click(object sender, EventArgs e)
            {
                try
                {
                    ControlVisibility("Result");
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }
        #endregion



            protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    FillDDL_Center();
                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }



            
}