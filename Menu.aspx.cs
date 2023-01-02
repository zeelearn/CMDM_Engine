using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;



public partial class Config_Course : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_Validation();
                ControlVisibility("Search");
                ddlParentMenuName.Enabled =false ;
                FillDDL_Parent();
                                
            }
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Fill drop down list and assign value and Text
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ds"></param>
        /// <param name="txtField"></param>
        /// <param name="valField"></param>
        private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }

        /// <summary>
        /// Fill List box and assign value and Text
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ds"></param>
        /// <param name="txtField"></param>
        /// <param name="valField"></param>
        private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }

        /// <summary>
        /// Clear Error Success Box
        /// </summary>
        private void Clear_Error_Success_Box()
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = false;
            lblSuccess.Text = "";
            lblerror.Text = "";
            UpdatePanelMsgBox.Update();
        }


        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = true;
                BtnAdd.Visible = true;
                BtnSaveAdd.Visible = true;
                BtnSaveEdit.Visible  = false;
                DivResultPanel.Visible = false;
                DivUserMenu.Visible = false;
                          
            }
            else if (Mode == "Result")
            {
                DivUserMenu.Visible = false;
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = true;
                lblPkey.Text = "";
                
            }
            else if (Mode == "Add")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivUserMenu.Visible = false;
                lblPkey.Text = "";
                BtnSaveEdit.Visible = false;
                BtnSaveAdd.Visible = true;
            }
            else if (Mode == "Edit")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivUserMenu.Visible = false; 
            }
            else if (Mode == "UserRole")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = false;
                DivUserMenu.Visible = true;
                lblmenuPK.Text = "";
            }
         
        }
        /// <summary>
        /// Show Error or success box on top base on boxtype and Error code
        /// </summary>
        /// <param name="BoxType">BoxType</param>
        /// <param name="Error_Code">Error_Code</param>
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
        
        
        /// <summary>
        /// Clear Add Panel 
        /// </summary>
        private void ClearAddPanel()
        {
            txtMenuName.Text = "";
            txtMenuCss.Text = "";
            //ddlParentMenuName.SelectedIndex = 0;
            ddlParentMenuName.Items.Clear();
            txtMenuLink.Text ="";
            txtMenuDiscription.Text = "";
            ddlMenuType.SelectedIndex = 0;
            //ddlParentMenuName.SelectedIndex = 0;
            chkActiveFlag.Checked = true;
            txtOrderNo.Text = "";
        }

        /// <summary>
        /// Clear Edit Panel 
        /// </summary>
       
        /// <summary>
        /// Clear Add Panel 
        /// </summary>
        private void ClearSearchPanel()
        {
            ddlApplicationName_Search.SelectedIndex = 0;
        }

        /// <summary>
        /// Fill Division drop down list
        /// </summary>
        private void FillDDL_Parent()
        {

            try
            {

                Clear_Error_Success_Box();

                

                DataSet dsAnpplication = ProductController.Usp_Get_Fill_ParentMenu(2);
                BindDDL(ddlApplicationName, dsAnpplication, "System_Description", "System_Code");
                ddlApplicationName.Items.Insert(0, "Select");
                ddlApplicationName.SelectedIndex = 0;
                
                BindDDL(ddlApplicationName_Search, dsAnpplication, "System_Description", "System_Code");
                ddlApplicationName_Search.Items.Insert(0, "Select");
                ddlApplicationName_Search.SelectedIndex = 0;
                
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


       
       
       
        /// <summary>
        /// Bind Datalist
        /// </summary>
        private void FillGrid(string Application_name)
        {
            try
            {
                Clear_Error_Success_Box();
               
                ControlVisibility("Result");

                DataSet dsGrid = ProductController.Get_Fill_MenuSearch(4,Application_name);
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();

                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {
                            lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                        }
                        else
                        {
                            lbltotalcount.Text = "0";
                        }
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    lbltotalcount.Text = "0";
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


        /// <summary>
        /// Insert data
        /// </summary>
        private void SaveData()
        {
            try
            {
                Clear_Error_Success_Box();

                if (ddlApplicationName.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Application Name");
                    ddlApplicationName.Focus();
                    return;
                }
                
                if (txtMenuName.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Name");
                    txtMenuName.Focus();
                    return;
                }

                if (txtMenuCss.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Css");
                    txtMenuCss.Focus();
                    return;
                }

                if (txtMenuLink.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Link");
                    txtMenuLink.Focus();
                    return;
                }

                if (txtMenuLink.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Link");
                    txtMenuLink.Focus();
                    return;
                }

                if (txtOrderNo.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Link");
                    txtOrderNo.Focus();
                    return;
                }
                

                if (ddlMenuType.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Menu Type");
                    ddlMenuType.Focus();
                    return;
                }

                
                string Menu_type = ddlMenuType.SelectedValue.ToString().Trim();
                if (Menu_type == "2")
                {
                    if (ddlParentMenuName.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Parent Menu Name");
                        ddlParentMenuName.Focus();
                        return;
                    }
                }

                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                //string CreatedBy = null;
                //CreatedBy = lblHeader_User_Code.Text;

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string CreatedBy = cookie.Values["UserID"];


                int ActiveFlag = 0;
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = 1;
                else
                    ActiveFlag = 0;

                int ResultId = 0;
                int menutype = 0;
                 //string Menu_type = ddlMenuType.SelectedValue.ToString().Trim();
                int order = 0;

                order = Convert .ToInt32 (txtOrderNo.Text.Trim());
                 if (Menu_type == "2")
                 {
                     menutype = 2;
                     ResultId = ProductController.Insert_Menu_Details(3, "02", txtMenuName.Text.ToString().Trim(), txtMenuCss.Text.Trim(), ddlParentMenuName.SelectedValue.ToString().Trim(), menutype, txtMenuLink.Text.Trim(), txtMenuDiscription.Text.Trim(), order, ActiveFlag, CreatedBy,ddlApplicationName .SelectedValue .ToString ());
                 }
                 else if (Menu_type == "1")
                 {
                     menutype = 1;
                     ResultId = ProductController.Insert_Menu_Details(3, "02", txtMenuName.Text.ToString().Trim(), txtMenuCss.Text.Trim(), "0", menutype, txtMenuLink.Text.Trim(), txtMenuDiscription.Text.Trim(), order, ActiveFlag, CreatedBy, ddlApplicationName.SelectedValue.ToString());
                 }

                
                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Duplicate Menu Name");
                    return;
                }
                else if (ResultId == 1)
                {
                    Show_Error_Success_Box("S", "Record saved Successfully.");
                    FillGrid(ddlApplicationName_Search .SelectedValue .ToString ().Trim ());
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

        /// <summary>
        /// Update data
        /// </summary>
        private void UpdateData()
        {
            //try
            //{
            //    Clear_Error_Success_Box();

            //    if (ddlDivisionEdit.SelectedIndex == 0)
            //    {
            //        Show_Error_Success_Box("E", "0001");
            //        ddlDivisionEdit.Focus();
            //        return;
            //    }
            //    if (ddlCCEdit.SelectedIndex == 0)
            //    {
            //        Show_Error_Success_Box("E", "Select Course Category");
            //        ddlCCEdit.Focus();
            //        return;
            //    }
            //    if (ddlBoardEdit.SelectedIndex == 0)
            //    {
            //        Show_Error_Success_Box("E", "Select Board");
            //        ddlBoardEdit.Focus();
            //        return;
            //    }
            //    if (ddlMediumEdit.SelectedIndex == 0)
            //    {
            //        Show_Error_Success_Box("E", "Select Medium");
            //        ddlMediumEdit.Focus();
            //        return;
            //    }
            //    if (txtEditCourseName.Text.Trim() == "")
            //    {
            //        Show_Error_Success_Box("E", "Enter Course Name");
            //        txtEditCourseName.Focus();
            //        return;
            //    }
            //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //    string UserID = cookie.Values["UserID"];

            //    int ActiveFlag = 0;
            //    if (chkEditActiveFlag.Checked == true)
            //        ActiveFlag = 1;
            //    else
            //        ActiveFlag = 0;
                
            //    int ResultId = 0;
            //    ResultId = ProductController.InsertUpdateCourseDetail(lblPkey.Text,ddlCCEdit.SelectedValue, ddlBoardEdit.SelectedValue,ddlMediumEdit.SelectedValue,ddlDivisionEdit.SelectedValue, txtEditCourseName.Text, txtEditCourseDisplayName.Text, txtEditCourseShortName.Text, txtEditCourseDesc.Text, txtEditCourseSequenceNo.Text, ActiveFlag, UserID, "2");
                                
            //    if (ResultId == -1)
            //    {
            //        Show_Error_Success_Box("E", "Duplicate Course Name");
            //        return;
            //    }
            //    else
            //    {
            //        Show_Error_Success_Box("S", "Record Update Successfully.");
            //        FillGrid("%%", ddlDivisionEdit.SelectedValue);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = ex.ToString();
            //    UpdatePanelMsgBox.Update();
            //    return;
            //}

        }

        #endregion


       
        #region Event's
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            
            ControlVisibility("Add");
            ClearAddPanel();
            Clear_Error_Success_Box();
        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
            Clear_Error_Success_Box();
        }

        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {
            FillGrid(ddlApplicationName_Search.SelectedValue.ToString().Trim());
        }

        
        protected void BtnSaveAdd_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Clear_Error_Success_Box();

            if (ddlApplicationName_Search.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Application Name");
                ddlApplicationName.Focus();
                return;
            }

            FillGrid(ddlApplicationName_Search.SelectedValue.ToString ().Trim ());
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearchPanel();
        }


        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "comEdit")
            {
                lblPkey.Text = "";
                ControlVisibility("Add");
                BtnSaveEdit.Visible = true;
                BtnSaveAdd.Visible = false;
                ClearAddPanel();
                Clear_Error_Success_Box();

                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.Get_Edit_MenuSearch(5, lblPkey.Text.Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlApplicationName.SelectedValue = ds.Tables[0].Rows[0]["Application_No"].ToString();
                    txtMenuName.Text = ds.Tables[0].Rows[0]["Menu_Name"].ToString();
                    txtMenuCss.Text = ds.Tables[0].Rows[0]["Menu_CSS"].ToString();
                    txtMenuLink.Text = ds.Tables[0].Rows[0]["Menu_Link"].ToString();
                    txtMenuDiscription.Text = ds.Tables[0].Rows[0]["Menu_Description"].ToString();
                    ddlMenuType.SelectedValue = ds.Tables[0].Rows[0]["Menu_Type"].ToString();

                    //

                    DataSet dsParentMenu = ProductController.Usp_Get_Fill_ParentMenu2(1, ddlApplicationName.SelectedValue.ToString().Trim());
                    BindDDL(ddlParentMenuName, dsParentMenu, "Menu_Name", "Menu_Code");
                    ddlParentMenuName.Items.Insert(0, "Select");
                    ddlParentMenuName.SelectedIndex = 0;

                    //
                    string a = ds.Tables[0].Rows[0]["Menu_Parent_Code"].ToString();
                    if (a == "0")
                    {
                        ddlParentMenuName.SelectedIndex = 0;
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        ddlParentMenuName.Enabled = true;
                        ddlParentMenuName.SelectedValue = ds.Tables[0].Rows[0]["Menu_Parent_Code"].ToString();
                    }
                    
                    txtOrderNo.Text = ds.Tables[0].Rows[0]["Display_Order_No"].ToString();

                    if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "0")
                        chkActiveFlag.Checked = false;
                    else
                        chkActiveFlag.Checked = true;
                }
            }
            else if (e.CommandName == "comUserMenu")
            {
                lblPkey.Text = "";
                ControlVisibility("UserRole");

                Clear_Error_Success_Box();

                lblmenuPK.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.Get_Edit_MenuSearch(5, lblmenuPK.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblMenuName.Text = ds.Tables[0].Rows[0]["Menu_Name"].ToString();
                    lblMenuLink.Text = ds.Tables[0].Rows[0]["Menu_Link"].ToString();
                }

                DataSet DSGrid = ProductController.Get_Edit_MenuSearch(7, lblmenuPK.Text.Trim());
                DLUserMenu01.DataSource = DSGrid;
                DLUserMenu01.DataBind();


                
                                
            }
        }

        protected void btnEditSave_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        #endregion            

        
       
       
       
       
        protected void ddlMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Menu_type = ddlMenuType.SelectedValue.ToString().Trim();
            if (Menu_type == "2")
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                ddlParentMenuName.Enabled = true;
            }
            else if (Menu_type == "1")
            {
                Label1.ForeColor = System.Drawing.Color.Black;
                ddlParentMenuName.Enabled = false;
            }

        }
        protected void BtnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Error_Success_Box();

                if (ddlApplicationName.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Application Name");
                    ddlApplicationName.Focus();
                    return;
                }

                if (txtMenuName.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Name");
                    txtMenuName.Focus();
                    return;
                }

                if (txtMenuCss.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Css");
                    txtMenuCss.Focus();
                    return;
                }

                if (txtMenuLink.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Link");
                    txtMenuLink.Focus();
                    return;
                }

                if (txtMenuLink.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Link");
                    txtMenuLink.Focus();
                    return;
                }

                if (txtOrderNo.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Menu Link");
                    txtOrderNo.Focus();
                    return;
                }


                if (ddlMenuType.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Menu Type");
                    ddlMenuType.Focus();
                    return;
                }


                string Menu_type = ddlMenuType.SelectedValue.ToString().Trim();
                if (Menu_type == "2")
                {
                    if (ddlParentMenuName.SelectedIndex == 0)
                    {
                        Show_Error_Success_Box("E", "Select Parent Menu Name");
                        ddlParentMenuName.Focus();
                        return;
                    }
                }

                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                //string CreatedBy = null;
                //CreatedBy = lblHeader_User_Code.Text;

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string CreatedBy = cookie.Values["UserID"];


                int ActiveFlag = 0;
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = 1;
                else
                    ActiveFlag = 0;

                int ResultId = 0;
                int menutype = 0;
                //string Menu_type = ddlMenuType.SelectedValue.ToString().Trim();
                int order = 0;

                order = Convert.ToInt32(txtOrderNo.Text.Trim());
                if (Menu_type == "2")
                {
                    menutype = 2;
                    ResultId = ProductController.Update_Menu_Details(6, "02", txtMenuName.Text.ToString().Trim(), txtMenuCss.Text.Trim(), ddlParentMenuName.SelectedValue.ToString().Trim(), menutype, txtMenuLink.Text.Trim(), txtMenuDiscription.Text.Trim(), order, ActiveFlag, CreatedBy, ddlApplicationName.SelectedValue.ToString(),lblPkey .Text .Trim ());
                }
                else if (Menu_type == "1")
                {
                    menutype = 1;
                    ResultId = ProductController.Update_Menu_Details(6, "02", txtMenuName.Text.ToString().Trim(), txtMenuCss.Text.Trim(), "0", menutype, txtMenuLink.Text.Trim(), txtMenuDiscription.Text.Trim(), order, ActiveFlag, CreatedBy, ddlApplicationName.SelectedValue.ToString(),lblPkey .Text .Trim ());
                }


                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Duplicate Menu Name");
                    return;
                }
                else if (ResultId == 1)
                {
                    Show_Error_Success_Box("S", "Record saved Successfully.");
                    FillGrid(ddlApplicationName_Search.SelectedValue.ToString().Trim());
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
        protected void btnUserMenuSave_Click(object sender, EventArgs e)
        {
            string Menucode = lblmenuPK.Text.Trim();
            string resid = null;

            
            foreach (DataListItem dtlItem in DLUserMenu01.Items)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkCheck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkCheck");
                if (chkCheck.Checked == true)
                {

                    Label lblRoleCode = (Label)dtlItem.FindControl("lblRoleCode");

                    resid = ProductController.Usp_DelUpdate_RoleMenuCode(Menucode, lblRoleCode.Text.Trim(), 8);

                }

            }


            if (resid == "1")
            {
                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "Record Save Successfully");

            }
            else
            {
                ControlVisibility("Result");
            }
        


        }
        protected void btnUserMenuClose_Click(object sender, EventArgs e)
        {
            FillGrid(ddlApplicationName_Search.SelectedValue.ToString().Trim());
        }
        protected void ddlApplicationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsParentMenu = ProductController.Usp_Get_Fill_ParentMenu2(1,ddlApplicationName .SelectedValue .ToString ().Trim ());
            BindDDL(ddlParentMenuName, dsParentMenu, "Menu_Name", "Menu_Code");
            ddlParentMenuName.Items.Insert(0, "Select");
            ddlParentMenuName.SelectedIndex = 0;
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
        protected void HLExport_Click(object sender, EventArgs e)
        {
            DataList1.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Menu_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='9'>Menu</b></TD></TR>");
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


