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

public partial class Config_Division : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
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
        else if (Mode == "TopSearch")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = false;

        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Result");

        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetDivision(string.Empty, txtSearchDivisionName.Text.Trim(), "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlDivision.DataSource = dsGrid;
                dlDivision.DataBind();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();

                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                dlDivision.DataSource = null;
                dlDivision.DataBind();

                DataList1.DataSource = null;
                DataList1.DataBind();

                lbltotalcount.Text = "0";
            }
        }
        else
        {
            dlDivision.DataSource = null;
            dlDivision.DataBind();

            DataList1.DataSource = null;
            DataList1.DataBind();

            lbltotalcount.Text = "0";
        }

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Config_Division.aspx");
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("TopSearch");
        Clear_Error_Success_Box();
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        txtdivisioncode.Text = "";
        txtdivisioncode.Enabled = true;
        txtdivisionlongdesc.Text = "";
        txtdivisionshortdesc.Text = "";

        Clear_Error_Success_Box();
        ControlVisibility("Add");
        BtnSaveEdit.Visible = false;
        //ddlAcademicYear_add.Enabled = true;
        //ddlDivision_add.Enabled = true;
        btnSave.Visible = true;
        lblHeader_Add.Text = "Add Division Details";
    }
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_Error_Success_Box();
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdivisionshortdesc.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Division Name");
                txtdivisionshortdesc.Focus();
                return;
            }

            if (txtdivisionlongdesc.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Description");
                txtdivisionlongdesc.Focus();
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

            //resultid = ProductController.Insert_Update_Division(txtdivisioncode.Text.Trim(), txtdivisionshortdesc.Text.Trim(), txtdivisionlongdesc.Text.Trim(), ActiveFlag, UserID, flag, "");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Division Code already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{


            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateDivision(resultid.Tables[0].Rows[0]["DivisionCode"].ToString(), resultid.Tables[0].Rows[0]["DivisionShortDesc"].ToString(), resultid.Tables[0].Rows[0]["DivisionLongDesc"].ToString(), resultid.Tables[0].Rows[0]["RDV"].ToString(), resultid.Tables[0].Rows[0]["CompanyCode"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());


            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Division(txtdivisioncode.Text.Trim(), txtdivisionshortdesc.Text.Trim(), txtdivisionlongdesc.Text.Trim(), ActiveFlag, UserID, "3", Return_Pkey_CRM);

            //    Fill_Grid();
            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;
            //    lblSuccess.Text = "Records Saved Successfully!!";
            //    UpdatePanelMsgBox.Update();
            //    return;

            //}


            resultid = ProductController.Insert_Update_Division(txtdivisioncode.Text.Trim(), txtdivisionshortdesc.Text.Trim(), txtdivisionlongdesc.Text.Trim(), ActiveFlag, UserID, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Division Code already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {

                Fill_Grid();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdivisioncode.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Division Code");
                txtdivisioncode.Focus();
                return;
            }
            if (txtdivisionshortdesc.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Division Name");
                txtdivisionshortdesc.Focus();
                return;
            }
            if (txtdivisionlongdesc.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Description");
                txtdivisionlongdesc.Focus();
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

            //resultid = ProductController.Insert_Update_Division(txtdivisioncode.Text.Trim(), txtdivisionshortdesc.Text.Trim(), txtdivisionlongdesc.Text.Trim(), ActiveFlag, UserID, flag, "");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Division Code already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateDivision(resultid.Tables[0].Rows[0]["DivisionCode"].ToString(), resultid.Tables[0].Rows[0]["DivisionShortDesc"].ToString(), resultid.Tables[0].Rows[0]["DivisionLongDesc"].ToString(), resultid.Tables[0].Rows[0]["RDV"].ToString(), resultid.Tables[0].Rows[0]["CompanyCode"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());
                
            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Division(txtdivisioncode.Text.Trim(), txtdivisionshortdesc.Text.Trim(), txtdivisionlongdesc.Text.Trim(), ActiveFlag, UserID, "3", Return_Pkey_CRM);

            //    Fill_Grid();
            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;
            //    lblSuccess.Text = "Records Saved Successfully!!";
            //    UpdatePanelMsgBox.Update();
            //    return;

            //}

            resultid = ProductController.Insert_Update_Division(txtdivisioncode.Text.Trim(), txtdivisionshortdesc.Text.Trim(), txtdivisionlongdesc.Text.Trim(), ActiveFlag, UserID, flag);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Division Code already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {



                Fill_Grid();
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Division_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Division</b></TD></TR>");
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
    protected void dlDivision_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");

            lblslotid.Text = e.CommandArgument.ToString();
            FilDivision(lblslotid.Text, e.CommandName);
        }
    }
    private void FilDivision(string PKey, string CommandName)
    {

        try
        {



            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetDivision(lblslotid.Text.Trim(), string.Empty, "2");

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtdivisioncode.Text = dsGrid.Tables[0].Rows[0]["source_division_code"].ToString();
                txtdivisioncode.Enabled = false;
                txtdivisionshortdesc.Text = dsGrid.Tables[0].Rows[0]["source_Division_shortdesc"].ToString();
                txtdivisionlongdesc.Text = dsGrid.Tables[0].Rows[0]["source_Division_longdesc"].ToString();



                if (dsGrid.Tables[0].Rows[0]["status"].ToString() == "1")
                {

                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                btnSave.Visible = false;
                BtnSaveEdit.Visible = true;
                lblHeader_Add.Text = "Edit Division Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
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


    private void Fill_Grid()
    {
        Clear_Error_Success_Box();
        ControlVisibility("Result");

        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetDivision(string.Empty, txtSearchDivisionName.Text.Trim(), "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlDivision.DataSource = dsGrid;
                dlDivision.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                dlDivision.DataSource = null;
                dlDivision.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            dlDivision.DataSource = null;
            dlDivision.DataBind();
            lbltotalcount.Text = "0";
        }

    }


}