using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Data.OleDb;
using System.IO;
using System.Data.Odbc;
using System.Text;


public partial class Master_Faculty_Subject : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            FillDDL_Division();
            FillDDL_AcadYear();
            ControlVisibility("Search");
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
    
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ClearControl(); 
        ControlVisibility("Search");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Add");
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
    }

    protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard_Add();
        FillDDL_Faculty();
    }

    protected void ddlCourseAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Result");

    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {

        if (ValidationData())
        {
            SaveData();
        }

    }
    
    protected void dlFaculty_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {

            Clear_Error_Success_Box();
            ClearControl();
            if (e.CommandName == "comEdit")
            {
                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.GetFaculty_Subject(lblPkey.Text,1);
                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            lblHeaderFacultySubject.Text = "Edit Teacher Subject Assignment";
                            txtColor.Text = ds.Tables[0].Rows[0]["ColorCode"].ToString();
                            txtPaymentRate.Text = ds.Tables[0].Rows[0]["PaymentRate"].ToString();
                            ddlAcademicYearAdd.SelectedValue = ddlAcademicYear.SelectedValue  ;
                            ddlDivisionAdd.SelectedValue = ds.Tables[0].Rows[0]["Division_Code"].ToString();

                            FillDDL_Standard_Add();
                            ddlCourseAdd.SelectedValue = ds.Tables[0].Rows[0]["Standard_Code"].ToString();


                            FillDDL_Subject();
                            ddlSubject.SelectedValue = ds.Tables[0].Rows[0]["Subject_Code"].ToString();

                            FillDDL_Faculty();
                            ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["Partner_Code"].ToString();
                            txtShortName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();

                           
                            DivResultPanel.Visible = false;
                            DivSearchPanel.Visible = false;
                            BtnShowSearchPanel.Visible = true;
                            btnAdd.Visible = true;
                            DivAddFacultySubject.Visible = true;

                        }
                    }
                }
            }
            else if (e.CommandName == "comDelete")
            {

                lbldelCode.Text = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);

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

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {
        try
        {


            Clear_Error_Success_Box();
            //Authorise the selected test
            string PKey = null;
            PKey = lbldelCode.Text;
            string[] Pkeylist = PKey.Split('%');   

            int ResultId = 0;
            ResultId = ProductController.DeleteFaculty_Subject(Pkeylist[0].ToString(), Pkeylist[1].ToString(), Pkeylist[2].ToString(), Pkeylist[3].ToString(), Pkeylist[4].ToString());


            //Close the Add Panel and go to Search Grid
            if (ResultId == 1)
            {
                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "0067");

            }
            else if (ResultId == 2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Teacher chapter assignment already done hence record cannot be deleted";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not deleted";
                UpdatePanelMsgBox.Update();
                return;
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
            btnAdd.Visible = true;
            DivAddFacultySubject.Visible = false;
            btndwntemplete.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            btnAdd.Visible = true;
            DivAddFacultySubject.Visible = false;
            btndwntemplete.Visible = false;


        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            btndwntemplete.Visible = true;
            btnupload.Visible = true;
            btnAdd.Visible = false;
            DivAddFacultySubject.Visible = true;
            btnsaveexcel.Visible = false;

        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            btnAdd.Visible = true;
            DivAddFacultySubject.Visible = true;
            btndwntemplete.Visible = false;

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
    /// Bind search  Datalist
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


            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text ;

            DataSet dsGrid = ProductController.GetFaculty_Subject(DivisionCode+"%"+AcedYear+"%"+StandardCode,2);
                       

            dlFaculty.DataSource = dsGrid;
            dlFaculty.DataBind();

            DataList1.DataSource = dsGrid;
            DataList1.DataBind();

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblStandard_Result.Text = ddlCourse.SelectedItem.ToString();
            lblAced_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
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
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_Standard_Add()
    {

        try
        {
            string Div_Code = null;
            Div_Code = ddlDivisionAdd.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourseAdd, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourseAdd.Items.Insert(0, "Select");
            ddlCourseAdd.SelectedIndex = 0;
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
            StandardCode = ddlCourseAdd.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            ddlSubject.SelectedIndex = 0;
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

            BindDDL(ddlAcademicYearAdd, dsAcadYear, "Description", "Id");
            ddlAcademicYearAdd.Items.Insert(0, "Select");
            ddlAcademicYearAdd.SelectedIndex = 0;
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
    /// Fill Faculty dropdown list
    /// </summary>
    private void FillDDL_Faculty()
    {
        try
        {

            string DivisionCode = "";
            DivisionCode = ddlDivisionAdd.SelectedValue;
            DataSet dsFaculty = ProductController.GetFacultyByDivisionCode(DivisionCode);

            BindDDL(ddlFaculty, dsFaculty, "FacultyName", "Partner_Code");
            ddlFaculty.Items.Insert(0, "Select");
            ddlFaculty.SelectedIndex = 0;
            
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
    /// Clear Controls 
    /// </summary>
    private void ClearControl()
    {
        ddlAcademicYearAdd.SelectedIndex = 0;
        ddlCourseAdd.Items.Clear();
        ddlDivisionAdd.SelectedIndex = 0;
        ddlFaculty.Items.Clear();
        ddlSubject.Items.Clear();
        txtColor.Text = "";
        txtPaymentRate.Text = "";
        lblHeaderFacultySubject.Text = "Add Teacher Subject Assignment";
        lbldelCode.Text = "";
        lblPkey.Text = "";
        txtShortName.Text = ""; 
    }
       
    /// <summary>
    /// Insert and update data
    /// </summary>
    private void SaveData()
    {
        try
        {




            Clear_Error_Success_Box();

            

            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivisionAdd.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYearAdd.SelectedItem.Text ;

            string StandardCode = "";
            StandardCode = ddlCourseAdd.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string Colorcode = null;
            int cnt = txtColor.Text.Length;

            if (cnt == 0)
            {
                Colorcode = "#FFFFFF";
            }
            else
            {
                Colorcode = txtColor.Text.Trim();
            }


            int ResultId = 0;
            
            if (lblPkey.Text == "")
            {

                ResultId = ProductController.InsertFaculty_Subject(DivisionCode.Trim(), YearName.Trim(), StandardCode.Trim(), SubjectCode.Trim(), ddlFaculty.SelectedValue, CreatedBy, Colorcode, txtPaymentRate.Text, txtShortName.Text.Trim());
            }
            else
            {
                ResultId = ProductController.UpdateFaculty_Subject(DivisionCode.Trim(), YearName.Trim(), StandardCode.Trim(), SubjectCode.Trim(), ddlFaculty.SelectedValue, CreatedBy, Colorcode, txtPaymentRate.Text, txtShortName.Text.Trim());

            }
            if (ResultId == 1 && lblPkey.Text=="")
            {
                Show_Error_Success_Box("S", "0000");
               // ClearControl();
                //ControlVisibility("Search");


                //Added by nisarg later
                ddlFaculty.SelectedIndex = 0;
                txtColor.Text = "";
                txtPaymentRate.Text = "";
                lblHeaderFacultySubject.Text = "Add Teacher Subject Assignment";
                lbldelCode.Text = "";
                lblPkey.Text = "";
                txtShortName.Text = ""; 
                

            }
            else if (ResultId == 1 && lblPkey.Text != "")
            {
                Show_Error_Success_Box("S", "0000");
                ClearControl();
                ControlVisibility("Search");
            }

            else if (ResultId == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Subject is already assigned to Teacher!!";
                UpdatePanelMsgBox.Update();
                ddlFaculty.Focus();
                return;

            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Teacher Subject assignment not saved";
                UpdatePanelMsgBox.Update();
                ddlFaculty.Focus();
                return;

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
    /// Controls Validation
    /// </summary>
    private bool ValidationData()
    {
        //Validate if all information is entered correctly

        bool flag = true;

        if (ddlDivisionAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivisionAdd.Focus();
            flag = false;
            return flag;
        }
        if (ddlAcademicYearAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcademicYearAdd.Focus();
            flag = false;
            return flag;
        }

        if (ddlCourseAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlCourseAdd.Focus();
            flag = false;
            return flag;
        }

        if (ddlSubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject.Focus();
            flag = false;
            return flag;
        }

        if (ddlFaculty.SelectedIndex == 0)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Select Teacher";
            UpdatePanelMsgBox.Update();
            ddlFaculty.Focus();
            flag = false;
            return flag;
        }
        
        if (txtShortName.Text == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter Teacher Short Name";
            UpdatePanelMsgBox.Update();
            txtShortName.Focus();
            flag = false;
            return flag; 
        }

        
        return flag;

    }

    #endregion
    //added by sujeer...08 feb 2017

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Faculty_Subject_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Faculty_Subject</b></TD></TR>");
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

    protected void btndwntemplete_Click(object sender, EventArgs e)
    {

        if (ddlDivisionAdd.SelectedIndex == 0)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Select Division Name";
            UpdatePanelMsgBox.Update();
            txtShortName.Focus();
            return;
        }
        if (ddlAcademicYearAdd.SelectedIndex ==0)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Select Acad Year";
            UpdatePanelMsgBox.Update();
            txtShortName.Focus();
            return;
        }
        if (ddlCourseAdd.SelectedIndex == 0)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Select Course";
            UpdatePanelMsgBox.Update();
            txtShortName.Focus();
            return;
        }


        DataSet dssubjects = new DataSet();
        dssubjects = ProductController.GetAllSubjectsforfaculty(ddlDivisionAdd.SelectedValue, ddlAcademicYearAdd.SelectedValue,ddlCourseAdd.SelectedValue);

        if (dssubjects.Tables[0].Rows.Count > 0 && dssubjects != null)
        {
           //New_UploadGrid.Visible = true;

            DataTable dt = new DataTable();
            dt = dssubjects.Tables[0];


            Response.ContentType = "Application/x-msexcel";
            Response.AddHeader("content-disposition", "attachment;filename=Concepts " + " " + DateTime.Now + ".csv");
            Response.Write(ExportToCSVFile(dt));
            Response.End();


        }

        else
        {
            Show_Error_Success_Box("E", "No Subjects Found For Selected Course");
        }




    }

    private string ExportToCSVFile(DataTable dtTable)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable.Columns.Count != 0)
        {
            foreach (DataColumn col in dtTable.Columns)
            {
                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dtTable.Rows)
            {
                foreach (DataColumn column in dtTable.Columns)
                {
                    sbldr.Append(row[column].ToString() + ',');
                }
                sbldr.Append("\r\n");
            }
        }
        return sbldr.ToString();
    }

    protected void Checkexcel()
    {

        if (!string.IsNullOrEmpty(uploadfile.FileName))
        {
            //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
            string FullName = Server.MapPath("~/Faculty_sub_upload") + "\\" + Path.GetFileName(uploadfile.FileName);
            lblfilepath.Text = FullName;
            lblfilename.Text = Path.GetFileName(uploadfile.FileName);
            string strFileType = Path.GetExtension(uploadfile.FileName).ToLower();
            if (strFileType != ".csv")
            {
                Show_Error_Success_Box("E", "Kindly Select Excel File With .CSV Extension");
                return;
            }

            else
            {
                try
                {
                    if (File.Exists(lblfilepath.Text))
                    {
                        Show_Error_Success_Box("E", "File Name Already Exists");
                        return;

                    }
                    uploadfile.SaveAs(FullName);

                    DataTable dtRaw = new DataTable();



                    //create object for CSVReader and pass the stream
                    ////CSVReader reader = new CSVReader(FFLExcel.PostedFile.InputStream);
                    FileStream fileStream = new FileStream(FullName, FileMode.Open);
                    CSVReader reader = new CSVReader(fileStream);
                    //get the header
                    string[] headers = reader.GetCSVLine();

                    //add headers
                    foreach (string strHeader in headers)
                    {
                        dtRaw.Columns.Add(strHeader);

                    }
                    DataRow NewRow = null;
                    int CurRowNo = 0;




                    string[] data = null;


                    data = reader.GetCSVLine();
                    //Read first line
                    CurRowNo = 1;
                    while (data != null)
                    {
                        dtRaw.Rows.Add(data);

                    NextCSVLine:


                        data = reader.GetCSVLine();
                        //Read next line
                        CurRowNo = CurRowNo + 1;
                    }
                    datalist_NewUploads1.DataSource = dtRaw;
                    datalist_NewUploads1.DataBind();

                    //New_UploadGrid.Visible = true;
                    datalist_NewUploads1.Visible = true;
                    Divbtnimport.Visible = true;
                    Msg_Error.Visible = false;
                   // DivNew_Upload.Visible = false;
                    //UpdatePanel2.Update();
                }
                catch (Exception e)
                {
                    Show_Error_Success_Box("E", e.ToString());
                }

            }

        }
        else
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Please Select File...!";
            Divbtnimport.Visible = false;
            return;
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
      

        Checkexcel();
        BtnSaveAdd.Visible = false;
        btnsaveexcel.Visible = true;
        btnupload.Visible = false;
        //btnsaveexcel.Visible = false;
        //Btnimport.Visible = true;
        //Btnimport.Enabled = true;
        //BtndwnldTemplate.Visible = false;

        //lblStandard_Result.Text = ddluploadcourse.SelectedItem.Text;
        //if (New_UploadGrid.Visible == true)
        //{
        //    btnsaveexcel.Visible = false;
        //    Btnimport.Visible = true;
        //    Btnimport.Enabled = true;
        //    BtndwnldTemplate.Visible = false;
        //}
    }
    protected void btnsaveexcel_Click(object sender, EventArgs e)
    {
        //Btnimport.Visible = false;
        DataSet dsimportcode = ProductController.GetRecord_Id_Log();
        string importcode = Convert.ToString(dsimportcode.Tables[0].Rows[0]["Record_Id"]);


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbldivision = (Label)item.FindControl("lbldivision");
                Label lblacad = (Label)item.FindControl("lblacad");
                Label lblcourse = (Label)item.FindControl("lblcourse");
                Label lblsubject = (Label)item.FindControl("lblsubject");
                Label lblsubcode = (Label)item.FindControl("lblsubcode");
                Label lblteachcode = (Label)item.FindControl("lblteachcode");
                Label lblcolor = (Label)item.FindControl("lblcolor");
                Label lblpayment = (Label)item.FindControl("lblpayment");
                Label lblshtname = (Label)item.FindControl("lblshtname");
                Label lblstatuss = (Label)item.FindControl("labelSTATUS");


                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                // Here you define what lbl should show the multiplication result

                if (lbldivision.Text == "" || lblacad.Text == "" || lblcourse.Text == "" || lblsubject.Text == "" || lblsubcode.Text == "" || lblteachcode.Text == "" || lblshtname.Text =="")
                {
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "[M514_Faculty_Subject]", lblfilename.Text, UserID, "Error");
                    lblstatuss.Text = "Error Mandatoty Fileds Are Blank";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblstatuss.Text = "Success";

                }

                try
                {
                    if (lblstatuss.Text == "Success")
                    {
                        string ds1 = ProductController.InsertFaculty_Subjectcsv(lbldivision.Text, lblacad.Text, lblcourse.Text, lblsubcode.Text, lblteachcode.Text, UserID, "#FFFFFF", lblpayment.Text, lblshtname.Text, importcode,lblfilename.Text);
                        //string ds1 = ProductController.Insert_Concepts_Import(ddluploadcourse.SelectedValue, lblsubjectname.Text, lblsubjectcode.Text, lblconceptname.Text.ToUpper(), lblconceptdescription.Text, lblconceptgenericname.Text, importcode, UserID, 1, lblfilename.Text, lblmoduleav.Text);

                        if (ds1 == "Record Inserted Sucessfully")
                        {
                            lblstatuss.Text = "Success";
                            lblstatuss.Visible = true;
                            lblSuccess.Text = "Records Saved Successfully";
                            lblstatuss.ForeColor = System.Drawing.Color.Green;                           
                        }

                        else if (ds1 == "Subject Does Not Exists")
                        {
                            
                            lblstatuss.Text = "Subject Does Not Exists";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ds1 == "Concept Already Exists")
                        {
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "C091_Concepts", lblfilename.Text, UserID, "Error");
                            lblstatuss.Text = "Concept Already Existsss";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }




                    }
                    else
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "Recoed not Saved!";
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }
        }
        //Btnimport.Visible = false;

        btnsaveexcel.Visible = false;
    }
}