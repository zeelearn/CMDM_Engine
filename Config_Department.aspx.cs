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


public partial class Config_Department : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");


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
            Clear_Error_Success_Box();
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;


            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;


        }
        else if (Mode == "TopSearch")
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
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Add")
        {
            Clear_Error_Success_Box();
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;


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

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        txtaddeditdepartmentname.Text = "";
        txtaddeditdepartmentlongdesc.Text = "";
        BtnSaveAdd.Visible = true;
        BtnSaveEdit.Visible = false;
        chkaddeditisactive.Checked = true;
        lblHeader_Add.Text = "Add Department";
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivAddPanel.Visible = false;
        DivSearchPanel.Visible = true;
        DivResultPanel.Visible = false;
        ControlVisibility("Search");

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtdepartmentname.Text = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {


        DataSet dsGrid = ProductController.GetDepartments(1, txtdepartmentname.Text);
        if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();
            dlGridExport.DataSource = dsGrid;
            dlGridExport.DataBind();
            BtnShowSearchPanel.Visible = true;
            lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Show_Error_Success_Box("E", "No Records Found");
            BtnSearch.Visible = true;
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


    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        if (txtaddeditdepartmentname.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Department Name");
            txtaddeditdepartmentname.Focus();
            return;
        }

        if (txtaddeditdepartmentlongdesc.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Department Long Desc");
            txtaddeditdepartmentlongdesc.Focus();
            return;

        }


        int ActiveFlag;
        if (chkaddeditisactive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string deptname = txtaddeditdepartmentname.Text;
        string deptlongdesc = txtaddeditdepartmentlongdesc.Text;

        string ds1 = ProductController.Insert_Department(2, deptname, deptlongdesc, UserID, ActiveFlag);

        if (ds1 == "Record Inserted")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            DataSet dsGrid = ProductController.GetDepartments(1, "");
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                BtnShowSearchPanel.Visible = true;
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "No Records Found");
                BtnSearch.Visible = true;
            }
        }

        else if (ds1 == "Already Exists") 
        {

            Show_Error_Success_Box("E", "Department Name Alreay Exists");
        }

        else
        {
            Show_Error_Success_Box("E", "Contact Administrator");
        }


    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "comEdit")
        {
            BtnSaveEdit.Visible = true;
            BtnSaveAdd.Visible = false;

            ControlVisibility("Add");
            string record_id = e.CommandArgument.ToString();
            lblHeader_Add.Text = "Edit Department";
            lblprimarykey.Text = Convert.ToString(e.CommandArgument);



            DataSet dsdepartments = ProductController.GetDepartmentDetailsByPk(3,record_id);
            if (dsdepartments.Tables[0].Rows.Count > 0)
            {
                txtaddeditdepartmentname.Text = Convert.ToString(dsdepartments.Tables[0].Rows[0]["DeptName"]);
                txtaddeditdepartmentlongdesc.Text = Convert.ToString(dsdepartments.Tables[0].Rows[0]["DeptLongDesc"]);
                int a = Convert.ToInt32(dsdepartments.Tables[0].Rows[0]["is_active"]);

                if (a == 1)
                {
                    chkaddeditisactive.Checked = true;
                }
                else
                {
                    chkaddeditisactive.Checked = false;
                }
            }
        }
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (txtaddeditdepartmentname.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Department Name");
            txtaddeditdepartmentname.Focus();
            return;
        }

        if (txtaddeditdepartmentlongdesc.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Department Long Desc");
            txtaddeditdepartmentlongdesc.Focus();
            return;

        }


        int ActiveFlag;
        if (chkaddeditisactive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string deptname = txtaddeditdepartmentname.Text;
        string deptlongdesc = txtaddeditdepartmentlongdesc.Text;

        string ds1 = ProductController.Update_Department(4, deptname, deptlongdesc, UserID, ActiveFlag,lblprimarykey.Text);

        if (ds1 == "Record Updated")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            DataSet dsGrid = ProductController.GetDepartments(1, "");
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                BtnShowSearchPanel.Visible = true;
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "No Records Found");
                BtnSearch.Visible = true;
            }
        }

        else if (ds1 == "Already Exists")
        {

            Show_Error_Success_Box("E", "Department Name Alreay Exists");
        }

        else
        {
            Show_Error_Success_Box("E", "Contact Administrator");
        }


    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        if (lblHeader_Add.Text == "Add Department")
        { 
        ControlVisibility("Search");
        }
        if (lblHeader_Add.Text == "Edit Department")
        {
            ControlVisibility("Result");
        }
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Departments  " + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Departments</b></TD></TR>");
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
}