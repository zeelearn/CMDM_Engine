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
            //Page_Validation();
            FillDDL_Division();
            Fill_Message("");
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
        fill_grid_message();

    }

    private void fill_grid_message()
    {
        Clear_Error_Success_Box();
        ControlVisibility("Result");


        DataSet dsGrid = new DataSet();
        if (ddlMessageName_SR.SelectedIndex == 0 && ddlDivision_SR.SelectedIndex == 0)
        {
            dsGrid = ProductController.GetMessageSetup_SearchField("","", 4);
        }
        else
        {
            dsGrid = ProductController.GetMessageSetup_SearchField(ddlMessageName_SR.SelectedValue.ToString().Trim(), ddlDivision_SR.SelectedValue.ToString().Trim(), 4);
        }

        
        
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

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision_SR.SelectedIndex = 0;
        ddlMessageName_SR.SelectedIndex = 0;
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("TopSearch");
        Clear_Error_Success_Box();
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
        ddlDivision_Add.Enabled = true;
        ddlMessage.Enabled = true;
        Clear_Field();
        BtnSaveEdit.Visible = false;
        btnSave.Visible = true;
        lblHeader_Add.Text = "Add Message Template";
    }

    private void Clear_Field()
    {
        ddlDivision_Add.SelectedIndex = 0;
        ddlMessageType.SelectedIndex = 0;
        ddlMessage.Items.Clear();
        ddlSendingType.SelectedIndex = 0;
        TextArea1.Value = "";
        lblslotid.Text = "";
        lbldelCode.Text = "";

        DataList2.DataSource = null;
        DataList2.DataBind();
    }

    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        Clear_Field();
        ControlVisibility("Result");
        fill_grid_message();
        Clear_Error_Success_Box();
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (ddlDivision_Add.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision_Add.Focus();
            return;
        }
        if (ddlMessageType.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select MessageType");
            ddlMessageType.Focus();
            return;
        }
        if (ddlMessage.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Message");
            ddlMessage.Focus();
            return;
        }
        if (ddlSendingType.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Sending Type");
            ddlSendingType.Focus();
            return;
        }

        if (TextArea1.Value == "")
        {
            Show_Error_Success_Box("E", "Message Template");
            TextArea1.Focus();
            return;
        }

        int resultid = 0;

        string template = TextArea1.Value.Trim();
        string newtemp = template.Replace("&", "%26").Replace("+", "%2D").Replace("%", "%25").Replace("#", "%23").Replace("=", "%3D").Replace("^", "%5E").Replace("~", "%7E");

        resultid = ProductController.Insert_Update_MessageTemplate_Edit(ddlMessage.SelectedValue.ToString().Trim(), ddlDivision_Add.SelectedValue.ToString().Trim(), ddlSendingType.SelectedValue.ToString().Trim(), newtemp, 6, lbldelCode.Text);

        if (resultid == 2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Record Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Clear_Field();
            fill_grid_message();
            return;

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
        
        if (ddlDivision_Add.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision_Add.Focus();
            return;
        }
        if (ddlMessageType.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select MessageType");
            ddlMessageType.Focus();
            return;
        }
        if (ddlMessage.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Message");
            ddlMessage.Focus();
            return;
        }
        if (ddlSendingType.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Sending Type");
            ddlSendingType.Focus();
            return;
        }

        if (TextArea1.Value == "")
        {
            Show_Error_Success_Box("E", "Message Template");
            TextArea1.Focus();
            return;
        }
        

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;

        string template = TextArea1.Value.Trim();
        string newtemp = template.Replace("&", "%26").Replace("+", "%2D").Replace("%", "%25").Replace("#", "%23").Replace("=", "%3D").Replace("^", "%5E").Replace("~", "%7E");

        resultid = ProductController.Insert_Update_MessageTemplate(ddlMessage.SelectedValue.ToString().Trim(), ddlDivision_Add.SelectedValue.ToString().Trim(), ddlSendingType.SelectedValue.ToString().Trim(), newtemp, 3);

        if (resultid == 2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Record Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Clear_Field();
            fill_grid_message();
            return;

        }
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString ();
            UpdatePanelMsgBox.Update();

            return;
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        
    }

    protected void dlDivision_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");

            lbldelCode.Text = e.CommandArgument.ToString();
            FilDivision(lbldelCode.Text);
            ddlDivision_Add.Enabled = false;
            ddlMessage.Enabled = false;

        }
    }


    private void FilDivision(string PKey)
    {

        try
        {

            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetMessage_For_Edit(PKey, 5);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                FillDDL_Division();
                ddlDivision_Add.SelectedValue = dsGrid.Tables[0].Rows[0]["division_code"].ToString();
                ddlMessageType.SelectedValue = dsGrid.Tables[0].Rows[0]["Message_Type"].ToString();
                Fill_Message(ddlMessageType.SelectedValue.ToString().Trim());
                ddlMessage.SelectedValue = dsGrid.Tables[0].Rows[0]["Message_Code"].ToString();
                ddlSendingType.SelectedValue = dsGrid.Tables[0].Rows[0]["Send_Type"].ToString();
                Fill_KeyWordGgrid(ddlMessage.SelectedValue);
                TextArea1.Value = dsGrid.Tables[0].Rows[0]["Message_Template"].ToString();

                btnSave.Visible = false;
                BtnSaveEdit.Visible = true;
                lblHeader_Add.Text = "Edit Message Template";

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


    

    protected void ddlMessage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_KeyWordGgrid(ddlMessage.SelectedValue);
    }

    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            ddlDivision_SR.Items.Clear();
            ddlDivision_Add.Items.Clear();
            string Company_Code = "MT";
            string DBname = "CDB";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
            BindDDL(ddlDivision_SR, dsDivision, "Division_Name", "Division_Code");
            ddlDivision_SR.Items.Insert(0, "Select");
            ddlDivision_SR.SelectedIndex = 0;

            BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
            ddlDivision_Add.Items.Insert(0, "Select");
            ddlDivision_Add.SelectedIndex = 0;


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
    protected void ddlMessageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Fill_Message(ddlMessageType .SelectedValue);
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

    private void Fill_Message(string Message_Type)
    {
        DataSet dsMessage = ProductController.GetAllMessage_ByMsgType(Message_Type, 1);
        BindDDL(ddlMessage, dsMessage, "Message_Name", "Message_Code");
        ddlMessage.Items.Insert(0, "Select");
        ddlMessage.SelectedIndex = 0;
        
        BindDDL(ddlMessageName_SR, dsMessage, "Message_Name", "Message_Code");
        ddlMessageName_SR.Items.Insert(0, "Select");
        ddlMessageName_SR.SelectedIndex = 0;
    }

    private void Fill_KeyWordGgrid(string Message_code)
    {
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetAllTemplateKeyword_ByMsgCode(Message_code, 2);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                DataList2.DataSource = dsGrid;
                DataList2.DataBind();

            }
            else
            {

                DataList2.DataSource = null;
                DataList2.DataBind();

            }
        }
        else
        {
            DataList2.DataSource = null;
            DataList2.DataBind();

        }
    }
    protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
           lblslotid.Text = e.CommandArgument.ToString();
           //System.Web.UI.HtmlControls.HtmlTextArea textare = 
           //HtmlTextArea form = FindControl("form-field-8");
           //TextBox1.Text = TextBox1 .Text + e.CommandArgument.ToString();
           TextArea1.Value = TextArea1.Value + e.CommandArgument.ToString();
           UpdatePanel3.Update();
        }
    }
}