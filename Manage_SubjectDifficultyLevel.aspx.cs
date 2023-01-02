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

public partial class Manage_SubjectDifficultyLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_Board();
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
            Clear_Error_Success_Box();
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;

        }
        else if (Mode == "TopSearch")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;


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

    private void FillDDL_Board()
    {

        try
        {

            Clear_Error_Success_Box();
           

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetAllActiveBoard(UserID);
            BindDDL(ddlboard, dsDivision, "Long_Description", "ID");
            ddlboard.Items.Insert(0, "Select");
            ddlboard.SelectedIndex = 0;


            BindDDL(ddladdboard, dsDivision, "Long_Description", "ID");
            ddladdboard.Items.Insert(0, "Select");
            ddladdboard.SelectedIndex = 0;
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

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    protected void ddlboard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Course();
    }

    private void FillDDL_Course()
    {

        try
        {

            Clear_Error_Success_Box();


            string Div_Code = null;
            Div_Code = ddlboard.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
            BindDDL(ddlStandard, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlStandard.Items.Insert(0, "Select");
            ddlStandard.SelectedIndex = 0;



            BindDDL(ddladdCourseName, dsAllStandard, "Standard_Name", "Standard_Code");
            ddladdCourseName.Items.Insert(0, "Select");
            ddladdCourseName.SelectedIndex = 0;

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

    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add();
    }
    private void FillDDL_Subject_Add()
    {
        Clear_Error_Success_Box();
        string Div_Code = null;
        Div_Code = ddlboard.SelectedValue;

        string YearName = null;
        YearName = "";

        string StandardCode = null;
        string StandardCodeadd = null;
        StandardCode = ddlStandard.SelectedValue;
        StandardCodeadd = ddladdCourseName.SelectedValue;
        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);
        DataSet dsStandardforadd = ProductController.GetAllSubjectsByStandard(StandardCodeadd);
        BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
        ddlSubject.Items.Insert(0, "Select");
        ddlSubject.SelectedIndex = 0;

        BindDDL(ddladdsubjectname, dsStandardforadd, "Subject_ShortName", "Subject_Code");
        ddladdsubjectname.Items.Insert(0, "Select");
        ddladdsubjectname.SelectedIndex = 0;

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        ddladdboard.Enabled = true;
        ddladdCourseName.Enabled = true;
        ddladdsubjectname.Enabled = true;
        BtnSaveEdit.Visible = false;
        BtnSaveAdd.Visible = true;
        lblHeader_Add.Text = "Create New Subject Difficuly Level";
        FillDDL_Board();
        ddladdCourseName.Items.Clear();
        ddladdsubjectname.Items.Clear();
        txtaddmaxvalue.Text = "";
        txtaddminvalue.Text="";
    }
    protected void ddladdboard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Course();
    }
    protected void ddladdCourseName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add();
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivAddPanel.Visible = false;
        DivSearchPanel.Visible = true;
        DivResultPanel.Visible = false;
        ControlVisibility("Search");
        //FillDDL_Board();
        //ddlStandard.Items.Clear();
        //ddlSubject.Items.Clear();
        

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
    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddladdboard.SelectedIndex==0)
        {
            Show_Error_Success_Box("E", "0105");
            return;
        }

        if (ddladdCourseName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0106");
            return;
        }

        if (ddladdsubjectname.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0107");
            return;
        }

        if (txtaddminvalue.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0108");
            return;
        }

        if (txtaddmaxvalue.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0109");
            return;
        }
        string board = ddladdboard.SelectedValue;
        string course = ddladdCourseName.SelectedValue;
        string subject = ddladdsubjectname.SelectedValue;
        int mivalue = Convert.ToInt32(txtaddminvalue.Text);
        int maxvalue =Convert.ToInt32(txtaddmaxvalue.Text);

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string ds1 = ProductController.Insert_Subject_Difficulty_Level(board, course, subject, mivalue, maxvalue, UserID, ActiveFlag);
        if (ds1 == "Record Inserted Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            ddladdboard.SelectedIndex = 0;
            ddladdCourseName.SelectedIndex = 0;
            ddladdsubjectname.SelectedIndex = 0;
            chkActive.Checked = true;

            string allboard = "";
            string allcourse = "";
            string allsubject = "";

            DataSet dsGrid = ProductController.Get_Subject_Difficulty_Level(allboard, allcourse, allsubject);
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                BtnShowSearchPanel.Visible = true;
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "0104");
            }


        }
        else
        {
            Show_Error_Success_Box("E", "Subject Difficulty Level Already Exists For The Selected Criteria");
        }
        
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");
            BtnSaveAdd.Visible = false;
            BtnSaveEdit.Visible = true;
            lblHeader_Add.Text = "Edit Subject Difficuly Level";
            BtnAdd.Visible = true;
           // string record_id = e.CommandArgument.ToString();
            lblprimarykey.Text= Convert.ToString(e.CommandArgument);
            string record_id = Convert.ToString(lblprimarykey.Text);
            DataSet dsBatch = ProductController.GET_SUBJECT_DIFFICULTY_LEVEL_RECORD_ID(record_id);

            if (dsBatch.Tables[0].Rows.Count > 0)
            {

                ddladdboard.SelectedItem.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Long_Description"]);
                ddladdboard.Enabled = false;
                ddladdCourseName.SelectedItem.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Course_Name"]);
                ddladdCourseName.Enabled = false;
                ddladdsubjectname.SelectedItem.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Subject_Name"]);
                ddladdsubjectname.Enabled = false;
                txtaddminvalue.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Min_Value"]);
                txtaddmaxvalue.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Max_Value"]);
                int a = Convert.ToInt32(dsBatch.Tables[0].Rows[0]["is_active"]);

                if (a == 1)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

            }

        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        if (ddlboard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0105");
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0106");
            return;
        }
        string board = ddlboard.SelectedValue;
        string course = ddlStandard.SelectedValue;
        string subject = "";
        if (ddlSubject.SelectedItem.Text =="Select")
        {
             subject = "";
        }
        else
        {
             subject = ddlSubject.SelectedValue;
        }

        ControlVisibility("Search");
 
        DataSet dsGrid = ProductController.Get_Subject_Difficulty_Level(board, course, subject);
        if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();
            dlGridExport.DataSource = dsGrid;
            dlGridExport.DataBind();
            BtnShowSearchPanel.Visible = true;
            lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Show_Error_Success_Box("E", "0104");
            BtnSearch.Visible = true;
        }
   
        
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlboard.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlSubject.Items.Clear();
        ddlboard.Focus();

    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (txtaddminvalue.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0108");
            return;
        }

        if (txtaddmaxvalue.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0109");
            return;
        }
        string record_id=Convert.ToString(lblprimarykey.Text);
        int mivalue = Convert.ToInt32(txtaddminvalue.Text);
        int maxvalue = Convert.ToInt32(txtaddmaxvalue.Text);

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }
        

        string ds1 = ProductController.Update_Subject_Difficulty_Level(record_id,mivalue, maxvalue, UserID, ActiveFlag);
        if (ds1 == "Record Updated Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            ddladdboard.SelectedIndex = 0;
            ddladdCourseName.SelectedIndex = 0;
            ddladdsubjectname.SelectedIndex = 0;
            chkActive.Checked = true;

            string allboard = "";
            string allcourse = "";
            string allsubject = "";

            DataSet dsGrid = ProductController.Get_Subject_Difficulty_Level(allboard, allcourse, allsubject);
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                BtnShowSearchPanel.Visible = true;
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "0104");
            }


        }
        else
        {
            Show_Error_Success_Box("E", "0103");
        }
        
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        if (lblHeader_Add.Text == "Create New Subject Difficuly Level")
        { 
        ControlVisibility("Search");
        FillDDL_Board();
        ddlStandard.Items.Clear();
        ddlSubject.Items.Clear();
        }
        if (lblHeader_Add.Text == "Edit Subject Difficuly Level")
        {
            ControlVisibility("Result");
            FillDDL_Board();
            ddlStandard.Items.Clear();
            ddlSubject.Items.Clear();
        
        }
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Subject Difficulty Level  " + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Subject Difficulty Level</b></TD></TR>");
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
}