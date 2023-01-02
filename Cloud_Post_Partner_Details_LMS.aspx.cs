using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;

public partial class Cloud_Post_Partner_Details_LMSaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
          
            FillDDL_Country();
            FillDDL_Status();
            FillDDL_Division();
        }

    }

    private void FillDDL_City()
    {
        string State_Code = null;
        State_Code = ddlState.SelectedValue;

        DataSet dsCity = ProductController.GetAllActiveCity(State_Code);
        BindDDL(ddlCity, dsCity, "City_Name", "City_Code");
        ddlCity.Items.Insert(0, "Select");
        ddlCity.SelectedIndex = 0;
    }

    private void FillDDL_State()
    {
        string Country_Code = null;
        Country_Code = ddlCountry.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlState, dsState, "State_Name", "State_Code");
        ddlState.Items.Insert(0, "Select");
        ddlState.SelectedIndex = 0;
    }

    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_City();
        Clear_Error_Success_Box();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_State();
        Clear_Error_Success_Box();
    }
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            string Company_Code = "MT";
            string DBname = "CDB";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
            BindDDL(ddlDivision_Sr, dsDivision, "Division_Name", "Division_Code");
            ddlDivision_Sr.Items.Insert(0, "Select Division");
            ddlDivision_Sr.SelectedIndex = 0;


        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    private void FillDDL_Status()
    {
        ddlStatus.Items.Insert(0, "All");
        ddlStatus.Items.Insert(1, "Active");
        ddlStatus.Items.Insert(2, "In Active");
        ddlStatus.SelectedIndex = 0;

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void FillDDL_Country()
    {

        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlCountry, dsDivision, "Country_Name", "Country_Code");
        ddlCountry.Items.Insert(0, "Select");
        ddlCountry.SelectedIndex = 0;

       

    }


    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
        }
        
        Clear_Error_Success_Box();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //Validate if all information is entered correctly
        if (ddlCountry.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0040");
            ddlCountry.Focus();
            return;
        }

        if (ddlState.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0041");
            ddlState.Focus();
            return;
        }

        if (ddlCity.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0042");
            ddlCity.Focus();
            return;
        }


        string LocationCode = "";
        //Dim LocationCnt As Integer
        //Dim LocationSelCnt As Integer = 0
        //For LocationCnt = 0 To ddlLocation.Items.Count - 1
        //    If ddlLocation.Items(LocationCnt).Selected = True Then
        //        LocationSelCnt = LocationSelCnt + 1
        //    End If
        //Next

        //If LocationSelCnt = 0 Then
        //    'When all is selected
        //    For LocationCnt = 0 To ddlLocation.Items.Count - 1
        //        LocationCode = LocationCode & ddlLocation.Items(LocationCnt).Value & ","
        //    Next
        //    If Right(LocationCode, 1) = "," Then LocationCode = Left(LocationCode, Len(LocationCode) - 1)
        //Else
        //    For LocationCnt = 0 To ddlLocation.Items.Count - 1
        //        If ddlLocation.Items(LocationCnt).Selected = True Then
        //            LocationCode = LocationCode & ddlLocation.Items(LocationCnt).Value & ","
        //        End If
        //    Next
        //    If Right(LocationCode, 1) = "," Then LocationCode = Left(LocationCode, Len(LocationCode) - 1)
        //End If

        int ActiveStatus = 0;
        if (ddlStatus.SelectedIndex == 0)
        {
            ActiveStatus = -1;
        }
        else if (ddlStatus.SelectedIndex == 1)
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        ControlVisibility("Result");

        string CountryCode = null;
        CountryCode = ddlCountry.SelectedValue;

        string StateCode = null;
        StateCode = ddlState.SelectedValue;

        string CityCode = null;
        CityCode = ddlCity.SelectedValue;

        string PartnerName = null;
        if (string.IsNullOrEmpty(txtPartnerName.Text.Trim()))
        {
            PartnerName = "%";
        }
        else
        {
            PartnerName = "%" + txtPartnerName.Text.Trim();
        }

        string HandPhone = null;
        if (string.IsNullOrEmpty(txtHandPhone.Text.Trim()))
        {
            HandPhone = "%";
        }
        else
        {
            HandPhone = "%" + txtHandPhone.Text.Trim();
        }

        string div_code = "";
        if (ddlDivision_Sr.SelectedIndex == 0)
        {
            div_code = "%";
        }
        else
        {
            div_code = ddlDivision_Sr.SelectedValue.ToString().Trim();
        }

        DataSet dsGrid = ProductController.GetPartnerMasterBy_Country_State_City_Division(CountryCode, StateCode, CityCode, LocationCode, PartnerName, HandPhone, ActiveStatus, "2", div_code);
        if (dsGrid != null)
        {
            if (dsGrid.Tables[0].Rows.Count > 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();



                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
            else
            {
                Show_Error_Success_Box("E", "No Records Found For Selected Criteria");
                ControlVisibility("Search");
            }
        }
        else
        {
            lbltotalcount.Text = "0";
        }

    }
    protected void btnpostlms_Click(object sender, EventArgs e)
    {
        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            Label Partner_Code = (Label)dtlItem.FindControl("lblPartnerCode");

            Send_Details_LMS(Partner_Code.Text);

        }

        BtnSearch_Click(sender, e);

    }
    private void Send_Details_LMS(string Partner_Code)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        try
        {
            DataSet dsdetails = ProductController.GET_PARTNER_DETAILS(Partner_Code);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DBConnection.connStringLMS);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var partnerdetailsinsert = new partnerdetailsinsert();
                partnerdetailsinsert.TeacherCode = dsdetails.Tables[0].Rows[0]["Partner_Code"].ToString();
                partnerdetailsinsert.TeacherName = dsdetails.Tables[0].Rows[0]["TeacherName"].ToString();
                partnerdetailsinsert.Sex = dsdetails.Tables[0].Rows[0]["Sex"].ToString();
                partnerdetailsinsert.JoiningDate = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["DOJ"]);
                partnerdetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                partnerdetailsinsert.CreatedBy = dsdetails.Tables[0].Rows[0]["Created_By"].ToString();
                partnerdetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Modified_On"]);
                partnerdetailsinsert.ModifiedBy = dsdetails.Tables[0].Rows[0]["ModifiedBy"].ToString();
                partnerdetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsActive"]);
                partnerdetailsinsert.IsDeleted = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsDeleted"]);

                var response = client.PostAsJsonAsync("teacher/addUpdTeacherMaster", partnerdetailsinsert).Result;

                Response_Status_Code = response.StatusCode.ToString();
                Response_Return_Phrase = response.ReasonPhrase;

                if (response.StatusCode.ToString() == "OK")
                {

                    var partneraccountetailsinsert = new partneraccountetailsinsert();
                    partneraccountetailsinsert.TeacherCode = dsdetails.Tables[0].Rows[0]["Partner_Code"].ToString();
                    partneraccountetailsinsert.TeacherEmailId = dsdetails.Tables[0].Rows[0]["EMailId"].ToString();
                    partneraccountetailsinsert.TeacherLoginId = dsdetails.Tables[0].Rows[0]["Partner_Code"].ToString();
                    partneraccountetailsinsert.Password = dsdetails.Tables[0].Rows[0]["Password"].ToString();
                    partneraccountetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                    partneraccountetailsinsert.CreatedBy = dsdetails.Tables[0].Rows[0]["Created_By"].ToString();
                    partneraccountetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Modified_On"]);
                    partneraccountetailsinsert.ModifiedBy = dsdetails.Tables[0].Rows[0]["ModifiedBy"].ToString();
                    partneraccountetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsActive"]);
                    partneraccountetailsinsert.IsDeleted = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsDeleted"]);

                    var responseaccount = client.PostAsJsonAsync("teacher/addUpdTeacherAccountDetails", partneraccountetailsinsert).Result;

                    Response_Status_Code = response.StatusCode.ToString();
                    Response_Return_Phrase = response.ReasonPhrase;

                    if (responseaccount.StatusCode.ToString() == "OK")
                    {
                        DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, 1, Partner_Code, responseaccount.StatusCode.ToString(), responseaccount.ReasonPhrase, UserID);
                    }

                    else
                    {
                        DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, responseaccount.StatusCode.ToString(), responseaccount.ReasonPhrase, UserID);
                    }
                }
                else
                {
                    DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
                }
            }



        }
        catch (Exception e)
        {
            DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, Response_Status_Code, Response_Return_Phrase, UserID);
        }
    }


    class partnerdetailsinsert
    {
        public string TeacherCode { get; set; }
        public string TeacherName { get; set; }
        public string Sex { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }


    }
    class partneraccountetailsinsert
    {
        public string TeacherCode { get; set; }
        public string TeacherEmailId { get; set; }
        public string TeacherLoginId { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }


    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
}