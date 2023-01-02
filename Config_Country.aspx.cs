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

public partial class Config_Country : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            
            //string a = ms.CreateCountry("IND", "India");
           

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
        Clear_Error_Success_Box();
        ControlVisibility("Result");
        string flags = "1";
        string var = "";
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetCountry(txtsearchcountry.Text + "%", string.Empty, flags);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = "0";

                dlGridExport.DataSource = null;
                dlGridExport.DataBind();
            }
        }
        else
        {
            dlGridDisplay.DataSource = null;
            dlGridDisplay.DataBind();
            lbltotalcount.Text = "0";

            dlGridExport.DataSource = null;
            dlGridExport.DataBind();
        }

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtsearchcountry.Text = "";
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Search");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        Clear_AddPanel();
        txtcountrycode.Text = "";
        txtcountryname.Text = "";
        txtcountrycode.Enabled = true;
        lblHeader_Add.Text = "Create New Country";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcountrycode.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Country Code");
                txtcountrycode.Focus();
                return;
            }
            if (txtcountryname.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Country Name");
                txtcountryname.Focus();
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

            //resultid = ProductController.Insert_Update_Country(txtcountrycode.Text, txtcountryname.Text, UserID, ActiveFlag, flag,"");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Country Name already Exists!!";
            //    UpdatePanelMsgBox.Update();
            //    return;
            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{
            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;


            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateCountry(resultid.Tables[0].Rows[0]["Countrycode"].ToString(), resultid.Tables[0].Rows[0]["Countryname"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Country(txtcountrycode.Text, txtcountryname.Text, UserID, ActiveFlag, "3", Return_Pkey_CRM);

            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";

            //    }



            resultid = ProductController.Insert_Update_Country(txtcountrycode.Text, txtcountryname.Text, UserID, ActiveFlag, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Country Name already Exists!!";
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

            if (txtcountryname.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Country Name");
                txtcountryname.Focus();
                return;
            }
            //Label lblHeader_User_Code = default(Label);
            //lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            //string CreatedBy = null;
            //CreatedBy = lblHeader_User_Code.Text;

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

            //resultid = ProductController.Insert_Update_Country(txtcountrycode.Text.Trim(), txtcountryname.Text, UserID, ActiveFlag, flag,"");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Country Name already Exists!!";
            //    UpdatePanelMsgBox.Update();
            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;
            //    lblSuccess.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateCountry(resultid.Tables[0].Rows[0]["Countrycode"].ToString(), resultid.Tables[0].Rows[0]["Countryname"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Country(txtcountrycode.Text, txtcountryname.Text, UserID, ActiveFlag, "3", Return_Pkey_CRM);

            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {
            //        lblSuccess.Text = "Records Saved Successfully!!";

            //    }
            //    //string a = ms.CreateCountry("IND", "India");

            //    UpdatePanelMsgBox.Update();
            //    FillGrid();
            //    return;

            //}


            resultid = ProductController.Insert_Update_Country(txtcountrycode.Text.Trim(), txtcountryname.Text, UserID, ActiveFlag, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Country Name already Exists!!";
                UpdatePanelMsgBox.Update();
                return;

            }
            else if (resultid == 1)
            {
                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Visible = true;
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
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Country_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Country(s)</b></TD></TR>");
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
    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "Edit")
        {
            ControlVisibility("Add");

            lblslotid.Text = e.CommandArgument.ToString();
            FillCountryDetails(lblslotid.Text, e.CommandName);
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
         


            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;
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
   
    #endregion
    protected void dlGridExport_ItemCommand(object source, DataListCommandEventArgs e)
    {
       
    }
    private void FillCountryDetails(string PKey, string CommandName)
    {

        try
        {

            string e = "";
            string flags = "2";
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetCountry(string.Empty, lblslotid.Text.Trim(), flags);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtcountrycode.Text = dsGrid.Tables[0].Rows[0]["country_code"].ToString();
                txtcountrycode.Enabled = false;
                txtcountryname.Text = dsGrid.Tables[0].Rows[0]["country_name"].ToString();             


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
                lblHeader_Add.Text = "Edit Country Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    private void FillGrid()
    {
        ControlVisibility("Result");
        try
        {
            
            string countryname = "";
            if (txtcountryname.Text.Trim() == "")
            {
                countryname = "%%";
            }
            else
                countryname = txtcountryname.Text;
            

            DataSet dsGrid = ProductController.GetCountry(txtcountryname.Text.Trim(), string.Empty, "3");
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();

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
        catch (Exception)
        {


        }
    }
}