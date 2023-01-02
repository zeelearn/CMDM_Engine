using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentMode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Cookies["MyCookiesLoginInfo"].Values["UserID"]))
        {
            if (!IsPostBack)
            {
                GetPaymentModeDetails();
                lbltotalcount.Text = Convert.ToString(PaymentModeGridView.Rows.Count);
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

    private void GetPaymentModeDetails()
    {
        #region Display Power backup type gridview

        DataSet PaymentModeDSet = new DataSet();
        PaymentModeDSet = Admin.DisplayAllPaymentMode();
        if (PaymentModeDSet.Tables[0].Rows.Count > 0)
        {
            PaymentModeGridView.DataSource = PaymentModeDSet.Tables[0];
            PaymentModeGridView.AutoGenerateColumns = false;
            PaymentModeGridView.DataBind();
        }
        else
        {
            PaymentModeGridView.DataSource = string.Empty;
            PaymentModeGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnPaymentModeSave.Visible = true;
        btnPaymentModeUpdate.Visible = false;
    }

    protected void btnPaymentModeSave_Click(object sender, EventArgs e)
    {
        string paymentModeID = txtPaymentModeID.Text;
        string paymentModeDescription = txtPaymentModeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoPaymentMode(paymentModeID, paymentModeDescription, createdBy,isActive);
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
            GetPaymentModeDetails();
        }
    }

    protected void btnPaymentModeUpdate_Click(object sender, EventArgs e)
    {
        string paymentModeID = txtPaymentModeID.Text;
        string paymentModeDescription = txtPaymentModeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdatePaymentMode(paymentModeID, paymentModeDescription, editedBy,isActive);
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
            btnPaymentModeCancel.Visible = false;
            txtPaymentModeID.Enabled = true;
            GetPaymentModeDetails();
        }
    }

    protected void PaymentModeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string paymentModeID = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = paymentModeID.Split('@');
            paymentModeID = splitIDs[0].ToString();
            string paymentModeDescription = splitIDs[1].ToString();
            int isActive = Convert.ToInt16(splitIDs[2].ToString());
            txtPaymentModeID.Text = paymentModeID;
            txtPaymentModeDescription.Text = paymentModeDescription;
            chkActive.Checked = (isActive == 1) ? false : true;
            btnPaymentModeSave.Visible = false;
            btnPaymentModeUpdate.Visible = true;
            txtPaymentModeID.Enabled = false;
            btnPaymentModeCancel.Visible = true;
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActivePaymentMode(paymentModeID, editedBy, 0);
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
                btnPaymentModeCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                txtPaymentModeID.Enabled = true;
                GetPaymentModeDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActivePaymentMode(paymentModeID, editedBy, 1);
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
                txtPaymentModeID.Enabled = true;
                btnPaymentModeCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetPaymentModeDetails();
            }
        }


    }

    protected void btnPaymentModeBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnPaymentModeCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnPaymentModeSave.Visible = true;
        btnPaymentModeUpdate.Visible = false;
        btnPaymentModeCancel.Visible = false;
        txtPaymentModeID.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        PaymentModeGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "PaymentMode_" + DateTime.Now + ".xls";
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

        PaymentModeGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < PaymentModeGridView.Rows.Count; i++)
        {
            GridViewRow row = PaymentModeGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnPaymentModeEdit").Visible = false;
        }
        PaymentModeGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        PaymentModeGridView.Visible = false;
    }
}