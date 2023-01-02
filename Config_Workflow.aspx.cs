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

partial class Config_Workflow : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page_Validation();
                ControlVisibility("Search");
                FillDDL_RequestType();
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;            
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            
        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = true;            
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

    /// <summary>
    /// Clear Search Panel 
    /// </summary>
    private void ClearSearchPanel()
    {
        txtUserName.Text = "";
        txtUserCode.Text = "";
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

    private void FillDDL_RequestType()
    {

        DataSet dsRequestType = ProductController.GetRequestType("1");
        BindDDL(ddlRequestType, dsRequestType, "Request_Type", "Request_Code");
        ddlRequestType.Items.Insert(0, "Select");
        ddlRequestType.SelectedIndex = 0;
    }


    private void FillGrid_UserMaster()
    {
        //Validate if all information is entered correctly
        try
        {
            Clear_Error_Success_Box();
            ControlVisibility("Result");
            string UserName = "", UserCode = "";

            if (txtUserName.Text.Trim() == "")
            {
                UserName = "%%";
            }
            else
                UserName = txtUserName.Text.Trim() + "%";

            if (txtUserCode.Text.Trim() == "")
            {
                UserCode = "%%";
            }
            else
                UserCode = txtUserCode.Text.Trim();

            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetUserByNameOrCode(UserName, UserCode, "1");
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

            //dlGridExport.DataSource = dsGrid;
            //dlGridExport.DataBind();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void SaveData()
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlRequestType.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Request Type");
                ddlRequestType.Focus();
                return;
            }
            if (txtLevelNo.Text.Trim() == "")
            {
                Show_Error_Success_Box("E", "Enter Level No");
                txtLevelNo.Focus();
                return;
            }
            string centreCode = "";
            for (int cnt = 0; cnt <= ddlCentre_add.Items.Count - 1; cnt++)
            {
                if (ddlCentre_add.Items[cnt].Selected == true)
                {
                    centreCode = centreCode + ddlCentre_add.Items[cnt].Value + ",";
                }
            }
            if (centreCode == "")
            {
                Show_Error_Success_Box("E", "Atleast One Centre Should be Selected");
                ddlCentre_add.Focus();
                return;
            }
            centreCode = centreCode.Substring(0, centreCode.Length - 1);
            int result = 0;
            result = ProductController.Insert_Update_RequestLevel(ddlRequestType.SelectedValue, txtLevelNo.Text.Trim(), lblUserCode_Result.Text, centreCode, "1");
            if (result == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();

                //ControlVisibility("Edit");                
                ddlRequestType.SelectedIndex = 0;
                ddlCentre_add.ClearSelection();
                AddButtonRow.Visible = true;
                GridRequestRow.Visible = true;
                RequestAddRow.Visible = false;
                
                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_RequestLevelByUserCode(lblUserCode_Result.Text, "1", "");
                if (dsGrid != null)
                {
                    lblUserName_Result.Text = dsGrid.Tables[0].Rows[0]["User_Display_Name"].ToString();
                    //dlGridRequest
                    dlGridRequest.DataSource = dsGrid.Tables[2];
                    dlGridRequest.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        try
        {
            FillGrid_UserMaster();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
   
    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearchPanel();
    }
    
    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (e.CommandName == "Edit")
            {
                ControlVisibility("Edit");
                lblUserCode_Result.Text = e.CommandArgument.ToString();
                ddlRequestType.SelectedIndex = 0;
                ddlCentre_add.ClearSelection();
                AddButtonRow.Visible = true;
                GridRequestRow.Visible = true;
                RequestAddRow.Visible = false;

                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_RequestLevelByUserCode(lblUserCode_Result.Text, "1","");
                if (dsGrid != null)
                {
                    lblUserName_Result.Text = dsGrid.Tables[0].Rows[0]["User_Display_Name"].ToString();
                    //dlGridRequest
                    dlGridRequest.DataSource = dsGrid.Tables[2];
                    dlGridRequest.DataBind();

                    DataRow[] foundRows;
                    foundRows = dsGrid.Tables[1].Select("Source_Center_Code LIKE '%'", "Source_Center_Code");
                    DataSet dsCentre = new DataSet("table");
                    DataTable CentreTable = dsGrid.Tables[1].Clone();
                    foreach (DataRow dr in foundRows)
                    {
                        CentreTable.ImportRow(dr);
                    }
                    dsCentre.Tables.Add(CentreTable);                    

                    BindListBox(ddlCentre_add, dsCentre, "Source_Center_Name", "Source_Center_Code");      
                }
                
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            AddButtonRow.Visible = false;
            GridRequestRow.Visible = false;
            RequestAddRow.Visible = true;
            ddlRequestType.SelectedIndex = 0;
            ddlCentre_add.ClearSelection();
            txtLevelNo.Text = "";
            lblHeaderRq_Add.Text = "Add New Request Level";
            ddlRequestType.Enabled = true;
            txtLevelNo.Enabled = true;
            btnSave.Visible = true;
            BtnSaveEdit.Visible = false;
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void dlGridRequest_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {

                AddButtonRow.Visible = false;
                GridRequestRow.Visible = false;
                RequestAddRow.Visible = true;
                ddlCentre_add.ClearSelection();
                lblHeaderRq_Add.Text = "Edit Request Level";
                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.Get_RequestLevelByUserCode(lblUserCode_Result.Text, "2",e.CommandArgument.ToString());
                if (dsGrid.Tables[0].Rows.Count > 0)
                {
                    ddlRequestType.SelectedValue = dsGrid.Tables[0].Rows[0]["Request_Code"].ToString();
                    txtLevelNo.Text = dsGrid.Tables[0].Rows[0]["Level_No"].ToString();

                    //Fill selected Centre
                    if (dsGrid.Tables[0].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= dsGrid.Tables[0].Rows.Count - 1; cnt++)
                        {
                            for (int rcnt = 0; rcnt <= ddlCentre_add.Items.Count - 1; rcnt++)
                            {
                                if (ddlCentre_add.Items[rcnt].Value == dsGrid.Tables[0].Rows[cnt]["center_code"].ToString())
                                {
                                    ddlCentre_add.Items[rcnt].Selected = true;
                                    break;
                                }
                            }
                        }
                    }         
                }
                ddlRequestType.Enabled = false;
                txtLevelNo.Enabled = false;
                btnSave.Visible = false;
                BtnSaveEdit.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlRequestType.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Request Type");
            ddlRequestType.Focus();
            return;
        }
        if (txtLevelNo.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Level No");
            txtLevelNo.Focus();
            return;
        }
        string centreCode = "";
        for (int cnt = 0; cnt <= ddlCentre_add.Items.Count - 1; cnt++)
        {
            if (ddlCentre_add.Items[cnt].Selected == true)
            {
                centreCode = centreCode + ddlCentre_add.Items[cnt].Value + ",";
            }
        }
        if (centreCode == "")
        {
            Show_Error_Success_Box("E", "Atleast One Centre Should be Selected");
            ddlCentre_add.Focus();
            return;
        }
        centreCode = centreCode.Substring(0, centreCode.Length - 1);
        int result = 0;
        result = ProductController.Insert_Update_RequestLevel(ddlRequestType.SelectedValue, txtLevelNo.Text.Trim(), lblUserCode_Result.Text, centreCode, "3");
        if (result == 1)
        {
            SaveData();
        }
        else if (result == -1)
        {
            Show_Error_Success_Box("E", "Request Already Exist");            
            return;
        }
    }

    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    public Config_Workflow()
    {
        Load += Page_Load;
    }

    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        AddButtonRow.Visible = true;
        GridRequestRow.Visible = true;
        RequestAddRow.Visible = false;
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "WorkFlow_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Workflow</b></TD></TR>");
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
}
