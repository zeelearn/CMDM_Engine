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
/// Summary description for Calling
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Calling : System.Web.Services.WebService {

    public Calling () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld() {
    //    return "Hello World";
    //}


    #region On Start
    [WebMethod]
    public string On_Start
        (string Phone, string User_Name, string User_Id, string User_Full_Name, string Agent_Access_Level, string Extension, string Extension_Type,
        string Cust_Id,string Leadset_Id,string Leadset_Name,string Campaign_Id,string Campaign_Name,string Process_Id,string Process_Name,string Customer_Json,
        string Process_Json,string Customer_Json_Key,string Process_Json_Key,string Cust_Unique_Id,string Reference_Id,string Date,string Time,string TimeStamp,
        string Mode_Of_Calling,string Wrap_Json)
    {
        string Message = "";
        using (SqlConnection con = new SqlConnection(DBConnection.connString))
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INSERT_UPDATE_CALL_LOG_ON_START", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@User_Id", SqlDbType.VarChar).Value = User_Id;
                cmd.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = User_Name;
                cmd.Parameters.Add("@User_Full_Name", SqlDbType.VarChar).Value = User_Full_Name;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Phone;
                cmd.Parameters.Add("@Agent_AccessLevel", SqlDbType.VarChar).Value = Agent_Access_Level;
                cmd.Parameters.Add("@Extension", SqlDbType.VarChar).Value = Extension;
                cmd.Parameters.Add("@Extension_Type", SqlDbType.VarChar).Value = Extension_Type;
                cmd.Parameters.Add("@Cust_Id", SqlDbType.VarChar).Value = Cust_Id;
                cmd.Parameters.Add("@Leadset_Id", SqlDbType.VarChar).Value = Leadset_Id;
                cmd.Parameters.Add("@Leadset_Name", SqlDbType.VarChar).Value = Leadset_Name;
                cmd.Parameters.Add("@Campaign_Id", SqlDbType.VarChar).Value = Campaign_Id;
                cmd.Parameters.Add("@Campaign_Name", SqlDbType.VarChar).Value = Campaign_Name;
                cmd.Parameters.Add("@Process_Id", SqlDbType.VarChar).Value = Process_Id;
                cmd.Parameters.Add("@Process_Name", SqlDbType.VarChar).Value = Process_Name;
                cmd.Parameters.Add("@Customer_JSON", SqlDbType.VarChar).Value = Customer_Json;
                cmd.Parameters.Add("@Process_JSON", SqlDbType.VarChar).Value = Process_Json;
                cmd.Parameters.Add("@Customer_JSON_Key", SqlDbType.VarChar).Value = Customer_Json_Key;
                cmd.Parameters.Add("@Process_JSON_Key", SqlDbType.VarChar).Value = Process_Json_Key;
                cmd.Parameters.Add("@Cust_Unique_Id", SqlDbType.VarChar).Value = Cust_Unique_Id;
                cmd.Parameters.Add("@Refernce_Id", SqlDbType.VarChar).Value = Reference_Id;
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = Date;
                cmd.Parameters.Add("@Time", SqlDbType.Time).Value = Time;
                cmd.Parameters.Add("@TimeStamp", SqlDbType.VarChar).Value = TimeStamp;
                cmd.Parameters.Add("@Mode_Of_Calling", SqlDbType.VarChar).Value = Mode_Of_Calling;
                cmd.Parameters.Add("@Wrap_Json", SqlDbType.VarChar).Value = Wrap_Json;
                              

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
     #region Call Disposed
    [WebMethod]
    public string Call_Disposed(string Dispose_Id,string Dispose_Name,string Dispose_Comment,string Agent_Talktime_Sec,string Que_Time,string Hold_Time,string Mute_Time,string Transfer_Time,
        string Conference_Time,string Customer_Name,string Customer_Email,string First_Dispose_Id,string First_Dispose_Name,string Second_Dispose_Id,string Second_Dispose_Name,
        string Third_Dispose_Id,string Third_Dispose_Name,string Call_Count,string Disconnected_By,string Start_Stamp,string Call_Back_Date)
    {
        string Message = "";
        using (SqlConnection con = new SqlConnection(DBConnection.connString))
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INSERT_UPDATE_CALL_LOG_CALL_DISPOSED", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Dispose_Id", SqlDbType.VarChar).Value = Dispose_Id;
                cmd.Parameters.Add("@Dispose_Name", SqlDbType.VarChar).Value = Dispose_Name;
                cmd.Parameters.Add("@Dispose_Comment", SqlDbType.VarChar).Value = Dispose_Comment;
                cmd.Parameters.Add("@Agent_Talktime_Sec", SqlDbType.VarChar).Value = Agent_Talktime_Sec;
                cmd.Parameters.Add("@Que_Time", SqlDbType.VarChar).Value = Que_Time;
                cmd.Parameters.Add("@Hold_Time", SqlDbType.VarChar).Value = Hold_Time;
                cmd.Parameters.Add("@Mute_Time", SqlDbType.VarChar).Value = Mute_Time;
                cmd.Parameters.Add("@Transfer_Time", SqlDbType.VarChar).Value = Transfer_Time;
                cmd.Parameters.Add("@Conference_Time", SqlDbType.VarChar).Value = Conference_Time;
                cmd.Parameters.Add("@Customer_Name", SqlDbType.VarChar).Value = Customer_Name;
                cmd.Parameters.Add("@Customer_Email", SqlDbType.VarChar).Value = Customer_Email;
                cmd.Parameters.Add("@First_Dispose_Id", SqlDbType.VarChar).Value = First_Dispose_Id;
                cmd.Parameters.Add("@First_Dispose_Name", SqlDbType.VarChar).Value = First_Dispose_Name;
                cmd.Parameters.Add("@Second_Dispose_Id", SqlDbType.VarChar).Value = Second_Dispose_Id;
                cmd.Parameters.Add("@Second_Dispose_Name", SqlDbType.VarChar).Value = Second_Dispose_Name;
                cmd.Parameters.Add("@Third_Dispose_Id", SqlDbType.VarChar).Value = Third_Dispose_Id;
                cmd.Parameters.Add("@Third_Dispose_Name", SqlDbType.VarChar).Value = Third_Dispose_Name;
                cmd.Parameters.Add("@Call_Count", SqlDbType.VarChar).Value = Call_Count;
                cmd.Parameters.Add("@Disconnected_By", SqlDbType.VarChar).Value = Disconnected_By;
                cmd.Parameters.Add("@Start_Stamp", SqlDbType.VarChar).Value = Start_Stamp;
                cmd.Parameters.Add("@Call_Back_Date", SqlDbType.VarChar).Value = Call_Back_Date;



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

    #endregion
    #endregion
}
