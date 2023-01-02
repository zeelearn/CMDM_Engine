using Microsoft.VisualBasic;
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
using System.Net.Http;
using System.Net.Http.Headers;
using LMSIntegration;
public partial class Cloud_Post_Teacher_Teaching_Details_LMS : System.Web.UI.Page
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


    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;

        }

        //Clear_Error_Success_Box();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDL_Subject();
        FillDDL_LMSProduct();



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



    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();

    }

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


        BindDDL(ddlcenter, dsCentre, "Center_Name", "Center_Code");
        ddlcenter.Items.Insert(0, "Select");
        ddlcenter.SelectedIndex = 0;

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

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
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
    protected void BtnSearch_Click(object sender, EventArgs e)
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

        if (ddlcenter.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center");
            ddlSubjectName.Focus();
            return;
        }

        ControlVisibility("Result");
        DataSet dsGrid = ProductController.GET_TEACHER_TEACHING_DETAILS_LMS(1,ddlcenter.SelectedValue,ddlAcademicYear.SelectedValue,ddlCourse.SelectedValue,ddlSubjectName.SelectedValue,ddlLMSProduct.SelectedValue);
        if (dsGrid != null)
        {
            if (dsGrid.Tables[0].Rows.Count > 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();



                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
            else
            {
                Show_Error_Success_Box("E", "No Records Found For Selected Criteria");
                ControlVisibility("Search");
            }
        }
        else
        {
            lbltotalcount.Text = "0";
        }


    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlcenter.Items.Clear();
        ddlCourse.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlSubjectName.Items.Clear();
        Clear_Error_Success_Box();
    }
    protected void btnpostlms_Click(object sender, EventArgs e)
    {
        foreach(DataListItem dlitem in dlGridDisplay.Items)
        {
            Label Partner_Code = (Label)dlitem.FindControl("lblPartnerCode");
            Label Center_Code = (Label)dlitem.FindControl("lblCenterCode");
            Label LMSProductCode = (Label)dlitem.FindControl("lblLMSProductCode");
            Label SubjectCode = (Label)dlitem.FindControl("lblSubjectCode");
            Send_Details_LMS(Partner_Code.Text, Center_Code.Text, LMSProductCode.Text, SubjectCode.Text, ddlAcademicYear.SelectedValue);

            
        }
        BtnSearch_Click(sender, e);
    }


    private void Send_Details_LMS(string Partner_Code, string Center_Code, string Product_Code, string Subject_Code, string Acad_Year)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        DateTime datetime = DateTime.Now;

        try
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DBConnection.connStringLMS);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var teacherteachingdetails = new teacherteachingdetails();
            teacherteachingdetails.TeacherCode = Partner_Code;
            teacherteachingdetails.CenterCode = Center_Code;
            teacherteachingdetails.ProductCode = Product_Code;
            teacherteachingdetails.SubjectCode = Subject_Code;
            teacherteachingdetails.CreatedOn = datetime;
            teacherteachingdetails.CreatedBy = UserID;
            teacherteachingdetails.ModifiedOn = datetime;
            teacherteachingdetails.ModifiedBy = UserID;
            teacherteachingdetails.IsActive = true;
            teacherteachingdetails.IsDeleted = false;

            var response = client.PostAsJsonAsync("teacher/addUpdTeacherTeachingDetails", teacherteachingdetails).Result;

            Response_Status_Code = response.StatusCode.ToString();
            Response_Return_Phrase = response.ReasonPhrase;

            if (response.StatusCode.ToString() == "OK")
            {


                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, 1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);


            }
            else
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }




        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());

            DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, Partner_Code + '%' + Center_Code + '%' + Product_Code + '%' + Subject_Code + '%' + Acad_Year,
            Response_Status_Code, Response_Return_Phrase, UserID);
        }
    }
    class teacherteachingdetails
    {
        public string TeacherCode { get; set; }
        public string CenterCode { get; set; }
        public string ProductCode { get; set; }
        public string SubjectCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }


    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
}