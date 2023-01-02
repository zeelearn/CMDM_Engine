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

using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;

partial class Master_Chapter : System.Web.UI.Page
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
        //DataSet dsDivision = null;
        //OrderDataService.OrderDataServiceSoapClient client = new OrderDataService.OrderDataServiceSoapClient();
        //dsDivision = client.GetCompany_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        //if (dsDivision != null)
        //{
        //    if (dsDivision.Tables.Count != 0)
        //    {
        //        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //    }
        //    else
        //    {
                
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //    }
            
        //}
        //else
        //{

        //    ddlDivision.Items.Insert(0, "Select");
        //    ddlDivision.SelectedIndex = 0;
        //}

        

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillGrid_Chapter()
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

        DataSet dsGrid = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(DivisionCode, "", StandardCode, SubjectCode,"1");

        //Copy dsGrid content from DataSet to DataTable
        DataTable dtGrid = null;
        dtGrid = dsGrid.Tables[0];

        //Add 1 Blank records
        dtGrid.Rows.Add("", "", "","", "",0, 0, 0,"", 0,0, 1, 1);

        dlGridDisplay.DataSource = dtGrid;

        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();

        lbltotalcount.Text = Convert.ToString(Convert.ToInt16(dsGrid.Tables[0].Rows.Count.ToString()) - 1);


    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid_Chapter();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
        BindDDL(ddlStandard, dsAllStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        ddlStandard.SelectedIndex = 0;

        //Dim YearName As String
        //YearName = ddlAcadyear.SelectedItem.ToString

        //Dim dsStandard As DataSet = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName)
        //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code")
        //ddlStandard.Items.Insert(0, "Select")
        //ddlStandard.SelectedIndex = 0
    }

    private void FillDDL_Subject_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = "";

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

        BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
        ddlSubject.Items.Insert(0, "Select");
        ddlSubject.SelectedIndex = 0;
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Subject_Add();
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        TextBox txtDLChapterShortName = (TextBox)e.Item.FindControl("txtDLChapterShortName");
        TextBox txtDLChapterName = (TextBox)e.Item.FindControl("txtDLChapterName");
        TextBox txtDLChapterDisplayName = (TextBox)e.Item.FindControl("txtDLChapterDisplayName");
        TextBox txtDLLectureCnt = (TextBox)e.Item.FindControl("txtDLLectureCnt");
        TextBox txtDLLectureMin = (TextBox)e.Item.FindControl("txtDLLectureMin");
        TextBox txtDLChapter_SequenceNo = (TextBox)e.Item.FindControl("txtDLChapter_SequenceNo");
        System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");
       
        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

        Label lblDLChapterShortName = (Label)e.Item.FindControl("lblDLChapterShortName");
        Label lblDLChapterName = (Label)e.Item.FindControl("lblDLChapterName");
        Label lblDLLectureCnt = (Label)e.Item.FindControl("lblDLLectureCnt");
        Label lblDLLectureMin = (Label)e.Item.FindControl("lblDLLectureMin");
        Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");
        Label lblDLChapterDisplayName = (Label)e.Item.FindControl("lblDLChapterDisplayName");
        Label lblDLCHapter_SequenceNo = (Label)e.Item.FindControl("lblDLCHapter_SequenceNo");

        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

        if (e.CommandName == "Edit")
        {
            txtDLChapterName.Visible = true;
            txtDLChapterDisplayName.Visible = true;
            txtDLChapterShortName.Visible = true;
            txtDLLectureCnt.Visible = true;
            txtDLLectureMin.Visible = true;
            txtDLChapter_SequenceNo.Visible = true;
            chkActiveFlag.Visible = true;

            lblDLChapterName.Visible = false;
            lblDLChapterShortName.Visible = false;
            lblDLLectureCnt.Visible = false;
            lblDLLectureMin.Visible = false;
            lblDLCHapter_SequenceNo.Visible = false;
            lblDLStatus.Visible = false;
            lblDLChapterDisplayName.Visible = false;

            lnkDLEdit.Visible = false;
            lnkDLSave.Visible = true;
            icon_Error.Visible = false;

            txtDLChapterShortName.Focus();
        }
        else if (e.CommandName == "Save")
        {
            //Validation
            if (string.IsNullOrEmpty(txtDLChapterName.Text.Trim()))
            {
                lbl_DLError.Title = "Enter Chapter Name";
                icon_Error.Visible = true;
                txtDLChapterName.Focus();
                return;
            }

            //Check if lecture count is a numeric value
            if (string.IsNullOrEmpty(txtDLLectureCnt.Text))
            {
                lbl_DLError.Title = "Enter number of lectures required for this Chapter";
                icon_Error.Visible = true;
                txtDLLectureCnt.Focus();
                return;
            }


            if (!IsNumeric(txtDLLectureCnt.Text))
            {

                lbl_DLError.Title = "Invalid entry in 'No. of Lectures' field";
                icon_Error.Visible = true;
                txtDLLectureCnt.Focus();
                return;
            }
            





            //Check if lecture min is a numeric value
            if (string.IsNullOrEmpty(txtDLLectureMin.Text))
            {
                lbl_DLError.Title = "Enter duration in minutes required for this Chapter";
                icon_Error.Visible = true;
                txtDLLectureMin.Focus();
                return;
            }

            if (!IsNumeric(txtDLLectureMin.Text))

            //if (Convert.ToBoolean(Convert.ToInt32(txtDLLectureMin.Text)) == false)
            {
                lbl_DLError.Title = "Invalid entry in 'Time in min' field";
                icon_Error.Visible = true;
                txtDLLectureMin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDLChapter_SequenceNo.Text))
            {
                lbl_DLError.Title = "Enter duration in minutes required for this Chapter";
                icon_Error.Visible = true;
                txtDLChapter_SequenceNo.Focus();
                return;
            }

            if (!IsNumeric(txtDLChapter_SequenceNo.Text))

            //if (Convert.ToBoolean(Convert.ToInt32(txtDLLectureMin.Text)) == false)
            {
                lbl_DLError.Title = "Invalid entry in 'Time in min' field";
                icon_Error.Visible = true;
                txtDLChapter_SequenceNo.Focus();
                return;
            }

            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = "";

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            string CreatedBy = null;
            CreatedBy = UserID;

            string ChapterCodeForEdit = null;
            ChapterCodeForEdit = e.CommandArgument.ToString();

            string ActiveFlag = "0";
            if (chkActiveFlag.Checked == true)            
                ActiveFlag = "1";            
            else            
                ActiveFlag = "0";   

            int ResultId = 0;
            //Mark exemption/absent/present for those students who are selected
            ResultId = ProductController.Insert_Chapter(DivisionCode, YearName, StandardCode, SubjectCode, txtDLChapterName.Text.Trim(), txtDLChapterDisplayName.Text.Trim(), Convert.ToDouble(txtDLLectureCnt.Text), Convert.ToInt32(txtDLLectureMin.Text), txtDLChapterShortName.Text, ChapterCodeForEdit, CreatedBy, ActiveFlag,Convert .ToInt32(txtDLChapter_SequenceNo.Text.Trim ()));

            if (ResultId == -1)
            {
                lbl_DLError.Title = "Duplicate chapter name or code";
                icon_Error.Visible = true;
                txtDLChapterName.Focus();
                return;
            }
            else
            {
                icon_Error.Visible = false;
                Send_Chapter_Details_LMS(DivisionCode, YearName, StandardCode, SubjectCode, txtDLChapterName.Text.Trim());
            }

            //Change look
            txtDLChapterName.Visible = false;
            txtDLChapterShortName.Visible = false;
            txtDLLectureCnt.Visible = false;
            txtDLLectureMin.Visible = false;
            chkActiveFlag.Visible = false;


            lblDLChapterName.Visible = true;
            lblDLChapterShortName.Visible = true;
            lblDLLectureCnt.Visible = true;
            lblDLLectureMin.Visible = true;
            lblDLStatus.Visible = true;
            lblDLChapterDisplayName.Visible = true;


            lnkDLEdit.Visible = true;
            lnkDLSave.Visible = false;

            FillGrid_Chapter();
        }

    }

    public Master_Chapter()
    {
        Load += Page_Load;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Chapter_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Chapter</b></TD></TR><TR><TD Colspan='2'><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Course : " + ddlStandard.SelectedItem.ToString() + "</b></TD><TD Colspan='3'><b>Subject : " + ddlSubject.SelectedItem.ToString() + "</b></TD></TR><TR></TR>");
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlStandard.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
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

    protected void dlGridDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    private void Send_Chapter_Details_LMS(string DivisionCode, string YearName, string StandardCode, string SubjectCode, string ChapterName)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        try
        {
            DataSet dsdetails = ProductController.GET_Chapter_DETAILS(DivisionCode, YearName, StandardCode, SubjectCode, ChapterName);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DBConnection.connStringLMS);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Chapterdetailsinsert = new Chapterlist();
                Chapterdetailsinsert.ChapterId = null;
                Chapterdetailsinsert.ChapterCode = dsdetails.Tables[0].Rows[0]["Chapter_Code"].ToString();
                Chapterdetailsinsert.ChapterName = dsdetails.Tables[0].Rows[0]["Chapter_Name"].ToString();
                Chapterdetailsinsert.ChapterDisplayName = dsdetails.Tables[0].Rows[0]["Chapter_DisplayName"].ToString();
                Chapterdetailsinsert.ChapterDescription = dsdetails.Tables[0].Rows[0]["Chapter_ShortName"].ToString();
                Chapterdetailsinsert.ChapterSequenceNo = Convert.ToInt32( dsdetails.Tables[0].Rows[0]["ChapterSequenceNo"].ToString());
                Chapterdetailsinsert.SubjectCode = dsdetails.Tables[0].Rows[0]["Subject_Code"].ToString();
                Chapterdetailsinsert.Reference_Course = null;
                Chapterdetailsinsert.Reference_Subject = null;
                Chapterdetailsinsert.Reference_Chapter = null;
                Chapterdetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Chapterdetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Chapterdetailsinsert.CreatedBy = UserID;
                Chapterdetailsinsert.ModifiedBy = UserID;
                Chapterdetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsActive"]);
                Chapterdetailsinsert.IsDeleted = false;

                var response = client.PostAsJsonAsync("Chapter/addUpdChapter", Chapterdetailsinsert).Result;

                Response_Status_Code = response.StatusCode.ToString();
                Response_Return_Phrase = response.ReasonPhrase;

                if (response.StatusCode.ToString() == "OK")
                {
                }
                else
                {
                    // DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
                }
            }



        }
        catch (Exception e)
        {
            //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, Response_Status_Code, Response_Return_Phrase, UserID);
        }
    }
    class Chapterlist
    {
        public string ChapterId { get; set; }
        public string ChapterCode { get; set; }
        public string ChapterName { get; set; }
        public string ChapterDisplayName { get; set; }
        public string ChapterDescription { get; set; }
        public int ChapterSequenceNo { get; set; }
        public string SubjectCode { get; set; }
        public string Reference_Course { get; set; }
        public string Reference_Subject { get; set; }
        public string Reference_Chapter { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }


    }
}
