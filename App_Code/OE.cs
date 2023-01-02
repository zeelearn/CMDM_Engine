using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.Xml;
using System.Text;
using System.IO;
using LMSIntegration;
using ShoppingCart.BL;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
/// <summary>
/// Summary description for OE
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class OE : System.Web.Services.WebService
{

    public OE()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    #region Code to insert contacts LMS
    [WebMethod]
    public string Insert_Contacts
        (string UserCode, string FirstName, string LastName, string EmailId, string ContactNo, string username, string password)
    {
        string Message = "";
        string Message2 = "";
        using (SqlConnection con = new SqlConnection(DBConnection.connString))
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INSERT_UPDATE_CONTACTS_LMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = EmailId;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ContactNo;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@ReturnMessage";
                outParam.SqlDbType = System.Data.SqlDbType.VarChar;
                outParam.Direction = System.Data.ParameterDirection.Output;
                outParam.Size = 1000;
                cmd.Parameters.Add(outParam);
                cmd.ExecuteNonQuery();
                Message = outParam.Value.ToString();
                //Message2 = outParam1.Value.ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        return Message;

    }



    #region Code to insert contacts product in LMS
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Insert_User_Product
        (string UserCode, string ProductCode, string ProductType, string SubscriptionType, string TransactionId)
    {
        string Message = "";
        string jsonreturnstudent = "";
        string jsonreturnbatch = "";
        string SBEntryCode = "";
        DataTable studentdetails = new DataTable("studentdetails");
        DataTable studentbatchdetails = new DataTable("studentbatchdetails");
        using (SqlConnection con = new SqlConnection(DBConnection.connString))
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INSERT_UPDATE_USER_PRODUCT_LMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 1;
                cmd.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                cmd.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = ProductCode;
                cmd.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = ProductType;
                cmd.Parameters.Add("@SubscriptionType", SqlDbType.VarChar).Value = SubscriptionType;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@ReturnMessage";
                outParam.SqlDbType = System.Data.SqlDbType.VarChar;
                outParam.Direction = System.Data.ParameterDirection.Output;
                outParam.Size = 1000;
                cmd.Parameters.Add(outParam);


                SqlParameter outParamsbentrycode = new SqlParameter();
                outParamsbentrycode.ParameterName = "@ReturnMessage_SBEntryCode";
                outParamsbentrycode.SqlDbType = System.Data.SqlDbType.VarChar;
                outParamsbentrycode.Direction = System.Data.ParameterDirection.Output;
                outParamsbentrycode.Size = 1000;
                cmd.Parameters.Add(outParamsbentrycode);
                cmd.ExecuteNonQuery();
                Message = outParam.Value.ToString();
                SBEntryCode = outParamsbentrycode.Value.ToString();
                //Message2 = outParam1.Value.ToString();
                if (SBEntryCode != "")
                {
                    using (SqlCommand cmd1 = new SqlCommand("USP_GET_STUDENT_DETAILS_BY_USERCODE_LMS", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        //INPUT parameters
                        cmd1.Parameters.AddWithValue("@SBEntryCode", SBEntryCode);
                       

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
                                SqlParameter outParamcontact =new SqlParameter();
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
                            }                            
                        }
                        else
                        {
                            jsonreturnstudent = "Record Not Found";
                        }
                    }
                }
                else
                {
                    jsonreturnstudent = "Record Not Found";
                }
                con.Close();
            }


            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        return Message;

    }
    #region Code to insert taransaction for user in LMS
    [WebMethod]
    public string Insert_Payment_Details
        (string TransactionId, string UserCode, string Txn_Status, string Txn_Msg, string Txn_Err_Msg, string Clnt_Txn_Ref, string Tpsl_Bank_Cd,
        string Tpsl_Txn_Id, string Txn_Amt, string Clnt_Rqst_Meta, string Tpsl_Txn_Time, string Tpsl_Rfnd_Id, string Bal_Amt, string Rqst_Token)
    {
        string Message = "";

        using (SqlConnection con = new SqlConnection(DBConnection.connString))
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INSERT_UPDATE_CART_TRANSACTIONS_LMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TransactionId", SqlDbType.VarChar).Value = TransactionId;
                cmd.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = UserCode;
                cmd.Parameters.Add("@Txn_Status", SqlDbType.VarChar).Value = Txn_Status;
                cmd.Parameters.Add("@Txn_Msg", SqlDbType.VarChar).Value = Txn_Msg;
                cmd.Parameters.Add("@Txn_Err_Msg", SqlDbType.VarChar).Value = Txn_Err_Msg;
                cmd.Parameters.Add("@Clnt_Txn_Ref", SqlDbType.VarChar).Value = Clnt_Txn_Ref;
                cmd.Parameters.Add("@Tpsl_Bank_Cd", SqlDbType.VarChar).Value = Tpsl_Bank_Cd;
                cmd.Parameters.Add("@Tpsl_Txn_Id", SqlDbType.VarChar).Value = Tpsl_Txn_Id;
                cmd.Parameters.Add("@Txn_Amt", SqlDbType.VarChar).Value = Txn_Amt;
                cmd.Parameters.Add("@Clnt_Rqst_Meta", SqlDbType.VarChar).Value = Clnt_Rqst_Meta;
                cmd.Parameters.Add("@Tpsl_Txn_Time", SqlDbType.VarChar).Value = Tpsl_Txn_Time;
                cmd.Parameters.Add("@Tpsl_Rfnd_Id", SqlDbType.VarChar).Value = Tpsl_Rfnd_Id;
                cmd.Parameters.Add("@Bal_Amt", SqlDbType.VarChar).Value = Bal_Amt;
                cmd.Parameters.Add("@Rqst_Token", SqlDbType.VarChar).Value = Rqst_Token;

                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@ReturnMessage";
                outParam.SqlDbType = System.Data.SqlDbType.VarChar;
                outParam.Direction = System.Data.ParameterDirection.Output;
                outParam.Size = 1000;
                cmd.Parameters.Add(outParam);
                cmd.ExecuteNonQuery();
                Message = outParam.Value.ToString();
                //Message2 = outParam1.Value.ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        return Message;

    }



    //public DataTable Get_Student_Details_LMS(string UserCode)
    //{
    //    //MenuCode = MenuCode ?? "";
    //    string jsonreturn = "";
    //    DataTable studentdetails = new DataTable("studentdetails");
    //    //if (!string.IsNullOrEmpty(Flag) && !string.IsNullOrEmpty(UserCode))
    //    //{
    //    using (SqlConnection con = new SqlConnection(DBConnection.connString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("USP_GET_STUDENT_DETAILS_BY_USERCODE_LMS", con))
    //        {
    //            try
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                //INPUT parameters
    //                cmd.Parameters.AddWithValue("@UserCode", UserCode);
    //                con.Open();
    //                SqlDataAdapter da = new SqlDataAdapter(cmd);
    //                da.Fill(studentdetails);
    //                if (studentdetails.Rows.Count > 0)
    //                {
    //                    jsonreturn = ConvertDataTableTojSonString(studentdetails);
    //                }
    //                else
    //                {
    //                    jsonreturn = "Record Not Found";
    //                }

    //            }
    //            catch (Exception ex)
    //            {

    //            }
    //        }
    //    }
    //    return studentdetails;
    //}

    public String ConvertDataTableTojSonString(DataTable dataTable)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

        Dictionary<String, Object> row;

        foreach (DataRow dr in dataTable.Rows)
        {
            row = new Dictionary<String, Object>();
            foreach (DataColumn col in dataTable.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            tableRows.Add(row);
        }
        return serializer.Serialize(tableRows);
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
    #endregion
    #endregion
    #endregion
    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

}
