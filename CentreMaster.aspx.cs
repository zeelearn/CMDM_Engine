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
                 GetCenterMasterDetails();
                 lbltotalcount.Text = Convert.ToString(CenterMasterGridView.Rows.Count);
            }
            else
            {
                //diverror.Visible = false;
               // divsuccess.Visible = false;
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

    private void GetCenterMasterDetails()
    {
        #region display Center Code DDL

        DataSet SenterCodeDSet = new DataSet();
        SenterCodeDSet = Admin.DisplayCenterCode();
        if (SenterCodeDSet.Tables[0].Rows.Count > 0)
        {
            ddlCenterCode.DataSource = SenterCodeDSet.Tables[0];
            ddlCenterCode.DataValueField = "Center_Code";
            ddlCenterCode.DataTextField = "Center_Name";
            ddlCenterCode.DataBind();
            ddlCenterCode.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlCenterCode.DataSource = string.Empty;
            ddlCenterCode.DataBind();
            ddlCenterCode.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region display Premises Id DDL

        DataSet PremisesIdDSet = new DataSet();
        PremisesIdDSet = Admin.DisplaypremisesId();
        if (PremisesIdDSet.Tables[0].Rows.Count > 0)
        {
            ddlPremicesId.DataSource = PremisesIdDSet.Tables[0];
            ddlPremicesId.DataValueField = "Premise_ID";
            ddlPremicesId.DataTextField = "Primise_Description";
            ddlPremicesId.DataBind();
            ddlPremicesId.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlPremicesId.DataSource = string.Empty;
            ddlPremicesId.DataBind();
            ddlPremicesId.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region center Master  gridview
        DataSet dvendorInfoDSet = new DataSet();
        dvendorInfoDSet = Admin.DisplayAllCenterMaster();
        if (dvendorInfoDSet.Tables[0].Rows.Count > 0)
        {
            CenterMasterGridView.DataSource = dvendorInfoDSet.Tables[0];
            CenterMasterGridView.AutoGenerateColumns = false;
            CenterMasterGridView.DataBind();
        }
        else
        {
            CenterMasterGridView.DataSource = string.Empty;
            CenterMasterGridView.DataBind();
        }
        #endregion

        ClearFields(Form.Controls);

        btnCenterMasterSave.Visible = true;
        btnCenterMasterUpdate.Visible = false;

    }

    protected void BtnStatusMasterBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnCenterMasterSave_Click(object sender, EventArgs e)
    {
        string centerCode = ddlCenterCode.SelectedValue;
        string PremisesId = ddlPremicesId.SelectedValue;
        string contactNo = txtContactNo.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoCenterMaster(centerCode, PremisesId, contactNo, createdBy,isActive);
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
            GetCenterMasterDetails();
        }
    }

    protected void btnCenterMasterUpdate_Click(object sender, EventArgs e)
    {
        string centerCode = ddlCenterCode.SelectedValue;
        string PremisesId = ddlPremicesId.SelectedValue;
        string contactNo = txtContactNo.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateCenterMaster(centerCode, PremisesId, contactNo, editedBy,isActive);
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
            btnCenterMasterCancel.Visible = false;
            ddlCenterCode.Enabled = true;
            ddlPremicesId.Enabled = true;
            GetCenterMasterDetails();
        }
    }
    protected void CenterMasterGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            string[] CommandArg=Convert.ToString(e.CommandArgument).Split(',');
            string centerCode = CommandArg[0];
            string PremiseId = CommandArg[1];
            //GridViewRow selectedRow = ReferenceDocGridView.Rows[index];
            DataSet ds = Admin.GetCenterMasterByCentreCode(centerCode, PremiseId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCenterCode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Centre_Code"]);
                ddlPremicesId.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Premise_ID"]);
                txtContactNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Contact_No"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                btnCenterMasterSave.Visible = false;
                btnCenterMasterUpdate.Visible = true;
                ddlCenterCode.Enabled = true;
                ddlPremicesId.Enabled = true;
                btnCenterMasterCancel.Visible = true;
            }
            divsuccess.Visible = false;
            diverror.Visible = false;
            //GetReferenceDocumentDetails();
        }
        else if (e.CommandName == "InActive")
        {
            string centerCode = Convert.ToString(e.CommandArgument);
            string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
            //GridViewRow selectedRow = ReferenceDocGridView.Rows[index];
            string errorMessage = Admin.ActiveInActiveCenterMaster(centerCode, editedBy, 0);
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
                btnCenterMasterCancel.Visible = false;
                ddlCenterCode.Enabled = true;
                ddlPremicesId.Enabled = true;
                //Response.Redirect("~/DocReferenceMaster.aspx");
            }
        }
        else if (e.CommandName == "Active")
        {
            string centerCode = Convert.ToString(e.CommandArgument);
            string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
            //GridViewRow selectedRow = ReferenceDocGridView.Rows[index];
            string errorMessage = Admin.ActiveInActiveCenterMaster(centerCode, editedBy, 1);
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
                btnCenterMasterCancel.Visible = false;
                ddlCenterCode.Enabled = true;
                ddlPremicesId.Enabled = true;
                //Response.Redirect("~/DocReferenceMaster.aspx");
            }
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        CenterMasterGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Center_" + DateTime.Now + ".xls";
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

        CenterMasterGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < CenterMasterGridView.Rows.Count; i++)
        {
            GridViewRow row = CenterMasterGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnCenterMasterEdit").Visible = false;
        }
        CenterMasterGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        CenterMasterGridView.Visible = false;
    }
}