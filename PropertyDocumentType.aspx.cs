﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Cookies["MyCookiesLoginInfo"].Values["UserID"]))
        {
            if (!IsPostBack)
            {
                GetPropertyDocumentTypeDetails();
                lbltotalcount.Text = Convert.ToString(PropertyDocumentTypeGridView.Rows.Count);
            }
            else
            {
                diverror.Visible = false;
                divsuccess.Visible = false;
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    public static void ClearFields(ControlCollection pageControls)
    {
        foreach (Control contl in pageControls)
        {
            string strCntName = (contl.GetType()).Name;

            switch (strCntName)
            {
                case "TextBox":
                    TextBox tbSource = (TextBox)contl;
                    tbSource.Text = "";
                    break;
                case "RadioButtonList":
                    RadioButtonList rblSource = (RadioButtonList)contl;
                    rblSource.SelectedIndex = -1;
                    break;
                case "DropDownList":
                    DropDownList ddlSource = (DropDownList)contl;
                    ddlSource.SelectedIndex = -1;
                    break;
                case "ListBox":
                    ListBox lbsource = (ListBox)contl;
                    lbsource.SelectedIndex = -1;
                    break;
            }
            ClearFields(contl.Controls);
        }
    }

    private void GetPropertyDocumentTypeDetails()
    {
        #region Display Property document type gridview

        DataSet propdocTypeDSet = new DataSet();
        propdocTypeDSet = Admin.DisplayAllPropertyDocumentType();
        if (propdocTypeDSet.Tables[0].Rows.Count > 0)
        {
            PropertyDocumentTypeGridView.DataSource = propdocTypeDSet.Tables[0];
            PropertyDocumentTypeGridView.AutoGenerateColumns = false;
            PropertyDocumentTypeGridView.DataBind();
        }
        else
        {
            PropertyDocumentTypeGridView.DataSource = string.Empty;
            PropertyDocumentTypeGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnPropertyDocumentTypeSave.Visible = true;
        btnPropertyDocumentTypeUpdate.Visible = false;
    }

    protected void btnPropertyDocumentTypeSave_Click(object sender, EventArgs e)
    {
        string propertyDocTypeID = txtPropertyDocID.Text;
        string description = txtPropertyDocDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoPropertyDocumentType(propertyDocTypeID, description, createdBy,isActive);
        if (errorMessage.ToLower().Contains("error") || errorMessage.ToLower().Contains("already"))
        {
            divsuccess.Visible = false;
            diverror.Visible = true;
            lblerrormsg.Text = errorMessage;
        }
        else
        {
            diverror.Visible = false;
            divsuccess.Visible = true;
            lblsuccessmsg.Text = errorMessage;
            GetPropertyDocumentTypeDetails();
        }
    }

    protected void btnPropertyDocumentTypeUpdate_Click(object sender, EventArgs e)
    {
        string propertyDocTypeID = txtPropertyDocID.Text;
        string description = txtPropertyDocDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdatePropertyDocumentType(propertyDocTypeID, description, editedBy,isActive);
        if (errorMessage.ToLower().Contains("error"))
        {
            divsuccess.Visible = false;
            diverror.Visible = true;
            lblerrormsg.Text = errorMessage;
        }
        else
        {
            diverror.Visible = false;
            divsuccess.Visible = true;
            lblsuccessmsg.Text = errorMessage;
            btnPropertyDocumentTypeCancel.Visible = false;
            txtPropertyDocID.Enabled = true;
            GetPropertyDocumentTypeDetails();
        }
    }

    protected void PropertyDocumentTypeGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string propertyDocTypeID = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = propertyDocTypeID.Split('@');
            propertyDocTypeID = splitIDs[0].ToString();
            string description = splitIDs[1].ToString();
            int isActive = Convert.ToInt16(splitIDs[2].ToString());
            txtPropertyDocID.Text = propertyDocTypeID;
            txtPropertyDocDescription.Text = description;
            chkActive.Checked = (isActive == 1) ? false : true;
            btnPropertyDocumentTypeSave.Visible = false;
            btnPropertyDocumentTypeUpdate.Visible = true;
            btnPropertyDocumentTypeCancel.Visible = true    ;
            txtPropertyDocID.Enabled = false;
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActivePropertyDocumentType(propertyDocTypeID, editedBy, 0);
            if (errorMessage.ToLower().Contains("error"))
            {
                divsuccess.Visible = false;
                diverror.Visible = true;
                lblerrormsg.Text = errorMessage;
            }
            else
            {
                diverror.Visible = false;
                divsuccess.Visible = true;
                lblsuccessmsg.Text = errorMessage;
                btnPropertyDocumentTypeCancel.Visible = false;
                txtPropertyDocID.Enabled = true;
                GetPropertyDocumentTypeDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActivePropertyDocumentType(propertyDocTypeID, editedBy, 1);
            if (errorMessage.ToLower().Contains("error"))
            {
                divsuccess.Visible = false;
                diverror.Visible = true;
                lblerrormsg.Text = errorMessage;
            }
            else
            {
                diverror.Visible = false;
                divsuccess.Visible = true;
                txtPropertyDocID.Enabled = true;
                btnPropertyDocumentTypeCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetPropertyDocumentTypeDetails();
            }
        }


    }



    protected void btnPropertyDocumentTypeBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnPropertyDocumentTypeCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnPropertyDocumentTypeSave.Visible = true;
        btnPropertyDocumentTypeUpdate.Visible = false;
        btnPropertyDocumentTypeCancel.Visible = false;
        txtPropertyDocID.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        PropertyDocumentTypeGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "PropertyDocumentType_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Academic Year</b></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);

        PropertyDocumentTypeGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < PropertyDocumentTypeGridView.Rows.Count; i++)
        {
            GridViewRow row = PropertyDocumentTypeGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnPropertyDocumentTypeEdit").Visible = false;
        }
        PropertyDocumentTypeGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        PropertyDocumentTypeGridView.Visible = false;
    }
}