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

partial class Manage_User : System.Web.UI.Page
{

    

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Designation();
            FillDDL_Department();
            FillDDL_Gender();
            Page_Validation();
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

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
        Clear_AddPanel();
        FillDDL_Division();
        FillDDL_Role();


        lblHeader_Add.Text = "Create New User";
        lblTestPKey_Hidden.Text = "";
        lblPkey.Text = "";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
        
    }

    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            
            DataSet dsDivision = ProductController.GetAllDivisionName_ForUserCreation(2);

            BindListBox(ddlUserDivision, dsDivision, "Source_Division_ShortDesc", "Source_Division_Code");

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


    private void FillDDL_Role()
    {
        try
        {
            ddlUserRole.Items.Clear();

            DataSet dsRole = ProductController.GetAllRoleCode_ForUserCreation(3);

            BindDDL(ddlUserRole, dsRole, "Role_Name", "Role_Code");
            ddlUserRole.Items.Insert(0, "Select");
            ddlUserRole.SelectedIndex = 0;

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

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
        Fill_Search();
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
            FillDDL_Division();
            FillDDL_Role();
            dlUserCenters.DataSource = "";
            dlUserCenters.DataBind();
            DataSet dsCRoom = ProductController.GetUserDetails_ByPKey(pkey, 7);
            if (dsCRoom.Tables[0].Rows.Count > 0)
            {
                lblTestPKey_Hidden.Text = pkey;


                txtuser_Display_Name.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Display_Name"]);
                txtUserEmailId.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Email_Id"]);
                txtUserEmpCode.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Name"]);
                txtHandPhone1_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["User_Mobile_No"]);
                txtagentid.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Agent_Id"]);
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


                if (Convert.ToInt32(dsCRoom.Tables[0].Rows[0]["Allow_To_Use_Internal_Flag"]) == 0)
                {
                    chkboxallowtouseinternal.Checked = false;
                }
                else
                {
                    chkboxallowtouseinternal.Checked = true;
                }









                DataSet dsCRoom1 = ProductController.GetUserDetails_ByPKey(pkey, 8);
                if (dsCRoom1.Tables[0].Rows.Count > 0)
                {
                    //Fill selected Batches
                    if (dsCRoom1.Tables[0].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= dsCRoom1.Tables[0].Rows.Count - 1; cnt++)
                        {
                            for (int rcnt = 0; rcnt <= ddlUserDivision.Items.Count - 1; rcnt++)
                            {
                                if (ddlUserDivision.Items[rcnt].Value == dsCRoom1.Tables[0].Rows[cnt]["Source_Division_Code"].ToString())
                                {
                                    ddlUserDivision.Items[rcnt].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                ddlUserRole.SelectedValue = Convert.ToString(dsCRoom1.Tables[0].Rows[0]["Role_ID"]);

                List<string> list = new List<string>();
                string Div_Code = "";
                foreach (ListItem li in ddlUserDivision.Items)
                {
                    if (li.Selected == true)
                    {
                        list.Add(li.Value);
                        Div_Code = string.Join(",", list.ToArray());
                    }
                }
                string DivCodes = Div_Code;

                DataSet dsCenter = ProductController.GetAllCenters_Filled_For_User(12, DivCodes, pkey);
                if (dsCenter != null)
                {
                    if (dsCenter.Tables.Count != 0)
                    {

                        dlUserCenters.DataSource = dsCenter;
                        dlUserCenters.DataBind();
                    }
                }



                if (dsCRoom1.Tables[0].Rows.Count > 0)
                {

                    for (int cnt = 0; cnt <= dsCRoom1.Tables[0].Rows.Count - 1; cnt++)
                    {

                        foreach (DataListItem dtlItem in dlUserCenters.Items)
                        {
                            CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkCenter");
                            Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                            if (Convert.ToString(lblCentreCode.Text).Trim() == Convert.ToString(dsCRoom1.Tables[0].Rows[cnt]["Center_Code"]).Trim())
                            {
                                chkDL_Select_Centre.Checked = true;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
        }


    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    protected void ddlCountry_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
       

        Clear_Error_Success_Box();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        
        Clear_Error_Success_Box();
    }

    

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    

    protected void BtnSave_Click(object sender, System.EventArgs e)
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


        if (ddlgender.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Gender");
            ddlgender.Focus();
            return;
        }

        if (ddlUserRole.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Role");
            ddlUserRole.Focus();
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

        int SelCntDiv = 0;
        List<string> list = new List<string>();
        string Batch = "";
        foreach (ListItem li in ddlUserDivision.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                Batch = string.Join(",", list.ToArray());
                SelCntDiv = SelCntDiv + 1;
            }
        }

        if (SelCntDiv == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            return;
        }

        int SelCntCen = 0;
        string DivisionCode = "";
        foreach (DataListItem dtlItem in dlUserCenters.Items)
        {
            CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
            //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
            Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");

            if (chkDL_Select_Center.Checked == true)
            {
                SelCntCen = SelCntCen + 1;
                DivisionCode = DivisionCode + lblCentreCode.Text + ",";

            }
        }
        DivisionCode = Common.RemoveComma(DivisionCode);
        //if (Strings.Right(DivisionCode, 1) == ",")
        //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

        if (txtagentid.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Agent Id");
            txtagentid.Focus();
            return;
        }

        if (SelCntCen == 0)
        {
            Show_Error_Success_Box("E", "Select Center(s)");
            dlUserCenters.Focus();
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

        int AllowToUSeInternalFag = 0;
        if (chkboxallowtouseinternal.Checked == true)
        {
            AllowToUSeInternalFag = 1;
        }
        else
        {
            AllowToUSeInternalFag = 0;
        }

        ResultId = ProductController.Insert_User(txtUserEmpCode.Text.Trim(), txtuser_Display_Name.Text.Trim(), txtUserEmailId.Text.Trim(), txtHandPhone1_Add.Text.Trim(), "", ActiveFlag, UserID, 1, ddlUserRole.SelectedValue, txtemprfid.Text, ddldepartment.SelectedValue, ddldepartment.SelectedItem.Text, ddldesignation_Add.SelectedValue, ddldesignation_Add.SelectedItem.Text, AllowToUSeInternalFag, ddlgender.SelectedItem.Text, txtdob.Value,txtagentid.Text);

        string resid = "";

        if (ResultId.Equals(""))
        {

        }
        else
        {
            foreach (DataListItem dtlItem in dlUserCenters.Items)
            {
                CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCenter");
                if (chkCheck.Checked == true)
                {

                    Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                    string RoleCenterId = ddlUserRole.SelectedValue.ToString().Trim() + lblCentreCode.Text.Trim() + ResultId;

                    resid = ProductController.Insert_Role_User_CenterAss(RoleCenterId, ddlUserRole.SelectedValue.ToString().Trim(), lblCentreCode.Text.Trim(), ResultId, 5);
                    

                }

            }


        }


        Resultid1 = ProductController.Get_Update_Role_Synch(11);

        
        //Close the Add Panel and go to Search Grid
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

    private void Clear_AddPanel()
    {
        dlUserCenters.DataSource = null;
        dlUserCenters.DataBind();

        txtuser_Display_Name.Text = "";
        txtUserEmailId.Text = "";
        txtUserEmpCode.Text = "";
        txtHandPhone1_Add.Text = "";
        ddlUserRole.Items.Clear();
        FillDDL_Role();
        chkActiveFlag.Checked = true;
        ddlUserDivision.Items.Clear();
        txtdob.Value = "";
        ddlgender.SelectedIndex = 0;
        txtemprfid.Text = "";
        txtagentid.Text = "";
        ddldepartment.SelectedIndex = 0;
        ddldesignation_Add.SelectedIndex = 0;
        FillDDL_Division();
        chkboxallowtouseinternal.Checked = true;
        chkActiveFlag.Checked = true;
    }

    //Protected Sub ddlLocation_Add_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation_Add.SelectedIndexChanged
    //    Clear_Error_Success_Box()
    //End Sub

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {
        //Clear_Error_Success_Box()
        //'Authorise the selected test
        //Dim PKey As String
        //PKey = lblPKey_Authorise.Text

        //Dim lblHeader_User_Code As Label
        //lblHeader_User_Code = CType(Master.FindControl("lblHeader_User_Code"), Label)

        //Dim AlteredBy As String
        //AlteredBy = lblHeader_User_Code.Text

        //Dim ResultId As Integer
        //ResultId = ProductController.UpdateTest_Authorise_Block(PKey, 2, AlteredBy)

        //'Close the Add Panel and go to Search Grid
        //If ResultId = 1 Then
        //    ControlVisibility("Result")
        //    BtnSearch_Click(sender, e)
        //    Show_Error_Success_Box("S", "0000")
        //    Clear_AddPanel()
        //End If
    }

    protected void ddlCity_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //FillDDL_Location()
        //Clear_Error_Success_Box()
    }

    

    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
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

        if (ddlgender.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Gender");
            ddlgender.Focus();
            return;
        }


        if (ddlUserRole.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Role");
            ddlUserRole.Focus();
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


        int SelCntDiv = 0;
        List<string> list = new List<string>();
        string Batch = "";
        foreach (ListItem li in ddlUserDivision.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                Batch = string.Join(",", list.ToArray());
                SelCntDiv = SelCntDiv + 1;
            }
        }

        if (SelCntDiv == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            return;
        }

        int SelCntCen = 0;
        string DivisionCode = "";
        foreach (DataListItem dtlItem in dlUserCenters.Items)
        {
            CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkCenter");
            //CheckBox chkDL_Select_Center = (CheckBox)dtlItem.FindControl("chkDL_Select_Center");
            Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");

            if (chkDL_Select_Center.Checked == true)
            {
                SelCntCen = SelCntCen + 1;
                DivisionCode = DivisionCode + lblCentreCode.Text + ",";

            }
        }
        DivisionCode = Common.RemoveComma(DivisionCode);
        //if (Strings.Right(DivisionCode, 1) == ",")
        //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

        if (txtagentid.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Agent Id");
            txtagentid.Focus();
            return;
        }

        if (SelCntCen == 0)
        {
            Show_Error_Success_Box("E", "Select Center(s)");
            dlUserCenters.Focus();
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

        int AllowToUSeInternalFag = 0;
        if (chkboxallowtouseinternal.Checked == true)
        {
            AllowToUSeInternalFag = 1;
        }
        else
        {
            AllowToUSeInternalFag = 0;
        }
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];


        ResultId = ProductController.InsertUpdate_User(txtUserEmpCode.Text.Trim(), txtuser_Display_Name.Text.Trim(), txtUserEmailId.Text.Trim(), txtHandPhone1_Add.Text.Trim(), "", ActiveFlag, UserID,9,lblPkey .Text .Trim (),ddlUserRole .SelectedValue .ToString ().Trim (),txtemprfid.Text,ddldepartment.SelectedValue,ddldepartment.SelectedItem.Text,ddldesignation_Add.SelectedValue,ddldesignation_Add.SelectedItem.Text,AllowToUSeInternalFag,ddlgender.SelectedItem.Text,txtdob.Value,txtagentid.Text);

        string resid = "";
        string resid1="";

        resid1 = ProductController.Insertupdatedelrole_User(lblPkey .Text .Trim (), 10);

            foreach (DataListItem dtlItem in dlUserCenters.Items)
            {
                CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCenter");
                if (chkCheck.Checked == true)
                {

                    Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                    string RoleCenterId = ddlUserRole.SelectedValue.ToString().Trim() + lblCentreCode.Text.Trim() + lblPkey.Text.Trim();

                    resid = ProductController.Insert_Role_User_CenterAss(RoleCenterId, ddlUserRole.SelectedValue.ToString().Trim(), lblCentreCode.Text.Trim(), lblPkey.Text.Trim(), 5);

                }

            }
            int Resultid1 = 0;
            Resultid1 = ProductController.Get_Update_Role_Synch(11);

        //Close the Add Panel and go to Search Grid
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

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "User_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>User</b></TD></TR>");
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

    
    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly

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
    protected void ddlUserDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string lblid = lblPkey.Text.ToString().Trim();
            int len = lblid.Length;

            if (len == 0)
            {
                List<string> list = new List<string>();
                string Div_Code = "";
                foreach (ListItem li in ddlUserDivision.Items)
                {
                    if (li.Selected == true)
                    {
                        list.Add(li.Value);
                        Div_Code = string.Join(",", list.ToArray());
                    }
                }
                string DivCodes = Div_Code;

                DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_ForUserCreation(4, DivCodes);
                if (dsCenter != null)
                {
                    if (dsCenter.Tables.Count != 0)
                    {

                        dlUserCenters.DataSource = dsCenter;
                        dlUserCenters.DataBind();
                    }
                }
            }
            else
            {

                DataSet dsCRoom1 = ProductController.GetUserDetails_ByPKey(lblid, 8);

                List<string> list = new List<string>();
                string Div_Code = "";
                foreach (ListItem li in ddlUserDivision.Items)
                {
                    if (li.Selected == true)
                    {
                        list.Add(li.Value);
                        Div_Code = string.Join(",", list.ToArray());
                    }
                }
                string DivCodes = Div_Code;

                DataSet dsCenter = ProductController.GetAllCenters_Filled_For_User(12, DivCodes, lblid);
                if (dsCenter != null)
                {
                    if (dsCenter.Tables.Count != 0)
                    {

                        dlUserCenters.DataSource = dsCenter;
                        dlUserCenters.DataBind();
                    }
                }



                if (dsCRoom1.Tables[0].Rows.Count > 0)
                {

                    for (int cnt = 0; cnt <= dsCRoom1.Tables[0].Rows.Count - 1; cnt++)
                    {

                        foreach (DataListItem dtlItem in dlUserCenters.Items)
                        {
                            CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkCenter");
                            Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                            if (Convert.ToString(lblCentreCode.Text).Trim() == Convert.ToString(dsCRoom1.Tables[0].Rows[cnt]["Center_Code"]).Trim())
                            {
                                chkDL_Select_Centre.Checked = true;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }

                    }
                }
            }

            


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

    //private void Fill_Centers(string Division)
    //{
       
    //}

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlUserCenters.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");

            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
        }

        

        
    }  
}
