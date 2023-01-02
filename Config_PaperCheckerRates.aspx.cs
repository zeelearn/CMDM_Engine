using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;



public partial class Config_PaperCheckerRates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
            
        }
    }


    #region Methods

    /// <summary>
    /// Visible panel base on Mode
    /// </summary>
    /// <param name="Mode">Mode</param>
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            lblPkey.Text = "";
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            BtnSaveEdit.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnSaveEdit.Visible = false;
        }
        else if (Mode == "Add")
        {
            lblPkey.Text = "";
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true ;
            DivAddPanel.Visible = true;
            BtnAdd.Visible = false;
            BtnSearch.Visible = true;
            BtnSaveEdit.Visible = false;
            BtnSaveAdd.Visible = true;

        }
        Clear_Error_Success_Box();
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
    /// Fill Division drop down list
    /// </summary>
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            string Company_Code = "MT";
            string DBname = "CDB";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
            BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
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
     /// Bind  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();

            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }


            
            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue.ToString ();

            string sbname = "";
            string slabName = txtSlab_Name_Sr.Text.Trim();
            if (slabName == "")
            {
                sbname = "%";
            }
            else
            {
                sbname = slabName;
            }


            string status = ddlStatus.SelectedValue.ToString().Trim();
            int istatus = Convert .ToInt32(status);

            
            DataSet dsGrid = null;
            dsGrid = ProductController.Get_SlabDetails_SR(DivisionCode, sbname, istatus, 4);


            dlFaculty.DataSource = null;
            dlFaculty.DataBind();


            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        dlFaculty.DataSource = dsGrid;
                        dlFaculty.DataBind();
                        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        dlFaculty.DataSource = null;
                        dlFaculty.DataBind();
                        lbltotalcount.Text = "0";
                    }
                    
                }
                else
                {
                    dlFaculty.DataSource = null;
                    dlFaculty.DataBind();
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                dlFaculty.DataSource = null;
                dlFaculty.DataBind();
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



    protected void dlFaculty_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");
            BtnSaveAdd.Visible = false;
            BtnSaveEdit.Visible = true;

            lblPkey.Text = "";
            lblPkey.Text = e.CommandArgument.ToString();
            FillSlabDetails(lblPkey.Text);
        }
    }


    private void FillSlabDetails(string PKey)
    {

        try
        {


            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.Get_SlabDetails_ED(PKey.Trim(), 5);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtSlab_Name_Add.Text = dsGrid.Tables[0].Rows[0]["Slab_Name"].ToString();

                if (dsGrid.Tables[0].Rows[0]["HActive"].ToString() == "1")
                {

                    chkActiveFlag.Checked = true;
                }
                else
                {
                    chkActiveFlag.Checked = false;
                }

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dsGrid.Tables[0];

                Gridview1.DataSource = dsGrid;
                Gridview1.DataBind();
            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
       
    #endregion

       

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
        
        
    }
    
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        clearAddPanel();

        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        lblPkey.Text = "";
        ControlVisibility("Add");
        SetInitialRow();
    }



    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Search();
    }

    public void Clear_Search()
    {
        ddlDivision.SelectedIndex = 0;
        txtSlab_Name_Sr.Text = "";
        ddlStatus.SelectedIndex = 0;
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {

        if (txtSlab_Name_Add.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Slab Name");
            txtSlab_Name_Add.Focus();
            return;
        }

        if (txtSlab_Name_Add.Text.Trim().Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Slab Name");
            txtSlab_Name_Add.Focus();
            return;
        }

        // Save Slab Name 
        string DivisionCode = ddlDivision.SelectedValue;
        int Icount = 0;

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string ResultidSBID = "";
        ResultidSBID = ProductController.Insert_SlabDetails(txtSlab_Name_Add.Text.Trim(), ddlDivision.SelectedValue.ToString().Trim(), ActiveFlag, UserID,1);

        if (ResultidSBID == "2")
        {
            Show_Error_Success_Box("E", "Record Already Exist");
            txtSlab_Name_Add.Focus();
            return;
        }
        else
        {
            lblPkey.Text = ResultidSBID;

            foreach (GridViewRow dtlItem in Gridview1.Rows)
            {
                int ActiveItem = 0;
                TextBox txtFromMarks = (TextBox)dtlItem.FindControl("TextBox1");
                TextBox txtToMarks = (TextBox)dtlItem.FindControl("TextBox2");
                TextBox txtrate = (TextBox)dtlItem.FindControl("TextBox4");
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkActiveFlag");
                if (chkitemck.Checked)
                {
                    ActiveItem = 1;
                }
                else
                {
                    ActiveItem = 0;
                }


                Label lblResult = (Label)dtlItem.FindControl("lblResult");
                lblResult.Text = "";

                string ResultId = "";
                double frommarks, tomarks;

                if(txtFromMarks.Text.Trim().Length != 0 || txtToMarks.Text.Trim().Length != 0 || txtrate.Text.Trim().Length != 0)
                {
                    frommarks = Convert.ToDouble(txtFromMarks.Text.Trim());
                    tomarks = Convert.ToDouble(txtToMarks.Text.Trim());

                    if (frommarks > tomarks || frommarks == tomarks)
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        lblResult.Text = "Error : Invalid Marks";
                        Icount = Icount + 1;
                    }

                    else
                    {
                        ResultId = ProductController.Insert_SlabItemDetails(lblPkey.Text.Trim (), txtFromMarks.Text.Trim(), txtToMarks.Text.Trim(), txtrate.Text.Trim(), ActiveItem, 2);

                        if (ResultId == "1")
                        {

                            lblResult.ForeColor = System.Drawing.Color.Green;
                            lblResult.Text = "Success";
                        }
                        if (ResultId == "-1")
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Error : Invalid Marks";
                            Icount = Icount + 1;
                        }

                    }

                }
                else
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Error : Enter Marks";
                    Icount = Icount + 1;
                }
            }
  

        }


        if (Icount > 0)
        {

            BtnSaveEdit.Visible = true;
            BtnSaveAdd.Visible = false;

        }
        else
        {
            //Fill Update Grid
            FillGrid();
        }
              
    }

    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {

        if (txtSlab_Name_Add.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Slab Name");
            txtSlab_Name_Add.Focus();
            return;
        }

        if (txtSlab_Name_Add.Text.Trim().Length == 0)
        {
            Show_Error_Success_Box("E", "Enter Slab Name");
            txtSlab_Name_Add.Focus();
            return;
        }

        // Save Slab Name 
        string DivisionCode = ddlDivision.SelectedValue;
        int Icount = 0;

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string ResultidSBID = "";
        ResultidSBID = ProductController.Update_SlabDetails(txtSlab_Name_Add.Text.Trim(),lblPkey .Text, ActiveFlag, UserID, 3);

        if (ResultidSBID == "2")
        {
            Show_Error_Success_Box("E", "Record Already Exist");
            txtSlab_Name_Add.Focus();
            return;
        }
        else
        {
            

            foreach (GridViewRow dtlItem in Gridview1.Rows)
            {
                int ActiveItem = 0;
                TextBox txtFromMarks = (TextBox)dtlItem.FindControl("TextBox1");
                TextBox txtToMarks = (TextBox)dtlItem.FindControl("TextBox2");
                TextBox txtrate = (TextBox)dtlItem.FindControl("TextBox4");
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkActiveFlag");
                if (chkitemck.Checked)
                {
                    ActiveItem = 1;
                }
                else
                {
                    ActiveItem = 0;
                }


                Label lblResult = (Label)dtlItem.FindControl("lblResult");
                lblResult.Text = "";

                string ResultId = "";
                double frommarks, tomarks;

                if (txtFromMarks.Text.Length != 0 && txtToMarks.Text.Length != 0 && txtrate.Text.Length != 0)
                {
                    frommarks = Convert.ToDouble(txtFromMarks.Text.Trim());
                    tomarks = Convert.ToDouble(txtToMarks.Text.Trim());

                    if (frommarks > tomarks || frommarks == tomarks)
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        lblResult.Text = "Error : Invalid Marks";
                        Icount = Icount + 1;
                    }

                    else
                    {
                        ResultId = ProductController.Insert_SlabItemDetails(lblPkey.Text.Trim(), txtFromMarks.Text.Trim(), txtToMarks.Text.Trim(), txtrate.Text.Trim(), ActiveItem, 2);

                        if (ResultId == "1")
                        {

                            lblResult.ForeColor = System.Drawing.Color.Green;
                            lblResult.Text = "Success";
                        }
                        if (ResultId == "-1")
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Error : Invalid Marks";
                            Icount = Icount + 1;
                        }

                    }

                }
                else
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Error : Enter Marks";
                    Icount = Icount + 1;
                }
            }


        }


        if (Icount > 0)
        {

            BtnSaveEdit.Visible = true;
            BtnSaveAdd.Visible = false;

        }
        else
        {
            //Fill Update Grid
            FillGrid();
        }

    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        clearAddPanel();
        FillGrid ();
    }

    public void clearAddPanel()
    {
        txtSlab_Name_Add.Text = "";
        chkActiveFlag.Checked = true;
        SetInitialRow();
        ControlVisibility("Search");
    }

    
   protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
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

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Is_Active", typeof(Int32)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Is_Active"] = 1;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview1.DataSource = dt;
        Gridview1.DataBind();
    }
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox4");
                    int IsActive = 1;
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("chkActiveFlag");
                    if (chkitemck.Checked)
                    {
                        IsActive = 1;
                    }
                    else
                    {
                        IsActive = 0;
                    }

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    drCurrentRow["Is_Active"] = 1;

                    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Is_Active"] = IsActive;

                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox4");

                    int IsActive = 1;
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("chkActiveFlag");
                    if (chkitemck.Checked)
                    {
                        IsActive = 1;
                    }
                    else
                    {
                        IsActive = 0;
                    }

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();
                    IsActive = Convert.ToInt32(dt.Rows[i]["Is_Active"]);
                    rowIndex++;
                }
            }
        }
    }

    
}