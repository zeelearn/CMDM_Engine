using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VendorMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Cookies["MyCookiesLoginInfo"].Values["UserID"]))
        {
            if (!IsPostBack)
            {
                GetVendorInfoDetails();
                lbltotalcount.Text = Convert.ToString(VendorInfoGridView.Rows.Count);
            }
            else
            {
                diverror.Visible = false;
                divsuccess.Visible = false;
                txtVendorCode.Enabled = false;
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

    private void GetVendorInfoDetails()
    {
        #region display Vendor Type DDL

        DataSet vendorTypeDSet = new DataSet();
        vendorTypeDSet = Admin.DisplayAllVendorTypeMaster();
        if (vendorTypeDSet.Tables[0].Rows.Count > 0)
        {
            ddlTypeOfVendor.DataSource = vendorTypeDSet.Tables[0];
            ddlTypeOfVendor.DataValueField = "Vendor_type_Id";
            ddlTypeOfVendor.DataTextField = "Vendor_type_Description";
            ddlTypeOfVendor.DataBind();
            ddlTypeOfVendor.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlTypeOfVendor.DataSource = string.Empty;
            ddlTypeOfVendor.DataBind();
            ddlTypeOfVendor.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region display Compnay Code DDL

        DataSet companyCodeDSet = new DataSet();
        companyCodeDSet = Admin.DisplayAllCompany();
        if (companyCodeDSet.Tables[0].Rows.Count > 0)
        {
            ddlCompanyCode.DataSource = companyCodeDSet.Tables[0];
            ddlCompanyCode.DataValueField = "Company_Code";
            ddlCompanyCode.DataTextField = "Company_Name";
            ddlCompanyCode.DataBind();
            ddlCompanyCode.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlCompanyCode.DataSource = string.Empty;
            ddlCompanyCode.DataBind();
            ddlCompanyCode.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region display Address Id DDL

        DataSet AddressIdDSet = new DataSet();
        AddressIdDSet = Admin.DisplayAllAddressId();
        if (AddressIdDSet.Tables[0].Rows.Count > 0)
        {
            ddlAddressId.DataSource = AddressIdDSet.Tables[0];
            ddlAddressId.DataValueField = "Address_ID";
            ddlAddressId.DataTextField = "Address_ID";
            ddlAddressId.DataBind();
            ddlAddressId.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlAddressId.DataSource = string.Empty;
            ddlAddressId.DataBind();
            ddlAddressId.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region display Country DDL

        DataSet countryDSet = new DataSet();
        countryDSet = Admin.DisplayAllCountry();
        if (countryDSet.Tables[0].Rows.Count > 0)
        {
            ddlCountry.DataSource = countryDSet.Tables[0];
            ddlCountry.DataValueField = "Country_Code";
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlCountry.DataSource = string.Empty;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion

        #region Vendor Info gridview
        DataSet dvendorInfoDSet = new DataSet();
        dvendorInfoDSet = Admin.DisplayAllVendorInfo();
        if (dvendorInfoDSet.Tables[0].Rows.Count > 0)
        {
            VendorInfoGridView.DataSource = dvendorInfoDSet.Tables[0];
            VendorInfoGridView.AutoGenerateColumns = false;
            VendorInfoGridView.DataBind();
        }
        else
        {
            VendorInfoGridView.DataSource = string.Empty;
            VendorInfoGridView.DataBind();
        }
        #endregion

        ClearFields(Form.Controls);

        btnVendorInfoSave.Visible = true;
        btnVendorInfoUpdate.Visible = false;
        txtVendorCode.Enabled = false;
    }

    protected void btnVendorInfoSave_Click(object sender, EventArgs e)
    {
        string typeOfVendor = ddlTypeOfVendor.SelectedValue;
        string vendorCode = txtVendorCode.Text;
        string companyCode = ddlCompanyCode.SelectedValue;
        string name1 = txtName1.Text;
        string name2 = txtName2.Text;
        string name3 = txtName3.Text;
        string name4 = txtName4.Text;
        string companyName = txtNameOfCompany.Text;
        string PanNo = txtPanNo.Text;
        string email = txtEmail.Text;
        string telephone = txtTelephoneNo.Text;
        string mobileno = txtMobNo1.Text;

        string addressID = ddlAddressId.SelectedValue;
        string building = string.Empty;//txtbuil.Text;
        string floorNo = txtFloorNo.Text;
        string roomNo = txtRoomNo.Text;
        string country = ddlCountry.SelectedValue;
        string state = ddlState.SelectedValue;
        string city = ddlCity.SelectedValue;
        string postalCode = txtPostalCode.Text;
        string street = txtStreet1.Text;
        string houseNo = txtHouseNo.Text;
        string street2 = txtStreet2.Text;
        string street3 = txtStreet3.Text;
        string street4 = txtStreet4.Text;
        string street5 = string.Empty;//txtStr.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string createdBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        string errorMessage = Admin.InsertIntoVendorInfo(typeOfVendor, vendorCode, companyCode, name1, name2, name3, name4, companyName, PanNo,
        email, telephone, mobileno, addressID, building, floorNo, roomNo,
         country, state, city, postalCode, street, houseNo, street2, street3, street4, street5, createdBy,isActive);

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
            GetVendorInfoDetails();
        }
    }

    protected void btnVendorInfoUpdate_Click(object sender, EventArgs e)
    {
        string typeOfVendor = ddlTypeOfVendor.SelectedValue;
        string vendorCode = txtVendorCode.Text;
        string companyCode = ddlCompanyCode.SelectedValue;
        string name1 = txtName1.Text;
        string name2 = txtName2.Text;
        string name3 = txtName3.Text;
        string name4 = txtName4.Text;
        string companyName = txtNameOfCompany.Text;
        string PanNo = txtPanNo.Text;
        string email = txtEmail.Text;
        string telephone = txtTelephoneNo.Text;
        string mobileno = txtMobNo1.Text;

        string addressID = ddlAddressId.SelectedValue;
       // string building = Convert.ToString(string.Empty);//txtbuil.Text;
        string floorNo = txtFloorNo.Text;
        string roomNo = txtRoomNo.Text;
        string country = ddlCountry.SelectedValue;
        string state = ddlState.SelectedValue;
        string city = ddlCity.SelectedValue;
        string postalCode = txtPostalCode.Text;
        string street = txtStreet1.Text;
        string houseNo = txtHouseNo.Text;
        string street2 = txtStreet2.Text;
        string street3 = txtStreet3.Text;
        string street4 = txtStreet4.Text;
      //  string street5 = Convert.ToString(string.Empty);//txtStr.Text;
        int isActive = (chkActive.Checked == false) ? 1 : 0;
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        string errorMessage = Admin.UpdateVendorInfo(typeOfVendor, vendorCode, companyCode, name1, name2, name3, name4, companyName, PanNo,
                                                    email, telephone, mobileno, addressID, floorNo, roomNo,
                                                     country, state, city, postalCode, street, houseNo, street2, street3, street4, editedBy,isActive);

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
            txtVendorCode.Enabled = true;
            btnVendorInfoCancel.Visible = false;
            ddlTypeOfVendor.Enabled = true;
            GetVendorInfoDetails();
        }
    }

    protected void VendorInfoGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string vendorCode = Convert.ToString(e.CommandArgument);
        string editedBy = Request.Cookies["MyCookiesLoginInfo"].Values["UserID"];

        if (e.CommandName == "EditRow")
        {
            DataSet ds = Admin.GetVendorInfoByVendorCode(vendorCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfVendor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Type_Of_Vendor"]);
                txtVendorCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Vendor_code"]);
                ddlCompanyCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Company_Code"]);
                txtName1.Text = Convert.ToString(ds.Tables[0].Rows[0]["NAME1"]);
                txtName2.Text = Convert.ToString(ds.Tables[0].Rows[0]["NAME2"]);
                txtName3.Text = Convert.ToString(ds.Tables[0].Rows[0]["NAME3"]);
                txtName4.Text = Convert.ToString(ds.Tables[0].Rows[0]["NAME4"]);
                txtPanNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Pan_No"]);
                txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email_Address"]);
                txtTelephoneNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Telephone_No"]);
                txtMobNo1.Text = Convert.ToString(ds.Tables[0].Rows[0]["Mobile_No"]);
                ddlAddressId.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Address_ID"]);
                //txtBu.Text = Convert.ToString(ds.Tables[0].Rows[0]["Building"]);
                txtFloorNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Floor_No"]);
                txtRoomNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Room_Number"]);
                txtNameOfCompany.Text = Convert.ToString(ds.Tables[0].Rows[0]["Vendor_Company_Name"]);
                ddlCountry.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Country"]);

                ddlCountry_SelectedIndexChanged(sender, e);
                ddlState.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["State"]);

                ddlState_SelectedIndexChanged(sender, e);
                ddlCity.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["City"]);

                txtPostalCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Postal_Code"]);
                txtStreet1.Text = Convert.ToString(ds.Tables[0].Rows[0]["Street"]);
                txtHouseNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["House_Number"]);
                txtStreet2.Text = Convert.ToString(ds.Tables[0].Rows[0]["Street_2"]);
                txtStreet3.Text = Convert.ToString(ds.Tables[0].Rows[0]["Street_3"]);
                txtStreet4.Text = Convert.ToString(ds.Tables[0].Rows[0]["Street_4"]);
                chkActive.Checked = (Convert.ToInt16(ds.Tables[0].Rows[0]["Is_Active"]) == 1) ? false : true;
                
               

                btnVendorInfoSave.Visible = false;
                btnVendorInfoUpdate.Visible = true;
                ddlTypeOfVendor.Enabled = false;
                txtVendorCode.Enabled = false;
                btnVendorInfoCancel.Visible = true;
   
                
            }

            divsuccess.Visible = false;
            diverror.Visible = false;
        }
        else if (e.CommandName == "InActive")
        {
            string errorMessage = Admin.ActiveInActiveVendorInfo(vendorCode, editedBy, 0);
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
                btnVendorInfoCancel.Visible = false;
                ddlTypeOfVendor.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                GetVendorInfoDetails();
            }
        }
        else if (e.CommandName == "Active")
        {
            string errorMessage = Admin.ActiveInActiveVendorInfo(vendorCode, editedBy, 1);
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
                btnVendorInfoCancel.Visible = false;
                ddlTypeOfVendor.Enabled = true;
                lblsuccessmsg.Text = errorMessage;
                GetVendorInfoDetails();
            }
        }
    }

    protected void btnVendorInfoCancel_Click(object sender, EventArgs e)
    {
        ClearFields(Form.Controls);
        btnVendorInfoCancel.Visible = false;
        btnVendorInfoUpdate.Visible = false;
        btnVendorInfoSave.Visible = true;
        txtVendorCode.Enabled = true;
        ddlTypeOfVendor.Enabled = true;
    }
    
    protected void btnVendorInfoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AppMasters.aspx");
    }

    protected void btnAddNewAddress_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/VendorAddress.aspx");
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region display State DDL
        string countryCode = ddlCountry.SelectedValue;
        DataSet stateDSet = new DataSet();
        stateDSet = Admin.DisplayAllStateByCountryCode(countryCode);
        if (stateDSet.Tables[0].Rows.Count > 0)
        {
            ddlState.DataSource = stateDSet.Tables[0];
            ddlState.DataValueField = "State_Code";
            ddlState.DataTextField = "State_Name";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlState.DataSource = string.Empty;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        ddlState_SelectedIndexChanged(sender, e);

        #endregion
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region display City DDL
        string stateCode = ddlState.SelectedValue;
        DataSet cityDSet = new DataSet();
        cityDSet = Admin.DisplayAllCityByStateCode(stateCode);
        if (cityDSet.Tables[0].Rows.Count > 0)
        {
            ddlCity.DataSource = cityDSet.Tables[0];
            ddlCity.DataValueField = "City_Code";
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
        else
        {
            ddlCity.DataSource = string.Empty;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--No Data--", "--Select--"));
        }

        #endregion
    }

    protected void ddlTypeOfVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string vendorType = ddlTypeOfVendor.SelectedValue;
        
         DataSet DSet = new DataSet();
        DSet = Admin.GetVendorCodeNumberByVendorType(vendorType);
        if (DSet.Tables[0].Rows.Count > 0)
        {
            txtVendorCode.Text = DSet.Tables[0].Rows[0]["Current_Number_Range"].ToString();
        }
    }

    //protected void txtPanNo_TextChanged(object sender, EventArgs e)
    //{

    //}
    protected void btnExport_Click(object sender, EventArgs e)
    {
        VendorInfoGridView.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Vendor_" + DateTime.Now + ".xls";
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

        VendorInfoGridView.HeaderRow.Cells[9].Visible = false;
        for (int i = 0; i < VendorInfoGridView.Rows.Count; i++)
        {
            GridViewRow row = VendorInfoGridView.Rows[i];
            row.Cells[9].Visible = false;
            row.Cells[9].FindControl("btnVendorInfoEdit").Visible = false;
        }
        VendorInfoGridView.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        VendorInfoGridView.Visible = false;
    }
}


