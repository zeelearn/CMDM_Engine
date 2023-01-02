using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;



public partial class Config_Medium : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;             
            }
            else if (Mode == "Result")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = true;
                DivEditPanel.Visible = false;                
            }
            else if (Mode == "Add")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;                
            }
            else if (Mode == "Edit")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = true;              
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
            txtAddMediumName.Text = "";
            txtAddMediumDisplayName.Text = "";
            txtMediumshortName.Text = "";
            txtMediumDescription.Text = "";
            chkActiveFlag.Checked = true;

        }

        /// <summary>
        /// Clear Edit Panel 
        /// </summary>
        private void ClearEditPanel()
        {
            txtEditMediumName.Text = "";
            txtEditMediumDisplayName.Text = "";
            txtEditMediumShortName.Text = "";
            txtEditMediumDescription.Text = "";
            chkEditActiveFlag.Checked = true;
        }

        /// <summary>
        /// Clear Add Panel 
        /// </summary>
        private void ClearSearchPanel()
        {
            txtMediumName.Text = "";
        }

        /// <summary>
        /// Bind search  Datalist
        /// </summary>
        private void FillGrid()
        {
            try
            {
                Clear_Error_Success_Box();
                string MediumName = "";
                if (txtMediumName.Text.Trim() == "")
                {
                    MediumName = "%%";
                }
                else
                    MediumName = txtMediumName.Text;
                ControlVisibility("Result");

                DataSet dsGrid = ProductController.Get_Medium_DetailsMaster(MediumName,"1");
                

                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {
                            dlGridDisplay.DataSource = dsGrid;
                            dlGridDisplay.DataBind();

                            DataList1.DataSource = dsGrid;
                            DataList1.DataBind();

                            lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
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
                if (txtAddMediumName.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Medium Name");
                    txtAddMediumName.Focus();
                    return;
                }
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];

                int ActiveFlag = 0;
                if (chkActiveFlag.Checked == true)
                    ActiveFlag = 1;
                else
                    ActiveFlag = 0;

                int ResultId = 0;
                //ResultId = ProductController.InsertUpdateMedium("", txtAddMediumName.Text, ActiveFlag, txtAddMediumDisplayName.Text, txtMediumshortName.Text, txtMediumDescription.Text, UserID, "1","");
                //if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
                //{
                //    Show_Error_Success_Box("E", "Duplicate Medium Name");
                //    return;
                //}
                //else
                //{
                //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                //    string Return_Pkey_CRM = ms.CreateMedium(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["MediumName"].ToString(), ResultId.Tables[0].Rows[0]["DisplayName"].ToString(), ResultId.Tables[0].Rows[0]["ShortName"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
                //    string ID = ResultId.Tables[0].Rows[0]["Id"].ToString();
                //    ResultId = null;
                //    ResultId = ProductController.InsertUpdateMedium(ID, txtAddMediumName.Text, ActiveFlag, txtAddMediumDisplayName.Text, txtMediumshortName.Text, txtMediumDescription.Text, UserID, "3", Return_Pkey_CRM);
                    
                //    FillGrid();
                //    Show_Error_Success_Box("S", "Record saved Successfully.");
                //}

                ResultId = ProductController.InsertUpdateMedium("", txtAddMediumName.Text, ActiveFlag, txtAddMediumDisplayName.Text, txtMediumshortName.Text, txtMediumDescription.Text, UserID, "1");
                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Duplicate Medium Name");
                    return;
                }
                else
                {

                    FillGrid();
                    Show_Error_Success_Box("S", "Record saved Successfully.");
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
            try
            {
                Clear_Error_Success_Box();

                if (txtEditMediumName.Text.Trim() == "")
                {
                    Show_Error_Success_Box("E", "Enter Medium Name");
                    txtEditMediumName.Focus();
                    return;
                }
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;

                int ActiveFlag = 0;
                if (chkEditActiveFlag.Checked == true)
                    ActiveFlag = 1;
                else
                    ActiveFlag = 0;

                int ResultId = 0;
                //ResultId = ProductController.InsertUpdateMedium(lblPkey.Text, txtEditMediumName.Text, ActiveFlag, txtEditMediumDisplayName.Text, txtEditMediumShortName.Text, txtEditMediumDescription.Text, CreatedBy, "2","");
                //if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
                //{
                //    Show_Error_Success_Box("E", "Duplicate Medium Name");
                //    return;
                //}
                //else
                //{
                //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                //    string Return_Pkey_CRM = ms.CreateMedium(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["MediumName"].ToString(), ResultId.Tables[0].Rows[0]["DisplayName"].ToString(), ResultId.Tables[0].Rows[0]["ShortName"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());

                //    ResultId = null;
                //    ResultId = ProductController.InsertUpdateMedium(lblPkey.Text, txtEditMediumName.Text, ActiveFlag, txtEditMediumDisplayName.Text, txtEditMediumShortName.Text, txtEditMediumDescription.Text, CreatedBy, "3", Return_Pkey_CRM);

                //    FillGrid();
                //    Show_Error_Success_Box("S", "Record Update Successfully.");
                //}

                ResultId = ProductController.InsertUpdateMedium(lblPkey.Text, txtEditMediumName.Text, ActiveFlag, txtEditMediumDisplayName.Text, txtEditMediumShortName.Text, txtEditMediumDescription.Text, CreatedBy, "2");
                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Duplicate Medium Name");
                    return;
                }
                else
                {

                    FillGrid();
                    Show_Error_Success_Box("S", "Record Update Successfully.");
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
            FillGrid();
        }

        protected void btnEditClose_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void BtnSaveAdd_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearchPanel();
        }

        

        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "comEdit")
            {
                ControlVisibility("Edit");
                ClearEditPanel();
                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.Get_Medium_DetailsByPKey(lblPkey.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEditMediumName.Text = ds.Tables[0].Rows[0]["MediumName"].ToString();
                    txtEditMediumDisplayName.Text = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    txtEditMediumShortName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();
                    txtEditMediumDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    if (ds.Tables[0].Rows[0]["Is_Active"].ToString() == "0")
                        chkEditActiveFlag.Checked = false;
                    else
                        chkEditActiveFlag.Checked = true;
                }
            }
        }

        protected void btnEditSave_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        #endregion            

        
       
       
       
       
        protected void txtMediumName_TextChanged(object sender, EventArgs e)
        {

        }
        protected void HLExport_Click(object sender, EventArgs e)
        {
            DataList1.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Medium_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Medium(s)</b></TD></TR>");
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
