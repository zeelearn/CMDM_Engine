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
                GetStatusMasterDetails();
                lbltotalcount.Text = Convert.ToString(StatusGridView.Rows.Count);
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

    private void GetStatusMasterDetails()
    {
        #region display Document reference master DDL
        
        DataSet docRefDSet = new DataSet();
        docRefDSet = Admin.DisplayAllDocReferenceMaster();
        if (docRefDSet.Tables[0].Rows.Count > 0)
        {
            ddlReferenceId.DataSource = docRefDSet.Tables[0];
            ddlReferenceId.DataValueField = "Ref_ID";
            ddlReferenceId.DataTextField = "Ref_ID"; //DocumentReference
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

        #region Dispaly status master gridview
        
        DataSet statusMasterDSet = new DataSet();
        statusMasterDSet = Admin.DisplayAllStatusMaster();
        if (statusMasterDSet.Tables[0].Rows.Count > 0)
        {
            StatusGridView.DataSource = statusMasterDSet.Tables[0];
            StatusGridView.AutoGenerateColumns = false;
            StatusGridView.DataBind();
        }
        else
        {
            StatusGridView.DataSource = string.Empty;
            StatusGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnStatusMasterSave.Visible = true;
        btnStatusMasterUpdate.Visible = false;
    }

    protected void btnStatusMasterSave_Click(object sender, EventArgs e)
    {
        string referenceID = ddlReferenceId.SelectedValue.ToString();
        string statusID = txtStatusId.Text;
        string statusDescription = txtStatusDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoStatusMaster(referenceID, statusID, statusDescription, createdBy,isActive);
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
            GetStatusMasterDetails();
        }


    }

    protected void btnStatusMasterUpdate_Click(object sender, EventArgs e)
    {
        string referenceID = ddlReferenceId.SelectedValue;
        string statusDescription = txtStatusDescription.Text;
        string statusID = txtStatusId.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateStatusMaster(referenceID, statusID, statusDescription, editedBy,isActive);
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
            ddlReferenceId.Enabled = true;
            txtStatusId.Enabled = true;
            btnStatusMasterCancel.Visible = false;
            GetStatusMasterDetails();
        }
    }

    protected void StatusGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string refID = Convert.ToString(e.CommandArgument);
        string[] splitIDs = refID.Split('@');
        string referenceID = splitIDs[0].ToString();
        string statusID = splitIDs[1].ToString();
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            DataSet ds = Admin.GetStatusMasterByRefIDAndStatusID(referenceID, statusID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlReferenceId.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Ref_Id"]);
                txtStatusDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Status_Description"]);
                txtStatusId.Text = Convert.ToString(ds.Tables[0].Rows[0]["Status_ID"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                btnStatusMasterSave.Visible = false;
                btnStatusMasterUpdate.Visible = true;
                ddlReferenceId.Enabled = false;
                btnStatusMasterCancel.Visible = true;
                txtStatusId.Enabled = false;
            }
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveStatusMaster(referenceID, statusID, editedBy, 0);
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
                btnStatusMasterCancel.Visible = false;
                txtStatusId.Enabled = true;
                ddlReferenceId.Enabled = true;
                GetStatusMasterDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveStatusMaster(referenceID, statusID, editedBy, 1);
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
                btnStatusMasterCancel.Visible = false;
                txtStatusId.Enabled = true;
               ddlReferenceId.Enabled = true;
                GetStatusMasterDetails();
            }
        }
    }



    protected void btnStatusMasterBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnVerticalMasterCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnStatusMasterCancel.Visible = false;
        btnStatusMasterUpdate.Visible = false;
        btnStatusMasterSave.Visible = true;
        txtStatusId.Enabled = true;
        ddlReferenceId.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        StatusGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Status_" + DateTime.Now + ".xls";
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

        StatusGridView.HeaderRow.Cells[4].Visible = false;
        for (int i = 0; i < StatusGridView.Rows.Count; i++)
        {
            GridViewRow row = StatusGridView.Rows[i];
            row.Cells[4].Visible = false;
            row.Cells[4].FindControl("btnstatusMasterEdit").Visible = false;
        }
        StatusGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        StatusGridView.Visible = false;
    }
}