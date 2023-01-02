using System;
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
                GetPowerBackupTypeDetails();
                lbltotalcount.Text = Convert.ToString(PowerBackupTypGridView.Rows.Count);
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

    private void GetPowerBackupTypeDetails()
    {
        #region Display Power backup type gridview

        DataSet pbtypeDSet = new DataSet();
        pbtypeDSet = Admin.DisplayAllPowerBackupType();
        if (pbtypeDSet.Tables[0].Rows.Count > 0)
        {
            PowerBackupTypGridView.DataSource = pbtypeDSet.Tables[0];
            PowerBackupTypGridView.AutoGenerateColumns = false;
            PowerBackupTypGridView.DataBind();
        }
        else
        {
            PowerBackupTypGridView.DataSource = string.Empty;
            PowerBackupTypGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnPowerBackupTypeSave.Visible = true;
        btnPowerBackupTypeUpdate.Visible = false;
    }

    protected void btnPowerBackupTypeSave_Click(object sender, EventArgs e)
    {
        string backupTypeID = txtPowerBackupTypeId.Text;
        string description = txtPowerBackupTypeDescreption.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoPowerBackupType(backupTypeID, description, createdBy,isActive);
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
            GetPowerBackupTypeDetails();
        }
    }

    protected void btnPowerBackupTypeUpdate_Click(object sender, EventArgs e)
    {
        string backupTypeID = txtPowerBackupTypeId.Text;
        string description = txtPowerBackupTypeDescreption.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdatePowerBackupType(backupTypeID, description, editedBy,isActive);
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
            btnPowerBackupTypeCancel.Visible = false;
            txtPowerBackupTypeId.Enabled = true;
            GetPowerBackupTypeDetails();
        }
    }

    protected void PowerBackupTypGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string backupTypeID = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = backupTypeID.Split('@');
            backupTypeID = splitIDs[0].ToString();
            string description = splitIDs[1].ToString();
            int isActive = Convert.ToInt16(splitIDs[2].ToString());

            txtPowerBackupTypeId.Text = backupTypeID;
            txtPowerBackupTypeDescreption.Text = description;
            chkActive.Checked = (isActive == 1) ? false : true;

            btnPowerBackupTypeSave.Visible = false;
            btnPowerBackupTypeUpdate.Visible = true;
            txtPowerBackupTypeId.Enabled = false;
            btnPowerBackupTypeCancel.Visible = true;
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActivePowerBackupType(backupTypeID, editedBy, 0);
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
                btnPowerBackupTypeCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                txtPowerBackupTypeId.Enabled = true;
                GetPowerBackupTypeDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActivePowerBackupType(backupTypeID, editedBy, 1);
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
                txtPowerBackupTypeId.Enabled = true;
                btnPowerBackupTypeCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetPowerBackupTypeDetails();
            }
        }


    }

    protected void btnPowerBackupTypeBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnPowerBackupTypeCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnPowerBackupTypeSave.Visible = true;
        btnPowerBackupTypeUpdate.Visible = false;
        btnPowerBackupTypeCancel.Visible = false;
        txtPowerBackupTypeId.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        PowerBackupTypGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "PowerBackupType_" + DateTime.Now + ".xls";
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

        PowerBackupTypGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < PowerBackupTypGridView.Rows.Count; i++)
        {
            GridViewRow row = PowerBackupTypGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnPowerBackupTypEdit").Visible = false;
        }
        PowerBackupTypGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        PowerBackupTypGridView.Visible = false;
    }
}