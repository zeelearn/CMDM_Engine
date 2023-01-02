using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;



public partial class Manage_Synch : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_Validation();
                ControlVisibility("Search");
                
                FillDDL_Parent();
                                
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


        private void FillDDL_Parent()
        {

            try
            {

                //Clear_Error_Success_Box();

                DataSet dsParentMenu = ProductController.Usp_Get_ManageSync(1,"","");
                BindDDL(ddlDatabaseName, dsParentMenu, "SyncDBName", "SyncDBCode");
                ddlDatabaseName.Items.Insert(0, "Select Database Name");
                ddlDatabaseName.SelectedIndex = 0;



                //DataSet dsAnpplication = ProductController.Usp_Get_Fill_ParentMenu(2);
                //BindDDL(ddlApplicationName, dsAnpplication, "System_Description", "System_Code");
                //ddlApplicationName.Items.Insert(0, "Select");
                //ddlApplicationName.SelectedIndex = 0;

                //BindDDL(ddlApplicationName_Search, dsAnpplication, "System_Description", "System_Code");
                //ddlApplicationName_Search.Items.Insert(0, "Select");
                //ddlApplicationName_Search.SelectedIndex = 0;

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
                
                DivSearchPanel.Visible = true;
                
                
                DivResultPanel.Visible = false;
                          
            }
            else if (Mode == "Result")
            {
                
                DivSearchPanel.Visible = false;
                
                DivResultPanel.Visible = true;
                
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
       

        /// <summary>
        /// Clear Edit Panel 
        /// </summary>
       
        /// <summary>
        /// Clear Add Panel 
        /// </summary>
        private void ClearSearchPanel()
        {
            ddlDatabaseName.SelectedIndex = 0;
        }

        /// <summary>
        /// Fill Division drop down list
        /// </summary>
       


       
       
       
        /// <summary>
        /// Bind Datalist
        /// </summary>
        private void FillGrid(string SyncDBCode)
        {
            try
            {
                Clear_Error_Success_Box();
               
                ControlVisibility("Result");
                string columnname = "";

                if (ddlDatabaseName.SelectedValue.ToString() == "01")
                {
                    columnname = "DB0Syncflag";
                }
                else if (ddlDatabaseName.SelectedValue.ToString() == "02")
                {
                    columnname = "DB1Syncflag";
                }


                DataSet dsGrid = ProductController.Usp_Get_ManageSync(2, SyncDBCode, columnname);
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
       

        /// <summary>
        /// Update data
        /// </summary>
        

        #endregion


       
        #region Event's
       

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
            Clear_Error_Success_Box();
        }

        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {
            FillGrid(ddlDatabaseName.SelectedValue.ToString().Trim());
        }

        
        

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Clear_Error_Success_Box();

            if (ddlDatabaseName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Database Name");
                ddlDatabaseName.Focus();
                return;
            }


           
            FillGrid(ddlDatabaseName.SelectedValue.ToString ().Trim ());
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearchPanel();
        }


        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int ResultId = 0;
            string spname = "";
            int value = 0;

            if (e.CommandName == "comSync")
            {
                spname = ProductController.Usp_Get_SyncSpName(ddlDatabaseName .SelectedValue .ToString (),3);
                
                string table_code = e.CommandArgument.ToString().Trim();
                ResultId = ProductController.Usp_Get_UpdateanageSync(table_code, spname, value);

                FillGrid(ddlDatabaseName.SelectedValue.ToString().Trim());

            }
           
        }

        
        #endregion            

        
       
       
       
       
       
        
        protected void HLExport_Click(object sender, EventArgs e)
        {
            DataList1.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Synchronization_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Synchronization</b></TD></TR>");
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
