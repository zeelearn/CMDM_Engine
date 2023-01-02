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

public partial class Config_Zone : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             //Page_Validation();
             FillDDL_SearchDivision();
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
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        try
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void FillDDL_Division()
    {
        try
        {


            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetDivision(string.Empty, string.Empty, "1");
            BindDDL(ddlDivision, dsDivision, "Source_Division_shortdesc", "Source_Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    //private void FillDDL_Division()
    //{
    //    string Company_Code = "MT";
    //    string DBname = "CDB";

    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    string UserID = cookie.Values["UserID"];
    //    string UserName = cookie.Values["UserName"];

    //    DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
    //    BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
    //    ddlDivision.Items.Insert(0, "Select");
    //    ddlDivision.SelectedIndex = 0;

    //}

    private void FillDDL_SearchDivision()
    {
        try
        {
            //string Company_Code = "MT";
            //string DBname = "CDB";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsSearchDivision = ProductController.GetDivision(string.Empty, string.Empty, "1");
            //DataSet dsSearchDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
            BindDDL(ddlsearchdivision, dsSearchDivision, "Source_Division_shortdesc", "Source_Division_Code");
            ddlsearchdivision.Items.Insert(0, "Select");
            ddlsearchdivision.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
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

        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;

        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsearchdivision.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlsearchdivision.Focus();
                return;
            }
            ControlVisibility("Result");

            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetZone(ddlsearchdivision.SelectedValue.Trim(), txtsearchzone.Text.Trim(), string.Empty, "1");
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    Clear_Error_Success_Box();
                    ddlBoard.DataSource = dsGrid;
                    ddlBoard.DataBind();

                    DataList1.DataSource = dsGrid;
                    DataList1.DataBind();

                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    ddlBoard.DataSource = null;
                    ddlBoard.DataBind();

                    DataList1.DataSource = null;
                    DataList1.DataBind();

                    lbltotalcount.Text = "0";
                }
            }
            else
            {

                ddlBoard.DataSource = null;
                ddlBoard.DataBind();

                DataList1.DataSource = null;
                DataList1.DataBind();

                lbltotalcount.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString ());
            //ddlDivision.Focus();
            return;
        }

        

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Config_Zone.aspx");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {

        Clear_Error_Success_Box();
        ControlVisibility("Add");
        BtnSaveEdit.Visible = false;
        txtzonename.Text = "";

        ddlDivision.Enabled = true;

        btnSave.Visible = true;
        lblHeader_Add.Text = "Add Zone Details";
        FillDDL_Division();
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Zone_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Zone</b></TD></TR>");
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
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    private void Fill_Grid()
    {
        ControlVisibility("Result");
        string flags = "3";

        DataSet dsGrid = new DataSet();
       // dsGrid = ProductController.GetZone(string.Empty,string.Empty, lblslotid.Text.Trim(), flags);
        dsGrid = ProductController.GetZone(ddlDivision.SelectedValue.Trim(), txtsearchzone.Text.ToString(), string.Empty, flags);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                ddlBoard.DataSource = dsGrid;
                ddlBoard.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                ddlBoard.DataSource = null;
                ddlBoard.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            ddlBoard.DataSource = null;
            ddlBoard.DataBind();
            lbltotalcount.Text = "0";
        }
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (txtzonename.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Zone name");
            txtzonename.Focus();
            return;
        }
        if (ddlDivision.SelectedValue == "select")
        {
            Show_Error_Success_Box("E", "select division name");
            ddlDivision.Focus();
            return;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string flag = "2";

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        resultid = ProductController.Insert_Update_Zone(txtzonename.Text.Trim(), lblslotid.Text.Trim(), ddlDivision.SelectedValue, ActiveFlag, UserID, flag);


        if (resultid == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Zone Name already Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Fill_Grid();
            return;

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtzonename.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Zone Name");
            txtzonename.Focus();
            return;
        }
        if (ddlDivision.SelectedValue == "")
        {
            Show_Error_Success_Box("E", "Select division");
            ddlDivision.Focus();
            return;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string flag = "1";

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        resultid = ProductController.Insert_Update_Zone(txtzonename.Text.Trim(), string.Empty, ddlDivision.SelectedValue, ActiveFlag, UserID, flag);
        if (resultid == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Zone Name already Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Fill_Grid();
            return;

        }


    }
    protected void ddlBoard_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");

            lblslotid.Text = e.CommandArgument.ToString();
            FillBoardDetails(lblslotid.Text, e.CommandName);
        }
    }
    private void FillBoardDetails(string PKey, string CommandName)
    {

        try
        {

          
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetZone(string.Empty,string.Empty, PKey, "2");

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                ddlDivision.Enabled = false;
                txtzonename.Text = dsGrid.Tables[0].Rows[0]["Zone_Name"].ToString();
                FillDDL_Division();
                ddlDivision.SelectedValue = dsGrid.Tables[0].Rows[0]["source_division_code"].ToString();

                if (dsGrid.Tables[0].Rows[0]["Is_Active"].ToString() == "1")
                {

                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                btnSave.Visible = false;
                BtnSaveEdit.Visible = true;
                lblHeader_Add.Text = "Edit Zone Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
}