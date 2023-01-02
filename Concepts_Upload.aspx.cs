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
using System.Data.OleDb;
using System.IO;
using System.Data.Odbc;
using System.Text;

public partial class Concepts_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Add");
            FillDDL_Standard();





        }

    }




    private void FillDDL_Standard()
    {
        try
        {

            Clear_Error_Success_Box();


            // Div_Code = ddlDivisionAdd.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandardLMSProduct();
            BindDDL(ddluploadcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddluploadcourse.Items.Insert(0, "Select");
            ddluploadcourse.SelectedIndex = 0;

            BindDDL(ddldownloadcourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddldownloadcourse.Items.Insert(0, "Select");
            ddldownloadcourse.SelectedIndex = 0;




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

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
            Clear_Error_Success_Box();
            //DivAddPanel.Visible = false;
            //DivSearchPanel.Visible = true;
            //BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = true;

        }
        else if (Mode == "TopSearch")
        {
            //DivAddPanel.Visible = false;
            //DivSearchPanel.Visible = true;
            //BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = true;
            //DivResultPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            //DivAddPanel.Visible = false;
            //DivSearchPanel.Visible = false;
            //BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = true;
            //DivResultPanel.Visible = true;
            //BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Add")
        {
            BtndwnldTemplate.Visible = true;
            New_UploadGrid.Visible = false;
            DivNew_Upload.Visible = true;
            //UpdatePanel2.Update();

        }

    }
    protected void ddluploadcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        //FillDDL_Subject_Add(ddluploadcourse.SelectedValue);
    }



    protected void BtndwnldTemplate_Click(object sender, EventArgs e)
    {
        ddldownloadcourse.SelectedIndex = 0;
        
        divcertiErr.Visible = false;
        lbldownloadErrormsg.Text = "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "downloadselection_show();", true);
        
        //Clear_Error_Success_Box();
        //if (ddluploadcourse.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Course");
        //    return;
        //}

        

        //DataSet dssubjects = new DataSet();
        //dssubjects = ProductController.GetAllSubjectssByStandardCodeanew(ddluploadcourse.SelectedValue);

        //if (dssubjects.Tables[0].Rows.Count > 0 && dssubjects != null)
        //{
        //    New_UploadGrid.Visible = true;
            
        //    DataTable dt = new DataTable();
        //    dt = dssubjects.Tables[0];


        //    Response.ContentType = "Application/x-msexcel";
        //    Response.AddHeader("content-disposition", "attachment;filename=Concepts " + " " + DateTime.Now + ".csv");
        //    Response.Write(ExportToCSVFile(dt));
        //    Response.End();
            

        //}
            
        //else
        //{
        //    Show_Error_Success_Box("E", "No Subjects Found For Selected Course");
        //}


        
    }

    protected void Btndownload_Click(object sender, EventArgs e)
    {
        if (Validatemandfields() == false)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "downloadselection_hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "downloadselection_show();", true);
        }


    }

    private bool Validatemandfields()
    {
        bool flag = true;
        //Clear_Error_Success_Box1();

        if (ddldownloadcourse.SelectedIndex == 0)
        {
            divcertiErr.Visible = true;
            lbldownloadErrormsg.Visible = true;
            lbldownloadErrormsg.Text = "Select Course";
            ddldownloadcourse.Focus();
            flag = false;
            return flag;
        }


        DataSet dssubjects = new DataSet();
        dssubjects = ProductController.GetAllSubjectssByStandardCodeanew(ddldownloadcourse.SelectedValue);

        if (dssubjects.Tables[0].Rows.Count > 0 && dssubjects != null)
        {
            New_UploadGrid.Visible = true;

            DataTable dt = new DataTable();
            dt = dssubjects.Tables[0];


            Response.ContentType = "Application/x-msexcel";
            Response.AddHeader("content-disposition", "attachment;filename=Concepts " + " " + DateTime.Now + ".csv");
            Response.Write(ExportToCSVFile(dt));
            Response.End();


        }

        else
        {
            Show_Error_Success_Box("E", "No Subjects Found For Selected Course");
        }


        return flag;

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
    protected void Checkexcel()
    {

        if (!string.IsNullOrEmpty(uploadfile.FileName))
        {
            //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
            string FullName = Server.MapPath("~/Concept_Upload") + "\\" + Path.GetFileName(uploadfile.FileName);
            lblfilepath.Text = FullName;
            lblfilename.Text = Path.GetFileName(uploadfile.FileName);
            string strFileType = Path.GetExtension(uploadfile.FileName).ToLower();
            if (strFileType != ".csv")
            {
                Show_Error_Success_Box("E", "Kindly Select Excel File With .CSV Extension");
                return;
            }

            else
            {
                try
                {
                    if (File.Exists(lblfilepath.Text))
                    {
                        Show_Error_Success_Box("E", "File Name Already Exists");
                        return;

                    }
                    uploadfile.SaveAs(FullName);

                    DataTable dtRaw = new DataTable();



                    //create object for CSVReader and pass the stream
                    ////CSVReader reader = new CSVReader(FFLExcel.PostedFile.InputStream);
                    FileStream fileStream = new FileStream(FullName, FileMode.Open);
                    CSVReader reader = new CSVReader(fileStream);
                    //get the header
                    string[] headers = reader.GetCSVLine();

                    //add headers
                    foreach (string strHeader in headers)
                    {
                        dtRaw.Columns.Add(strHeader);

                    }
                    DataRow NewRow = null;
                    int CurRowNo = 0;




                    string[] data = null;


                    data = reader.GetCSVLine();
                    //Read first line
                    CurRowNo = 1;
                    while (data != null)
                    {
                        dtRaw.Rows.Add(data);

                    NextCSVLine:


                        data = reader.GetCSVLine();
                        //Read next line
                        CurRowNo = CurRowNo + 1;
                    }
                    datalist_NewUploads1.DataSource = dtRaw;
                    datalist_NewUploads1.DataBind();

                    New_UploadGrid.Visible = true;
                    datalist_NewUploads1.Visible = true;
                    Divbtnimport.Visible = true;
                    Msg_Error.Visible = false;
                    DivNew_Upload.Visible = false;
                    //UpdatePanel2.Update();
                }
                catch (Exception e)
                {
                    Show_Error_Success_Box("E", e.ToString());
                }

            }

        }
        else
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Please Select File...!";
            Divbtnimport.Visible = false;
            return;
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddluploadcourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddluploadcourse.Focus();
            return;
        }



        Checkexcel();
        lblStandard_Result.Text = ddluploadcourse.SelectedItem.Text;
        if (New_UploadGrid.Visible==true)
        { 
        btnsaveexcel.Visible = false;
        Btnimport.Visible = true;
        Btnimport.Enabled = true;
        BtndwnldTemplate.Visible = false;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
    }
    protected void Btnimport_Click(object sender, EventArgs e)
    {
        Btnimport.Visible = false;
        DataSet dsimportcode = ProductController.GetRecord_Id_Log();
        string importcode = Convert.ToString(dsimportcode.Tables[0].Rows[0]["Record_Id"]);


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblsubjectname = (Label)item.FindControl("lblsubjectname");
                Label lblsubjectcode = (Label)item.FindControl("lblsubjectcode");
                Label lblconceptname = (Label)item.FindControl("lblconceptname");
                Label lblconceptdescription = (Label)item.FindControl("lblconceptsdescription");
                Label lblconceptgenericname = (Label)item.FindControl("lblconceptgenericname");
                Label lblmoduleav = (Label)item.FindControl("lblmoduleav");
                
                Label lblstatuss = (Label)item.FindControl("labelSTATUS");


                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];




                // Here you define what lbl should show the multiplication result

                if ( lblsubjectname.Text == ""|| lblsubjectcode.Text == ""|| lblconceptname.Text == "" || lblconceptgenericname.Text == "")
                {
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "C091_Concepts", lblfilename.Text, UserID, "Error");
                    lblstatuss.Text = "Error Mandatoty Fileds Are Blank";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblstatuss.Text = "Success";

                }

                


                try
                {
                    if (lblstatuss.Text == "Success")
                    {

                        string ds1 = ProductController.Insert_Concepts_Import(ddluploadcourse.SelectedValue, lblsubjectname.Text, lblsubjectcode.Text, lblconceptname.Text.ToUpper(), lblconceptdescription.Text, lblconceptgenericname.Text, importcode, UserID, 1, lblfilename.Text,lblmoduleav.Text);

                        if (ds1 == "Record Inserted Sucessfully")
                        {
                            lblstatuss.Text = "Success";
                            lblstatuss.Visible = true;
                            lblSuccess.Text = "Records Saved Successfully";
                            lblstatuss.ForeColor = System.Drawing.Color.Green;
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "C091_Concepts", lblfilename.Text, UserID, "Success");
                        }

                        else if (ds1=="Subject Does Not Exists")
                        {
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "C091_Concepts", lblfilename.Text, UserID, "Error");
                            lblstatuss.Text = "Subject Does Not Exists";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ds1=="Concept Already Exists")
                        {
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "C091_Concepts", lblfilename.Text, UserID, "Error");
                            lblstatuss.Text = "Concept Already Existsss";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }




                    }
                    else
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "Recoed not Saved!";
                    }

                }
                catch (Exception ex)
                {
                    Show_Error_Success_Box("E", ex.ToString());
                }
            }
        }
        Btnimport.Visible = false;
        btnsaveexcel.Visible = true;
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Manage_Concepts.aspx");
    }
    protected void btnsaveexcel_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        //adding column
        table.Columns.Add("Subject_Name", typeof(string));
        table.Columns.Add("Subject_Code", typeof(string));
        table.Columns.Add("Concept_Name", typeof(string));
        table.Columns.Add("Concept_Description", typeof(string));
        table.Columns.Add("Concept_Generic_Name", typeof(string));
        table.Columns.Add("Status", typeof(string));


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            string Subject_Name = "", Subject_Code = "", Concept_Name = "", Concept_Description = "", Concept_Generic_Name="", Status = "";

            Subject_Name = ((Label)item.FindControl("lblsubjectname")).Text;
            Subject_Code = ((Label)item.FindControl("lblsubjectcode")).Text;
            Concept_Name = ((Label)item.FindControl("lblconceptname")).Text;
            Concept_Description = ((Label)item.FindControl("lblconceptsdescription")).Text;
            Concept_Generic_Name = ((Label)item.FindControl("lblconceptgenericname")).Text;
            Status = ((Label)item.FindControl("labelSTATUS")).Text;

            table.Rows.Add(Subject_Name, Subject_Code, Concept_Name, Concept_Description, Concept_Generic_Name, Status);



        }


        Response.ContentType = "Application/x-msexcel";
        Response.AddHeader("content-disposition", "attachment;filename=Concept Status" + " " + DateTime.Now + ".csv");
        Response.Write(ExportToCSVFile(table));
        Response.End();
    }
    private string ExportToCSVFile(DataTable dtTable)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable.Columns.Count != 0)
        {
            foreach (DataColumn col in dtTable.Columns)
            {
                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dtTable.Rows)
            {
                foreach (DataColumn column in dtTable.Columns)
                {
                    sbldr.Append(row[column].ToString() + ',');
                }
                sbldr.Append("\r\n");
            }
        }
        return sbldr.ToString();
    }



}