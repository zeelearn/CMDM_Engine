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
public partial class Manage_DifficultyLevelParameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
           // Page_Validation();
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
            //txtconceptname.Focus();
        }
        else if (Mode == "Edit")
        {
            DivEditPanel.Visible = true;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            //txteditconcepname.Focus();
        }
        Clear_Error_Success_Box();
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        string Parameter_Name = txtsearchparamaetrname.Text;
        DataSet dsGrid = ProductController.Get_DifficultyLevelParamater(Parameter_Name);
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
            Show_Error_Success_Box("E", "0104");
            BtnSearch.Visible = true;
        }
   
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {

        ControlVisibility("Search");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        txtaddparamatername.Text = "";
        txtaddparamatershortdesc.Text = "";
        txtaddparamaterlongdesc.Text = "";

        ControlVisibility("Add");
    }



    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (txtaddparamatername.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Parameter Name");
            return;
        }

        if (txtaddparamatershortdesc.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Parametre Short Desc");
            return;
        }

        if (txtaddparamaterlongdesc.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Parametre Long Desc");
            return;
        }


        string parametername = txtaddparamatername.Text;
        string paramatershortdesc = txtaddparamatershortdesc.Text;
        string paramaterlongdesc = txtaddparamaterlongdesc.Text;
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string ds1 = ProductController.Insert_UpdateDifficultyLevelParameter(1,parametername, paramatershortdesc, paramaterlongdesc, UserID, ActiveFlag,"");
        if (ds1 == "Record Inserted Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            txtaddparamatername.Text = "";
            txtaddparamatershortdesc.Text = "";
            txtaddparamaterlongdesc.Text = "";
            chkActive.Checked = true;

            string Parameter_Name = "";
            DataSet dsGrid = ProductController.Get_DifficultyLevelParamater(Parameter_Name);
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "0104");
            }


        }
        else
        {
            Show_Error_Success_Box("E", "Parameter Name Already Exists");
        }
        

    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Edit");
            string record_id = e.CommandArgument.ToString();
            lblPKey_Edit.Text = Convert.ToString(e.CommandArgument);

            DataSet dsBatch = ProductController.GET_DifficultyLevelParameter_RECORD_ID(record_id);

            if (dsBatch.Tables[0].Rows.Count > 0)
            {

                txteditparametername.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["parameter_name"]);
                txteditparametername.Enabled = false;
                txteditparametershordesc.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["parameter_short_description"]);
                txteditparameterlongdesc.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["parameter_long_description"]);
                int a = Convert.ToInt32(dsBatch.Tables[0].Rows[0]["is_active"]);

                if (a == 1)
                {
                    Chkactiveedit.Checked = true;
                }
                else
                {
                    Chkactiveedit.Checked = false;
                }

            }

        }
    }
    protected void btnedit_save_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (txteditparametername.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Parameter Name");
            return;
        }

        if (txteditparametershordesc.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Parameter Short Desc");
            return;
        }


        if (txteditparameterlongdesc.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Parameter Long Desc");
            return;
        }

        string recordid = null;
        recordid = lblPKey_Edit.Text;
        string parametername = txteditparametername.Text;
        string parametershortdesc = txteditparametershordesc.Text;
        string parameterlongdesc = txteditparameterlongdesc.Text;
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int ActiveFlag = 0;
        if (Chkactiveedit.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string ds1 = ProductController.Insert_UpdateDifficultyLevelParameter(2, parametername, parametershortdesc, parameterlongdesc, UserID, ActiveFlag, lblPKey_Edit.Text);
        if (ds1 == "Record Updated Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            txteditparametername.Text = "";
            txteditparametershordesc.Text = "";
            txteditparameterlongdesc.Text = "";
            Chkactiveedit.Checked = true;

            string Parameter_Name = "";
            DataSet dsGrid = ProductController.Get_DifficultyLevelParamater(Parameter_Name);
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "0104");
            }


        }
        else
        {
            Show_Error_Success_Box("E", "Parameter Name Already Exists");
        }
        

    }
    protected void btnedit_cose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Difficulty Level Parameter  " + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Difficulty Level Parameter</b></TD></TR>");
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtsearchparamaetrname.Text = "";
    }
}