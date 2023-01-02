using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Data.SqlClient;
using LMSIntegration;
using System.Net.Http.Headers;
using System.Net.Http;

public partial class Clous_Post_Student_Details_LMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            ddlcriteria.Items.Add("Select");
            ddlcriteria.Items.Add("Contact and Batch Not Posted");
            ddlcriteria.Items.Add("Batch Not Posted");

        }
    }

    private void Page_Validation()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo Info = new System.IO.FileInfo(Path);
        string pageName = Info.Name;

        int ResultId = 0;

        ResultId = ProductController.Chk_Page_Validation(pageName, UserID, "DB00");

        if (ResultId >= 1)
        {
            //Allow
        }
        else
        {
            Response.Redirect("~/Homepage.aspx", false);
        }

    }


    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            Clear_Error_Success_Box();

            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;

        }
        else if (Mode == "Result")
        {
            //Clear_Error_Success_Box();

            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }


    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
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

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlcriteria.SelectedItem.Text == "Select")
        {
            Show_Error_Success_Box("E", "Select Critera");
        }
        else if (ddlcriteria.SelectedItem.Text == "Contact and Batch Not Posted")
        {
            using (SqlConnection con = new SqlConnection(DBConnection.connString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_GET_CLOUD_POSTING_ANALYSIS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@Contact_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Product_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Account_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Contatc_Cloud_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Batch_Cloud_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@From_Date", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@To_Date", SqlDbType.VarChar).Value = "";
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet result = new DataSet();
                    da.Fill(result);

                    if (result.Tables[0].Rows.Count > 0)
                    {
                        lbltotalcount.Text = result.Tables[0].Rows.Count.ToString();
                        ControlVisibility("Result");
                        dlGridDisplay.DataSource = result;
                        dlGridDisplay.DataBind();


                    }

                    else
                    {
                        Show_Error_Success_Box("E", "No Records Found For Selected Criteria");
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }

            }





        }
        else if (ddlcriteria.SelectedItem.Text == "Batch Not Posted")
        {


            using (SqlConnection con = new SqlConnection(DBConnection.connString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_GET_CLOUD_POSTING_ANALYSIS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 3;
                    cmd.Parameters.Add("@Contact_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Product_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Account_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Contatc_Cloud_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Batch_Cloud_Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@From_Date", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@To_Date", SqlDbType.VarChar).Value = "";
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet result = new DataSet();
                    da.Fill(result);

                    if (result.Tables[0].Rows.Count > 0)
                    {

                        lbltotalcount.Text = result.Tables[0].Rows.Count.ToString();
                        ControlVisibility("Result");
                        dlGridDisplay.DataSource = result;
                        dlGridDisplay.DataBind();


                    }

                    else
                    {
                        Show_Error_Success_Box("E", "No Records Found For Selected Criteria");
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }

            }

        }
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '%' });
            string UserCode = commandArgs[0];
            string SBEntrycode = commandArgs[1];
            string SPID = commandArgs[2];
            string ProductCode = commandArgs[3];
            string ProductType = commandArgs[4];
            string SubscriptionType = commandArgs[5];
            string TransactionId = commandArgs[6];



            // string record_id = e.CommandArgument.ToString();
            //lblprimarykey.Text = Convert.ToString(e.CommandArgument);
            //string record_id = Convert.ToString(lblprimarykey.Text);


            if (SBEntrycode.Length > 1)
            {
                using (SqlConnection con = new SqlConnection(DBConnection.connString))
                {
                    try
                    {
                        if (ddlcriteria.SelectedItem.Text == "Contact and Batch Not Posted")
                        {
                            con.Open();
                            DataTable studentdetails = new DataTable("studentdetails");
                            DataTable studentbatchdetails = new DataTable("studentbatchdetails");
                            using (SqlCommand cmd1 = new SqlCommand("USP_GET_STUDENT_DETAILS_BY_USERCODE_LMS", con))
                            {
                                cmd1.CommandType = CommandType.StoredProcedure;
                                //INPUT parameters
                                cmd1.Parameters.AddWithValue("@SBEntryCode", SBEntrycode);


                                SqlDataAdapter da = new SqlDataAdapter(cmd1);

                                DataSet dsstudentdetails = new DataSet();

                                da.Fill(dsstudentdetails);

                                studentdetails = dsstudentdetails.Tables[0];
                                studentbatchdetails = dsstudentdetails.Tables[1]; ;

                                if (studentdetails.Rows.Count > 0 && studentbatchdetails.Rows.Count > 0)
                                {
                                    HttpClient client = new HttpClient();
                                    client.BaseAddress = new Uri(DBConnection.connStringLMS);
                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    var registerUser = new registerUser();
                                    registerUser.usercode = dsstudentdetails.Tables[0].Rows[0]["usercode"].ToString();
                                    registerUser.flag = Convert.ToInt32(dsstudentdetails.Tables[0].Rows[0]["flag"]);
                                    registerUser.fName = dsstudentdetails.Tables[0].Rows[0]["fName"].ToString();
                                    registerUser.mName = dsstudentdetails.Tables[0].Rows[0]["mName"].ToString();
                                    registerUser.lName = dsstudentdetails.Tables[0].Rows[0]["lName"].ToString();
                                    registerUser.emailId = dsstudentdetails.Tables[0].Rows[0]["emailId"].ToString();
                                    registerUser.contactTypeCode = dsstudentdetails.Tables[0].Rows[0]["contactTypeCode"].ToString();
                                    registerUser.contactNo1 = dsstudentdetails.Tables[0].Rows[0]["contactNo1"].ToString();
                                    registerUser.contactNo2 = dsstudentdetails.Tables[0].Rows[0]["contactNo2"].ToString();
                                    registerUser.address1 = dsstudentdetails.Tables[0].Rows[0]["address1"].ToString();
                                    registerUser.pincode = dsstudentdetails.Tables[0].Rows[0]["pincode"].ToString();
                                    registerUser.cityCode = dsstudentdetails.Tables[0].Rows[0]["cityCode"].ToString();
                                    registerUser.stateCode = dsstudentdetails.Tables[0].Rows[0]["stateCode"].ToString();
                                    registerUser.countryCode = dsstudentdetails.Tables[0].Rows[0]["countryCode"].ToString();
                                    registerUser.studentCode = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString();
                                    registerUser.sbEntryCode = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                    registerUser.sbEntryCodeOld = dsstudentdetails.Tables[0].Rows[0]["sbEntryCodeOld"].ToString();
                                    registerUser.studentAccountId = dsstudentdetails.Tables[0].Rows[0]["studentAccountId"].ToString();
                                    registerUser.dateOfAdmission = Convert.ToDateTime(dsstudentdetails.Tables[0].Rows[0]["dateOfAdmission"].ToString());
                                    registerUser.totalFees = Convert.ToDecimal(dsstudentdetails.Tables[0].Rows[0]["totalFees"].ToString());
                                    registerUser.paymentMade = Convert.ToDecimal(dsstudentdetails.Tables[0].Rows[0]["paymentMade"].ToString());
                                    registerUser.paymentPending = Convert.ToDecimal(dsstudentdetails.Tables[0].Rows[0]["paymentPending"].ToString());
                                    registerUser.centerCodeJoining = dsstudentdetails.Tables[0].Rows[0]["centerCodeJoining"].ToString();
                                    registerUser.centerCodeCurrent = dsstudentdetails.Tables[0].Rows[0]["centerCodeCurrent"].ToString();
                                    registerUser.userReference = dsstudentdetails.Tables[0].Rows[0]["userReference"].ToString();
                                    registerUser.classRoomProductCode = dsstudentdetails.Tables[0].Rows[0]["classRoomProductCode"].ToString();
                                    registerUser.customerTypeCode = dsstudentdetails.Tables[0].Rows[0]["customerTypeCode"].ToString();
                                    registerUser.userTypeCode = dsstudentdetails.Tables[0].Rows[0]["userTypeCode"].ToString();

                                    var response = client.PostAsJsonAsync("User/registerUser", registerUser).Result;


                                    if (response.IsSuccessStatusCode)
                                    {
                                        SqlCommand cmdcontact = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                                        cmdcontact.CommandType = CommandType.StoredProcedure;
                                        cmdcontact.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 2;
                                        cmdcontact.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                                        cmdcontact.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                                        cmdcontact.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                                        cmdcontact.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                                        cmdcontact.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                                        cmdcontact.Parameters.Add("@SBentryCode_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                        cmdcontact.Parameters.Add("@SPID_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString(); ;
                                        cmdcontact.Parameters.Add("@Contact_Return_Reason_Phrase", SqlDbType.VarChar).Value = response.ReasonPhrase;
                                        cmdcontact.Parameters.Add("@Contact_Return_Error_Status_Code", SqlDbType.VarChar).Value = response.StatusCode;
                                        SqlParameter outParamcontact = new SqlParameter();
                                        outParamcontact.ParameterName = "@ReturnMessage";
                                        outParamcontact.SqlDbType = System.Data.SqlDbType.VarChar;
                                        outParamcontact.Direction = System.Data.ParameterDirection.Output;
                                        outParamcontact.Size = 1000;
                                        cmdcontact.Parameters.Add(outParamcontact);
                                        cmdcontact.ExecuteNonQuery();

                                        var registerUserBatch = new registerUserBatch();
                                        registerUserBatch.usercode = dsstudentdetails.Tables[1].Rows[0]["usercode"].ToString();
                                        registerUserBatch.flag = Convert.ToInt32(dsstudentdetails.Tables[1].Rows[0]["flag"]);
                                        registerUserBatch.studentCode = dsstudentdetails.Tables[1].Rows[0]["studentCode"].ToString();
                                        registerUserBatch.userReference = dsstudentdetails.Tables[1].Rows[0]["userReference"].ToString();
                                        registerUserBatch.centerCode = dsstudentdetails.Tables[1].Rows[0]["centerCode"].ToString();
                                        registerUserBatch.batchCode = dsstudentdetails.Tables[1].Rows[0]["batchCode"].ToString();
                                        registerUserBatch.productCode = dsstudentdetails.Tables[1].Rows[0]["productCode"].ToString();
                                        registerUserBatch.classRoomProductCode = dsstudentdetails.Tables[1].Rows[0]["classRoomProductCode"].ToString();
                                        registerUserBatch.installationMedia = dsstudentdetails.Tables[1].Rows[0]["installationMedia"].ToString();
                                        registerUserBatch.productPlatformCode = dsstudentdetails.Tables[1].Rows[0]["productPlatformCode"].ToString();
                                        registerUserBatch.productDeliveryModeCode = dsstudentdetails.Tables[1].Rows[0]["productDeliveryModeCode"].ToString();

                                        var responsebatch = client.PostAsJsonAsync("user/registerUserBatch", registerUserBatch).Result;

                                        if (responsebatch.IsSuccessStatusCode)
                                        {
                                            SqlCommand cmdbatch = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                                            cmdbatch.CommandType = CommandType.StoredProcedure;
                                            cmdbatch.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 3;
                                            cmdbatch.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                                            cmdbatch.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                                            cmdbatch.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                                            cmdbatch.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                                            cmdbatch.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                                            cmdbatch.Parameters.Add("@SBentryCode_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                            cmdbatch.Parameters.Add("@SPID_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString(); ;
                                            cmdbatch.Parameters.Add("@Batch_Return_Reason_Phrase", SqlDbType.VarChar).Value = responsebatch.ReasonPhrase;
                                            cmdbatch.Parameters.Add("@Batch_Return_Error_Status_Code", SqlDbType.VarChar).Value = responsebatch.StatusCode;
                                            SqlParameter outParambatch = new SqlParameter();
                                            outParambatch.ParameterName = "@ReturnMessage";
                                            outParambatch.SqlDbType = System.Data.SqlDbType.VarChar;
                                            outParambatch.Direction = System.Data.ParameterDirection.Output;
                                            outParambatch.Size = 1000;
                                            cmdbatch.Parameters.Add(outParambatch);
                                            cmdbatch.ExecuteNonQuery();

                                        }
                                        else
                                        {
                                            SqlCommand cmdbatch = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                                            cmdbatch.CommandType = CommandType.StoredProcedure;
                                            cmdbatch.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 3;
                                            cmdbatch.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                                            cmdbatch.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                                            cmdbatch.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                                            cmdbatch.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                                            cmdbatch.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                                            cmdbatch.Parameters.Add("@SBentryCode_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                            cmdbatch.Parameters.Add("@SPID_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString(); ;
                                            cmdbatch.Parameters.Add("@Batch_Return_Reason_Phrase", SqlDbType.VarChar).Value = responsebatch.ReasonPhrase;
                                            cmdbatch.Parameters.Add("@Batch_Return_Error_Status_Code", SqlDbType.VarChar).Value = responsebatch.StatusCode;
                                            SqlParameter outParambatch = new SqlParameter();
                                            outParambatch.ParameterName = "@ReturnMessage";
                                            outParambatch.SqlDbType = System.Data.SqlDbType.VarChar;
                                            outParambatch.Direction = System.Data.ParameterDirection.Output;
                                            outParambatch.Size = 1000;
                                            cmdbatch.Parameters.Add(outParambatch);
                                            cmdbatch.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        SqlCommand cmdcontact = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                                        cmdcontact.CommandType = CommandType.StoredProcedure;
                                        cmdcontact.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 2;
                                        cmdcontact.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                                        cmdcontact.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                                        cmdcontact.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                                        cmdcontact.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                                        cmdcontact.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                                        cmdcontact.Parameters.Add("@SBentryCode_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                        cmdcontact.Parameters.Add("@SPID_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString(); ;
                                        cmdcontact.Parameters.Add("@Contact_Return_Reason_Phrase", SqlDbType.VarChar).Value = response.ReasonPhrase;
                                        cmdcontact.Parameters.Add("@Contact_Return_Error_Status_Code", SqlDbType.VarChar).Value = response.StatusCode;
                                        SqlParameter outParamcontact = new SqlParameter();
                                        outParamcontact.ParameterName = "@ReturnMessage";
                                        outParamcontact.SqlDbType = System.Data.SqlDbType.VarChar;
                                        outParamcontact.Direction = System.Data.ParameterDirection.Output;
                                        outParamcontact.Size = 1000;
                                        cmdcontact.Parameters.Add(outParamcontact);
                                        cmdcontact.ExecuteNonQuery();

                                        Clear_Error_Success_Box();
                                    }
                                }
                                else
                                {
                                    Show_Error_Success_Box("E", "Batch Creation or Assignment Not Done Kindly Check");
                                 
                                }
                            }

                            BtnSearch_Click(source, e);
                        }
                        else if (ddlcriteria.SelectedItem.Text == "Batch Not Posted")
                        {
                            con.Open();
                            DataTable studentdetails = new DataTable("studentdetails");
                            DataTable studentbatchdetails = new DataTable("studentbatchdetails");
                            using (SqlCommand cmd1 = new SqlCommand("USP_GET_STUDENT_DETAILS_BY_USERCODE_LMS", con))
                            {
                                cmd1.CommandType = CommandType.StoredProcedure;
                                //INPUT parameters
                                cmd1.Parameters.AddWithValue("@SBEntryCode", SBEntrycode);


                                SqlDataAdapter da = new SqlDataAdapter(cmd1);

                                DataSet dsstudentdetails = new DataSet();

                                da.Fill(dsstudentdetails);

                                studentdetails = dsstudentdetails.Tables[0];
                                studentbatchdetails = dsstudentdetails.Tables[1]; ;

                                if (studentdetails.Rows.Count > 0 && studentbatchdetails.Rows.Count > 0)
                                {
                                    HttpClient client = new HttpClient();
                                    client.BaseAddress = new Uri(DBConnection.connStringLMS);
                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    var registerUserBatch = new registerUserBatch();
                                    registerUserBatch.usercode = dsstudentdetails.Tables[1].Rows[0]["usercode"].ToString();
                                    registerUserBatch.flag = Convert.ToInt32(dsstudentdetails.Tables[1].Rows[0]["flag"]);
                                    registerUserBatch.studentCode = dsstudentdetails.Tables[1].Rows[0]["studentCode"].ToString();
                                    registerUserBatch.userReference = dsstudentdetails.Tables[1].Rows[0]["userReference"].ToString();
                                    registerUserBatch.centerCode = dsstudentdetails.Tables[1].Rows[0]["centerCode"].ToString();
                                    registerUserBatch.batchCode = dsstudentdetails.Tables[1].Rows[0]["batchCode"].ToString();
                                    registerUserBatch.productCode = dsstudentdetails.Tables[1].Rows[0]["productCode"].ToString();
                                    registerUserBatch.classRoomProductCode = dsstudentdetails.Tables[1].Rows[0]["classRoomProductCode"].ToString();
                                    registerUserBatch.installationMedia = dsstudentdetails.Tables[1].Rows[0]["installationMedia"].ToString();
                                    registerUserBatch.productPlatformCode = dsstudentdetails.Tables[1].Rows[0]["productPlatformCode"].ToString();
                                    registerUserBatch.productDeliveryModeCode = dsstudentdetails.Tables[1].Rows[0]["productDeliveryModeCode"].ToString();

                                    var responsebatch = client.PostAsJsonAsync("user/registerUserBatch", registerUserBatch).Result;

                                    if (responsebatch.IsSuccessStatusCode)
                                    {
                                        SqlCommand cmdbatch = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                                        cmdbatch.CommandType = CommandType.StoredProcedure;
                                        cmdbatch.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 3;
                                        cmdbatch.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                                        cmdbatch.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                                        cmdbatch.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                                        cmdbatch.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                                        cmdbatch.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                                        cmdbatch.Parameters.Add("@SBentryCode_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                        cmdbatch.Parameters.Add("@SPID_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString(); ;
                                        cmdbatch.Parameters.Add("@Batch_Return_Reason_Phrase", SqlDbType.VarChar).Value = responsebatch.ReasonPhrase;
                                        cmdbatch.Parameters.Add("@Batch_Return_Error_Status_Code", SqlDbType.VarChar).Value = responsebatch.StatusCode;
                                        SqlParameter outParambatch = new SqlParameter();
                                        outParambatch.ParameterName = "@ReturnMessage";
                                        outParambatch.SqlDbType = System.Data.SqlDbType.VarChar;
                                        outParambatch.Direction = System.Data.ParameterDirection.Output;
                                        outParambatch.Size = 1000;
                                        cmdbatch.Parameters.Add(outParambatch);
                                        cmdbatch.ExecuteNonQuery();

                                    }
                                    else
                                    {
                                        SqlCommand cmdbatch = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                                        cmdbatch.CommandType = CommandType.StoredProcedure;
                                        cmdbatch.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 3;
                                        cmdbatch.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                                        cmdbatch.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                                        cmdbatch.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                                        cmdbatch.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                                        cmdbatch.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                                        cmdbatch.Parameters.Add("@SBentryCode_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["sbEntryCode"].ToString();
                                        cmdbatch.Parameters.Add("@SPID_P", SqlDbType.VarChar).Value = dsstudentdetails.Tables[0].Rows[0]["studentCode"].ToString(); ;
                                        cmdbatch.Parameters.Add("@Batch_Return_Reason_Phrase", SqlDbType.VarChar).Value = responsebatch.ReasonPhrase;
                                        cmdbatch.Parameters.Add("@Batch_Return_Error_Status_Code", SqlDbType.VarChar).Value = responsebatch.StatusCode;
                                        SqlParameter outParambatch = new SqlParameter();
                                        outParambatch.ParameterName = "@ReturnMessage";
                                        outParambatch.SqlDbType = System.Data.SqlDbType.VarChar;
                                        outParambatch.Direction = System.Data.ParameterDirection.Output;
                                        outParambatch.Size = 1000;
                                        cmdbatch.Parameters.Add(outParambatch);
                                        cmdbatch.ExecuteNonQuery();

                                        Clear_Error_Success_Box();
                                    }
                                }
                                else
                                {
                                    Show_Error_Success_Box("E", "Batch Creation or Assignment Not Done Kindly Check");

                                }
                            }

                        }
                       
                        BtnSearch_Click(source, e);
                    }

                    catch (Exception ex)
                    {
                        Show_Error_Success_Box("E", ex.ToString());
                    }
                }

            }
        }

    }




    class registerUser
    {
        public string usercode { get; set; }
        public int flag { get; set; }
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }
        public string emailId { get; set; }
        public string contactTypeCode { get; set; }
        public string contactNo1 { get; set; }
        public string contactNo2 { get; set; }
        public string address1 { get; set; }
        public string location { get; set; }
        public string pincode { get; set; }
        public string cityCode { get; set; }
        public string stateCode { get; set; }
        public string countryCode { get; set; }
        public string studentCode { get; set; }

        public string sbEntryCode { get; set; }
        public string sbEntryCodeOld { get; set; }
        public string studentAccountId { get; set; }
        public DateTime dateOfAdmission { get; set; }
        public decimal totalFees { get; set; }
        public decimal paymentMade { get; set; }
        public decimal paymentPending { get; set; }
        public string centerCodeJoining { get; set; }

        public string centerCodeCurrent { get; set; }
        public string userReference { get; set; }
        public string classRoomProductCode { get; set; }
        public string customerTypeCode { get; set; }
        public string userTypeCode { get; set; }

    }


    class registerUserBatch
    {
        public string usercode { get; set; }
        public int flag { get; set; }
        public string studentCode { get; set; }
        public string userReference { get; set; }
        public string centerCode { get; set; }
        public string batchCode { get; set; }
        public string productCode { get; set; }
        public string classRoomProductCode { get; set; }
        public string installationMedia { get; set; }
        public string productPlatformCode { get; set; }
        public string productDeliveryModeCode { get; set; }

    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlcriteria.SelectedIndex = 0;
    }
}