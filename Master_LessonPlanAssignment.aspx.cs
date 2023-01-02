using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;

partial class Master_LessonPlanAssignment : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;

            ddlLMSProduct.Items.Insert(0, "Select");
            ddlLMSProduct.SelectedIndex = 0;

            ddlSubjectName.Items.Insert(0, "Select");
            ddlSubjectName.SelectedIndex = 0; 
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
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = True
        }
        Clear_Error_Success_Box();
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_Division()
    {
        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID , Company_Code, "", "", "2", DBname);
        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;            

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

    private void Fill_GridDisplay()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        string DivCode = ddlDivision.SelectedValue, AcadYear = ddlAcademicYear.SelectedValue, Course = ddlCourse.SelectedValue,
               LMSProduct = ddlLMSProduct.SelectedValue, SubjectCode = ddlSubjectName.SelectedValue;

        DataSet dsLessonPlanAssignment = ProductController.GetLessonPlanAssignment(DivCode, AcadYear, Course, SubjectCode, LMSProduct, UserID, "1");

        if (dsLessonPlanAssignment != null)
        {
            if (dsLessonPlanAssignment.Tables[0].Rows.Count > 0)
            {
                ControlVisibility("Result");

                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblAcadYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
                lblStandard_Result.Text = ddlCourse.SelectedItem.ToString();
                lblLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString();
                lblSubject_Result.Text = ddlSubjectName.SelectedItem.ToString();

                dlGridExport.DataSource = dsLessonPlanAssignment.Tables[0];
                dlGridExport.DataBind();

                dlGridDisplay.DataSource = dsLessonPlanAssignment.Tables[0];
                dlGridDisplay.DataBind();
                lbltotalcount.Text = dsLessonPlanAssignment.Tables[0].Rows.Count.ToString();

                DataView dv = new DataView(dsLessonPlanAssignment.Tables[1]);
                DataTable dtfilter = new DataTable();

                foreach (DataListItem dtlItem in dlGridDisplay.Items)
                {
                    DropDownList ddlLessonPlan = (DropDownList)dtlItem.FindControl("ddlLessonPlan");
                    Label lblChapter_Code = (Label)dtlItem.FindControl("lblChapter_Code");
                    Label lblLessonPlancode = (Label)dtlItem.FindControl("lblLessonPlancode");
                    Label lblLessonPlanName = (Label)dtlItem.FindControl("lblLessonPlanName");

                    dv.RowFilter = string.Empty;
                    dv.RowFilter = "ChapterCode = '" + lblChapter_Code.Text + "'";
                    dtfilter = new DataTable();
                    dtfilter = dv.ToTable();

                    BindDDLTable(ddlLessonPlan, dtfilter, "LessonPlanName", "LessonPlanCode");
                    ddlLessonPlan.Items.Insert(0, "Select");
                    ddlLessonPlan.SelectedIndex = 0;

                    ddlLessonPlan.SelectedValue = lblLessonPlancode.Text;
                    // ddlLessonPlan.SelectedIndex = ddlLessonPlan.Items.IndexOf(ddlLessonPlan.Items.FindByText(lblLessonPlanName.Text));
                }


            }
            else
            {
                Show_Error_Success_Box("E", "Records Not found");
                return;
            }
        }
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    private void BindDDLTable(DropDownList ddl, DataTable dt, string txtField, string valField)
    {
        ddl.DataSource = dt;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E","Select Division");
            return;
        }
        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Academic Year");
            return;
        }
        if (ddlCourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }
        if (ddlLMSProduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select LMS Product");
            return;
        }
        if (ddlSubjectName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject");
            return;
        }

        Fill_GridDisplay();
    }

   
  

       

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Search");
    }



    public Master_LessonPlanAssignment()
    {
        Load += Page_Load;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "LessonPlanSequence_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>LessonPlan Sequence</b></TD></TR><TR><TD Colspan='2'><b>Division : " + lblDivision_Result.Text + "</b></TD><TD><b>Acad Year : " + lblAcadYear_Result.Text + "</b></TD><TD><b>Course : " + lblStandard_Result.Text + "</b></TD></TR>"+
            "<TR><TD Colspan='2'><b>LMSProduct : " + lblLMSProduct_Result.Text + "</b></TD><TD><b>Subject : " + lblSubject_Result.Text + "</b></TD><TD></TD></TR><TR></TR></Table>");
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
   


    private bool IsNumeric(object value)
    {
        try
        {
            int i = Convert.ToInt32(value.ToString());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        ddlLMSProduct.Items.Clear();
        ddlLMSProduct.Items.Insert(0, "Select");
        ddlLMSProduct.SelectedIndex = 0;
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
        FillDDL_LMSProduct();
    }

     protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;

        ddlCourse.Items.Clear();
        ddlCourse.Items.Insert(0, "Select");
        ddlCourse.SelectedIndex = 0;

        ddlLMSProduct.Items.Clear();
        ddlLMSProduct.Items.Insert(0, "Select");
        ddlLMSProduct.SelectedIndex = 0;

        ddlSubjectName.Items.Clear();
        ddlSubjectName.Items.Insert(0, "Select");
        ddlSubjectName.SelectedIndex = 0;
    }

     protected void BtnClose_Click(object sender, EventArgs e)
     {
         Clear_Error_Success_Box();
         ControlVisibility("Search");
     }
     protected void BtnSave_Click(object sender, EventArgs e)
     {
         Clear_Error_Success_Box();
         string XMLData = "<LessonPlanAssignments>";

         List<string> LectureSequenceList = new List<string>();
         foreach (DataListItem dtlItem in dlGridDisplay.Items)
         {
             DropDownList ddlLessonPlan = (DropDownList)dtlItem.FindControl("ddlLessonPlan");
             TextBox txtDLLectureCnt = (TextBox)dtlItem.FindControl("txtDLLectureCnt");
             Label lblErrorSaveMessage = (Label)dtlItem.FindControl("lblErrorSaveMessage");
             Label lblChapter_Code = (Label)dtlItem.FindControl("lblChapter_Code");

             lblErrorSaveMessage.Text = "";

             if (((txtDLLectureCnt.Text.Trim() == "0") || (txtDLLectureCnt.Text.Trim() == "")) && (ddlLessonPlan.SelectedIndex != 0))
             {                 
                 lblErrorSaveMessage.CssClass = "red";
                 lblErrorSaveMessage.Text = "Enter Lecture Sequence";
                 txtDLLectureCnt.Focus();
                 return;
             }

             if ((txtDLLectureCnt.Text.Trim() != "0") && (ddlLessonPlan.SelectedIndex == 0))
             {
                 lblErrorSaveMessage.CssClass = "red";
                 lblErrorSaveMessage.Text = "Select Lesson Plan";
                 ddlLessonPlan.Focus();
                 return;
             }
             //Findout Duplicate Lecture Sequence
             if ((txtDLLectureCnt.Text.Trim() != "0") && txtDLLectureCnt.Text.Trim() != "")
             {
                 foreach (string element in LectureSequenceList)
                 {
                     if (element.ToString() == Convert.ToString(lblChapter_Code.Text.Trim()) + '%'+ txtDLLectureCnt.Text.Trim())
                     {
                         lblErrorSaveMessage.CssClass = "red";
                         lblErrorSaveMessage.Text = "Duplicate Lecture Sequence";
                         txtDLLectureCnt.Focus();
                         return;
                     }
                 }

                 LectureSequenceList.Add(Convert.ToString(lblChapter_Code.Text.Trim()) + '%' + Convert.ToString(txtDLLectureCnt.Text.Trim()));

                 XMLData = XMLData + "<LessonPlanAssignment><Chapter_Code>" + lblChapter_Code.Text + "</Chapter_Code><LectureSequence>" + txtDLLectureCnt.Text.Trim() + "</LectureSequence><LessonPlanCode>" + ddlLessonPlan.SelectedValue + "</LessonPlanCode></LessonPlanAssignment>";

             }
         }//Complete foreach (DataListItem dtlItem in dlGridDisplay.Items)

         XMLData = XMLData + "</LessonPlanAssignments>";


         if (XMLData == "<LessonPlanAssignments></LessonPlanAssignments>")
         {
             Show_Error_Success_Box("E", "Enter atleast one Lecture Sequence");
             return;
         }

         HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
         string UserID = cookie.Values["UserID"];

         string DivCode = ddlDivision.SelectedValue, AcadYear = ddlAcademicYear.SelectedValue, Course = ddlCourse.SelectedValue,
                LMSProduct = ddlLMSProduct.SelectedValue, SubjectCode = ddlSubjectName.SelectedValue;
         try
         {
             DataSet dsLessonPlanAssignment = ProductController.InsertUpdate_LessonPlanAssignment(DivCode, AcadYear, Course, SubjectCode, LMSProduct, UserID, XMLData, "1");
             if (dsLessonPlanAssignment != null)
             {
                 if (dsLessonPlanAssignment.Tables[0].Rows[0]["Reasult"].ToString() == "1")
                 {
                     Fill_GridDisplay();
                     Show_Error_Success_Box("S", "Record's saved successfully");
                 }
             }
         }
         catch (Exception ex)
         {
             Show_Error_Success_Box("E", ex.ToString());
             return;
         }
     }


}
