using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using ShoppingCart.BL;
using Encryption.BL;

public partial class Manage_MICR_Code : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
           // GetAllBankDetails();
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

    
    public void BindBankDetails(string micrCode,string BankName)
    {
        try
        {
            Clear_Error_Success_Box();
            ControlVisibility("Result");

            DataSet ds = ProductController.GetBankDetails("1", micrCode, BankName);
            //grdBankDetails.DataSource = ds;
            //grdBankDetails.DataBind();

            if (ds != null)
            {
                if (ds.Tables.Count != 0)
                {
                    grdBankDetails.DataSource = ds;
                    grdBankDetails.DataBind();
                    lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    grdBankDetails.DataSource = null;
                    grdBankDetails.DataBind();
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                grdBankDetails.DataSource = null;
                grdBankDetails.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update(); 
        }
    }
    //public void GetAllBankDetails()
    //{
    //    DataSet ds = ProductController.GetBankDetails("2", "", "");
    //    grdBankDetails.DataSource = ds;
    //    grdBankDetails.DataBind();
    //    lblErrormsg.Visible = false;
    //}
    

    
    #  region GridView Event
        protected void grdBankDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowIndex = e.NewEditIndex;
            grdBankDetails.EditIndex = e.NewEditIndex;
            string val = (string)grdBankDetails.DataKeys[rowIndex]["Micrno"];           
            BindBankDetails(val,"%%");            
        }
        protected void grdBankDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdBankDetails.EditIndex = -1;
            //GetAllBankDetails();
            //BtnSearch_Click(sender, e);
            
        }
        protected void grdBankDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string micrno = grdBankDetails.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = (GridViewRow)grdBankDetails.Rows[e.RowIndex];

            string citycode = ((TextBox)grdBankDetails.Rows[e.RowIndex].FindControl("txtCityCode")).Text.Trim();
            string bankcode = ((TextBox)grdBankDetails.Rows[e.RowIndex].FindControl("txtBankCode")).Text.Trim();
            string branchcode = ((TextBox)grdBankDetails.Rows[e.RowIndex].FindControl("txtbranchcode")).Text.Trim();
            string bankname = ((TextBox)grdBankDetails.Rows[e.RowIndex].FindControl("txtbankname")).Text.Trim();
            string bankbranch = ((TextBox)grdBankDetails.Rows[e.RowIndex].FindControl("txtbankbranch")).Text.Trim();

            grdBankDetails.EditIndex = -1;
            DataSet ds = ProductController.Insert_Update_BankDetails("2", citycode, bankcode, branchcode, bankbranch, bankname, micrno);
            
            lblErrormsg.Visible = true;
            
            if (ds.Tables[0].Rows[0]["Column1"].ToString() == "-1")
            {
                lblErrormsg.Text = "MICRNo already exists.";
                lblErrormsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblErrormsg.Text = "Record Updated Successfully ";
                lblErrormsg.ForeColor = System.Drawing.Color.Green;
               // GetAllBankDetails();
                BtnSearch_Click(sender, e);
            }                 
        }
        protected void grdBankDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBankDetails.PageIndex = e.NewPageIndex;
            //GetAllBankDetails();
        }
        protected void grdBankDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                var citycode = grdBankDetails.FooterRow.FindControl("TextBox1") as TextBox;
                var bankcode = grdBankDetails.FooterRow.FindControl("TextBox2") as TextBox;
                var branchcode = grdBankDetails.FooterRow.FindControl("TextBox3") as TextBox;
                var bankname = grdBankDetails.FooterRow.FindControl("TextBox4") as TextBox;
                var bankbranch = grdBankDetails.FooterRow.FindControl("TextBox5") as TextBox;

                DataSet ds = ProductController.Insert_Update_BankDetails("1", citycode.Text, bankcode.Text, branchcode.Text, bankbranch.Text, bankname.Text, citycode.Text + bankcode.Text + branchcode.Text);
                                
                lblErrormsg.Visible = true;
                if (ds.Tables[0].Rows[0]["Column1"].ToString() == "-1")
                {
                    lblErrormsg.Text = "MICRNo already exists.";                                      
                    lblErrormsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    lblErrormsg.Text = "Record Inserted Successfully ";
                    lblErrormsg.ForeColor = System.Drawing.Color.Green;
                    //GetAllBankDetails();
                    BindBankDetails("%%", bankname.Text);
                }                

            }
        }
    #endregion        
   
    protected void BtnSearch_Click(object sender, EventArgs e)
    {                    
        if ((txtMicrNo.Text.Trim() == "") && (txtBankName.Text.Trim()==""))
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter MICR Number or Bank Name";
            UpdatePanelMsgBox.Update();            
            txtMicrNo.Focus();
            return;
        }
        lblErrormsg.Visible = false;
        string micrno = string.Empty,BankName=string.Empty;
        if (txtMicrNo.Text.Trim() == "")
            micrno = "%%";
        else
            micrno = txtMicrNo.Text.Trim();

        if (txtBankName.Text.Trim() == "")
            BankName = "%%";
        else
            BankName = txtBankName.Text.Trim();
        BindBankDetails(micrno, BankName);   
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtMicrNo.Text = "";
        txtBankName.Text = "";
        lblErrormsg.Visible = false;
        
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");       
        lblHeader_Add.Text = "Add MICR No";
        txtCityCode.Text = "";
        txtBankCode.Text = "";
        txtBranchCode.Text = "";
        txtBankNameAdd.Text = "";
        txtBranchName.Text = "";
        
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }


    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCityCode.Text.Trim() == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter City Code";
            UpdatePanelMsgBox.Update();
            return;
        }
        if (txtBankCode.Text.Trim() == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter Bank Code";
            UpdatePanelMsgBox.Update();
            txtBankCode.Focus();
            return;
        }
        if (txtBranchCode.Text.Trim() == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter Branch Code";
            UpdatePanelMsgBox.Update();
            txtBranchCode.Focus();
            return;
        }
        if (txtBankNameAdd.Text.Trim() == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter Bank Name";
            UpdatePanelMsgBox.Update();
            txtBankNameAdd.Focus();
            return;
        }
        if (txtBranchName.Text.Trim() == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter Branch Name";
            UpdatePanelMsgBox.Update();
            txtBranchName.Focus();
            return;
        }
        DataSet ds = ProductController.Insert_Update_BankDetails("1", txtCityCode.Text.Trim(), txtBankCode.Text.Trim(), txtBranchCode.Text, txtBranchName.Text, txtBankNameAdd.Text.Trim(), txtCityCode.Text.Trim() + txtBankCode.Text.Trim() + txtBranchCode.Text.Trim());
                
        if (ds.Tables[0].Rows[0]["Column1"].ToString() == "-1")
        {            
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "MICRNo already exists.";
            UpdatePanelMsgBox.Update();            
            return;
        }
        else
        {            
            //GetAllBankDetails();
            BindBankDetails("%%", txtBankNameAdd.Text.Trim());
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
            DivResult.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivResult.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResult.Visible = false;

        }
       
        else if (Mode == "TopSearch")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResult.Visible = false;

        }


    }



}  