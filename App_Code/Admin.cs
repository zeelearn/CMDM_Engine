using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;


using System.Diagnostics;
using System.Text;
using ShoppingCart.DAL;

using ShoppingCart.BL;



/// <summary>
/// Summary description for Admin
/// </summary>
public class Admin
{
	public Admin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet getDataSet(string sql)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
       // SqlConnection conn = new SqlConnection(ConnectionString.GetConnectionString());
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CONSTR"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        conn.Open();
        da.Fill(ds);
        conn.Close();
        conn.Dispose();
        da.Dispose();
        return ds;
    }

    #region M700_Doc_Reference_Master

    public static string InsertIntoDocReferenceMaster(string refID, string refDocDescription, int noRangeCountFrom, int noRangeCountTo, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M700_Doc_Reference_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RefId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@RefDocDescription", SqlDbType.VarChar).Value = refDocDescription;
                    cmd.Parameters.Add("@NoRangeCountFrom", SqlDbType.Int).Value = noRangeCountFrom;
                    cmd.Parameters.Add("@NoRangeCountTo", SqlDbType.Int).Value = noRangeCountTo;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }
        
    }

    public static string UpdateDocReferenceMaster(string refID, string refDocDescription, int noRangeCountFrom, int noRangeCountTo, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByRefID_M700_Doc_Reference_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RefId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@RefDocDescription", SqlDbType.VarChar).Value = refDocDescription;
                    cmd.Parameters.Add("@NoRangeCountFrom", SqlDbType.Int).Value = noRangeCountFrom;
                    cmd.Parameters.Add("@NoRangeCountTo", SqlDbType.Int).Value = noRangeCountTo;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveDocReferenceMaster(string refID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByRefID_M700_Doc_Reference_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RefId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllDocReferenceMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M700_Doc_Reference_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllDocReferenceMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
           
        }

        return ds;
    }

    public static DataSet GetDocReferenceMasterByRefID(string refID)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByRefID_M700_Doc_Reference_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RefId", SqlDbType.NVarChar).Value = refID;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }
           
        }
        catch (Exception ex)
        {
           
        }

        return ds;
    }

    #endregion M700_Doc_Reference_Master

    #region M701_Status_Master

    public static string InsertIntoStatusMaster(string refID, string statusID,string statusDescription, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_StatusMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReferenceId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@StatusId", SqlDbType.VarChar).Value = statusID;
                    cmd.Parameters.Add("@StatusDescription", SqlDbType.VarChar).Value = statusDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateStatusMaster(string refID, string statusID, string statusDescription, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByRefIDAndStatusID_M701_Status_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReferenceId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@StatusId", SqlDbType.VarChar).Value = statusID;
                    cmd.Parameters.Add("@StatusDescription", SqlDbType.VarChar).Value = statusDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveStatusMaster(string refID, string statusID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByRefIDAndStatus_ID_M701_Status_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReferenceID", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@StatusID", SqlDbType.VarChar).Value = statusID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllStatusMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M701_Status_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllStatusMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetStatusMasterByRefIDAndStatusID(string refID, string statusID)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByRefIDAndStatusID_M701_Status_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReferenceID", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@StatusID", SqlDbType.VarChar).Value = statusID;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M701_Status_Master

    #region M702_Address_Type

    public static string InsertIntoAddressTypeMaster(string addressID, string typeDescription, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M702_Address_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AddressId", SqlDbType.NVarChar).Value = addressID;
                    cmd.Parameters.Add("@TypeDescription", SqlDbType.VarChar).Value = typeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateAddressTypeMaster(string addressID, string typeDescription, string editedBy,int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByAddressID_M702_Address_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AddressId", SqlDbType.NVarChar).Value = addressID;
                    cmd.Parameters.Add("@TypeDescription", SqlDbType.VarChar).Value = typeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveAddressTypeMaster(string addressID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByAddressID_M702_Address_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AddressId", SqlDbType.NVarChar).Value = addressID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllAddressTypeMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M702_Address_Type");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllAddressTypeMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetAddressTypeMasterByAddressID(string addressID)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByAddressId_M702_Address_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AddressID", SqlDbType.NVarChar).Value = addressID;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M702_Address_Type

    #region M703_Vendor_Type_Master

    public static string InsertIntoVendorTypeMaster(string refID, string vendorTypeID, string vendorTypeDescription, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M703_Vendor_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReferenceId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@VendorTypeId", SqlDbType.NVarChar).Value = vendorTypeID;
                    cmd.Parameters.Add("@VendorTypeDescription", SqlDbType.VarChar).Value = vendorTypeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateVendorTypeMaster(string refID, string vendorTypeID, string vendorTypeDescription, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByVendorTypeID_M703_VendorType_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReferenceId", SqlDbType.NVarChar).Value = refID;
                    cmd.Parameters.Add("@VendorTypeId", SqlDbType.NVarChar).Value = vendorTypeID;
                    cmd.Parameters.Add("@VendorTypeDescription", SqlDbType.VarChar).Value = vendorTypeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveVendorTypeMaster(string vendorTypeID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByVendortypeId_M703_Vendor_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VendorTypeId", SqlDbType.VarChar).Value = vendorTypeID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllVendorTypeMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M703_Vendor_Type_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllVendorTypeMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetVendorTypeMasterByVendorTypeID(string vendorTypeID)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByVendor_typeId_M703_Vendor_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VendortypeId", SqlDbType.NVarChar).Value = vendorTypeID;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M703_Vendor_Type_Master

    #region M705_Designation_Master

    public static string InsertIntoDesignationMaster(string DesignationId, string DesignationDescription, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_DesignationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DesignationId", SqlDbType.NVarChar).Value = DesignationId;
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = DesignationDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateDesignationMaster(string designationId, string dsignationDescription, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_M705_Designation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DesignationId", SqlDbType.NVarChar).Value = designationId;
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = dsignationDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveDesignationMaster(string designationId, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByDesignationId_M703_VendorM705_Designation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DesignationID", SqlDbType.NVarChar).Value = designationId;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllDesignationMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M705_Designation_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllDesignationMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetDesignationMasterByDesignationId(string DesignationID)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByDesignationID_M705_Designation_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DesignationID", SqlDbType.NVarChar).Value = DesignationID;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M705_Designation_Master

    #region M706_Division_Responsible_person

    public static string InsertIntoDevisionResponsiblePerson(string divisionCode, string respPersonId, string personName, string designationID, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_Division_Responsible_person", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar).Value = divisionCode;
                    cmd.Parameters.Add("@RespPerId", SqlDbType.VarChar).Value = respPersonId;
                    cmd.Parameters.Add("@PersonName", SqlDbType.VarChar).Value = personName;
                    cmd.Parameters.Add("@DesignationId", SqlDbType.VarChar).Value = designationID;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateDevisionResponsiblePerson(string divisionCode, string respPersonId, string personName, string designationID, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByRespPersonID_M706_Division_Responsible_person", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DivisionCode", SqlDbType.NVarChar).Value = divisionCode;
                    cmd.Parameters.Add("@RespPerId", SqlDbType.NVarChar).Value = respPersonId;
                    cmd.Parameters.Add("@PersonName", SqlDbType.VarChar).Value = personName;
                    cmd.Parameters.Add("@DesignationId", SqlDbType.VarChar).Value = designationID;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveDevisionResponsiblePerson(string respPersonId, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByRespPersonId_M706_Division_Responsible_person", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RespPersonId", SqlDbType.VarChar).Value = respPersonId;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllDevisionResponsiblePerson()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M706_Division_Responsible_person");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllDevisionResponsiblePerson", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayAllDivision()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
           // ds = getDataSet("SELECT * FROM UV_Select_C007_Division");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllDivision", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M706_Division_Responsible_person

    #region M707A_Premises_Type

    public static string InsertIntoPremisesType(string premisesTypeID,string refid, string premisesDesc, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M707A_Premises_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PremisesTypeId", SqlDbType.NVarChar).Value = premisesTypeID;
                    cmd.Parameters.Add("@RefId", SqlDbType.NVarChar).Value = refid;
                    cmd.Parameters.Add("@PremisesTypeDescription", SqlDbType.VarChar).Value = premisesDesc;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePremisesType(string premisesTypeID,string refId, string premisesDesc, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPremisesTypeID_M707A_Premises_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PremisesTypeId", SqlDbType.NVarChar).Value = premisesTypeID;
                    cmd.Parameters.Add("@RefId", SqlDbType.NVarChar).Value = refId;
                    cmd.Parameters.Add("@PremisesTypeDescription", SqlDbType.VarChar).Value = premisesDesc;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePremisesType(string premisesTypeID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPremiseTypeID_M707A_Premises_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PremiseTypeId", SqlDbType.NVarChar).Value = premisesTypeID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllPremisesType()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M707A_Premises_Type");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPremisesType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayReferencId()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M700_Doc_Reference_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayReferencId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M707A_Premises_Type

    #region M708_Agreement_Type_Master

    public static string InsertIntoAgreementTypeMaster(string agreementTypeCode, string agreementTypeDescription, string createdBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M708_Agreement_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AgreementTypeCode", SqlDbType.VarChar).Value = agreementTypeCode;
                    cmd.Parameters.Add("@AgreementTypeDescription", SqlDbType.VarChar).Value = agreementTypeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = createdBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateAgreementTypeMaster(string agreementTypeCode, string agreementTypeDescription, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByAgreementTypeCode_M708_Agreement_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AgreementTypeCode", SqlDbType.NVarChar).Value = agreementTypeCode;
                    cmd.Parameters.Add("@AgreementTypeDescription", SqlDbType.VarChar).Value = agreementTypeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveAgreementTypeMaster(string agreementTypeCode, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByAgreementTypeCode_M702_Address_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AgreementTypeCode", SqlDbType.NVarChar).Value = agreementTypeCode;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllAgreementTypeMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M708_Agreement_Type_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllAgreementTypeMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetAgreementTypeMasterByAgreementTypeCode(string agreementTypeCode)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByAgreementTypeCode_M708_Agreement_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AgreementTypeCode", SqlDbType.VarChar).Value = agreementTypeCode;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M708_Agreement_Type_Master

    #region M709_Period_Master

    public static string InsertIntoPeriodMaster(string periodCode, string periodDesc, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M709Period_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PeriodCode", SqlDbType.VarChar).Value = periodCode;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = periodDesc;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePeriodMaster(string periodCode, string periodDesc, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPeriodCode_M709_Period_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PeriodCode", SqlDbType.NVarChar).Value = periodCode;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = periodDesc;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePeriodMaster(string periodCode, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPeriodCode_M709_Period_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PeriodCode", SqlDbType.VarChar).Value = periodCode;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllPeriodMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M709_Period_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPeriodMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M709_Period_Master

    #region M710_Payment_Type_Master

    public static string InsertIntoPaymentTypeMaster(string paymentCode, string paymentDesc, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_PaymentType_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentCode", SqlDbType.VarChar).Value = paymentCode;
                    cmd.Parameters.Add("@PaymentDescription", SqlDbType.VarChar).Value = paymentDesc;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePaymentTypeMaster(string paymentCode, string paymentDesc, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPeriodCode_M710_Payment_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentCode", SqlDbType.NVarChar).Value = paymentCode;
                    cmd.Parameters.Add("@PaymentDescription", SqlDbType.VarChar).Value = paymentDesc;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePaymentTypeMaster(string paymentCode, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPaymentCode_M710_Payment_Type_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentCode", SqlDbType.VarChar).Value = paymentCode;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllPaymentTypeMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M710_Payment_Type_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPaymentTypeMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M710_Payment_Type_Master

    #region M711_Terms_Of_Payment

    public static string InsertIntoTermsOfPayment(string paymentKey, string description, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M711_TermsOfPayment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TermsOfPaymentKey", SqlDbType.VarChar).Value = paymentKey;
                    cmd.Parameters.Add("@TermsOfPaymentDescription", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateTermsOfPayment(string paymentKey, string description, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPaymentKey_M711_Terms_Of_Payment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TermsOfPaymentKey", SqlDbType.VarChar).Value = paymentKey;
                    cmd.Parameters.Add("@TermsOfPaymentDescription", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveTermsOfPayment(string paymentKey, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPaymentKey_M711_Terms_Of_Payment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TermsofPaymentKey", SqlDbType.VarChar).Value = paymentKey;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllTermsOfPayment()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M711_Terms_Of_Payment");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllTermsOfPayment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M711_Terms_Of_Payment

    #region M713_Power_Backup_Type

    public static string InsertIntoPowerBackupType(string backupTypeID, string description, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M713_PowerBackupType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BackupTypeId", SqlDbType.VarChar).Value = backupTypeID;
                    cmd.Parameters.Add("@BackupTypeDescription", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePowerBackupType(string backupTypeID, string description, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByBackupTypeID_M713_Power_Backup_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BackupTypeId", SqlDbType.VarChar).Value = backupTypeID;
                    cmd.Parameters.Add("@BackupTypeDescription", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePowerBackupType(string backupTypeID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByBakupId_M713_Power_Backup_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BakupTypeId", SqlDbType.VarChar).Value = backupTypeID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllPowerBackupType()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M713_Power_Backup_Type");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPowerBackupType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M713_Power_Backup_Type

    #region M714_Property_Document_Type

    public static string InsertIntoPropertyDocumentType(string propertyDocID, string description, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M714_PropertyDocumentType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PropertyDocId", SqlDbType.VarChar).Value = propertyDocID;
                    cmd.Parameters.Add("@DocDescription", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePropertyDocumentType(string propertyDocID, string description, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPropertyDocID_M714_Property_Document_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PropertyDocId", SqlDbType.VarChar).Value = propertyDocID;
                    cmd.Parameters.Add("@DocDescription", SqlDbType.VarChar).Value = description;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePropertyDocumentType(string propertyDocID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPropertyDocID_M714_Property_Document_Type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PropertyDocId", SqlDbType.VarChar).Value = propertyDocID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllPropertyDocumentType()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
           // ds = getDataSet("SELECT * FROM UV_Select_M714_Property_Document_Type");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPropertyDocumentType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M714_Property_Document_Type

    #region M716_Vertical_master

    public static string InsertIntoVerticalMaster(string verticalId, string verticalDescription, string createdBy)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M716_VerticalMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VerticalId", SqlDbType.NVarChar).Value = verticalId;
                    cmd.Parameters.Add("@VerticalDescription", SqlDbType.VarChar).Value = verticalDescription;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = createdBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateVerticalMaster(string verticalId, string verticalDescription, string editedBy)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_M716_ByVertical_Id_Vertical_master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VerticalId", SqlDbType.NVarChar).Value = verticalId;
                    cmd.Parameters.Add("@VerticalDescription", SqlDbType.VarChar).Value = verticalDescription;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveVerticalMaster(string verticalId, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByVerticalId_M716_Vertical_master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VerticalId", SqlDbType.NVarChar).Value = verticalId;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllVerticalMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            ds = getDataSet("SELECT * FROM UV_Select_M716_Vertical_master");
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetVerticalMasterByVerticalId(string verticalId)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByVerticalId_Vertical_master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VerticalId", SqlDbType.NVarChar).Value = verticalId;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M716_Vertical_master

    #region M703A_Vendor_Info and M704_Vendor_Address

    public static string InsertIntoVendorInfo(string typeOfVendor, string vendorCode, string companyCode,string name1,string name2,string name3,string name4,string companyName,string PanNo,
        string email,string telephone,string mobileno,string addressID,string building,string floorNo,string roomNo,
        string country,string state,string city,string postalCode,string street,string houseNo,string street2,string street3,string street4,string street5,string createdBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M703A_Vendor_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TypeOfVendor", SqlDbType.NVarChar).Value = typeOfVendor;
                    cmd.Parameters.Add("@VendorCode", SqlDbType.NVarChar).Value = vendorCode;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar).Value = companyCode;
                    cmd.Parameters.Add("@Name1", SqlDbType.VarChar).Value = name1;
                    cmd.Parameters.Add("@Name2", SqlDbType.VarChar).Value = name2;
                    cmd.Parameters.Add("@Name3", SqlDbType.VarChar).Value = name3;
                    cmd.Parameters.Add("@Name4", SqlDbType.VarChar).Value = name4;
                    cmd.Parameters.Add("@NameOfCompany", SqlDbType.VarChar).Value = companyName;
                    cmd.Parameters.Add("@PanNo", SqlDbType.VarChar).Value = PanNo;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@TelephoneNo", SqlDbType.VarChar).Value = telephone;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = mobileno;
                    cmd.Parameters.Add("@AddressID", SqlDbType.VarChar).Value = addressID;
                    cmd.Parameters.Add("@Building", SqlDbType.VarChar).Value = building;
                    cmd.Parameters.Add("@FloorNo", SqlDbType.VarChar).Value = floorNo;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = roomNo;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = postalCode;
                    cmd.Parameters.Add("@Street", SqlDbType.VarChar).Value = street;
                    cmd.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = houseNo;
                    cmd.Parameters.Add("@Street2", SqlDbType.VarChar).Value = street2;
                    cmd.Parameters.Add("@Street3", SqlDbType.VarChar).Value = street3;
                    cmd.Parameters.Add("@Street4", SqlDbType.VarChar).Value = street4;
                    cmd.Parameters.Add("@Street5", SqlDbType.NVarChar).Value = street5;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = createdBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateVendorInfo(string typeOfVendor, string vendorCode, string companyCode, string name1, string name2, string name3, string name4, string companyName, string PanNo,
        string email, string telephone, string mobileno, string addressID, string floorNo, string roomNo,
        string country, string state, string city, string postalCode, string street, string houseNo, string street2, string street3, string street4, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByVendorCode_M703A_Vendor_Info", con))
                {
                    cmd.Parameters.Add("@TypeOfVendor", SqlDbType.NVarChar).Value = typeOfVendor;
                    cmd.Parameters.Add("@VendorCode", SqlDbType.NVarChar).Value = vendorCode;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar).Value = companyCode;
                    cmd.Parameters.Add("@Name1", SqlDbType.VarChar).Value = name1;
                    cmd.Parameters.Add("@Name2", SqlDbType.VarChar).Value = name2;
                    cmd.Parameters.Add("@Name3", SqlDbType.VarChar).Value = name3;
                    cmd.Parameters.Add("@Name4", SqlDbType.VarChar).Value = name4;
                    cmd.Parameters.Add("@NameOfCompany", SqlDbType.VarChar).Value = companyName;
                    cmd.Parameters.Add("@PanNo", SqlDbType.VarChar).Value = PanNo;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@TelephoneNo", SqlDbType.VarChar).Value = telephone;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = mobileno;
                    cmd.Parameters.Add("@AddressID", SqlDbType.VarChar).Value = addressID;
                   // cmd.Parameters.Add("@Building", SqlDbType.VarChar).Value = building;
                    cmd.Parameters.Add("@FloorNo", SqlDbType.VarChar).Value = floorNo;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = roomNo;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = postalCode;
                    cmd.Parameters.Add("@Street", SqlDbType.VarChar).Value = street;
                    cmd.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = houseNo;
                    cmd.Parameters.Add("@Street2", SqlDbType.VarChar).Value = street2;
                    cmd.Parameters.Add("@Street3", SqlDbType.VarChar).Value = street3;
                    cmd.Parameters.Add("@Street4", SqlDbType.VarChar).Value = street4;
                    //cmd.Parameters.Add("@Street5", SqlDbType.NVarChar).Value = street5;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                   // cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                   // cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = "Record Update successfully";  //(string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveVendorInfo(string vendorCode, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByVendorCode_M703A_Vendor_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VendorCode", SqlDbType.VarChar).Value = vendorCode;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllVendorInfo()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M703A_Vendor_Info");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllVendorInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayAllCompany()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_C006_Company");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayAllAddressId()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M702_Address_Type");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllAddressId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetVendorInfoByVendorCode(string vendorCode)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByVendorCode_M703A_Vendor_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VendorCode", SqlDbType.VarChar).Value = vendorCode;
                    cmd.Parameters.Add("@VendorType", SqlDbType.NVarChar).Value = string.Empty;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M703A_Vendor_Info and M704_Vendor_Address

    #region Common function

    public static DataSet DisplayAllCountry()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_C001_Country");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllCountry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayAllStateByCountryCode(string countryCode)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet(string.Format("SELECT * FROM UV_Select_C002_State WHERE Country_Code = '{0}'",countryCode));
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllStateByCountryCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryCode", countryCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayAllCityByStateCode(string stateCode)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet(string.Format("SELECT * FROM UV_Select_C004_City WHERE State_Code = '{0}'", stateCode));
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllCityByStateCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StateCode", stateCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetVendorCodeNumberByVendorType(string vendorType)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet(string.Format("SELECT * FROM UV_Select_VendorTypeWithDocReference WHERE Vendor_type_Id = '{0}'; ",vendorType));
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_GetVendorCodeNumberByVendorType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Vendor_type_Id", vendorType);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion common function

    #region M712_Centre_Master

    public static string InsertIntoCenterMaster(string centerCode, string premisesId, string contactNo, string createdBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M712_CentreMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CenterCode", SqlDbType.NVarChar).Value = centerCode;
                    cmd.Parameters.Add("@PremisesId", SqlDbType.VarChar).Value = premisesId;
                    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = contactNo;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = createdBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdateCenterMaster(string centerCode, string premisesId, string contactNo, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_M712_Centre_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CenterCode", SqlDbType.NVarChar).Value = centerCode;
                    cmd.Parameters.Add("@PremisesId", SqlDbType.VarChar).Value = premisesId;
                    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = contactNo;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActiveCenterMaster(string centerCode, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByCentreCode_M712_Centre_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CenterCode", SqlDbType.NVarChar).Value = centerCode;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllCenterMaster()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M712_Centre_Master");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllCenterMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayCenterCode()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_C009_Centers");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayCenterCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplaypremisesId()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M707B_Premises_Info");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplaypremisesId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }



    public static DataSet GetCenterMasterByCentreCode(string centerCode,string PremiseId)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByCentreCode_M712_Centre_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CentreCode", SqlDbType.NVarChar).Value = centerCode;
                    cmd.Parameters.Add("@PremiseID", SqlDbType.VarChar).Value = PremiseId;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M712_Centre_Master 

    #region M707B_Premises_Info

    public static string InsertIntoPremisesInfo(string proposalType, string proposalId, string premiseTypeID, string premiseID, string primiseDescription, string zone, string state, string country, string city, string building, string floorNo, string roomNo, string companyName, string postalCode, string street, string houseNo, string street2, string street3, string street4, string street5, decimal approxCarpetArea, decimal approxBuiltupArea, decimal carpetAreaAsPerTermSheet, int noOfClassroom, int studentCapacityPerRoom, DateTime commencementDate, string createdBy)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M707B_Premises_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProposalType", SqlDbType.VarChar).Value = proposalType;
                    cmd.Parameters.Add("@ProposalId", SqlDbType.VarChar).Value = proposalId;
                    cmd.Parameters.Add("@PremiseTypeID", SqlDbType.VarChar).Value = premiseTypeID;
                    cmd.Parameters.Add("@PremiseID", SqlDbType.VarChar).Value = premiseID;
                    cmd.Parameters.Add("@PrimiseDescription", SqlDbType.VarChar).Value = primiseDescription;
                    cmd.Parameters.Add("@Zone", SqlDbType.VarChar).Value = zone;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@Building", SqlDbType.VarChar).Value = building;
                    cmd.Parameters.Add("@FloorNo", SqlDbType.VarChar).Value = floorNo;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = roomNo;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyName;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = postalCode;
                    cmd.Parameters.Add("@Street", SqlDbType.VarChar).Value = street;
                    cmd.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = houseNo;
                    cmd.Parameters.Add("@Street2", SqlDbType.VarChar).Value = street2;
                    cmd.Parameters.Add("@Street3", SqlDbType.VarChar).Value = street3;
                    cmd.Parameters.Add("@Street4", SqlDbType.VarChar).Value = street4;
                    cmd.Parameters.Add("@Street5", SqlDbType.VarChar).Value = street5;
                    cmd.Parameters.Add("@ApproxCarpetArea", SqlDbType.Decimal).Value = approxCarpetArea;
                    cmd.Parameters.Add("@ApproxBuiltupArea", SqlDbType.Decimal).Value = approxBuiltupArea;
                    cmd.Parameters.Add("@CarpetAreaAsPerTermSheet", SqlDbType.Decimal).Value = carpetAreaAsPerTermSheet;
                    cmd.Parameters.Add("@NoOfClassroom", SqlDbType.Int).Value = noOfClassroom;
                    cmd.Parameters.Add("@StudentCapacityPerRoom", SqlDbType.Int).Value = studentCapacityPerRoom;
                    cmd.Parameters.Add("@CommencementDate", SqlDbType.DateTime).Value = commencementDate.ToString("yyyy/MM/dd HH:mm");
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = createdBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePremisesInfo(string premiseTypeID, string premiseID, string primiseDescription, string zone, string state, string country, string city, string building, string floorNo, string roomNo, string companyName, string postalCode, string street, string houseNo, string street2, string street3, string street4, string street5, decimal approxCarpetArea, decimal approxBuiltupArea, decimal carpetAreaAsPerTermSheet, int noOfClassroom, int studentCapacityPerRoom, DateTime commencementDate, string editedBy)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPremise_ID_M707B_Premises_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PremiseTypeID", SqlDbType.VarChar).Value = premiseTypeID;
                    cmd.Parameters.Add("@PremiseID", SqlDbType.VarChar).Value = premiseID;
                    cmd.Parameters.Add("@PrimiseDescription", SqlDbType.VarChar).Value = primiseDescription;
                    cmd.Parameters.Add("@Zone", SqlDbType.VarChar).Value = zone;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@Building", SqlDbType.VarChar).Value = building;
                    cmd.Parameters.Add("@FloorNo", SqlDbType.VarChar).Value = floorNo;
                    cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = roomNo;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyName;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = postalCode;
                    cmd.Parameters.Add("@Street", SqlDbType.VarChar).Value = street;
                    cmd.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = houseNo;
                    cmd.Parameters.Add("@Street2", SqlDbType.VarChar).Value = street2;
                    cmd.Parameters.Add("@Street3", SqlDbType.VarChar).Value = street3;
                    cmd.Parameters.Add("@Street4", SqlDbType.VarChar).Value = street4;
                    cmd.Parameters.Add("@Street5", SqlDbType.VarChar).Value = street5;
                    cmd.Parameters.Add("@ApproxCarpetArea", SqlDbType.Decimal).Value = approxCarpetArea;
                    cmd.Parameters.Add("@ApproxBuiltupArea", SqlDbType.Decimal).Value = approxBuiltupArea;
                    cmd.Parameters.Add("@CarpetAreaAsPerTermSheet", SqlDbType.Decimal).Value = carpetAreaAsPerTermSheet;
                    cmd.Parameters.Add("@NoOfClassroom", SqlDbType.Int).Value = noOfClassroom;
                    cmd.Parameters.Add("@StudentCapacityPerRoom", SqlDbType.Int).Value = studentCapacityPerRoom;
                    cmd.Parameters.Add("@CommencementDate", SqlDbType.DateTime).Value = commencementDate.ToString("yyyy-MM-dd HH:mm");
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePremisesInfo(string premiseID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPremisesID_M707B_Premises_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PremiseID", SqlDbType.NVarChar).Value = premiseID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllCityByState(string stateCode)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet(string.Format("SELECT * FROM UV_Select_C004_City WHERE State_Code = '{0}'", stateCode));
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllCityByState", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State_Code", stateCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayC001Country()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_C001_Country");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayC001Country", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayC010Zone()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_C010_Zone");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayC010Zone", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayC002StateByContryCode(string contryCode )
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
           // ds = getDataSet(string.Format("SELECT * FROM UV_Select_C002_State WHERE Country_Code = '{0}'", contryCode));
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayC002StateByContryCode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Country_Code", contryCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayAllPremisesInfo(string proposalid)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M707B_Premises_Info where Proposal_Id='"+proposalid+"'");
           // ds = getDataSet("select Proposal_ID,Premise_Type_ID,Premise_ID,Primise_Description,City,Company_name,Approx_Carpet_Area,No_of_Class_Room,Dbo.GetDateFormat(Commencement_Date) as Commencement_Date,Is_Active from UV_Select_M707B_Premises_Info where Proposal_Id='"+proposalid+"'");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPremisesInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Proposal_Id", proposalid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet GetPremisesInfoByPremisesId(string premiseID)
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByPremise_ID_M707B_Premises_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PremiseID", SqlDbType.VarChar).Value = premiseID;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M707B_Premises_Info

    #region M717_Payment_Mode

    public static string InsertIntoPaymentMode(string paymentModeID, string paymentModeDescription, string CreatedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_M717_Payment_Mode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentModeID", SqlDbType.VarChar).Value = paymentModeID;
                    cmd.Parameters.Add("@PaymentModeDescription", SqlDbType.VarChar).Value = paymentModeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while saving record";//ex.Message;
        }

    }

    public static string UpdatePaymentMode(string paymentModeID, string paymentModeDescription, string editedBy,int IsActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPaymentModeID_M717_Payment_Mode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentModeID", SqlDbType.VarChar).Value = paymentModeID;
                    cmd.Parameters.Add("@PaymentModeDescription", SqlDbType.VarChar).Value = paymentModeDescription;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while updating record"; //ex.Message;
        }

    }

    public static string ActiveInActivePaymentMode(string paymentModeID, string editedBy, int isActive)
    {
        string executeMessage = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPeriodCode_M717_Payment_Mode", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PaymentModeID", SqlDbType.VarChar).Value = paymentModeID;
                    cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    executeMessage = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
            return executeMessage;
        }
        catch (Exception ex)
        {
            return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
        }

    }

    public static DataSet DisplayAllPaymentMode()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UW_Select_M717_Payment_Mode");
             using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
             {
                 using (SqlCommand cmd = new SqlCommand("Usp_DisplayAllPaymentMode", con))
                 {
                     cmd.CommandType = CommandType.StoredProcedure;
                     SqlDataAdapter da = new SqlDataAdapter(cmd);
                     da.Fill(ds);
                 }
             }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    #endregion M717_Payment_Mode


    #region TADM0001_PremisesProposal_H

    //public static string InsertIntoPremisesProposal(string ProposalFor, string companyCode, string proposalType, string proposalID, DateTime proposalDate, string PremiseAt,
    //    string ProximityTo, decimal Approxarea, decimal ApproxRent, int NoOfClassroom, int StudentcapacityPerClassroom,
    //    string Office_Space, string Discussionroom,string staffRoom, string StaffToilets, string GenToilets, string CasseteAC, string PowerBackupRequired, string PowerBackupType,
    //    string Pantry_with_AquaGuard, string BenchesinActiveRoom, string NoticeBoard, string WaterDispenser, string NamePlatesandMTBoard,
    //    string FilmOnClassORBlinds, string AdditionalRequirement, DateTime AnticipatedDateOfCommencementBatch, string createdBy)
    //{
    //    string executeMessage = string.Empty;
    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_TADM0001_PremisesProposal_H", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@Proposal_For", SqlDbType.VarChar).Value = ProposalFor;
    //                cmd.Parameters.Add("@Proposal_ID", SqlDbType.VarChar).Value = proposalID;
    //                cmd.Parameters.Add("@Proposal_Type", SqlDbType.VarChar).Value = proposalType;
    //                cmd.Parameters.Add("@Company_code", SqlDbType.VarChar).Value = companyCode;
                   
                    
    //                cmd.Parameters.Add("@Proposal_Date", SqlDbType.DateTime).Value = proposalDate;
    //                cmd.Parameters.Add("@Premise_At", SqlDbType.VarChar).Value = PremiseAt;
    //                cmd.Parameters.Add("@Proximity_To", SqlDbType.VarChar).Value = ProximityTo;
    //                cmd.Parameters.Add("@Approx_area", SqlDbType.Decimal).Value = Approxarea;
    //                cmd.Parameters.Add("@No_Of_Classroom", SqlDbType.Int).Value = NoOfClassroom;
    //                cmd.Parameters.Add("@Student_capacity_Per_Classroom", SqlDbType.Int).Value = StudentcapacityPerClassroom;
    //                cmd.Parameters.Add("@Approx_Rent", SqlDbType.Decimal).Value = ApproxRent;
    //                cmd.Parameters.Add("@Additional_Requirement", SqlDbType.VarChar).Value = AdditionalRequirement;
    //                cmd.Parameters.Add("@Power_Backup_Type", SqlDbType.VarChar).Value = PowerBackupType;
    //                cmd.Parameters.Add("@Anticipated_Date_Of_Commencement_of_Batch", SqlDbType.DateTime).Value = AnticipatedDateOfCommencementBatch;
    //                cmd.Parameters.Add("@Discussion_room", SqlDbType.VarChar).Value = Discussionroom;

    //                cmd.Parameters.Add("@OfficeSpace", SqlDbType.VarChar).Value = Office_Space;
                   
    //                cmd.Parameters.Add("@Staff_Room", SqlDbType.VarChar).Value = staffRoom;
    //                cmd.Parameters.Add("@Staff_Toilets", SqlDbType.VarChar).Value = StaffToilets;
    //                cmd.Parameters.Add("@Gen_Toilets", SqlDbType.VarChar).Value = GenToilets;
    //                cmd.Parameters.Add("@Cassete_AC", SqlDbType.VarChar).Value = CasseteAC;
    //                cmd.Parameters.Add("@Power_Backup_Required", SqlDbType.VarChar).Value = PowerBackupRequired;
                    
    //                cmd.Parameters.Add("@Pantry_with_AquaGuard", SqlDbType.VarChar).Value = Pantry_with_AquaGuard;
    //                cmd.Parameters.Add("@Benches_in_ActiveRoom", SqlDbType.VarChar).Value = BenchesinActiveRoom;
    //                cmd.Parameters.Add("@Notice_Board", SqlDbType.VarChar).Value = NoticeBoard;
    //                cmd.Parameters.Add("@Water_Dispenser", SqlDbType.VarChar).Value = WaterDispenser;
    //                cmd.Parameters.Add("@Name_Plates_and_MT_Board", SqlDbType.VarChar).Value = NamePlatesandMTBoard;
    //                cmd.Parameters.Add("@Film_On_ClassORBlinds", SqlDbType.VarChar).Value = FilmOnClassORBlinds;
                    
                    
    //                cmd.Parameters.Add("@Created_By", SqlDbType.VarChar).Value = createdBy;
    //                cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
    //                cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
    //                con.Open();
    //                cmd.ExecuteNonQuery();
    //                executeMessage = (string)cmd.Parameters["@ERROR"].Value;
    //                con.Close();
    //            }
    //        }
    //        return executeMessage;
    //    }
    //    catch (Exception ex)
    //    {
    //        return executeMessage = "Error occured while saving record";//ex.Message;
    //    }

    //}


    // Comment by Rajesh
    //public static string InsertIntoPremisesProposal(PremisesProposalModel objPremisesProposal)
    //{
    //    string executeMessage = string.Empty;
    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("Usp_InsertInto_TADM0001_PremisesProposal_H", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@Proposal_For", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.ProposalFor);
    //                cmd.Parameters.Add("@Proposal_ID", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.proposalID);
    //                cmd.Parameters.Add("@Proposal_Description", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.ProposalDescription);
    //                cmd.Parameters.Add("@ProposalTypeCode", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.proposalType);
    //                cmd.Parameters.Add("@Company_code", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.companyCode);


    //                cmd.Parameters.Add("@Proposal_Date", SqlDbType.DateTime).Value = Convert.ToString(objPremisesProposal.proposalDate);
    //                cmd.Parameters.Add("@Premise_At", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.PremiseAt);
    //                cmd.Parameters.Add("@Proximity_To", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.ProximityTo);
    //                cmd.Parameters.Add("@Approx_area", SqlDbType.Decimal).Value = Convert.ToString(objPremisesProposal.Approxarea);
    //                cmd.Parameters.Add("@No_Of_Classroom", SqlDbType.Int).Value = Convert.ToString(objPremisesProposal.NoOfClassroom);
    //                cmd.Parameters.Add("@Student_capacity_Per_Classroom", SqlDbType.Int).Value = Convert.ToString(objPremisesProposal.StudentcapacityPerClassroom);
    //                cmd.Parameters.Add("@Approx_Rent", SqlDbType.Decimal).Value = Convert.ToString(objPremisesProposal.ApproxRent);
    //                cmd.Parameters.Add("@Additional_Requirement", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.AdditionalRequirement);
    //                cmd.Parameters.Add("@Power_Backup_Type", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.PowerBackupType);
    //                cmd.Parameters.Add("@Anticipated_Date_Of_Commencement_of_Batch", SqlDbType.DateTime).Value = Convert.ToString(objPremisesProposal.AnticipatedDateOfCommencementBatch);
    //                cmd.Parameters.Add("@Discussion_room", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.Discussionroom);

    //                cmd.Parameters.Add("@OfficeSpace", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.Office_Space);

    //                cmd.Parameters.Add("@Staff_Room", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.staffRoom);
    //                cmd.Parameters.Add("@Staff_Toilets", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.StaffToilets);
    //                cmd.Parameters.Add("@Gen_Toilets", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.GenToilets);
    //                cmd.Parameters.Add("@Cassete_AC", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.CasseteAC);
    //                cmd.Parameters.Add("@digitalIn", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.DigitalGenerator);
    //                cmd.Parameters.Add("@Power_Backup_Required", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.PowerBackupType);

    //                cmd.Parameters.Add("@Pantry_with_AquaGuard", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.Pantry_with_AquaGuard);
    //                cmd.Parameters.Add("@Benches_in_ActiveRoom", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.BenchesinActiveRoom);
    //                cmd.Parameters.Add("@Notice_Board", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.NoticeBoard);
    //                cmd.Parameters.Add("@Water_Dispenser", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.WaterDispenser);
    //                cmd.Parameters.Add("@Name_Plates_and_MT_Board", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.NamePlatesandMTBoard);
    //                cmd.Parameters.Add("@Film_On_ClassORBlinds", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.FilmOnClassORBlinds);


    //                cmd.Parameters.Add("@Created_By", SqlDbType.VarChar).Value = Convert.ToString(objPremisesProposal.createdBy);
    //                cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
    //                cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;


    //                con.Open();
    //                cmd.ExecuteNonQuery();

    //                executeMessage = cmd.Parameters["@ERROR"].Value.ToString();

    //                con.Close();
    //                //if (executeMessage.Trim() != "" && !executeMessage.ToUpper().Contains("Successfully"))
    //                //{
    //                //    return executeMessage;
    //                //}


    //                foreach (VerticalModel vr in objPremisesProposal.verticals)
    //                {
    //                    try
    //                    {
    //                        using (SqlCommand cmnd = new SqlCommand("Usp_InsertInto_TADM0002_PremisesProposal_I", con))
    //                        {
    //                            cmnd.CommandType = CommandType.StoredProcedure;
    //                            cmnd.Parameters.Add("@sProposalTypeCode", SqlDbType.VarChar).Value = objPremisesProposal.proposalType;
    //                            cmnd.Parameters.Add("@sProposal_ID", SqlDbType.VarChar).Value = objPremisesProposal.proposalID;
    //                            cmnd.Parameters.Add("@sVertical_Code", SqlDbType.VarChar).Value = vr.DivisionCode;
    //                            cmnd.Parameters.Add("@sResp_Person_Id ", SqlDbType.VarChar).Value = vr.ResponsiblePersonId;
    //                            //    cmnd.Parameters.Add("@sCreated_By", SqlDbType.VarChar).Value = vr.Created_By;
    //                            cmnd.Parameters.Add("@sCreated_By", SqlDbType.VarChar).Value = objPremisesProposal.createdBy;
    //                            //cmnd.Parameters.Add("@sCreated_On", SqlDbType.VarChar).Value = vr.CreatedOn;
    //                            //  cmnd.Parameters.Add("@sEdited_By", SqlDbType.VarChar).Value = vr.EditedBy;
    //                            // cmnd.Parameters.Add("@sEdited_On", SqlDbType.VarChar).Value = vr.EditedOn;

    //                            cmnd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
    //                            cmnd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
    //                            con.Open();
    //                            cmnd.ExecuteNonQuery();

    //                            executeMessage = cmnd.Parameters["@ERROR"].Value.ToString();

    //                            con.Close();

    //                            //if (executeMessage.Trim() != "" && !executeMessage.ToUpper().Contains("Successfully"))
    //                            //{
    //                            //    return executeMessage;
    //                            //}

    //                        }
    //                        //  return executeMessage;
    //                    }

    //                    catch (Exception ex)
    //                    {
    //                        return executeMessage = "Error occured while saving record";


    //                    }


    //                }

    //                executeMessage = (string)cmd.Parameters["@ERROR"].Value;

    //            }
    //        }
    //        return executeMessage;
    //    }
    //    catch (Exception ex)
    //    {
    //        return executeMessage = "Error occured while saving record";//ex.Message;
    //    }

    //}

    //public static void InsertIntoVertical(VerticalModel objVerticalModel)
    //{
    //    SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
    //    //string strqry = "insert into TADM0002_PremisesProposal_I (Proposal_ID,Vertical_Code,Resp_Person_Id) values " +
    //    //        "('" + objVerticalModel.ProposalID + "','" + objVerticalModel.DivisionCode + "','" + objVerticalModel.ResponsiblePersonId + "')";
    //    SqlCommand cmd = new SqlCommand("Usp_InsertIntoVertical", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Proposal_ID", objVerticalModel.ProposalID);
    //    cmd.Parameters.AddWithValue("@Vertical_Code", objVerticalModel.DivisionCode);
    //    cmd.Parameters.AddWithValue("@Resp_Person_Id", objVerticalModel.ResponsiblePersonId);
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //}

    public static DataSet DisplayproId()
    {
       
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("Select max (Current_Number_Range)+1 'ProposalId' from M700_Doc_Reference_Master where Ref_Id ='PPFN'");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayproId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;

    }

    public static DataSet DisplayproIdRelocation()
    {

        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("Select max (Current_Number_Range)+1 'ProposalId' from M700_Doc_Reference_Master where Ref_Id ='PPFR'");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayproIdRelocation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;

    }

    public static DataSet DisplayVerticals()
    {

        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("select D1.Division_ShortDesc, D2.Person_Name from C007_Division D1, M706_Division_Responsible_person D2 where D1.Division_Code = D2.Division_Code");
          //  ds = getDataSet("select D1.Division_ShortDesc,D1.Division_Code,D2.Person_Name from C007_Division D1, M706_Division_Responsible_person D2 where D1.Division_Code = D2.Division_Code");

           // ds = getDataSet("select D1.Division_ShortDesc,D1.Division_Code,D2.Resp_Person_Id,D2.Person_Name from C007_Division D1, M706_Division_Responsible_person D2 where D1.Division_Code = D2.Division_Code");
         //   ds = getDataSet("select D1.Source_Division_ShortDesc, D1.source_Division_Code,D2.Resp_Person_Id,D2.Person_Name from C007_Division D1, M706_Division_Responsible_person D2 where D1.Source_Division_Code = D2.Division_Code");
           // ds = getDataSet("select D1.Source_Division_ShortDesc, D1.source_Division_Code,D2.Resp_Person_Id,D2.Person_Name from C007_Division D1, M706_Division_Responsible_person D2 where D1.Source_Division_Code=D2.Division_Code");
            //ds = getDataSet("select D1.Source_Division_ShortDesc, D1.source_Division_Code,D2.Resp_Person_Id,D2.Person_Name from C007_Division D1 left outer join M706_Division_Responsible_person D2 on D1.Source_Division_Code=D2.Division_Code");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayVerticals", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;

    }

    public static DataSet DisplayPowerBackupType()
    {

        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
           // ds = getDataSet("Select * from UV_Select_M713_Power_Backup_Type");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayPowerBackupType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;

    }

    ////public static string UpdatePremisesInfo(string premiseTypeID, string premiseID, string primiseDescription, string zone, string state, string country, string city, string building, string floorNo, string roomNo, string companyName, string postalCode, string street, string houseNo, string street2, string street3, string street4, string street5, decimal approxCarpetArea, decimal approxBuiltupArea, decimal carpetAreaAsPerTermSheet, int noOfClassroom, int studentCapacityPerRoom, DateTime commencementDate, string editedBy)
    ////{
    ////    string executeMessage = string.Empty;
    ////    try
    ////    {
    ////        using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
    ////        {
    ////            using (SqlCommand cmd = new SqlCommand("Usp_Update_ByPremise_ID_M707B_Premises_Info", con))
    ////            {
    ////                cmd.CommandType = CommandType.StoredProcedure;
    ////                cmd.Parameters.Add("@PremiseTypeID", SqlDbType.VarChar).Value = premiseTypeID;
    ////                cmd.Parameters.Add("@PremiseID", SqlDbType.VarChar).Value = premiseID;
    ////                cmd.Parameters.Add("@PrimiseDescription", SqlDbType.VarChar).Value = primiseDescription;
    ////                cmd.Parameters.Add("@Zone", SqlDbType.VarChar).Value = zone;
    ////                cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
    ////                cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
    ////                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
    ////                cmd.Parameters.Add("@Building", SqlDbType.VarChar).Value = building;
    ////                cmd.Parameters.Add("@FloorNo", SqlDbType.VarChar).Value = floorNo;
    ////                cmd.Parameters.Add("@RoomNo", SqlDbType.VarChar).Value = roomNo;
    ////                cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyName;
    ////                cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = postalCode;
    ////                cmd.Parameters.Add("@Street", SqlDbType.VarChar).Value = street;
    ////                cmd.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = houseNo;
    ////                cmd.Parameters.Add("@Street2", SqlDbType.VarChar).Value = street2;
    ////                cmd.Parameters.Add("@Street3", SqlDbType.VarChar).Value = street3;
    ////                cmd.Parameters.Add("@Street4", SqlDbType.VarChar).Value = street4;
    ////                cmd.Parameters.Add("@Street5", SqlDbType.VarChar).Value = street5;
    ////                cmd.Parameters.Add("@ApproxCarpetArea", SqlDbType.Decimal).Value = approxCarpetArea;
    ////                cmd.Parameters.Add("@ApproxBuiltupArea", SqlDbType.Decimal).Value = approxBuiltupArea;
    ////                cmd.Parameters.Add("@CarpetAreaAsPerTermSheet", SqlDbType.Decimal).Value = carpetAreaAsPerTermSheet;
    ////                cmd.Parameters.Add("@NoOfClassroom", SqlDbType.Int).Value = noOfClassroom;
    ////                cmd.Parameters.Add("@StudentCapacityPerRoom", SqlDbType.Int).Value = studentCapacityPerRoom;
    ////                cmd.Parameters.Add("@CommencementDate", SqlDbType.DateTime).Value = commencementDate.ToString("yyyy-MM-dd HH:mm");
    ////                cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
    ////                cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
    ////                cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
    ////                con.Open();
    ////                cmd.ExecuteNonQuery();
    ////                executeMessage = (string)cmd.Parameters["@ERROR"].Value;
    ////                con.Close();
    ////            }
    ////        }
    ////        return executeMessage;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        return executeMessage = "Error occured while updating record"; //ex.Message;
    ////    }

    ////}

    ////public static string ActiveInActivePremisesInfo(string premiseID, string editedBy, int isActive)
    ////{
    ////    string executeMessage = string.Empty;
    ////    try
    ////    {
    ////        using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
    ////        {
    ////            using (SqlCommand cmd = new SqlCommand("Usp_ActiveInActive_ByPremisesID_M707B_Premises_Info", con))
    ////            {
    ////                cmd.CommandType = CommandType.StoredProcedure;
    ////                cmd.Parameters.Add("@PremiseID", SqlDbType.NVarChar).Value = premiseID;
    ////                cmd.Parameters.Add("@EditedBy", SqlDbType.VarChar).Value = editedBy;
    ////                cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = isActive;
    ////                cmd.Parameters.Add("@ERROR", SqlDbType.Char, 1000);
    ////                cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
    ////                con.Open();
    ////                cmd.ExecuteNonQuery();
    ////                executeMessage = (string)cmd.Parameters["@ERROR"].Value;
    ////                con.Close();
    ////            }
    ////        }
    ////        return executeMessage;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        return executeMessage = "Error occured while InActivate or Activate record"; //ex.Message;
    ////    }

    ////}

    public static DataSet DisplayCompany()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_C006_Company");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    public static DataSet DisplayNewProposalId()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_New_ProposalID");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayNewProposalId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    //public static DataSet DisplayVerticals()
    //{
    //    string executeMessage = string.Empty;
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        ds = getDataSet("SELECT * FROM UV_Select_M716_Vertical_master");
    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //    return ds;
    //}




    public static DataSet DisplayPremisesId()
    {
        string executeMessage = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            //ds = getDataSet("SELECT * FROM UV_Select_M707B_Premises_Info");
            using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_DisplayPremisesId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ds;
    }

    ////public static DataSet GetPremisesInfoByPremisesId(string premiseID)
    ////{
    ////    string executeMessage = string.Empty;
    ////    DataSet ds = new DataSet();
    ////    try
    ////    {
    ////        using (SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString()))
    ////        {
    ////            using (SqlCommand cmd = new SqlCommand("Usp_Edit_ByPremise_ID_M707B_Premises_Info", con))
    ////            {
    ////                cmd.CommandType = CommandType.StoredProcedure;
    ////                cmd.Parameters.Add("@PremiseID", SqlDbType.VarChar).Value = premiseID;
    ////                SqlDataAdapter da = new SqlDataAdapter();
    ////                da.SelectCommand = cmd;
    ////                da.Fill(ds);
    ////            }
    ////        }

    ////    }
    ////    catch (Exception ex)
    ////    {

    ////    }

    ////    return ds;
    ////}

    #endregion TADM0001_PremisesProposal_H

    #region TADM001A_PremisesProposal_Approve_D

    public static int GetApprovalLevelByUserCode(string UserCode)
    {
        int approvalLevel = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
            //SqlCommand cmd = new SqlCommand("select ApprovalLevel from TADM002A_PremisesProposal_Approval_C where ApprovalLevel_UserCode='" + UserCode + "'", con);
            SqlCommand cmd = new SqlCommand("Usp_GetApprovalLevelByUserCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApprovalLevel_UserCode", UserCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                approvalLevel = Convert.ToInt16(dt.Rows[0]["ApprovalLevel"].ToString());
            }
            return approvalLevel;
        }
        catch (Exception ex)
        {
            //return executeMessage = "Error occured while saving record";//ex.Message;
            return 0;
        }
    }

    #endregion

}