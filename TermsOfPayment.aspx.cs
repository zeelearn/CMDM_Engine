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
                GetTermsOfPaymentDetails();
                lbltotalcount.Text = Convert.ToString(TermsOfPaymentGridView.Rows.Count);
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

    private void GetTermsOfPaymentDetails()
    {
        #region Display Terms of payment gridview

        DataSet paymentTermDSet = new DataSet();
        paymentTermDSet = Admin.DisplayAllTermsOfPayment();
        if (paymentTermDSet.Tables[0].Rows.Count > 0)
        {
            TermsOfPaymentGridView.DataSource = paymentTermDSet.Tables[0];
            TermsOfPaymentGridView.AutoGenerateColumns = false;
            TermsOfPaymentGridView.DataBind();
        }
        else
        {
            TermsOfPaymentGridView.DataSource = string.Empty;
            TermsOfPaymentGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnTermsOfPaymentSave.Visible = true;
        btnTermsOfPaymentUpdate.Visible = false;
    }

    protected void btnTermsOfPaymentSave_Click(object sender, EventArgs e)
    {
        string paymentKey = txtTermsOfPaymentKey.Text;
        string description = txtTermsOfPaymentDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoTermsOfPayment(paymentKey, description, createdBy,isActive);
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
            GetTermsOfPaymentDetails();
        }
    }

    protected void btnTermsOfPaymentUpdate_Click(object sender, EventArgs e)
    {
        string paymentKey = txtTermsOfPaymentKey.Text;
        string description = txtTermsOfPaymentDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateTermsOfPayment(paymentKey, description, editedBy,isActive);
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
            btnTermsOfPaymentCancel.Visible = false;
            txtTermsOfPaymentKey.Enabled = true;
            GetTermsOfPaymentDetails();
        }
    }

    protected void TermsOfPaymentGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string paymentKey = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = paymentKey.Split('@');
            paymentKey = splitIDs[0].ToString();
            string description = splitIDs[1].ToString();
            int isActive = Convert.ToInt16(splitIDs[2].ToString());
            txtTermsOfPaymentKey.Text = paymentKey;
            txtTermsOfPaymentDescription.Text = description;
            chkActive.Checked = (isActive == 1) ? false : true;
            btnTermsOfPaymentSave.Visible = false;
            btnTermsOfPaymentUpdate.Visible = true;
            txtTermsOfPaymentKey.Enabled = false;
            btnTermsOfPaymentCancel.Visible = true;
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveTermsOfPayment(paymentKey, editedBy, 0);
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
                txtTermsOfPaymentKey.Enabled = true;
                btnTermsOfPaymentCancel.Visible = false;
                GetTermsOfPaymentDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveTermsOfPayment(paymentKey, editedBy, 1);
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
                txtTermsOfPaymentKey.Enabled = true;
                btnTermsOfPaymentCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetTermsOfPaymentDetails();
            }
        }


    }

    protected void btnTermsOfPaymentBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnTermsOfPaymentCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnTermsOfPaymentCancel.Visible = false;
        btnTermsOfPaymentUpdate.Visible = false;
        btnTermsOfPaymentSave.Visible = true;
        txtTermsOfPaymentKey.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        TermsOfPaymentGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "TermsOfPayment_" + DateTime.Now + ".xls";
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

        TermsOfPaymentGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < TermsOfPaymentGridView.Rows.Count; i++)
        {
            GridViewRow row = TermsOfPaymentGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnTermsOfPaymentEdit").Visible = false;
        }
        TermsOfPaymentGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        TermsOfPaymentGridView.Visible = false;
    }
}