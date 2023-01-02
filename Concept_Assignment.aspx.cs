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

public partial class Concept_Assignment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_Standard();

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
            Clear_Error_Success_Box();
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            FillDDL_Standard();
            DivResultPanel.Visible = false;


        }

    }


    private void FillDDL_Standard()
    {
        try
        {

            Clear_Error_Success_Box();


           // Div_Code = ddlDivisionAdd.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
            BindDDL(ddladdCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddladdCourse.Items.Insert(0, "Select");
            ddladdCourse.SelectedIndex = 0;

            BindDDL(ddlStandard, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlStandard.Items.Insert(0, "Select");
            ddlStandard.SelectedIndex = 0;
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
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        BtnSaveAdd.Visible = true;
        BtnSaveEdit.Visible = false;
        ddladdCourse.Enabled = true;
        ddladdSubject.Items.Clear();
        ddladdSubject.Enabled = true;
        ddladdchapter.Items.Clear();
        ddladdchapter.Enabled = true;
        ddladdconcepts.Items.Clear();
        ddladdconcepts.Enabled = true;
        chkActive.Checked = true;
        lblHeader_Add.Text = "Assign Concepts";
    }
    protected void ddladdCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Subject_Add();
        ddladdchapter.Items.Clear();
    }
    private void FillDDL_Subject_Add()
    {
       
        



        string StandardCode = null;
        StandardCode = ddladdCourse.SelectedValue;
        string standardcodeforsearch = null;
        standardcodeforsearch = ddlStandard.SelectedValue;

        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);
        DataSet dsStandarddorsearch = ProductController.GetAllSubjectsByStandard(standardcodeforsearch);

        BindDDL(ddladdSubject, dsStandard, "Subject_ShortName", "Subject_Code");
        ddladdSubject.Items.Insert(0, "Select");
        ddladdSubject.SelectedIndex = 0;

        BindDDL(ddlsubject, dsStandarddorsearch, "Subject_ShortName", "Subject_Code");
        ddlsubject.Items.Insert(0, "Select");
        ddlsubject.SelectedIndex = 0;
    }

    protected void ddladdSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Chapter();
    }

    private void FillDDL_Chapter()
    {
        //public static DataSet GetAllChaptersBy_Division_Year_Standard_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode)
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        DataSet dschapter = new DataSet();
        DataSet dschapterforsearch = new DataSet();
        dschapter = ProductController.GetAllChaptersByStandardCodeandsubjectcode(ddladdCourse.SelectedValue, ddladdSubject.SelectedValue);
        dschapterforsearch = ProductController.GetAllChaptersByStandardCodeandsubjectcode(ddlStandard.SelectedValue, ddlsubject.SelectedValue);
        BindDDL(ddladdchapter, dschapter, "chapter_name", "chapter_code");
        ddladdchapter.Items.Insert(0, "Select");
        ddladdchapter.SelectedIndex = 0;

        BindDDL(ddlchapter, dschapterforsearch, "chapter_name", "chapter_code");
        ddlchapter.Items.Insert(0, "Select");
        ddlchapter.SelectedIndex = 0;


    }


    private void FillDDL_Concepts()
    {
        try
        {
            Clear_Error_Success_Box();
            DataSet dsClassroomProd = ProductController.GetAllConcepts(ddladdCourse.SelectedValue,ddladdSubject.SelectedValue,ddladdchapter.SelectedValue);
            BindListBox(ddladdconcepts, dsClassroomProd, "Concept_Name", "Record_Id");
            //BindListBox(ddladdconcepts, dsClassroomProd, "Concept_Name", "Record_Id");
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    protected void ddladdchapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Concepts();
    }
    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
 
        {

            BtnSaveEdit.Visible = false;
            if (ddladdCourse.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                //ddladdCourse.Focus();
                return;
            }

            if (ddladdSubject.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0005");
                //ddladdSubject.Focus();
                return;
            }

            if (ddladdchapter.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Chapter");
                //ddladdchapter.Focus();
                return;
            }

            string Concepts = "";
            
            if (ddladdconcepts.Items.Count<=0)
            {
                Show_Error_Success_Box("E", "Select At Least One Concept");
                //ddladdconcepts.Focus();
                return;
            }
          
            
            for (int cnt = 0; cnt <= ddladdconcepts.Items.Count - 1; cnt++)
            {
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];

                if (ddladdconcepts.Items[cnt].Selected == true)
                {
                    Concepts = ddladdconcepts.Items[cnt].Value;


                    //DataSet dsrecordid = ProductController.GetRecord_Id_Concept_Assignment();
                    //string record_id = Convert.ToString(dsrecordid.Tables[0].Rows[0]["Record_Id"]);
                    int ActiveFlag;
                    if (chkActive.Checked == true)
                    {
                        ActiveFlag = 1;
                    }
                    else
                    {
                        ActiveFlag = 0;
                    }
                    string ds1 = ProductController.Insert_Concept_Assignment("", ddladdCourse.SelectedValue, ddladdSubject.SelectedValue, ddladdchapter.SelectedValue, Concepts, UserID, ActiveFlag);

                  

                }

                
                

            }
            if (ddladdconcepts.SelectedIndex==-1)
            {
                Show_Error_Success_Box("E", "Select At Least One Concept");
                //ddladdconcepts.Focus();
                return;
            }
            else
            {
                ControlVisibility("Result");
                Show_Error_Success_Box("S", "0000");
                ddladdCourse.SelectedIndex = 0;
                ddladdSubject.SelectedIndex = 0;
                ddladdchapter.SelectedIndex = 0;
                ddladdconcepts.SelectedIndex = 0;

                chkActive.Checked = true;

                string course = "";
                string subject = "";
                string chapter = "";
                DataSet dsGrid = ProductController.GET_CONCEPT_ASSIGNMENT(course, subject, chapter);
                if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
                {
                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                    DivResultPanel.Visible = true;
                    DivSearchPanel.Visible = false;
                    dlGridDisplay.DataSource = dsGrid;
                    dlGridDisplay.DataBind();
                    dlGridExport.DataSource = dsGrid;
                    dlGridExport.DataBind();
                }
                else
                {
                    Show_Error_Success_Box("E", "0104");
                }
            }

            
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

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "comEdit")
        {
            BtnSaveEdit.Visible = true;
            BtnSaveAdd.Visible = false;

            ControlVisibility("Add");
            string record_id = e.CommandArgument.ToString();
            lblHeader_Add.Text = "Edit Assigned Concepts";
            lblprimarykey.Text = Convert.ToString(e.CommandArgument);

            DataSet dsBatch = ProductController.GET_CONCEPT_ASSIGNMENT_RECORD_ID(record_id);

            if (dsBatch.Tables[0].Rows.Count > 0)
            {

                ddladdCourse.SelectedItem.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Course_Name"]);
                ddladdCourse.Enabled = false;
                ddladdSubject.SelectedItem.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Subject_Name"]);
                ddladdSubject.Enabled = false;
                ddladdchapter.SelectedItem.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Chapter_Name"]);
                ddladdchapter.Enabled = false;
                ddladdconcepts.Items.Clear();
                ddladdconcepts.Items.Add (Convert.ToString(dsBatch.Tables[0].Rows[0]["Concept_Name"]));
                ddladdconcepts.SelectedIndex = 0;
                //ddladdconcepts.SelectedItem.Text = (Convert.ToString(dsBatch.Tables[0].Rows[0]["Concept_Name"]));
                ddladdconcepts.Enabled = false;
               
                


              

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
        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }

        if (ddlsubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject");
            return;
        }
        string course = ddlStandard.SelectedValue;
        string subject = ddlsubject.SelectedValue;
        string chapter = "";
        if (ddlchapter.SelectedItem.Text == "Select")
        {
            chapter = "";
        }
        else
        {
            chapter = ddlchapter.SelectedValue;
        }

        ControlVisibility("Search");

        DataSet dsGrid = ProductController.GET_CONCEPT_ASSIGNMENT(course, subject, chapter);
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
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Subject_Add();
        ddlchapter.Items.Clear();
    }
    protected void ddlsubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Chapter();
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivAddPanel.Visible = false;
        DivSearchPanel.Visible = true;
        DivResultPanel.Visible = false;
        ControlVisibility("Search");


    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        string record_id = Convert.ToString(lblprimarykey.Text);
        

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


        string ds1 = ProductController.UPDATE_CONCEPT_ASSIGNMENT(record_id,  UserID, ActiveFlag);
        if (ds1 == "Record Updated Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            ddladdCourse.SelectedIndex = 0;
            ddladdSubject.SelectedIndex = 0;
            ddladdchapter.SelectedIndex = 0;
            ddladdconcepts.SelectedIndex = 0;
            chkActive.Checked = true;

            string course = "";
            string subject = "";
            string chapter = "";

            DataSet dsGrid = ProductController.GET_CONCEPT_ASSIGNMENT(course, subject, chapter);
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlsubject.Items.Clear();
        ddlchapter.Items.Clear();
        FillDDL_Standard();
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        if (lblHeader_Add.Text == "Assign Concepts")
        { 
        ControlVisibility("Search");
        }

        if (lblHeader_Add.Text == "Edit Assigned Concepts")
        {
            ControlVisibility("Result");
        }

    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Concept Assignment  " + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Concept Assignment</b></TD></TR>");
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
    protected void btnnavigateexcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Concept_Assignment_Upload.aspx");
    }
}