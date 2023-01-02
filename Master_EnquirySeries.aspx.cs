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

public partial class Master_EnquirySeries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
           
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

    /// <summary>
    /// Fill Academic Year dropdown
    /// </summary>
    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
            ddlAcadYear.Items.Insert(0, "Select");
            ddlAcadYear.SelectedIndex = 0;

            BindDDL(ddlAcadYearAdd, dsAcadYear, "Description", "Id");
            ddlAcadYearAdd.Items.Insert(0, "Select");
            ddlAcadYearAdd.SelectedIndex = 0;   
            
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


    private void Clear_SearchPanel()
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlZone.Items.Clear();
        ddlCenter.Items.Clear();

    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        lblSuccess.Text = "";
        UpdatePanelMsgBox.Update();
    }
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
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
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
           
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            btnSave.Visible = true;

        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            btnSave.Visible = false;
        }

        else if (Mode == "EditClose")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }
        Clear_Error_Success_Box();
        
        ControlVisibility("Result");
        
        DataSet dsGrid = new DataSet();
        //dsGrid = ProductController.GetCenter(txtsearchcentername.Text.Trim(), ddlDivision.SelectedValue,1);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlDisplay.DataSource= dsGrid;
                dlDisplay.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();
            }
            else
            {
                dlDisplay.DataSource= null;
                dlDisplay.DataBind();
                lbltotalcount.Text = "0";

                DataList1.DataSource = null;
                DataList1.DataBind();
            }
        }
        else
        {
            dlDisplay.DataSource= null;
            dlDisplay.DataBind();
            lbltotalcount.Text = "0";

            DataList1.DataSource = null;
            DataList1.DataBind();
        }

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        lblHeader_Add.Text = "Create New Enquiry Series";
        clear_edit_addfiled();        
        btnSave.Visible = true;
        lblPKey.Text = "";
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_SearchPanel();
    }
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Config_Center.aspx");
        ControlVisibility("Search");
        Clear_Error_Success_Box();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();



        if (ddlDivisionName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivisionName.Focus();
            return;
        }

        if (ddlAcadYearAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcadYearAdd.Focus();
            return;
        }

        if (ddlZoneAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Zone");
            ddlZone.Focus();
            return;
        }

        if (ddlCenterAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center");
            ddlCenterAdd.Focus();
            return;
        }


        string ProductCode = "";
        for (int cnt = 0; cnt <= ddlProduct_add.Items.Count - 1; cnt++)
        {
            if (ddlProduct_add.Items[cnt].Selected == true)
            {
                ProductCode = ProductCode + ddlProduct_add.Items[cnt].Value + ",";
            }
        }
        if (ProductCode == "")
        {
            Show_Error_Success_Box("E", "Atleast one Product should be selected");
            ddlProduct_add.Focus();
            return;
        }

        if (txtStartNumber.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Start Number");
            txtStartNumber.Focus();
            return;
        }

        if (txtEndNumber.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter End Number");
            txtEndNumber.Focus();
            return;
        }

        if (Convert.ToInt32(txtEndNumber.Text.Trim()) <= Convert.ToInt32(txtStartNumber.Text.Trim()))
        {
            Show_Error_Success_Box("E", "End Number is less than or Equal to Start Number");
            txtEndNumber.Focus();
            return;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string flag = "";

        if (lblPKey.Text == "")
            flag = "1";
        else
            flag = "2";

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        //resultid = ProductController.Insert_Update_Center(ddlCountry.SelectedValue, ddlCity.SelectedValue, ddlCity.SelectedItem.Text, ddllocation.SelectedValue, ddllocation.SelectedItem.Text, ddlDivisionName.SelectedValue, txtcentercode.Text, txtcentername.Text,"","","","", ActiveFlag, UserID, "1",ddlState.SelectedValue .ToString (),ddlZone .SelectedValue .ToString ().Trim (),txtcenshort.Text .Trim ());

        if (resultid == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Center Code already Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            clear_edit_addfiled();
            ///
            ControlVisibility("Result");

            DataSet dsGrid = new DataSet();
            //dsGrid = ProductController.GetCenter(txtsearchcentername.Text.Trim(), ddlDivisionName.SelectedValue, 1);
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {

                    dlDisplay.DataSource= dsGrid;
                    dlDisplay.DataBind();
                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    dlDisplay.DataSource= null;
                    dlDisplay.DataBind();
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                dlDisplay.DataSource= null;
                dlDisplay.DataBind();
                lbltotalcount.Text = "0";
            }

            return;

        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Enquiery_Series" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Enquiry Series</b></TD></TR>");
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
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {        
        Clear_Error_Success_Box();
    }
        

    
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
            BindDDL(ddlDivisionName, dsDivision, "Division_Name", "Division_Code");
            ddlDivisionName.Items.Insert(0, "Select");
            ddlDivisionName.SelectedIndex = 0;

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
    protected void dlDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Edit");
            clear_edit_addfiled();
            
        }
    }
   
    protected void btnClear_Edit_Click(object sender, EventArgs e)
    {
        ControlVisibility("EditClose");
        Clear_Error_Success_Box();
    }
    protected void ddlDivisionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Zone();
    }

    private void FillDDL_Zone()
    {
        DataSet dsCity = ProductController.GetallZonebyDivision(ddlDivisionName.SelectedValue.Trim(),1);
        BindDDL(ddlZoneAdd, dsCity, "Zone_Name", "Zone_Code");
        ddlZoneAdd.Items.Insert(0, "Select Zone");
        ddlZoneAdd.SelectedIndex = 0;
    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    private void FillDDL_ZoneSearchPanel()
    {
        DataSet dsZone = ProductController.GetallZonebyDivision(ddlDivision.SelectedValue.Trim(), 1);
        BindDDL(ddlZone, dsZone, "Zone_Name", "Zone_Code");
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }



    private void FillDDL_CenterSearchPanel()
    {
        DataSet dsCenter = ProductController.GetAllCenterbyZone(ddlZone.SelectedValue.Trim());
        BindDDL(ddlCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
        ddlCenter.Items.Insert(0, "All");
        ddlCenter.SelectedIndex = 0;
    }


    private void FillDDL_CenterAddPanel()
    {
        DataSet dsCenter = ProductController.GetAllCenterbyZone(ddlZoneAdd.SelectedValue.Trim());
        BindDDL(ddlCenterAdd, dsCenter, "Source_Center_Name", "Source_Center_Code");
        ddlCenterAdd.Items.Insert(0, "Select");
        ddlCenterAdd.SelectedIndex = 0;
    }

   

    private void FillDDL_Product()
    {
        try
        {
            ddlProduct_add.Items.Clear();
            string CenterCode = null;
            CenterCode = ddlCenterAdd.SelectedValue;

            DataSet dsProduct = ProductController.GetAllActiveStreamsBy_Center_Year(ddlAcadYearAdd.SelectedValue, CenterCode, "1");
            BindListBox(ddlProduct_add, dsProduct, "Stream_Name", "Stream_Code");
        }
        catch
        {
            ddlProduct_add.Items.Clear();
        }
    }

    private void clear_edit_addfiled()
    {

        ddlDivisionName.SelectedIndex = 0;
        ddlAcadYearAdd.SelectedIndex = 0;
        ddlZoneAdd.Items.Clear();
        ddlCenterAdd.Items.Clear();
        ddlProduct_add.Items.Clear();
        chkActive.Checked = true;
        txtStartNumber.Text = "";
        txtEndNumber.Text = "";
       
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCenter.Items.Clear();
        FillDDL_ZoneSearchPanel();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_CenterSearchPanel();
    }
    protected void ddlZoneAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_CenterAddPanel();
    }
    protected void ddlAcadYearAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Product();
    }
    protected void ddlCenterAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Product();
    }
}