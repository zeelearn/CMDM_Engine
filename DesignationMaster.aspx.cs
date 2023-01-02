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
                GetDesignationMasterDetails();
                lbltotalcount.Text = Convert.ToString(DesignationMasterGridView.Rows.Count);
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

    private void GetDesignationMasterDetails()
    {
        DataSet dSet = new DataSet();
        dSet = Admin.DisplayAllDesignationMaster();
        if (dSet.Tables[0].Rows.Count > 0)
        {
            DesignationMasterGridView.DataSource = dSet.Tables[0];
            DesignationMasterGridView.AutoGenerateColumns = false;
            DesignationMasterGridView.DataBind();
        }
        else
        {
            DesignationMasterGridView.DataSource = string.Empty;
            DesignationMasterGridView.DataBind();
        }

        ClearFields(Form.Controls);

        btnDesignationMasterSave.Visible = true;
        btnDesignationMasterUpdate.Visible = false;
    }

    protected void BtnDesignationMasterBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnDesignationMasterSave_Click(object sender, EventArgs e)
    {
        string DesignationId = txtDesignationId.Text;
        string DesignationDescription = txtDesignationDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string CreatedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoDesignationMaster(DesignationId, DesignationDescription, CreatedBy,isActive);
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
            GetDesignationMasterDetails();
        }
    }

    protected void DesignationMasterGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DesignationId = Convert.ToString(e.CommandArgument);
        string EditedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            DataSet ds = Admin.GetDesignationMasterByDesignationId(DesignationId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDesignationId.Text = Convert.ToString(ds.Tables[0].Rows[0]["Designation_ID"]);
                txtDesignationDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Designation"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                btnDesignationMasterSave.Visible = false;
                btnDesignationMasterUpdate.Visible = true;
                txtDesignationId.Enabled = false;
            }
            divsuccess.Visible = false;
            diverror.Visible = false;
            btnDesignationMasterCancel.Visible = true;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveDesignationMaster(DesignationId, EditedBy, 0);
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
                txtDesignationId.Enabled = true;
                btnDesignationMasterCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetDesignationMasterDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveDesignationMaster(DesignationId, EditedBy, 1);
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
                txtDesignationId.Enabled = true;
                btnDesignationMasterCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetDesignationMasterDetails();
            }
        }


    }


    protected void btnDesignationMasterUpdate_Click(object sender, EventArgs e)
    {
        string designationId = txtDesignationId.Text;
        string dsignationDescription = txtDesignationDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateDesignationMaster(designationId, dsignationDescription, editedBy,isActive);
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
            txtDesignationId.Enabled = true;
            btnDesignationMasterCancel.Visible = false;
            GetDesignationMasterDetails();
        }

    }

    protected void btnDesignationMasterCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnDesignationMasterCancel.Visible = false;
        btnDesignationMasterUpdate.Visible = false;
        btnDesignationMasterSave.Visible = true;
        txtDesignationId.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DesignationMasterGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Designation_" + DateTime.Now + ".xls";
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

        DesignationMasterGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < DesignationMasterGridView.Rows.Count; i++)
        {
            GridViewRow row = DesignationMasterGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnDesignationMasterEdit").Visible = false;
        }
        DesignationMasterGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        DesignationMasterGridView.Visible = false;
    }
}
