using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;


public partial class Faculty_lecture_count_SSC : System.Web.UI.Page
{
   
    #region Events


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

    //private void Page_Validation()
    //{
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    string UserID = cookie.Values["UserID"];
    //    string UserName = cookie.Values["UserName"];

    //    string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
    //    System.IO.FileInfo Info = new System.IO.FileInfo(Path);
    //    string pageName = Info.Name;

    //    int ResultId = 0;

    //    ResultId = ProductController.Chk_Page_Validation(pageName, UserID, "DB00");

    //    if (ResultId >= 1)
    //    {
    //        //Allow
    //    }
    //    else
    //    {
    //        Response.Redirect("~/Homepage.aspx", false);
    //    }

    //}

    private void FillDDL_Center()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_ForUserCreation(4, ddlDivision.SelectedValue);
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

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Center();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlLMSProduct.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlCourse.Items.Clear();
    }


    protected void btnTopSearch_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Search");
    }


    protected void ddlLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Faculty();

    }



    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
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
            btnTopSearch.Visible = false;
            BtnAdd.Visible = false;


        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;


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
                ddlCourse.Focus();
                return;
            }
            if (ddlCenter.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Center Name");
                return;
            }

            if(Daterange.Value =="")
            {
                Show_Error_Success_Box("E", "Select Date");
                return;
            }

            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text;

            string center = "";
            for (int cnt = 0; cnt <= ddlCenter.Items.Count - 1; cnt++)
            {
                if (ddlCenter.Items[cnt].Selected == true)
                {
                    center = center + ddlCenter.Items[cnt].Value + ",";
                }
            }


         string DateRange = "";
        DateRange = Daterange.Value;
        string fromdate = "";
        string todate = "";
        fromdate = DateRange.Substring(0, 10);
        todate = DateRange.Substring(13, 10);
     


            string LMSProduct = "";
            for (int cnt = 0; cnt <= ddlLMSProduct.Items.Count - 1; cnt++)
            {
                if (ddlLMSProduct.Items[cnt].Selected == true)
                {
                    LMSProduct = LMSProduct + ddlLMSProduct.Items[cnt].Value + ",";
                }
            }

           

            string facultycode = "";
            facultycode = ddlfaculty.SelectedValue;

            DataSet dsGrid = ProductController.Get_leturecountmonthly(DivisionCode, AcedYear, StandardCode, LMSProduct, facultycode, fromdate, todate,center);


            dlfaculty.DataSource = dsGrid;
            dlfaculty.DataBind();

            DataList1.DataSource = dsGrid;
            DataList1.DataBind();



            lblDivision.Text = ddlDivision.SelectedItem.ToString();
            lblCourse.Text = ddlCourse.SelectedItem.ToString();
            lblAcademicYear.Text = ddlAcademicYear.SelectedItem.ToString();
            lblLMSProduct.Text = ddlLMSProduct.SelectedItem.ToString();
            lblfaculty.Text = ddlfaculty.SelectedItem.ToString();
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
            BindListBox(ddlLMSProduct, dsAllLMSProduct, "ProductName", "ProductCode");
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


    private void FillDDL_Faculty()
    {

        try
        {
            //string Div_Code = null;
            //Div_Code = ddlDivision.SelectedValue;
            string acadyear = null;
            acadyear = ddlAcademicYear.SelectedValue;
            string coursecode = null;
            coursecode = ddlCourse.SelectedValue;
            string lmsproductcode = null;
            lmsproductcode = ddlLMSProduct.SelectedValue;


            DataSet dsfaculty = ProductController.GetAllActive_Allfaculty(coursecode, acadyear, lmsproductcode);
            BindDDL(ddlfaculty, dsfaculty, "Facultyname", "Partner_Code");
            ddlfaculty.Items.Insert(0, "Select");
            ddlfaculty.SelectedIndex = 0;
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
    /// Clear Controls 
    /// </summary>
    private void ClearControl()
    {

    }





    #endregion
    protected void HLExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Faculty Lecture Count" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='10'>Faculty Lecture Count</b></TD></TR><TR><TD Colspan='2'><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD><b>Course : " + ddlCourse.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Faculty Name : " + ddlfaculty.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Acad Year: " + ddlAcademicYear.SelectedItem.ToString() + "<b></TD></TR><TR></TR>");
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