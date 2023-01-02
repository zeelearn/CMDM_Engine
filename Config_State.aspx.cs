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

public partial class Config_State : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            FillDDL_Country();
            FillDDL_SearchCountry();
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
        if (ddlsearchcountry.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Country");
            ddlCountry.Focus();
            return;
        }

        Clear_Error_Success_Box();
        ControlVisibility("Result");
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetState(ddlsearchcountry.SelectedValue.Trim(), txtSearchState.Text.Trim(), string.Empty, "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();

                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();

                DataList1.DataSource = null;
                DataList1.DataBind();

                lbltotalcount.Text = "0";
            }
        }
        else
        {

            dlGridDisplay.DataSource = null;
            dlGridDisplay.DataBind();

            DataList1.DataSource = null;
            DataList1.DataBind();

            lbltotalcount.Text = "0";
        }

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearchState.Text = "";
        ddlsearchcountry.SelectedIndex = 0;
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
        lblHeader_Add.Text = "Create New State";
        ddlCountry.SelectedIndex = 0;
        txtstatename.Text = "";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "State_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>State(s)</b></TD></TR>");
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
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_Error_Success_Box();
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Country");
            ddlCountry.Focus();
            return;
        }
        if (txtstatename.Text == "")
        {
            Show_Error_Success_Box("E", "Enter state");
            txtstatename.Focus();
            return;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string flag = "2";

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        //resultid = ProductController.Insert_Update_State(lblslotid.Text, ddlCountry.SelectedValue, txtstatename.Text, UserID, ActiveFlag, flag,"");

        //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
        //{
        //    Msg_Error.Visible = true;
        //    Msg_Success.Visible = false;
        //    lblerror.Text = "State Name already Exists!!";
        //    UpdatePanelMsgBox.Update();
        //    return;
            
        //}
        //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
        //{
        //    Msg_Error.Visible = false;
        //    Msg_Success.Visible = true;
        //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
        //    string Return_Pkey_CRM = ms.CreateState(resultid.Tables[0].Rows[0]["StateCode"].ToString(), resultid.Tables[0].Rows[0]["StateName"].ToString(), resultid.Tables[0].Rows[0]["Country"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            
        //    resultid = null;
        //    resultid = ProductController.Insert_Update_State(lblslotid.Text, ddlCountry.SelectedValue, txtstatename.Text.Trim(), UserID, ActiveFlag, "3", Return_Pkey_CRM);

        //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
        //    {
        //        lblSuccess.Text = "Records Saved Successfully!!";
        //    }
        //    UpdatePanelMsgBox.Update();
        //    FillGrid();
        //    return;

        //}


        resultid = ProductController.Insert_Update_State(lblslotid.Text, ddlCountry.SelectedValue, txtstatename.Text, UserID, ActiveFlag, flag);

        if (resultid == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "State Name already Exists!!";
            UpdatePanelMsgBox.Update();
            return;

        }
        else if (resultid == 1)
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            FillGrid();
            return;

        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCountry.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Country");
                ddlCountry.Focus();
                return;
            }
            if (txtstatename.Text == "")
            {
                Show_Error_Success_Box("E", "Enter State Name");
                txtstatename.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int resultid = 0;
            string flag = "1";

            int ActiveFlag = 0;
            if (chkActiveFlag.Checked == true)
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }

            //resultid = ProductController.Insert_Update_State(string.Empty, ddlCountry.SelectedValue, txtstatename.Text.Trim(), UserID, ActiveFlag, flag, "");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "State Name already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;


            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateState(resultid.Tables[0].Rows[0]["StateCode"].ToString(), resultid.Tables[0].Rows[0]["StateName"].ToString(), resultid.Tables[0].Rows[0]["Country"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    string statecode = resultid.Tables[0].Rows[0]["StateCode"].ToString();
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_State(statecode, ddlCountry.SelectedValue, txtstatename.Text.Trim(), UserID, ActiveFlag, "3", Return_Pkey_CRM);

            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";
            //    }
            //    UpdatePanelMsgBox.Update();
            //    FillGrid();
            //    return;

            //}


            resultid = ProductController.Insert_Update_State(string.Empty, ddlCountry.SelectedValue, txtstatename.Text.Trim(), UserID, ActiveFlag, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "State Name already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();
                FillGrid();
                return;

            }

        }

        catch(Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    #region Methods
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
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
        chkActiveFlag.Checked = false;
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;

    }
    private void FillDDL_Country()
    {

        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlCountry, dsDivision, "Country_Name", "Country_Code");
        ddlCountry.Items.Insert(0, "Select");
        ddlCountry.SelectedIndex = 0;
    }
    private void FillDDL_SearchCountry()
    {
        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlsearchcountry, dsDivision, "Country_Name", "Country_Code");
        ddlsearchcountry.Items.Insert(0, "Select");
        ddlsearchcountry.SelectedIndex = 0;
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillGrid()
    {

        ControlVisibility("Result");
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetState(ddlsearchcountry.SelectedValue.Trim(), txtSearchState.Text.Trim(), string.Empty, "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            dlGridDisplay.DataSource = null;
            dlGridDisplay.DataBind();
            lbltotalcount.Text = "0";
        }
    }
    #endregion
    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "Edit")
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
            dsGrid = ProductController.GetState(string.Empty, string.Empty, lblslotid.Text, "2");

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtstatename.Text = dsGrid.Tables[0].Rows[0]["state_Name"].ToString();
                ddlCountry.SelectedValue = dsGrid.Tables[0].Rows[0]["country_code"].ToString();

                if (dsGrid.Tables[0].Rows[0]["Is_Active"].ToString() == "1")
                {
                    chkActiveFlag.Checked = true;
                }
                else
                {
                    chkActiveFlag.Checked = false;
                }

                BtnSave.Visible = false;
                BtnSaveEdit.Visible = true;
                lblHeader_Add.Text = "Edit State Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

}