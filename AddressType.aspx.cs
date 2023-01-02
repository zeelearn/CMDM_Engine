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
                GetAddressTypeDetails();
                lbltotalcount.Text = Convert.ToString(AddressTypeGridView.Rows.Count);
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

    private void GetAddressTypeDetails()
    {
        DataSet dSet = new DataSet();
        dSet = Admin.DisplayAllAddressTypeMaster();
        if (dSet.Tables[0].Rows.Count > 0)
        {
            AddressTypeGridView.DataSource = dSet.Tables[0];
            AddressTypeGridView.AutoGenerateColumns = false;
            AddressTypeGridView.DataBind();
        }
        else
        {
            AddressTypeGridView.DataSource = string.Empty;
            AddressTypeGridView.DataBind();
        }

        ClearFields(Form.Controls);

        btnAddressTypeSave.Visible = true;
        btnAddressTypeUpdate.Visible = false;
    }

    protected void btnAddressTypeSave_Click(object sender, EventArgs e)
    {
        string addressID = txtAddressId.Text;
        string addressTypeDesc = txtTypeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.InsertIntoAddressTypeMaster(addressID, addressTypeDesc, createdBy,isActive);
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
            GetAddressTypeDetails();
        }
    }

    protected void btnAddressTypeUpdate_Click(object sender, EventArgs e)
    {
        string addressID = txtAddressId.Text;
        string addressTypeDesc = txtTypeDescription.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];
        string errorMessage = Admin.UpdateAddressTypeMaster(addressID, addressTypeDesc, editedBy,isActive);
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
            txtAddressId.Enabled = true;
            btnAddressTypeCancel.Visible = false;
            GetAddressTypeDetails();
        }

    }

    protected void AddressTypeGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string addressID = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            DataSet ds = Admin.GetAddressTypeMasterByAddressID(addressID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAddressId.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address_ID"]);
                txtTypeDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["Type_Description"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                btnAddressTypeSave.Visible = false;
                btnAddressTypeUpdate.Visible = true;
                txtAddressId.Enabled = false;
                btnAddressTypeCancel.Visible = true;
            }
            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveAddressTypeMaster(addressID, editedBy, 0);
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
                btnAddressTypeCancel.Visible = false;
                txtAddressId.Enabled = true;
                GetAddressTypeDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveAddressTypeMaster(addressID, editedBy, 1);
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
                btnAddressTypeCancel.Visible = false;
                txtAddressId.Enabled = true;
                GetAddressTypeDetails();
            }
        }


    }

    protected void BtnStatusMasterBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }
    protected void btnVerticalMasterCancel_Click(object sender, EventArgs e)
    {
         ClearFields(Form.Controls);
         btnAddressTypeCancel.Visible = false;
         btnAddressTypeUpdate.Visible = false;
         btnAddressTypeSave.Visible = true;
        txtAddressId.Enabled = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        AddressTypeGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "AddressType_" + DateTime.Now + ".xls";
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

        AddressTypeGridView.HeaderRow.Cells[3].Visible = false;
        for (int i = 0; i < AddressTypeGridView.Rows.Count; i++)
        {
            GridViewRow row = AddressTypeGridView.Rows[i];
            row.Cells[3].Visible = false;
            row.Cells[3].FindControl("btnAddressTypeEdit").Visible = false;
        }
        AddressTypeGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        AddressTypeGridView.Visible = false;
    }
}