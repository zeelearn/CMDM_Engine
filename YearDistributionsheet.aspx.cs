using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using LMSIntegration;



public partial class YearDistributionsheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            //UPDATETEACHERSteaching();
            //TEACHERS_Chapter_Details("", "", "", "", "", "", "", "");
        }
    }


    #region Methods

    /// <summary>
    /// Visible panel base on Mode
    /// </summary>
    /// <param name="Mode">Mode</param>
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = true;

        }
        Clear_Error_Success_Box();
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
            BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
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


    private void FillDDL_Batch()
    {
        try
        {
            ddlBatch.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string ProductCode = null;
            ProductCode = ddlLMSProduct.SelectedValue;


            string Centre_Code = ddlCenter_Add.SelectedValue;


            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, Centre_Code, "1");
            BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");

            ListItem listItem = new ListItem();
            listItem.Text = "All Batch";
            listItem.Value = "0";
            ddlBatch.Items.Insert(0, listItem);

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
    /// Bind  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();

            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }


            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0002");
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlCourse.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlCourse.Focus();
                return;
            }


            if (ddlLMSProduct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select LMS Product");
                ddlLMSProduct.Focus();
                return;
            }



            if (ddlSubjectName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0005");
                ddlSubjectName.Focus();
                return;
            }

            //if (ddlSchHorizon.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "Select Scheduling Horizon");
            //    ddlSchHorizon.Focus();
            //    return;
            //}
            string Centre_Code = "";
            string Centre_Name = "";
            int CentreCnt = 0;
            int CentreSelCnt = 0;
            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    CentreSelCnt = CentreSelCnt + 1;
                }
            }

            if (CentreSelCnt == 0)
            {
                //When all is selected
                //for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                //{
                //    Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Text + ",";
                //}               

                //Centre_Code = Common.RemoveComma(Centre_Code);

                Show_Error_Success_Box("E", "0006");
                ddlCenters.Focus();
                return;

            }
            else
            {
                for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                {
                    if (ddlCenters.Items[CentreCnt].Selected == true)
                    {
                        Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                        Centre_Name = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                    }
                }
                Centre_Code = Common.RemoveComma(Centre_Code);
                Centre_Name = Common.RemoveComma(Centre_Name);
            }


            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubjectName.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcademicYear.SelectedValue;


            string LmsProduct = "";
            LmsProduct = ddlLMSProduct.SelectedValue;

            DataSet dsGrid = null;
            dsGrid = ProductController.GetYearDistributionsheetBy_Division_Year_Standard_Subject(DivisionCode, AcademicYear, StandardCode, SubjectCode, Centre_Code, LmsProduct, ddlSchHorizon.SelectedValue);


            grvChapter.DataSource = null;
            grvChapter.DataBind();
            grvChapter.Columns.Clear();



            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        DivChapter.Style.Add("width", (280 + (80 * dsGrid.Tables[0].Columns.Count)).ToString());
                        DivChapter.Style.Add("padding-top", "25px");
                        divBottom.Visible = true;
                        int ColCnt = 0;
                        foreach (DataColumn col in dsGrid.Tables[0].Columns)
                        {
                            //Declare the bound field and allocate memory for the bound field.
                            BoundField bfield = new BoundField();

                            //Initalize the DataField value.
                            bfield.DataField = col.ColumnName;
                            bfield.HeaderText = col.ColumnName;

                            if (ColCnt == 0)
                            {
                                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                                bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                                bfield.ItemStyle.Width = Unit.Pixel(200); //"200";

                                //table table-striped table-bordered table-hover

                            }
                            else
                            {
                                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                                bfield.ItemStyle.CssClass = "gridtext";
                                bfield.HeaderStyle.CssClass = "gridtext";
                                //testSpace.Attributes.Add("style", "text-align: center;");

                            }

                            //Add the newly created bound field to the GridView.
                            grvChapter.Columns.Add(bfield);
                            ColCnt = ColCnt + 1;
                        }




                        grvChapter.DataSource = dsGrid.Tables[0];
                        grvChapter.DataBind();
                        lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                        //int cellCount = this.grvChapter.Rows[0].Cells.Count;
                        //int rowsCount = this.grvChapter.Rows.Count;
                        //for (int j = 0; j < rowsCount; j++)
                        //{
                        //    for (int i = 2; i < cellCount; i++)
                        //    {
                        //        TextBox textBox = new TextBox();
                        //        textBox.ID = "txtCenter_R" + j.ToString() + "_C"  + i.ToString();
                        //        textBox.Style.Add("width", "50px");                                
                        //        this.grvChapter.Rows[j].Cells[i].Controls.Add(textBox);
                        //    }
                        //}
                    }
                    else
                    {
                        divBottom.Visible = false;
                    }
                }
                else
                {
                    divBottom.Visible = false;
                }
            }
            else
            {
                divBottom.Visible = false;
            }

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();
            lblSubject_Result.Text = ddlSubjectName.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCenter_Result.Text = Centre_Name;
            lblLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString();
            lblSchedulingHorizon_Result.Text = ddlSchHorizon.SelectedItem.ToString();
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
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_Standard()
    {

        try
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;
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
    /// Fill Subject dropdown
    /// </summary>
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubjectName, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubjectName.Items.Insert(0, "Select");
            ddlSubjectName.SelectedIndex = 0;
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
    /// Fill Academic Year dropdown
    /// </summary>
    private void FillDDL_AcadYear()
    {
        try
        {

            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            ddlAcademicYear.Items.Insert(0, "Select");
            ddlAcademicYear.SelectedIndex = 0;
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
    /// Fill Centers Based on login user 
    /// </summary>
    private void FillDDL_Centre()
    {
        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, Div_Code, "", "5", DBname);

        BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");


    }


    #endregion

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();


    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();
        FillDDL_SchedulingHorizon();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlCenters.Items.Clear();
        ddlCourse.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlSubjectName.Items.Clear();
        Clear_Error_Success_Box();
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        // Save Year Distribution of each row in the datalistf  

        try
        {
            Clear_Error_Success_Box();

            if (ddlBatch.SelectedValue != "")
            {

                string DivisionCode = ddlDivision.SelectedValue;
                string Acad_Year = ddlAcademicYear.SelectedItem.Text;
                string Standard_Code = ddlCourse.SelectedValue;
                string LMSProductCode = ddlLMSProduct.SelectedValue;
                string SchedulHorizonTypeCode = ddlSchHorizon.SelectedValue;
                string CenterCode = ddlCenter_Add.SelectedValue;
                string SubjectCode = ddlSubjectName.SelectedValue;
                string BatchCode = "0";

                //int BatchSelCnt = 0;
                //for (BatchSelCnt = 0; BatchSelCnt <= ddlBatch.Items.Count - 1; BatchSelCnt++)
                //{
                //    if (ddlBatch.Items[BatchSelCnt].Selected == true)
                //    {
                //        BatchCode = BatchCode + ddlBatch.Items[BatchSelCnt].Value + ",";

                //    }
                //}
                //BatchCode = Common.RemoveComma(BatchCode);


                BatchCode = ddlBatch.SelectedValue;


                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                //CreatedBy = lblHeader_User_Code.Text;
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                CreatedBy = cookie.Values["UserID"];
                var partnercodes = new List<string>();
                foreach (DataListItem dtlItem in dlGridChapter.Items)
                {
                    //TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");
                    ListBox lstTeacherName = (ListBox)dtlItem.FindControl("ddlTeacher");
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
                    if (chkitemck.Checked == true)
                    {

                        int TeacherCnt = 0;
                        string Teacher_Code = "";

                        for (TeacherCnt = 0; TeacherCnt <= lstTeacherName.Items.Count - 1; TeacherCnt++)
                        {
                            if (lstTeacherName.Items[TeacherCnt].Selected == true)
                            {
                                Teacher_Code = Teacher_Code + lstTeacherName.Items[TeacherCnt].Value + ",";
                                if (partnercodes.Contains(lstTeacherName.Items[TeacherCnt].Value))
                                {
                                }
                                else
                                {
                                    partnercodes.Add(lstTeacherName.Items[TeacherCnt].Value);
                                }

                            }
                        }
                        Teacher_Code = Common.RemoveComma(Teacher_Code);
                        Label lblDLLectureCnt = (Label)dtlItem.FindControl("lblDLLectureCnt");
                        Label lblDLChapterCode = (Label)dtlItem.FindControl("lblDLChapterCode");
                        Label lblResult = (Label)dtlItem.FindControl("lblResult");
                         string chapterCode="";
                        chapterCode=lblDLChapterCode.Text;
                        lblResult.Text = "";
                        int ResultId = 0;
                        //if (Teacher_Code.Trim() != "")
                        //{
                        ResultId = ProductController.Insert_YearDistribution(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, CenterCode, SubjectCode, lblDLChapterCode.Text, Teacher_Code, CreatedBy, BatchCode);

                        if (ResultId == 1)
                        {

                            lblResult.ForeColor = System.Drawing.Color.Green;
                            lblResult.Text = "Success";
                        }
                        if (ResultId == -1)
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Error : Invalid Faculty code";
                        }

                        if (ResultId == -3)
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Error : Faculty is already utilize in TimeTable";
                        }

                        if (ResultId == 0)
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Error :Data not saved";

                        }
                    }
                    //}
                    //else
                    //{
                    //    lblResult.Text = "";
                    //}
                }

                //  int c= partnercodes.Count;
                foreach (var partners in partnercodes)
                {
                    Send_Details_LMS(partners, CenterCode, LMSProductCode, SubjectCode, Acad_Year);
                    TEACHERS_Chapter_Details(DivisionCode, Standard_Code, partners, CenterCode, LMSProductCode, SubjectCode, BatchCode, Acad_Year);
                   

                }
            }
            else
            {

                Show_Error_Success_Box("E", "Please Select Batch");
                ddlBatch.Focus();
                return;
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }



    private void Send_Details_LMS(string Partner_Code, string Center_Code, string Product_Code, string Subject_Code, string Acad_Year)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        DateTime datetime = DateTime.Now;

        try
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DBConnection.connStringLMS);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var teacherteachingdetails = new teacherteachingdetails();
            teacherteachingdetails.TeacherCode = Partner_Code;
            teacherteachingdetails.CenterCode = Center_Code;
            teacherteachingdetails.ProductCode = Product_Code;
            teacherteachingdetails.SubjectCode = Subject_Code;
            teacherteachingdetails.CreatedOn = datetime;
            teacherteachingdetails.CreatedBy = UserID;
            teacherteachingdetails.ModifiedOn = datetime;
            teacherteachingdetails.ModifiedBy = UserID;
            teacherteachingdetails.IsActive = true;
            teacherteachingdetails.IsDeleted = false;

            var response = client.PostAsJsonAsync("teacher/addUpdTeacherTeachingDetails", teacherteachingdetails).Result;

            Response_Status_Code = response.StatusCode.ToString();
            Response_Return_Phrase = response.ReasonPhrase;

            if (response.StatusCode.ToString() == "OK")
            {


                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, 1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);


            }
            else
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }




        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());

            DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
            Response_Status_Code, Response_Return_Phrase, UserID);
        }
    }
    class teacherteachingdetails
    {
        public string TeacherCode { get; set; }
        public string CenterCode { get; set; }
        public string ProductCode { get; set; }
        public string SubjectCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }


    }


    ///add on syn chapterCode
    ///
  
 

    private void Send_Chapter_Details_LMS(string Division_Code,string Course_Code,string Partner_Code, string Center_Code, string Product_Code, string Subject_Code,string ChapterCode,string BatchCode, string ActivityId, int IsActive, string Acad_Year)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        DateTime datetime = DateTime.Now;

        try
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DBConnection.connStringLMS);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var teacherChapterdetails = new teacherChapterdetails();

            teacherChapterdetails.DivisionCode = Division_Code;
            teacherChapterdetails.CourseCode = Course_Code;
            teacherChapterdetails.TeacherCode = Partner_Code;
            teacherChapterdetails.CenterCode = Center_Code;
            teacherChapterdetails.ProductCode = Product_Code;
            teacherChapterdetails.SubjectCode = Subject_Code;
            teacherChapterdetails.ChapterCode = ChapterCode;
            teacherChapterdetails.BatchCode = BatchCode;
            teacherChapterdetails.ActivityId = ActivityId;
            teacherChapterdetails.IsActive = IsActive;
            teacherChapterdetails.AcadYear = Acad_Year;
            teacherChapterdetails.CreatedOn = datetime;
            teacherChapterdetails.CreatedBy = UserID;
            teacherChapterdetails.ModifiedOn = datetime;
            teacherChapterdetails.ModifiedBy = UserID;
  

            var response = client.PostAsJsonAsync("teacher/addUpdTeacherChapterDetails", teacherChapterdetails).Result;

            Response_Status_Code = response.StatusCode.ToString();
            Response_Return_Phrase = response.ReasonPhrase;

            if (response.StatusCode.ToString() == "OK")
            {


                DataSet dsreturn1 = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(3, 1, Division_Code + '%' + Course_Code + '%' + Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year + '%' + ChapterCode + '%' + BatchCode,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);


            }
            else
            {
                //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
                //response.StatusCode.ToString(), response.ReasonPhrase, UserID);
                DataSet dsreturn1 = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(3, -1, Division_Code + '%' + Course_Code + '%' + Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year + '%' + ChapterCode + '%' + BatchCode,
             response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }




        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());

            //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
            //Response_Status_Code, Response_Return_Phrase, UserID);
        }
    }
    class teacherChapterdetails
    {
        public string DivisionCode { get; set; }
        public string CourseCode { get; set; }
        public string TeacherCode { get; set; }
        public string CenterCode { get; set; }
        public string ProductCode { get; set; }
        public string SubjectCode { get; set; }
        public string ChapterCode { get; set; }
        public string BatchCode { get; set; }
        public string ActivityId { get; set; }
        public int IsActive { get; set; }
        public string AcadYear { get; set; }
              
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        

       

    }


    ///


    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        FillGrid();
        btnCopyfaculty.Visible = false;
        dlGridChapter.DataSource = null;
        dlGridChapter.DataBind();
        divBottom.Visible = false;
    }

    protected void BtnAssign_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        ddlCenter_Add.Items.Clear();
        int CentreCnt;
        for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
        {
            if (ddlCenters.Items[CentreCnt].Selected == true)
            {

                ddlCenter_Add.Items.Add(new ListItem(ddlCenters.Items[CentreCnt].Text, ddlCenters.Items[CentreCnt].Value));
            }
        }
        divBottom.Visible = false;
        ddlCenter_Add.Items.Insert(0, "Select");
        ddlCenter_Add.SelectedIndex = 0;

        lblDivision_Add.Text = lblDivision_Result.Text;
        lblAcadYear_Add.Text = lblAcademicYear_Result.Text;
        lblCourse_Add.Text = lblCourse_Result.Text;
        lblLMSProduct_Add.Text = lblLMSProduct_Result.Text;
        lblSubject_Add.Text = lblSubject_Result.Text;
        lblSchedulingHorizon_Add.Text = lblSchedulingHorizon_Result.Text;
        dlGridChapter.DataSource = null;
        dlGridChapter.DataBind();

        ddlBatch.Items.Clear();

        divBottom.Visible = true;
        BtnSaveAdd.Visible = false;
        BtnCloseAdd.Visible = true;

    }

    protected void ddlCenter_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCenter_Add.SelectedItem.Value != "Select")
        {
            FillDDL_Batch();
            //if (ddlBatch.Items.Count == 1)
            //{
            Fill_Assign_Teacher();
            btnCopyfaculty.Visible = true;
            // }
        }
        else
        {
            BtnSaveAdd.Visible = false;
            dlGridChapter.DataSource = null;
            dlGridChapter.DataBind();
        }


        //string DivisionCode = null;
        //DivisionCode = ddlDivision.SelectedValue;

        //string StandardCode = "";
        //StandardCode = ddlCourse.SelectedValue;

        //string AcedYear = "";
        //AcedYear = ddlAcademicYear.SelectedItem.Text;
        //divBottom.Visible = true;



        //if (ddlCenter_Add.SelectedItem.Value != "Select")
        //{
        //    FillDDL_Batch();



        //    DataSet dsGrid = ProductController.GetYearDistributionsheetBy_Division_Year_Standard_Subject_Center(DivisionCode, AcedYear, ddlSubjectName.SelectedItem.Value, ddlCenter_Add.SelectedItem.Value, ddlLMSProduct.SelectedItem.Value, ddlSchHorizon.SelectedItem.Value);

        //    if (dsGrid != null)
        //    {
        //        if (dsGrid.Tables.Count != 0)
        //        {
        //            BtnSaveAdd.Visible = true;
        //            dlGridChapter.DataSource = dsGrid;
        //            dlGridChapter.DataBind();


        //            if (dsGrid.Tables[1].Rows.Count > 0)
        //            {
        //                for (int cnt = 0; cnt <= dsGrid.Tables[1].Rows.Count - 1; cnt++)
        //                {
        //                    for (int rcnt = 0; rcnt <= ddlBatch.Items.Count - 1; rcnt++)
        //                    {
        //                        if (ddlBatch.Items[rcnt].Value == dsGrid.Tables[1].Rows[cnt]["Batch_Code"].ToString())
        //                        {
        //                            ddlBatch.Items[rcnt].Selected = true;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }

        //            foreach (DataListItem dtlItem in dlGridChapter.Items)
        //            {
        //                ListBox ddlTeacher = (ListBox)dtlItem.FindControl("ddlTeacher");
        //                Label lblTeacher = (Label)dtlItem.FindControl("lblParnerCode");
        //                ddlTeacher.Items.Clear();
        //                if (dsGrid.Tables[2] != null)
        //                {
        //                    if (dsGrid.Tables[2].Rows.Count != 0)
        //                    {
        //                        ddlTeacher.DataSource = dsGrid.Tables[2];
        //                        ddlTeacher.DataTextField = "FacultyShortName";
        //                        ddlTeacher.DataValueField = "Partner_Code";
        //                        ddlTeacher.DataBind();

        //                        int RCnt1 = 0;


        //                        string[] TeacherList = lblTeacher.Text.Split(',');

        //                        if (TeacherList.Length != 0)
        //                        {
        //                            foreach (string item in TeacherList)
        //                            {
        //                                for (RCnt1 = 0; RCnt1 <= ddlTeacher.Items.Count - 1; RCnt1++)
        //                                {
        //                                    if (item == ddlTeacher.Items[RCnt1].Value)
        //                                    {
        //                                        ddlTeacher.Items[RCnt1].Selected = true;
        //                                        break; // TODO: might not be correct. Was : Exit For
        //                                    }
        //                                }

        //                            }
        //                        }


        //                    }
        //                }

        //            }


        //        }
        //        else
        //        {
        //            BtnSaveAdd.Visible = false;
        //            dlGridChapter.DataSource = null;
        //            dlGridChapter.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        BtnSaveAdd.Visible = false;
        //        dlGridChapter.DataSource = null;
        //        dlGridChapter.DataBind();
        //    }
        //}
        //else
        //{
        //    BtnSaveAdd.Visible = false;
        //    dlGridChapter.DataSource = null;
        //    dlGridChapter.DataBind();
        //}

    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
    }

    protected void ddlLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_SchedulingHorizon();
    }

    private void FillDDL_SchedulingHorizon()
    {
        try
        {
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text;

            string LMSProductCode = "";
            if (ddlLMSProduct.Items.Count > 0) { LMSProductCode = ddlLMSProduct.SelectedItem.Value; }

            DataSet dsSchedulingHorizon = ProductController.Get_Schedule_Horizon(DivisionCode + "%" + AcedYear + "%" + StandardCode + "%" + LMSProductCode, 4);
            BindDDL(ddlSchHorizon, dsSchedulingHorizon, "Schedule_Horizon_Type_Name", "Schedule_Horizon_Type_Code");
            ddlSchHorizon.Items.Insert(0, "Select");
            ddlSchHorizon.SelectedIndex = 0;
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

    private void FillDDL_LMSProduct()
    {

        try
        {
            string AcademicYear = null;
            AcademicYear = ddlAcademicYear.SelectedItem.Text;


            string Course = null;
            Course = ddlCourse.SelectedValue;

            DataSet dsAllLMSProduct = ProductController.GetLMSProductByCourse_AcadYear(Course, AcademicYear);
            BindDDL(ddlLMSProduct, dsAllLMSProduct, "ProductName", "ProductCode");
            ddlLMSProduct.Items.Insert(0, "Select");
            ddlLMSProduct.SelectedIndex = 0;
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
    protected void chkCenter_CheckedChanged(object sender, EventArgs e)
    {

        //CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        int i = 0;
        foreach (DataListItem dtlItem in dlGridChapter.Items)
        {
            i++;
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
            ListBox lstbxteachers = (ListBox)dtlItem.FindControl("ddlTeacher");
          
            Label lblteachers = (Label)dtlItem.FindControl("lblteachers");
            if (chkitemck.Checked == true)
            {
                lblteachers.Visible = false;
                lstbxteachers.Visible = true;
                
            }
            else
            {
                lblteachers.Visible = true;
                lstbxteachers.Visible = false;
               // lstbxteachers.SelectedIndex = -1;
            }
            
            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");

        }


    }
    //protected void ddlTeacher_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (DataListItem dtlItem in dlGridChapter.Items)
    //    {
    //        CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
    //        ListBox lstbxteachers = (ListBox)dtlItem.FindControl("ddlTeacher");
    //        Label lblteachers = (Label)dtlItem.FindControl("lblteachers");
    //        string[] TeacherList = lstbxteachers.Text.Split(',');
    //        string a = lstbxteachers.SelectedValue.ToString();

    //        if (chkitemck.Checked == true)
    //        {
    //            lblteachers.Visible = false;
    //            lstbxteachers.Visible = true;
    //            int RCnt1 = 0;
    //            //string[] TeacherList = lstbxteachers.Text.Split(',');


    //        }
    //    }
    //    var partnercodes = new List<string>();
    //    foreach (DataListItem dtlItem in dlGridChapter.Items)
    //    {
    //        //TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");
    //        ListBox lstTeacherName = (ListBox)dtlItem.FindControl("ddlTeacher");
    //        CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
    //        if (chkitemck.Checked == true)
    //        {

    //            int TeacherCnt = 0;
    //            string Teacher_Code = "";

    //            for (TeacherCnt = 0; TeacherCnt <= lstTeacherName.Items.Count - 1; TeacherCnt++)
    //            {
    //                if (lstTeacherName.Items[TeacherCnt].Selected == true)
    //                {
    //                    Teacher_Code = Teacher_Code + lstTeacherName.Items[TeacherCnt].Value + ",";
    //                    if (partnercodes.Contains(lstTeacherName.Items[TeacherCnt].Value))
    //                    {
    //                    }
    //                    else
    //                    {
    //                        partnercodes.Add(lstTeacherName.Items[TeacherCnt].Value);
    //                    }

    //                }
    //            }
    //            Teacher_Code = Common.RemoveComma(Teacher_Code);
    //        }

    //    }

    //}

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridChapter.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
            Label lblteachers = (Label)dtlItem.FindControl("lblteachers");
            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
            //TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");
            ListBox lstbxteachers = (ListBox)dtlItem.FindControl("ddlTeacher");
            if (chkitemck.Checked == true)
            {
                lblteachers.Visible = false;
                lstbxteachers.Visible = true;
            }
            else
            {
                lblteachers.Visible = true;
                lstbxteachers.Visible = false;

            }

        }
        updatepanneladd.Update();


    }







    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Assign_Teacher();
    }

    private void Fill_Assign_Teacher()
    {
        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlCourse.SelectedValue;

        string AcedYear = "";
        AcedYear = ddlAcademicYear.SelectedItem.Text;
        divBottom.Visible = true;


        string LmsProductCode = "";
        LmsProductCode = ddlLMSProduct.SelectedValue;

        string BatchCode = "";
        BatchCode = ddlBatch.SelectedValue;


        if (ddlCenter_Add.SelectedItem.Value != "Select")
        {

            DataSet dsGrid = ProductController.GetYearDistributionsheetBy_Division_Year_Standard_Subject_Center(DivisionCode, AcedYear, ddlSubjectName.SelectedItem.Value, ddlCenter_Add.SelectedItem.Value, StandardCode, ddlSchHorizon.SelectedItem.Value, LmsProductCode, BatchCode);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    BtnSaveAdd.Visible = true;
                    updatepanneladd.Update();
                    dlGridChapter.DataSource = dsGrid;
                    dlGridChapter.DataBind();

                    int count = 0;
                    

                    foreach (DataListItem dtlItem in dlGridChapter.Items)
                    {
                      
                        ListBox ddlTeacher = (ListBox)dtlItem.FindControl("ddlTeacher");
                        Label lblTeacher = (Label)dtlItem.FindControl("lblParnerCode");
                        Label lblteachers = (Label)dtlItem.FindControl("lblteachers");
                        ddlTeacher.Items.Clear();
                        if (dsGrid.Tables[1] != null)
                        {
                            if (dsGrid.Tables[1].Rows.Count != 0)
                            {
                                ddlTeacher.DataSource = dsGrid.Tables[1];
                                ddlTeacher.DataTextField = "FacultyShortName";
                                ddlTeacher.DataValueField = "Partner_Code";
                                ddlTeacher.DataBind();


                                int RCnt1 = 0;


                                string[] TeacherList = lblTeacher.Text.Split(',');

                                if (TeacherList.Length != 0)
                                {
                                    foreach (string item in TeacherList)
                                    {
                                        for (RCnt1 = 0; RCnt1 <= ddlTeacher.Items.Count - 1; RCnt1++)
                                        {
                                            if (item == ddlTeacher.Items[RCnt1].Value)
                                            {
                                                ddlTeacher.Items[RCnt1].Selected = true;
                                                lblteachers.Text = lblteachers.Text + ddlTeacher.Items[RCnt1].ToString() + ',';
                                                
                                                break; // TODO: might not be correct. Was : Exit For
                                            }
                                        }

                                    }
                                }

                                if (lblteachers.Text.Length >= 1)
                                {
                                    lblteachers.Text = lblteachers.Text.Substring(0, lblteachers.Text.Length - 1);
                                }
                                ddlTeacher.Visible = false;

                            }
                        }

                    }
                   

                }
                else
                {
                    BtnSaveAdd.Visible = false;
                    dlGridChapter.DataSource = null;
                    dlGridChapter.DataBind();
                }
            }
            else
            {
                BtnSaveAdd.Visible = false;
                dlGridChapter.DataSource = null;
                dlGridChapter.DataBind();
            }
        }
        else
        {
            BtnSaveAdd.Visible = false;
            dlGridChapter.DataSource = null;
            dlGridChapter.DataBind();
        }





    }
    protected void btnCopyfaculty_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        string Teacher = "";
        List<string> list1 = new List<string>();
        int count = 0;
        foreach (DataListItem dtlItem in dlGridChapter.Items)
        {
            count++;


            ListBox ddlTeacher = (ListBox)dtlItem.FindControl("ddlTeacher");
            CheckBox chkCenter = (CheckBox)dtlItem.FindControl("chkCenter");

            if (count == 1 && chkCenter.Checked == true)
            {
                Clear_Error_Success_Box();
          
                foreach (ListItem li in ddlTeacher.Items)
                {
                    if (li.Selected == true)
                    {

                        list1.Add(li.Value);
                        Teacher = string.Join(",", list1.ToArray());

                    }
                }
               

            }
           // ddlTeacher.SelectedIndex = -1;
          
            
           // ddlTeacher.SelectedIndex=-1;
        }
       
      
                       
          
                int RCnt1 = 0;
                if (Teacher.Length != 0)
                {
                    foreach (DataListItem dtlItem1 in dlGridChapter.Items)
                    {
                        ListBox ddlTeacher = (ListBox)dtlItem1.FindControl("ddlTeacher");
                        CheckBox chkCenter = (CheckBox)dtlItem1.FindControl("chkCenter");
                        
                        if (chkCenter.Checked == true)
                        {
                            ddlTeacher.SelectedIndex = -1;
                        foreach (string item in list1)
                        {

                            for (RCnt1 = 0; RCnt1 <= ddlTeacher.Items.Count - 1; RCnt1++)
                            {
                                 
                                if (item == ddlTeacher.Items[RCnt1].Value)
                                {
                                    ddlTeacher.Items[RCnt1].Selected = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                               
                            }

                        }
                      }
                        
                        updatepanneladd.Update();
                   }
               }
                else
                {
                    Show_Error_Success_Box("E", "Atleast one teacher has been selected for first row...");
                }


            }

    //private void UPDATETEACHERSteaching()
    //{

    //    DataSet dsCity = ProductController.GetAllActiveTeacher(3, "");
    //    if (dsCity.Tables[0].Rows.Count > 0)
    //    {

    //        for (int cnt = 0; cnt <= dsCity.Tables[0].Rows.Count - 1; cnt++)
    //        {

    //            string partners = dsCity.Tables[0].Rows[cnt]["partner_code"].ToString();
    //            string CenterCode = dsCity.Tables[0].Rows[cnt]["Centre_Code"].ToString();
    //            string LMSProductCode = dsCity.Tables[0].Rows[cnt]["LMSProductCode"].ToString();
    //            string SubjectCode = dsCity.Tables[0].Rows[cnt]["Subject_Code"].ToString();
    //            string Acad_Year = dsCity.Tables[0].Rows[cnt]["Acad_Year"].ToString();

    //            Send_Details_LMS(partners, CenterCode, LMSProductCode, SubjectCode, Acad_Year);

    //        }
    //    }
    //}


    private void TEACHERS_Chapter_Details( string DivisionCode, string Standard_Code, string partners, string CenterCode, string LMSProductCode, string SubjectCode, string BatchCode, string Acad_Year)
    {
        int flag = 3;
        //int flag = 4;
        DataSet dsteachchapter = ProductController.GetAllActiveTeacher_chapter(DivisionCode, Standard_Code,partners,CenterCode,LMSProductCode,SubjectCode,BatchCode,Acad_Year,flag);
        if (dsteachchapter.Tables[0].Rows.Count > 0)
        {

            for (int cnt = 0; cnt <= dsteachchapter.Tables[0].Rows.Count - 1; cnt++)
            {

                
                 string Division_Code1 = dsteachchapter.Tables[0].Rows[cnt]["Division_Code"].ToString();
                 string AcadYear1 = dsteachchapter.Tables[0].Rows[cnt]["Acad_Year"].ToString();
                 string CourseCode1 = dsteachchapter.Tables[0].Rows[cnt]["Stream_Code"].ToString();

                string ActivityId1 = dsteachchapter.Tables[0].Rows[cnt]["Activity_Id"].ToString();
                string CenterCode1 = dsteachchapter.Tables[0].Rows[cnt]["Centre_Code"].ToString();
                string SubjectCode1 = dsteachchapter.Tables[0].Rows[cnt]["Subject_Code"].ToString();
                string ProductCode1 = dsteachchapter.Tables[0].Rows[cnt]["LMSProductCode"].ToString();
                string TeacherCode1 = dsteachchapter.Tables[0].Rows[cnt]["Partner_Code"].ToString();
                string ChapterCode1 = dsteachchapter.Tables[0].Rows[cnt]["Chapter_Code"].ToString();
                string BatchCode1 = dsteachchapter.Tables[0].Rows[cnt]["Batch_Code"].ToString();
                int IsActive1 = Int32.Parse(dsteachchapter.Tables[0].Rows[cnt]["IsActive"].ToString()); ;
                //bool IsActive1 = false;
                //if (dsteachchapter.Tables[0].Rows[cnt]["IsActive"].ToString() == "1")
                //{

                //    IsActive1 = true;
                //}



                Send_Chapter_Details_LMS(Division_Code1, CourseCode1, TeacherCode1, CenterCode1, ProductCode1, SubjectCode1, ChapterCode1, BatchCode1, ActivityId1, IsActive1, AcadYear1);
                  

            }
        }
    }
        
      

    }

