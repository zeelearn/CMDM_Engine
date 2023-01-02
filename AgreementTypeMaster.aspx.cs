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
                GetAgreementTypeMasterDetails();
                lbltotalcount.Text = Convert.ToString(AgreementTypeMasterGridView.Rows.Count);
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

    private void GetAgreementTypeMasterDetails()
    {
        DataSet dSet = new DataSet();
        dSet = Admin.DisplayAllAgreementTypeMaster();
        if (dSet.Tables[0].Rows.Count > 0)
        {
            AgreementTypeMasterGridView.DataSource = dSet.Tables[0];
            AgreementTypeMasterGridView.AutoGenerateColumns = false;
            AgreementTypeMasterGridView.DataBind();
        }
        else
        {
            AgreementTypeMasterGridView.DataSource = string.Empty;
            AgreementTypeMasterGridView.DataBind();
        }

        ClearFields(Form.Controls);

        btnAgreementTypeMasterSave.Visible = true;
        btnAgreementTypeMasterUpdate.Visible = false;
    }

    protected void BtnStatusMasterBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnAgreementTypeMasterSave_Click(object sender, EventArgs e)
    {
        string agreementTypeCode = txtAgreementTypeCode.Text;
        string agreementTypeDescription = txtAgreementTypeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoAgreementTypeMaster(agreementTypeCode, agreementTypeDescription, createdBy,isActive);
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
            GetAgreementTypeMasterDetails();
        }
    }

    protected void AgreementTypeMasterGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string agreementTypeCode = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            DataSet ds = Admin.GetAgreementTypeMasterByAgreementTypeCode(agreementTypeCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAgreementTypeCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Agreement_Type_Code"]);
                txtAgreementTypeDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Agreement_Type_Description"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                btnAgreementTypeMasterSave.Visible = false;
                btnAgreementTypeMasterUpdate.Visible = true;
                txtAgreementTypeCode.Enabled = false;
            }
            divsuccess.Visible = false;
            diverror.Visible = false;
            btnAgreementTypeMasterCancel.Visible = true;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveAgreementTypeMaster(agreementTypeCode, editedBy, 0);
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
                txtAgreementTypeCode.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                btnAgreementTypeMasterCancel.Visible = false;
                GetAgreementTypeMasterDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveAgreementTypeMaster(agreementTypeCode, editedBy, 1);
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
                txtAgreementTypeCode.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                btnAgreementTypeMasterCancel.Visible = false;
                GetAgreementTypeMasterDetails();
            }
        }
    }

    protected void btnAgreementTypeMasterCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnAgreementTypeMasterCancel.Visible = false;
        btnAgreementTypeMasterUpdate.Visible = false;
        btnAgreementTypeMasterSave.Visible = true;
        txtAgreementTypeCode.Enabled = true;
    }

    protected void btnAgreementTypeMasterUpdate_Click(object sender, EventArgs e)
    {
        string agreementTypeCode = txtAgreementTypeCode.Text;
        string agreementTypeDescription = txtAgreementTypeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateAgreementTypeMaster(agreementTypeCode, agreementTypeDescription, editedBy,isActive);
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
            txtAgreementTypeCode.Enabled = true;
            btnAgreementTypeMasterCancel.Visible = false;
            GetAgreementTypeMasterDetails();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        AgreementTypeMasterGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "AgreementType_" + DateTime.Now + ".xls";
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

        AgreementTypeMasterGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < AgreementTypeMasterGridView.Rows.Count; i++)
        {
            GridViewRow row = AgreementTypeMasterGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnAgreementTypeMasterEdit").Visible = false;
        }
        AgreementTypeMasterGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        AgreementTypeMasterGridView.Visible = false;
    }
}