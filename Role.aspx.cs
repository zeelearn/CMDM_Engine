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

partial class Role : System.Web.UI.Page
{

    

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            ControlVisibility("Search");
            
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivAddPanel.Visible = false;
            DivAssignUser.Visible = false;
            BtnAdd.Visible = true;
            lblPKey_Edit.Text = "";
            
        }
        else if (Mode == "Result")
        {
            DivSearchPanel.Visible = false ;
            DivResultPanel.Visible = true;
            DivAddPanel.Visible = false;
            DivAssignUser.Visible = false;
            BtnAdd.Visible = true;
            BtnShowSearchPanel.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivAddPanel.Visible = true;
            DivAssignUser.Visible = false;
            BtnAdd.Visible = false;
        }
        else if (Mode == "User")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivAddPanel.Visible = false;
            DivAssignUser.Visible = true;
            BtnAdd.Visible = false;
        }
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        try
        {
            ControlVisibility("Add");
            Fill_AddMenu(1);
            Clear_AddPanel();
        }
        catch (Exception )
        {
            Response.Redirect("Role.aspx");
        }

    }

    private void fillAddEditMenu(int flag, string roleid)
    {
        DataSet dsGrid = ProductController.Get_Fill_AddMenu(flag, roleid);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                dlMenu_Add.DataSource = dsGrid;
                dlMenu_Add.DataBind();
            }
        }
    }

    private void fillAddEditUser(int flag, string roleid)
    {
        DataSet dsGrid = ProductController.Get_Fill_AddMenu(flag, roleid);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                DataList1.DataSource = dsGrid;
                DataList1.DataBind();
            }
        }
    }

    private void Fill_AddMenu(int flg)
    {
        DataSet dsGrid = ProductController.Get_Fill_AddMenu(flg,"");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                if (flg == 1)
                {
                    dlMenu_Add.DataSource = dsGrid;
                    dlMenu_Add.DataBind();
                }
                else if (flg == 2)
                {
                    DataList1.DataSource = dsGrid;
                    DataList1.DataBind();
                }
                

            }
        }
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        FillGrid();
        UpdatePanelresultpanel.Update();
        BtnShowSearchPanel.Visible = true;
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search"); 
        UpdatePanelSearch.Update();

    }

      protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        if (string.IsNullOrEmpty(txtAddRoleName.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter Role Name");
            txtAddRoleName.Focus();
            return;
        }

        //string ActivityCode = "";
        //foreach (DataListItem dtlItem in dlCapacity_Add.Items)
        //{
        //    CheckBox chkDL_Select_Activity = (CheckBox)dtlItem.FindControl("chkDL_Select_Activity");
        //    Label lblDL_Activity_Id = (Label)dtlItem.FindControl("lblDL_Activity_Id");
        //    if (chkDL_Select_Activity.Checked == true)
        //    {
        //        SelCnt = SelCnt + 1;
        //        ActivityCode = ActivityCode + lblDL_Activity_Id.Text + ",";
        //    }
        //}

          int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

          string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;
          string RollCode="";
        string ResultId = null;
        ResultId = ProductController.Usp_Insert_RoleNew(txtAddRoleName.Text.ToString().Trim(), ActiveFlag, CreatedBy, 1,RollCode,"","",0);

        if (ResultId == "-2")
        {
            Show_Error_Success_Box("E", "Record Already Exist");
            txtAddRoleName.Focus();
            return;
        }
        string resid = "";

        foreach (DataListItem dtlItem in dlMenu_Add.Items)
        {
            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
            if (chkCheck.Checked == true)
            {
                
                Label lblMenucode = (Label)dtlItem.FindControl("lblMenuCode");

                Label Menutype = (Label)dtlItem.FindControl("lblMenuType");
                Label lblMenuName = (Label)dtlItem.FindControl("lblMenuName");

                resid = ProductController.Usp_Insert_RoleNew("", 0, CreatedBy, 2, ResultId, lblMenucode.Text, lblMenuName.Text, 1);
                    
             }

         }


        if(ResultId =="1")
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "Record Save Successfully");
            Clear_AddPanel();
            FillGrid();
            UpdatePanelresultpanel.Update();
        }
        ControlVisibility("Result"); 
        FillGrid();
        UpdatePanelresultpanel.Update();

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
        txtAddRoleName.Text = "";
        lblPKey_Edit.Text = "";
        
    }

              
    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        if (string.IsNullOrEmpty(txtAddRoleName.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter Role Name");
            txtAddRoleName.Focus();
            return;
        }


        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;
        string RollCode = lblPKey_Edit.Text .Trim ();
        string ResultId = null;
        ResultId = ProductController.Usp_Insert_RoleNew(txtAddRoleName.Text.ToString().Trim(), ActiveFlag, CreatedBy, 3, RollCode, "", "", 0);

        if (ResultId == "-2")
        {
            Show_Error_Success_Box("E", "Record Already Exist");
            txtAddRoleName.Focus();
            return;
        }
        string resid = "";

        
        foreach (DataListItem dtlItem in dlMenu_Add.Items)
        {
            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
            if (chkCheck.Checked == true)
            {
                
                Label lblMenucode = (Label)dtlItem.FindControl("lblMenuCode");

                Label Menutype = (Label)dtlItem.FindControl("lblMenuType");
                Label lblMenuName = (Label)dtlItem.FindControl("lblMenuName");

                resid = ProductController.Usp_Insert_RoleNew("", 0, CreatedBy, 2, ResultId, lblMenucode.Text, lblMenuName.Text, 1);

            }

        }


        if (resid == "1")
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "Record Save Successfully");
            Clear_AddPanel();
            FillGrid();
                UpdatePanelresultpanel.Update();
        }
        
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {

        DataList2.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Role_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Role</b></TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            DataList2.RenderControl(oHtmlTextWriter1);
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();

            DataList2.Visible = false;
    }

    protected void ddlTitle_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");

        FillGrid();
        UpdatePanelresultpanel.Update();
    }

    private void FillGrid()
    {
        string rolename="";

        if (txtSearchRole.Text.Trim() == "")
        {
            rolename = "%%";
        }
        else
        {
            rolename = txtSearchRole.Text;
        }
            
        string status = ddlStatus.SelectedValue.ToString();
        int act=1;
        
        if(status=="Active")
        {
            act=1;
        }
        else if(status=="Inactive")
        {
            act=0;
        }

        DataSet dsGrid = ProductController.Fill_Role_SearchPanel(rolename, act);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                DataList2.DataSource = dsGrid;
                DataList2.DataBind();

                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
        }
        else
        {
            lbltotalcount.Text = "0";
        }
    }

    
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearchRole.Text = "";
        ddlStatus.SelectedIndex = 0;
    }
    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");
            lblPKey_Edit.Text = "";
            lblPKey_Edit.Text = e.CommandArgument.ToString().Trim();
            fillRoleDetails(lblPKey_Edit.Text .Trim ());
            BtnSave.Visible = false;
            BtnSaveEdit.Visible = true;
            BtnAdd.Visible = false;
            BtnShowSearchPanel.Visible = true;
            UpdatePanelAddPnl.Update();
        }
        else if (e.CommandName == "comUser")
        {
            ControlVisibility("User");
            lblroleid_User.Text = e.CommandArgument.ToString().Trim();
            fillDetails(lblroleid_User.Text.ToString().Trim());
            fillcheckbox(lblroleid_User.Text.ToString().Trim());
            UpdatePanelAssignUser.Update();
        }
    }

    private void fillcheckbox(string role)
    {
        DataSet dsCRoom = ProductController.Get_Fill_AddMenu(4, role);
        int count = 0;
        
        for (int cnt = 0; cnt <= dsCRoom.Tables[0].Rows.Count - 1; cnt++)
        {

            foreach (DataListItem dtlItem in DataList1.Items)
            {
                CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("CHKUser");
                Label lblusercode = (Label)dtlItem.FindControl("lblusercode");
                if (Convert.ToString(lblusercode.Text).Trim() == Convert.ToString(dsCRoom.Tables[0].Rows[cnt]["User_Code"]).Trim())
                {
                    chkDL_Select_Centre.Checked = true;
                    count=count+1;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

        }

        if (count == 0)
        {
            BtnSave_User.Visible = true;
            BtnSaveEdit_User.Visible = false;
        }
        else if (count > 0)
        {
            BtnSave_User.Visible = false;
            BtnSaveEdit_User.Visible = true;
        }
    }

    private void fillRoleDetails(string roleid)
    {
        //Fill_AddMenu(1);
        fillAddEditMenu(6, roleid);

        DataSet dsCRoom1 = ProductController.Get_Fill_AddMenu(5, roleid);
        txtAddRoleName.Text = Convert.ToString(dsCRoom1.Tables[0].Rows[0]["Role_Name"]).ToString().Trim();

        DataSet dsCRoom = ProductController.Get_Fill_AddMenu(3,roleid );
        //DataSet dsGrid = ProductController.Get_Fill_AddMenu(flg);

        if (dsCRoom.Tables[0].Rows.Count > 0)
        {
            
            for (int cnt = 0; cnt <= dsCRoom.Tables[0].Rows.Count - 1; cnt++)
            {

                foreach (DataListItem dtlItem in dlMenu_Add.Items)
                {
                    CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkCheck");
                    Label lblMenuCode = (Label)dtlItem.FindControl("lblMenuCode");
                    if (Convert.ToString(lblMenuCode.Text).Trim() == Convert.ToString(dsCRoom.Tables[0].Rows[cnt]["Menu_Code"]).Trim())
                    {
                        chkDL_Select_Centre.Checked = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

            }
        }
        else
        {
            for (int cnt = 0; cnt <= dsCRoom.Tables[0].Rows.Count - 1; cnt++)
            {

                foreach (DataListItem dtlItem in dlMenu_Add.Items)
                {
                    CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkCheck");
                    Label lblMenuCode = (Label)dtlItem.FindControl("lblMenuCode");
                    if (Convert.ToString(lblMenuCode.Text).Trim() == Convert.ToString(dsCRoom.Tables[0].Rows[cnt]["Menu_Code"]).Trim())
                    {
                        chkDL_Select_Centre.Checked = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

            }
        }
        
    }

    private void fillDetails(string RoleId)
    {
        DataSet dsCRoom = ProductController.Get_Role_Details(RoleId, 1);


        if (dsCRoom.Tables[0].Rows.Count > 0)
        {

            txtRoleName_User.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Role_Name"]);
            lblroleid_User.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Role_Code"]);

            //Fill_AddMenu(2);
            fillAddEditUser(7, RoleId);
        }
    }


    
    protected void BtnSave_User_Click(object sender, EventArgs e)
    {
        string rolecode = lblroleid_User.Text.ToString().Trim();
        //int isactive = "1";

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

          string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string resultid = "";
        foreach (DataListItem dtlItem in DataList1.Items)
        {
            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("CHKUser");
            if (chkCheck.Checked == true)
            {
                
                Label usercode = (Label)dtlItem.FindControl("lblusercode");

                resultid = ProductController.Usp_Insert_RoleUser(usercode.Text.Trim(), rolecode, 1, 2, CreatedBy);

            }

        }

        if (resultid == "1")
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "Record Save Successfully");
            Clear_AddPanel();
            FillGrid();
            UpdatePanelresultpanel.Update();
        }
        ControlVisibility("Result");
        FillGrid();
        UpdatePanelresultpanel.Update();
    }
    
    protected void Button3_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
        FillGrid();
        UpdatePanelresultpanel.Update();
    }
    protected void BtnSaveEdit_User_Click(object sender, EventArgs e)
    {
        string resultid="";

        resultid = ProductController.Usp_Insert_RoleUser("", lblroleid_User.Text.Trim (), 0, 3, "");

        /////////

        

        string rolecode = lblroleid_User.Text.ToString().Trim();
        //int isactive = "1";
        
            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;


            foreach (DataListItem dtlItem in DataList1.Items)
            {
                CheckBox chkCheck = (CheckBox)dtlItem.FindControl("CHKUser");
                if (chkCheck.Checked == true)
                {
                    
                    Label usercode = (Label)dtlItem.FindControl("lblusercode");

                    resultid = ProductController.Usp_Insert_RoleUser(usercode.Text.Trim(), rolecode, 1, 2, CreatedBy);

                }

            }

            if (resultid == "1")
            {
                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "Record Save Successfully");
                Clear_AddPanel();
                FillGrid();
                UpdatePanelresultpanel.Update();
            }



        

    }

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlMenu_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
        }




    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in DataList1.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("CHKUser");
            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
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

}
