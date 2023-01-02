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


public partial class Manage_Concepts : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_Standard();
            
            
              

         
        }
    }


    private void FillDDL_Standard()
    {
        try
        {

            Clear_Error_Success_Box();


            // Div_Code = ddlDivisionAdd.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
            BindDDL(ddladdcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddladdcourse.Items.Insert(0, "Select");
            ddladdcourse.SelectedIndex = 0;

            BindDDL(ddleditcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddleditcourse.Items.Insert(0, "Select");
            ddleditcourse.SelectedIndex = 0;

            BindDDL(ddlsearchcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlsearchcourse.Items.Insert(0, "Select");
            ddlsearchcourse.SelectedIndex = 0;


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

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        //txtconceptname.Text = "";
        //txtconceptdescription.Text = "";
        //txtconceptgenericname.Text = "";
        //chkActive.Checked = true;
        ClearAdd_EditPanel();

        ControlVisibility("Add");
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
            //txtsearchconceptname.Focus();
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
    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        if (ddladdcourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }

        if (ddladdsubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject");
            return;
        }

        if (txtconceptname.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0101");
            return;
        }

        if (txtconceptgenericname.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0102");
            return;
        }

        string conceptname=txtconceptname.Text;
        string conceptdescription=txtconceptdescription.Text;
        string conceptgenericname=txtconceptgenericname.Text;
        string moduleav = txtmoduleav.Text;
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

        string ds1 = ProductController.Insert_Concepts(ddladdcourse.SelectedValue, ddladdsubject.SelectedValue, conceptname.ToUpper(), conceptdescription, conceptgenericname, UserID, ActiveFlag, "", moduleav);

        if (ds1 == "Record Inserted Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");
            txtconceptname.Text = "";
            txtconceptdescription.Text = "";
            txtconceptgenericname.Text = "";
            chkActive.Checked = true;

            string Concept_name = "";
            DataSet dsGrid = ProductController.Get_Concepts(ddladdcourse.SelectedValue,ddladdsubject.SelectedValue, Concept_name);
            if (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                lbltotalcount.Text= dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "0104");
            }
            

        }
        else
        {
            Show_Error_Success_Box("E", "0103");
        }
        
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        FillDDL_Subject_Add(ddlsearchcourse.SelectedValue);
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        if (ddlsearchcourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        
        }

        if (ddlsearchsubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject");
            return;

        }
        
        ControlVisibility("Search");
        string Concept_name = txtsearchconceptname.Text;
        DataSet dsGrid = ProductController.Get_Concepts(ddlsearchcourse.SelectedValue,ddlsearchsubject.SelectedValue, Concept_name);
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

    private void ClearAdd_EditPanel()
    {
        ddladdcourse.SelectedIndex = 0;
        ddleditcourse.SelectedIndex = 0;
        ddladdsubject.Items.Clear();
        ddleditsubject.Items.Clear();
        txtconceptname.Text = "";
        txtconceptdescription.Text = "";
        txtconceptgenericname.Text = "";
        txteditconcepname.Text = "";
        txteditconceptdescription.Text = "";
        txteditconceptgenericname.Text = "";
        txtmoduleav.Text = "";
        txteditmoduleav.Text = "";


        chkActive.Checked = true;


        Chkactiveedit.Checked = true;
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
    }
    protected void HLExport_Click1(object sender, EventArgs e)
    {

        
    }
    protected void HLExport_Click2(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Concepts  " + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Concepts</b></TD></TR>");
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

    protected void dlGridDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
  
    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Edit");
            string record_id = e.CommandArgument.ToString();
            lblPKey_Edit.Text = Convert.ToString(e.CommandArgument);
            
            DataSet dsBatch = ProductController.GET_CONCEPTS_RECORD_ID(record_id);

            if (dsBatch.Tables[0].Rows.Count > 0)
            {
                FillDDL_Subject_Add(Convert.ToString(dsBatch.Tables[0].Rows[0]["Course_Code"]));
                ddleditcourse.SelectedValue = Convert.ToString(dsBatch.Tables[0].Rows[0]["Course_Code"]);
                ddleditsubject.SelectedValue = Convert.ToString(dsBatch.Tables[0].Rows[0]["subject_Code"]);
                txteditconcepname.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["concept_name"]);
                //txteditconcepname.Enabled = false;
                txteditconceptdescription.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["concept_description"]);
                txteditconceptgenericname.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["concept_generic_name"]);
                txteditmoduleav.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["module_av"]);
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

        if (ddleditcourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }


        if (ddleditsubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject");
            return;
        }

        if (txteditconcepname.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0101");
            return;
        }

        if (txteditconceptgenericname.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "0102");
            return;
        }

        string recordid = null;
        recordid = lblPKey_Edit.Text;
        string conceptname = txteditconcepname.Text;
        string conceptdescription = txteditconceptdescription.Text;
        string conceptgenericname = txteditconceptgenericname.Text;
        string moduleav = txteditmoduleav.Text;
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

        string ds1 = ProductController.Edit_Concepts(recordid, ddleditcourse.SelectedValue, ddleditsubject.SelectedValue, conceptname.ToUpper(), conceptdescription, conceptgenericname, UserID, ActiveFlag, moduleav);
        if (ds1 == "Record Updated Sucessfully")
        {
            ControlVisibility("Result");
            Show_Error_Success_Box("S", "0000");

            

            string Concept_name = "";
            DataSet dsGrid = ProductController.Get_Concepts(ddleditcourse.SelectedValue,ddleditsubject.SelectedValue, Concept_name);
            ClearAdd_EditPanel();
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
        else if(ds1=="Duplicate")
        {
            Show_Error_Success_Box("E", "0103");
        }
        
    }
    protected void dlGridExport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnClearSearch_Click1(object sender, EventArgs e)
    {
        ddlsearchcourse.SelectedIndex = 0;
        ddlsearchsubject.Items.Clear();
        txtsearchconceptname.Text = "";
       
    }
    protected void btnedit_cose_Click(object sender, EventArgs e)
    {
         ControlVisibility("Result");
        
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {

        ControlVisibility("Search");
        
    }
    private void FillDDL_Subject_Add(string Course)
    {





        //string StandardCode = null;
        //StandardCode = ddladdcourse.SelectedValue;
        //string standardcodeforsearch = null;
        //standardcodeforsearch = ddleditcourse.SelectedValue;

        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(Course);
        DataSet dsStandarddorsearch = ProductController.GetAllSubjectsByStandard(Course);

        DataSet dsStandarddoredit = ProductController.GetAllSubjectsByStandard(Course);

        BindDDL(ddladdsubject, dsStandard, "Subject_ShortName", "Subject_Code");
        ddladdsubject.Items.Insert(0, "Select");
        ddladdsubject.SelectedIndex = 0;

        BindDDL(ddlsearchsubject, dsStandarddorsearch, "Subject_ShortName", "Subject_Code");
        ddlsearchsubject.Items.Insert(0, "Select");
        ddlsearchsubject.SelectedIndex = 0;

        BindDDL(ddleditsubject, dsStandarddoredit, "Subject_ShortName", "Subject_Code");
        ddleditsubject.Items.Insert(0, "Select");
        ddleditsubject.SelectedIndex = 0;


    }

    protected void ddladdcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddladdcourse.SelectedValue);
    }

    protected void ddleditcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddleditcourse.SelectedValue);
    }
    protected void ddlsearchsubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlsearchcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject_Add(ddlsearchcourse.SelectedValue);
    }
    protected void btnnavigateexcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Concepts_Upload.aspx");
    }
}