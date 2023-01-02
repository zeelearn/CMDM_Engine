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
                GetPaymentTypeMasterDetails();
                lbltotalcount.Text = Convert.ToString(PaymentTypeMasterGridView.Rows.Count);
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

    private void GetPaymentTypeMasterDetails()
    {
        #region Display payment type master gridview

        DataSet paymentTypeDSet = new DataSet();
        paymentTypeDSet = Admin.DisplayAllPaymentTypeMaster();
        if (paymentTypeDSet.Tables[0].Rows.Count > 0)
        {
            PaymentTypeMasterGridView.DataSource = paymentTypeDSet.Tables[0];
            PaymentTypeMasterGridView.AutoGenerateColumns = false;
            PaymentTypeMasterGridView.DataBind();
        }
        else
        {
            PaymentTypeMasterGridView.DataSource = string.Empty;
            PaymentTypeMasterGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnPaymentTypeSave.Visible = true;
        btnPaymentTypeUpdate.Visible = false;
    }

    protected void btnPaymentTypeSave_Click(object sender, EventArgs e)
    {
        string paymentCode = txtPaymentCode.Text;
        string description = txtPaymentDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoPaymentTypeMaster(paymentCode, description, createdBy,isActive);
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
            GetPaymentTypeMasterDetails();
        }
    }

    protected void btnPaymentTypeUpdate_Click(object sender, EventArgs e)
    {
        string paymentCode = txtPaymentCode.Text;
        string description = txtPaymentDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdatePaymentTypeMaster(paymentCode, description, editedBy,isActive);
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
            txtPaymentCode.Enabled = true;
            btnPaymentTypeCancel.Visible = false;
            GetPaymentTypeMasterDetails();
        }
    }

    protected void PaymentTypeMasterGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string paymentCode = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = paymentCode.Split('@');
            paymentCode = splitIDs[0].ToString();
            string description = splitIDs[1].ToString();
            int isActive = Convert.ToInt16(splitIDs[2].ToString());
            txtPaymentCode.Text = paymentCode;
            txtPaymentDescription.Text = description;
            chkActive.Checked = (isActive == 1) ? false : true;
            btnPaymentTypeSave.Visible = false;
            btnPaymentTypeUpdate.Visible = true;
            txtPaymentCode.Enabled = false;
            btnPaymentTypeBack.Visible = true;
            btnPaymentTypeCancel.Visible = true;
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActivePaymentTypeMaster(paymentCode, editedBy, 0);
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
                txtPaymentCode.Enabled = true;
                btnPaymentTypeCancel.Visible = false;
                GetPaymentTypeMasterDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActivePaymentTypeMaster(paymentCode, editedBy, 1);
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
                txtPaymentCode.Enabled = true;
                btnPaymentTypeCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetPaymentTypeMasterDetails();
            }
        }


    }

    protected void btnPaymentTypeBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnPaymentTypeCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnPaymentTypeCancel.Visible = false;
        btnPaymentTypeUpdate.Visible = false;
        btnPaymentTypeSave.Visible = true;
        txtPaymentCode.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        PaymentTypeMasterGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "PaymentType_" + DateTime.Now + ".xls";
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

        PaymentTypeMasterGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < PaymentTypeMasterGridView.Rows.Count; i++)
        {
            GridViewRow row = PaymentTypeMasterGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnPaymentTypeMasterEdit").Visible = false;
        }
        PaymentTypeMasterGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        PaymentTypeMasterGridView.Visible = false;
    }
}