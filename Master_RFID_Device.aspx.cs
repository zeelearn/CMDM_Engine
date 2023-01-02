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

public partial class Master_RFID_Device : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
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

        Fill_GridDisplay();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            txtDeviceSearch.Text = "";
            ddlStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

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
        BtnSaveEdit.Visible = false;
        btnSave.Visible = true;
        lblHeader_Add.Text = "Add RFID Device";
        txtDeviceName.Text = "";
        chkActive.Checked = true;
        Fill_GridCentreDiv();
    }
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_Error_Success_Box();
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (txtDeviceName.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Device Name");
            txtDeviceName.Focus();
            return;
        }
        string CentreCode = "";
        foreach (DataListItem dtlItem in dlGridCentreDiv.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
            if (chkitemck.Checked == true)
            {
                Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                CentreCode = CentreCode + lblCentreCode.Text + ",";
            }
        }

        if (CentreCode != "")
        {
            CentreCode = CentreCode.Substring(0, CentreCode.Length - 1);
        }

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
            ActiveFlag = 1;
        else
            ActiveFlag = 0;

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        int ResultId = 0;
        ResultId = ProductController.InsertUpdateRFID_Device(lblPKey.Text, txtDeviceName.Text.Trim(), ActiveFlag, UserID, CentreCode, "2");
        if (ResultId == -1)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Record already Exists!!";
            UpdatePanelMsgBox.Update();
            return;
        }
        else if (ResultId == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Record Updated Successfully!!";
            UpdatePanelMsgBox.Update();
            Fill_GridDisplay();
            return;
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDeviceName.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Device Name");
            txtDeviceName.Focus();
            return;
        }
        string CentreCode = "";
        foreach (DataListItem dtlItem in dlGridCentreDiv.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
            if (chkitemck.Checked== true)
            {
                Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                CentreCode = CentreCode + lblCentreCode.Text + ",";
            }
        }

        if (CentreCode != "")
        {
            CentreCode = CentreCode.Substring(0, CentreCode.Length - 1);
        }

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
            ActiveFlag = 1;
        else
            ActiveFlag = 0;     

         HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

        int ResultId = 0;
        ResultId = ProductController.InsertUpdateRFID_Device("", txtDeviceName.Text.Trim(), ActiveFlag, UserID, CentreCode, "1");
        if (ResultId == -1)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Record already Exists!!";
            UpdatePanelMsgBox.Update();
            return;
        }
        else if (ResultId == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Record Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Fill_GridDisplay();
            return;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

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

    private void Fill_GridCentreDiv()
    {
        DataSet dsGrid = ProductController.Get_RFID_Centre_Div("1");
        dlGridCentreDiv.DataSource = dsGrid;
        dlGridCentreDiv.DataBind();
    }


    private void Fill_GridDisplay()
    {
        Clear_Error_Success_Box();
        ControlVisibility("Result");
        string DeviceName="",ActivFlag="";
        if (txtDeviceSearch.Text.Trim() == "")
        {
            DeviceName = "%%";
        }
        else
            DeviceName = txtDeviceSearch.Text.Trim();

        if (ddlStatus.SelectedValue == "0")
            ActivFlag = "%%";
        else if (ddlStatus.SelectedValue == "1")
            ActivFlag = "1";
        else if (ddlStatus.SelectedValue == "2")
            ActivFlag = "0";

        DataSet dsGrid = new DataSet();

        dsGrid = ProductController.GetRFIDDevice("",DeviceName,ActivFlag, "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                dlDisplay.DataSource = dsGrid;
                dlDisplay.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                dlDisplay.DataSource = null;
                dlDisplay.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            dlDisplay.DataSource = null;
            dlDisplay.DataBind();
            lbltotalcount.Text = "0";
        }

    }

    protected void chkAllCentre_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridCentreDiv.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
            chkitemck.Checked = s.Checked;
        }
        
        Clear_Error_Success_Box();

    }

    protected void dlDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");
            BtnSaveEdit.Visible = true;
            btnSave.Visible = false;
            lblHeader_Add.Text = "Edit RFID Device";
            Fill_GridCentreDiv();
            lblPKey.Text = e.CommandArgument.ToString();
            txtDeviceName.Text = "";
            DataSet dsGrid = new DataSet();            
            dsGrid = ProductController.GetRFIDDevice(lblPKey.Text, "", "", "2");
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtDeviceName.Text = dsGrid.Tables[0].Rows[0]["Device_Name"].ToString();
                if (Convert.ToInt32(dsGrid.Tables[0].Rows[0]["Is_Active"]) == 0)
                {
                    chkActive.Checked = false;
                }
                else
                {
                    chkActive.Checked = true;
                }
               
                for (int cnt = 0; cnt <= dsGrid.Tables[1].Rows.Count - 1; cnt++)
                {
                    foreach (DataListItem dtlItem in dlGridCentreDiv.Items)
                    {
                        Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                        if (lblCentreCode.Text == dsGrid.Tables[1].Rows[cnt]["Center_Code"].ToString())
                        {
                            CheckBox chkitemcentre = (CheckBox)dtlItem.FindControl("chkCentre");
                            chkitemcentre.Checked = true;
                            break;
                        }
                    }
                }
               
            }
        }
    }
}