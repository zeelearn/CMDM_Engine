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


public partial class Cloud_Posting_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            ddlcontactstatus.Items.Add("All");
            ddlcontactstatus.Items.Add("Contact Not Created");
            ddlcontactstatus.Items.Add("Contact Created Successfully");

            ddlproductstatus.Items.Add("All");
            ddlproductstatus.Items.Add("Product Not Purchased");
            ddlproductstatus.Items.Add("Product Purchased");

            ddlaccountstatus.Items.Add("All");
            ddlaccountstatus.Items.Add("Account Not Created");
            ddlaccountstatus.Items.Add("Account Created");

            ddlcontactstatuscloud.Items.Add("All");
            ddlcontactstatuscloud.Items.Add("Contact Not Created in Cloud");
            ddlcontactstatuscloud.Items.Add("Contact Created in Cloud");

            ddlbatchstatuscloud.Items.Add("All");
            ddlbatchstatuscloud.Items.Add("Batch Not Created in Cloud");
            ddlbatchstatuscloud.Items.Add("Batch Created in Cloud");
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
          
        }
        else if (Mode == "Result")
        {
           
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
        Clear_Error_Success_Box();
        if (add_new_id_date_range_picker_1.Value == "")
        {
            Show_Error_Success_Box("E", "Select Period");
            UpdatePanelMsgBox.Focus();
            // id_date_range_picker_1.Focus();
            return;
        }

           string DateRange = "";
            DateRange = add_new_id_date_range_picker_1.Value;
            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            using (SqlConnection con = new SqlConnection(DBConnection.connString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_GET_CLOUD_POSTING_ANALYSIS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Contact_Status", SqlDbType.VarChar).Value = ddlcontactstatus.SelectedItem.Text;
                    cmd.Parameters.Add("@Product_Status", SqlDbType.VarChar).Value = ddlproductstatus.SelectedItem.Text;
                    cmd.Parameters.Add("@Account_Status", SqlDbType.VarChar).Value = ddlaccountstatus.SelectedItem.Text;
                    cmd.Parameters.Add("@Contatc_Cloud_Status", SqlDbType.VarChar).Value = ddlcontactstatuscloud.SelectedItem.Text;
                    cmd.Parameters.Add("@Batch_Cloud_Status", SqlDbType.VarChar).Value = ddlbatchstatuscloud.SelectedItem.Text;
                    cmd.Parameters.Add("@From_Date", SqlDbType.VarChar).Value = FromDate;
                    cmd.Parameters.Add("@To_Date", SqlDbType.VarChar).Value = ToDate;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet result = new DataSet();
                    da.Fill(result);

                    if (result.Tables[0].Rows.Count>0)
                    {
                        //DataList dllist =new DataList ();

                        //dllist.DataSource = result;
                        //dllist.DataBind();
                        //dllist.Visible = true;
                        string filename = "Cloud Posting Analysis " + DateTime.Now;
                        ExportToExcel(result.Tables[0], filename);


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


    void ExportToExcel(DataTable dt, string FileName)
    {
        if (dt.Rows.Count > 0)
        {
            string filename = FileName + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition",
                                  "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlcontactstatus.SelectedIndex = 0;
        ddlproductstatus.SelectedIndex = 0;
        ddlaccountstatus.SelectedIndex = 0;
        ddlcontactstatuscloud.SelectedIndex = 0;
        ddlbatchstatuscloud.SelectedIndex = 0;
        add_new_id_date_range_picker_1.Value = "";
    }
}