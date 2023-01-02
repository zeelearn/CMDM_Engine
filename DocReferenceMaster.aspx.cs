using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DocReferenceMaster : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Cookies["MyCookiesLoginInfo"].Values["UserID"]))
        {
            if (!IsPostBack)
            {
                GetReferenceDocumentDetails();
                lbltotalcount.Text = Convert.ToString(ReferenceDocGridView.Rows.Count);
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

    private void GetReferenceDocumentDetails()
    {
        DataSet dSet = new DataSet();
        dSet = Admin.DisplayAllDocReferenceMaster();
        if (dSet.Tables[0].Rows.Count > 0)
        {
            ReferenceDocGridView.DataSource = dSet.Tables[0];
            ReferenceDocGridView.AutoGenerateColumns = false;
            //ReferenceDocGridView.Columns["Ref_Id"] = dSet.Tables[0].Rows[0][""];
            ReferenceDocGridView.DataBind();
        }
        else
        {
            ReferenceDocGridView.DataSource = string.Empty;
            ReferenceDocGridView.DataBind();
        }

        ClearFields(Form.Controls);

        btnDocReferenceMasterSave.Visible = true;
        btnDocReferenceMasterUpdate.Visible = false;
    }

    protected void BtnDocReferenceMasterBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnDocReferenceMasterSave_Click(object sender, EventArgs e)
    {
        string referenceID = txtReferenceId.Text;
        string referenceDocDesc = txtReferenceDocumentDescription.Text;
        int noRangeCountFrom = Convert.ToInt32(txtNoRangeCountFrom.Text);
        int noRangeCountTo = Convert.ToInt32(txtNoRangeCountTo.Text);
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoDocReferenceMaster(referenceID, referenceDocDesc, noRangeCountFrom, noRangeCountTo, createdBy,isActive);
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
            GetReferenceDocumentDetails();
            //Response.Redirect("~/DocReferenceMaster.aspx");
        }


    }

    protected void btnDocReferenceMasterUpdate_Click(object sender, EventArgs e)
    {
        string referenceID = txtReferenceId.Text;
        string referenceDocDesc = txtReferenceDocumentDescription.Text;
        int noRangeCountFrom = Convert.ToInt32(txtNoRangeCountFrom.Text);
        int noRangeCountTo = Convert.ToInt32(txtNoRangeCountTo.Text);
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateDocReferenceMaster(referenceID, referenceDocDesc, noRangeCountFrom, noRangeCountTo, editedBy,isActive);
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
            txtReferenceId.Enabled = true;
            GetReferenceDocumentDetails();
            btnDocReferenceMasterCancel.Visible = false;
        }

    }

    protected void ReferenceDocGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            string referenceID = Convert.ToString(e.CommandArgument);

            //GridViewRow selectedRow = ReferenceDocGridView.Rows[index];
            DataSet ds = Admin.GetDocReferenceMasterByRefID(referenceID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtReferenceId.Text = Convert.ToString(ds.Tables[0].Rows[0]["Ref_Id"]);
                txtReferenceDocumentDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Ref_Doc_Description"]);
                txtNoRangeCountFrom.Text = Convert.ToString(ds.Tables[0].Rows[0]["Number_Range_Count_From"]);
                txtNoRangeCountTo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Number_Range_Count_To"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                btnDocReferenceMasterSave.Visible = false;
                btnDocReferenceMasterUpdate.Visible = true;
                txtReferenceId.Enabled = false;
                btnDocReferenceMasterCancel.Visible = true;
            }
            divsuccess.Visible = false;
            diverror.Visible = false;
            //GetReferenceDocumentDetails();
        }
        else if (e.CommandName == "InActive")
        {
            string referenceID = Convert.ToString(e.CommandArgument);
            string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
            //GridViewRow selectedRow = ReferenceDocGridView.Rows[index];
            string errorMessage = Admin.ActiveInActiveDocReferenceMaster(referenceID, editedBy, 0);
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
                btnDocReferenceMasterCancel.Visible = false;
                txtReferenceId.Enabled = true;
                GetReferenceDocumentDetails();
                //Response.Redirect("~/DocReferenceMaster.aspx");
            }
        }
        else if (e.CommandName == "Active")
        {
            string referenceID = Convert.ToString(e.CommandArgument);
            string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
            //GridViewRow selectedRow = ReferenceDocGridView.Rows[index];
            string errorMessage = Admin.ActiveInActiveDocReferenceMaster(referenceID, editedBy, 1);
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
                btnDocReferenceMasterCancel.Visible = false;
                txtReferenceId.Enabled = true;
                GetReferenceDocumentDetails();
                //Response.Redirect("~/DocReferenceMaster.aspx");
            }
        }


    }


    protected void btnDocReferenceMasterCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnDocReferenceMasterCancel.Visible = false;
        btnDocReferenceMasterUpdate.Visible = false;
        btnDocReferenceMasterSave.Visible = true;
        txtReferenceId.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ReferenceDocGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Document_" + DateTime.Now + ".xls";
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

        ReferenceDocGridView.HeaderRow.Cells[5].Visible = false;
        for (int i = 0; i < ReferenceDocGridView.Rows.Count; i++)
        {
            GridViewRow row = ReferenceDocGridView.Rows[i];
            row.Cells[5].Visible = false;
            row.Cells[5].FindControl("btnDocReferenceMasterEdit").Visible = false;
        }
        ReferenceDocGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        ReferenceDocGridView.Visible = false;
    }
}