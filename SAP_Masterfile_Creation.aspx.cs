using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Configuration;

public partial class SAP_Masterfile_Creation : System.Web.UI.Page
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
            ControlVisibility("Search");
            Clear_Error_Success_Box();

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
    private void FillDDL_Center()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_1(4, ddlDivisionAdd.SelectedValue);
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
    private void FillDDL_Center1()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_1(4, ddlDivision.SelectedValue);
            if (dsCenter != null)
            {
                if (dsCenter.Tables.Count != 0)
                {
                    BindListBox(ddlCenter1, dsCenter, "Source_Center_Name", "Source_Center_Code");
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
    private void FillDDL_Centeredite()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWiseedite(4, ddlDivision.SelectedValue);
            if (dsCenter != null)
            {
                if (dsCenter.Tables.Count != 0)
                {
                    BindListBox(ddlCenteredite, dsCenter, "Source_Center_Name", "Source_Center_Code");
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
            DataSet dsClassroomCourse = ProductController.Get_ClassRoomCourse1("1", ddlDivisionAdd.SelectedValue);
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
    private void Fill_Grid()
    {
        Clear_Error_Success_Box();
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.Get_SAPClassroomProduct(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, txtStreamName.Text.Trim(), "", "", "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables[0].Rows.Count > 0)
            {

                dlClassRoomProduct.DataSource = dsGrid;
                dlClassRoomProduct.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                //dlGridExport.DataSource = dsGrid;
                //dlGridExport.DataBind();
            }
            else
            {
                Show_Error_Success_Box("E", "Record  Not Found");
                ControlVisibility("Search");
                return;
            }
        }
        else
        {
            dlClassRoomProduct.DataSource = null;
            dlClassRoomProduct.DataBind();
            lbltotalcount.Text = "0";

            //dlGridExport.DataSource = null;
            //dlGridExport.DataBind();
        }

    }
    private void Fill_Grid1()
    {
        string DivisionCode = null;
        DivisionCode = ddlDivisionAdd.SelectedValue;

        string acadyear = null;
        acadyear = ddlAcadYearAdd.SelectedValue;
        string stream = null;
        stream = txtStreamName.Text.Trim();

        Clear_Error_Success_Box();
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.Get_SAPClassroomProduct(DivisionCode, acadyear, stream, "", "", "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlClassRoomProduct.DataSource = dsGrid;
                dlClassRoomProduct.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

          
            }
   
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
        }

        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;
            btnTopSearch.Visible = true;
            divSubject.Visible = false;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;

        }
        else if (Mode == "SubjectGroup")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;

        }
        else if (Mode == "PricingItem")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;

        }
        else if (Mode == "PricingHeader")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;

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
            txtStreamName.Text = "";

            //ddlStatus.SelectedIndex = 0;
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
            Clear_ClassRoomProductAddPanel();
            divRegst.Visible = false;
            divSubject.Visible = false;
            divsubjects.Visible = false;
            divsubjectsedit.Visible = false; divsubedit.Visible = false;
            diveditreg.Visible = false; Divedit.Visible = false;

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    //protected void Savecourse()
    protected void BtnRegSave_Click(object sender, EventArgs e)
    {


        //Saving part
        string DivisionCode = null;
        DivisionCode = ddlDivisionAdd.SelectedValue;

        string acadyear = null;
        acadyear = ddlAcadYearAdd.SelectedValue;

        if (acadyear == "Select")
        {
            Show_Error_Success_Box("E", "Please Select Acad Year ");

            ddlAcadYearAdd.Focus();
            return;
        }
        else
        {
            Clear_Error_Success_Box();
        }

        string course = null;
        course = ddlClassRoomCourse.SelectedValue;
        if (course == "Select")
        {
            Show_Error_Success_Box("E", "Please Select Course ");
            return;
        }

        string streamname = null;
        streamname = txtProductName.Text;
        if (streamname == "")
        {
            Show_Error_Success_Box("E", "Please Add Stream Name ");
            return;
        }


        string Desc = null;
        Desc = txtDescription.Text;
        if (Desc == "")
        {
            Show_Error_Success_Box("E", "Please Add Stream Description");
            return;
        }

        string Feezone = null;
        Feezone = ddlFeesZone.SelectedValue;
        if (Feezone == "Select")
        {
            Show_Error_Success_Box("E", "Please Select FeesZone");
            return;
        }

        string coursedate = null;
        coursedate = txtCoursePeriod.Value;
        if (coursedate == "")
        {
            Show_Error_Success_Box("E", "Please Select Course Date");
            return;
        }

        string admdate = null;
        admdate = txtAdmissionPeriod.Value;
        if (admdate == "")
        {
            Show_Error_Success_Box("E", "Please Select Admission Date");
            return;
        }

        string centercode = null;
        //centercode = ddlCenter.SelectedValue;

        for (int cnt = 0; cnt <= ddlCenter.Items.Count - 1; cnt++)
        {
            if (ddlCenter.Items[cnt].Selected == true)
            {
                centercode = centercode + ddlCenter.Items[cnt].Value + ",";
            }
        }
        if (centercode == "")
        {
            Show_Error_Success_Box("E", "Atleast one Center should be selected");
            ddlCenter.Focus();
            return;
        }

        centercode = centercode.Substring(0, centercode.Length - 1);
        coursedate = txtCoursePeriod.Value;
        Crs_SDate = coursedate.Substring(0, 10);
        Crs_EDate = coursedate.Substring(13, 10);

        admdate = txtAdmissionPeriod.Value;
        Adm_SDate = admdate.Substring(0, 10);
        Adm_EDate = admdate.Substring(13, 10);

        string Subjectdate;
      

        string txtprise = String.Empty;
        string txtCRF = string.Empty;
        string TxtCRFvalue = string.Empty;
        string txtTotal = string.Empty;
        string sujectgruopcode = "";
        string Subjectcode = "";
       // CheckBox s = sender as CheckBox;
      


            string ResultId = "0";
           
                foreach (DataListItem TextBox in dlSubjects1.Items)
                {

                    
                    Label lblSubCode = (Label)TextBox.FindControl("lblSubCode");
                    Label lblsubgrup = (Label)TextBox.FindControl("lblsubgrup");
                    //HtmlTextBox txtsubjdate = (HtmlInputText)TextBox.FindControl("txtsubjdate");
                    TextBox txtsubjdate = (TextBox)TextBox.FindControl("txtsubjdate");

                    Subjectdate = txtsubjdate.Text;
                    Sub_Sdate = Subjectdate.Substring(0, 10);
                    Sub_Edate = Subjectdate.Substring(13, 10);

                    txtprise = (TextBox.FindControl("txtprise") as TextBox).Text;
                    txtCRF = (TextBox.FindControl("txtCRF") as TextBox).Text;
                    TxtCRFvalue = (TextBox.FindControl("TxtCRFvalue") as TextBox).Text;
                    txtTotal = (TextBox.FindControl("txtTotal") as TextBox).Text;
                    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                    string UserID = cookie.Values["UserID"];
                    string UserName = cookie.Values["UserName"];
                    Subjectcode = lblSubCode.Text;
                    sujectgruopcode = lblsubgrup.Text;

                    ResultId = ProductController.InsertUpdateSubjectgrp(DivisionCode, acadyear, course, streamname, Desc, Feezone,
                        Crs_SDate, Crs_EDate, Adm_SDate, Adm_EDate, centercode, sujectgruopcode, Subjectcode, Sub_Sdate, Sub_Edate, txtprise, txtCRF, TxtCRFvalue, txtTotal, "1", UserID);

                    if (ResultId == "-1")
                    {
                        Show_Error_Success_Box("E", " Duplicate Master Data ");
                 
                    }
                    else
                    {

                        SaveRegistrationfees();
                        DivAddPanel.Visible = false;
                        divRegst.Visible = false;
                        DivResultPanel.Visible = true;
                        divSubject.Visible = false;
                        divsubjects.Visible = false;
                        Fill_Grid1();
                        ControlVisibility("Result");
                        UpdatePanel1.Update();
                        Show_Error_Success_Box("S", "Stream Details are Saved Successfully");                
                        
                        
                    }
                }

            //}
        
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
            
         

            string CenterCode = "", 
                CoursePeriod = "", Crs_SDate = "", Crs_EDate = "", AdmPeriod = "", Adm_SDate = "", Adm_EDate = "", AlowDpFlag = "0", DpDate = "", MaxChequeFlag = "0", ChequeDate = "", MaxNoOfChequesInFullDP = "", MaxDayGapInChequesInFullDP = "";
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
    
    protected void SaveRegistrationfees()
   
    {
        string DivisionCode = null;
        DivisionCode = ddlDivisionAdd.SelectedValue;

        string acadyear = null;
        acadyear = ddlAcadYearAdd.SelectedValue;

        string course = null;
        course = ddlClassRoomCourse.SelectedValue;



        string Feezone = null;
        Feezone = ddlFeesZone.SelectedValue;

        string coursedate = null;
        coursedate = txtCoursePeriod.Value;
        Crs_SDate = coursedate.Substring(0, 10);
        Crs_EDate = coursedate.Substring(13, 10);

        //string Sdate = "";
        //string Edate = "";

        string txtAmount = string.Empty;
        string Regcode = string.Empty;
        string VouchrName = string.Empty;
        string matcode = string.Empty;
        string txtedate = string.Empty;

        string ResultId = "0";
        string FDate = "";
        

        foreach (DataListItem TextBox in dsregst.Items)
        {

            CheckBox chkitemck = (CheckBox)TextBox.FindControl("chkCheck");
            Label lblRegcode = (Label)TextBox.FindControl("lblRegcode");
            if (chkitemck.Checked == true)
            {
                matcode = matcode + lblRegcode.Text + ",";
                Label lalvoutype = (Label)TextBox.FindControl("lalvoutype");
                // txtPeriod1.Value



                txtAmount = (TextBox.FindControl("txtAmount") as TextBox).Text;

                matcode = lblRegcode.Text;
                VouchrName = lalvoutype.Text;
                HtmlInputText txtPeriod1 = (HtmlInputText)TextBox.FindControl("txtPeriod1");
                
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];


                ResultId = ProductController.InsertUpdaterobomate(DivisionCode, course, Feezone,
                    Crs_SDate, Crs_EDate, txtPeriod1.Value, "", matcode, VouchrName, txtAmount, "2", UserID, acadyear);
            }


        }



        //if (ResultId == "0")
        //{
        //    Fill_Grid1();
        //    ControlVisibility("Result");
        //    Show_Error_Success_Box("S", "Stream Details are Saved Successfully");

        //    DivAddPanel.Visible = false;
        //    divRegst.Visible = false;
        //    DivResultPanel.Visible = true;
        //    UpdatePanel1.Update();
        //}


        //if (ResultId == "-1")
        //{

        //    Show_Error_Success_Box("E", "Stream Details are Already Exist");


        //}


    }
    protected void btnCloseItemLevel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
         
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
     #endregion
    /// <summary>
    /// added by sujeer for new changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            FillDDL_Center();
            FillDDL_ClassRoomCourse();


        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void ddlClassRoomCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            divSubject.Visible = true;
            FillGrid_Get_subjectgroup();
            Label14.Text = "Select Subject Group ";

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string subjectgroupcode = "";

            CheckBox s = sender as CheckBox;
            //Set checked status of hidden check box to items in grid

            foreach (DataListItem dtlItem in dlSubjects.Items)
            {

                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lblSubgrCode = (Label)dtlItem.FindControl("lblSubgrCode");
                chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {

                    subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                }
                else
                {

                }
            }

            DataSet ds = ProductController.Get_subject_forSap1("2", ddlDivisionAdd.SelectedValue, subjectgroupcode,txtCoursePeriod.Value);
            
            dlSubjects1.DataSource = ds;
            dlSubjects1.DataBind();
            divsubjects.Visible = true;
            Label22.Text = " Add Subject Price ";
            FillRegistrationtab();
         
            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string subjectgroupcode = "";
            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dlSubjects.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lblSubgrCode = (Label)dtlItem.FindControl("lblSubgrCode");
                //chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {
                    subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                }
                else
                {

                }

                {

                    //subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                    DataSet ds = ProductController.Get_subject_forSap1("2", ddlDivisionAdd.SelectedValue, subjectgroupcode,txtCoursePeriod.Value);
                    dlSubjects1.DataSource = ds;
                    dlSubjects1.DataBind();
                    divsubjects.Visible = true;
                    Label22.Text = " Add Subject Price ";
                    FillRegistrationtab();
                    
                }

            }




            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    //private void FillGrid_Module()
    //{

    //    string DivisionCode = null;
    //    DivisionCode = ddlDivisionAdd.SelectedValue;

    //    string centercode = null;
    //    centercode = ddlCenter.SelectedValue;



    //    DataSet dsGrid = ProductController.Get_subject_forSap(DivisionCode, centercode, "1");

    //    //Copy dsGrid content from DataSet to DataTable
    //    DataTable dtGrid = null;
    //    dtGrid = dsGrid.Tables[0];

    //    //Add 1 Blank records
    //    dtGrid.Rows.Add(0, 0, 0, 0, 0, 0, 0, 1, 1);

    //    //dlGridDisplay.DataSource = dtGrid;

    //    //dlGridDisplay.DataSource = dsGrid;
    //    // dlGridDisplay.DataBind();
    //    lbltotalcount.Text = Convert.ToString(Convert.ToInt16(dsGrid.Tables[0].Rows.Count.ToString()) - 1);

    //}
    private void FillGrid_Get_subjectgroup()
    {
        try
        {
            DataSet dsSubject = ProductController.FillGrid_Get_subjectgroup("3", ddlDivisionAdd.SelectedValue, ddlClassRoomCourse.SelectedValue);
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
    public System.Web.UI.WebControls.DropDownList ddlSubjectGroup { get; set; }
    //protected void dlGridDisplay_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        var ddlSubjectGroup = e.Item.FindControl("ddlSubjectGroup") as DropDownList;
    //        DataSet ds = ProductController.Get_subjectgroup("3", ddlDivisionAdd.SelectedValue);
    //        BindDDL(ddlSubjectGroup, ds, "Material_Name", "Material_Code");
    //        ddlSubjectGroup.Items.Insert(0, "Select");

    //       Label lblSubjectGroupCode = (Label)e.Item.FindControl("lblSubjectGroupCode");

    //       ddlSubjectGroup.SelectedValue = lblSubjectGroupCode.Text;


    //        var ddlsubject = e.Item.FindControl("ddlsubject") as DropDownList;
    //        DataSet ds1 = ProductController.Get_subject_forSap1("2", ddlDivisionAdd.SelectedValue);
    //        BindDDL(ddlsubject, ds1, "Material_Name", "Material_Code");
    //        ddlsubject.Items.Insert(0, "Select");
    //        Label lblSubject = (Label)e.Item.FindControl("lblSubject");
    //        ddlsubject.SelectedValue = lblSubject.Text;


    //    }

    //}
    protected void txtprise_Changed(object sender, EventArgs e)
    {
        DataListItem currentRow = (DataListItem)((TextBox)sender).Parent;
        TextBox txtprise = (TextBox)currentRow.FindControl("txtprise");
        TextBox txtCRF = (TextBox)currentRow.FindControl("txtCRF");
        TextBox txtTotal = (TextBox)currentRow.FindControl("txtTotal");

        //int count = Convert.ToInt32(txtprise.Text) + Convert.ToInt32(txtCRF.Text);
        //txtTotal.Text = count.ToString();

    }
    protected void txtCRF_Changed(object sender, EventArgs e)
    {
        DataListItem currentRow = (DataListItem)((TextBox)sender).Parent;


        // DataList currentRow = (DataList)((TextBox)sender).Parent.Parent.Parent.Parent;
        TextBox txtprise = (TextBox)currentRow.FindControl("txtprise");
        TextBox txtCRF = (TextBox)currentRow.FindControl("txtCRF");
        TextBox txtTotal = (TextBox)currentRow.FindControl("txtTotal");
        TextBox TxtCRFvalue = (TextBox)currentRow.FindControl("TxtCRFvalue");

        int count = Convert.ToInt32(txtprise.Text) + Convert.ToInt32(txtCRF.Text);
        txtTotal.Text = count.ToString();

        decimal percent = (Convert.ToDecimal(txtCRF.Text) / Convert.ToDecimal(txtprise.Text)) * 100;
        TxtCRFvalue.Text = decimal.Round(percent, 2, MidpointRounding.AwayFromZero).ToString();


        //decimal percent = (100 - (Convert.ToDecimal(txtprise.Text) / Convert.ToDecimal(txtTotal.Text)) * 100);
        //TxtCRFvalue.Text = decimal.Round(percent, 2, MidpointRounding.AwayFromZero).ToString();



    }
    private void FillRegistrationtab()

   {

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsReg = ProductController.Get_Division_wiseCRF(ddlDivisionAdd.SelectedValue, ddlClassRoomCourse.SelectedValue,"6");
            dsregst.DataSource = dsReg;
            dsregst.DataBind();
            divRegst.Visible = true;
            Label24.Text = "Add Registration/Robomate Fee";
         

}
    public string Crs_SDate { get; set; }
    public string Crs_EDate { get; set; }
    public string Adm_SDate { get; set; }
    public string Adm_EDate { get; set; }
    public string Sub_Sdate { get; set; }
    public string Sub_Edate { get; set; }
    public int cnt { get; set; }
    //protected void dlClassRoomProduct_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    //{
    //    if (e.CommandName == "comSubGroup")
    //    {
    //        Lblpk.Text = e.CommandArgument.ToString();
    //        DataSet dsmaster = ProductController.FillGrid_mastertable("7", "", "");
    //        dladditem.DataSource = dsmaster;
    //        dladditem.DataBind();

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDivOverride();", true);
    //    }
    //}

    //protected void chkAll_CheckedChanged1(object sender, EventArgs e)
    //{
    //    CheckBox s = sender as CheckBox;
    //    foreach (DataListItem dtlItem in dladditem.Items)
    //    {
    //        CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");

    //        chkitemck.Checked = s.Checked;
    //    }
    //    UpdatePanel3.Update();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDivOverride();", true);
    //}
    protected void dlClassRoomProduct_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "comSubGroup")
        {
            Lblpk.Text = e.CommandArgument.ToString();

            string pk = null;
            pk = Lblpk.Text;

            DataSet ds = ProductController.FillGrid_mastertableexport(pk, "");

            ds.Tables[0].TableName = "Course Master";
            ds.Tables[1].TableName = "Batch Master";
            ds.Tables[2].TableName = "Price Master";
            ds.Tables[3].TableName = "Registration";
            ds.Tables[4].TableName = "Robomate ST";
            ds.Tables[5].TableName = "Discount Master";

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (DataTable dt in ds.Tables)
                {
                    //Add DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Stream Master File.xls");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        if (e.CommandName == "comDiscount") 
        {
            Lblpk.Text = e.CommandArgument.ToString();
            

            string pk = null;
            pk = Lblpk.Text;
            Session["Lblpk.Text"] = pk;
            //DataSet dsGrid = ProductController.Get_discount_Sap(pk,"", "3");
            Response.Redirect("Sap_Discount_Master.aspx", false);


        }

        if (e.CommandName == "Comeditstream")
        {
            Lblpk.Text = e.CommandArgument.ToString();


            string pk = null;
            pk = Lblpk.Text;
            Session["Lblpk.Text"] = pk;
            //DataSet dsGrid = ProductController.Get_discount_Sap(pk,"", "3");
            Response.Redirect("SAP_Stream_Master_Edit.aspx", false);


        }


        if (e.CommandName == "comdeleteclick")
        {
            Lblpk.Text = e.CommandArgument.ToString();


            string pk = null;
            pk = Lblpk.Text;
            Session["Lblpk.Text"] = pk;
            //DataSet dsGrid = ProductController.Get_discount_Sap(pk,"", "3");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
                


        }




        if (e.CommandName == "comaddcenter")
        {
            Clear_Error_Success_Box();
         divSubject.Visible = false; divsubjectsedit.Visible = false; divsubedit.Visible = false;
        divsubjects.Visible = false; divRegst.Visible = false; diveditrobomate.Visible = false;
        diveditreg.Visible = false; Divedit.Visible = false;
           
        
            Lblpk.Text = e.CommandArgument.ToString();


            string pk = null;
            pk = Lblpk.Text;
              DataSet dsGrid = ProductController.Getstreamdetailsforedite(pk);
              if (dsGrid != null)
              {
                  if (dsGrid.Tables[0].Rows.Count > 0)
                  {
                      ///FillDDL_Division();
                      Txtdiv1.Text = dsGrid.Tables[0].Rows[0]["divname"].ToString();
                      Txtdiv1.Enabled = false;
                      divisionname1.Text = dsGrid.Tables[0].Rows[0]["division"].ToString();
                      acadyear1.Text = dsGrid.Tables[0].Rows[0]["year"].ToString();
                      acadyear1.Enabled = false;
                      Txtcoursename1.Text = dsGrid.Tables[0].Rows[0]["cousename"].ToString();
                      Txtcoursename1.Enabled = false;
                      Classroom1.Text = dsGrid.Tables[0].Rows[0]["course"].ToString();
                      
                      txtstream1.Text = dsGrid.Tables[0].Rows[0]["streamname"].ToString();
                      txtstream1.Enabled = false;
                      txtbatch1.Text =dsGrid.Tables[0].Rows[0]["course"].ToString()+ dsGrid.Tables[0].Rows[0]["batch"].ToString();
                      txtbatch.Text = dsGrid.Tables[0].Rows[0]["batch"].ToString();
                      txtbatch1.Enabled = false;
                      FillDDL_Centeredite();

                      for (int cnt = 0; cnt <= dsGrid.Tables[0].Rows.Count - 1; cnt++)
                      {
                          for (int rcnt = 0; rcnt <= ddlCenteredite.Items.Count - 1; rcnt++)
                          {
                              if (ddlCenteredite.Items[rcnt].Value == dsGrid.Tables[0].Rows[cnt]["center"].ToString())
                              {
                                  ddlCenteredite.Items[rcnt].Selected = true;
                                  break;
                              }
                          }
                      }

                      //if (dsGrid.Tables[6].Rows.Count > 0)
                      //{

                      //    Dasubedit1.DataSource = dsGrid.Tables[6];
                      //    Dasubedit1.DataBind();
                      //    divsubeite1.Visible = true;
                      //    Label90.Text = " Edit Subject Price ";

                      //}

                      //if (dsGrid.Tables[7].Rows.Count >= 0)
                      //{

                      //    Daregedite1.DataSource = dsGrid.Tables[7];
                      //    Daregedite1.DataBind();
                      //    divregedite1.Visible = true;
                      //    Label92.Text = " Edit Registration Price ";

                      //}


                      //if (dsGrid.Tables[8].Rows.Count >= 0)
                      //{

                      //    Darobomate1.DataSource = dsGrid.Tables[8];
                      //    Darobomate1.DataBind();
                      //    divrobomate1.Visible = true;
                      //    Label94.Text = " Edit Robomate Price ";

                      //}

          

                      //ddlCenteredite.SelectedValue = dsGrid.Tables[0].Rows[0]["center"].ToString();
                      ddlCenteredite.Enabled = true;
                      Divcenteredit.Visible = true;
                      Label80.Text = " Edit Details";
                      

                  }
              }

        }

        if (e.CommandName == "comviewclick")
        {
            Divcenteredit.Visible = false;
            Clear_Error_Success_Box();
            
            Lblpk.Text = e.CommandArgument.ToString();
            string pk = null;
            pk=Lblpk.Text;
            DataSet dsGrid = ProductController.Getstreamdetailsforview(pk);
            if (dsGrid != null)
            {
                if (dsGrid.Tables[0].Rows.Count > 0)
                {
                    ///FillDDL_Division();
                    divisionname.Text = dsGrid.Tables[0].Rows[0]["divname"].ToString();
                    divisionname.Enabled = false;
                    Txtdiv.Text = dsGrid.Tables[0].Rows[0]["division"].ToString();
                    acadyear.Text = dsGrid.Tables[0].Rows[0]["year"].ToString();
                    acadyear.Enabled = false;
                    Txtclassroom1.Text = dsGrid.Tables[0].Rows[0]["cousename"].ToString();
                    Txtclassroom1.Enabled = false;
                    Classroom.Text = dsGrid.Tables[0].Rows[0]["course"].ToString();
                    txtstream.Text = dsGrid.Tables[0].Rows[0]["streamname"].ToString();
                    txtstream.Enabled = false;
                    txtbatch.Text = dsGrid.Tables[0].Rows[0]["course"].ToString()+ dsGrid.Tables[0].Rows[0]["batch"].ToString();
                    txtbatch.Enabled = false;
                    FillDDL_Center1();
                    for (int cnt = 0; cnt <= dsGrid.Tables[0].Rows.Count - 1; cnt++)
                    {
                        for (int rcnt = 0; rcnt <= ddlCenter1.Items.Count - 1; rcnt++)
                        {
                            if (ddlCenter1.Items[rcnt].Value == dsGrid.Tables[0].Rows[cnt]["center"].ToString())
                            {
                                ddlCenter1.Items[rcnt].Selected = true;
                                break;
                            }
                        }
                    }

           
                    
                    ddlCenter1.Enabled = false;
                    Divedit.Visible = true;
                    Label64.Text = " Batch Details";
                
                }

                if (dsGrid.Tables[1].Rows.Count > 0)
                {

                    Subedite1.DataSource = dsGrid.Tables[1];
                    Subedite1.DataBind();
                    divsubedit.Visible = true;
                    Label72.Text = " Subject group Details";

                }

                if (dsGrid.Tables[2].Rows.Count > 0)
                {
                    subjectedit.DataSource = dsGrid.Tables[2];
                    subjectedit.DataBind();
                    divsubjectsedit.Visible = true;
                    Label74.Text = " Subject Details";

                }
                if (dsGrid.Tables[3].Rows.Count > 0)
                {
                    registration.DataSource = dsGrid.Tables[3];
                    registration.DataBind();
                    diveditreg.Visible = true;
                    Lberror.Visible = false;
                    Label77.Text = " Registration Details";

                }
                if (dsGrid.Tables[3].Rows.Count <= 0)
                {
                    registration.DataSource = dsGrid.Tables[3];
                    registration.DataBind();
                    diveditreg.Visible = false;
                    registration.Visible = false;
                    Label77.Text = " Registration Details";
                    Lberror.Visible = true;
                    Lberror.Text = "Records Not Found";
                }


                if (dsGrid.Tables[4].Rows.Count > 0)
                {
                    robomate.DataSource = dsGrid.Tables[4];
                    robomate.DataBind();
                    diveditrobomate.Visible = true;
                    lbldlerror.Visible = false;
                    Label76.Text = " Robomate Details";

                }
                if (dsGrid.Tables[4].Rows.Count <= 0)
                {
                    robomate.DataSource = dsGrid.Tables[4];
                    robomate.DataBind();
                    diveditrobomate.Visible = true;
                    robomate.Visible = false;
                    Label76.Text = " Robomate Details";
                    lbldlerror.Visible = true;
                    lbldlerror.Text = "Records Not Found";

                }


                if (dsGrid.Tables[5].Rows.Count > 0)
                {
                    dsdiscount.DataSource = dsGrid.Tables[5];
                    dsdiscount.DataBind();
                    divDiscount.Visible = true;
                    lberor.Visible = false;
                    Label88.Text = " Discount Details";

                }
                if (dsGrid.Tables[5].Rows.Count <= 0)
                {
                    dsdiscount.DataSource = dsGrid.Tables[5];
                    dsdiscount.DataBind();
                    divDiscount.Visible = true;
                    dsdiscount.Visible = false;
                    Label88.Text = " Discount Details";
                    lberor.Visible = true;
                    lberor.Text = "Records Not Found";

                }



            }

        }
    }
    protected void btnTopSearch_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        divSubject.Visible = false; divsubjectsedit.Visible = false; divsubedit.Visible = false;
        divsubjects.Visible = false; divRegst.Visible = false; diveditrobomate.Visible = false;
        diveditreg.Visible = false; Divedit.Visible = false; Divcenteredit.Visible = false;
    }
    protected void ddlCenter1_SelectedIndexChanged(object sender, EventArgs e)
    {


        FillDDL_Center1();
           
    }
    protected void BtnSvaeedite_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

                divisionname1.Text.Trim();
                acadyear1.Text.Trim();
                Classroom1.Text.Trim();
                txtstream1.Text.Trim();
                txtbatch1.Text.Trim();

            string CenterCode = "";
            CenterCode = ddlCenteredite.SelectedValue;
                 if (CenterCode == "")
            {
                Show_Error_Success_Box("E", "Atleast one Center should be selected");
                ddlCenteredite.Focus();
                return;
            }
            //ADD NEW CENTER DETAILS
                 for (int cnt = 0; cnt <= ddlCenteredite.Items.Count - 1; cnt++)                
                   {
                       if (ddlCenteredite.Items[cnt].Selected == true)
                       {
                           CenterCode = ddlCenteredite.Items[cnt].Value;
                           divisionname1.Text.Trim();
                           acadyear1.Text.Trim();
                           Classroom1.Text.Trim();
                           txtstream1.Text.Trim();
                           txtbatch.Text.Trim();

                           //CenterCode = ddlCenteredite.SelectedValue;

                           HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                           string UserID = cookie.Values["UserID"];

                           string ResultId = "0";

                           ResultId = ProductController.Updatestreammaster(divisionname1.Text.Trim(), acadyear1.Text.Trim(), Classroom1.Text.Trim(), txtbatch.Text.Trim(), CenterCode, "", "", UserID);
                           if (ResultId == "")
                           {

                               ControlVisibility("Result");
                               Show_Error_Success_Box("S", " Saved Successfully");

                               Divcenteredit.Visible = false;
                               DivResultPanel.Visible = true;
                               UpdatePanel2.Update();
                           }
                       }
                 }

            //update stream master
                 string Subjectdate;
                 string txtprise = String.Empty;
                 string txtCRF = string.Empty;
                 string TxtCRFvalue = string.Empty;
                 string txtTotal = string.Empty;
                 string sujectgruopcode = "";
                 string Subjectcode = "";
                 string txtAmount = "";
                 string txtroboAmount = "";
                 string Regdate = "";
                 string Reg_Sdate = "";
                 string Robodate = "";
                 string Robo_Sdate = "";
                 // CheckBox s = sender as CheckBox;
                 string ResultId1 = "0";

                          Classroom1.Text.Trim();
                           txtstream1.Text.Trim();
                           txtbatch.Text.Trim();

                           foreach (DataListItem TextBox in Dasubedit1.Items)
                           {

                               Label lblSubCode = (Label)TextBox.FindControl("lblSubCode");
                               Label lblsubgrup = (Label)TextBox.FindControl("lblsubgrup");
                               //HtmlTextBox txtsubjdate = (HtmlInputText)TextBox.FindControl("txtsubjdate");
                               TextBox txtsubjdate = (TextBox)TextBox.FindControl("txtsubjdate");
                               Subjectdate = txtsubjdate.Text;
                               Sub_Sdate = Subjectdate.Substring(0, 10);
                               Sub_Edate = Subjectdate.Substring(13, 10);
                               txtprise = (TextBox.FindControl("txtprise") as TextBox).Text;
                               txtCRF = (TextBox.FindControl("txtCRF") as TextBox).Text;
                               TxtCRFvalue = (TextBox.FindControl("TxtCRFvalue") as TextBox).Text;
                               txtTotal = (TextBox.FindControl("txtTotal") as TextBox).Text;
                               HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                               string UserID = cookie.Values["UserID"];
                               string UserName = cookie.Values["UserName"];
                               Subjectcode = lblSubCode.Text;
                               sujectgruopcode = lblsubgrup.Text;
                               ResultId1 = ProductController.UpdateSubjectgrpdetails(sujectgruopcode, Subjectcode, Sub_Sdate, Sub_Edate, txtprise, txtCRF, TxtCRFvalue, txtTotal, "", "", "", "", "", "", UserID, Classroom1.Text.Trim(), txtbatch.Text.Trim(),"","");

                           }
                           foreach (DataListItem TextBox1 in Daregedite1.Items)
                           {
                               Label lblRegcode = (Label)TextBox1.FindControl("lblRegcode");
                               Label lalvoutype = (Label)TextBox1.FindControl("lalvoutype");
                               txtAmount = (TextBox1.FindControl("txtAmount") as TextBox).Text;
                               TextBox txtPeriod1 = (TextBox)TextBox1.FindControl("txtPeriod1");
                               Regdate = txtPeriod1.Text;
                               Reg_Sdate = Regdate.Substring(0, 10);
                               HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                              
                               string UserID = cookie.Values["UserID"];
                               string UserName = cookie.Values["UserName"];

                               ResultId1 = ProductController.UpdateSubjectgrpdetails("", "", "", "", "", "", "", "", lblRegcode.Text, lalvoutype.Text, txtAmount, "", "", "", UserID, Classroom1.Text.Trim(), txtbatch.Text.Trim(),Reg_Sdate,"");

                           }
                         foreach (DataListItem TextBox2 in Darobomate1.Items)
                         {
                             Label lblRobocode1 = (Label)TextBox2.FindControl("lblRobocode1");
                             Label lalrobvoutype = (Label)TextBox2.FindControl("lalrobvoutype");
                             txtroboAmount = (TextBox2.FindControl("txtroboAmount") as TextBox).Text;
                             TextBox txtPeriod1 = (TextBox)TextBox2.FindControl("txtPeriod1");
                             Robodate = txtPeriod1.Text;
                             Robo_Sdate = Regdate.Substring(0, 10);

                             HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                             string UserID = cookie.Values["UserID"];
                             string UserName = cookie.Values["UserName"];

                             ResultId1 = ProductController.UpdateSubjectgrpdetails("", "", "", "", "", "", "", "", "", "", "", lblRobocode1.Text, lalrobvoutype.Text, txtroboAmount, UserID, Classroom1.Text.Trim(), txtbatch.Text.Trim(),"", Robo_Sdate);

                         }
                                                                        
            
            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnclosseditClick(object sender, EventArgs e)
    {
        
        {

            ControlVisibility("Result");
            divSubject.Visible = false; divsubjectsedit.Visible = false; divsubedit.Visible = false;
            divsubjects.Visible = false; divRegst.Visible = false; diveditrobomate.Visible = false;
            diveditreg.Visible = false; Divedit.Visible = false; Divcenteredit.Visible = false;

        }
        
    }
    protected void btnDelete_Yes_Click(object sender, EventArgs e)
    {
        
        string pk = null;
        pk = Lblpk.Text;
        Session["Lblpk.Text"] = pk;
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet dsGrid = ProductController.Getstreamdetailsfordelete(pk, UserID);
        {
            Show_Error_Success_Box("S", " Stream Details Deleted Successfully");
            Clear_Error_Success_Box();
        }



    }
    
}