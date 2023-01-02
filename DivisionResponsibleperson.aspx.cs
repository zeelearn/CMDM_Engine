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
                GetDivisionResponsiblePersonDetails();
                lbltotalcount.Text = Convert.ToString(DivisionRespPersonGridView.Rows.Count);
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

    private void GetDivisionResponsiblePersonDetails()
    {
        #region display Division DDL

        DataSet divisionDSet = new DataSet();
        divisionDSet = Admin.DisplayAllDivision();
        if (divisionDSet.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataSource = divisionDSet.Tables[0];
           // ddlDivision.DataValueField = "Division_Code";
          //  ddlDivision.DataTextField = "DivisionDescription";
            ddlDivision.DataValueField = "Source_Division_Code";
            ddlDivision.DataTextField = "Source_Division_ShortDesc";
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlDivision.DataSource = string.Empty;
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region display Designation DDL

        DataSet designationDSet = new DataSet();
        designationDSet = Admin.DisplayAllDesignationMaster();
        if (designationDSet.Tables[0].Rows.Count > 0)
        {
            ddlDesignation.DataSource = designationDSet.Tables[0];
            ddlDesignation.DataValueField = "Designation_ID";
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlDesignation.DataSource = string.Empty;
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region Dispaly Division Responsible Person gridview

        DataSet divRespPersonDSet = new DataSet();
        divRespPersonDSet = Admin.DisplayAllDevisionResponsiblePerson();
        if (divRespPersonDSet.Tables[0].Rows.Count > 0)
        {
            DivisionRespPersonGridView.DataSource = divRespPersonDSet.Tables[0];
            DivisionRespPersonGridView.AutoGenerateColumns = false;
            DivisionRespPersonGridView.DataBind();
        }
        else
        {
            DivisionRespPersonGridView.DataSource = string.Empty;
            DivisionRespPersonGridView.DataBind();
        }

        #endregion

        ClearFields(Form.Controls);

        btnDivisionRespPersonSave.Visible = true;
        btnDivisionRespPersonUpdate.Visible = false;
    }

    protected void btnDivisionRespPersonSave_Click(object sender, EventArgs e)
    {
        string divisionID = ddlDivision.SelectedValue;
        string responsiblePersonID = txtResponsiblePersonId.Text;
        string personName = txtPersonName.Text;
        string designationID = ddlDesignation.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoDevisionResponsiblePerson(divisionID, responsiblePersonID, personName, designationID, createdBy,isActive);
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
            GetDivisionResponsiblePersonDetails();
        }


    }

    protected void btnDivisionRespPersonUpdate_Click(object sender, EventArgs e)
    {
        string divisionID = ddlDivision.SelectedValue;
        string responsiblePersonID = txtResponsiblePersonId.Text;
        string personName = txtPersonName.Text;
        string designationID = ddlDesignation.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateDevisionResponsiblePerson(divisionID, responsiblePersonID, personName,designationID, editedBy,isActive);
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
            txtResponsiblePersonId.Enabled = true;
            btnDivisionRespPersonCancel.Visible = false;
            GetDivisionResponsiblePersonDetails();
        }
    }

    protected void DivisionRespPersonGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string responsiblePersonID = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            string[] splitIDs = responsiblePersonID.Split('@');
            responsiblePersonID = splitIDs[0].ToString();
            string divisionCode = splitIDs[1].ToString();
            string personName = splitIDs[2].ToString();
            string designationID = splitIDs[3].ToString();
            int isActive = Convert.ToInt16(splitIDs[4].ToString());

            txtResponsiblePersonId.Text = responsiblePersonID;
            ddlDivision.SelectedValue = divisionCode;
            txtPersonName.Text = personName;
            ddlDesignation.SelectedValue = designationID;
            chkActive.Checked = (isActive == 1) ? false : true;

            btnDivisionRespPersonSave.Visible = false;
            btnDivisionRespPersonUpdate.Visible = true;
            txtResponsiblePersonId.Enabled = false;
            btnDivisionRespPersonCancel.Visible = true;

            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveDevisionResponsiblePerson(responsiblePersonID, editedBy, 0);
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
                txtResponsiblePersonId.Enabled = true;
                btnDivisionRespPersonCancel.Visible = false;
                lblsuccessmsg.Text = errorMessage;
                GetDivisionResponsiblePersonDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveDevisionResponsiblePerson(responsiblePersonID, editedBy, 1);
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
                btnDivisionRespPersonCancel.Visible = false;
                txtResponsiblePersonId.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                GetDivisionResponsiblePersonDetails();
            }
        }
    }

    protected void btnDivisionRespPersonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnDivisionRespPersonCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnDivisionRespPersonCancel.Visible = false;
        btnDivisionRespPersonSave.Visible = true;
        btnDivisionRespPersonUpdate.Visible = false;
        txtResponsiblePersonId.Enabled = true;
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DivisionRespPersonGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "DivisionResponsiblePerson_" + DateTime.Now + ".xls";
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

        DivisionRespPersonGridView.HeaderRow.Cells[5].Visible = false;
        for (int i = 0; i < DivisionRespPersonGridView.Rows.Count; i++)
        {
            GridViewRow row = DivisionRespPersonGridView.Rows[i];
            row.Cells[5].Visible = false;
            row.Cells[5].FindControl("btnDivisionRespPersonEdit").Visible = false;
        }
        DivisionRespPersonGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        DivisionRespPersonGridView.Visible = false;
    }
}