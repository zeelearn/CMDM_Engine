using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;



public partial class Config_Course : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_Validation();
                ControlVisibility("Search");
                FillDDL_Division();
                FillDDL_Category();
                FillDDL_Board();
                FillDDL_Medium();
                
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
        #endregion

        #region Methods
        
        /// <summary>
        /// Fill drop down list and assign value and Text
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ds"></param>
        /// <param name="txtField"></param>
        /// <param name="valField"></param>
        private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }

        /// <summary>
        /// Fill List box and assign value and Text
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ds"></param>
        /// <param name="txtField"></param>
        /// <param name="valField"></param>
        private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }

        /// <summary>
        /// Clear Error Success Box
        /// </summary>
        private void Clear_Error_Success_Box()
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = false;
            lblSuccess.Text = "";
            lblerror.Text = "";
            UpdatePanelMsgBox.Update();
        }


        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = true;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;             
            }
            else if (Mode == "Result")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = true;
                DivEditPanel.Visible = false;                
            }
            else if (Mode == "Add")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;                
            }
            else if (Mode == "Edit")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = true;              
            }
         
        }
        /// <summary>
        /// Show Error or success box on top base on boxtype and Error code
        /// </summary>
        /// <param name="BoxType">BoxType</param>
        /// <param name="Error_Code">Error_Code</param>
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
        private void Show_Error_Success_Boxpopup(string BoxType, string Error_Code)
        {
            if (BoxType == "E")
            {
                divpopuperror.Visible = true;
                Msg_Success.Visible = false;
                lblpoperror.Text = ProductController.Raise_Error(Error_Code);
                UpdatePanel2.Update();
                
            }
            else
            {
                Msg_Success.Visible = true;
                Msg_Error.Visible = false;
                lblSuccess.Text = ProductController.Raise_Error(Error_Code);
                UpdatePanelMsgBox.Update();
            }
        }
        
        

        
        /// <summary>
        /// Clear Add Panel 
        /// </summary>
        private void ClearAddPanel()
        {
            ddlDivisionAdd.SelectedIndex = 0;
            ddlCCName.SelectedIndex = 0;
            ddlBoardName.SelectedIndex = 0;
            ddlMediumName.SelectedIndex = 0;
            txtAddCourseName.Text = "";
            txtAddCourseDisplayName.Text = "";
            txtAddCourseShortName.Text = "";
            txtAddCourseDescription.Text = "";
            txtCourseSequenceNo.Text = "";
            chkActiveFlag.Checked = true;
            addchkisonline.Checked = false;
        }

        /// <summary>
        /// Clear Edit Panel 
        /// </summary>
        private void ClearEditPanel()
        {
            txtCourseName.Text = "";
            txtEditCourseDisplayName.Text = "";
            txtEditCourseShortName.Text = "";
            txtEditCourseSequenceNo.Text = "";
            txtEditCourseDesc.Text = "";
            chkEditActiveFlag.Checked = true;
            editchkisonline.Checked = true;

        }

        /// <summary>
        /// Clear Add Panel 
        /// </summary>
        private void ClearSearchPanel()
        {           
            ddlDivisionName.SelectedIndex = 0;
            txtCourseName.Text = "";
        }

        /// <summary>
        /// Fill Division drop down list
        /// </summary>
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
                BindDDL(ddlDivisionName, dsDivision, "Division_Name", "Division_Code");
                ddlDivisionName.Items.Insert(0, "Select");
                ddlDivisionName.SelectedIndex = 0;
                BindDDL(ddlDivisionAdd, dsDivision, "Division_Name", "Division_Code");
                ddlDivisionAdd.Items.Insert(0, "Select");
                ddlDivisionAdd.SelectedIndex = 0;
                BindDDL(ddlDivisionEdit, dsDivision, "Division_Name", "Division_Code");
                ddlDivisionEdit.Items.Insert(0, "Select");
                ddlDivisionEdit.SelectedIndex = 0;
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


        /// <summary>
        /// Fill Medium drop down list
        /// </summary>
        private void FillDDL_Medium()
        {

            try
            {

                Clear_Error_Success_Box();
               
                DataSet dsMedium = ProductController.Get_Medium_Details("%%");
                BindDDL(ddlMediumName, dsMedium, "MediumName", "Id");
                ddlMediumName.Items.Insert(0, "Select");
                ddlMediumName.SelectedIndex = 0;
                BindDDL(ddlMediumEdit, dsMedium, "MediumName", "Id");
                ddlMediumEdit.Items.Insert(0, "Select");
                ddlMediumEdit.SelectedIndex = 0;
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

        /// <summary>
        /// Fill Board drop down list
        /// </summary>
        private void FillDDL_Board()
        {
            try
            {
                Clear_Error_Success_Box();

                DataSet dsMedium = ProductController.GetBoardDetails("", "", "3");
                BindDDL(ddlBoardName, dsMedium, "Long_Description", "Id");
                ddlBoardName.Items.Insert(0, "Select");
                ddlBoardName.SelectedIndex = 0;
                BindDDL(ddlBoardEdit, dsMedium, "Long_Description", "Id");
                ddlBoardEdit.Items.Insert(0, "Select");
                ddlBoardEdit.SelectedIndex = 0;

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

        /// <summary>
        /// Fill Category drop down list
        /// </summary>
        private void FillDDL_Category()
        {
            try
            {
                Clear_Error_Success_Box();

                DataSet dsCourseCat = ProductController.Get_CourseCategory("%%",1);
                BindDDL(ddlCCName, dsCourseCat, "CourseCategoryName", "CourseCategoryCode");
                ddlCCName.Items.Insert(0, "Select");
                ddlCCName.SelectedIndex = 0;
                BindDDL(ddlCCEdit, dsCourseCat, "CourseCategoryName", "CourseCategoryCode");
                ddlCCEdit.Items.Insert(0, "Select");
                ddlCCEdit.SelectedIndex = 0;
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

        /// <summary>
        /// Bind Datalist
        /// </summary>
        private void FillGrid(string CourseName, string DivisionCode)
        {
            try
            {
                Clear_Error_Success_Box();
               
                ControlVisibility("Result");

                DataSet dsGrid = ProductController.Get_CourseDetail(CourseName, "", "", "", DivisionCode, "", "1", "");
                

                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {
                            dlGridDisplay.DataSource = dsGrid;
                            dlGridDisplay.DataBind();

                            DataList1.DataSource = dsGrid;
                            DataList1.DataBind();
                            
                            lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();

                            
                        }
                        else
                        {
                            dlGridDisplay.DataSource = null;
                            dlGridDisplay.DataBind();

                            DataList1.DataSource = null;
                            DataList1.DataBind();

                            lbltotalcount.Text = "0";
                        }
                    }
                    else
                    {
                        dlGridDisplay.DataSource = null;
                        dlGridDisplay.DataBind();

                        DataList1.DataSource = null;
                        DataList1.DataBind();

                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();

                    DataList1.DataSource = null;
                    DataList1.DataBind();

                    lbltotalcount.Text = "0";
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


        /// <summary>
        /// Insert data
        /// </summary>
        private void SaveData()
        {
            try
            {
                Clear_Error_Success_Box();
                if (ddlDivisionAdd.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0001");
                    ddlDivisionAdd.Focus();
                    return;
                }
                if (ddlCCName.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Course Category");
                    ddlCCName.Focus();
                    return;
                }
                if (ddlBoardName.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Board");
                    ddlBoardName.Focus();
                    return;
                }
                if (ddlMediumName.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Medium");
                    ddlMediumName.Focus();
                    return;
                }
                if (txtAddCourseName.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Course Name");
                    txtAddCourseName.Focus();
                    return;
                }

                if (txtAddCourseName.MaxLength > 150)
                {
                    Show_Error_Success_Box("E", "Course Name Max Length(150 Characters)");
                    txtAddCourseName.Focus();
                    return;
                }
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;

                int ActiveFlag = 0;
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = 1;
                else
                    ActiveFlag = 0;

                int OnlineFlag = 0;
                if (addchkisonline.Checked == true)
                    OnlineFlag = 1;
                else
                    OnlineFlag = 0;
                
                int ResultId = 0;
                //ResultId = ProductController.InsertUpdateCourseDetail("", ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlDivisionAdd.SelectedValue, txtAddCourseName.Text, txtAddCourseDisplayName.Text, txtAddCourseShortName.Text, txtAddCourseDescription.Text, txtCourseSequenceNo.Text, ActiveFlag, CreatedBy, "1",OnlineFlag,"");
                //if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
                //{
                //    Show_Error_Success_Box("E", "Duplicate Course Name");
                //    return;
                //}
                //else
                //{
                //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                //    string Return_Pkey_CRM = ms.CreateCourse(ResultId.Tables[0].Rows[0]["CourseCode"].ToString(), ResultId.Tables[0].Rows[0]["CourseName"].ToString(), ResultId.Tables[0].Rows[0]["RecordNumber"].ToString(), ResultId.Tables[0].Rows[0]["DivisionCode"].ToString(), ResultId.Tables[0].Rows[0]["Medium"].ToString(), ResultId.Tables[0].Rows[0]["CourseDisplayName"].ToString(), ResultId.Tables[0].Rows[0]["CourseShortName"].ToString(), ResultId.Tables[0].Rows[0]["CourseDescription"].ToString(), ResultId.Tables[0].Rows[0]["IsOnline"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString(), ResultId.Tables[0].Rows[0]["CourseCategory"].ToString(), ResultId.Tables[0].Rows[0]["Board"].ToString());
                //    string CourseCode = ResultId.Tables[0].Rows[0]["CourseCode"].ToString();

                //    ResultId = null;
                //    ResultId = ProductController.InsertUpdateCourseDetail(CourseCode, ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlDivisionAdd.SelectedValue, txtAddCourseName.Text, txtAddCourseDisplayName.Text, txtAddCourseShortName.Text, txtAddCourseDescription.Text, txtCourseSequenceNo.Text, ActiveFlag, CreatedBy, "3", OnlineFlag, Return_Pkey_CRM);
                //    FillGrid("%%", ddlDivisionAdd.SelectedValue);
                //    Show_Error_Success_Box("S", "Record saved Successfully.");
                //}

                ResultId = ProductController.InsertUpdateCourseDetail("", ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlDivisionAdd.SelectedValue, txtAddCourseName.Text, txtAddCourseDisplayName.Text, txtAddCourseShortName.Text, txtAddCourseDescription.Text, txtCourseSequenceNo.Text, ActiveFlag, CreatedBy, "1", OnlineFlag);
                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Duplicate Course Name");
                    return;
                }
                else
                {

                    FillGrid("%%", ddlDivisionAdd.SelectedValue);
                    Show_Error_Success_Box("S", "Record saved Successfully.");
                    Send_Details_LMSBroad(ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlDivisionAdd.SelectedValue, txtAddCourseName.Text);
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

        /// <summary>
        /// Update data
        /// </summary>
        private void UpdateData()
        {
            try
            {
                Clear_Error_Success_Box();

                if (ddlDivisionEdit.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0001");
                    ddlDivisionEdit.Focus();
                    return;
                }
                if (ddlCCEdit.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Course Category");
                    ddlCCEdit.Focus();
                    return;
                }
                if (ddlBoardEdit.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Board");
                    ddlBoardEdit.Focus();
                    return;
                }
                if (ddlMediumEdit.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Medium");
                    ddlMediumEdit.Focus();
                    return;
                }
                if (txtEditCourseName.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Course Name");
                    txtEditCourseName.Focus();
                    return;
                }
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];

                int ActiveFlag = 0;
                if (chkEditActiveFlag.Checked == true)
                    ActiveFlag = 1;
                else
                    ActiveFlag = 0;

                int OnlineFlag = 0;
                if (editchkisonline.Checked == true)
                    OnlineFlag = 1;
                else
                    OnlineFlag = 0;
                
                int ResultId = 0;
                //ResultId = ProductController.InsertUpdateCourseDetail(lblPkey.Text, ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlDivisionEdit.SelectedValue, txtEditCourseName.Text, txtEditCourseDisplayName.Text, txtEditCourseShortName.Text, txtEditCourseDesc.Text, txtEditCourseSequenceNo.Text, ActiveFlag, UserID, "2", OnlineFlag,"");
                                
                //if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
                //{
                //    Show_Error_Success_Box("E", "Duplicate Course Name");
                //    return;
                //}
                //else
                //{


                //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                //    string Return_Pkey_CRM = ms.CreateCourse(ResultId.Tables[0].Rows[0]["CourseCode"].ToString(), ResultId.Tables[0].Rows[0]["CourseName"].ToString(), ResultId.Tables[0].Rows[0]["RecordNumber"].ToString(), ResultId.Tables[0].Rows[0]["DivisionCode"].ToString(), ResultId.Tables[0].Rows[0]["Medium"].ToString(), ResultId.Tables[0].Rows[0]["CourseDisplayName"].ToString(), ResultId.Tables[0].Rows[0]["CourseShortName"].ToString(), ResultId.Tables[0].Rows[0]["CourseDescription"].ToString(), ResultId.Tables[0].Rows[0]["IsOnline"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString(), ResultId.Tables[0].Rows[0]["CourseCategory"].ToString(), ResultId.Tables[0].Rows[0]["Board"].ToString());
                   

                //    ResultId = null;
                //    ResultId = ProductController.InsertUpdateCourseDetail(lblPkey.Text, ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlDivisionEdit.SelectedValue, txtEditCourseName.Text, txtEditCourseDisplayName.Text, txtEditCourseShortName.Text, txtEditCourseDesc.Text, txtEditCourseSequenceNo.Text, ActiveFlag, UserID, "3", OnlineFlag, Return_Pkey_CRM);
                    
                //    FillGrid("%%", ddlDivisionEdit.SelectedValue);
                //    Show_Error_Success_Box("S", "Record Update Successfully.");
                //}


                ResultId = ProductController.InsertUpdateCourseDetail(lblPkey.Text, ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlDivisionEdit.SelectedValue, txtEditCourseName.Text, txtEditCourseDisplayName.Text, txtEditCourseShortName.Text, txtEditCourseDesc.Text, txtEditCourseSequenceNo.Text, ActiveFlag, UserID, "2", OnlineFlag);

                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Duplicate Course Name");
                    return;
                }
                else
                {

                    FillGrid("%%", ddlDivisionEdit.SelectedValue);
                    Show_Error_Success_Box("S", "Record Update Successfully.");
                    Send_Details_LMSBroad(ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlDivisionEdit.SelectedValue, txtEditCourseName.Text);
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

        #endregion


       
        #region Event's
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            
            ControlVisibility("Add");
            ClearAddPanel();
            Clear_Error_Success_Box();
        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
            Clear_Error_Success_Box();
        }

        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {
            if (ddlDivisionAdd.SelectedIndex == 0)
            {
                if (ddlDivisionName.SelectedIndex == 0)
                {
                    ControlVisibility("Search");
                }
                else
                    FillGrid("%%", ddlDivisionName.SelectedValue);
            }
            else
                FillGrid("%%", ddlDivisionAdd.SelectedValue);
        }

        protected void btnEditClose_Click(object sender, EventArgs e)
        {
            if (ddlDivisionEdit.SelectedIndex == 0)
            {
                if (ddlDivisionName.SelectedIndex == 0)
                {
                    ControlVisibility("Search");
                }
                else
                    FillGrid("%%", ddlDivisionName.SelectedValue);
            }
            else
                FillGrid("%%", ddlDivisionEdit.SelectedValue);
        }

        protected void BtnSaveAdd_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlDivisionName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivisionName.Focus();
                return;
            }
            string CourseName="";
            if (string.IsNullOrEmpty(txtCourseName.Text.Trim()))
            {
               CourseName="%%";
            }
            else
                CourseName=txtCourseName.Text.Trim();
                FillGrid(CourseName, ddlDivisionName.SelectedValue);
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearchPanel();
        }


        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "comEdit")
            {
                Clear_Error_Success_Box();
                ControlVisibility("Edit");
                ClearEditPanel();
                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.Get_CourseDetail("", "", "", "", "", lblPkey.Text, "2","");
                    
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDivisionEdit.SelectedValue = ds.Tables[0].Rows[0]["Division_Code"].ToString();
                    ddlCCEdit.SelectedValue = ds.Tables[0].Rows[0]["CourseCateogryId"].ToString();
                    ddlBoardEdit.SelectedValue = ds.Tables[0].Rows[0]["Board_ID"].ToString();
                    ddlMediumEdit.SelectedValue = ds.Tables[0].Rows[0]["Medium_ID"].ToString();
                    txtEditCourseName.Text = ds.Tables[0].Rows[0]["Course_Name"].ToString();
                    txtEditCourseDisplayName.Text = ds.Tables[0].Rows[0]["Course_Display_Name"].ToString();
                    txtEditCourseShortName.Text = ds.Tables[0].Rows[0]["Course_Short_Name"].ToString();
                    txtEditCourseSequenceNo.Text = ds.Tables[0].Rows[0]["CourseSequenceNo"].ToString();
                    txtEditCourseDesc.Text = ds.Tables[0].Rows[0]["Course_Description"].ToString();
                    if (ds.Tables[0].Rows[0]["Is_Active"].ToString() == "0")
                        chkEditActiveFlag.Checked = false;
                    else
                        chkEditActiveFlag.Checked = true;
                    if (ds.Tables[0].Rows[0]["Is_Online"].ToString() == "0")
                        editchkisonline.Checked = false;
                    else
                        editchkisonline.Checked = true;
                }
            }
            if (e.CommandName == "comCopy")
            {

                lblCourse_Code.Text = e.CommandArgument.ToString();
                txtCourse.Text = "";
                txtCourseDisplay.Text = "";
                divpopuperror.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDivCOnfirmationCopyCourse();", true);
                
            }

            if (e.CommandName == "AddSubIcon")
            {
                
                lblCourse_Code.Text = e.CommandArgument.ToString();
                string pk = null;
                pk = lblCourse_Code.Text;
                Session["lblCourse_Code.Text"] = pk;
                Response.Redirect("Master_Subject_Icon.aspx", false);

            }
        }

        protected void btnEditSave_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        #endregion            
          
        protected void HLExport_Click(object sender, EventArgs e)
        {
            DataList1.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Course_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='12'>Course</b></TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            DataList1.RenderControl(oHtmlTextWriter1);
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();

            DataList1.Visible = false;
        }
        protected void btnuploadviexcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Course_Master.aspx");
        }
        protected void dlGridDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCourse.Text.Trim() == "")
                {
                    Show_Error_Success_Boxpopup("E", "Enter Course Name");
                    txtCourse.Focus();
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDivCOnfirmationCopyCourse();", true);
                  
                    return;
                }
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];
                string Result = ProductController.Insert_Copyof_ReferenceCourse(lblCourse_Code.Text, UserID, txtCourse.Text, txtCourseDisplay.Text);
                if (Result == "1")
                {
                    BtnSearch_Click(this, e);
                    Show_Error_Success_Box("S", "Copy of this Course Created Successfully");
                }
                else if (Result == "-1")
                {
                    Show_Error_Success_Boxpopup("E", "Duplicate Course Name");
                    txtCourse.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDivCOnfirmationCopyCourse();", true);
                }
            }
            catch (Exception EX)
            {
            }
        }


        private void Send_Details_LMSBroad(string Coursecategory,string Borad, string Meadium, string Division, string Coursename)
        {
            string Response_Status_Code = "";
            string Response_Return_Phrase = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            try
            {
                DataSet dsdetails = ProductController.GET_Course_DETAILS(Coursecategory, Borad, Meadium, Division,Coursename);
                if (dsdetails.Tables[0].Rows.Count > 0)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(DBConnection.connStringLMS);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Coursedetailsinsert = new Courselist();
                    Coursedetailsinsert.CourseId = "null";
                    Coursedetailsinsert.CourseCategoryCode = dsdetails.Tables[0].Rows[0]["CourseCateogryId"].ToString();
                    Coursedetailsinsert.BoardCode = dsdetails.Tables[0].Rows[0]["Board_ID"].ToString();
                    Coursedetailsinsert.MediumCode = dsdetails.Tables[0].Rows[0]["Medium_ID"].ToString();
                    Coursedetailsinsert.DivisionCode = dsdetails.Tables[0].Rows[0]["Division_Code"].ToString();
                    Coursedetailsinsert.CourseCode = dsdetails.Tables[0].Rows[0]["Course_Code"].ToString();
                    Coursedetailsinsert.CourseName = dsdetails.Tables[0].Rows[0]["Course_Name"].ToString();
                    Coursedetailsinsert.CourseDisplayName = dsdetails.Tables[0].Rows[0]["Course_Display_Name"].ToString();
                    Coursedetailsinsert.CourseShortName = dsdetails.Tables[0].Rows[0]["Course_Short_Name"].ToString();
                    Coursedetailsinsert.CourseDescription = dsdetails.Tables[0].Rows[0]["Course_Description"].ToString();
                    Coursedetailsinsert.CourseSequenceNo = 1;
                    Coursedetailsinsert.CourseHierarchyCode ="CH03";
                    Coursedetailsinsert.Is_Online = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Is_Online"]);
                    Coursedetailsinsert.FreeDuration = 0;
                    Coursedetailsinsert.FreeVideo = null;
                    Coursedetailsinsert.FreeTest = null;
                    Coursedetailsinsert.Version = "";
                    Coursedetailsinsert.Reference_Course = "";
                    Coursedetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                    Coursedetailsinsert.CreatedBy = UserID;
                    Coursedetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Modified_On"]);
                    Coursedetailsinsert.ModifiedBy = UserID;
                    Coursedetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Is_Active"]);
                    Coursedetailsinsert.IsDeleted = false;
                    Coursedetailsinsert.CoursePromoCode = "";
                    Coursedetailsinsert.ShareQuestionCourse = "";
                    Coursedetailsinsert.CourseURLName = "";
                    Coursedetailsinsert.CName = "";


                    var response = client.PostAsJsonAsync("Course/addUpdCourse", Coursedetailsinsert).Result;

                    Response_Status_Code = response.StatusCode.ToString();
                    Response_Return_Phrase = response.ReasonPhrase;

                    if (response.StatusCode.ToString() == "OK")
                    {
                    }
                    else
                    {
                      
                    }
                }



            }
            catch (Exception e)
            {
                
            }
        }
        class Courselist
        {
            public string CourseId { get; set; }
            public string CourseCategoryCode { get; set; }
            public string BoardCode { get; set; }
            public string MediumCode { get; set; }
            public string DivisionCode { get; set; }
            public string CourseCode { get; set; }
            public string CourseName { get; set; }
            public string CourseDisplayName { get; set; }
            public string CourseShortName { get; set; }
            public string CourseDescription { get; set; }
            public int CourseSequenceNo { get; set; }
            public string CourseHierarchyCode { get; set; }
            public Boolean Is_Online { get; set; }
            public int FreeDuration { get; set; }
            public int? FreeVideo { get; set; }
            public int? FreeTest { get; set; }
            public string Version { get; set; }
            public string Reference_Course { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime ModifiedOn { get; set; }
            public string ModifiedBy { get; set; }
            public Boolean IsActive { get; set; }
            public Boolean IsDeleted { get; set; }
            public string CoursePromoCode { get; set; }
            public string ShareQuestionCourse { get; set; }
            public string CourseURLName { get; set; }
            public string CName { get; set; }

        }

    
}
