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

partial class OE_Configration : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            FillDDL_ConfigTable();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
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

    /// <summary>
    /// Clear Search Panel 
    /// </summary>
    private void ClearSearchPanel()
    {
        ddlConfigTable.SelectedIndex = 0;           
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

    private void FillDDL_ConfigTable()
    {       
        DataSet dsConfigTable = new DataSet();
        dsConfigTable = ProductController.GetConfigTable("1","");
        BindDDL(ddlConfigTable, dsConfigTable, "TableDisplayName", "Table_ID");
        ddlConfigTable.Items.Insert(0, "Select");
        ddlConfigTable.SelectedIndex = 0;
    }

    private void FillGrid()
    {
        try
        {
            ControlVisibility("Result");
            DataSet dsConfigTable = new DataSet();
            dsConfigTable = ProductController.GetConfigTable("2", ddlConfigTable.SelectedValue);

            DataTable dtGrid = null;
            dtGrid = dsConfigTable.Tables[0];

            if (ddlConfigTable.SelectedValue == "009")   //Score Range Table  
            {
                dlGridDisplay.Visible = false;
                dlGridDisplay1.Visible = true;
                dlGridDisplay2.Visible = false;

                //Add 1 Blank records        
                dtGrid.Rows.Add("", "", "", 1, 0, 1, 1);
                dlGridDisplay1.DataSource = dtGrid;

                dlGridDisplay1.DataSource = dsConfigTable;
                dlGridDisplay1.DataBind();
            }
            else if (ddlConfigTable.SelectedValue == "020")   // Sales_Stage Table  
            {
                dlGridDisplay.Visible = false;
                dlGridDisplay1.Visible = false;
                dlGridDisplay2.Visible = true;

                //Add 1 Blank records        
                dtGrid.Rows.Add("", "", "", 1, 0, 1, 1);
                dlGridDisplay2.DataSource = dtGrid;

                dlGridDisplay2.DataSource = dsConfigTable;
                dlGridDisplay2.DataBind();
            }
            else
            {
                dlGridDisplay.Visible = true;
                dlGridDisplay1.Visible = false;
                dlGridDisplay2.Visible = false;
                //Add 1 Blank records        
                dtGrid.Rows.Add("", "", 1, 0, 1, 1);
                dlGridDisplay.DataSource = dtGrid;

                dlGridDisplay.DataSource = dsConfigTable;
                dlGridDisplay.DataBind();

                //dlGridExport.DataSource = dsConfigTable;
                //dlGridExport.DataBind();
            }


            lbltotalcount.Text = Convert.ToString(Convert.ToInt16(dsConfigTable.Tables[0].Rows.Count.ToString()) - 1);
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        if (ddlConfigTable.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Config Name");
            ddlConfigTable.Focus();
            return;
        }
        
        FillGrid();
        lblConfigName_Result.Text = ddlConfigTable.SelectedItem.ToString();
    }
    
    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearchPanel();
    }

    //protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    //{
    //    try
    //    {
    //        TextBox txtDLID = (TextBox)e.Item.FindControl("txtDLID");
    //        TextBox txtDLMediumName = (TextBox)e.Item.FindControl("txtDLMediumName");
    //        TextBox txtDLDescription = (TextBox)e.Item.FindControl("txtDLDescription");
    //        System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");

    //        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

    //        Label lblDLID = (Label)e.Item.FindControl("lblDLID");
    //        Label lblDLMediumName = (Label)e.Item.FindControl("lblDLMediumName");
    //        Label lblDLDescription = (Label)e.Item.FindControl("lblDLDescription");
    //        Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");

    //        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
    //        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

    //        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

    //        if (e.CommandName == "Edit")
    //        {
    //            txtDLID.Visible = true;
    //            //txtDLMediumName.Visible = true;
    //            txtDLDescription.Visible = true;
    //            chkActiveFlag.Visible = true;

    //            lblDLID.Visible = false;
    //            //lblDLMediumName.Visible = false;
    //            lblDLDescription.Visible = false;
    //            lblDLStatus.Visible = false;

    //            lnkDLEdit.Visible = false;
    //            lnkDLSave.Visible = true;
    //            icon_Error.Visible = false;

    //            txtDLDescription.Focus();
    //        }
    //        else if (e.CommandName == "Save")
    //        {
    //            //Validation
    //            if (string.IsNullOrEmpty(txtDLDescription.Text.Trim()))
    //            {
    //                lbl_DLError.Title = "Enter Description";
    //                icon_Error.Visible = true;
    //                txtDLDescription.Focus();
    //                return;
    //            }

    //            string ActiveFlag = "0";
    //            if (chkActiveFlag.Checked == true)
    //                ActiveFlag = "1";
    //            else
    //                ActiveFlag = "0";

    //            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //            string UserID = cookie.Values["UserID"];

    //            DataSet ResultId = null;
    //            ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, txtDLID.Text, txtDLDescription.Text, ActiveFlag, UserID, "", "","");
    //            if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
    //            {
    //                lbl_DLError.Title = "Duplicate Record";
    //                icon_Error.Visible = true;
    //                txtDLDescription.Focus();
    //                return;
    //            }
    //            else
    //            {
    //                if (ddlConfigTable.SelectedValue == "012")
    //                {
    //                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //                    string Return_Pkey_CRM = ms.CreateInstitute(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
    //                    string Pkey = ResultId.Tables[0].Rows[0]["Id"].ToString();
    //                    ResultId = null;
    //                    ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, Pkey, txtDLDescription.Text, ActiveFlag, UserID, "", "", Return_Pkey_CRM);
    //                }



    //                else if (ddlConfigTable.SelectedValue == "017")
    //                {
    //                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //                    string Return_Pkey_CRM = ms.CreateContactSource(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
    //                    string Pkey = ResultId.Tables[0].Rows[0]["Id"].ToString();
    //                    ResultId = null;
    //                    ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, Pkey, txtDLDescription.Text, ActiveFlag, UserID, "", "", Return_Pkey_CRM);
    //                }


    //                else if (ddlConfigTable.SelectedValue == "022")
    //                {
    //                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //                    string Return_Pkey_CRM = ms.CreateCustomerType(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
    //                    string Pkey = ResultId.Tables[0].Rows[0]["Id"].ToString();
    //                    ResultId = null;
    //                    ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, Pkey, txtDLDescription.Text, ActiveFlag, UserID, "", "", Return_Pkey_CRM);
    //                }

    //                else if (ddlConfigTable.SelectedValue == "029")
    //                {
    //                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //                    string Return_Pkey_CRM = ms.CreateOrderStatus(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
    //                    string Pkey = ResultId.Tables[0].Rows[0]["Id"].ToString();
    //                    ResultId = null;
    //                    ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, Pkey, txtDLDescription.Text, ActiveFlag, UserID, "", "", Return_Pkey_CRM);
    //                }

    //                else if (ddlConfigTable.SelectedValue == "030")
    //                {
    //                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
    //                    string Return_Pkey_CRM = ms.CreateOrderSource(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
    //                    string Pkey = ResultId.Tables[0].Rows[0]["Id"].ToString();
    //                    ResultId = null;
    //                    ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, Pkey, txtDLDescription.Text, ActiveFlag, UserID, "", "", Return_Pkey_CRM);
    //                }

    //                icon_Error.Visible = false;
    //            }

    //            //Change look
    //            txtDLID.Visible = false;
    //            txtDLDescription.Visible = false;
    //            chkActiveFlag.Visible = false;

    //            lblDLID.Visible = true;
    //            lblDLDescription.Visible = true;
    //            lblDLStatus.Visible = true;

    //            lnkDLEdit.Visible = true;
    //            lnkDLSave.Visible = false;

    //            FillGrid();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Show_Error_Success_Box("E", ex.ToString());
    //    }
    //}


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {
            TextBox txtDLID = (TextBox)e.Item.FindControl("txtDLID");
            TextBox txtDLMediumName = (TextBox)e.Item.FindControl("txtDLMediumName");
            TextBox txtDLDescription = (TextBox)e.Item.FindControl("txtDLDescription");
            System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");

            HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

            Label lblDLID = (Label)e.Item.FindControl("lblDLID");
            Label lblDLMediumName = (Label)e.Item.FindControl("lblDLMediumName");
            Label lblDLDescription = (Label)e.Item.FindControl("lblDLDescription");
            Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");

            LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
            LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

            Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

            if (e.CommandName == "Edit")
            {
                txtDLID.Visible = true;
                //txtDLMediumName.Visible = true;
                txtDLDescription.Visible = true;
                chkActiveFlag.Visible = true;

                lblDLID.Visible = false;
                //lblDLMediumName.Visible = false;
                lblDLDescription.Visible = false;
                lblDLStatus.Visible = false;

                lnkDLEdit.Visible = false;
                lnkDLSave.Visible = true;
                icon_Error.Visible = false;

                txtDLDescription.Focus();
            }
            else if (e.CommandName == "Save")
            {
                //Validation
                if (string.IsNullOrEmpty(txtDLDescription.Text.Trim()))
                {
                    lbl_DLError.Title = "Enter Description";
                    icon_Error.Visible = true;
                    txtDLDescription.Focus();
                    return;
                }

                string ActiveFlag = "0";
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = "1";
                else
                    ActiveFlag = "0";

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];

                int ResultId = 0;
                ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, txtDLID.Text, txtDLDescription.Text, ActiveFlag, UserID, "", "");
                if (ResultId == -1)
                {
                    lbl_DLError.Title = "Duplicate Record";
                    icon_Error.Visible = true;
                    txtDLDescription.Focus();
                    return;
                }
                else
                {
                    icon_Error.Visible = false;
                }

                //Change look
                txtDLID.Visible = false;
                txtDLDescription.Visible = false;
                chkActiveFlag.Visible = false;

                lblDLID.Visible = true;
                lblDLDescription.Visible = true;
                lblDLStatus.Visible = true;

                lnkDLEdit.Visible = true;
                lnkDLSave.Visible = false;

                FillGrid();

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }


    //protected void dlGridDisplay1_ItemCommand(object source, DataListCommandEventArgs e)
    //{
    //    try
    //    {
    //        TextBox txtDLID = (TextBox)e.Item.FindControl("txtDLID");
    //        TextBox txtDLShortDesc = (TextBox)e.Item.FindControl("txtDLShortDesc");
    //        TextBox txtDLLongDesc = (TextBox)e.Item.FindControl("txtDLLongDesc");
    //        System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");

    //        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

    //        Label lblDLID = (Label)e.Item.FindControl("lblDLID");
    //        Label lblDLShortDesc = (Label)e.Item.FindControl("lblDLShortDesc");
    //        Label lblDLLongDesc = (Label)e.Item.FindControl("lblDLLongDesc");
    //        Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");

    //        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
    //        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

    //        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

    //        if (e.CommandName == "Edit")
    //        {
    //            txtDLID.Visible = true;
    //            txtDLShortDesc.Visible = true;
    //            txtDLLongDesc.Visible = true;
    //            chkActiveFlag.Visible = true;

    //            lblDLID.Visible = false;
    //            lblDLShortDesc.Visible = false;
    //            lblDLLongDesc.Visible = false;
    //            lblDLStatus.Visible = false;

    //            lnkDLEdit.Visible = false;
    //            lnkDLSave.Visible = true;
    //            icon_Error.Visible = false;

    //            txtDLShortDesc.Focus();
    //        }
    //        else if (e.CommandName == "Save")
    //        {
    //            //Validation
    //            if (string.IsNullOrEmpty(txtDLShortDesc.Text.Trim()))
    //            {
    //                lbl_DLError.Title = "Enter Short Description";
    //                icon_Error.Visible = true;
    //                txtDLShortDesc.Focus();
    //                return;
    //            }
    //            if (string.IsNullOrEmpty(txtDLLongDesc.Text.Trim()))
    //            {
    //                lbl_DLError.Title = "Enter Long Description";
    //                icon_Error.Visible = true;
    //                txtDLLongDesc.Focus();
    //                return;
    //            }
    //            string ActiveFlag = "0";
    //            if (chkActiveFlag.Checked == true)
    //                ActiveFlag = "1";
    //            else
    //                ActiveFlag = "0";

    //            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //            string UserID = cookie.Values["UserID"];

    //            DataSet ResultId = null;
    //            ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, txtDLID.Text, txtDLLongDesc.Text, ActiveFlag, UserID, txtDLShortDesc.Text, "","");
    //            if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
    //            {
    //                lbl_DLError.Title = "Duplicate Record";
    //                icon_Error.Visible = true;
    //                txtDLShortDesc.Focus();
    //                return;
    //            }
    //            else
    //            {
    //                icon_Error.Visible = false;
    //            }

    //            //Change look
    //            txtDLID.Visible = false;
    //            txtDLShortDesc.Visible = false;
    //            txtDLLongDesc.Visible = false;
    //            chkActiveFlag.Visible = false;

    //            lblDLID.Visible = true;
    //            lblDLShortDesc.Visible = true;
    //            lblDLLongDesc.Visible = true;
    //            lblDLStatus.Visible = true;

    //            lnkDLEdit.Visible = true;
    //            lnkDLSave.Visible = false;

    //            FillGrid();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Show_Error_Success_Box("E", ex.ToString());
    //    }
    //}

    //protected void dlGridDisplay2_ItemCommand(object source, DataListCommandEventArgs e)
    //{
    //    try
    //    {
    //        TextBox txtDLID = (TextBox)e.Item.FindControl("txtDLID");
    //        TextBox txtDLDesc = (TextBox)e.Item.FindControl("txtDLDesc");
    //        TextBox txtDLPerc = (TextBox)e.Item.FindControl("txtDLPerc");
    //        System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");

    //        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

    //        Label lblDLID = (Label)e.Item.FindControl("lblDLID");
    //        Label lblDLDesc = (Label)e.Item.FindControl("lblDLDesc");
    //        Label lblDLPerc = (Label)e.Item.FindControl("lblDLPerc");
    //        Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");

    //        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
    //        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

    //        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

    //        if (e.CommandName == "Edit")
    //        {
    //            txtDLID.Visible = true;
    //            txtDLDesc.Visible = true;
    //            txtDLPerc.Visible = true;
    //            chkActiveFlag.Visible = true;

    //            lblDLID.Visible = false;
    //            lblDLDesc.Visible = false;
    //            lblDLPerc.Visible = false;
    //            lblDLStatus.Visible = false;

    //            lnkDLEdit.Visible = false;
    //            lnkDLSave.Visible = true;
    //            icon_Error.Visible = false;

    //            txtDLDesc.Focus();
    //        }
    //        else if (e.CommandName == "Save")
    //        {
    //            //Validation
    //            if (string.IsNullOrEmpty(txtDLDesc.Text.Trim()))
    //            {
    //                lbl_DLError.Title = "Enter Description";
    //                icon_Error.Visible = true;
    //                txtDLDesc.Focus();
    //                return;
    //            }
    //            if (string.IsNullOrEmpty(txtDLPerc.Text.Trim()))
    //            {
    //                lbl_DLError.Title = "Enter Percentage";
    //                icon_Error.Visible = true;
    //                txtDLPerc.Focus();
    //                return;
    //            }
    //            string ActiveFlag = "0";
    //            if (chkActiveFlag.Checked == true)
    //                ActiveFlag = "1";
    //            else
    //                ActiveFlag = "0";

    //            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //            string UserID = cookie.Values["UserID"];

    //            DataSet ResultId = null;
    //            ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, txtDLID.Text, txtDLDesc.Text, ActiveFlag, UserID, "", txtDLPerc.Text.Trim(),"");
    //            if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
    //            {
    //                lbl_DLError.Title = "Duplicate Record";
    //                icon_Error.Visible = true;
    //                txtDLDesc.Focus();
    //                return;
    //            }
    //            else
    //            {
    //                icon_Error.Visible = false;
    //            }

    //            //Change look
    //            txtDLID.Visible = false;
    //            txtDLDesc.Visible = false;
    //            txtDLPerc.Visible = false;
    //            chkActiveFlag.Visible = false;

    //            lblDLID.Visible = true;
    //            lblDLDesc.Visible = true;
    //            lblDLPerc.Visible = true;
    //            lblDLStatus.Visible = true;

    //            lnkDLEdit.Visible = true;
    //            lnkDLSave.Visible = false;

    //            FillGrid();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Show_Error_Success_Box("E", ex.ToString());
    //    }
    //}


    protected void dlGridDisplay1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            TextBox txtDLID = (TextBox)e.Item.FindControl("txtDLID");
            TextBox txtDLShortDesc = (TextBox)e.Item.FindControl("txtDLShortDesc");
            TextBox txtDLLongDesc = (TextBox)e.Item.FindControl("txtDLLongDesc");
            System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");

            HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

            Label lblDLID = (Label)e.Item.FindControl("lblDLID");
            Label lblDLShortDesc = (Label)e.Item.FindControl("lblDLShortDesc");
            Label lblDLLongDesc = (Label)e.Item.FindControl("lblDLLongDesc");
            Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");

            LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
            LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

            Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

            if (e.CommandName == "Edit")
            {
                txtDLID.Visible = true;
                txtDLShortDesc.Visible = true;
                txtDLLongDesc.Visible = true;
                chkActiveFlag.Visible = true;

                lblDLID.Visible = false;
                lblDLShortDesc.Visible = false;
                lblDLLongDesc.Visible = false;
                lblDLStatus.Visible = false;

                lnkDLEdit.Visible = false;
                lnkDLSave.Visible = true;
                icon_Error.Visible = false;

                txtDLShortDesc.Focus();
            }
            else if (e.CommandName == "Save")
            {
                //Validation
                if (string.IsNullOrEmpty(txtDLShortDesc.Text.Trim()))
                {
                    lbl_DLError.Title = "Enter Short Description";
                    icon_Error.Visible = true;
                    txtDLShortDesc.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDLLongDesc.Text.Trim()))
                {
                    lbl_DLError.Title = "Enter Long Description";
                    icon_Error.Visible = true;
                    txtDLLongDesc.Focus();
                    return;
                }
                string ActiveFlag = "0";
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = "1";
                else
                    ActiveFlag = "0";

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];

                int ResultId = 0;
                ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, txtDLID.Text, txtDLLongDesc.Text, ActiveFlag, UserID, txtDLShortDesc.Text, "");
                if (ResultId == -1)
                {
                    lbl_DLError.Title = "Duplicate Record";
                    icon_Error.Visible = true;
                    txtDLShortDesc.Focus();
                    return;
                }
                else
                {
                    icon_Error.Visible = false;
                }

                //Change look
                txtDLID.Visible = false;
                txtDLShortDesc.Visible = false;
                txtDLLongDesc.Visible = false;
                chkActiveFlag.Visible = false;

                lblDLID.Visible = true;
                lblDLShortDesc.Visible = true;
                lblDLLongDesc.Visible = true;
                lblDLStatus.Visible = true;

                lnkDLEdit.Visible = true;
                lnkDLSave.Visible = false;

                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void dlGridDisplay2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            TextBox txtDLID = (TextBox)e.Item.FindControl("txtDLID");
            TextBox txtDLDesc = (TextBox)e.Item.FindControl("txtDLDesc");
            TextBox txtDLPerc = (TextBox)e.Item.FindControl("txtDLPerc");
            System.Web.UI.HtmlControls.HtmlInputCheckBox chkActiveFlag = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkActiveFlag");

            HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

            Label lblDLID = (Label)e.Item.FindControl("lblDLID");
            Label lblDLDesc = (Label)e.Item.FindControl("lblDLDesc");
            Label lblDLPerc = (Label)e.Item.FindControl("lblDLPerc");
            Label lblDLStatus = (Label)e.Item.FindControl("lblDLStatus");

            LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
            LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

            Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

            if (e.CommandName == "Edit")
            {
                txtDLID.Visible = true;
                txtDLDesc.Visible = true;
                txtDLPerc.Visible = true;
                chkActiveFlag.Visible = true;

                lblDLID.Visible = false;
                lblDLDesc.Visible = false;
                lblDLPerc.Visible = false;
                lblDLStatus.Visible = false;

                lnkDLEdit.Visible = false;
                lnkDLSave.Visible = true;
                icon_Error.Visible = false;

                txtDLDesc.Focus();
            }
            else if (e.CommandName == "Save")
            {
                //Validation
                if (string.IsNullOrEmpty(txtDLDesc.Text.Trim()))
                {
                    lbl_DLError.Title = "Enter Description";
                    icon_Error.Visible = true;
                    txtDLDesc.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDLPerc.Text.Trim()))
                {
                    lbl_DLError.Title = "Enter Percentage";
                    icon_Error.Visible = true;
                    txtDLPerc.Focus();
                    return;
                }
                string ActiveFlag = "0";
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = "1";
                else
                    ActiveFlag = "0";

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];

                int ResultId = 0;
                ResultId = ProductController.Insert_Update_ConfigTables(ddlConfigTable.SelectedValue, txtDLID.Text, txtDLDesc.Text, ActiveFlag, UserID, "", txtDLPerc.Text.Trim());
                if (ResultId == -1)
                {
                    lbl_DLError.Title = "Duplicate Record";
                    icon_Error.Visible = true;
                    txtDLDesc.Focus();
                    return;
                }
                else
                {
                    icon_Error.Visible = false;
                }

                //Change look
                txtDLID.Visible = false;
                txtDLDesc.Visible = false;
                txtDLPerc.Visible = false;
                chkActiveFlag.Visible = false;

                lblDLID.Visible = true;
                lblDLDesc.Visible = true;
                lblDLPerc.Visible = true;
                lblDLStatus.Visible = true;

                lnkDLEdit.Visible = true;
                lnkDLSave.Visible = false;

                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    public OE_Configration()
    {
        Load += Page_Load;
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


 
}
