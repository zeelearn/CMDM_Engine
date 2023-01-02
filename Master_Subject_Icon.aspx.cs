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

public partial class Master_Subject_Icon : System.Web.UI.Page
{
     protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            
            FillGrid_Chapter();
        }
    }

    //private void Page_Validation()
    //{
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    string UserID = cookie.Values["UserID"];
    //    string UserName = cookie.Values["UserName"];

    //    string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
    //    System.IO.FileInfo Info = new System.IO.FileInfo(Path);
    //    string pageName = Info.Name;

    //    int ResultId = 0;

    //    ResultId = ProductController.Chk_Page_Validation(pageName, UserID, "DB00");

    //    if (ResultId >= 1)
    //    {
    //        //Allow
    //    }
    //    else
    //    {
    //        Response.Redirect("~/Homepage.aspx", false);
    //    }

    //}

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            //DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

  
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillGrid_Chapter()
    {
        //Validate if all information is entered correctly
  

        ControlVisibility("Result");
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

        DataSet dsGrid = ProductController.GetAllSubjectfor_icon(Session["lblCourse_Code.Text"].ToString(), "1");

        //Copy dsGrid content from DataSet to DataTable
        DataTable dtGrid = null;
        dtGrid = dsGrid.Tables[0];

        //Add 1 Blank records
        //dtGrid.Rows.Add("", "", "","", "",0, 0, 0,"", 0,0, 1, 1);
        dtGrid.Rows.Add("","", "","", 0, 1, 1);
        dlGridDisplay.DataSource = dtGrid;

        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        //lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        //lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        //lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();

        lbltotalcount.Text = Convert.ToString(Convert.ToInt16(dsGrid.Tables[0].Rows.Count.ToString()) - 1);


    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid_Chapter();
    }

    

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        //ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        TextBox txtDLSubiconname = (TextBox)e.Item.FindControl("txtDLSubiconname");
        TextBox txtDLsubicon = (TextBox)e.Item.FindControl("txtDLsubicon");
        TextBox txtDLsubfont = (TextBox)e.Item.FindControl("txtDLsubfont");
    
        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

        Label lblDLsubiconname = (Label)e.Item.FindControl("lblDLsubiconname");
        Label lblDLsubicon = (Label)e.Item.FindControl("lblDLsubicon");
        Label lblDLsubfont = (Label)e.Item.FindControl("lblDLsubfont");
        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

        if (e.CommandName == "Edit")
        {
            txtDLSubiconname.Visible = true;
            txtDLsubicon.Visible = true;
            txtDLsubfont.Visible = true;


            lblDLsubiconname.Visible = false;
            lblDLsubicon.Visible = false;
            lblDLsubfont.Visible = false;
           

            lnkDLEdit.Visible = false;
            lnkDLSave.Visible = true;
            icon_Error.Visible = false;

            txtDLSubiconname.Focus();
        }
        else if (e.CommandName == "Save")
        {
            //Validation
            if (string.IsNullOrEmpty(txtDLSubiconname.Text))
            {
                lbl_DLError.Title = "Enter Subject Icon Name";
                icon_Error.Visible = true;
                txtDLSubiconname.Focus();
                return;
            }

            //Check if lecture count is a numeric value
            if (string.IsNullOrEmpty(txtDLsubicon.Text))
            {
                lbl_DLError.Title = "Add subject Icon";
                icon_Error.Visible = true;
                txtDLsubicon.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtDLsubfont.Text))
            {

                lbl_DLError.Title = "Add Subject Font";
                icon_Error.Visible = true;
                txtDLsubfont.Focus();
                return;
            }
            

            //Saving part
          string YearName = null;
            YearName = "";

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            string CreatedBy = null;
            CreatedBy = UserID;

            string ChapterCodeForEdit = null;
            ChapterCodeForEdit = e.CommandArgument.ToString();

           

            int ResultId = 0;
            //Mark exemption/absent/present for those students who are selected
            ResultId = ProductController.Insert_subject_icon(Session["lblCourse_Code.Text"].ToString(), txtDLSubiconname.Text, txtDLsubicon.Text, txtDLsubfont.Text, ChapterCodeForEdit, CreatedBy);

            if (ResultId == -1)
            {
                lbl_DLError.Title = "Duplicate chapter name or code";
                icon_Error.Visible = true;
                return;
            }
            else
            {
                icon_Error.Visible = false;
            }

            //Change look
            txtDLSubiconname.Visible = false;
            txtDLsubicon.Visible = false;
            txtDLsubfont.Visible = false;
           


            lblDLsubiconname.Visible = true;
            lblDLsubicon.Visible = true;
            lblDLsubfont.Visible = true;
           


            lnkDLEdit.Visible = true;
            lnkDLSave.Visible = false;

            FillGrid_Chapter();
        }

    }

    


    private bool IsNumeric(object value)
    {
        try
        {
            int i = Convert.ToInt32(value.ToString());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected void dlGridDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}