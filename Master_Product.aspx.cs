using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;



public partial class Master_Product : System.Web.UI.Page
{

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");

            FillDDL_Division();
            FillDDL_AcademicYear();
            FillDDL_Category();
            FillDDL_Board();
            FillDDL_Medium();
            FillDDL_ProductCategory();
            FillDDl_Product_Type();

            //ddlproducttype.Items.Add("Select");
            //ddlproducttype.Items.Add("Robomate+");
            //ddlproducttype.Items.Add("Assessment");
            //ddlproducttype.Items.Add("Ebooks");

            //ddlplan.Items.Add("Select");
            //ddlplan.Items.Add("Freemium");
            //ddlplan.Items.Add("Premium");
            //ddlplan.Items.Add("Premium +");
            //ddlplan.Items.Add("Classroom");

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
    #endregion

    #region Methods

    /// <summary>
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    /// 


    private void FillDDl_Product_Type()
    {

        try
        {

            Clear_Error_Success_Box();


            DataSet dsproducttype = ProductController.Get_Product_Type();
            BindDDL(ddlproducttype, dsproducttype, "Product_Type", "Record_Id");
            ddlproducttype.Items.Insert(0, "Select");
            ddlproducttype.SelectedIndex = 0;


            BindDDL(ddlsearchproducttype, dsproducttype, "Product_Type", "Record_Id");
            ddlsearchproducttype.Items.Insert(0, "Select");
            ddlsearchproducttype.SelectedIndex = 0;


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


    private void FillDDl_Product_Plan(string producttype)
    {

        try
        {

            Clear_Error_Success_Box();


            DataSet dsproductsubtype = ProductController.Get_Product_Subtype(producttype);
            BindDDL(ddlproductsubtype, dsproductsubtype, "Product_Subtype", "Record_Id");
            ddlproductsubtype.Items.Insert(0, "Select");
            ddlproductsubtype.SelectedIndex = 0;


            BindDDL(ddlsearchproductsubtype, dsproductsubtype, "Product_Subtype", "Record_Id");
            ddlsearchproductsubtype.Items.Insert(0, "Select");
            ddlsearchproductsubtype.SelectedIndex = 0;


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


    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            Clear_Error_Success_Box();
            DivAddNew.Visible = false;
            BtnAdd.Visible = true;
            DivBeforeadd.Visible = false;
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = false;
            DivResulPanel1.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddNew.Visible = false;
            DivBeforeadd.Visible = false;
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = true;
            DivResulPanel1.Visible = false;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = false;
        }

        else if (Mode == "Result New")
        {
            DivAddNew.Visible = false;
            DivBeforeadd.Visible = false;
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivResulPanel1.Visible = true;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = false;
        }
        else if (Mode == "Add")
        {
            //DivBeforeadd.Visible = false;
            DivAddNew.Visible = false;
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivResulPanel1.Visible = false;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = false;
        }

        else if (Mode == "Add New 1")
        {
            //DivBeforeadd.Visible = false;
            DivAddPanel.Visible = false;
            DivAddNew.Visible = true;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivResulPanel1.Visible = false;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = false;
        }
        else if (Mode == "Add New")
        {
            //DivAddPanel.Visible = true;
            DivAddNew.Visible = false;
            DivBeforeadd.Visible = true;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivResulPanel1.Visible = false;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = false;
        }
        else if (Mode == "Edit")
        {
            DivAddNew.Visible = false;
            DivBeforeadd.Visible = false;
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivResulPanel1.Visible = false;
            DivEditPanel.Visible = true;
            DivEditPanelNew.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }

        else if (Mode == "Edit New")
        {
            DivAddNew.Visible = false;
            DivBeforeadd.Visible = false;
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            DivResulPanel1.Visible = false;
            DivEditPanel.Visible = false;
            DivEditPanelNew.Visible = true;
            BtnShowSearchPanel.Visible = true;
        }
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
    /// Clear Add Panel 
    /// </summary>
    private void ClearAddPanel()
    {
        ddlDivisionAdd.SelectedIndex = 0;
        ddlCourseAdd.Items.Clear();
        ddlAcdYearAdd.SelectedIndex = 0;
        ddlCCName.SelectedIndex = 0;
        ddlBoardName.SelectedIndex = 0;
        ddlMediumName.SelectedIndex = 0;
        ddlAddClassRoomProduct.Items.Clear();
        lstboxaddsubjects.Items.Clear();
        ddlProductCat.SelectedIndex = 0;
        txtAddSKUCode.Text = "";
        txtAddName.Text = "";
        txtAddDescription.Text = "";
        txtAddPrice.Text = "";
        txtAddBucketName.Text = "";
        ddlAddExamMonth.SelectedIndex = 0;
        ddlAddExamYear.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
        chkMarketFlag.Checked = true;
        chkActiveFlag.Checked = true;
        ddladdnewdivision.SelectedIndex = 0;
        ddladdnewcourse.Items.Clear();
        ddladdnewacadyear.SelectedIndex = 0;
        ddladdnewcoursecategory.SelectedIndex = 0;
        ddladdnewboard.SelectedIndex = 0;
        ddladdnewmedium.SelectedIndex = 0;
        lstboxaddnewsubjects.Items.Clear();
        //lstboxaddnewclassroomproducts.Items.Clear();
        //ddladdnewclassroomproducts.SelectedIndex = 0;
        ddladdnewclassroomproducts.Items.Clear();
        ddladdnewproductcategory.SelectedIndex = 0;
        txtaddnewskucode.Text = "";
        txtaddnewproductname.Text = "";
        txtaddnewdescription.Text = "";
        ddladdnewexammonth.SelectedIndex = 0;
        ddladdnewexamyear.SelectedIndex = 0;
        chkboxaddnewisactive.Checked = true;
        add_new_id_date_range_picker_1.Value = "";
        add_new_chkMarketFlag.Checked = true;
    }

    /// <summary>
    /// Clear Edit Panel 
    /// </summary>
    private void ClearEditPanel()
    {
        ddlEditClassRoomProduct.ClearSelection();
        lstboxeditsubjects.ClearSelection();
        ddlEditProductCat.SelectedIndex = 0;
        txtEditSKUCode.Text = "";
        txtEditName.Text = "";
        txtEditDescription.Text = "";
        txtEditPrice.Text = "";
        txtEditBucketName.Text = "";
        ddlEditExamMonth.SelectedIndex = 0;
        ddlEditExamYear.SelectedIndex = 0;
        id_Edit_date_range_picker_1.Value = "";
        chkEditMarketFlag.Checked = true;
        chkEditActiveFlag.Checked = true;
    }

    private void ClearEditPanelNew()
    {

        lstboxeditnewsubjects.ClearSelection();
        //lstboxeditnewclassroomproducts.ClearSelection();
       
        //ddleditnewdivision.SelectedIndex = 0;
        //ddleditnewcourse.SelectedIndex = 0;
        //ddleditnewacadyear.SelectedIndex = 0;
        //ddleditnewcoursecategory.SelectedIndex = 0;
        //ddleditnewboard.SelectedIndex = 0;
        //ddleditnewmedium.SelectedIndex = 0;
        // ddleditnewproductcategory.SelectedIndex = 0;
        txteditnewskucode.Text = "";
        txteditnewproductname.Text = "";
        txteditnewdescription.Text = "";
        txteditnewbucketname.Text = "";
        ddleditnewexammonth.SelectedIndex = 0;
        ddleditnewexamyear.SelectedIndex = 0;
        edit_new_id_Edit_date_range_picker_1.Value = "";
        edit_new_chkEditMarketFlag.Checked = true;
    }


    /// <summary>
    /// Clear Add Panel 
    /// </summary>
    private void ClearSearchPanel()
    {
        ddlDivisionName.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
        ddlsearchproducttype.SelectedIndex = 0;
        ddlsearchproductsubtype.SelectedIndex = 0;
        txtProductcode.Text = "";
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
            BindDDL(ddlDivisionName, dsDivision, "Division_Name", "Division_Code");
            ddlDivisionName.Items.Insert(0, "Select");
            ddlDivisionName.SelectedIndex = 0;

            BindDDL(ddlDivisionAdd, dsDivision, "Division_Name", "Division_Code");
            ddlDivisionAdd.Items.Insert(0, "Select");
            ddlDivisionAdd.SelectedIndex = 0;

            BindDDL(ddlDivisionEdit, dsDivision, "Division_Name", "Division_Code");
            ddlDivisionEdit.Items.Insert(0, "Select");
            ddlDivisionEdit.SelectedIndex = 0;


            BindDDL(ddladdnewdivision, dsDivision, "Division_Name", "Division_Code");
            ddladdnewdivision.Items.Insert(0, "Select");
            ddladdnewdivision.SelectedIndex = 0;

            BindDDL(ddleditnewdivision, dsDivision, "Division_Name", "Division_Code");
            ddleditnewdivision.Items.Insert(0, "Select");
            ddleditnewdivision.SelectedIndex = 0;
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
    /// Fill Medium drop down list
    /// </summary>
    private void FillDDL_Medium()
    {

        try
        {

            Clear_Error_Success_Box();


            DataSet dsMedium = ProductController.Get_Medium_Details("%%");
            BindDDL(ddlMediumName, dsMedium, "MediumName", "Id");
            ddlMediumName.Items.Insert(0, "Select");
            ddlMediumName.SelectedIndex = 0;

            BindDDL(ddlMediumEdit, dsMedium, "MediumName", "Id");
            ddlMediumEdit.Items.Insert(0, "Select");
            ddlMediumEdit.SelectedIndex = 0;


            BindDDL(ddladdnewmedium, dsMedium, "MediumName", "Id");
            ddladdnewmedium.Items.Insert(0, "Select");
            ddladdnewmedium.SelectedIndex = 0;

            BindDDL(ddleditnewmedium, dsMedium, "MediumName", "Id");
            ddleditnewmedium.Items.Insert(0, "Select");
            ddleditnewmedium.SelectedIndex = 0;
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
    /// Fill Board drop down list
    /// </summary>
    private void FillDDL_AcademicYear()
    {
        try
        {
            Clear_Error_Success_Box();

            DataSet dsAcaYear = ProductController.GetAcademiceYear("", "", "2");
            BindDDL(ddlAcademicYear, dsAcaYear, "Description", "Description");
            ddlAcademicYear.Items.Insert(0, "Select");
            ddlAcademicYear.SelectedIndex = 0;

            BindDDL(ddlAcdYearAdd, dsAcaYear, "Description", "Description");
            ddlAcdYearAdd.Items.Insert(0, "Select");
            ddlAcdYearAdd.SelectedIndex = 0;

            BindDDL(ddlAcdYearEdit, dsAcaYear, "Description", "Description");
            ddlAcdYearEdit.Items.Insert(0, "Select");
            ddlAcdYearEdit.SelectedIndex = 0;


            BindDDL(ddladdnewacadyear, dsAcaYear, "Description", "Description");
            ddladdnewacadyear.Items.Insert(0, "Select");
            ddladdnewacadyear.SelectedIndex = 0;

            BindDDL(ddleditnewacadyear, dsAcaYear, "Description", "Description");
            ddleditnewacadyear.Items.Insert(0, "Select");
            ddleditnewacadyear.SelectedIndex = 0;
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
    /// Fill Board drop down list
    /// </summary>
    private void FillDDL_Board()
    {
        try
        {
            Clear_Error_Success_Box();

            DataSet dsMedium = ProductController.GetBoardDetails("", "", "3");
            BindDDL(ddlBoardName, dsMedium, "Long_Description", "Id");
            ddlBoardName.Items.Insert(0, "Select");
            ddlBoardName.SelectedIndex = 0;

            BindDDL(ddlBoardEdit, dsMedium, "Long_Description", "Id");
            ddlBoardEdit.Items.Insert(0, "Select");
            ddlBoardEdit.SelectedIndex = 0;


            BindDDL(ddladdnewboard, dsMedium, "Long_Description", "Id");
            ddladdnewboard.Items.Insert(0, "Select");
            ddladdnewboard.SelectedIndex = 0;

            BindDDL(ddleditnewboard, dsMedium, "Long_Description", "Id");
            ddleditnewboard.Items.Insert(0, "Select");
            ddleditnewboard.SelectedIndex = 0;
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
    /// Fill ClassRoom Products drop down list
    /// </summary>
    private void FillDDL_ClassroomProducts(string DivisionCode)
    {
        try
        {
            Clear_Error_Success_Box();
            DataSet dsClassroomProd = ProductController.GetClassroomProducts(DivisionCode, "1");
            BindListBox(ddlAddClassRoomProduct, dsClassroomProd, "Stream_LDesc", "Stream_Code");
            BindListBox(ddlEditClassRoomProduct, dsClassroomProd, "Stream_LDesc", "Stream_Code");
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

    private void FillDDL_ClassroomProducts_New(string DivisionCode)
    {
        try
        {
            Clear_Error_Success_Box();
            DataSet dsClassroomProd = ProductController.GetClassroomProducts(DivisionCode, "1");
            BindDDL(ddladdnewclassroomproducts, dsClassroomProd, "Stream_LDesc", "Stream_Code");
            ddladdnewclassroomproducts.Items.Insert(0, "Select");
            ddladdnewclassroomproducts.SelectedIndex = 0;

            BindDDL(ddleditnewclassroomproducts, dsClassroomProd, "Stream_LDesc", "Stream_Code");
            ddleditnewclassroomproducts.Items.Insert(0, "Select");
            ddleditnewclassroomproducts.SelectedIndex = 0;
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
    /// Fill Category drop down list
    /// </summary>
    private void FillDDL_Category()
    {
        try
        {
            Clear_Error_Success_Box();

            DataSet dsCourseCat = ProductController.Get_CourseCategory("%%", 1);
            BindDDL(ddlCCName, dsCourseCat, "CourseCategoryName", "CourseCategoryCode");
            ddlCCName.Items.Insert(0, "Select");
            ddlCCName.SelectedIndex = 0;

            BindDDL(ddlCCEdit, dsCourseCat, "CourseCategoryName", "CourseCategoryCode");
            ddlCCEdit.Items.Insert(0, "Select");
            ddlCCEdit.SelectedIndex = 0;

            BindDDL(ddladdnewcoursecategory, dsCourseCat, "CourseCategoryName", "CourseCategoryCode");
            ddladdnewcoursecategory.Items.Insert(0, "Select");
            ddladdnewcoursecategory.SelectedIndex = 0;


            BindDDL(ddleditnewcoursecategory, dsCourseCat, "CourseCategoryName", "CourseCategoryCode");
            ddleditnewcoursecategory.Items.Insert(0, "Select");
            ddleditnewcoursecategory.SelectedIndex = 0;
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
    /// Fill Product Category drop down list
    /// </summary>
    private void FillDDL_ProductCategory()
    {
        try
        {
            Clear_Error_Success_Box();

            DataTable dt = ProductController.GetProductCatTable();
            foreach (DataRow dr in dt.Rows)
            {
                ddlProductCat.Items.Add(dr[1].ToString()); //binding the dropdownlist 
            }
            ddlProductCat.Items.Insert(0, "Select");
            ddlProductCat.SelectedIndex = 0;

            foreach (DataRow dr in dt.Rows)
            {
                ddlEditProductCat.Items.Add(dr[1].ToString()); //binding the dropdownlist 
            }
            ddlEditProductCat.Items.Insert(0, "Select");
            ddlEditProductCat.SelectedIndex = 0;

            foreach (DataRow dr in dt.Rows)
            {
                ddladdnewproductcategory.Items.Add(dr[1].ToString()); //binding the dropdownlist 
            }
            ddladdnewproductcategory.Items.Insert(0, "Select");
            ddladdnewproductcategory.SelectedIndex = 0;


            foreach (DataRow dr in dt.Rows)
            {
                ddleditnewproductcategory.Items.Add(dr[1].ToString()); //binding the dropdownlist 
            }
            ddleditnewproductcategory.Items.Insert(0, "Select");
            ddleditnewproductcategory.SelectedIndex = 0;

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
    /// Fill Course drop down list
    /// </summary>
    private void FillDDL_Course()
    {

        try
        {

            Clear_Error_Success_Box();


            string Div_Code = null;
            Div_Code = ddlDivisionName.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
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
    /// Fill Course Add Panel drop down list
    /// </summary>
    private void FillDDL_CourseAddPanel()
    {

        try
        {

            Clear_Error_Success_Box();


            string Div_Code = null;
            Div_Code = ddlDivisionAdd.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
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
    /// Fill Course Add Panel drop down list
    /// </summary>
    /// 


    private void FillDDL_CourseAddPanelnew()
    {

        try
        {

            Clear_Error_Success_Box();


            //string Div_Code = null;
            //Div_Code = ddladdnewdivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
            BindDDL(ddladdnewcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddladdnewcourse.Items.Insert(0, "Select");
            ddladdnewcourse.SelectedIndex = 0;

            BindDDL(ddleditnewcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddleditnewcourse.Items.Insert(0, "Select");
            ddleditnewcourse.SelectedIndex = 0;
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
    /// Fill Course Add Panel drop down list
    /// </summary>
    private void FillDDL_CourseEditPanel()
    {

        try
        {

            Clear_Error_Success_Box();


            string Div_Code = null;
            Div_Code = ddlDivisionEdit.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
            BindDDL(ddlCourseEdit, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourseEdit.Items.Insert(0, "Select");
            ddlCourseEdit.SelectedIndex = 0;
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
    /// Bind Datalist
    /// </summary>
    private void FillGrid(string DivisionCode1, string CourseCode1, string AcYear,string Product_Type,string Product_SubType)
    {
        try
        {

            
            Clear_Error_Success_Box();
            string CourseCatCode = "", BoardCode = "", MediumCode = "", CourseCode = CourseCode1, AcadYear = AcYear, DivisionCode = DivisionCode1;

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            ControlVisibility("Result");
            HttpCookie cookie1 = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID1 = cookie1.Values["UserID"];
            DataSet dsGrid = ProductController.Get_LMSProduct("", CourseCatCode, BoardCode, MediumCode, CourseCode, AcadYear, DivisionCode, "1", UserID, Product_Type, Product_SubType);
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();

            DataList1.DataSource = dsGrid;
            DataList1.DataBind();

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





    private void FillGridNew(  string DivisionCode1, string CourseCode1, string AcYear, string Product_Type, string Product_SubType)
    {
        try
        {


            Clear_Error_Success_Box();
            string CourseCatCode = "", BoardCode = "", MediumCode = "", CourseCode = CourseCode1, AcadYear = AcYear, DivisionCode = DivisionCode1, productcode = txtProductcode.Text;

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            ControlVisibility("Result New");
            HttpCookie cookie1 = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID1 = cookie1.Values["UserID"];
            DataSet dsGrid = ProductController.Get_LMSProduct(productcode, CourseCatCode, BoardCode, MediumCode, CourseCode, AcadYear, DivisionCode, "3", UserID, Product_Type, Product_SubType);
            dlGridDisplaynew.DataSource = dsGrid;
            dlGridDisplaynew.DataBind();

            DataList3.DataSource = dsGrid;
            DataList3.DataBind();

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalcountnew.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                    }
                    else
                    {
                        lbltotalcountnew.Text = "0";
                    }
                }
                else
                {
                    lbltotalcountnew.Text = "0";
                }
            }
            else
            {
                lbltotalcountnew.Text = "0";
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
    /// Insert data
    /// </summary>
    private void SaveData()
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlproducttype.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Type");
                UpdatePanelMsgBox.Focus();
                return;
            }

            if (ddlproductsubtype.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Sub Type");
                UpdatePanelMsgBox.Focus();
                return;
            }

            if (ddlDivisionAdd.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                UpdatePanelMsgBox.Focus();
                //ddlDivisionAdd.Focus();
                return;
            }
            if (ddlCourseAdd.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                UpdatePanelMsgBox.Focus();
                //ddlCourseAdd.Focus();
                return;
            }
            if (ddlAcdYearAdd.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                UpdatePanelMsgBox.Focus();
                //ddlAcdYearAdd.Focus();
                return;
            }

            if (ddlCCName.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course Category");
                UpdatePanelMsgBox.Focus();
                //ddlCCName.Focus();
                return;
            }
            if (ddlBoardName.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Board");
                UpdatePanelMsgBox.Focus();
                //ddlBoardName.Focus();
                return;
            }
            if (ddlMediumName.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Medium");
                UpdatePanelMsgBox.Focus();
                //ddlMediumName.Focus();
                return;
            }

            string ClassRoomProduct = "";
            for (int cnt = 0; cnt <= ddlAddClassRoomProduct.Items.Count - 1; cnt++)
            {
                if (ddlAddClassRoomProduct.Items[cnt].Selected == true)
                {
                    ClassRoomProduct = ClassRoomProduct + ddlAddClassRoomProduct.Items[cnt].Value + ",";
                }
            }
            if (ClassRoomProduct == "")
            {
                Show_Error_Success_Box("E", "Select at list One Class Room Product");
                UpdatePanelMsgBox.Focus();
                //ddlAddClassRoomProduct.Focus();
                return;
            }

            ClassRoomProduct = ClassRoomProduct.Substring(0, ClassRoomProduct.Length - 1);


            string Subjects = "";
            for (int cnt = 0; cnt <= lstboxaddsubjects.Items.Count - 1; cnt++)
            {
                if (lstboxaddsubjects.Items[cnt].Selected == true)
                {
                    Subjects = Subjects + lstboxaddsubjects.Items[cnt].Value + ",";
                }
            }
            if (Subjects == "")
            {
                Show_Error_Success_Box("E", "Select At Least One Subject");
                UpdatePanelMsgBox.Focus();
                //ddlAddClassRoomProduct.Focus();
                return;
            }

            Subjects = Subjects.Substring(0, Subjects.Length - 1);


            //FillDDL_ClassroomProducts(ddlDivisionName.SelectedValue);
            if (ddlProductCat.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Category");
                UpdatePanelMsgBox.Focus();
                //ddlProductCat.Focus();
                return;
            }

            if (txtAddName.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Product Name");
                UpdatePanelMsgBox.Focus();
                //txtAddName.Focus();
                return;
            }


            if (ddlAddExamMonth.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Month");
                UpdatePanelMsgBox.Focus();
                //ddlAddExamMonth.Focus();
                return;
            }
            if (ddlAddExamYear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Year");
                UpdatePanelMsgBox.Focus();
                //ddlAddExamYear.Focus();
                return;
            }

            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Product Validity");
                UpdatePanelMsgBox.Focus();
               // id_date_range_picker_1.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            int MarketFlag = 0;
            if (chkMarketFlag.Checked == true)
                MarketFlag = 1;
            else
                MarketFlag = 0;

            int ActiveFlag = 0;
            if (chkActiveFlag.Checked == true)
                ActiveFlag = 1;
            else
                ActiveFlag = 0;

            int ResultId = 0;
            //ResultId = ProductController.InsertUpdateLMSProduct("", ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlCourseAdd.SelectedValue, ddlAcdYearAdd.SelectedValue, ddlDivisionAdd.SelectedValue, ClassRoomProduct, ddlProductCat.SelectedValue, txtAddSKUCode.Text, txtAddName.Text, txtAddDescription.Text, txtAddPrice.Text, txtAddBucketName.Text, ddlAddExamMonth.SelectedValue, ddlAddExamYear.SelectedValue, FromDate, ToDate, MarketFlag, "", ActiveFlag, UserID, "1", ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue,Subjects,"");

            //if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
            //{
            //    Show_Error_Success_Box("E", "Duplicate Product Name");
            //    return;
            //}
            //else
            //{

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateProduct(ResultId.Tables[0].Rows[0]["ProductCode"].ToString(), ResultId.Tables[0].Rows[0]["ProductName"].ToString(), ResultId.Tables[0].Rows[0]["DeviceSKU"].ToString(),
            //        ResultId.Tables[0].Rows[0]["ProductDescription"].ToString(), ResultId.Tables[0].Rows[0]["ProductCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductGroupId"].ToString(),
            //        ResultId.Tables[0].Rows[0]["CourseCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductTypeId"].ToString(), ResultId.Tables[0].Rows[0]["ProductSubTypeId"].ToString(),
            //        ResultId.Tables[0].Rows[0]["AcadYear"].ToString(), ResultId.Tables[0].Rows[0]["CourseCode"].ToString(), ResultId.Tables[0].Rows[0]["BoardCode"].ToString(),
            //        ResultId.Tables[0].Rows[0]["MediumCode"].ToString(), ResultId.Tables[0].Rows[0]["DivisionCode"].ToString(), ResultId.Tables[0].Rows[0]["IsDeleted"].ToString(),
            //        ResultId.Tables[0].Rows[0]["IsActive"].ToString(), ResultId.Tables[0].Rows[0]["FromDate"].ToString(), ResultId.Tables[0].Rows[0]["ToDate"].ToString());

            //    string Pkey = ResultId.Tables[0].Rows[0]["ProductCode"].ToString();
            //    ResultId = null;

            //    ResultId = ProductController.InsertUpdateLMSProduct(Pkey, ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlCourseAdd.SelectedValue, ddlAcdYearAdd.SelectedValue, ddlDivisionAdd.SelectedValue, ClassRoomProduct, ddlProductCat.SelectedValue, txtAddSKUCode.Text, txtAddName.Text, txtAddDescription.Text, txtAddPrice.Text, txtAddBucketName.Text, ddlAddExamMonth.SelectedValue, ddlAddExamYear.SelectedValue, FromDate, ToDate, MarketFlag, "", ActiveFlag, UserID, "3", ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue, Subjects, Return_Pkey_CRM);
            //    Show_Error_Success_Box("S", "Record saved Successfully.");


            //    FillGrid(ddlDivisionAdd.SelectedValue, ddlCourseAdd.SelectedValue, ddlAcdYearAdd.SelectedValue,ddlproducttype.SelectedValue,ddlproductsubtype.SelectedValue);
            //}

            ResultId = ProductController.InsertUpdateLMSProduct("", ddlCCName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, ddlCourseAdd.SelectedValue, ddlAcdYearAdd.SelectedValue, ddlDivisionAdd.SelectedValue, ClassRoomProduct, ddlProductCat.SelectedValue, txtAddSKUCode.Text, txtAddName.Text, txtAddDescription.Text, txtAddPrice.Text, txtAddBucketName.Text, ddlAddExamMonth.SelectedValue, ddlAddExamYear.SelectedValue, FromDate, ToDate, MarketFlag, "", ActiveFlag, UserID, "1", ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue, Subjects);

            if (ResultId == -1)
            {
                Show_Error_Success_Box("E", "Duplicate Product Name");
                return;
            }
            else
            {
                Show_Error_Success_Box("S", "Record saved Successfully.");
                FillGrid(ddlDivisionAdd.SelectedValue, ddlCourseAdd.SelectedValue, ddlAcdYearAdd.SelectedValue, ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue);
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
    /// Update data
    /// </summary>
    private void UpdateData()
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlDivisionEdit.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                UpdatePanelMsgBox.Focus();
                //ddlDivisionEdit.Focus();
                return;
            }
            if (ddlCourseEdit.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                UpdatePanelMsgBox.Focus();
                //ddlCourseEdit.Focus();
                return;
            }
            if (ddlAcdYearEdit.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                UpdatePanelMsgBox.Focus();
                //ddlAcdYearEdit.Focus();
                return;
            }

            if (ddlCCEdit.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course Category");
                UpdatePanelMsgBox.Focus();
                //ddlCCEdit.Focus();
                return;
            }
            if (ddlBoardEdit.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Board");
                UpdatePanelMsgBox.Focus();
                //ddlBoardEdit.Focus();
                return;
            }
            if (ddlMediumEdit.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Medium");
                UpdatePanelMsgBox.Focus();
                //ddlMediumName.Focus();
                return;
            }

            string ClassRoomProduct = "";
            for (int cnt = 0; cnt <= ddlEditClassRoomProduct.Items.Count - 1; cnt++)
            {
                if (ddlEditClassRoomProduct.Items[cnt].Selected == true)
                {
                    ClassRoomProduct = ClassRoomProduct + ddlEditClassRoomProduct.Items[cnt].Value + ",";
                }
            }
            if (ClassRoomProduct == "")
            {
                Show_Error_Success_Box("E", "Select at list One Class Room Product");
                UpdatePanelMsgBox.Focus();
                //ddlEditClassRoomProduct.Focus();
                return;
            }

            ClassRoomProduct = ClassRoomProduct.Substring(0, ClassRoomProduct.Length - 1);


            string Subject = "";
            for (int cnt = 0; cnt <= lstboxeditsubjects.Items.Count - 1; cnt++)
            {
                if (lstboxeditsubjects.Items[cnt].Selected == true)
                {
                    Subject = Subject + lstboxeditsubjects.Items[cnt].Value + ",";
                }
            }
            if (Subject == "")
            {
                Show_Error_Success_Box("E", "Select At Least One Subject ");
                UpdatePanelMsgBox.Focus();
                //lstboxeditsubjects.Focus();
                return;
            }

            Subject = Subject.Substring(0, Subject.Length - 1);
            if (ddlEditProductCat.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Category");
                UpdatePanelMsgBox.Focus();
                //ddlEditProductCat.Focus();
                return;
            }

            if (txtEditName.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Product Name");
                UpdatePanelMsgBox.Focus();
                //txtEditName.Focus();
                return;
            }

            if (ddlEditExamMonth.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Month");
                UpdatePanelMsgBox.Focus();
                //ddlEditExamMonth.Focus();
                return;
            }
            if (ddlEditExamYear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Year");
                UpdatePanelMsgBox.Focus();
                //ddlEditExamYear.Focus();
                return;
            }

            if (id_Edit_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Product Validity");
                UpdatePanelMsgBox.Focus();
                //id_Edit_date_range_picker_1.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            string DateRange = "";
            DateRange = id_Edit_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            int MarketFlag = 0;
            if (chkEditMarketFlag.Checked == true)
                MarketFlag = 1;
            else
                MarketFlag = 0;

            int ActiveFlag = 0;
            if (chkEditActiveFlag.Checked == true)
                ActiveFlag = 1;
            else
                ActiveFlag = 0;

            int ResultId = 0;
            //ResultId = ProductController.InsertUpdateLMSProduct(lblPkey.Text, ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlCourseEdit.SelectedValue, ddlAcdYearEdit.SelectedValue, ddlDivisionEdit.SelectedValue, ClassRoomProduct, ddlEditProductCat.SelectedValue, txtEditSKUCode.Text, txtEditName.Text, txtEditDescription.Text, txtEditPrice.Text, txtEditBucketName.Text, ddlEditExamMonth.SelectedValue, ddlEditExamYear.SelectedValue, FromDate, ToDate, MarketFlag, "", ActiveFlag, UserID, "2", "", "",Subject,"");

            //if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
            //{
            //    Show_Error_Success_Box("E", "Duplicate Product Name");
            //    return;
            //}
            //else
            //{

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateProduct(ResultId.Tables[0].Rows[0]["ProductCode"].ToString(), ResultId.Tables[0].Rows[0]["ProductName"].ToString(), ResultId.Tables[0].Rows[0]["DeviceSKU"].ToString(),
            //        ResultId.Tables[0].Rows[0]["ProductDescription"].ToString(), ResultId.Tables[0].Rows[0]["ProductCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductGroupId"].ToString(),
            //        ResultId.Tables[0].Rows[0]["CourseCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductTypeId"].ToString(), ResultId.Tables[0].Rows[0]["ProductSubTypeId"].ToString(),
            //        ResultId.Tables[0].Rows[0]["AcadYear"].ToString(), ResultId.Tables[0].Rows[0]["CourseCode"].ToString(), ResultId.Tables[0].Rows[0]["BoardCode"].ToString(),
            //        ResultId.Tables[0].Rows[0]["MediumCode"].ToString(), ResultId.Tables[0].Rows[0]["DivisionCode"].ToString(), ResultId.Tables[0].Rows[0]["IsDeleted"].ToString(),
            //        ResultId.Tables[0].Rows[0]["IsActive"].ToString(), ResultId.Tables[0].Rows[0]["FromDate"].ToString(), ResultId.Tables[0].Rows[0]["ToDate"].ToString());

            //    string ProductType = ResultId.Tables[0].Rows[0]["ProductTypeId"].ToString();
            //    string ProductSubType = ResultId.Tables[0].Rows[0]["ProductSubTypeId"].ToString();

            //    ResultId = ProductController.InsertUpdateLMSProduct(lblPkey.Text, ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlCourseEdit.SelectedValue, ddlAcdYearEdit.SelectedValue, ddlDivisionEdit.SelectedValue, ClassRoomProduct, ddlEditProductCat.SelectedValue, txtEditSKUCode.Text, txtEditName.Text, txtEditDescription.Text, txtEditPrice.Text, txtEditBucketName.Text, ddlEditExamMonth.SelectedValue, ddlEditExamYear.SelectedValue, FromDate, ToDate, MarketFlag, "", ActiveFlag, UserID, "3", ProductType,ProductSubType, Subject, Return_Pkey_CRM);
            //    Show_Error_Success_Box("S", "Record Update Successfully.");
            //    FillGrid(ddlDivisionEdit.SelectedValue, ddlCourseEdit.SelectedValue, ddlAcdYearEdit.SelectedValue,ddlsearchproducttype.SelectedValue,ddlsearchproductsubtype.SelectedValue);
            //}

            ResultId = ProductController.InsertUpdateLMSProduct(lblPkey.Text, ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue, ddlMediumEdit.SelectedValue, ddlCourseEdit.SelectedValue, ddlAcdYearEdit.SelectedValue, ddlDivisionEdit.SelectedValue, ClassRoomProduct, ddlEditProductCat.SelectedValue, txtEditSKUCode.Text, txtEditName.Text, txtEditDescription.Text, txtEditPrice.Text, txtEditBucketName.Text, ddlEditExamMonth.SelectedValue, ddlEditExamYear.SelectedValue, FromDate, ToDate, MarketFlag, "", ActiveFlag, UserID, "2", "", "", Subject);

            if (ResultId == -1)
            {
                Show_Error_Success_Box("E", "Duplicate Product Name");
                return;
            }
            else
            {
                Show_Error_Success_Box("S", "Record Update Successfully.");
                FillGrid(ddlDivisionEdit.SelectedValue, ddlCourseEdit.SelectedValue, ddlAcdYearEdit.SelectedValue, ddlsearchproducttype.SelectedValue, ddlsearchproductsubtype.SelectedValue);
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



    #region Event's
    protected void BtnAdd_Click(object sender, EventArgs e)
    {

        ControlVisibility("Add New");
        FillDDl_Product_Type();
        ClearAddPanel();
        Clear_Error_Success_Box();
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
        ddlproductsubtype.Items.Clear();
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        ddlproducttype.Items.Clear();
        ddlproductsubtype.Items.Clear();
        ddlproductsubtype.Items.Clear();
        BtnAdd.Visible = true;
    }

    protected void btnEditClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlsearchproducttype.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Product Type");
            ddlsearchproducttype.Focus();
            return;
        }

        if (ddlsearchproductsubtype.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Product Sub Type");
            ddlsearchproductsubtype.Focus();
            return;
        }
        if (ddlDivisionName.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivisionName.Focus();
            return;
        }

        //if (ddlCourse.SelectedItem.ToString() == "Select")
        //{
        //    Show_Error_Success_Box("E", "Select Course");
        //    ddlCourse.Focus();
        //    return;
        //}
        //if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        //{
        //    Show_Error_Success_Box("E", "Select Academic Year");
        //    ddlAcademicYear.Focus();
        //    return;
        //}

        
        if (ddlsearchproducttype.SelectedItem.Text == "Robomate+" && ddlsearchproductsubtype.SelectedItem.Text == "Traditional Classroom")
        {
            FillGrid(ddlDivisionName.SelectedValue, ddlCourse.SelectedValue, ddlAcademicYear.SelectedValue,ddlsearchproducttype.SelectedValue,ddlsearchproductsubtype.SelectedValue);
        }

        if (ddlsearchproducttype.SelectedItem.Text == "Robomate+" && ddlsearchproductsubtype.SelectedItem.Text == "Robomate+ Classroom")
        {
            FillGridNew(ddlDivisionName.SelectedValue, ddlCourse.SelectedValue, ddlAcademicYear.SelectedValue, ddlsearchproducttype.SelectedValue, ddlsearchproductsubtype.SelectedValue);
        }

        if (ddlsearchproducttype.SelectedItem.Text == "Assessment" && ddlsearchproductsubtype.SelectedItem.Text == "Traditional Classroom")
        {
            FillGrid(ddlDivisionName.SelectedValue, ddlCourse.SelectedValue, ddlAcademicYear.SelectedValue,ddlsearchproducttype.SelectedValue,ddlsearchproductsubtype.SelectedValue);
        }

        if (ddlsearchproducttype.SelectedItem.Text == "Assessment" && ddlsearchproductsubtype.SelectedItem.Text == "Robomate+ Classroom")
        {
            FillGridNew(ddlDivisionName.SelectedValue, ddlCourse.SelectedValue, ddlAcademicYear.SelectedValue, ddlsearchproducttype.SelectedValue, ddlsearchproductsubtype.SelectedValue);
        }
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearchPanel();
    }


    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "comEdit")
            {
                
                ClearEditPanel();
                lblPkey.Text = e.CommandArgument.ToString();
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                DataSet ds = ProductController.Get_LMSProduct(lblPkey.Text, "", "", "", "", "", "", "2", UserID, "", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillDDL_ClassroomProducts(ds.Tables[0].Rows[0]["DivisionCode"].ToString());
                    FillDDL_Subject_Add(ds.Tables[0].Rows[0]["CourseCode"].ToString());



                    ddlDivisionEdit.SelectedValue = ds.Tables[0].Rows[0]["DivisionCode"].ToString();
                    ddlDivisionEdit_SelectedIndexChanged(source, e);
                    ddlCourseEdit.SelectedValue = ds.Tables[0].Rows[0]["CourseCode"].ToString();
                    ddlAcdYearEdit.SelectedValue = ds.Tables[0].Rows[0]["AcademicYearCode"].ToString();
                    ddlCCEdit.SelectedValue = ds.Tables[0].Rows[0]["CourseCategoryCode"].ToString();
                    ddlBoardEdit.SelectedValue = ds.Tables[0].Rows[0]["BoardCode"].ToString();
                    ddlMediumEdit.SelectedValue = ds.Tables[0].Rows[0]["MediumCode"].ToString();

                    ddlEditProductCat.SelectedValue = ds.Tables[0].Rows[0]["ProductCatCode"].ToString();
                    txtEditSKUCode.Text = ds.Tables[0].Rows[0]["SKUCode"].ToString();
                    txtEditName.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    txtEditDescription.Text = ds.Tables[0].Rows[0]["ProductDescription"].ToString();
                    txtEditPrice.Text = ds.Tables[0].Rows[0]["Price"].ToString();
                    txtEditBucketName.Text = ds.Tables[0].Rows[0]["BucketName"].ToString();
                    ddlEditExamMonth.SelectedValue = ds.Tables[0].Rows[0]["ExamMonth"].ToString();
                    ddlEditExamYear.SelectedValue = ds.Tables[0].Rows[0]["ExamYear"].ToString();
                    id_Edit_date_range_picker_1.Value = ds.Tables[0].Rows[0]["FromDate"].ToString() + " - " + ds.Tables[0].Rows[0]["ToDate"].ToString();

                    if (ds.Tables[0].Rows[0]["MarketFlag"].ToString() == "0")
                        chkEditMarketFlag.Checked = false;
                    else
                        chkEditMarketFlag.Checked = true;

                    if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "False")
                        chkEditActiveFlag.Checked = false;
                    else
                        chkEditActiveFlag.Checked = true;

                    for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
                    {
                        for (int rcnt = 0; rcnt <= ddlEditClassRoomProduct.Items.Count - 1; rcnt++)
                        {
                            if (ddlEditClassRoomProduct.Items[rcnt].Value == ds.Tables[1].Rows[cnt]["ClassRoomProductCode"].ToString())
                            {
                                ddlEditClassRoomProduct.Items[rcnt].Selected = true;
                                break;
                            }
                        }
                    }



                    if (Convert.ToInt32(ds.Tables[2].Rows[0]["BatchCount"].ToString()) == 0)
                    {
                        btnEditSave.Visible = true;
                    }
                    else
                        btnEditSave.Visible = false;


                    for (int cnt = 0; cnt <= ds.Tables[3].Rows.Count - 1; cnt++)
                    {
                        for (int rcnt = 0; rcnt <= lstboxeditsubjects.Items.Count - 1; rcnt++)
                        {
                            if (lstboxeditsubjects.Items[rcnt].Value == ds.Tables[3].Rows[cnt]["SubjectCode"].ToString())
                            {
                                lstboxeditsubjects.Items[rcnt].Selected = true;
                                break;
                            }
                        }
                    }

                    ControlVisibility("Edit");

                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void btnEditSave_Click(object sender, EventArgs e)
    {
        UpdateData();
    }

    #endregion

    protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_ClassroomProducts(ddlDivisionAdd.SelectedValue);
        FillDDL_CourseAddPanel();
    }
    protected void ddlDivisionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Course();
    }
    protected void ddlDivisionEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_ClassroomProducts(ddlDivisionEdit.SelectedValue);
        FillDDL_CourseEditPanel();
    }



    protected void HLExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Product_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Product</b></TD></TR>");
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


    protected void ddlproductsubtype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlproducttype.SelectedItem.Text == "Robomate+" && ddlproductsubtype.SelectedItem.Text == "Traditional Classroom")
        {
            ControlVisibility("Add");
            ClearAddPanel();
            Clear_Error_Success_Box();


        }
        else if (ddlproducttype.SelectedItem.Text == "Robomate+" && ddlproductsubtype.SelectedItem.Text == "Robomate+ Classroom")
        {
            ControlVisibility("Add New 1");
            ClearAddPanel();
            Clear_Error_Success_Box();
            DataSet dsplan = ProductController.GetAllPlan();
            if (dsplan != null)
            {
                if (dsplan.Tables.Count != 0)
                {

                    dlplanprice.DataSource = dsplan;
                    dlplanprice.DataBind();
                }
            }

        }

        else if (ddlproducttype.SelectedItem.Text == "Assessment" && ddlproductsubtype.SelectedItem.Text == "Traditional Classroom")
        {
            ControlVisibility("Add");
            ClearAddPanel();
            Clear_Error_Success_Box();
        }

        else if (ddlproducttype.SelectedItem.Text == "Assessment" && ddlproductsubtype.SelectedItem.Text == "Robomate+ Classroom")
        {
            ControlVisibility("Add New 1");
            ClearAddPanel();
            Clear_Error_Success_Box();
            DataSet dsplan = ProductController.GetAllPlan();
            if (dsplan != null)
            {
                if (dsplan.Tables.Count != 0)
                {

                    dlplanprice.DataSource = dsplan;
                    dlplanprice.DataBind();
                }
            }

        }
    }
    protected void ddlproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDl_Product_Plan(ddlproducttype.SelectedValue);
    }
    protected void ddladdnewdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_ClassroomProducts_New(ddladdnewdivision.SelectedValue);
        FillDDL_CourseAddPanelnew();
    }
    protected void ddladdnewcoursecategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddladdnewcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddladdnewcourse.SelectedValue);
    }

    private void FillDDL_Subject_Add(string course)
    {





        //string StandardCode = null;
        //StandardCode = ddladdnewcourse.SelectedValue;


        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(course);


        BindListBox(lstboxaddnewsubjects, dsStandard, "Subject_ShortName", "Subject_Code");
        BindListBox(lstboxeditnewsubjects, dsStandard, "Subject_ShortName", "Subject_Code");


        BindListBox(lstboxaddsubjects, dsStandard, "Subject_ShortName", "Subject_Code");
        BindListBox(lstboxeditsubjects, dsStandard, "Subject_ShortName", "Subject_Code");


    }

    //protected void btnaddnewsave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //       Clear_Error_Success_Box();
    //        if (ddlproducttype.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Product Type");
    //            UpdatePanelMsgBox.Focus();
    //            //ddlproducttype.Focus();
                   
    //            return;
    //        }

    //        if (ddlproductsubtype.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Product Sub Type");
    //            UpdatePanelMsgBox.Focus();
    //            //ddlproductsubtype.Focus();
    //            return;
    //        }

    //        if (ddladdnewdivision.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Division");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewdivision.Focus();
    //            return;
    //        }
    //        if (ddladdnewcourse.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Course");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewcourse.Focus();
    //            return;
    //        }
    //        if (ddladdnewacadyear.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Academic Year");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewacadyear.Focus();
    //            return;
    //        }

    //        if (ddladdnewcoursecategory.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Course Category");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewcoursecategory.Focus();
    //            return;
    //        }
    //        if (ddladdnewboard.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Board");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewboard.Focus();
    //            return;
    //        }
    //        if (ddladdnewmedium.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Medium");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewmedium.Focus();
    //            return;
    //        }

    //        string subjects = "";
    //        for (int cnt = 0; cnt <= lstboxaddnewsubjects.Items.Count - 1; cnt++)
    //        {
    //            if (lstboxaddnewsubjects.Items[cnt].Selected == true)
    //            {
    //                subjects = subjects + lstboxaddnewsubjects.Items[cnt].Value + ",";
    //            }
    //        }
    //        if (subjects == "")
    //        {
    //            Show_Error_Success_Box("E", "Select at least One Subject");
    //            UpdatePanelMsgBox.Focus();
    //            //lstboxaddnewsubjects.Focus();
    //            return;
    //        }

    //        subjects = subjects.Substring(0, subjects.Length - 1);



    //        string Classroomprodcts = "";
    //        //for (int cnt = 0; cnt <= lstboxaddnewclassroomproducts.Items.Count - 1; cnt++)
    //        //{
    //        //    if (lstboxaddnewclassroomproducts.Items[cnt].Selected == true)
    //        //    {
    //        //        Classroomprodcts = Classroomprodcts + lstboxaddnewclassroomproducts.Items[cnt].Value + ",";
    //        //    }
    //        //}
    //        if (ddladdnewclassroomproducts.SelectedIndex == 0)
    //        {
    //            Show_Error_Success_Box("E", "Select Class Room Product");
    //            UpdatePanelMsgBox.Focus();
    //            //lstboxaddnewsubjects.Focus();
    //            return;
    //        }

    //        Classroomprodcts = ddladdnewclassroomproducts.SelectedValue;

    //        //FillDDL_ClassroomProducts(ddlDivisionName.SelectedValue);
    //        if (ddladdnewproductcategory.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Product Category");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewproductcategory.Focus();
    //            return;
    //        }

    //        if (txtaddnewproductname.Text.Trim() == "")
    //        {
    //            Show_Error_Success_Box("E", "Enter Product Name");
    //            UpdatePanelMsgBox.Focus();
    //            //txtaddnewproductname.Focus();
    //            return;
    //        }


    //        if (ddladdnewexammonth.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Exam Month");
    //            UpdatePanelMsgBox.Focus();
    //           // ddladdnewexammonth.Focus();
    //            return;
    //        }
    //        if (ddladdnewexamyear.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Exam Year");
    //            UpdatePanelMsgBox.Focus();
    //            //ddladdnewexamyear.Focus();
    //            return;
    //        }


    //        if (add_new_id_date_range_picker_1.Value == "")
    //        {
    //            Show_Error_Success_Box("E", "Select Product Validity");
    //            UpdatePanelMsgBox.Focus();
    //            // id_date_range_picker_1.Focus();
    //            return;
    //        }

    //        //HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //        //string UserID = cookie.Values["UserID"];

    //        string DateRange = "";
    //        DateRange = add_new_id_date_range_picker_1.Value;

    //        string FromDate, ToDate;
    //        FromDate = DateRange.Substring(0, 10);
    //        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

    //        int MarketFlag = 0;
    //        if (add_new_chkMarketFlag.Checked == true)
    //            MarketFlag = 1;
    //        else
    //            MarketFlag = 0;


    //        int ActiveFlag = 0;
    //        if (chkboxaddnewisactive.Checked == true)
    //            ActiveFlag = 1;
    //        else
    //            ActiveFlag = 0;

    //        int SelCntCen = 0;
    //        string Plan = "";
    //        foreach (DataListItem dtlItem in dlplanprice.Items)
    //        {
    //            CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
    //            //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
    //            Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
    //            TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

    //            if (chkDL_Select_Center.Checked == true)
    //            {
    //                if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
    //                {
    //                    Show_Error_Success_Box("E", "Enter Amount");
    //                    UpdatePanelMsgBox.Focus();
    //                    //txtplanamt.Focus();

    //                    return;
    //                }
    //                SelCntCen = SelCntCen + 1;
    //                Plan = Plan + lblplanid.Text + ",";

    //            }
    //        }
    //        Plan = Common.RemoveComma(Plan);
    //        //if (Strings.Right(DivisionCode, 1) == ",")
    //        //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

    //        if (SelCntCen == 0)
    //        {
    //            Show_Error_Success_Box("E", "Select Plan(s)");
    //            UpdatePanelMsgBox.Focus();
    //            //dlplanprice.Focus();
    //            return;
    //        }

    //        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //        string UserID = cookie.Values["UserID"];
    //        DataSet ResultId = ProductController.InsertUpdateLMSProductSubjectPricing("", ddladdnewcoursecategory.SelectedValue, ddladdnewboard.SelectedValue, ddladdnewmedium.SelectedValue, ddladdnewcourse.SelectedValue, ddladdnewacadyear.SelectedValue, ddladdnewdivision.SelectedValue, subjects, ddladdnewproductcategory.SelectedValue, txtaddnewskucode.Text, txtaddnewproductname.Text, txtaddnewdescription.Text, txtaddnewbucketname.Text, ddladdnewexammonth.SelectedValue, ddladdnewexamyear.SelectedValue, ActiveFlag, UserID, "1", ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue, Classroomprodcts, FromDate, ToDate, MarketFlag,"");
    //        string ResultIdfilnal;
    //        if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "Duplicate")
    //        {
    //            Clear_Error_Success_Box();
    //            Show_Error_Success_Box("E", "Product Already Exists For Selected Criteria");
    //            return;

    //        }
    //        else if (ResultId.Tables[0].Rows[0]["Status"].ToString() != "Duplicate")
    //        {
    //            Clear_Error_Success_Box();
    //            foreach (DataListItem dtlItem in dlplanprice.Items)
    //            {
    //                CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
    //                //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
    //                Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
    //                TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

    //                if (chkDL_Select_Center.Checked == true)
    //                {
    //                    if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
    //                    {
    //                        Show_Error_Success_Box("E", "Enter Amount");
    //                        UpdatePanelMsgBox.Focus();
    //                        //txtplanamt.Focus();
    //                        return;
    //                    }

    //                    SelCntCen = SelCntCen + 1;
    //                    string planid = lblplanid.Text;
    //                    string planamount = txtplanamt.Text;
    //                    ResultIdfilnal = ProductController.InsertUpdateLMSProductSubjectPricingPlans(ResultId.Tables[0].Rows[0]["ProductCode"].ToString(), planid, planamount);
    //                    //Plan = Plan + lblplanid.Text + ",";

    //                }
    //            }

    //            ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //            string Return_Pkey_CRM = ms.CreateProduct(ResultId.Tables[0].Rows[0]["ProductCode"].ToString(), ResultId.Tables[0].Rows[0]["ProductName"].ToString(), ResultId.Tables[0].Rows[0]["DeviceSKU"].ToString(),
    //                ResultId.Tables[0].Rows[0]["ProductDescription"].ToString(), ResultId.Tables[0].Rows[0]["ProductCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductGroupId"].ToString(),
    //                ResultId.Tables[0].Rows[0]["CourseCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductTypeId"].ToString(), ResultId.Tables[0].Rows[0]["ProductSubTypeId"].ToString(),
    //                ResultId.Tables[0].Rows[0]["AcadYear"].ToString(), ResultId.Tables[0].Rows[0]["CourseCode"].ToString(), ResultId.Tables[0].Rows[0]["BoardCode"].ToString(),
    //                ResultId.Tables[0].Rows[0]["MediumCode"].ToString(), ResultId.Tables[0].Rows[0]["DivisionCode"].ToString(), ResultId.Tables[0].Rows[0]["IsDeleted"].ToString(),
    //                ResultId.Tables[0].Rows[0]["IsActive"].ToString(), ResultId.Tables[0].Rows[0]["FromDate"].ToString(), ResultId.Tables[0].Rows[0]["ToDate"].ToString());

    //            string Pkey = ResultId.Tables[0].Rows[0]["ProductCode"].ToString();
    //            ResultId = null;

    //            ResultId = ProductController.InsertUpdateLMSProductSubjectPricing(Pkey, ddladdnewcoursecategory.SelectedValue, ddladdnewboard.SelectedValue, ddladdnewmedium.SelectedValue, ddladdnewcourse.SelectedValue, ddladdnewacadyear.SelectedValue, ddladdnewdivision.SelectedValue, subjects, ddladdnewproductcategory.SelectedValue, txtaddnewskucode.Text, txtaddnewproductname.Text, txtaddnewdescription.Text, txtaddnewbucketname.Text, ddladdnewexammonth.SelectedValue, ddladdnewexamyear.SelectedValue, ActiveFlag, UserID,"3", ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue, Classroomprodcts, FromDate, ToDate, MarketFlag, Return_Pkey_CRM);
               
    //            Show_Error_Success_Box("S", "Record Inserted Sucessfully");
    //            FillGridNew(ddladdnewdivision.SelectedValue, ddladdnewcourse.SelectedValue, ddladdnewacadyear.SelectedValue, ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue);
    //        }


    //    }


    //    catch (Exception ex)
    //    {
    //        Msg_Error.Visible = true;
    //        Msg_Success.Visible = false;
    //        lblerror.Text = ex.ToString();
    //        UpdatePanelMsgBox.Update();
    //        return;
    //    }


    //}


    protected void btnaddnewsave_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlproducttype.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Type");
                UpdatePanelMsgBox.Focus();
                //ddlproducttype.Focus();

                return;
            }

            if (ddlproductsubtype.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Sub Type");
                UpdatePanelMsgBox.Focus();
                //ddlproductsubtype.Focus();
                return;
            }

            if (ddladdnewdivision.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                UpdatePanelMsgBox.Focus();
                //ddladdnewdivision.Focus();
                return;
            }
            if (ddladdnewcourse.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                UpdatePanelMsgBox.Focus();
                //ddladdnewcourse.Focus();
                return;
            }
            if (ddladdnewacadyear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                UpdatePanelMsgBox.Focus();
                //ddladdnewacadyear.Focus();
                return;
            }

            if (ddladdnewcoursecategory.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course Category");
                UpdatePanelMsgBox.Focus();
                //ddladdnewcoursecategory.Focus();
                return;
            }
            if (ddladdnewboard.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Board");
                UpdatePanelMsgBox.Focus();
                //ddladdnewboard.Focus();
                return;
            }
            if (ddladdnewmedium.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Medium");
                UpdatePanelMsgBox.Focus();
                //ddladdnewmedium.Focus();
                return;
            }

            string subjects = "";
            for (int cnt = 0; cnt <= lstboxaddnewsubjects.Items.Count - 1; cnt++)
            {
                if (lstboxaddnewsubjects.Items[cnt].Selected == true)
                {
                    subjects = subjects + lstboxaddnewsubjects.Items[cnt].Value + ",";
                }
            }
            if (subjects == "")
            {
                Show_Error_Success_Box("E", "Select at least One Subject");
                UpdatePanelMsgBox.Focus();
                //lstboxaddnewsubjects.Focus();
                return;
            }

            subjects = subjects.Substring(0, subjects.Length - 1);



            string Classroomprodcts = "";
            //for (int cnt = 0; cnt <= lstboxaddnewclassroomproducts.Items.Count - 1; cnt++)
            //{
            //    if (lstboxaddnewclassroomproducts.Items[cnt].Selected == true)
            //    {
            //        Classroomprodcts = Classroomprodcts + lstboxaddnewclassroomproducts.Items[cnt].Value + ",";
            //    }
            //}
            if (ddladdnewclassroomproducts.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Class Room Product");
                UpdatePanelMsgBox.Focus();
                //lstboxaddnewsubjects.Focus();
                return;
            }

            Classroomprodcts = ddladdnewclassroomproducts.SelectedValue;

            //FillDDL_ClassroomProducts(ddlDivisionName.SelectedValue);
            if (ddladdnewproductcategory.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Category");
                UpdatePanelMsgBox.Focus();
                //ddladdnewproductcategory.Focus();
                return;
            }

            if (txtaddnewproductname.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Product Name");
                UpdatePanelMsgBox.Focus();
                //txtaddnewproductname.Focus();
                return;
            }


            if (ddladdnewexammonth.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Month");
                UpdatePanelMsgBox.Focus();
                // ddladdnewexammonth.Focus();
                return;
            }
            if (ddladdnewexamyear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Year");
                UpdatePanelMsgBox.Focus();
                //ddladdnewexamyear.Focus();
                return;
            }


            if (add_new_id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Product Validity");
                UpdatePanelMsgBox.Focus();
                // id_date_range_picker_1.Focus();
                return;
            }

            //HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //string UserID = cookie.Values["UserID"];

            string DateRange = "";
            DateRange = add_new_id_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            int MarketFlag = 0;
            if (add_new_chkMarketFlag.Checked == true)
                MarketFlag = 1;
            else
                MarketFlag = 0;


            int ActiveFlag = 0;
            if (chkboxaddnewisactive.Checked == true)
                ActiveFlag = 1;
            else
                ActiveFlag = 0;

            int SelCntCen = 0;
            string Plan = "";
            foreach (DataListItem dtlItem in dlplanprice.Items)
            {
                CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
                //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
                Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
                TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

                if (chkDL_Select_Center.Checked == true)
                {
                    if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
                    {
                        Show_Error_Success_Box("E", "Enter Amount");
                        UpdatePanelMsgBox.Focus();
                        //txtplanamt.Focus();

                        return;
                    }
                    SelCntCen = SelCntCen + 1;
                    Plan = Plan + lblplanid.Text + ",";

                }
            }
            Plan = Common.RemoveComma(Plan);
            //if (Strings.Right(DivisionCode, 1) == ",")
            //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

            if (SelCntCen == 0)
            {
                Show_Error_Success_Box("E", "Select Plan(s)");
                UpdatePanelMsgBox.Focus();
                //dlplanprice.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string ResultId = ProductController.InsertUpdateLMSProductSubjectPricing("", ddladdnewcoursecategory.SelectedValue, ddladdnewboard.SelectedValue, ddladdnewmedium.SelectedValue, ddladdnewcourse.SelectedValue, ddladdnewacadyear.SelectedValue, ddladdnewdivision.SelectedValue, subjects, ddladdnewproductcategory.SelectedValue, txtaddnewskucode.Text, txtaddnewproductname.Text, txtaddnewdescription.Text, txtaddnewbucketname.Text, ddladdnewexammonth.SelectedValue, ddladdnewexamyear.SelectedValue, ActiveFlag, UserID, "1", ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue, Classroomprodcts, FromDate, ToDate, MarketFlag);
            string ResultIdfilnal;
            if (ResultId == "Duplicate")
            {
                Clear_Error_Success_Box();
                Show_Error_Success_Box("E", "Product Already Exists For Selected Criteria");
                return;

            }
            else if (ResultId != "Duplicate")
            {
                Clear_Error_Success_Box();
                foreach (DataListItem dtlItem in dlplanprice.Items)
                {
                    CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
                    //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
                    Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
                    TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

                    if (chkDL_Select_Center.Checked == true)
                    {
                        if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
                        {
                            Show_Error_Success_Box("E", "Enter Amount");
                            UpdatePanelMsgBox.Focus();
                            //txtplanamt.Focus();
                            return;
                        }

                        SelCntCen = SelCntCen + 1;
                        string planid = lblplanid.Text;
                        string planamount = txtplanamt.Text;
                        ResultIdfilnal = ProductController.InsertUpdateLMSProductSubjectPricingPlans(ResultId, planid, planamount);
                        //Plan = Plan + lblplanid.Text + ",";

                    }
                }


                Show_Error_Success_Box("S", "Record Inserted Sucessfully");
                FillGridNew(ddladdnewdivision.SelectedValue, ddladdnewcourse.SelectedValue, ddladdnewacadyear.SelectedValue, ddlproducttype.SelectedValue, ddlproductsubtype.SelectedValue);
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

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlplanprice.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");

            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
            TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");
            if (chkitemck.Checked == true)
            {
                txtplanamt.Enabled = true;
            }
            else
            {
                txtplanamt.Enabled = false;
                txtplanamt.Text = "";
            }

        }


    }




    protected void chkAttendanceAll_CheckedChangededit(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dleditnewplanprice.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");

            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
            TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");
            if (chkitemck.Checked == true)
            {
                txtplanamt.Enabled = true;
            }
            else
            {
                txtplanamt.Enabled = false;
                txtplanamt.Text = "";
            }

        }


    }




    protected void chkCenter_CheckedChanged(object sender, EventArgs e)
    {

        //CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlplanprice.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
            TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");
            if (chkitemck.Checked == true)
            {
                txtplanamt.Enabled = true;
            }
            else
            {
                txtplanamt.Enabled = false;
                txtplanamt.Text = "";
            }

            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");

        }


    }

    protected void chkCenter_CheckedChangededit(object sender, EventArgs e)
    {

        //CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dleditnewplanprice.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
            TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");
            if (chkitemck.Checked == true)
            {
                txtplanamt.Enabled = true;
            }
            else
            {
                txtplanamt.Enabled = false;
                txtplanamt.Text = "";
            }

            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");

        }


    }
   
    protected void btnaddnewclose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        ddlproducttype.Items.Clear();
        ddlproductsubtype.Items.Clear();
        ddlproductsubtype.Items.Clear();
        BtnAdd.Visible = true;

    }
    protected void ddlsearchproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDl_Product_Plan(ddlsearchproducttype.SelectedValue);
    }
    protected void btnexportnew_Click(object sender, EventArgs e)
    {
        DataList3.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Product_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Product</b></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        DataList3.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

        DataList3.Visible = false;

    }






    protected void dlGridDisplaynew_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ComEdit")
            {
                
                ClearEditPanelNew();
                lblPkey.Text = e.CommandArgument.ToString();
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                DataSet ds = ProductController.Get_LMSProduct(lblPkey.Text, "", "", "", "", "", "", "4", UserID, "", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillDDL_Subject_Add(ds.Tables[0].Rows[0]["CourseCode"].ToString());
                    FillDDL_ClassroomProducts_New(ds.Tables[0].Rows[0]["DivisionCode"].ToString());
                    ddleditnewdivision.SelectedValue = ds.Tables[0].Rows[0]["DivisionCode"].ToString();

                    ddleditnewdivision_SelectedIndexChanged(source, e);
                    ddleditnewcourse.SelectedValue = ds.Tables[0].Rows[0]["CourseCode"].ToString();
                    ddleditnewacadyear.SelectedValue = ds.Tables[0].Rows[0]["AcademicYearCode"].ToString();
                    ddleditnewcoursecategory.SelectedValue = ds.Tables[0].Rows[0]["CourseCategoryCode"].ToString();
                    ddleditnewboard.SelectedValue = ds.Tables[0].Rows[0]["BoardCode"].ToString();
                    ddleditnewmedium.SelectedValue = ds.Tables[0].Rows[0]["MediumCode"].ToString();

                    ddleditnewproductcategory.SelectedValue = ds.Tables[0].Rows[0]["ProductCatCode"].ToString();
                    txteditnewskucode.Text = ds.Tables[0].Rows[0]["SKUCode"].ToString();
                    txteditnewproductname.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    txteditnewdescription.Text = ds.Tables[0].Rows[0]["ProductDescription"].ToString();

                    txteditnewbucketname.Text = ds.Tables[0].Rows[0]["BucketName"].ToString();
                    ddleditnewexammonth.SelectedValue = ds.Tables[0].Rows[0]["ExamMonth"].ToString();
                    ddleditnewexamyear.SelectedValue = ds.Tables[0].Rows[0]["ExamYear"].ToString();
                    ddleditnewclassroomproducts.SelectedValue = ds.Tables[3].Rows[0]["ClassRoomProductCode"].ToString();
                    edit_new_id_Edit_date_range_picker_1.Value = ds.Tables[0].Rows[0]["FromDate"].ToString() + " - " + ds.Tables[0].Rows[0]["ToDate"].ToString();

                    if (ds.Tables[0].Rows[0]["MarketFlag"].ToString() == "0")
                        edit_new_chkEditMarketFlag.Checked = false;
                    else
                        edit_new_chkEditMarketFlag.Checked = true;

                    if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "False")
                        chkboxeditnewisactive.Checked = false;
                    else
                        chkboxeditnewisactive.Checked = true;

                    for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
                    {
                        for (int rcnt = 0; rcnt <= lstboxeditnewsubjects.Items.Count - 1; rcnt++)
                        {
                            if (lstboxeditnewsubjects.Items[rcnt].Value == ds.Tables[1].Rows[cnt]["SubjectCode"].ToString())
                            {
                                lstboxeditnewsubjects.Items[rcnt].Selected = true;
                                break;
                            }
                        }
                    }


                    //for (int cnt = 0; cnt <= ds.Tables[3].Rows.Count - 1; cnt++)
                    //{
                    //    for (int rcnt = 0; rcnt <= lstboxeditnewclassroomproducts.Items.Count - 1; rcnt++)
                    //    {
                    //        if (lstboxeditnewclassroomproducts.Items[rcnt].Value == ds.Tables[3].Rows[cnt]["ClassRoomProductCode"].ToString())
                    //        {
                    //            lstboxeditnewclassroomproducts.Items[rcnt].Selected = true;
                    //            break;
                    //        }
                    //    }
                    //}



                    DataSet dsplan = ProductController.GetAllPlan();
                    if (dsplan != null)
                    {
                        if (dsplan.Tables.Count != 0)
                        {

                            dleditnewplanprice.DataSource = dsplan;
                            dleditnewplanprice.DataBind();
                        }
                    }


                    DataSet dsplanprice = ProductController.GetAllPlanPricing(lblPkey.Text);



                    if (dsplanprice.Tables[0].Rows.Count > 0)
                    {

                        for (int cnt = 0; cnt <= dsplanprice.Tables[0].Rows.Count - 1; cnt++)
                        {

                            foreach (DataListItem dtlItem in dleditnewplanprice.Items)
                            {
                                CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
                                //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
                                Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
                                TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");
                                if (Convert.ToString(lblPkey.Text).Trim() == Convert.ToString(dsplanprice.Tables[0].Rows[cnt]["ProductCode"]).Trim() && lblplanid.Text == Convert.ToString(dsplanprice.Tables[0].Rows[cnt]["PlanId"]).Trim())
                                {
                                    chkDL_Select_Center.Checked = true;
                                    txtplanamt.Text = Convert.ToString(dsplanprice.Tables[0].Rows[cnt]["PlanPrice"]).Trim();
                                    txtplanamt.Enabled = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }

                        }
                    }


                    if (Convert.ToInt32(ds.Tables[2].Rows[0]["BatchCount"].ToString()) == 0)
                    {
                        btneditnewsave.Visible = true;
                    }
                    else
                    {
                        btneditnewsave.Visible = false;
                    }

                    ControlVisibility("Edit New");
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddleditnewdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_ClassroomProducts_New(ddleditnewdivision.SelectedValue);

        FillDDL_CourseAddPanelnew();
    }
    protected void ddleditnewcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddleditnewcourse.SelectedValue);
    }

    protected void btneditnewsave_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();


            if (ddleditnewdivision.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddleditnewdivision.Focus();
                return;
            }
            if (ddleditnewcourse.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                ddleditnewcourse.Focus();
                return;
            }
            if (ddleditnewacadyear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddleditnewacadyear.Focus();
                return;
            }

            if (ddleditnewcoursecategory.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course Category");
                ddleditnewcoursecategory.Focus();
                return;
            }
            if (ddleditnewboard.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Board");
                ddleditnewboard.Focus();
                return;
            }
            if (ddleditnewmedium.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Medium");
                ddleditnewmedium.Focus();
                return;
            }

            string subjects = "";
            for (int cnt = 0; cnt <= lstboxeditnewsubjects.Items.Count - 1; cnt++)
            {
                if (lstboxeditnewsubjects.Items[cnt].Selected == true)
                {
                    subjects = subjects + lstboxeditnewsubjects.Items[cnt].Value + ",";
                }
            }
            if (subjects == "")
            {
                Show_Error_Success_Box("E", "Select at least One Subject");
                lstboxeditnewsubjects.Focus();
                return;
            }

            subjects = subjects.Substring(0, subjects.Length - 1);


            string Classroomproducts = "";
            //for (int cnt = 0; cnt <= lstboxeditnewclassroomproducts.Items.Count - 1; cnt++)
            //{
            //    if (lstboxeditnewclassroomproducts.Items[cnt].Selected == true)
            //    {
            //        Classroomproducts = Classroomproducts + lstboxeditnewclassroomproducts.Items[cnt].Value + ",";
            //    }
            //}
            if (ddleditnewclassroomproducts.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Class Room Product");
                ddleditnewclassroomproducts.Focus();
                return;
            }

            Classroomproducts = ddleditnewclassroomproducts.SelectedValue;

            //FillDDL_ClassroomProducts(ddlDivisionName.SelectedValue);
            if (ddleditnewproductcategory.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Product Category");
                ddleditnewproductcategory.Focus();
                return;
            }

            if (txteditnewproductname.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Product Name");
                txteditnewproductname.Focus();
                return;
            }


            if (ddleditnewexammonth.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Month");
                ddleditnewexammonth.Focus();
                return;
            }
            if (ddleditnewacadyear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Exam Year");
                ddleditnewacadyear.Focus();
                return;
            }

            if (edit_new_id_Edit_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Product Validity");
                UpdatePanelMsgBox.Focus();
                //id_Edit_date_range_picker_1.Focus();
                return;
            }

            //HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //string UserID = cookie.Values["UserID"];

            string DateRange = "";
            DateRange = edit_new_id_Edit_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            int MarketFlag = 0;
            if (edit_new_chkEditMarketFlag.Checked == true)
                MarketFlag = 1;
            else
                MarketFlag = 0;


            int ActiveFlag = 0;
            if (chkboxeditnewisactive.Checked == true)
                ActiveFlag = 1;
            else
                ActiveFlag = 0;

            int SelCntCen = 0;
            string Plan = "";
            foreach (DataListItem dtlItem in dleditnewplanprice.Items)
            {
                CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
                //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
                Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
                TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

                if (chkDL_Select_Center.Checked == true)
                {
                    if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
                    {
                        Show_Error_Success_Box("E", "Enter Amount");
                        txtplanamt.Focus();
                        return;
                    }
                    SelCntCen = SelCntCen + 1;
                    Plan = Plan + lblplanid.Text + ",";

                }
            }
            Plan = Common.RemoveComma(Plan);
            //if (Strings.Right(DivisionCode, 1) == ",")
            //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

            if (SelCntCen == 0)
            {
                Show_Error_Success_Box("E", "Select Plan(s)");
                dleditnewplanprice.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string ResultId = ProductController.InsertUpdateLMSProductSubjectPricing(lblPkey.Text, ddleditnewcoursecategory.SelectedValue, ddleditnewboard.SelectedValue, ddleditnewmedium.SelectedValue, ddleditnewcourse.SelectedValue, ddleditnewacadyear.SelectedValue, ddleditnewdivision.SelectedValue, subjects, ddleditnewproductcategory.SelectedValue, txteditnewskucode.Text, txteditnewproductname.Text, txteditnewdescription.Text, txteditnewbucketname.Text, ddleditnewexammonth.SelectedValue, ddleditnewexamyear.SelectedValue, ActiveFlag, UserID, "2", "", "", Classroomproducts, FromDate, ToDate, MarketFlag);
            string ResultIdfilnal;
            if (ResultId == "Duplicate")
            {
                Clear_Error_Success_Box();
                Show_Error_Success_Box("E", "Product Already Exists For Selected Criteria");
                return;

            }
            else if (ResultId != "Duplicate")
            {
                Clear_Error_Success_Box();
                string resultdelete = ProductController.DeleteLMSProductSubjectPricingPlans(lblPkey.Text);
                if (resultdelete == "Success")
                {
                    foreach (DataListItem dtlItem in dleditnewplanprice.Items)
                    {
                        CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
                        //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
                        Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
                        TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

                        if (chkDL_Select_Center.Checked == true)
                        {
                            if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
                            {
                                Show_Error_Success_Box("E", "Enter Amount");
                                txtplanamt.Focus();
                                return;
                            }

                            SelCntCen = SelCntCen + 1;
                            string planid = lblplanid.Text;
                            string planamount = txtplanamt.Text;

                            ResultIdfilnal = ProductController.InsertUpdateLMSProductSubjectPricingPlans(lblPkey.Text, planid, planamount);
                            //Plan = Plan + lblplanid.Text + ",";

                        }
                    }


                    Show_Error_Success_Box("S", "Record Updated Sucessfully");
                    FillGridNew(ddleditnewdivision.SelectedValue, ddleditnewcourse.SelectedValue, ddleditnewacadyear.SelectedValue, ddlsearchproducttype.SelectedValue, ddlsearchproductsubtype.SelectedValue);
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
    //protected void btneditnewsave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();


    //        if (ddleditnewdivision.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Division");
    //            ddleditnewdivision.Focus();
    //            return;
    //        }
    //        if (ddleditnewcourse.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Course");
    //            ddleditnewcourse.Focus();
    //            return;
    //        }
    //        if (ddleditnewacadyear.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Academic Year");
    //            ddleditnewacadyear.Focus();
    //            return;
    //        }

    //        if (ddleditnewcoursecategory.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Course Category");
    //            ddleditnewcoursecategory.Focus();
    //            return;
    //        }
    //        if (ddleditnewboard.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Board");
    //            ddleditnewboard.Focus();
    //            return;
    //        }
    //        if (ddleditnewmedium.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Medium");
    //            ddleditnewmedium.Focus();
    //            return;
    //        }

    //        string subjects = "";
    //        for (int cnt = 0; cnt <= lstboxeditnewsubjects.Items.Count - 1; cnt++)
    //        {
    //            if (lstboxeditnewsubjects.Items[cnt].Selected == true)
    //            {
    //                subjects = subjects + lstboxeditnewsubjects.Items[cnt].Value + ",";
    //            }
    //        }
    //        if (subjects == "")
    //        {
    //            Show_Error_Success_Box("E", "Select at least One Subject");
    //            lstboxeditnewsubjects.Focus();
    //            return;
    //        }

    //        subjects = subjects.Substring(0, subjects.Length - 1);


    //        string Classroomproducts = "";
    //        //for (int cnt = 0; cnt <= lstboxeditnewclassroomproducts.Items.Count - 1; cnt++)
    //        //{
    //        //    if (lstboxeditnewclassroomproducts.Items[cnt].Selected == true)
    //        //    {
    //        //        Classroomproducts = Classroomproducts + lstboxeditnewclassroomproducts.Items[cnt].Value + ",";
    //        //    }
    //        //}
    //        if (ddleditnewclassroomproducts.SelectedIndex == 0)
    //        {
    //            Show_Error_Success_Box("E", "Select Class Room Product");
    //            ddleditnewclassroomproducts.Focus();
    //            return;
    //        }

    //        Classroomproducts = ddleditnewclassroomproducts.SelectedValue;

    //        //FillDDL_ClassroomProducts(ddlDivisionName.SelectedValue);
    //        if (ddleditnewproductcategory.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Product Category");
    //            ddleditnewproductcategory.Focus();
    //            return;
    //        }

    //        if (txteditnewproductname.Text.Trim() == "")
    //        {
    //            Show_Error_Success_Box("E", "Enter Product Name");
    //            txteditnewproductname.Focus();
    //            return;
    //        }


    //        if (ddleditnewexammonth.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Exam Month");
    //            ddleditnewexammonth.Focus();
    //            return;
    //        }
    //        if (ddleditnewacadyear.SelectedItem.ToString() == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Exam Year");
    //            ddleditnewacadyear.Focus();
    //            return;
    //        }

    //        if (edit_new_id_Edit_date_range_picker_1.Value == "")
    //        {
    //            Show_Error_Success_Box("E", "Select Product Validity");
    //            UpdatePanelMsgBox.Focus();
    //            //id_Edit_date_range_picker_1.Focus();
    //            return;
    //        }

    //        //HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //        //string UserID = cookie.Values["UserID"];

    //        string DateRange = "";
    //        DateRange = edit_new_id_Edit_date_range_picker_1.Value;

    //        string FromDate, ToDate;
    //        FromDate = DateRange.Substring(0, 10);
    //        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

    //        int MarketFlag = 0;
    //        if (edit_new_chkEditMarketFlag.Checked == true)
    //            MarketFlag = 1;
    //        else
    //            MarketFlag = 0;

            
    //        int ActiveFlag = 0;
    //        if (chkboxeditnewisactive.Checked == true)
    //            ActiveFlag = 1;
    //        else
    //            ActiveFlag = 0;

    //        int SelCntCen = 0;
    //        string Plan = "";
    //        foreach (DataListItem dtlItem in dleditnewplanprice.Items)
    //        {
    //            CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
    //            //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
    //            Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
    //            TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

    //            if (chkDL_Select_Center.Checked == true)
    //            {
    //                if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
    //                {
    //                    Show_Error_Success_Box("E", "Enter Amount");
    //                    txtplanamt.Focus();
    //                    return;
    //                }
    //                SelCntCen = SelCntCen + 1;
    //                Plan = Plan + lblplanid.Text + ",";

    //            }
    //        }
    //        Plan = Common.RemoveComma(Plan);
    //        //if (Strings.Right(DivisionCode, 1) == ",")
    //        //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

    //        if (SelCntCen == 0)
    //        {
    //            Show_Error_Success_Box("E", "Select Plan(s)");
    //            dleditnewplanprice.Focus();
    //            return;
    //        }

    //        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //        string UserID = cookie.Values["UserID"];
    //        DataSet ResultId = ProductController.InsertUpdateLMSProductSubjectPricing(lblPkey.Text, ddleditnewcoursecategory.SelectedValue, ddleditnewboard.SelectedValue, ddleditnewmedium.SelectedValue, ddleditnewcourse.SelectedValue, ddleditnewacadyear.SelectedValue, ddleditnewdivision.SelectedValue, subjects, ddleditnewproductcategory.SelectedValue, txteditnewskucode.Text, txteditnewproductname.Text, txteditnewdescription.Text, txteditnewbucketname.Text, ddleditnewexammonth.SelectedValue, ddleditnewexamyear.SelectedValue, ActiveFlag, UserID, "2", "", "",Classroomproducts,FromDate,ToDate,MarketFlag,"");
    //        string ResultIdfilnal;
    //        if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "Duplicate")
    //        {
    //            Clear_Error_Success_Box();
    //            Show_Error_Success_Box("E", "Product Already Exists For Selected Criteria");
    //            return;

    //        }
    //        else if (ResultId.Tables[0].Rows[0]["Status"].ToString() != "Duplicate")
    //        {
    //            Clear_Error_Success_Box();
    //            string resultdelete = ProductController.DeleteLMSProductSubjectPricingPlans(lblPkey.Text);
    //            if (resultdelete == "Success")
    //            {
    //                foreach (DataListItem dtlItem in dleditnewplanprice.Items)
    //                {
    //                    CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
    //                    //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
    //                    Label lblplanid = (Label)dtlItem.FindControl("lblplanid");
    //                    TextBox txtplanamt = (TextBox)dtlItem.FindControl("txtplanamt");

    //                    if (chkDL_Select_Center.Checked == true)
    //                    {
    //                        if (txtplanamt.Enabled == true && txtplanamt.Text.Length == 0)
    //                        {
    //                            Show_Error_Success_Box("E", "Enter Amount");
    //                            txtplanamt.Focus();
    //                            return;
    //                        }

    //                        SelCntCen = SelCntCen + 1;
    //                        string planid = lblplanid.Text;
    //                        string planamount = txtplanamt.Text;

    //                        ResultIdfilnal = ProductController.InsertUpdateLMSProductSubjectPricingPlans(lblPkey.Text, planid, planamount);
    //                        //Plan = Plan + lblplanid.Text + ",";

    //                    }
    //                }

    //                ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //                string Return_Pkey_CRM = ms.CreateProduct(ResultId.Tables[0].Rows[0]["ProductCode"].ToString(), ResultId.Tables[0].Rows[0]["ProductName"].ToString(), ResultId.Tables[0].Rows[0]["DeviceSKU"].ToString(),
    //                    ResultId.Tables[0].Rows[0]["ProductDescription"].ToString(), ResultId.Tables[0].Rows[0]["ProductCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductGroupId"].ToString(),
    //                    ResultId.Tables[0].Rows[0]["CourseCategoryId"].ToString(), ResultId.Tables[0].Rows[0]["ProductTypeId"].ToString(), ResultId.Tables[0].Rows[0]["ProductSubTypeId"].ToString(),
    //                    ResultId.Tables[0].Rows[0]["AcadYear"].ToString(), ResultId.Tables[0].Rows[0]["CourseCode"].ToString(), ResultId.Tables[0].Rows[0]["BoardCode"].ToString(),
    //                    ResultId.Tables[0].Rows[0]["MediumCode"].ToString(), ResultId.Tables[0].Rows[0]["DivisionCode"].ToString(), ResultId.Tables[0].Rows[0]["IsDeleted"].ToString(),
    //                    ResultId.Tables[0].Rows[0]["IsActive"].ToString(), ResultId.Tables[0].Rows[0]["FromDate"].ToString(), ResultId.Tables[0].Rows[0]["ToDate"].ToString());

    //                string ProductType = ResultId.Tables[0].Rows[0]["ProductTypeId"].ToString();
    //                string ProductSubType = ResultId.Tables[0].Rows[0]["ProductSubTypeId"].ToString();

    //                ResultId = ProductController.InsertUpdateLMSProductSubjectPricing(lblPkey.Text, ddleditnewcoursecategory.SelectedValue, ddleditnewboard.SelectedValue, ddleditnewmedium.SelectedValue, ddleditnewcourse.SelectedValue, ddleditnewacadyear.SelectedValue, ddleditnewdivision.SelectedValue, subjects, ddleditnewproductcategory.SelectedValue, txteditnewskucode.Text, txteditnewproductname.Text, txteditnewdescription.Text, txteditnewbucketname.Text, ddleditnewexammonth.SelectedValue, ddleditnewexamyear.SelectedValue, ActiveFlag, UserID, "3", "", "", Classroomproducts, FromDate, ToDate, MarketFlag, Return_Pkey_CRM);
    //                Show_Error_Success_Box("S", "Record Updated Sucessfully");
    //                FillGridNew(ddleditnewdivision.SelectedValue, ddleditnewcourse.SelectedValue, ddleditnewacadyear.SelectedValue, ddlsearchproducttype.SelectedValue, ddlsearchproductsubtype.SelectedValue);
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Msg_Error.Visible = true;
    //        Msg_Success.Visible = false;
    //        lblerror.Text = ex.ToString();
    //        UpdatePanelMsgBox.Update();
    //        return;
    //    }



    //}
    protected void btneditnewclose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void ddlCourseAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddlCourseAdd.SelectedValue);
    }
    protected void ddlCourseEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddlCourseEdit.SelectedValue);
    }
    protected void ddladdnewacadyear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

