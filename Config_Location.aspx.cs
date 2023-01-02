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

public partial class Config_Location : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            FillDDL_SearchCountry();
            FillDDL_Country();            
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

    private void FillDDL_State()
    {
        string Country_Code = null;
        Country_Code = ddlCountry.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlState, dsState, "State_Name", "State_Code");
        ddlState.Items.Insert(0, "Select");
        ddlState.SelectedIndex = 0;
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
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
        if (ddlsearchcity.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select City");
            ddlsearchcity.Focus();
            return;
        }

        txtlocation.Text = "";
        Clear_Error_Success_Box();
        ControlVisibility("Add");
        BtnSaveEdit.Visible = false;
        //ddlAcademicYear_add.Enabled = true;
        //ddlDivision_add.Enabled = true;
        btnSave.Visible = true;
        lblHeader_Add.Text = "Add Location Details";
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ddlsearchcity.SelectedIndex = 0;
        ddlSearchCountry.SelectedIndex = 0;
        ddlSearchState.SelectedIndex = 0;
        txtSearchLocation.Text = "";
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
       /* Clear_Error_Success_Box();
        ControlVisibility("Result");*/

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
        if (ddlsearchcity.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select City");
            ddlsearchcity.Focus();
            return;
        }

       
        ControlVisibility("Result");


        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetLocation(ddlSearchCountry.SelectedValue, ddlSearchState.SelectedValue, ddlsearchcity.SelectedValue, txtSearchLocation.Text.Trim(), "1");
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Config_Location.aspx");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Location_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Location(s)</b></TD></TR>");
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
        clear_AddPanel();
        ControlVisibility("Search");
        Clear_Error_Success_Box();
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
            if (ddlState.SelectedValue == "select")
            {
                Show_Error_Success_Box("E", "select state");
                ddlState.Focus();
                return;
            }
            if (ddlCity.SelectedValue == "select")
            {
                Show_Error_Success_Box("E", "select city");
                ddlCity.Focus();
                return;
            }
            if (txtlocation.Text == "")
            {
                Show_Error_Success_Box("E", "Enter location Name");
                txtlocation.Focus();
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

            //resultid = ProductController.Insert_Update_Location(lblslotid.Text.Trim(), ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCity.SelectedValue, txtlocation.Text, UserID, ActiveFlag, flag,"");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Location Name already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateLocation(resultid.Tables[0].Rows[0]["LocationCode"].ToString(), resultid.Tables[0].Rows[0]["LocationName"].ToString(), resultid.Tables[0].Rows[0]["Country"].ToString(), resultid.Tables[0].Rows[0]["State"].ToString(), resultid.Tables[0].Rows[0]["City"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    string LocationCode = resultid.Tables[0].Rows[0]["LocationCode"].ToString();
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Location(lblslotid.Text.Trim(), ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCity.SelectedValue, txtlocation.Text, UserID, ActiveFlag, "3",Return_Pkey_CRM);
            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";
            //    }

            //    clear_AddPanel();
            //    UpdatePanelMsgBox.Update();
            //    Fill_Grid();
            //    return;

            //}

            resultid = ProductController.Insert_Update_Location(lblslotid.Text.Trim(), ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCity.SelectedValue, txtlocation.Text, UserID, ActiveFlag, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Location Name already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                clear_AddPanel();
                UpdatePanelMsgBox.Update();
                Fill_Grid();
                return;

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
        
    }

    private void Fill_Grid()
    {
        ControlVisibility("Result");
        string flags = "1";
        
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetLocation(ddlSearchCountry.SelectedValue, ddlSearchState.SelectedValue, ddlsearchcity.SelectedValue, txtSearchLocation.Text, flags);
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
                Show_Error_Success_Box("E", "Select state");
                ddlState.Focus();
                return;
            }
            if (ddlCity.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select City");
                ddlCity.Focus();
                return;
            }
            if (txtlocation.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Location Name");
                txtlocation.Focus();
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

            //resultid = ProductController.Insert_Update_Location(string.Empty, ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCity.SelectedValue, txtlocation.Text.Trim(), UserID, ActiveFlag, flag,"");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Location Name already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateLocation(resultid.Tables[0].Rows[0]["LocationCode"].ToString(), resultid.Tables[0].Rows[0]["LocationName"].ToString(), resultid.Tables[0].Rows[0]["Country"].ToString(), resultid.Tables[0].Rows[0]["State"].ToString(), resultid.Tables[0].Rows[0]["City"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    string LocationCode = resultid.Tables[0].Rows[0]["LocationCode"].ToString();
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Location(LocationCode, ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCity.SelectedValue, txtlocation.Text.Trim(), UserID, ActiveFlag, "3", Return_Pkey_CRM);
            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";
            //    }

            //    UpdatePanelMsgBox.Update();
            //    clear_AddPanel();
            //    Fill_Grid();
            //    return;

            //}

            resultid = ProductController.Insert_Update_Location(string.Empty, ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCity.SelectedValue, txtlocation.Text.Trim(), UserID, ActiveFlag, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Location Name already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();
                clear_AddPanel();
                Fill_Grid();
                return;

            }
            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void clear_AddPanel()
    {
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();

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
    protected void ddlSearchCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_SearchState();
        Clear_Error_Success_Box();
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
    protected void ddlSearchState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_City();
    }
    private void FillDDL_City()
    {
        string Country_Code = null;
        Country_Code = ddlSearchCountry.SelectedValue;

        DataSet dscity = ProductController.GetCity(Country_Code, ddlSearchState.SelectedValue, string.Empty, "1");
        BindDDL(ddlsearchcity, dscity, "city_Name", "city_Code");
        ddlsearchcity.Items.Insert(0, "Select");
        ddlsearchcity.SelectedIndex = 0;
    }
    private void FillDDLCity()
    {
        string Country_Code = null;
        Country_Code = ddlCountry.SelectedValue;

        DataSet dscity = ProductController.GetCity(Country_Code, ddlState.SelectedValue, string.Empty, "1");
        BindDDL(ddlCity, dscity, "city_Name", "city_Code");
        ddlCity.Items.Insert(0, "Select");
        ddlCity.SelectedIndex = 0;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_State();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDLCity();
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
        
            dsGrid = ProductController.GetLocation(string.Empty, string.Empty, string.Empty, PKey, "2");

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
               ddlCountry.SelectedValue  = dsGrid.Tables[0].Rows[0]["country_code"].ToString();
               FillDDL_State();
                ddlState.SelectedValue = dsGrid.Tables[0].Rows[0]["state_code"].ToString();
                FillDDLCity();
                ddlCity.SelectedValue = dsGrid.Tables[0].Rows[0]["City_Code"].ToString();
                txtlocation.Text = dsGrid.Tables[0].Rows[0]["Location_Name"].ToString();


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
                lblHeader_Add.Text = "Edit Board Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }


    private void fill_Search()
    {
         Clear_Error_Success_Box();
        

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
        if (ddlsearchcity.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select City");
            ddlsearchcity.Focus();
            return;
        }


        ControlVisibility("Result");


        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetLocation(ddlSearchCountry.SelectedValue, ddlSearchState.SelectedValue, ddlsearchcity.SelectedValue, txtSearchLocation.Text.Trim(), "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                Clear_Error_Success_Box();
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