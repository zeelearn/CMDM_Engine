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
                GetPremisesTypeDetails();
                lbltotalcount.Text = Convert.ToString(PremisesTypeGridView.Rows.Count);
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

    private void GetPremisesTypeDetails()
    {
        #region Display DDL of ReferenceId from M700_Doc_Reference_Master

        DataSet ReferenceIdDSet = new DataSet();
        ReferenceIdDSet = Admin.DisplayReferencId();
        if (ReferenceIdDSet.Tables[0].Rows.Count > 0)
        {
            ddlReferenceId.DataSource = ReferenceIdDSet.Tables[0];
            ddlReferenceId.DataValueField = "Ref_Id";
            ddlReferenceId.DataTextField = "Ref_Id";
            ddlReferenceId.DataBind();
            ddlReferenceId.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlReferenceId.DataSource = string.Empty;
            ddlReferenceId.DataBind();
            ddlReferenceId.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion 

        #region Display premises type gridview

        DataSet premisesDSet = new DataSet();
        premisesDSet = Admin.DisplayAllPremisesType();
        if (premisesDSet.Tables[0].Rows.Count > 0)
        {
            PremisesTypeGridView.DataSource = premisesDSet.Tables[0];
            PremisesTypeGridView.AutoGenerateColumns = false;
            PremisesTypeGridView.DataBind();
        }
        else
        {
            PremisesTypeGridView.DataSource = string.Empty;
            PremisesTypeGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnPremisesTypeSave.Visible = true;
        btnPremisesTypeUpdate.Visible = false;
    }


    protected void btnPremisesTypeSave_Click(object sender, EventArgs e)
    {
        string premisesTypeId = txtPremisesTypeId.Text;
        string refId = ddlReferenceId.SelectedValue;
        string premisesTypeDesc = txtPremisesTypeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoPremisesType(premisesTypeId, refId, premisesTypeDesc, createdBy,isActive);
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
            GetPremisesTypeDetails();
        }
    }

    protected void btnPremisesTypeUpdate_Click(object sender, EventArgs e)
    {
        string premisesTypeId = txtPremisesTypeId.Text;
        string refId = ddlReferenceId.SelectedValue;
        string premisesTypeDesc = txtPremisesTypeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdatePremisesType(premisesTypeId, refId, premisesTypeDesc, editedBy,isActive);
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
            txtPremisesTypeId.Enabled = true;
            btnPremisesTypeCancel.Visible = false;
            GetPremisesTypeDetails();
        }
    }

    protected void PremisesTypeGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string premisesTypeID = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = premisesTypeID.Split('@');
            premisesTypeID = splitIDs[0].ToString();
            string premisesDesc = splitIDs[1].ToString();
            string referenceId = splitIDs[2].ToString();
            int isActive = Convert.ToInt16(splitIDs[3].ToString());

            txtPremisesTypeId.Text = premisesTypeID;
            ddlReferenceId.SelectedValue = referenceId;
            txtPremisesTypeDescription.Text = premisesDesc;
            chkActive.Checked = (isActive == 1) ? false : true;
            btnPremisesTypeSave.Visible = false;
            btnPremisesTypeUpdate.Visible = true;
            txtPremisesTypeId.Enabled = false;
            btnPremisesTypeCancel.Visible = true;
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActivePremisesType(premisesTypeID, editedBy, 0);
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
                btnPremisesTypeCancel.Visible = false;
                txtPremisesTypeId.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                GetPremisesTypeDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActivePremisesType(premisesTypeID, editedBy, 1);
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
                btnPremisesTypeCancel.Visible = false;
                txtPremisesTypeId.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                GetPremisesTypeDetails();
            }
        }


    }

    protected void btnPremisesTypeBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnDesignationMasterCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnPremisesTypeCancel.Visible = false;
        btnPremisesTypeUpdate.Visible = false;
        btnPremisesTypeSave.Visible = true;
        txtPremisesTypeId.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        PremisesTypeGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "PremisesType_" + DateTime.Now + ".xls";
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

        PremisesTypeGridView.HeaderRow.Cells[4].Visible = false;
        for (int i = 0; i < PremisesTypeGridView.Rows.Count; i++)
        {
            GridViewRow row = PremisesTypeGridView.Rows[i];
            row.Cells[4].Visible = false;
            row.Cells[4].FindControl("btnpremisesTypeEdit").Visible = false;
        }
        PremisesTypeGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        PremisesTypeGridView.Visible = false;
    }
}