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
using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;

partial class Master_Module : System.Web.UI.Page
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

    private void FillGrid_Module()
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

        if (ddlChapter.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Chapter");
            ddlChapter.Focus();
            return;
        }

        //Comment on 26 May 2015
        //if (ddlTopic.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Topic");
        //    ddlTopic.Focus();
        //    return;
        //}

        //if (ddlSubTopic.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select SubTopic");
        //    ddlSubTopic.Focus();
        //    return;
        //}

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlStandard.SelectedValue;

        string SubjectCode = "";
        SubjectCode = ddlSubject.SelectedValue;

        string ChapterCode = "";
        ChapterCode = ddlChapter.SelectedValue;
        string TopicCode = "";
        string SubTopicCode = "";
        if (ddlTopic.SelectedIndex != 0)
        {           
            TopicCode = ddlTopic.SelectedValue;
            if (ddlSubTopic.SelectedIndex != 0)
            {
                SubTopicCode = ddlSubTopic.SelectedValue;
            }
        }  

        
        

        DataSet dsGrid = ProductController.Get_ModuleDetail("", DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, "4");

        //Copy dsGrid content from DataSet to DataTable
        DataTable dtGrid = null;
        dtGrid = dsGrid.Tables[0];

        //Add 1 Blank records
        dtGrid.Rows.Add("", "", "", "", 0, 0, 0, 1, 1);

        dlGridDisplay.DataSource = dtGrid;

        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();
        lblChapter_Result.Text = ddlChapter.SelectedItem.ToString();
        
        if (ddlTopic.SelectedItem.ToString() == "Select")
        {
            lblTopic_Result.Text = "";
            lblSubTopic_Result.Text = "";
        }
        else
        {
            lblTopic_Result.Text = ddlTopic.SelectedItem.ToString();            
        }

        if (ddlSubTopic.SelectedIndex != -1)
        {
            if ((ddlSubTopic.SelectedItem.ToString() != "Select"))
                lblSubTopic_Result.Text = ddlSubTopic.SelectedItem.ToString();
            else
                lblSubTopic_Result.Text = "";            
        }


        lbltotalcount.Text = Convert.ToString(Convert.ToInt16(dsGrid.Tables[0].Rows.Count.ToString()) - 1); 
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid_Module();
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

        

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

        BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
        ddlSubject.Items.Insert(0, "Select");
        ddlSubject.SelectedIndex = 0;
    }

    private void FillDDL_Chapter()
    {
        //public static DataSet GetAllChaptersBy_Division_Year_Standard_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode)
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        DataSet dschapter = new DataSet();
        dschapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(ddlDivision.SelectedValue, string.Empty, ddlStandard.SelectedValue, ddlSubject.SelectedValue);
        BindDDL(ddlChapter, dschapter, "chapter_name", "chapter_code");
        ddlChapter.Items.Insert(0, "Select");
        ddlChapter.SelectedIndex = 0;

    }

    /// <summary>
    /// Fill Topic drop down list
    /// </summary>
    private void FillTopic()
    {       
        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlStandard.SelectedValue;

        string SubjectCode = "";
        SubjectCode = ddlSubject.SelectedValue;

        string ChapterCode = "";
        ChapterCode = ddlChapter.SelectedValue;

        DataSet dsGrid = ProductController.GetAllTopicsBy_Division_Year_Standard_Subject_Chapter(DivisionCode, StandardCode, SubjectCode, ChapterCode, "1");
        BindDDL(ddlTopic, dsGrid, "Topic_Name", "Topic_Code");
        ddlTopic.Items.Insert(0, "Select");
        ddlTopic.SelectedIndex = 0;
    }

    /// <summary>
    /// Fill SubTopic drop down list
    /// </summary>
    private void FillSubTopic_Add()
    {
       
        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlStandard.SelectedValue;

        string SubjectCode = "";
        SubjectCode = ddlSubject.SelectedValue;

        string ChapterCode = "";
        ChapterCode = ddlChapter.SelectedValue;

        string TopicCode = "";
        TopicCode = ddlTopic.SelectedValue;

        DataSet dsSubTopic = ProductController.GetAllSubTopicsBy_Division_Year_Standard_Subject_Chapter_Topic(DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, "1");
        BindDDL(ddlSubTopic, dsSubTopic, "SubTopic_Name", "SubTopic_Code");
        ddlSubTopic.Items.Insert(0, "Select");
        ddlSubTopic.SelectedIndex = 0;
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Subject_Add();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Chapter();
    }


    protected void ddlChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTopic();
    }

    protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSubTopic_Add();
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {

        TextBox txtDLTopicName = (TextBox)e.Item.FindControl("txtDLTopicName");
        TextBox txtDLDisplayName = (TextBox)e.Item.FindControl("txtDLDisplayName");
        TextBox txtDLTopic_Description = (TextBox)e.Item.FindControl("txtDLTopic_Description");
        TextBox txtDLTopic_SequenceNo = (TextBox)e.Item.FindControl("txtDLTopic_SequenceNo");
        System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");
       
        //HtmlInputButton chkActiveFlag = (HtmlInputButton)e.Item.FindControl("chkActiveFlag");
        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

        Label lblDLTopicName = (Label)e.Item.FindControl("lblDLTopicName");
        Label lblDLDisplayName = (Label)e.Item.FindControl("lblDLDisplayName");
        Label lblDLTopic_Description = (Label)e.Item.FindControl("lblDLTopic_Description");
        Label lblDLTopic_SequenceNo = (Label)e.Item.FindControl("lblDLTopic_SequenceNo");
        Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");
        Label lblDLTopic_Code=(Label)e.Item.FindControl("lblDLTopic_Code");

        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

        if (e.CommandName == "Edit")
        {
            txtDLDisplayName.Visible = true;
            txtDLTopicName.Visible = true;
            txtDLTopic_Description.Visible = true;
            txtDLTopic_SequenceNo.Visible = true;
            chkActiveFlag.Visible = true;            

            lblDLDisplayName.Visible = false;
            lblDLTopicName.Visible = false;
            lblDLTopic_Description.Visible = false;
            lblDLTopic_SequenceNo.Visible = false;
            lblDLStatus.Visible = false;

            lnkDLEdit.Visible = false;
            lnkDLSave.Visible = true;
            icon_Error.Visible = false;

            txtDLTopicName.Focus();
        }
        else if (e.CommandName == "Save")
        {
            //Validation
            if (string.IsNullOrEmpty(txtDLTopicName.Text.Trim()))
            {
                lbl_DLError.Title = "Enter Module Name";
                icon_Error.Visible = true;
                txtDLTopicName.Focus();
                return;
            }

            //Check if lecture count is a numeric value
            if (string.IsNullOrEmpty(txtDLTopic_SequenceNo.Text))
            {
                lbl_DLError.Title = "Sequence no is numeric";
                icon_Error.Visible = true;
                txtDLTopic_SequenceNo.Focus();
                return;
            }

            try
            {
                int Seqno = Convert.ToInt32(txtDLTopic_SequenceNo.Text);
            }
            catch (Exception)
            {
                lbl_DLError.Title = "Sequence no is numeric";
                icon_Error.Visible = true;
                txtDLTopic_SequenceNo.Focus();
                return;
            }
            
            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

           

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            string ChapterCode = "";
            ChapterCode = ddlChapter.SelectedValue;

            string TopicCode = "";
            string SubTopicCode = "";

            if (ddlTopic.SelectedIndex != 0)
            {
                TopicCode = ddlTopic.SelectedValue;
                if (ddlSubTopic.SelectedIndex != 0)
                {
                    SubTopicCode = ddlSubTopic.SelectedValue;
                }
            }
            
            

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];     

            string ChapterCodeForEdit = null;
            ChapterCodeForEdit = e.CommandArgument.ToString();

            int ActiveFlag = 0;
            if (chkActiveFlag.Checked == true)            
                ActiveFlag = 1;            
            else            
                ActiveFlag = 0;            

            int ResultId = 0;                           
            ResultId = ProductController.InsertUpdateModule(lblDLTopic_Code.Text, DivisionCode, StandardCode, SubjectCode, ChapterCode, TopicCode, SubTopicCode, txtDLTopicName.Text, txtDLDisplayName.Text, txtDLTopic_Description.Text, txtDLTopic_SequenceNo.Text, ActiveFlag, UserID, "1");
            
            if (ResultId == -1)
            {
                lbl_DLError.Title = "Duplicate Module name";
                icon_Error.Visible = true;
                txtDLTopicName.Focus();
                return;
            }
            else
            {
                icon_Error.Visible = false;
                Send_Module_Details_LMS(DivisionCode, "", StandardCode, SubjectCode, ChapterCode, TopicCode, txtDLTopicName.Text);
            }

            //Change look
            txtDLDisplayName.Visible = false;
            txtDLTopicName.Visible = false;
            txtDLTopic_Description.Visible = false;
            txtDLTopic_SequenceNo.Visible = false;
            chkActiveFlag.Visible = false;

            lblDLDisplayName.Visible = true;
            lblDLTopicName.Visible = true;
            lblDLTopic_Description.Visible = true;
            lblDLTopic_SequenceNo.Visible = true;
            lblDLStatus.Visible = true;

            lnkDLEdit.Visible = true;
            lnkDLSave.Visible = false;

            FillGrid_Module();
        }

    }

    /// <summary>
    /// Clear Search Panel 
    /// </summary>
    private void ClearSearchPanel()
    {
        ddlDivision.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlSubject.Items.Clear();
        ddlChapter.Items.Clear();
        ddlTopic.Items.Clear();
        ddlSubTopic.Items.Clear();
    }


    public Master_Module()
    {
        Load += Page_Load;
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearchPanel();
    }

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
    

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Module_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Module</b></TD></TR><TR><TD Colspan='2'><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Course : " + ddlStandard.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Subject : " + ddlSubject.SelectedItem.ToString() + "</b></TD></TR><TR><TD Colspan='2'><b>Chapter : " + ddlChapter.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Topic : " + ddlTopic.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Sub Topic : " + ddlSubTopic.SelectedItem.ToString() + "</b></TD></TR><TR></TR>");
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

    private void Send_Module_Details_LMS(string DivisionCode, string YearName, string StandardCode, string SubjectCode, string ChapterName, string Topicname, string Module)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        try
        {
            DataSet dsdetails = ProductController.GET_Module_DETAILS(DivisionCode, YearName, StandardCode, SubjectCode, ChapterName, Topicname, Module);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DBConnection.connStringLMS);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Moduledetailsinsert = new Modulelist();
                Moduledetailsinsert.ModuleId = null;
                Moduledetailsinsert.ModuleCode = dsdetails.Tables[0].Rows[0]["Module_Code"].ToString();
                Moduledetailsinsert.ModuleNo = dsdetails.Tables[0].Rows[0]["Module_SequenceNo"].ToString();
                Moduledetailsinsert.ModuleName = dsdetails.Tables[0].Rows[0]["Module_Name"].ToString();
                Moduledetailsinsert.ModuleDisplayName = dsdetails.Tables[0].Rows[0]["Module_DisplayName"].ToString();
                Moduledetailsinsert.ModuleDescription = dsdetails.Tables[0].Rows[0]["Module_Description"].ToString();
                Moduledetailsinsert.CourseCode = dsdetails.Tables[0].Rows[0]["Standard_Code"].ToString();
                Moduledetailsinsert.SubjectCode = dsdetails.Tables[0].Rows[0]["Subject_Code"].ToString();
                Moduledetailsinsert.ChapterCode = dsdetails.Tables[0].Rows[0]["Chapter_Code"].ToString();
                Moduledetailsinsert.TopicCode = dsdetails.Tables[0].Rows[0]["Topic_Code"].ToString();
                Moduledetailsinsert.SubTopicCode = dsdetails.Tables[0].Rows[0]["SubTopic_Code"].ToString();
                Moduledetailsinsert.Reference_Course = null;
                Moduledetailsinsert.Reference_Subject = null;
                Moduledetailsinsert.Reference_Chapter = null;
                Moduledetailsinsert.Reference_Topic = null;
                Moduledetailsinsert.Reference_SubTopic = null;
                Moduledetailsinsert.Reference_Module = null;
                Moduledetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Moduledetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Moduledetailsinsert.CreatedBy = UserID;
                Moduledetailsinsert.ModifiedBy = UserID;
                Moduledetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsActive"]);
                Moduledetailsinsert.IsDeleted = false;

                var response = client.PostAsJsonAsync("Module/addUpdModule", Moduledetailsinsert).Result;

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
    class Modulelist
    {
        public string ModuleId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDisplayName { get; set; }
        public string  ModuleDescription { get; set; }
        public string CourseCode { get; set; }
        public string SubjectCode { get; set; }
        public string ChapterCode { get; set; }
        public string TopicCode { get; set; }
        public string SubTopicCode { get; set; }
        public string Reference_Course { get; set; }
        public string Reference_Subject { get; set; }
        public string Reference_Chapter { get; set; }
        public string Reference_Topic { get; set; }
        public string Reference_SubTopic { get; set; }
        public string Reference_Module { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }


    }
}
