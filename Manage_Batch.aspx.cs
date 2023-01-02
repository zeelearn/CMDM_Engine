using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using ShoppingCart.BL;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;



public partial class Manage_Batch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            
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

     protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear.Focus();
            return;
        }

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = "";
        int StdCnt = 0;
        int StdSelCnt = 0;
        for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
        {
            if (ddlStandard.Items[StdCnt].Selected == true)
            {
                StdSelCnt = StdSelCnt + 1;
            }
        }

        if (StdSelCnt == 0)
        {
            //When all is selected
            for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            {
                StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
            }

            StandardCode = Common.RemoveComma(StandardCode);
            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
        }
        else
        {
            for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            {
                if (ddlStandard.Items[StdCnt].Selected == true)
                {
                    StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
                }
            }
            StandardCode = Common.RemoveComma(StandardCode);
            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
        }

        string CentreCode = "";
        int CentreCnt = 0;
        int CentreSelCnt = 0;
        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
        {
            if (ddlCentre.Items[CentreCnt].Selected == true)
            {
                CentreSelCnt = CentreSelCnt + 1;
            }
        }

        if (CentreSelCnt == 0)
        {
            //all are selected
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
            }
            CentreCode = Common.RemoveComma(CentreCode);
            //CentreCode = CentreCode.LastIndexOf(",") == CentreCode.Length - 1 ? CentreCode.Substring(0, CentreCode.Length - 1) : CentreCode;
            //if (Strings.Right(CentreCode, 1) == ",")
            //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        }
        else
        {
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                if (ddlCentre.Items[CentreCnt].Selected == true)
                {
                    CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
                }
            }
            CentreCode = Common.RemoveComma(CentreCode);
            //if (Strings.Right(CentreCode, 1) == ",")
            //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        }

        string BatchName = null;
        if (string.IsNullOrEmpty(txtBatchName.Text.Trim()))
        {
            BatchName = "%";
        }
        else
        {
            BatchName = "%" + txtBatchName.Text.Trim();
        }

        DataSet dsGrid = ProductController.GetBatchBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, CentreCode, BatchName);
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
        lbltotalcount.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }

      private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivEditPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivEditPanel.Visible = false;
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivEditPanel.Visible = false;
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }
        else if (Mode == "Edit")
        {
            DivEditPanel.Visible = true;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Replicate" || e.CommandName == "Edit")
        {
            ControlVisibility("Edit");
            lblPKey_Edit.Text =Convert.ToString(e.CommandArgument);
            FillBatchDetails(lblPKey_Edit.Text, e.CommandName);
        }
    }

    private void FillBatchDetails(string PKey, string CommandName)
    {
        
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet dsBatch = ProductController.GetBatchBY_PKey(PKey, UserID);

        if (dsBatch.Tables[0].Rows.Count > 0)
        {

            lblDivision_Edit.Text = lblDivision_Result.Text;
            lblAcadYear_Edit.Text = lblAcadYear_Result.Text;
            lblStandard_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Standard_Name"]);
            lblCentre_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Centre_Name"]);
            lblBatchName_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["BatchName"]);
            txtBatchShortName_Edit.Text =
                
                
            Convert.ToString(dsBatch.Tables[0].Rows[0]["BatchShortName"]);
            lblProduct_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Products"]);
            lblSubject_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Subjects"]);
            txtBatchStrength_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["MaxCapacity"]);
            lblCurBatchStrength_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["CurBatchStrength"]);
            lblStandardCode_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Standard_Code"]);
            lblCentreCode_Edit.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Centre_Code"]);
            //if (dsBatch.Tables[1].Rows.Count != 0)
            //{
            //    txtlmsproduct.Text = Convert.ToString(dsBatch.Tables[1].Rows[0]["ProductName"]);
            //    lmsproductcode.Text = Convert.ToString(dsBatch.Tables[1].Rows[0]["ProductCode"]);
            //}
            txtlmsproduct.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["ProductName"]);
            lmsproductcode.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["LMS_ProductCode"]);
           // ddlSubject_Edit.Enabled = false;
            //int Allow_Subject_Update = 0;

            if (Convert.ToInt32(dsBatch.Tables[3].Rows[0]["BatchCount"]) == 0)
            {
                ddlSubject_Edit.Enabled = true;

            }
            else if (Convert.ToInt32(dsBatch.Tables[3].Rows[0]["BatchCount"]) == 1)
            {
                ddlSubject_Edit.Enabled = false;
            }

            else
            { 
            ddlSubject_Edit.Enabled = false;
            }

            
        }
        else
        {
            lblDivision_Edit.Text = "";
            lblAcadYear_Edit.Text = "";
            lblStandard_Edit.Text = "";
            lblCentre_Edit.Text = "";
            lblBatchName_Edit.Text = "";
            txtBatchShortName_Edit.Text = "";
            lblProduct_Edit.Text = "";
            lblSubject_Edit.Text = "";
            txtBatchStrength_Edit.Text = "0";
            lblCurBatchStrength_Edit.Text = "0";
            lblStandardCode_Edit.Text = "";
            lblCentreCode_Edit.Text = "";            
            txtlmsproduct.Text = "";
            lmsproductcode.Text = "";

        }

        if (CommandName == "Edit")
        {
            //fill Products Combo
            //if there are no students in this batch then only allow user to edit this

            ddlProduct_Edit.DataSource = dsBatch.Tables[1];
            ddlProduct_Edit.DataTextField = "Stream_Name";
            ddlProduct_Edit.DataValueField = "Stream_Code";
            ddlProduct_Edit.DataBind();

            
            int RCnt = 0;
            if (dsBatch.Tables[1].Rows.Count > 0)
            {
                for (RCnt = 0; RCnt <= dsBatch.Tables[1].Rows.Count - 1; RCnt++)
                {
                    if (dsBatch.Tables[1].Rows[RCnt]["Selected_Batch_Code"].ToString().Trim() == lblPKey_Edit.Text.Trim())
                    {
                        ddlProduct_Edit.Items[RCnt].Selected = true;
                    }
                }
            }

            ddlSubject_Edit_Hidden.DataSource = dsBatch.Tables[2];
            ddlSubject_Edit_Hidden.DataTextField = "Subject_Name";
            ddlSubject_Edit_Hidden.DataValueField = "Subject_Code";
            ddlSubject_Edit_Hidden.DataBind();

            FillDDL_Subject_Edit(Convert.ToString(dsBatch.Tables[0].Rows[0]["LMS_ProductCode"]));

            if (Convert.ToInt32(lblCurBatchStrength_Edit.Text) == 0)
            {
                lblProduct_Edit.Visible = false;
                lblSubject_Edit.Visible = false;
                ddlSubject_Edit.Visible = true;
                ddlProduct_Edit.Visible = true;

                ChkActive.Disabled = false;
            }
            else
            {
                lblProduct_Edit.Visible = true;
                lblSubject_Edit.Visible = true;
                ddlSubject_Edit.Visible = false;
                ddlProduct_Edit.Visible = false;

                ChkActive.Disabled = true;
            }
            txtBatchShortName_Edit.Visible = true;
            ChkActive.Visible = true;
            BlockBatchHelp.Visible = true;
            NewBatchCountHelp.Visible = false;
            lblBolckBatch.Visible = true;
            lblNewBatchCount.Visible = false;
            lblEditBatchDetails_Header.Text = "Edit Batch Details";
            txtNewBatchCount_Edit.Visible = false;
            BtnSaveReplicate.Visible = false;
            BtnSaveEdit.Visible = true;
            txtBatchStrength_Edit.Enabled = true;
        }
        else
        {
            lblProduct_Edit.Visible = true;
            lblSubject_Edit.Visible = true;
            ddlSubject_Edit.Visible = false;
            ddlProduct_Edit.Visible = false;
            txtBatchShortName_Edit.Visible = false;
            ChkActive.Visible = false;
            BlockBatchHelp.Visible = false;
            NewBatchCountHelp.Visible = true;
            lblBolckBatch.Visible = false;
            lblNewBatchCount.Visible = true;
            lblEditBatchDetails_Header.Text = "Create New Batch(es) with reference to Batch " + lblBatchName_Edit.Text;
            txtNewBatchCount_Edit.Visible = true;
            BtnSaveReplicate.Visible = true;
            BtnSaveEdit.Visible = false;
            txtBatchStrength_Edit.Enabled = false;
            if (dsBatch != null)
            {
                if (dsBatch.Tables.Count != 0)
                {
                    if (dsBatch.Tables[0].Rows.Count != 0)
                    {
                        txtlmsproduct.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["ProductName"]);
                        lmsproductcode.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["LMS_ProductCode"]);
                    }
                    else
                    {
                        txtlmsproduct.Text = "";
                        lmsproductcode.Text = "";
                    }

                }
                else
                {
                    txtlmsproduct.Text = "";
                    lmsproductcode.Text = "";
                }
            }
            else
            {
                txtlmsproduct.Text = "";
                lmsproductcode.Text = "";
            }           

        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
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

    private void FillDDL_Division()
    {
        string Company_Code = "MT";
        string DBname = "CDB";
        
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;

        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Add.Items.Insert(0, "Select");
        ddlDivision_Add.SelectedIndex = 0;

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;

        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

    }

    private void FillDDL_Centre()
    {
        

        string Company_Code = "MT";
        string DBName = "CDB";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, Div_Code, "", "5", DBName);

        BindListBox(ddlCentre_Add, dsCentre, "Center_Name", "Center_Code");
        

    }

    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Centre();
        FillDDL_AllStandard();
        //FillDDL_Product();
        Clear_Error_Success_Box();
    }

    private void FillDDL_AllStandard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
        BindDDL(ddlStandard_Add, dsAllStandard, "Standard_Name", "Standard_Code");
        ddlStandard_Add.Items.Insert(0, "Select");
        ddlStandard_Add.SelectedIndex = 0;
    }

    private void FillDDL_Product()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        string AAG = null;
        AAG = ddlStandard_Add.SelectedValue;

        DataSet dsProduct = ProductController.GetAllActiveStreamsBy_Division_Year(Div_Code, "", AAG, "3");

        BindListBox(ddlProduct_Add, dsProduct, "Stream_Name", "Stream_Code");
    }





    protected void ddlStandard_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        
        FillDDL_LMSProduct();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Subject(string Product_Code)
    {
        string Stream_Code = null;
        Stream_Code = "";

        int PrdCnt = 0;
        for (PrdCnt = 0; PrdCnt <= ddlProduct_Add.Items.Count - 1; PrdCnt++)
        {
            if (ddlProduct_Add.Items[PrdCnt].Selected == true)
            {
                //Stream_Code = Stream_Code & "'" & ddlProduct_Add.Items(PrdCnt).Value & "',"
                Stream_Code = Stream_Code + ddlProduct_Add.Items[PrdCnt].Value + ",";
            }
        }
        //if (Strings.Right(Stream_Code, 1) == ",")
        //    Stream_Code = Strings.Left(Stream_Code, Strings.Len(Stream_Code) - 1);
        Stream_Code = Common.RemoveComma(Stream_Code);
        string AAG = null;
        AAG = ddlStandard_Add.SelectedValue;

        DataSet dsSubject = ProductController.Get_ClassroomProduct_Subject_By_LMS_PrdouctCode(Product_Code, "");
        ddlSubject_Add.DataSource = dsSubject.Tables[1];
        ddlSubject_Add.DataTextField = "Subject_Name";
        ddlSubject_Add.DataValueField = "Subject_Code";
        ddlSubject_Add.DataBind();

        //BindListBox(ddlSubject_Add, ds, "Subject_Name", "Subject_Code");
    }

    protected void ddlProduct_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        
        //FillDDL_LMSProduct();
        Clear_Error_Success_Box();
    }

    protected void BtnSaveAdd_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validate if all information is entered correctly
        if (ddlDivision_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision_Add.Focus();
            return;
        }

        if (ddlAcadYear_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear_Add.Focus();
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard_Add.Focus();
            return;
        }
        if (ddllmsproduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select LMS Product");
            ddllmsproduct.Focus();
            return;
        }
        

        int SelCnt = 0;
        SelCnt = 0;
        for (int cnt = 0; cnt <= ddlProduct_Add.Items.Count - 1; cnt++)
        {
            if (ddlProduct_Add.Items[cnt].Selected == true)
            {
                SelCnt = SelCnt + 1;
            }
        }
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0004");
            ddlProduct_Add.Focus();
            return;
        }

        SelCnt = 0;
        for (int cnt = 0; cnt <= ddlSubject_Add.Items.Count - 1; cnt++)
        {
            if (ddlSubject_Add.Items[cnt].Selected == true)
            {
                SelCnt = SelCnt + 1;
            }
        }
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject_Add.Focus();
            return;
        }

        SelCnt = 0;
        for (int cnt = 0; cnt <= ddlCentre_Add.Items.Count - 1; cnt++)
        {
            if (ddlCentre_Add.Items[cnt].Selected == true)
            {
                SelCnt = SelCnt + 1;
            }
        }
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0006");
            ddlCentre_Add.Focus();
            return;
        }

        //Save
        int ResultId = 0;
        string DivisionCode = null;
        DivisionCode = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;


        string ProductCode = "";
        int PrdCnt = 0;
        for (PrdCnt = 0; PrdCnt <= ddlProduct_Add.Items.Count - 1; PrdCnt++)
        {
            if (ddlProduct_Add.Items[PrdCnt].Selected == true)
            {
                ProductCode = ProductCode + ddlProduct_Add.Items[PrdCnt].Value + ",";
            }
        }
        ProductCode = Common.RemoveComma(ProductCode);
        //if (Strings.Right(ProductCode, 1) == ",")
        //    ProductCode = Strings.Left(ProductCode, Strings.Len(ProductCode) - 1);


        string SubjectCode = "";
        int SubCnt = 0;
        for (SubCnt = 0; SubCnt <= ddlSubject_Add.Items.Count - 1; SubCnt++)
        {
            if (ddlSubject_Add.Items[SubCnt].Selected == true)
            {
                SubjectCode = SubjectCode + ddlSubject_Add.Items[SubCnt].Value + ",";
            }
        }
        SubjectCode = Common.RemoveComma(SubjectCode);
        //if (Strings.Right(SubjectCode, 1) == ",")
        //    SubjectCode = Strings.Left(SubjectCode, Strings.Len(SubjectCode) - 1);
        string CentreCode = "";
        int CentreCnt = 0;
        for (CentreCnt = 0; CentreCnt <= ddlCentre_Add.Items.Count - 1; CentreCnt++)
        {
            if (ddlCentre_Add.Items[CentreCnt].Selected == true)
            {
                CentreCode = CentreCode + ddlCentre_Add.Items[CentreCnt].Value + ",";
            }
        }
        CentreCode = Common.RemoveComma(CentreCode);
        //if (Strings.Right(CentreCode, 1) == ",")
        //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        
        int MaxBatchStrength = 0;
        MaxBatchStrength = Convert.ToInt32(txtBatchStrength_Add.Text);

        //Label lblHeader_User_Code = default(Label);
        //lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        //string CreatedBy = null;
        //CreatedBy = lblHeader_User_Code.Text;
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string LMSProduct = ddllmsproduct.SelectedValue;

        ResultId = ProductController.Insert_Batches(DivisionCode, YearName, StandardCode, ProductCode, SubjectCode, CentreCode, MaxBatchStrength, UserID, LMSProduct);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);// 
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
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

    private void Clear_AddPanel()
    {
        ddlDivision_Add.SelectedIndex = 0;
        ddlAcadYear_Add.SelectedIndex = 0;

        ddlStandard_Add.Items.Clear();
        ddlProduct_Add.Items.Clear();
        ddlSubject_Add.Items.Clear();
        ddlCentre_Add.Items.Clear();
        txtBatchStrength_Add.Text = "";
    }

    private void Clear_EditPanel()
    {
        txtNewBatchCount_Edit.Text = "";
        txtBatchShortName_Edit.Text = "";
        txtBatchStrength_Edit.Text = "";
    }

    protected void ddlSubject_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Centre();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Search_Centre()
    {
        string Company_Code = "MT";
        string DBName = "CDB";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, Div_Code, "", "5", DBName);

        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        if (ddlAcadYear.SelectedItem.ToString() != "Select")
        {
            YearName = ddlAcadYear.SelectedItem.ToString();//"2014-2015";//ddlAcadYear.SelectedItem.ToString();181818
        }
        else
        {
            YearName = "2014-2015";//ddlAcadYear.SelectedItem.ToString();181818
        }
        

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //ddlStandard.Items.Insert(0, "All")
        //ddlStandard.SelectedIndex = 0
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }

    protected void BtnCloseEdit_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
    }

    protected void ddlProduct_Edit_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //FillDDL_Subject_Edit(ddlProduct_Edit.SelectedValue);
        Clear_Error_Success_Box();
    }

    private void FillDDL_Subject_Edit(string Product_Code)
    {
        string Stream_Code = null;
        Stream_Code = "";

        int PrdCnt = 0;
        for (PrdCnt = 0; PrdCnt <= ddlProduct_Edit.Items.Count - 1; PrdCnt++)
        {
            if (ddlProduct_Edit.Items[PrdCnt].Selected == true)
            {
                Stream_Code = Stream_Code + ddlProduct_Edit.Items[PrdCnt].Value + ",";
            }
        }
        Stream_Code = Common.RemoveComma(Stream_Code);
        //Stream_Code = Stream_Code.LastIndexOf(",") == Stream_Code.Length - 1 ? Stream_Code.Substring(0, Stream_Code.Length - 1) : Stream_Code;
        //if (Strings.Right(Stream_Code, 1) == ",")
        //    Stream_Code = Strings.Left(Stream_Code, Strings.Len(Stream_Code) - 1);

        string AAG = null;
        AAG = lblStandardCode_Edit.Text;


        DataSet dsSubject = ProductController.Get_ClassroomProduct_Subject_By_LMS_PrdouctCode(Product_Code, "");
        ddlSubject_Edit.DataSource = dsSubject.Tables[1];
        ddlSubject_Edit.DataTextField = "Subject_Name";
        ddlSubject_Edit.DataValueField = "Subject_Code";
        ddlSubject_Edit.DataBind();

        //BindListBox(ddlSubject_Add, dsSubject, "Subject_Name", "Subject_Code");

        //Check if any subject is already selected or not
        int RCnt = 0;
        int RCnt1 = 0;
        for (RCnt = 0; RCnt <= ddlSubject_Edit_Hidden.Items.Count - 1; RCnt++)
        {
            for (RCnt1 = 0; RCnt1 <= ddlSubject_Edit.Items.Count - 1; RCnt1++)
            {
                if (ddlSubject_Edit_Hidden.Items[RCnt].Value == ddlSubject_Edit.Items[RCnt1].Value)
                {
                    ddlSubject_Edit.Items[RCnt1].Selected = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        }

    }

    protected void ddlSubject_Edit_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        int SelCnt = 0;
        SelCnt = 0;
        for (int cnt = 0; cnt <= ddlProduct_Edit.Items.Count - 1; cnt++)
        {
            if (ddlProduct_Edit.Items[cnt].Selected == true)
            {
                SelCnt = SelCnt + 1;
            }
        }
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0004");
            ddlProduct_Edit.Focus();
            return;
        }

        SelCnt = 0;
        for (int cnt = 0; cnt <= ddlSubject_Edit.Items.Count - 1; cnt++)
        {
            if (ddlSubject_Edit.Items[cnt].Selected == true)
            {
                SelCnt = SelCnt + 1;
            }
        }
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject_Edit.Focus();
            return;
        }

        //Save
        int ResultId = 0;

        string PKey = null;
        PKey = lblPKey_Edit.Text;

        string ProductCode = "";
        int PrdCnt = 0;
        for (PrdCnt = 0; PrdCnt <= ddlProduct_Edit.Items.Count - 1; PrdCnt++)
        {
            if (ddlProduct_Edit.Items[PrdCnt].Selected == true)
            {
                ProductCode = ProductCode + ddlProduct_Edit.Items[PrdCnt].Value + ",";
            }
        }
        ProductCode = Common.RemoveComma(ProductCode);
        string SubjectCode = "";
        int SubCnt = 0;
        for (SubCnt = 0; SubCnt <= ddlSubject_Edit.Items.Count - 1; SubCnt++)
        {
            if (ddlSubject_Edit.Items[SubCnt].Selected == true)
            {
                SubjectCode = SubjectCode + ddlSubject_Edit.Items[SubCnt].Value + ",";
            }
        }
        SubjectCode = Common.RemoveComma(SubjectCode);
        //if (Strings.Right(SubjectCode, 1) == ",")
        //    SubjectCode = Strings.Left(SubjectCode, Strings.Len(SubjectCode) - 1);

        int MaxBatchStrength = 0;
        MaxBatchStrength = Convert.ToInt32(txtBatchStrength_Edit.Text);

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        int IsActiveFlag = 0;
        if (ChkActive.Checked == true)
        {
            IsActiveFlag = 0;
        }
        else
        {
            IsActiveFlag = 1;
        }

        string BatchShortName = null;
        BatchShortName = txtBatchShortName_Edit.Text;

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string LMSProduct = txtlmsproduct.Text;

        string AlteredBy = null;
        AlteredBy = UserID;

        ResultId = ProductController.Update_Batch(PKey, ProductCode, SubjectCode, MaxBatchStrength, BatchShortName, IsActiveFlag, AlteredBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            Clear_EditPanel();
        }

       // BtnSearch_Click(sender, e);
    }

    protected void BtnSaveReplicate_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validate if all information is entered correctly
        if (Convert.ToInt32(txtNewBatchCount_Edit.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0009");
            txtNewBatchCount_Edit.Focus();
            return;
        }

        //Save
        int ResultId = 0;

        int NewBatchCount = 0;
        NewBatchCount = Convert.ToInt32(txtNewBatchCount_Edit.Text);

        //Label lblHeader_User_Code = default(Label);
        //lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string LMSProduct = txtlmsproduct.Text;
        string LMSProductcode = lmsproductcode.Text;
       

        string CreatedBy = null;
        CreatedBy = UserID;

        ResultId = ProductController.Insert_Batches_LikeExistingBatch(lblPKey_Edit.Text, lblCentreCode_Edit.Text, NewBatchCount, CreatedBy,LMSProductcode);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
           // BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_EditPanel();
        }


    }

    //protected void BtnSearch_Click(object sender, System.EventArgs e)
    //{
    //    //Validate if all information is entered correctly
    //    if (ddlDivision.SelectedIndex == 0)
    //    {
    //        Show_Error_Success_Box("E", "0001");
    //        ddlDivision.Focus();
    //        return;
    //    }

    //    if (ddlAcadYear.SelectedIndex == 0)
    //    {
    //        Show_Error_Success_Box("E", "0002");
    //        ddlAcadYear.Focus();
    //        return;
    //    }

    //    ControlVisibility("Result");

    //    string DivisionCode = null;
    //    DivisionCode = ddlDivision.SelectedValue;

    //    string YearName = null;
    //    YearName = ddlAcadYear.SelectedItem.ToString();

    //    string StandardCode = "";
    //    int StdCnt = 0;
    //    int StdSelCnt = 0;
    //    for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
    //    {
    //        if (ddlStandard.Items[StdCnt].Selected == true)
    //        {
    //            StdSelCnt = StdSelCnt + 1;
    //        }
    //    }

    //    if (StdSelCnt == 0)
    //    {
    //        //When all is selected
    //        for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
    //        {
    //            StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
    //        }
    //        StandardCode = StandardCode.LastIndexOf(",") == StandardCode.Length - 1 ? StandardCode.Substring(0, StandardCode.Length - 1) : StandardCode;
    //        //if (Strings.Right(StandardCode, 1) == ",")
    //        //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
    //    }
    //    else
    //    {
    //        for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
    //        {
    //            if (ddlStandard.Items[StdCnt].Selected == true)
    //            {
    //                StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
    //            }
    //        }
    //        StandardCode = StandardCode.LastIndexOf(",") == StandardCode.Length - 1 ? StandardCode.Substring(0, StandardCode.Length - 1) : StandardCode;
    //        //if (Strings.Right(StandardCode, 1) == ",")
    //        //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
    //    }

    //    string CentreCode = "";
    //    int CentreCnt = 0;
    //    int CentreSelCnt = 0;
    //    for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
    //    {
    //        if (ddlCentre.Items[CentreCnt].Selected == true)
    //        {
    //            CentreSelCnt = CentreSelCnt + 1;
    //        }
    //    }

    //    if (CentreSelCnt == 0)
    //    {
    //        //all are selected
    //        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
    //        {
    //            CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
    //        }
    //        CentreCode = CentreCode.LastIndexOf(",") == CentreCode.Length - 1 ? CentreCode.Substring(0, CentreCode.Length - 1) : CentreCode;
    //        //if (Strings.Right(CentreCode, 1) == ",")
    //        //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
    //    }
    //    else
    //    {
    //        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
    //        {
    //            if (ddlCentre.Items[CentreCnt].Selected == true)
    //            {
    //                CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
    //            }
    //        }
    //        CentreCode = CentreCode.LastIndexOf(",") == CentreCode.Length - 1 ? CentreCode.Substring(0, CentreCode.Length - 1) : CentreCode;
    //        //if (Strings.Right(CentreCode, 1) == ",")
    //        //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
    //    }

    //    string BatchName = null;
    //    if ((txtBatchName.Text.Trim() == ""))
    //    {
    //        BatchName = "%";
    //    }
    //    else
    //    {
    //        BatchName = "%" + txtBatchName.Text.Trim();
    //    }

    //    DataSet dsGrid = ProductController.GetBatchBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, CentreCode, BatchName);
    //    dlGridDisplay.DataSource = dsGrid;
    //    dlGridDisplay.DataBind();

    //    dlGridExport.DataSource = dsGrid;
    //    dlGridExport.DataBind();

    //    lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
    //    lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
    //    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
    //}
    public Manage_Batch()
    {
        Load += Page_Load;
    }

    private void FillDDL_LMSProduct()
    {
        //string Stream_Code = null;
        //Stream_Code = "";

        //int PrdCnt = 0;
        //for (PrdCnt = 0; PrdCnt <= ddlProduct_Add.Items.Count - 1; PrdCnt++)
        //{
        //    if (ddlProduct_Add.Items[PrdCnt].Selected == true)
        //    {
        //        //Stream_Code = Stream_Code & "'" & ddlProduct_Add.Items(PrdCnt).Value & "',"
        //        Stream_Code = Stream_Code + ddlProduct_Add.Items[PrdCnt].Value + ",";
        //    }
        //}
        //if (Strings.Right(Stream_Code, 1) == ",")
        //    Stream_Code = Strings.Left(Stream_Code, Strings.Len(Stream_Code) - 1);
        //Stream_Code = Common.RemoveComma(Stream_Code);
        //DataSet dsSubject = ProductController.GetAllActiveSubjectsBy_Stream_AAG(Stream_Code, AAG, 1);
        //DataSet dslmsproduct = ProductController.GetAllActiveLMSProductBy_Stream(Stream_Code, 1);

        DataSet dslmsproduct = ProductController.GetAllActiveLMSProductBy_Course_Acad_year(ddlStandard_Add.SelectedValue, ddlAcadYear_Add.SelectedValue);
        BindDDL(ddllmsproduct, dslmsproduct, "ProductName", "Product_Code");
        ddllmsproduct.Items.Insert(0, "Select");
        ddllmsproduct.SelectedIndex = 0;

        //BindListBox(ddlSubject_Add, dsSubject, "Subject_Name", "Subject_Code");
    }

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Batch_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>Batch</b></TD></TR>");
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

    protected void ddllmsproduct_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddllmsproduct.SelectedIndex == 0)
        {
            ddlProduct_Add.Items.Clear();
            ddlSubject_Add.Items.Clear();
        }
        else { 
        FillDDL_Product();
        FillDDL_Subject(ddllmsproduct.SelectedValue);
        DataSet ds = ProductController.Get_ClassroomProduct_Subject_By_LMS_PrdouctCode(ddllmsproduct.SelectedValue,"");
        for (int cnt = 0; cnt <= ds.Tables[0].Rows.Count - 1; cnt++)
        {
            for (int rcnt = 0; rcnt <= ddlProduct_Add.Items.Count - 1; rcnt++)
            {
                if (ddlProduct_Add.Items[rcnt].Value == ds.Tables[0].Rows[cnt]["ClassRoomProductCode"].ToString())
                {
                    ddlProduct_Add.Items[rcnt].Selected = true;
                    break;
                }
            }
        }



        for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
        {
            for (int rcnt = 0; rcnt <= ddlSubject_Add.Items.Count - 1; rcnt++)
            {
                if (ddlSubject_Add.Items[rcnt].Value == ds.Tables[1].Rows[cnt]["SubjectCode"].ToString())
                {
                    ddlSubject_Add.Items[rcnt].Selected = true;
                    break;
                }
            }
        }

        //ddlSubject_Add.IsEnabled = false;
        ddlSubject_Add.Enabled = false;
        }
    }
    protected void dlGridExport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}