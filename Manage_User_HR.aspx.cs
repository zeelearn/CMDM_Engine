using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Globalization;

public partial class Manage_User_HR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Designation();
            FillDDL_Department();
            FillDDL_Gender();
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

    private void FillDDL_Designation()
    {
        try
        {
            DataSet dsGrid = ProductController.GetAllDesignation();
            BindDDL(ddldesignation_Add, dsGrid, "Designation_Long_Desc", "Record_Id");
            ddldesignation_Add.Items.Insert(0, "Select");
            ddldesignation_Add.SelectedIndex = 0;
        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());
        }
    }


    private void FillDDL_Department()
    {
        try
        {
            DataSet dsGrid = ProductController.GetAllDepartment();
            BindDDL(ddldepartment, dsGrid, "DeptLongDesc", "DeptId");
            ddldepartment.Items.Insert(0, "Select");
            ddldepartment.SelectedIndex = 0;
        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());
        }
    }

    private void FillDDL_Gender()
    {
        try
        {


            ddlgender.Items.Add("Select");
            ddlgender.Items.Add("Male");
            ddlgender.Items.Add("Female");
            ddlgender.SelectedIndex = 0;
        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());
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
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
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
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Fill_Search();
    }
    private void Fill_Search()
    {

        ControlVisibility("Result");

        string UserDisplayname = null;
        if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
        {
            UserDisplayname = "%";
        }
        else
        {
            UserDisplayname = "%" + txtUserName.Text.Trim();
        }

        string UserCode = null;
        if (string.IsNullOrEmpty(txtUsercode.Text.Trim()))
        {
            UserCode = "%";
        }
        else
        {
            UserCode = "%" + txtUsercode.Text.Trim();
        }

        DataSet dsGrid = ProductController.GetAllUsersBy_Search(UserDisplayname, UserCode, 6);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();

                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
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

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtUserName.Text = "";
        txtUsercode.Text = "";
    }



    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        Clear_AddPanel();
        
        


        lblHeader_Add.Text = "Create New User";
        lblTestPKey_Hidden.Text = "";
        lblPkey.Text = "";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
        
    }

    private void Clear_AddPanel()
    {
       

        txtuser_Display_Name.Text = "";
        txtUserEmailId.Text = "";
        txtUserEmpCode.Text = "";
        txtHandPhone1_Add.Text = "";
       
        chkActiveFlag.Checked = true;
        txtdob.Value = "";
        ddlgender.SelectedIndex = 0;
        txtemprfid.Text = "";
        ddldepartment.SelectedIndex = 0;
        ddldesignation_Add.SelectedIndex = 0;
        
        chkActiveFlag.Checked = true;
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
        Fill_Search();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        //Validate if all information is entered correctly

        if (string.IsNullOrEmpty(txtuser_Display_Name.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter User Display Name");
            txtuser_Display_Name.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtUserEmpCode.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter Employee Code");
            txtUserEmpCode.Focus();
            return;
        }
        if (txtdob.Value == "")
        {
            Show_Error_Success_Box("E", "Enter DOB");
            //txtdob.Focus();
            return;

        }


        DateTime dtStart = DateTime.Parse(txtdob.Value);
        TimeSpan sp = DateTime.Now - dtStart;

        if (sp.Days < 18 * 365)
        {
            Show_Error_Success_Box("E", "DOB Shoule Be Above 18 years");
            //txtdob.Focus();
            return;

        }

        if (ddlgender.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Gender");
            ddlgender.Focus();
            return;
        }

        if (txtemprfid.Text.Trim().Length == 0)
        { 
            Show_Error_Success_Box("E","Enter EMP Rfid");
            txtemprfid.Focus();
            return;
        }


        if (txtHandPhone1_Add.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Mobile No");
            txtHandPhone1_Add.Focus();
            return;
        }
        if (txtHandPhone1_Add.Text.Length > 0 && txtHandPhone1_Add.Text.Length < 10)
        {
            Show_Error_Success_Box("E", "Mobile No Must Be Of 10 Digits");
            txtHandPhone1_Add.Focus();
            return;
        }




        if (ddldepartment.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Department");
            ddldepartment.Focus();
        }
        if (ddldesignation_Add.SelectedIndex == 0)
        {

            Show_Error_Success_Box("E", "Select Designation");
            ddldesignation_Add.Focus();
            return;
        }


        //Save
        string ResultId = null;
        int Resultid1 = 0;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }
        
        ResultId = ProductController.Insert_User(txtUserEmpCode.Text.Trim(), txtuser_Display_Name.Text.Trim(), txtUserEmailId.Text.Trim(), txtHandPhone1_Add.Text.Trim(), "", ActiveFlag, UserID, 13, "", txtemprfid.Text, ddldepartment.SelectedValue, ddldepartment.SelectedItem.Text, ddldesignation_Add.SelectedValue, ddldesignation_Add.SelectedItem.Text, 0, ddlgender.SelectedItem.Text, txtdob.Value, "0");

        if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "Duplicate Emp Code");
            txtuser_Display_Name.Focus();
            return;
        }
        else
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }

    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //lbldelCode.Text = e.CommandArgument.ToString();
            //txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        else if (e.CommandName == "Edit")
        {

            ControlVisibility("Add");
            BtnSave.Visible = false;
            BtnSaveEdit.Visible = true;

            lblPkey.Text = e.CommandArgument.ToString();
            lblHeader_Add.Text = "Edit User Details";
            lblTestPKey_Hidden.Text = "";
            fill_details(lblPkey.Text);
        }
    }


    private void fill_details(string pkey)
    {
        try
        {

            DataSet dsCRoom = ProductController.GetUserDetails_ByPKey(pkey, 7);
            if (dsCRoom.Tables[0].Rows.Count > 0)
            {
                lblTestPKey_Hidden.Text = pkey;


                txtuser_Display_Name.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Display_Name"]);
                txtUserEmailId.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Email_Id"]);
                txtUserEmpCode.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Name"]);
                txtHandPhone1_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Mobile_No"]);

                if (Convert.ToInt32(dsCRoom.Tables[0].Rows[0]["IsActive"]) == 0)
                {
                    chkActiveFlag.Checked = false;
                }
                else
                {
                    chkActiveFlag.Checked = true;
                }
                //DateTime dtedtitdob = Convert.ToDateTime(dsCRoom.Tables[0].Rows[0]["User_Date_Of_Birth"].ToString(), CultureInfo.InvariantCulture);
                txtdob.Value = dsCRoom.Tables[0].Rows[0]["User_Date_Of_Birth"].ToString();
                if (dsCRoom.Tables[0].Rows[0]["User_Gender"].ToString() == "")
                {
                    ddlgender.SelectedIndex = 0;
                }
                else
                {
                    ddlgender.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Gender"]);
                }
                if (dsCRoom.Tables[0].Rows[0]["DeptId"].ToString() == "")
                {
                    ddldepartment.SelectedIndex = 0;
                }
                else
                {
                    ddldepartment.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["DeptId"]);
                }
                if (dsCRoom.Tables[0].Rows[0]["Designation_Id"].ToString() == "")
                {
                    ddldesignation_Add.SelectedIndex = 0;
                }
                else
                {
                    ddldesignation_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Designation_Id"]);
                }
                txtemprfid.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Emp_RFID"]);





            }




        }
        catch (Exception ex)
        {
        }
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtuser_Display_Name.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter User Display Name");
            txtuser_Display_Name.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtUserEmpCode.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter Employee Code");
            txtUserEmpCode.Focus();
            return;
        }


        if (txtdob.Value == "")
        {
            Show_Error_Success_Box("E", "Enter DOB");
            //txtdob.Focus();
            return;

        }


        DateTime dtStart = DateTime.Parse(txtdob.Value);
        TimeSpan sp = DateTime.Now - dtStart;

        if (sp.Days < 18 * 365)
        {
            Show_Error_Success_Box("E", "DOB Shoule Be Above 18 years");
            //txtdob.Focus();
            return;

        }

        if (ddlgender.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Gender");
            ddlgender.Focus();
            return;
        }

        if (txtemprfid.Text.Trim().Length == 0)
        {
            Show_Error_Success_Box("E", "Enter EMP Rfid");
            txtemprfid.Focus();
            return;
        }

        if (txtHandPhone1_Add.Text.Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Mobile No");
            txtHandPhone1_Add.Focus();
            return;
        }
        if (txtHandPhone1_Add.Text.Length > 0 && txtHandPhone1_Add.Text.Length < 10)
        {
            Show_Error_Success_Box("E", "Mobile No Must Be Of 10 Digits");
            txtHandPhone1_Add.Focus();
            return;
        }



        if (ddldepartment.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Department");
            ddldepartment.Focus();
        }
        if (ddldesignation_Add.SelectedIndex == 0)
        {

            Show_Error_Success_Box("E", "Select Designation");
            ddldesignation_Add.Focus();
            return;
        }


        
        //Save
        string ResultId = null;


        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        ResultId = ProductController.InsertUpdate_User(txtUserEmpCode.Text.Trim(), txtuser_Display_Name.Text.Trim(), txtUserEmailId.Text.Trim(), txtHandPhone1_Add.Text.Trim(), "", ActiveFlag, UserID, 14, lblPkey.Text.Trim(), "", txtemprfid.Text, ddldepartment.SelectedValue, ddldepartment.SelectedItem.Text, ddldesignation_Add.SelectedValue, ddldesignation_Add.SelectedItem.Text,0, ddlgender.SelectedItem.Text, txtdob.Value,"0");

        if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0058");
            txtuser_Display_Name.Focus();
            return;
        }
        else
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }
    }
}