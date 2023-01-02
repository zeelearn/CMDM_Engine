
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Configuration;


public partial class Sap_Discount_Master : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];
                DataSet ds1 = ProductController.Get_discount_Sap(Session["Lblpk.Text"].ToString(), "", "1");
                ControlVisibility("Add");
                FillDDL_subjectgrp();
                FillDDL_payplan();
                FillDDL_plan();
                lblHeader_Add.Text = "ADD Discount Details";
                //dlGridDisplay.DataSource = ds1;
                //dlGridDisplay.DataBind();

                txtCourse.Text = ds1.Tables[0].Rows[0]["Material_of_material_type_ZCRS"].ToString();
                txtCourse.Enabled = false;
                txtbatch.Text = ds1.Tables[0].Rows[0]["Batch"].ToString();
                txtbatch.Enabled = false;
                DataSet dsGrid_previous = new DataSet();
                dsGrid_previous = ProductController.Get_discount_details(txtCourse.Text, txtbatch.Text, 4);
                if (dsGrid_previous != null)
                {
                    if (dsGrid_previous.Tables[0].Rows.Count > 0)
                    {
                        lbldlerror.Visible = false;
                        lbldlerror.Text = "";
                        DataList3.DataSource = dsGrid_previous;
                        DataList3.DataBind();
                        DataList3.Visible = true;
                        //datatlist.data = dsGrid;
                    }
                    else
                    {
                        DataList3.DataSource = null;
                        DataList3.DataBind();
                        DataList3.Visible = false;
                        lbldlerror.Visible = true;
                        lbldlerror.Text = "No Discount Records Found";
                    }
                }
                            
            }
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

  

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
           
        }
 
        else if (Mode == "Add")
        {
            DivDiscountHistory.Visible = true;
            DivAddPanel.Visible = true;
            btnSave.Visible = true;

        }
    
    
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (string.IsNullOrEmpty(txtdiscount.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter Discount Value");
        
            return;
        }
        if (ddlSubgrp.SelectedValue =="")
        {
            Show_Error_Success_Box("E", "Select Subject Group");
          
            return;
        }
        if (DDLPayplan.SelectedValue == "")
        {
            Show_Error_Success_Box("E", "Select Condition Type");
          
            return;
        }
        if (DDlplan.SelectedValue == "")
        { 
            Show_Error_Success_Box("E", "Select Pay Plan");
            return;
        }


        if (txtValidity.Value == "")
        {
            Show_Error_Success_Box("E", "Enter Period ");
        }

        string subjectgrp = "";
        //centercode = ddlCenter.SelectedValue;

        for (int cnt = 0; cnt <= ddlSubgrp.Items.Count - 1; cnt++)
        {
            if (ddlSubgrp.Items[cnt].Selected == true)
            {
                subjectgrp = subjectgrp + ddlSubgrp.Items[cnt].Value + ",";
            }
        }
       

        if (subjectgrp == "")
        {
            Show_Error_Success_Box("E", "Atleast one subject should be selected");
            ddlSubgrp.Focus();
            return;
        }

       //subjectgrp = subjectgrp.Substring(0, subjectgrp.Length - 1);

        string DateRange = "";
        DateRange = txtValidity.Value;
        FDate = DateRange.Substring(0, 10);
        ToDate = DateRange.Substring(13, 10);


        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string resultid = "0";


        resultid = ProductController.Insert_discount_details(txtCourse.Text, txtbatch.Text, subjectgrp, DDLPayplan.SelectedValue,
         FDate, ToDate, txtdiscount.Text, DDlplan.SelectedValue, UserID, 3);

        if (resultid == "0")
        {
            Show_Error_Success_Box("S", "Record Saved Succesfully");
           
            DataSet dsGrid_previous = new DataSet();
            dsGrid_previous = ProductController.Get_discount_details(txtCourse.Text, txtbatch.Text, 4);
            if (dsGrid_previous != null)
            {
                if (dsGrid_previous.Tables[0].Rows.Count > 0)
                {
                    lbldlerror.Visible = false;
                    lbldlerror.Text = "";
                    DataList3.DataSource = dsGrid_previous;
                    DataList3.DataBind();
                    DataList3.Visible = true;
                  
                }
                else
                {
                    DataList3.DataSource = null;
                    DataList3.DataBind();
                    DataList3.Visible = false;
                    lbldlerror.Visible = true;
                    lbldlerror.Text = "No Discount Records Found";
                }
            }

        }

    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("SAP_Masterfile_Creation.aspx", false);
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

    private void FillDDL_subjectgrp()
    {
        {
            DataSet dssubjectgrp = ProductController.Get_ddlSubgrp(Session["Lblpk.Text"].ToString(),"","2");
            BindListBox(ddlSubgrp, dssubjectgrp, "Material_Name", "Material_Code");
            ddlSubgrp.Items.Insert(0, " ");
            ddlSubgrp.SelectedIndex = 0;

        }
    }
    private void FillDDL_payplan()
    {
        {
            DataSet dspayplan = ProductController.Get_ddlpayplan(Session["Lblpk.Text"].ToString(), "", "3");
            BindDDL(DDLPayplan, dspayplan, "Pay_Plan_name", "Condition_Type");
            DDLPayplan.Items.Insert(0, "Select");
            DDLPayplan.SelectedIndex = 0;

        }
    }

    private void FillDDL_plan()
    {
        {
            DataSet dsplan = ProductController.Get_ddlpayplan(Session["Lblpk.Text"].ToString(), "", "4");
            BindDDL(DDlplan, dsplan, "Pay_Plan_Code", "Pay_Plan_Description");
            DDlplan.Items.Insert(0, "Select");
            DDlplan.SelectedIndex = 0;

        }
    }



    public string ToDate { get; set; }

    public string FDate { get; set; }
}