using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using LMSIntegration;

public partial class Calling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
                string Phone = Request.QueryString["Phone"];
                string username = Request.QueryString["username"];
                string user_Id = Request.QueryString["user Id"];

                string Message = "";
                using (SqlConnection con = new SqlConnection(DBConnection.connString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("USP_INSERT_UPDATE_CALL_LOG_ON_START", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1;
                        cmd.Parameters.Add("@User_Id", SqlDbType.VarChar).Value = user_Id;
                        cmd.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@User_Full_Name", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Phone;
                        cmd.Parameters.Add("@Agent_AccessLevel", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Extension", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Extension_Type", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Cust_Id", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Leadset_Id", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Leadset_Name", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Campaign_Id", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Campaign_Name", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Process_Id", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Process_Name", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Customer_JSON", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Process_JSON", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Customer_JSON_Key", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Process_JSON_Key", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Cust_Unique_Id", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Refernce_Id", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Date", SqlDbType.Date).Value = "2016-07-08";
                        cmd.Parameters.Add("@Time", SqlDbType.Time).Value = "20:58:20.670";
                        cmd.Parameters.Add("@TimeStamp", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Mode_Of_Calling", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@Wrap_Json", SqlDbType.VarChar).Value = "";


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
           
            

        }   

    }
}