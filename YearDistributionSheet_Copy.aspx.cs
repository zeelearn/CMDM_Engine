﻿using Microsoft.VisualBasic;
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


public partial class YearDistributionSheet_Copy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
        }

    }
    private void FillDDL_AcadYear()
    {
        try
        {

            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            ddlAcademicYear.Items.Insert(0, "Select");
            ddlAcademicYear.SelectedIndex = 0;

            //fill copy to acad_year
            BindDDL(ddlcopytoacadyear, dsAcadYear, "Description", "Id");
            ddlcopytoacadyear.Items.Insert(0, "Select");
            ddlcopytoacadyear.SelectedIndex = 0;
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

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();
        FillDDL_SchedulingHorizon();
    }
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



    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
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


    private void FillDDL_LMSProduct_Copy_To()
    {

        try
        {
            string AcademicYear = null;
            AcademicYear = ddlcopytoacadyear.SelectedItem.Text;


            string Course = null;
            Course = ddlCourse.SelectedValue;

            DataSet dsAllLMSProduct = ProductController.GetLMSProductByCourse_AcadYear(Course, AcademicYear);
            BindDDL(ddlcopytolmsproduct, dsAllLMSProduct, "ProductName", "ProductCode");
            ddlcopytolmsproduct.Items.Insert(0, "Select");
            ddlcopytolmsproduct.SelectedIndex = 0;
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


    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
    }
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


    protected void ddlLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

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
        //dlGridChapter.DataSource = null;
        //dlGridChapter.DataBind();

        ddlBatch.Items.Clear();

        divBottom.Visible = true;
        //BtnSaveAdd.Visible = false;
        BtnCloseAdd.Visible = true;
    }
    protected void ddlCenter_Add_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCenter_Add.SelectedItem.Value != "Select")
        {
            FillDDL_Batch();
        }
        else
        {
            BtnSaveAdd.Visible = false;
            
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




    protected void ddlcopytoacadyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct_Copy_To();
        
    }
    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlCenter_Add.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCenter_Add.Focus();
                return;

            }

            if (ddlcopytoacadyear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Copy To Acad Year");
                ddlcopytoacadyear.Focus();
                return;
            }


            if (ddlcopytolmsproduct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Copy To LMS Product");
                ddlcopytolmsproduct.Focus();
                return;
            }



            if (ddlAcademicYear.SelectedValue == ddlcopytoacadyear.SelectedValue)
            {
                Show_Error_Success_Box("E", "Acad Year and copy to Acad Year cannot be same");
                ddlcopytoacadyear.Focus();
                return;
            }

            if (ddlLMSProduct.SelectedValue == ddlcopytolmsproduct.SelectedValue)
            {
                Show_Error_Success_Box("E", "LMS Product and copy to LMS Product cannot be same");
                ddlcopytolmsproduct.Focus();
                return;
            }


            string DivisionCode = ddlDivision.SelectedValue;
            string Acad_Year_Old = ddlAcademicYear.SelectedItem.Text;
            string Standard_Code = ddlCourse.SelectedValue;
            string LMSProductCode_Old = ddlLMSProduct.SelectedValue;
            string SchedulHorizonTypeCode = ddlSchHorizon.SelectedValue;
            string CenterCode = ddlCenter_Add.SelectedValue;
            string SubjectCode = ddlSubjectName.SelectedValue;
            string BatchCode = "0";
            string Acad_Year_New = ddlcopytoacadyear.SelectedItem.Text;
            string LMSProductCode_New = ddlcopytolmsproduct.SelectedValue;


            BatchCode = ddlBatch.SelectedValue;
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int ResultId = ProductController.Copy_YearDistribution(DivisionCode, Acad_Year_Old, Acad_Year_New, Standard_Code, SchedulHorizonTypeCode, SubjectCode, CenterCode, LMSProductCode_Old, LMSProductCode_New, BatchCode, UserID);
            if (ResultId == 1)
            {
                Show_Error_Success_Box("S", "Records Copied Sucessfully");
            }
            else
            {
                Show_Error_Success_Box("E", "Unknown Error Contact Administrator");
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        FillGrid();
        
        divBottom.Visible = false;
    }
}