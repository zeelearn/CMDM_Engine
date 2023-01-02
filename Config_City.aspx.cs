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

public partial class Config_City : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
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

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void FillDDL_Country()
    {

        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlCountry, dsDivision, "Country_Name", "Country_Code");
        ddlCountry.Items.Insert(0, "Select");
        ddlCountry.SelectedIndex = 0;

        //BindDDL(ddlCountry_Add, dsDivision, "Country_Name", "Country_Code");
        //ddlCountry_Add.Items.Insert(0, "Select");
        //ddlCountry_Add.SelectedIndex = 0;

    }
    private void FillDDL_SearchCountry()
    {
        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlSearchCountry, dsDivision, "Country_Name", "Country_Code");
        ddlSearchCountry.Items.Insert(0, "Select");
        ddlSearchCountry.SelectedIndex = 0;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {


        if (ddlSearchCountry.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Country");
            ddlSearchCountry.Focus();
            return;
        }
        if (ddlSearchState.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select State");
            ddlSearchState.Focus();
            return;
        }
        Clear_Error_Success_Box();
        ControlVisibility("Result");
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetCity(ddlSearchCountry.SelectedValue, ddlSearchState.SelectedValue, txtsearchcity.Text.Trim(), "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                ddlBoard.DataSource = dsGrid;
                ddlBoard.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();
            }
            else
            {
                ddlBoard.DataSource = null;
                ddlBoard.DataBind();
                lbltotalcount.Text = "0";

                DataList1.DataSource = null;
                DataList1.DataBind();
            }
        }
        else
        {
            ddlBoard.DataSource = null;
            ddlBoard.DataBind();
            lbltotalcount.Text = "0";

            DataList1.DataSource = null;
            DataList1.DataBind();
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtsearchcity.Text = "";
        ddlSearchCountry.SelectedIndex = 0;
        ddlSearchState.SelectedIndex = 0;
    }
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_Error_Success_Box();
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
        string filenamexls1 = "City_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>City(s)</b></TD></TR>");
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
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
        lblHeader_Add.Text = "Create New City";
        ddlCountry.SelectedIndex = 0;
        //ddlState.SelectedIndex = 0;
        ddlState.Items.Clear();

        txtCity.Text = "";
        BtnSaveEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCountry.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Country");
                ddlCountry.Focus();
                return;
            }
            if (ddlState.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select State");
                ddlState.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                Show_Error_Success_Box("E", "Enter City");
                txtCity.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int resultid = 0;


            int ActiveFlag = 0;
            if (chkActive.Checked == true)
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }

            //resultid = ProductController.Insert_Update_City(ddlCountry.SelectedValue, ddlState.SelectedValue, txtCity.Text.Trim(), UserID, ActiveFlag, "1", string.Empty, "");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "City Name already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateCity(resultid.Tables[0].Rows[0]["CityCode"].ToString(), resultid.Tables[0].Rows[0]["City"].ToString(), resultid.Tables[0].Rows[0]["State"].ToString(), resultid.Tables[0].Rows[0]["Country"].ToString(),resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    string City_Code = resultid.Tables[0].Rows[0]["CityCode"].ToString();
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_City(ddlCountry.SelectedValue, ddlState.SelectedValue, txtCity.Text.Trim(), UserID, ActiveFlag, "3", City_Code, Return_Pkey_CRM);

            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";
            //    }
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}


            resultid = ProductController.Insert_Update_City(ddlCountry.SelectedValue, ddlState.SelectedValue, txtCity.Text.Trim(), UserID, ActiveFlag, "1", string.Empty);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "City Name already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();

                return;

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCountry.SelectedValue == "select")
            {
                Show_Error_Success_Box("E", "Select Country");
                ddlCountry.Focus();
                return;
            }
            if (ddlState.SelectedValue == "")
            {
                Show_Error_Success_Box("E", "Select State");
                ddlState.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                Show_Error_Success_Box("E", "Enter City");
                txtCity.Focus();
                return;
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int resultid = 0;


            int ActiveFlag = 0;
            if (chkActive.Checked == true)
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }

            //resultid = ProductController.Insert_Update_City(ddlCountry.SelectedValue, ddlState.SelectedValue, txtCity.Text.Trim(), UserID, ActiveFlag, "2", lblslotid.Text.Trim(), "");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "City Name already Exists!!";
            //    UpdatePanelMsgBox.Update();
            //    return;
            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{
            //    //Msg_Error.Visible = false;
            //    //Msg_Success.Visible = true;
            //    //lblSuccess.Text = "Records Saved Successfully!!";
            //    //UpdatePanelMsgBox.Update();
            //    //return;
            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateCity(resultid.Tables[0].Rows[0]["CityCode"].ToString(), resultid.Tables[0].Rows[0]["City"].ToString(), resultid.Tables[0].Rows[0]["State"].ToString(), resultid.Tables[0].Rows[0]["Country"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_City(ddlCountry.SelectedValue, ddlState.SelectedValue, txtCity.Text.Trim(), UserID, ActiveFlag, "3", lblslotid.Text.Trim(), Return_Pkey_CRM);

            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";
            //    }

            //    UpdatePanelMsgBox.Update();
            //    FillGrid();
            //    return;

            //}

            resultid = ProductController.Insert_Update_City(ddlCountry.SelectedValue, ddlState.SelectedValue, txtCity.Text.Trim(), UserID, ActiveFlag, "2", lblslotid.Text.Trim());

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "City Name already Exists!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (resultid == 1)
            {
                //Msg_Error.Visible = false;
                //Msg_Success.Visible = true;
                //lblSuccess.Text = "Records Saved Successfully!!";
                //UpdatePanelMsgBox.Update();
                //return;
                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();
                FillGrid();
                return;

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_State();
        Clear_Error_Success_Box();
    }
    private void FillDDL_State()
    {
        string Country_Code = null;
        Country_Code = ddlCountry.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlState, dsState, "State_Name", "State_Code");
        ddlState.Items.Insert(0, "Select");
        ddlState.SelectedIndex = 0;
    }
    private void FillDDL_SearchState()
    {
        string Country_Code = null;
        Country_Code = ddlSearchCountry.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlSearchState, dsState, "State_Name", "State_Code");
        ddlSearchState.Items.Insert(0, "Select");
        ddlSearchState.SelectedIndex = 0;
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


            string flags = "2";
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetCity(string.Empty, string.Empty, lblslotid.Text.Trim(), "2");

            if (dsGrid.Tables[0].Rows.Count > 0)
            {

                ddlCountry.SelectedValue = dsGrid.Tables[0].Rows[0]["country_code"].ToString();
                FillDDL_State();
                ddlState.SelectedValue = dsGrid.Tables[0].Rows[0]["State_Code"].ToString();
                txtCity.Text = dsGrid.Tables[0].Rows[0]["city_name"].ToString();



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
                lblHeader_Add.Text = "Edit City Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void ddlSearchCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_SearchState();
        Clear_Error_Success_Box();
    }
    private void FillGrid()
    {

        ControlVisibility("Result");


        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetCity(ddlSearchCountry.SelectedValue, ddlSearchState.SelectedValue, txtsearchcity.Text.Trim(), "1");
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
}