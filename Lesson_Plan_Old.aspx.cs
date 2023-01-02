using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Web;



public partial class Lesson_Plan : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
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
            BtnAdd.Visible = false;
            DivAdd.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivAdd.Visible = false;

        }

        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivAdd.Visible = true;
            ddlModuleAdd.Items.Clear();
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

    private void Clear_Error_Success_Box1()
    {
        divErrorProduct.Visible = false;
        lblErrorProduct.Text = "";
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

    private void Show_Error_Success_Box1(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            divErrorProduct.Visible = true;
            lblErrorProduct.Text = ProductController.Raise_Error(Error_Code);
        }

    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        try
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillDDL_Division()
    {
        try
        {
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

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        try
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillGrid()
    {
        try
        {
            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }

            if (ddlStandard.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlStandard.Focus();
                return;
            }

            if (ddlSubject.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0005");
                ddlSubject.Focus();
                return;
            }

            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;
            string Pkey = string.Empty;

            Pkey = DivisionCode + '%' + StandardCode + '%' + SubjectCode;



            DataSet dsGrid = ProductController.GetLessonPlanHeader(Pkey, 2);

            if (dsGrid != null)
            {

                if (dsGrid.Tables.Count != 0)
                {
                    dlGridDisplay.DataSource = dsGrid.Tables[0];
                    dlGridDisplay.DataBind();

                    DataList1.DataSource = dsGrid.Tables[1];
                    DataList1.DataBind();

                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lbltotalcount.Text = "0";
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();

                    DataList1.DataSource = null;
                    DataList1.DataBind();

                    
                }
            }
            else
            {
                lbltotalcount.Text = "0";
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();

                DataList1.DataSource = null;
                DataList1.DataBind();
            }

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
            lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();

            lblDivision_Add.Text = ddlDivision.SelectedItem.ToString();
            lblCourse_Add.Text = ddlStandard.SelectedItem.ToString();
            lblSubject_Add.Text = ddlSubject.SelectedItem.ToString();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillDDL_Standard()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlStandard, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlStandard.Items.Insert(0, "Select");
            ddlStandard.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillDDL_Subject_Add()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string StandardCode = null;
            StandardCode = ddlStandard.SelectedValue;

            DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            ddlSubject.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillChapter_Add()
    {
        try
        {
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;
            DataSet dsGrid = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(DivisionCode, "", StandardCode, SubjectCode);
            BindDDL(ddlChapter_Add, dsGrid, "Chapter_Name", "Chapter_Code");
            ddlChapter_Add.Items.Insert(0, "Select");
            ddlChapter_Add.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillTopic_Add()
    {
        try
        {
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            string ChapterCode = "";
            ChapterCode = ddlChapter_Add.SelectedValue;

            DataSet dsGrid = ProductController.GetAllTopicsBy_Division_Year_Standard_Subject_Chapter(DivisionCode, StandardCode, SubjectCode, ChapterCode, "1");
            BindDDL(ddlTopic_Add, dsGrid, "Topic_Name", "Topic_Code");
            ddlTopic_Add.Items.Insert(0, "Select");
            ddlTopic_Add.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillSubTopic_Add()
    {
        try
        {
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            string ChapterCode = "";
            ChapterCode = ddlChapter_Add.SelectedValue;

            string TopicCode = "";
            TopicCode = ddlTopic_Add.SelectedValue;

            DataSet dsGrid = ProductController.GetAllSubTopicsBy_Division_Year_Standard_Subject_Chapter_Topic(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, "1");
            BindDDL(ddlSubTopic_Add, dsGrid, "SubTopic_Name", "SubTopic_Code");
            ddlSubTopic_Add.Items.Insert(0, "Select");
            ddlSubTopic_Add.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    


    private void FillModule_Add()
    {
        try
        {
            ddlModuleAdd.Items.Clear();
            if (ddlDivision.SelectedIndex == 0)
            {                
                ddlDivision.Focus();
                return;
            }
            if (ddlStandard.SelectedIndex == 0)
            {
                ddlStandard.Focus();
                return;
            }
            if (ddlSubject.SelectedIndex == 0)
            {
                ddlSubject.Focus();
                return;
            }
            if (ddlChapter_Add.SelectedIndex == 0)
            {
                ddlChapter_Add.Focus();
                return;
            }
            //if (ddlTopic_Add.SelectedIndex == 0)
            //{
            //    ddlTopic_Add.Focus();
            //    return;
            //}
            //if (ddlSubTopic_Add.SelectedIndex == 0)
            //{
            //    ddlSubTopic_Add.Focus();
            //    return;
            //}
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            string ChapterCode = "";
            ChapterCode = ddlChapter_Add.SelectedValue;
            string TopicCode = "";
            string SubTopicCode = "";

            if (ddlTopic_Add.SelectedIndex != 0)
            {
                TopicCode = ddlTopic_Add.SelectedValue;
                if (ddlSubTopic_Add.SelectedIndex != 0)
                {
                    SubTopicCode = ddlSubTopic_Add.SelectedValue;
                }
            }

            DataSet dsGrid = ProductController.Get_ModuleDetail("", DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, "3");
            BindListBox(ddlModuleAdd, dsGrid, "Module_Name", "Module_Code");

            //if (ddlTopic_Add.SelectedIndex == 0)
            //{
            //    DataSet dsGrid = ProductController.GetAllModuleBy_Division_Year_Standard_Subject_Chapter_Topic_SubTopic(DivisionCode, StandardCode, SubjectCode, ChapterCode, "", "", "2");
            //    BindListBox(ddlModuleAdd, dsGrid, "Module_Name", "Module_Code");
            //}
            //else if (ddlSubTopic_Add.SelectedIndex == 0)
            //{                
            //    TopicCode = ddlTopic_Add.SelectedValue;
            //    DataSet dsGrid = ProductController.GetAllModuleBy_Division_Year_Standard_Subject_Chapter_Topic_SubTopic(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, "", "3");
            //    BindListBox(ddlModuleAdd, dsGrid, "Module_Name", "Module_Code");
            //}
            //else 
            //{
            //    TopicCode = ddlTopic_Add.SelectedValue;
            //    string SubTopicCode = "";
            //    SubTopicCode = ddlSubTopic_Add.SelectedValue;                
            //    DataSet dsGrid = ProductController.GetAllModuleBy_Division_Year_Standard_Subject_Chapter_Topic_SubTopic(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, "1");
            //    BindListBox(ddlModuleAdd, dsGrid, "Module_Name", "Module_Code");
            //}

            

            
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillContent_Location()
    {
        try
        {
            DataSet dsGrid = ProductController.GetContentLocation(1);
            BindDDL(ddlLocation, dsGrid, "LocationName", "LocationCode");
            ddlLocation.Items.Insert(0, "Select");
            ddlLocation.SelectedIndex = 0;
        
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillContent_Type()
    {
        try
        {
            string Location = "";
            Location = ddlLocation.SelectedValue;
            DataSet dsGrid = ProductController.GetContentType(Location, 1);
            BindDDL(ddlContentType, dsGrid, "ContentName", "ContentCode");
            ddlContentType.Items.Insert(0, "Select");
            ddlContentType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }


    private void FillLessonPlanTest()
    {
        try
        {
            string Module = "";
            Module = ddlModule.SelectedValue;
            DataSet dsGrid = ProductController.GetLessonPlanTest(Module, 1, "");
            BindDDL(ddlTest, dsGrid, "TestName", "TestCode");
            ddlTest.Items.Insert(0, "Select");
            ddlTest.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    
    private bool ValidationHeader()
    {
        bool flag = true;
        if (txtLessonPlanName_Add.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Lesson Plan Name");
            txtLessonPlanName_Add.Focus();
            flag = false;
            return flag;
        }
        if (ddlChapter_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Chapter");
            ddlChapter_Add.Focus();
            flag = false;
            return flag;
        }
        if (ddlTopic_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Topic");
            ddlTopic_Add.Focus();
            flag = false;
            return flag;
        }
        if (ddlSubTopic_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Sub Topic");
            ddlSubTopic_Add.Focus();
            flag = false;
            return flag;
        }

        return flag;
    }

    private bool ValidationProductContent()
    {
        bool flag = true;
        Clear_Error_Success_Box1();

        if (ddlModule.SelectedIndex == -1)
        {
            Show_Error_Success_Box1("E", "Select Module");
            ddlModule.Focus();
            flag = false;
            return flag;
        }
        if (ddlModule.SelectedIndex == 0)
        {
            Show_Error_Success_Box1("E", "Select Module");
            ddlModule.Focus();
            flag = false;
            return flag;
        }
        if (txtProductContentName.Text.Trim() == "")
        {
            Show_Error_Success_Box1("E", "Enter Product Content Name");
            txtProductContentName.Focus();
            flag = false;
            return flag;
        }
        if (txtVersionID.Text.Trim() == "")
        {
            Show_Error_Success_Box1("E", "Enter Version");
            txtVersionID.Focus();
            flag = false;
            return flag;
        }
        if (ddlLocation.SelectedIndex == 0)
        {
            Show_Error_Success_Box1("E", "Select Location");
            ddlLocation.Focus();
            flag = false;
            return flag;
        }
        if (ddlContentType.SelectedIndex == 0)
        {
            Show_Error_Success_Box1("E", "Select Content Type");
            ddlContentType.Focus();
            flag = false;
            return flag;
        }

        return flag;
    }

    private void SaveLessonHeader()
    {
        try
        {

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            string ChapterCode = "";
            ChapterCode = ddlChapter_Add.SelectedValue;

            string TopicCode = "";
            string SubTopicCode = "";           

            if (ddlTopic_Add.SelectedIndex != 0)
            {
                TopicCode = ddlTopic_Add.SelectedValue;
                if (ddlSubTopic_Add.SelectedIndex != 0)
                {
                    SubTopicCode = ddlSubTopic_Add.SelectedValue;
                }
            }  
            bool EOD = false;
            if (chkIsLastLecture.Checked)
            {
                EOD = true;
            }
            int IsActive = 0;
            if (chkIsActiveHeader.Checked)
            {
                IsActive = 1;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];


            string Pkey = "";
            Pkey = DivisionCode + '%' + StandardCode + '%' + SubjectCode + '%' + ChapterCode + '%' + TopicCode + '%' + SubTopicCode;


            string Result = string.Empty;

            if (lblPkey.Text == "")
            {
                Result = ProductController.Insert_UpdateLessonPlanHeader(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, "", txtLessonPlanName_Add.Text.Trim(), txtDisplayName_Add.Text.Trim(), txtDescription_Add.Text.Trim(), EOD, IsActive, UserID, Pkey, "1");

                if (Result == "-1")
                {
                    Show_Error_Success_Box("E", "Record already exists");
                }
                else if (Result == "-2")
                {
                    Show_Error_Success_Box("E", "Record not saved");

                }
                else
                {
                    SaveLessonProductConatent(Result);
                    lblPkey.Text = Pkey + '%' + Result;
                    Show_Error_Success_Box("S", "0000");
                }
            }
            else
            {
                string[] chrSpit = lblPkey.Text.Split('%');
                string LessonPlanCode = chrSpit[6].ToString();
                Result = ProductController.Insert_UpdateLessonPlanHeader(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, LessonPlanCode, txtLessonPlanName_Add.Text.Trim(), txtDisplayName_Add.Text.Trim(), txtDescription_Add.Text.Trim(), EOD, IsActive, UserID, Pkey, "2");

                if (Result == "1")
                {
                    Show_Error_Success_Box("S", "0000");
                }
                if (Result == "-1")
                {
                    Show_Error_Success_Box("E", "Record already exists");
                }
                else if (Result == "-2")
                {
                    Show_Error_Success_Box("E", "Record not saved");
                }
            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void SaveLessonProductConatent(string LessonPlanCode)
    {
        try
        {


            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            string ChapterCode = "";
            ChapterCode = ddlChapter_Add.SelectedValue;

            string TopicCode = "";
            TopicCode = ddlTopic_Add.SelectedValue;

            string SubTopicCode = "";
            SubTopicCode = ddlSubTopic_Add.SelectedValue;

            string ModuleCode = "";
            ModuleCode = ddlModule.SelectedValue;

            string ProductContentCode = "";

            string VersionId = "";
            VersionId = txtVersionID.Text.Trim();

            int IsActive = 0;
            if (chkIsActivePContant.Checked)
            {
                IsActive = 1;
            }

            string ProductContentName = "";
            ProductContentName = txtProductContentName.Text.Trim();


            string ProductContentDisplayName = "";
            ProductContentDisplayName = txtProductContentDisplayName.Text.Trim();


            string ProductContentDescription = "";
            ProductContentDescription = txtDescription.Text.Trim();

            string ProductCode = "";

            string ProductContentFileUrl = "";

            string ProductContentImageUrl = "";

            string KeyPath = "";


            string LocationCode = "";
            LocationCode = ddlLocation.SelectedValue;


            string ContentTypeCode = "";
            ContentTypeCode = ddlContentType.SelectedValue;

            string TestCode = "";
            if (ddlTest.SelectedValue != "Select")
            {
                TestCode = ddlTest.SelectedValue;
            }


            string Dimension1 = "";

            string Dimension1Unit = "";

            string Dimension1Value = "";

            string Dimension2 = "";

            string Dimension2Unit = "";

            string Dimension2Value = "";

            string Dimension3 = "";

            string Dimension3Unit = "";

            string Dimension3Value = "";

            string Dimension4 = "";

            string Dimension4Unit = "";

            string Dimension4Value = "";

            string Dimension5 = "";

            string Dimension5Unit = "";

            string Dimension5Value = "";


            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];


            string Pkey = "";
            Pkey = DivisionCode + '%' + StandardCode + '%' + SubjectCode + '%' + ChapterCode + '%' + TopicCode + '%' + SubTopicCode + '%' + ModuleCode + '%' + LessonPlanCode + '%' + VersionId;


            string Result = string.Empty;

            if (lblProductPkey.Text == "")
            {
                Result = ProductController.Insert_LessonPlanContent(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, ModuleCode, LessonPlanCode, "", txtVersionID.Text.Trim(), txtProductContentName.Text.Trim(), txtProductContentDisplayName.Text.Trim(),

                    txtDescription.Text.Trim(), ProductCode, ProductContentFileUrl, ProductContentImageUrl, KeyPath, LocationCode, ContentTypeCode, TestCode, Dimension1, Dimension1Unit, Dimension1Value, Dimension2, Dimension2Unit, Dimension2Value, Dimension3, Dimension3Unit, Dimension3Value, Dimension4, Dimension4Unit, Dimension4Value, Dimension5, Dimension5Unit, Dimension5Value, IsActive, UserID, Pkey, "1");

                if (Result == "-1")
                {
                    Show_Error_Success_Box("E", "Record already exists");
                }
                else if (Result == "-2")
                {
                    Show_Error_Success_Box("E", "Record not saved");

                }
                else
                {
                    Show_Error_Success_Box("S", "0000");
                }

            }
            else
            {
                string[] chrSpit = lblProductPkey.Text.Split('%');
                ProductContentCode = chrSpit[9].ToString();
                Result = ProductController.Insert_LessonPlanContent(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, ModuleCode, LessonPlanCode, ProductContentCode, txtVersionID.Text.Trim(), txtProductContentName.Text.Trim(), txtProductContentDisplayName.Text.Trim(),

                    txtDescription.Text.Trim(), ProductCode, ProductContentFileUrl, ProductContentImageUrl, KeyPath, LocationCode, ContentTypeCode, TestCode, Dimension1, Dimension1Unit, Dimension1Value, Dimension2, Dimension2Unit, Dimension2Value, Dimension3, Dimension3Unit, Dimension3Value, Dimension4, Dimension4Unit, Dimension4Value, Dimension5, Dimension5Unit, Dimension5Value, IsActive, UserID, Pkey, "2");

                if (Result == "1")
                {
                    Show_Error_Success_Box("S", "0000");
                }
                if (Result == "-1")
                {
                    Show_Error_Success_Box("E", "Record already exists");
                }
                else if (Result == "-2")
                {
                    Show_Error_Success_Box("E", "Record not saved");
                }
            }

        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void ClearLessonHeader()
    {
        try
        {
            lblPkey.Text = "";
            lblHeader.Text = "Create New Lesson Plan";
            txtDescription_Add.Text = "";
            txtDisplayName_Add.Text = "";
            txtLessonPlanName_Add.Text = "";
            chkIsActiveHeader.Checked = false;
            chkIsLastLecture.Checked = false;
            ddlSubTopic_Add.Items.Clear();
            ddlTopic_Add.Items.Clear();
            ddlChapter_Add.Items.Clear();
            ddlChapter_Add.Enabled = true;
            ddlTopic_Add.Enabled = true;
            ddlSubTopic_Add.Enabled = true;
            btnEdit.Visible = false;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void ClearLessonProduct()
    {
        try
        {
            ddlModule.Items.Clear();
            txtVersionID.Text = "";
            txtDuration.Text = "";
            txtNoQuestion.Text = "";
            ddlTest.Items.Clear();
            txtProductContentDisplayName.Text = "";
            txtProductContentName.Text = "";
            txtDescription.Text = "";
            ddlModule.Items.Clear();
            ddlContentType.Items.Clear();
            chkIsActivePContant.Checked = false;
            lblProductPkey.Text = "";
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
            ClearLessonHeader();
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();
            ClearLessonProduct();
            lblPkey.Text = "";
            lblProductPkey.Text = "";
            btnEdit.Visible = false;
            ControlVisibility("Add");
            FillChapter_Add();
            dlAssign_Add.DataSource = null;
            dlAssign_Add.DataBind();
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
            lblPkey.Text = "";
            lblProductPkey.Text = "";
            ClearLessonHeader();
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();
            ClearLessonProduct();
            ControlVisibility("Result");
            btnEdit.Visible = false;
            dlAssign_Add.DataSource = null;
            dlAssign_Add.DataBind();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlChapter_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlTopic_Add.Items.Clear();
            ddlSubTopic_Add.Items.Clear();
            ddlModule.Items.Clear();
            FillTopic_Add();
            FillModule_Add();

        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlTopic_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubTopic_Add.Items.Clear();
            ddlModule.Items.Clear();
            FillSubTopic_Add();
            FillModule_Add();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlSubTopic_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlModule.Items.Clear();
            FillModule_Add();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {


            ddlDivision.SelectedIndex = 0;
            ddlStandard.Items.Clear();
            ddlSubject.Items.Clear();
            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Subject_Add();
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    protected void btnAssignProduct_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationHeader())
            {
                Clear_Error_Success_Box();
                Clear_Error_Success_Box1();
                ClearLessonProduct();
                lblProductPkey.Text = "";
                FillModule_Add();
                FillContent_Location();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAssignProduct();", true);
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {


            if (e.CommandArgument.ToString() != "")
            {
                lblHeader.Text = "Edit Lesson Plan";
                lblPkey.Text = "";
                string DivisionCode = null;
                DivisionCode = ddlDivision.SelectedValue;

                string StandardCode = "";
                StandardCode = ddlStandard.SelectedValue;

                string SubjectCode = "";
                SubjectCode = ddlSubject.SelectedValue;

                string Pkey = string.Empty;
                Pkey = e.CommandArgument.ToString();

                DataSet ds = ProductController.GetLessonPlanHeader(Pkey, 1);


                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        ControlVisibility("Add");


                        txtLessonPlanName_Add.Text = ds.Tables[0].Rows[0]["LessonPlanName"].ToString();
                        txtDescription_Add.Text = ds.Tables[0].Rows[0]["LessonPlanDescription"].ToString();
                        txtDisplayName_Add.Text = ds.Tables[0].Rows[0]["LessonPlanDisplayName"].ToString();
                        //btnEdit.Visible = true;
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IsActive"]) == 1)
                        {
                            chkIsActiveHeader.Checked = true;
                        }
                        else
                        {
                            chkIsActiveHeader.Checked = false;
                        }
                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["EOC"]) == true)
                        {
                            chkIsLastLecture.Checked = true;
                        }
                        else
                        {
                            chkIsLastLecture.Checked = false;
                        }

                        FillChapter_Add();

                        ddlChapter_Add.SelectedValue = ds.Tables[0].Rows[0]["ChapterCode"].ToString();
                        ddlChapter_Add.Enabled = false;
                        ddlTopic_Add.Enabled = false;
                        ddlSubTopic_Add.Enabled = false;

                        FillTopic_Add();
                        if (ds.Tables[0].Rows[0]["TopicCode"].ToString() != "")
                        {
                            ddlTopic_Add.SelectedValue = ds.Tables[0].Rows[0]["TopicCode"].ToString();                            

                            FillSubTopic_Add();
                            if (ds.Tables[0].Rows[0]["SubTopicCode"].ToString() != "")
                            {
                                ddlSubTopic_Add.SelectedValue = ds.Tables[0].Rows[0]["SubTopicCode"].ToString();                                
                            }
                        }
                        FillModule_Add();

                        //Fill selected Modules
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
                            {
                                for (int rcnt = 0; rcnt <= ddlModuleAdd.Items.Count - 1; rcnt++)
                                {
                                    if (ddlModuleAdd.Items[rcnt].Value == ds.Tables[1].Rows[cnt]["ModuleCode"].ToString())
                                    {
                                        ddlModuleAdd.Items[rcnt].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }      
                        lblPkey.Text = ds.Tables[0].Rows[0]["Pkey"].ToString();


                        //dlAssign_Add.DataSource = ds.Tables[1];
                        //dlAssign_Add.DataBind();


                    }
                    else
                    {
                        btnEdit.Visible = false;
                        dlAssign_Add.DataSource = null;
                        dlAssign_Add.DataBind();
                    }
                }
                else
                {
                    btnEdit.Visible = false;
                    dlAssign_Add.DataSource = null;
                    dlAssign_Add.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationHeader())
            {
                Clear_Error_Success_Box();
                SaveLessonHeader();
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ValidationProductContent() == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAssignProduct();", true);
            }
            else
            {
                if (lblPkey.Text == "")
                {
                    SaveLessonHeader();
                }
                else
                {
                    string[] chrSpit = lblPkey.Text.Split('%');
                    string LessonPlanCode = chrSpit[6].ToString();
                    SaveLessonProductConatent(LessonPlanCode);
                }
                DataSet ds = ProductController.GetLessonPlanContent(lblPkey.Text, 2);
                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        dlAssign_Add.DataSource = ds;
                        dlAssign_Add.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    protected void dlAssign_Add_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                DataSet ds = ProductController.GetLessonPlanContent(e.CommandArgument.ToString(), 1);

                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        FillModule_Add();
                        FillContent_Location();
                        ddlModule.SelectedValue = ds.Tables[0].Rows[0]["ModuleCode"].ToString();
                        txtVersionID.Text = ds.Tables[0].Rows[0]["VersionId"].ToString();
                        txtProductContentName.Text = ds.Tables[0].Rows[0]["ProductContentName"].ToString();
                        txtProductContentDisplayName.Text = ds.Tables[0].Rows[0]["ProductContentDisplayName"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["ProductContentDescription"].ToString();
                        ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["LocationCode"].ToString();


                        FillContent_Type();
                        ddlContentType.SelectedValue = ds.Tables[0].Rows[0]["ContentTypeCode"].ToString();
                        FillLessonPlanTest();

                        if (ds.Tables[0].Rows[0]["TestCode"].ToString() != "0")
                        {

                            ddlTest.SelectedValue = ds.Tables[0].Rows[0]["TestCode"].ToString();
                        }

                        txtNoQuestion.Text = ds.Tables[0].Rows[0]["TotalQuestion"].ToString();
                        txtDuration.Text = ds.Tables[0].Rows[0]["TestDuration"].ToString();
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IsActive"]) == 1)
                        {
                            chkIsActivePContant.Checked = true;
                        }
                        else
                        {
                            chkIsActivePContant.Checked = false;
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAssignProduct();", true);

                        lblProductPkey.Text = ds.Tables[0].Rows[0]["PKey"].ToString();
                    }
                    else
                    {
                        lblProductPkey.Text = "";

                    }
                }
                else
                {
                    lblProductPkey.Text = "";
                }
            }

        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLocation.SelectedIndex == 0)
            {
                ddlContentType.Items.Clear();
            }
            else if (ddlLocation.SelectedIndex == -1)
            {
                ddlContentType.Items.Clear();
            }
            else
            {
                FillContent_Type();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAssignProduct();", true);
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E",ex.ToString());
        }
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlModule.SelectedIndex == 0)
            {
                ddlTest.Items.Clear();
            }
            else if (ddlModule.SelectedIndex == -1)
            {
                ddlTest.Items.Clear();
            }
            else
            {
                FillLessonPlanTest();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAssignProduct();", true);
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            if (ddlTest.SelectedIndex == 0)
            {
                txtDuration.Text = "";
                txtNoQuestion.Text = "";
            }
            else if (ddlTest.SelectedIndex == -1)
            {
                txtDuration.Text = "";
                txtNoQuestion.Text = "";

            }
            else
            {
                string TestCode = "";
                TestCode = ddlTest.SelectedValue;
                DataSet dsGrid = ProductController.GetLessonPlanTest("", 2, TestCode);
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        txtDuration.Text = dsGrid.Tables[0].Rows[0]["TestDuration"].ToString();
                        txtNoQuestion.Text = dsGrid.Tables[0].Rows[0]["TotalQuestion"].ToString();
                    }
                    else
                    {
                        txtDuration.Text = "";
                        txtNoQuestion.Text = "";
                    }
                }
                else
                {
                    txtDuration.Text = "";
                    txtNoQuestion.Text = "";
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAssignProduct();", true);
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        if (txtLessonPlanName_Add.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Lesson Plan Name");
            txtLessonPlanName_Add.Focus();
            return;
        }
        if (ddlChapter_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Chapter");
            ddlChapter_Add.Focus();
            return;
        }
        

        string ModuleCode = "";
        for (int cnt = 0; cnt <= ddlModuleAdd.Items.Count - 1; cnt++)
        {
            if (ddlModuleAdd.Items[cnt].Selected == true)
            {
                ModuleCode = ModuleCode + ddlModuleAdd.Items[cnt].Value + ",";
            }
        }

        if (ModuleCode == "")
        {
            Show_Error_Success_Box("E", "Atleast one Module should be selected");
            ddlModuleAdd.Focus();
            return;
        }

        ModuleCode = ModuleCode.Substring(0, ModuleCode.Length - 1);

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlStandard.SelectedValue;

        string SubjectCode = "";
        SubjectCode = ddlSubject.SelectedValue;

        string ChapterCode = "";
        ChapterCode = ddlChapter_Add.SelectedValue;

        string TopicCode = "";
        string SubTopicCode = "";
        if (ddlTopic_Add.SelectedIndex != 0)
        {
            TopicCode = ddlTopic_Add.SelectedValue;
        }        
        if (ddlSubTopic_Add.SelectedIndex != 0)
        {
            SubTopicCode = ddlSubTopic_Add.SelectedValue;
        }       
        

        bool EOD = false;
        if (chkIsLastLecture.Checked)
        {
            EOD = true;
        }
        int IsActive = 0;
        if (chkIsActiveHeader.Checked)
        {
            IsActive = 1;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];


        string Pkey = "";
        Pkey = DivisionCode + '%' + StandardCode + '%' + SubjectCode + '%' + ChapterCode + '%' + TopicCode + '%' + SubTopicCode;


        string Result = string.Empty;

        if (lblPkey.Text == "")
        {
            Result = ProductController.Insert_UpdateLessonPlanHeader(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, "", txtLessonPlanName_Add.Text.Trim(), txtDisplayName_Add.Text.Trim(), txtDescription_Add.Text.Trim(), EOD, IsActive, UserID, Pkey, "1",ModuleCode);

            if (Result == "-1")
            {
                Show_Error_Success_Box("E", "Record already exists");
            }
            else if (Result == "-2")
            {
                Show_Error_Success_Box("E", "Record not saved");

            }
            else
            {
                Show_Error_Success_Box("S", "Record Saved successfully");
                BtnSearch_Click(sender,e);
            }
        }

        else
        {
            string[] chrSpit = lblPkey.Text.Split('%');
            string LessonPlanCode = chrSpit[6].ToString();
            Result = ProductController.Insert_UpdateLessonPlanHeader(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, LessonPlanCode, txtLessonPlanName_Add.Text.Trim(), txtDisplayName_Add.Text.Trim(), txtDescription_Add.Text.Trim(), EOD, IsActive, UserID, Pkey, "2",ModuleCode);


            if (Result == "1")
            {
                Show_Error_Success_Box("S", "0000");
                BtnSearch_Click(sender, e);
            }
            if (Result == "-1")
            {
                Show_Error_Success_Box("E", "Record already exists");
            }
            else if (Result == "-2")
            {
                Show_Error_Success_Box("E", "Record not saved");
            }
        }     
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Lesson_Plan_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Lesson_Plan</b></TD></TR><TR><TD Colspan='2'><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD><b>Course : " + ddlStandard.SelectedItem.ToString() + "</b></TD><TD Colspan='3'><b>Subject : " + ddlSubject.SelectedItem.ToString() + "<b></TD></TR><TR></TR>");
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
}