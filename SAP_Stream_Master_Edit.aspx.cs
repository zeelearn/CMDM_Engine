using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Configuration;

public partial class SAP_Stream_Master_Edit : System.Web.UI.Page
{
    #region Page Load
     protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];
                DataSet ds1 = ProductController.GetstreamMasterforedit(Session["Lblpk.Text"].ToString(),"1");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlDivisionedit.Text = ds1.Tables[0].Rows[0]["DIVISIONNAME"].ToString();
                    ddlDivisionedit.Enabled = false;
                    Ddldivisioncode.Text = ds1.Tables[0].Rows[0]["DIVISIONCODE"].ToString();
                    Ddldivisioncode.Visible = false;
                    ddlAcadYearedit.Text = ds1.Tables[0].Rows[0]["YEARNAME"].ToString();
                    ddlAcadYearedit.Enabled = false;
                    ddlClassRoomCourse.Text = ds1.Tables[0].Rows[0]["COURSENAME"].ToString();
                    ddlClassRoomCourse.Enabled = false;
                    Ddlcoursecode.Text = ds1.Tables[0].Rows[0]["COURSECODE"].ToString();
                    Ddlcoursecode.Visible = false;

                    ddlstreamcode.Text = ds1.Tables[0].Rows[0]["streamcode"].ToString();
                    ddlstreamcode.Visible = false;

                    txtProductName.Text = ds1.Tables[0].Rows[0]["STREAMNAME"].ToString();
                    txtDescription.Text = ds1.Tables[0].Rows[0]["STREAMDESC"].ToString();
                    ddlFeesZone.Text = ds1.Tables[0].Rows[0]["FEEZONE"].ToString();
                    ddlFeesZone.Visible = false;
                    txtCoursePeriod.Value = ds1.Tables[0].Rows[0]["COUSESandEdate"].ToString();
                    txtAdmissionPeriod.Value = ds1.Tables[0].Rows[0]["AdmissionSandEdate"].ToString();
                    
                    FillDDL_Center1();
                    for (int cnt = 0; cnt <= ds1.Tables[0].Rows.Count - 1; cnt++)
                    {
                        for (int rcnt = 0; rcnt <= ddlCenter.Items.Count - 1; rcnt++)
                        {
                            if (ddlCenter.Items[rcnt].Value == ds1.Tables[0].Rows[cnt]["CENTERCODE"].ToString())
                            {
                                ddlCenter.Items[rcnt].Selected = true;
                                break;
                            }
                        }
                    }

                    
                    ControlVisibility("Add");
                    //FillDDL_ClassRoomCourse();
                    lblHeader_Add.Text = "Edit Stream Details";
                    divSubject.Visible = true;
                    FillGrid_Get_subjectgroup();
                    Label14.Text = "Select Subject Group ";
                    Subject_grp_selected_for_course(sender, e);
                    Subject_selected_for_course(sender, e);
                    REG_selected_for_course(sender, e);
                }
             }
        }
    }

    #endregion
    #region Methods



    //for subject group bind
     private void Subject_grp_selected_for_course(object sender, EventArgs e)
     {
         try
         {
             
             DataSet dsCourse = ProductController.GetstreamMasterforedit(Session["Lblpk.Text"].ToString(), "2");
             if (dsCourse != null)
                 if (dsCourse.Tables[0].Rows.Count > 0)
                 {

                     for (int cnt = 0; cnt <= dsCourse.Tables[0].Rows.Count - 1; cnt++)
                     {

                         foreach (DataListItem dtlItem in dlSubjects.Items)
                         {
                             
                             CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                             Label lblSubgrCode = (Label)dtlItem.FindControl("lblSubgrCode");
                             if (Convert.ToString(lblSubgrCode.Text).Trim() == Convert.ToString(dsCourse.Tables[0].Rows[cnt]["SUBGRPCODE"]).Trim())
                             {
                                 chkitemck.Checked = true;
                                 chkCheck_CheckedChanged(sender, e);
                                 break; 
                             }
                         }

                     }
                 }



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
    //subject bind
     private void Subject_selected_for_course(object sender, EventArgs e)
     {
         try
         {

             DataSet dsCourse = ProductController.GetstreamMasterforedit(Session["Lblpk.Text"].ToString(), "3");
             if (dsCourse != null)
                 if (dsCourse.Tables[0].Rows.Count > 0)
                 {

                     for (int cnt = 0; cnt <= dsCourse.Tables[0].Rows.Count - 1; cnt++)
                     {

                         foreach (DataListItem dtlItem in dlSubjects1.Items)
                         {

                             CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                             Label lblSubCode = (Label)dtlItem.FindControl("lblSubCode");
                             Label lblsubgrup = (Label)dtlItem.FindControl("lblsubgrup");
                             if (Convert.ToString(lblSubCode.Text).Trim() == Convert.ToString(dsCourse.Tables[0].Rows[cnt]["Material_of_material_type_ZSUB"]).Trim())
                             if (Convert.ToString(lblsubgrup.Text).Trim() == Convert.ToString(dsCourse.Tables[0].Rows[cnt]["Material_of_material_type_ZSGR"]).Trim())
                             {

                                 TextBox txtsubjdate = (TextBox)dtlItem.FindControl("txtsubjdate");
                                 txtsubjdate.Text = dsCourse.Tables[0].Rows[cnt]["subjectdate"].ToString();
                                 
                                 TextBox txtprise = (TextBox)dtlItem.FindControl("txtprise");
                                 txtprise.Text = dsCourse.Tables[0].Rows[cnt]["Subject_Prices"].ToString();

                                 TextBox txtCRF = (TextBox)dtlItem.FindControl("txtCRF");
                                 txtCRF.Text = dsCourse.Tables[0].Rows[cnt]["CRF"].ToString();

                                 TextBox TxtCRFvalue = (TextBox)dtlItem.FindControl("TxtCRFvalue");
                                 TxtCRFvalue.Text = dsCourse.Tables[0].Rows[cnt]["CRF_Value"].ToString();

                                 TextBox txtTotal = (TextBox)dtlItem.FindControl("txtTotal");
                                 txtTotal.Text = dsCourse.Tables[0].Rows[cnt]["Total_Course_Fees"].ToString();

                                 break;
                             }
                         }

                     }
                 }



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
    //reg&robomate
     private void REG_selected_for_course(object sender, EventArgs e)
     {
         try
         {

             DataSet dsCourse = ProductController.GetstreamMasterforedit(Session["Lblpk.Text"].ToString(), "4");
             if (dsCourse != null)
                 if (dsCourse.Tables[0].Rows.Count > 0)
                 {

                     for (int cnt = 0; cnt <= dsCourse.Tables[0].Rows.Count - 1; cnt++)
                     {

                         foreach (DataListItem dtlItem in dsregst.Items)
                         {

                             CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                             Label lblRegcode = (Label)dtlItem.FindControl("lblRegcode");
                             if (Convert.ToString(lblRegcode.Text).Trim() == Convert.ToString(dsCourse.Tables[0].Rows[cnt]["Material_for_registration"]).Trim())
                             {
                                 chkitemck.Checked = true;
                                 System.Web.UI.HtmlControls.HtmlInputText txtPeriod1 = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("txtPeriod1");
                                 txtPeriod1.Value = dsCourse.Tables[0].Rows[0]["regdate"].ToString();

                                 TextBox txtAmount = (TextBox)dtlItem.FindControl("txtAmount");
                                 txtAmount.Text = dsCourse.Tables[0].Rows[0]["Registration_Fee"].ToString();
                                 break;
                             }

                       
                         }

                     }
                 }
             if (dsCourse.Tables[1].Rows.Count > 0)
             {

                 for (int cnt = 0; cnt <= dsCourse.Tables[1].Rows.Count - 1; cnt++)
                 {

                     foreach (DataListItem dtlItem in dsregst.Items)
                     {

                         CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                         Label lblRegcode = (Label)dtlItem.FindControl("lblRegcode");
                        if (Convert.ToString(lblRegcode.Text).Trim() == Convert.ToString(dsCourse.Tables[1].Rows[cnt]["Material_for_Robomate"]).Trim())
                         {
                             chkitemck.Checked = true;
                             //HtmlInputText txtPeriod1 = (HtmlInputText)TextBox.FindControl("txtPeriod1");
                             System.Web.UI.HtmlControls.HtmlInputText txtPeriod1 = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("txtPeriod1");
                             txtPeriod1.Value = dsCourse.Tables[1].Rows[0]["rombosdate"].ToString();

                             TextBox txtAmount = (TextBox)dtlItem.FindControl("txtAmount");
                             txtAmount.Text = dsCourse.Tables[1].Rows[0]["Robomate_fee"].ToString();
                             break;
                         }
                     }

                 }
             }



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

    private void Clear_ClassRoomProductAddPanel()
    {
        ddlClassRoomCourse.Text = "";
        ddlCenter.Items.Clear();
        txtProductName.Text = "";
        txtDescription.Text = "";
        ddlFeesZone.Text = "";
        txtCoursePeriod.Value = "";
        txtAdmissionPeriod.Value = "";

    }
    
    private void FillDDL_Center()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_1(4, Ddldivisioncode.Text);
            if (dsCenter != null)
            {
                if (dsCenter.Tables.Count != 0)
                {
                    BindListBox(ddlCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
                }
            }
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
    private void FillDDL_Center1()
    {

        try
        {

            DataSet dsCenter = ProductController.GetAllCenters_DivisionWise_1(4, Ddldivisioncode.Text);
            if (dsCenter != null)
            {
                if (dsCenter.Tables.Count != 0)
                {
                    BindListBox(ddlCenter, dsCenter, "Source_Center_Name", "Source_Center_Code");
                }
            }
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
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            btnTopSearch.Visible = false;
            BtnAdd.Visible = true;
        }

        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = false;
            BtnAdd.Visible = true;
            btnTopSearch.Visible = true;
            divSubject.Visible = false;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
           

        }
        else if (Mode == "SubjectGroup")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            

        }
        else if (Mode == "PricingItem")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            

        }
        else if (Mode == "PricingHeader")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
           
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
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    #endregion
    #region Events
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ddlDivision.SelectedIndex = 0;
            ddlAcadYear.SelectedIndex = 0;
            txtStreamName.Text = "";

            //ddlStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivision.Focus();
                return;
            }
            if (ddlAcadYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddlAcadYear.Focus();
                return;
            }



            ControlVisibility("Result");
            //Fill_Grid();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            ControlVisibility("Add");
            lblHeader_Add.Text = "Edit ClassRoom Product";
            Clear_ClassRoomProductAddPanel();
            divRegst.Visible = false;
            divSubject.Visible = false;
            divsubjects.Visible = false;
          
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    //protected void Savecourse()
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {



        //Saving part
        string DivisionCode = null;
        DivisionCode = Ddldivisioncode.Text;

        string acadyear = null;
        acadyear = ddlAcadYearedit.Text;

        
        string course = null;
        course = Ddlcoursecode.Text;
        if (course == "Select")
        {
            Show_Error_Success_Box("E", "Please Select Course ");
            return;
        }

        string streamname = null;
        streamname = txtProductName.Text;
        if (streamname == "")
        {
            Show_Error_Success_Box("E", "Please Add Stream Name ");
            return;
        }


        string Desc = null;
        Desc = txtDescription.Text;
        if (Desc == "")
        {
            Show_Error_Success_Box("E", "Please Add Stream Description");
            return;
        }

        string Feezone = null;
        Feezone = ddlFeesZone.Text;
        if (Feezone == "Select")
        {
            Show_Error_Success_Box("E", "Please Select FeesZone");
            return;
        }

        string coursedate = null;
        coursedate = txtCoursePeriod.Value;
        if (coursedate == "")
        {
            Show_Error_Success_Box("E", "Please Select Course Date");
            return;
        }

        string admdate = null;
        admdate = txtAdmissionPeriod.Value;
        if (admdate == "")
        {
            Show_Error_Success_Box("E", "Please Select Admission Date");
            return;
        }

        string centercode = null;
        //centercode = ddlCenter.SelectedValue;

        for (int cnt = 0; cnt <= ddlCenter.Items.Count - 1; cnt++)
        {
            if (ddlCenter.Items[cnt].Selected == true)
            {
                centercode = centercode + ddlCenter.Items[cnt].Value + ",";
            }
        }
        if (centercode == "")
        {
            Show_Error_Success_Box("E", "Atleast one Center should be selected");
            ddlCenter.Focus();
            return;
        }

        centercode = centercode.Substring(0, centercode.Length - 1);
        coursedate = txtCoursePeriod.Value;
        Crs_SDate = coursedate.Substring(0, 10);
        Crs_EDate = coursedate.Substring(13, 10);

        admdate = txtAdmissionPeriod.Value;
        Adm_SDate = admdate.Substring(0, 10);
        Adm_EDate = admdate.Substring(13, 10);

        string Subjectdate;


        string txtprise = String.Empty;
        string txtCRF = string.Empty;
        string TxtCRFvalue = string.Empty;
        string txtTotal = string.Empty;
        string sujectgruopcode = "";
        string Subjectcode = "";
        // CheckBox s = sender as CheckBox;



        string ResultId = "0";

        foreach (DataListItem TextBox in dlSubjects1.Items)
        {


            Label lblSubCode = (Label)TextBox.FindControl("lblSubCode");
            Label lblsubgrup = (Label)TextBox.FindControl("lblsubgrup");
            //HtmlTextBox txtsubjdate = (HtmlInputText)TextBox.FindControl("txtsubjdate");
            TextBox txtsubjdate = (TextBox)TextBox.FindControl("txtsubjdate");

            Subjectdate = txtsubjdate.Text;
            Sub_Sdate = Subjectdate.Substring(0, 10);
            Sub_Edate = Subjectdate.Substring(13, 10);

            txtprise = (TextBox.FindControl("txtprise") as TextBox).Text;
            txtCRF = (TextBox.FindControl("txtCRF") as TextBox).Text;
            TxtCRFvalue = (TextBox.FindControl("TxtCRFvalue") as TextBox).Text;
            txtTotal = (TextBox.FindControl("txtTotal") as TextBox).Text;
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];
            Subjectcode = lblSubCode.Text;
            sujectgruopcode = lblsubgrup.Text;

            ResultId = ProductController.InsertUpdatecoursemaster(DivisionCode, acadyear, course, streamname, Desc, Feezone,
                Crs_SDate, Crs_EDate, Adm_SDate, Adm_EDate, centercode, sujectgruopcode, Subjectcode, Sub_Sdate, Sub_Edate, txtprise, txtCRF, TxtCRFvalue, txtTotal, "1", UserID, ddlstreamcode.Text);

            if (ResultId == "-1")
            {
                Show_Error_Success_Box("E", " Duplicate Master Data ");

            }
            else
            {

                SaveRegistrationfees();
                DivAddPanel.Visible = false;
                divRegst.Visible = false;
                divSubject.Visible = false;
                divsubjects.Visible = false;
                //Fill_Grid1();
                ControlVisibility("Result");
                UpdatePanel1.Update();
                Show_Error_Success_Box("S", "Stream Details are Saved Successfully");


            }
        }

        

    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        
        {
            
            Clear_Error_Success_Box();
            Response.Redirect("SAP_Masterfile_Creation.aspx", false);
            divSubject.Visible = false;
            divsubjects.Visible = false; divRegst.Visible = false; 

        }
        

    }
    

    protected void SaveRegistrationfees()
    {

        string pk = null;
        pk = Lblpk.Text;
        Session["Lblpk.Text"] = pk;
        
        string DivisionCode = null;
        DivisionCode = Ddldivisioncode.Text;

        string acadyear = null;
        acadyear = ddlAcadYearedit.Text;

        string course = null;
        course = Ddlcoursecode.Text;



        string Feezone = null;
        Feezone = ddlFeesZone.Text;

        string coursedate = null;
        coursedate = txtCoursePeriod.Value;
        Crs_SDate = coursedate.Substring(0, 10);
        Crs_EDate = coursedate.Substring(13, 10);

        //string Sdate = "";
        //string Edate = "";

        string txtAmount = string.Empty;
        string Regcode = string.Empty;
        string VouchrName = string.Empty;
        string matcode = string.Empty;
        string txtedate = string.Empty;

        string ResultId = "0";
        string FDate = "";


        foreach (DataListItem TextBox in dsregst.Items)
        {

            CheckBox chkitemck = (CheckBox)TextBox.FindControl("chkCheck");
            Label lblRegcode = (Label)TextBox.FindControl("lblRegcode");
            if (chkitemck.Checked == true)
            {
                matcode = matcode + lblRegcode.Text + ",";
                Label lalvoutype = (Label)TextBox.FindControl("lalvoutype");
                // txtPeriod1.Value



                txtAmount = (TextBox.FindControl("txtAmount") as TextBox).Text;

                matcode = lblRegcode.Text;
                VouchrName = lalvoutype.Text;
                HtmlInputText txtPeriod1 = (HtmlInputText)TextBox.FindControl("txtPeriod1");

                HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                string UserID = cookie.Values["UserID"];
                string UserName = cookie.Values["UserName"];


                ResultId = ProductController.Insert_Update_robomate(DivisionCode, course, Feezone,
                    Crs_SDate, Crs_EDate, txtPeriod1.Value, "", matcode, VouchrName, txtAmount, "2", UserID, acadyear, ddlstreamcode.Text);
            }


        }



        


    }
    protected void btnCloseItemLevel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btnCloseItemHeader_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btnClose_SubGroupToResult_Click(object sender, EventArgs e)
    {
        try
        {
            ControlVisibility("Result");

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    #endregion
   
    
    protected void ddlClassRoomCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            divSubject.Visible = true;
            FillGrid_Get_subjectgroup();
            Label14.Text = "Select Subject Group ";

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string subjectgroupcode = "";

            CheckBox s = sender as CheckBox;
            //Set checked status of hidden check box to items in grid

            foreach (DataListItem dtlItem in dlSubjects.Items)
            {

                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lblSubgrCode = (Label)dtlItem.FindControl("lblSubgrCode");
                chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {

                    subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                }
                else
                {

                }
            }

            DataSet ds = ProductController.Get_subject_forSap1("2", Ddldivisioncode.Text, subjectgroupcode, txtCoursePeriod.Value);

            dlSubjects1.DataSource = ds;
            dlSubjects1.DataBind();
            divsubjects.Visible = true;
            Label22.Text = " Edit Subject Price ";
            FillRegistrationtab();

            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string subjectgroupcode = "";
            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dlSubjects.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lblSubgrCode = (Label)dtlItem.FindControl("lblSubgrCode");
                //chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {
                    subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                }
                else
                {

                }

                {

                    //subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                    
                    DataSet ds = ProductController.Get_subject_forSap1("2", Ddldivisioncode.Text, subjectgroupcode, txtCoursePeriod.Value);
                    dlSubjects1.DataSource = ds;
                    dlSubjects1.DataBind();
                    divsubjects.Visible = true;
                    Label22.Text = " Edit Subject Price ";
                    FillRegistrationtab();

                }

            }




            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    
    private void FillGrid_Get_subjectgroup()
    {
        try
        {
            DataSet dsSubject = ProductController.FillGrid_Get_subjectgroup("3", Ddldivisioncode.Text, Ddlcoursecode.Text);
            dlSubjects.DataSource = dsSubject;
            dlSubjects.DataBind();

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
    public System.Web.UI.WebControls.DropDownList ddlSubjectGroup { get; set; }
    
    protected void txtprise_Changed(object sender, EventArgs e)
    {
        DataListItem currentRow = (DataListItem)((TextBox)sender).Parent;
        TextBox txtprise = (TextBox)currentRow.FindControl("txtprise");
        TextBox txtCRF = (TextBox)currentRow.FindControl("txtCRF");
        TextBox txtTotal = (TextBox)currentRow.FindControl("txtTotal");


    }
    protected void txtCRF_Changed(object sender, EventArgs e)
    {
        DataListItem currentRow = (DataListItem)((TextBox)sender).Parent;


        TextBox txtprise = (TextBox)currentRow.FindControl("txtprise");
        TextBox txtCRF = (TextBox)currentRow.FindControl("txtCRF");
        TextBox txtTotal = (TextBox)currentRow.FindControl("txtTotal");
        TextBox TxtCRFvalue = (TextBox)currentRow.FindControl("TxtCRFvalue");

        int count = Convert.ToInt32(txtprise.Text) + Convert.ToInt32(txtCRF.Text);
        txtTotal.Text = count.ToString();

        decimal percent = (Convert.ToDecimal(txtCRF.Text) / Convert.ToDecimal(txtprise.Text)) * 100;
        TxtCRFvalue.Text = decimal.Round(percent, 2, MidpointRounding.AwayFromZero).ToString();



    }
    private void FillRegistrationtab()
    {

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        DataSet dsReg = ProductController.Get_Division_wiseCRF(Ddldivisioncode.Text, ddlClassRoomCourse.Text, "6");
        dsregst.DataSource = dsReg;
        dsregst.DataBind();
        divRegst.Visible = true;
        Label24.Text = "Add Registration/Robomate Fee";


    }
    public string Crs_SDate { get; set; }
    public string Crs_EDate { get; set; }
    public string Adm_SDate { get; set; }
    public string Adm_EDate { get; set; }
    public string Sub_Sdate { get; set; }
    public string Sub_Edate { get; set; }
    public int cnt { get; set; }
 
   
    protected void btnTopSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("SAP_Masterfile_Creation.aspx", false);
        divSubject.Visible = false; 
        divsubjects.Visible = false; divRegst.Visible = false; 
        
    }
    protected void ddlCenter1_SelectedIndexChanged(object sender, EventArgs e)
    {


        //FillDDL_Center1();

    }
    
    protected void BtnclosseditClick(object sender, EventArgs e)
    {

        {
            Response.Redirect("SAP_Masterfile_Creation.aspx", false);
            divSubject.Visible = false;
            divsubjects.Visible = false; divRegst.Visible = false; 
            

        }

    }
    
}