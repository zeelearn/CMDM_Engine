using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Configuration;

public partial class Stream_Download_from_SAP_Mastertable : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            FillDDL_Division();
            FillDDL_AcadYear();
           
            ControlVisibility("Search");
            Clear_Error_Success_Box();

        }
    }
   
 
    private void Clear_ClassRoomProductAddPanel()
    {
        //ddlDivisionAdd.SelectedIndex = 0;
        //ddlAcadYearAdd.SelectedIndex = 0;
       

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

            //BindDDL(ddlDivisionAdd, dsDivision, "Division_Name", "Division_Code");
            //ddlDivisionAdd.Items.Insert(0, "Select");
            //ddlDivisionAdd.SelectedIndex = 0;
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
    private void FillDDL_Center()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_1(4, "");
            if (dsCenter != null)
            {
                if (dsCenter.Tables.Count != 0)
                {
                    //BindListBox(ddlCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
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
    private void FillDDL_Center1()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_1(4, ddlDivision.SelectedValue);
            if (dsCenter != null)
            {
                if (dsCenter.Tables.Count != 0)
                {
                   // BindListBox(ddlCenter1, dsCenter, "Source_Center_Name", "Source_Center_Code");
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
   
    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
            ddlAcadYear.Items.Insert(0, "Select");
            ddlAcadYear.SelectedIndex = 0;

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
    
    private void Fill_Grid()
    {
        Clear_Error_Success_Box();
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.Get_SAPstream_details(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, txtStreamCode.Text.Trim(), "", "", "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables[0].Rows.Count > 0)
            {

               
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                dlClassRoomProduct.DataSource = dsGrid;
                dlClassRoomProduct.DataBind();
            }
            else
            {
                Show_Error_Success_Box("E", "Record  Not Found");
                ControlVisibility("Search");
                return;
            }
        }
        else
        {
            

            //dlGridExport.DataSource = null;
            //dlGridExport.DataBind();
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
            DivDowmloadPanel.Visible = false;
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            btnTopSearch.Visible = false;
            BtnDownload.Visible = true;
        }

        else if (Mode == "Result")
        {
            DivDowmloadPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = false;
            BtnDownload.Visible = true;
            DivResultPanel.Visible = true;
            btnTopSearch.Visible = true;
          

        }
        else if (Mode == "Add")
        {
            DivDowmloadPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnDownload.Visible = false;
            DivResultPanel.Visible = false;

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
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    
    
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ddlDivision.SelectedIndex = 0;
            ddlAcadYear.SelectedIndex = 0;
            txtStreamName.Text = "";

            //ddlStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivision.Focus();
                return;
            }
            if (ddlAcadYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddlAcadYear.Focus();
                return;
            }
            if (txtStreamCode.Text == "")
            {
                Show_Error_Success_Box("E", "StreamCode Can't be Null");
                return;
            }



            ControlVisibility("Result");
            Fill_Grid();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            ControlVisibility("Add");
            lblHeader_Add.Text = "Download Stream Master";
            Clear_ClassRoomProductAddPanel();
            

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    
    
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ControlVisibility("Result");
            Clear_Error_Success_Box();

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
   

   
    protected void btnCloseItemLevel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btnCloseItemHeader_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btnClose_SubGroupToResult_Click(object sender, EventArgs e)
    {
        try
        {
            ControlVisibility("Result");

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    
    /// <summary>
    /// added by sujeer for new changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            FillDDL_Center();
            


        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
  
   
   
   
    public System.Web.UI.WebControls.DropDownList ddlSubjectGroup { get; set; }
  
    
    public string Crs_SDate { get; set; }
    public string Crs_EDate { get; set; }
    public string Adm_SDate { get; set; }
    public string Adm_EDate { get; set; }
    public string Sub_Sdate { get; set; }
    public string Sub_Edate { get; set; }
    public int cnt { get; set; }
   
    
    protected void btnTopSearch_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
  
    }
    protected void ddlCenter1_SelectedIndexChanged(object sender, EventArgs e)
    {


        FillDDL_Center1();

    }
    
    protected void BtnclosseditClick(object sender, EventArgs e)
    {

        {

            ControlVisibility("Result");
        

        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    { 
    

    }
    
}

