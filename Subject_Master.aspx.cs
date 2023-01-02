﻿using Microsoft.VisualBasic;
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
using System.Text.RegularExpressions;


public partial class Subject_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //  Page_Validation();
            ControlVisibility("Add");
            FillDDL_Division();


        }
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
            //BindDDL(ddlDivisionName, dsDivision, "Division_Name", "Division_Code");
            //ddlDivisionName.Items.Insert(0, "Select");
            //ddlDivisionName.SelectedIndex = 0;
            BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
            //BindDDL(ddlDivisionEdit, dsDivision, "Division_Name", "Division_Code");
            //ddlDivisionEdit.Items.Insert(0, "Select");
            //ddlDivisionEdit.SelectedIndex = 0;
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


    protected void BtndwnldTemplate_Click(object sender, EventArgs e)
    {
        New_UploadGrid.Visible = true;
        //To Get the physical Path of the file(me2.doc)
        string filepath = Server.MapPath("~/Template/Subject Upload Template.csv");



        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath);

        // Checking if file exists
        if (myfile.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now + " " + myfile.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", myfile.Length.ToString());

            // Set the ContentType
            //Response.ContentType = ReturnExtension(myfile.Extension.ToLower());

            // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(myfile.FullName);

            // End the response
            Response.End();
        }


    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageSubject.aspx");
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourseName.Items.Clear();
        if (ddlDivision.SelectedIndex == 0)
        {
            ddlDivision.Focus();
            return;
        }
        BindAddCourseName();
    }

    public void BindAddCourseName()
    {
        DataSet dscoursename = new DataSet();
        dscoursename = ProductController.GetCourseName(ddlDivision.SelectedValue, string.Empty, string.Empty, "3");
        BindDDL(ddlCourseName, dscoursename, "course_name", "course_code");
        ddlCourseName.Items.Insert(0, "Select");
        ddlCourseName.SelectedIndex = 0;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision.Focus();
            return;
        }

        if (ddlCourseName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddlCourseName.Focus();
            return;
        }
        Checkexcel();
        if (New_UploadGrid.Visible == true)
        {
            btnsaveexcel.Visible = false;
            Btnimport.Visible = true;
            Btnimport.Enabled = true;
            BtndwnldTemplate.Visible = false;
            lbldivisionresult.Text = ddlDivision.SelectedItem.Text;
            lblcourseresult.Text = ddlCourseName.SelectedItem.Text;
            
        }
    }


    protected void Checkexcel()
    {

        if (!string.IsNullOrEmpty(uploadfile.FileName))
        {
            //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
            string FullName = Server.MapPath("~/Subject_Upload") + "\\" + Path.GetFileName(uploadfile.FileName);
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
                    string subjects=null;
                    foreach (DataListItem item in datalist_NewUploads1.Items)
                    {
                        if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                        {

                            Label lblsubject = (Label)item.FindControl("lblsubject");
                            subjects = subjects+ lblsubject.Text.Trim() + ',';
                        }
                    }
                    if (subjects.Length > 1)
                    {
                        subjects = subjects.Substring(0, subjects.Length - 1);
                    }

                    string ResultId = ProductController.CheckSubjects(ddlCourseName.SelectedValue, subjects);

                    if (ResultId == "0")
                    {
                        //uploadfile.SaveAs(FullName);
                        New_UploadGrid.Visible = true;
                        datalist_NewUploads1.Visible = true;
                        Divbtnimport.Visible = true;
                        Msg_Error.Visible = false;
                        DivNew_Upload.Visible = false;
                    }
                    else 
                    {

                        Show_Error_Success_Box("E", "Subject Already Exists");
                        datalist_NewUploads1.DataSource = null;
                        datalist_NewUploads1.DataBind();
                        return;
                    }

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


    protected void Btnimport_Click(object sender, EventArgs e)
    {
        Btnimport.Enabled = false;
        DataSet dsimportcode = ProductController.GetRecord_Id_Log();
        string importcode = Convert.ToString(dsimportcode.Tables[0].Rows[0]["Record_Id"]);

        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
            {
                
                Label lblsubjectdisplayname = (Label)item.FindControl("lblsubjectdisplayname");
                Label lblsubject = (Label)item.FindControl("lblsubject");
                Label lblchapterdisplayname = (Label)item.FindControl("lblchapterdisplayname");
                Label lblchapter = (Label)item.FindControl("lblchapter");
                Label lbltopicdisplayname = (Label)item.FindControl("lbltopicdisplayname");
                Label lbltopic = (Label)item.FindControl("lbltopic");
                Label lblsubtopicdisplayname = (Label)item.FindControl("lblsubtopicdisplayname");
                Label lblsubtopic = (Label)item.FindControl("lblsubtopic");
                Label lblmoduledisplayname = (Label)item.FindControl("lblmoduledisplayname");
                Label lblmodule = (Label)item.FindControl("lblmodule");
                Label lbllessonplandisplayname = (Label)item.FindControl("lbllessionplannamedisplayname");
                Label lbllessionplanname = (Label)item.FindControl("lbllessionplanname");
                Label lblstatuss = (Label)item.FindControl("labelSTATUS");




                Match matchSubject = Regex.Match(lblsubject.Text, "[^a-zA-Z0-9_ -]", RegexOptions.IgnoreCase);
                Match matchChapter = Regex.Match(lblchapter.Text, "[^a-zA-Z0-9_ -]", RegexOptions.IgnoreCase);
                Match matchTopic = Regex.Match(lbltopic.Text, "[^a-zA-Z0-9_ -]", RegexOptions.IgnoreCase);
                Match matchSubtopic = Regex.Match(lblsubtopic.Text, "[^a-zA-Z0-9_ -]", RegexOptions.IgnoreCase);
                Match matchModule = Regex.Match(lblmodule.Text, "[^a-zA-Z0-9_-]", RegexOptions.IgnoreCase);
                Match matchLessonplan = Regex.Match(lbllessionplanname.Text, "[^a-zA-Z0-9_ -]", RegexOptions.IgnoreCase);

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];




                // Here you define what lbl should show the multiplication result

                if (lblsubject.Text.Trim() == "" || lblchapter.Text.Trim() == "" || lblmodule.Text.Trim() == "" ||
                     lbllessionplanname.Text.Trim() == "")
                {

                    lblstatuss.Text = "Error Mandatoty Fileds Are Blank";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

                else if (lbltopic.Text.Trim() == "" && (lbltopicdisplayname.Text.Trim().Length >= 1 || lblsubtopic.Text.Trim().Length >= 1 || lblsubtopicdisplayname.Text.Trim().Length >= 1))
                {
                    lblstatuss.Text = "Error Course Structure Used Is Wrong";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

                else if (lbltopicdisplayname.Text.Trim().Length >= 1 && lbltopic.Text.Trim().Length <= 0)
                {
                    lblstatuss.Text = "Error Course Structure Used Is Wrong";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

                else if (lblsubtopic.Text.Trim().Length >= 1 && lbltopic.Text.Trim().Length <= 0)
                {
                    lblstatuss.Text = "Error Course Structure Used Is Wrong";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

                else if (lblsubtopicdisplayname.Text.Trim().Length >= 1 && (lbltopic.Text.Trim().Length <= 0 || lblsubtopic.Text.Trim().Length <= 0))
                {
                    lblstatuss.Text = "Error Course Structure Used Is Wrong";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

               

                else if (lblmodule.Text.Contains(" "))
                {
                    lblstatuss.Text = "Module Name Field Should Not Contains Space";
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }


                else if (matchSubject.Success)
                {
                    lblstatuss.Text = "Subject Name Field Should Not Contain " + matchSubject.Value;
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

                else if (matchChapter.Success)
                {
                    lblstatuss.Text = "Chapter Name Field Should Not Contain " + matchChapter.Value;
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }

                else if (matchTopic.Success)
                {
                    lblstatuss.Text = "Topic Name Field Should Not Contain " + matchTopic.Value;
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }
                else if (matchSubtopic.Success)
                {
                    lblstatuss.Text = "Sub Topic Name Field Should Not Contain " + matchSubtopic.Value;
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }
                else if (matchModule.Success)
                {
                    lblstatuss.Text = "Module Name Field Should Not Contain " + matchModule.Value;
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }
                else if (matchLessonplan.Success)
                {
                    lblstatuss.Text = "Lesson Plan Name Field Should Not Contain " + matchLessonplan.Value;
                    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
                }


                else
                {
                    lblstatuss.Text = "Success";

                }




                try
                {
                    if (lblstatuss.Text == "Success")
                    {

                        string ResultId = ProductController.InsertSubjectStructure(ddlDivision.SelectedValue, ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.Text,lblsubject.Text.Trim(), lblchapter.Text.Trim(), lbltopic.Text.Trim(), lblsubtopic.Text.Trim(), lblmodule.Text.Trim(), lbllessionplanname.Text.Trim(), 1,
                        UserID, importcode,  lblsubjectdisplayname.Text.Trim(), lblchapterdisplayname.Text.Trim(), lbltopicdisplayname.Text.Trim(),
                        lblsubtopicdisplayname.Text.Trim(), lblmoduledisplayname.Text.Trim(), lbllessonplandisplayname.Text.Trim(), lblfilename.Text);

                        if (ResultId == "Duplicate")
                        {

                            lblstatuss.Text = "Error(Dupicate)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Course Structure", lblfilename.Text, UserID, "Error");
                        }


                        else if (ResultId == "Module Not Added In LessonPlan Because LessonPlan Structure Is Different")
                        {

                            lblstatuss.Text = "Module Not Added In LessonPlan Because LessonPlan Structure Is Different";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Course Structure", lblfilename.Text, UserID, "Error");
                        }
                       

                        else if (ResultId == "Subject Name Length Is Gretaer Than Maximum Length(50)")
                        {
                            lblstatuss.Text = "Subject Name Length Is Gretaer Than Maximum Length(50)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Subject Display Name Length Is Gretaer Than Maximum Length(100)")
                        {
                            lblstatuss.Text = "Subject Display Name Length Is Gretaer Than Maximum Length(100)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Chapter Name Length Is Gretaer Than Maximum Length(300)")
                        {
                            lblstatuss.Text = "Chapter Name Length Is Gretaer Than Maximum Length(300)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Chapter Display Name Length Is Gretaer Than Maximum Length(500)")
                        {
                            lblstatuss.Text = "Chapter Display Name Length Is Gretaer Than Maximum Length(500)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;

                        }
                        else if (ResultId == "Topic Name Length Is Gretaer Than Maximum Length(100)")
                        {
                            lblstatuss.Text = "Topic Name Length Is Gretaer Than Maximum Length(100)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ResultId == "Topic Display Name Length Is Gretaer Than Maximum Length(200)")
                        {
                            lblstatuss.Text = "Topic Display Name Length Is Gretaer Than Maximum Length(200)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Sub Topic Name Length Is Gretaer Than Maximum Length(100)")
                        {
                            lblstatuss.Text = "Sub Topic Name Length Is Gretaer Than Maximum Length(100)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Sub Topic Display Name Length Is Gretaer Than Maximum Length(250)")
                        {
                            lblstatuss.Text = "Sub Topic Display Name Length Is Gretaer Than Maximum Length(250)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;

                        }

                        else if (ResultId == "Module Name Length Is Gretaer Than Maximum Length(100)")
                        {
                            lblstatuss.Text = "Module Name Length Is Gretaer Than Maximum Length(100)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ResultId == "Module Display Name Length Is Gretaer Than Maximum Length(200)")
                        {
                            lblstatuss.Text = "Module Display Name Length Is Gretaer Than Maximum Length(200)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ResultId == "Lesson Plan Name Length Is Gretaer Than Maximum Length(100)")
                        {
                            lblstatuss.Text = "Lesson Plan Name Length Is Gretaer Than Maximum Length(100)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ResultId == "Lesson Plan Display Name Length Is Gretaer Than Maximum Length(100)")
                        {
                            lblstatuss.Text = "Lesson Plan Display Name Length Is Gretaer Than Maximum Length(100)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ResultId == "Record Inserted Succesfully")
                        {
                            lblstatuss.Text = "Success";
                            lblstatuss.Visible = true;
                            lblSuccess.Text = "Records Saved Successfully";
                            lblstatuss.ForeColor = System.Drawing.Color.Green;
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Course Structure", lblfilename.Text, UserID, "Success");
                        }
                        else
                        {
                            lblstatuss.Text = "Unknown Error Kindly Coordinate With Administrator";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT(importcode, "Subject Structure", lblfilename.Text, UserID, "Error");
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
                    //lblstatuss.Text = ex.ToString();

                    //lblstatuss.Text = "Error -(Date format should be MM/DD/YYYY )";
                }
            }
        }
        Btnimport.Visible = false;
        btnsaveexcel.Visible = true;

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
    }
    protected void btnsaveexcel_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        //adding column
        
        table.Columns.Add("Subject_Display_Name", typeof(string));
        table.Columns.Add("Subject", typeof(string));
        table.Columns.Add("Chapter_Display_Name", typeof(string));
        table.Columns.Add("Chapter", typeof(string));
        table.Columns.Add("Topic_Display_Name", typeof(string));
        table.Columns.Add("Topic", typeof(string));
        table.Columns.Add("SubTopic_Display_Name", typeof(string));
        table.Columns.Add("SubTopic", typeof(string));
        table.Columns.Add("Module_Display_Name", typeof(string));
        table.Columns.Add("Module", typeof(string));
        table.Columns.Add("LessonPlan_Display_Name", typeof(string));
        table.Columns.Add("Lesson_Plan_Name", typeof(string));
        table.Columns.Add("Status", typeof(string));


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            string  Subject_Display_Name = "", Subject = "", Chapter_Display_Name = "", Chapter = "", Topic_Display_Name = "", Topic = "",
            SubTopic_Display_Name = "", SubTopic = "", Module_Display_Name = "", Module = "", LessonPlan_Display_Name = "", Lesson_Plan_Name = "", Status = "";






            Subject_Display_Name = ((Label)item.FindControl("lblsubjectdisplayname")).Text;
            Subject = ((Label)item.FindControl("lblsubject")).Text;
            Chapter_Display_Name = ((Label)item.FindControl("lblchapterdisplayname")).Text;
            Chapter = ((Label)item.FindControl("lblchapter")).Text;
            Topic_Display_Name = ((Label)item.FindControl("lbltopicdisplayname")).Text;
            Topic = ((Label)item.FindControl("lbltopic")).Text;
            SubTopic_Display_Name = ((Label)item.FindControl("lblsubtopicdisplayname")).Text;
            SubTopic = ((Label)item.FindControl("lblsubtopic")).Text;
            Module_Display_Name = ((Label)item.FindControl("lblmoduledisplayname")).Text;
            Module = ((Label)item.FindControl("lblmodule")).Text;
            LessonPlan_Display_Name = ((Label)item.FindControl("lbllessionplannamedisplayname")).Text;
            Lesson_Plan_Name = ((Label)item.FindControl("lbllessionplanname")).Text;
            Status = ((Label)item.FindControl("labelSTATUS")).Text;

            table.Rows.Add(Subject_Display_Name, Subject, Chapter_Display_Name, Chapter, Topic_Display_Name, Topic, SubTopic_Display_Name, SubTopic, Module_Display_Name, Module, LessonPlan_Display_Name, Lesson_Plan_Name, Status);



        }


        Response.ContentType = "Application/x-msexcel";
        Response.AddHeader("content-disposition", "attachment;filename=Subject Upload Status" + " " + DateTime.Now + ".csv");
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