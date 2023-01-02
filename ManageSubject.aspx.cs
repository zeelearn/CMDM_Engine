using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Web.UI;
using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;

    public partial class ManageSubject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_Validation();
               // BindCourseCategory();
                FillDDL_Division();
                //BindBoardName();
                //BindMedium();
                ControlVisibility("Search");
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
        //private void BindBoardName()
        //{
        //    DataSet dsboardname = new DataSet();
        //    dsboardname = ProductController.GetBoardDetails(string.Empty, string.Empty, "3");
        //    BindDDL(ddlBoardName, dsboardname, "long_description", "id");
        //    ddlBoardName.Items.Insert(0, "Select");
        //    ddlBoardName.SelectedIndex = 0;
        //}
        //private void BindMedium()
        //{
        //    DataSet dsmedium = new DataSet();
        //    dsmedium = ProductController.Get_Medium_DetailsByPKey("%%");
        //    BindDDL(ddlMediumName, dsmedium, "MediumName", "id");
        //    ddlMediumName.Items.Insert(0, "Select");
        //    ddlMediumName.SelectedIndex = 0;
        //}
        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = true;
                btnTopSearch.Visible = false;
                BtnAdd.Visible = true;
                
            }
            else if (Mode == "TopSearch")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = true;
                btnTopSearch.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = false;
            }
            else if (Mode == "Result")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                btnTopSearch.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = true;
                btnTopSearch.Visible = true;


            }
            else if (Mode == "Add")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                btnTopSearch.Visible = true;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;


            }

        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlDivisionName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivisionName.Focus();
                return;
            }
            if (ddlsearchcoursename.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course Name");
                ddlsearchcoursename.Focus();
                return;
            }


            string flag;
            flag = "2";
            DataSet dsdlsubject = new DataSet();
            dsdlsubject = ProductController.GetSubject_ByCourse(ddlsearchcoursename.SelectedValue, txtsearchsubjectname.Text.Trim(),string.Empty, flag);
            //dlSubject.DataSource = dsdlsubject;
            //dlSubject.DataBind();
            
            if (dsdlsubject != null)
            {
                if (dsdlsubject.Tables.Count != 0)
                {

                    dlSubject.DataSource = dsdlsubject;
                    dlSubject.DataBind();

                    DataList1.DataSource = dsdlsubject;
                    DataList1.DataBind();

                    lbltotalcount.Text = dsdlsubject.Tables[0].Rows.Count.ToString();
                    Clear_Error_Success_Box();
                }
                else
                {
                    dlSubject.DataSource = null;
                    dlSubject.DataBind();

                    DataList1.DataSource = null;
                    DataList1.DataBind();

                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                dlSubject.DataSource = null;
                dlSubject.DataBind();

                DataList1.DataSource = null;
                DataList1.DataBind();

                lbltotalcount.Text = "0";
            }
            ControlVisibility("Result");
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            ControlVisibility("Add");
            lblHeader_Add.Text = "Create New Subject";
            txtSubjectSequenceNo.Text = "";
            txtSubjectName.Text = "";
            txtSubjectName.Enabled = true;
            txtSubjectDisplayName.Enabled = true;
            txtSubjectSequenceNo.Enabled = true;
            txtSubjectDisplayName.Text = "";
            ddlCourseName.Items.Clear();
            ddlDivision.SelectedIndex = 0;
            ddlCourseName.Enabled = true;
            BtnSaveAdd.Visible = true;
            BtnSaveEdit.Visible = false;

            Clear_Error_Success_Box();

            ddlDivision.Enabled = true;
            ddlCourseName.Enabled = true;
            chkreference.Enabled = true;
            chkreference.Visible = false;
            chkreference.Checked = false;
            ddlReferencecoursename.Enabled = true;
            ddlreferencesubjectname.Enabled = true;
            row3.Visible = false;


        }
       
        protected void btnTopSearch_Click(object sender, EventArgs e)
        {
            Clear_Error_Success_Box();
            ControlVisibility("TopSearch");

        }
        protected void chkreference_CheckedChanged(object sender, EventArgs e)
        {
            if (chkreference.Checked == true)
            {
                /* tblref.Visible = true;
                 tblref1.Visible = true;*/

                if (ddlDivision.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Division");
                    ddlDivision.Focus();
                    chkreference.Checked = false;
                    return;
                }
                if (ddlCourseName.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Select Course");
                    ddlCourseName.Focus();
                    chkreference.Checked = false;
                    return;
                }

                row3.Visible = true;
                ddlCourseName.Enabled = false;
                BindReferenceCourseName();

                ddlReferencecoursename.Items.Remove(ddlReferencecoursename.Items.FindByValue(ddlCourseName.SelectedValue));
                txtSubjectDisplayName.Enabled = false;
                txtSubjectSequenceNo.Enabled = false;
                txtSubjectName.Enabled = false;

            }
            else
            {
                row3.Visible = false;
                ddlCourseName.Enabled = true;
                txtSubjectDisplayName.Enabled = true;
                txtSubjectSequenceNo.Enabled = true;
                txtSubjectName.Enabled = true;
                txtSubjectDisplayName.Text = "";
                txtSubjectName.Text = "";
                txtSubjectSequenceNo.Text = "";

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
        //private void BindCourseCategory()
        //{
        //    DataSet dscategoryname = new DataSet();
        //    dscategoryname = ProductController.Get_CourseCategory("%%");
        //    BindDDL(ddlCategoryName, dscategoryname, "coursecategoryname", "CourseCateogryId");
        //    ddlCategoryName.Items.Insert(0, "Select");
        //    ddlCategoryName.SelectedIndex = 0;
           
 
           
        //}                   
        protected void ddlMediumName_SelectedIndexChanged(object sender, EventArgs e)
        {            
            BindCourseName();
            chkreference.Visible = true;
        }
        public void BindCourseName()
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourseName, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourseName.Items.Insert(0, "Select");
            ddlCourseName.SelectedIndex = 0;

            //DataSet dscoursename = new DataSet();
            //dscoursename = ProductController.GetCourseName(ddlCategoryName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, "1");
            //BindDDL(ddlCourseName, dscoursename, "course_name", "course_code");
            //ddlCourseName.Items.Insert(0, "Select");
            //ddlCourseName.SelectedIndex = 0;

        }
        public void BindCourseName_Search()
        {
            DataSet dscoursename = new DataSet();
            dscoursename = ProductController.GetCourseName(ddlDivisionName.SelectedValue, string.Empty, string.Empty, "3");
            BindDDL(ddlsearchcoursename, dscoursename, "course_name", "course_code");
            ddlsearchcoursename.Items.Insert(0, "Select");
            ddlsearchcoursename.SelectedIndex = 0;
            
            
        }

        public void BindAddCourseName()
        {
            DataSet dscoursename = new DataSet();
            dscoursename = ProductController.GetCourseName(ddlDivision.SelectedValue, string.Empty, string.Empty, "3");
            BindDDL(ddlCourseName, dscoursename, "course_name", "course_code");
            ddlCourseName.Items.Insert(0, "Select");
            ddlCourseName.SelectedIndex = 0;
        }
        private void BindReferenceCourseName()
        {
            string Div_Code = null;
            Div_Code = "";


            int CentreCnt = 0;
            for (CentreCnt = 0; CentreCnt <= ddlDivision.Items.Count - 1; CentreCnt++)
            {
                if (!ddlDivision.Items[CentreCnt].Value.Contains("Select"))
                {
                    Div_Code = Div_Code + ddlDivision.Items[CentreCnt].Value + ",";   
                }                          
                
            }
            Div_Code = Common.RemoveComma(Div_Code);



            DataSet dsAllStandard = ProductController.GetAllStandard_ByDivisionCode(Div_Code);
            BindDDL(ddlReferencecoursename, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlReferencecoursename.Items.Insert(0, "Select");
            ddlReferencecoursename.SelectedIndex = 0;
            //DataSet dsrefcoursename = new DataSet();
            //dsrefcoursename = ProductController.GetCourseName(ddlCategoryName.SelectedValue, string.Empty, string.Empty, "2");
            //txtSubjectSequenceNo.Text = dsrefcoursename.Tables[0].Rows[0]["coursesequenceno"].ToString();
            //DataSet dscoursename = new DataSet();
            //dscoursename = ProductController.GetCourseName(ddlCategoryName.SelectedValue, ddlBoardName.SelectedValue, ddlMediumName.SelectedValue, "1");
            //BindDDL(ddlReferencecoursename, dscoursename, "course_name", "course_code");
            //ddlReferencecoursename.Items.Insert(0, "Select");
            //ddlReferencecoursename.SelectedIndex = 0;          
            
           
        }
        private void BindReferenceSubjectName()
        {
            DataSet dsrefsubject = new DataSet();
            dsrefsubject = ProductController.GetSubject_ByCourse(ddlReferencecoursename.SelectedValue,string.Empty,string.Empty,"1");
            BindDDL(ddlreferencesubjectname, dsrefsubject, "subject_name", "subject_code");
            ddlreferencesubjectname.Items.Insert(0, "Select");
            ddlreferencesubjectname.SelectedIndex = 0;
            
           
        }
        protected void ddlReferencecoursename_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindReferenceSubjectName();
            
        }
        protected void ddlreferencesubjectname_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsrefsubject = new DataSet();
            dsrefsubject = ProductController.GetSubject_ByCourse(ddlReferencecoursename.SelectedValue,string.Empty,string.Empty,"1");

            txtSubjectDisplayName.Text = ddlreferencesubjectname.SelectedItem.ToString();
            txtSubjectName.Text = ddlreferencesubjectname.SelectedItem.ToString();
        }
        protected void BtnSaveAdd_Click(object sender, EventArgs e)
        {

            if (txtSubjectName.Text == "")
            {
                Show_Error_Success_Box("E", "0095");
                txtSubjectName.Focus();
                return;
            }
            if (ddlCourseName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourseName.Focus();
                return;
            }

            //if (TxtIcon.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Please Add Icon Colour Code");
            //    TxtIcon.Focus();
            //    return;
            //}
            //if (Txtfont.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Please Add Icon Font");
            //    Txtfont.Focus();
            //    return;
            //}

            if (chkreference.Checked == true)
            {
                if (ddlReferencecoursename.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0093");
                    ddlReferencecoursename.Focus();
                    return;
                }
                if (ddlreferencesubjectname.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0094");
                    ddlreferencesubjectname.Focus();
                    return;
                }
            }
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int resultid = 0;
            string flag = "1";

            string ActiveFlag = "0";
            if (chkActive.Checked == true)
            {
                ActiveFlag = "1";
            }
            else
            {
                ActiveFlag = "0";
            }


            if (chkreference.Checked == false)
            {
                // resultid = ProductController.Insert_Update_Subject(txtSubjectName.Text, ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.Text, UserID, txtSubjectDisplayName.Text.Trim(), ActiveFlag, flag, string.Empty, string.Empty, string.Empty, 0, string.Empty);
                resultid = ProductController.Insert_UPdate_SubjectNEW(ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.ToString(), txtSubjectName.Text, "0", string.Empty, string.Empty, txtSubjectDisplayName.Text, txtSubjectSequenceNo.Text.Trim(), UserID, ActiveFlag, flag, TxtIcon.Text, Txtfont.Text, ddlSubiconname.SelectedValue, "");

            }
            else
            {
                //resultid = ProductController.Insert_Update_Subject(txtSubjectName.Text, ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.Text, UserID, txtSubjectDisplayName.Text.Trim(), ActiveFlag, flag, ddlreferencesubjectname.SelectedValue, txtSubjectSequenceNo.Text.Trim(), ddlReferencecoursename.SelectedValue, 1, string.Empty);
                resultid = ProductController.Insert_UPdate_SubjectNEW(ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.ToString(), txtSubjectName.Text, "1", ddlReferencecoursename.SelectedValue, ddlreferencesubjectname.SelectedValue, txtSubjectDisplayName.Text, txtSubjectSequenceNo.Text.Trim(), UserID, ActiveFlag, flag, TxtIcon.Text, Txtfont.Text, ddlSubiconname.SelectedValue, "");
            }

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record already Exists!!";
                UpdatePanelMsgBox.Update();
                
                return;

            }
            else if (resultid == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();
                ControlVisibility("Result");
                Send_Subject_Details_LMS(ddlCourseName.SelectedValue, txtSubjectName.Text);

                return;

            }
        }

        protected void ddlSubiconname_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            DataSet ds = new DataSet();
            ds = ProductController.Getfont_icon(ddlSubiconname.SelectedValue);
           // ddlSubiconname.SelectedValue = ds.Tables[0].Rows[0]["Subject_Icon_Name"].ToString();
            TxtIcon.Text = ds.Tables[0].Rows[0]["Subject_Icon_Code"].ToString();
            Txtfont.Text = ds.Tables[0].Rows[0]["Subject_Font"].ToString();
           
            
        }




        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {
            ControlVisibility("Result");
            
        }
        protected void ddlsearchmediumname_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCourseName_Search();
        }
        protected void dlSubject_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Clear_Error_Success_Box();
            if (e.CommandName == "comEdit")
            {
                ControlVisibility("Add");

                lblslotid.Text = e.CommandArgument.ToString();
                FillSubjectDetails(lblslotid.Text, e.CommandName);
               
            }
            else if (e.CommandName == "comCopy")
            {
                try
                {
                    string copyData = e.CommandArgument.ToString();
                    string[] str = copyData.Split('%');

                    string course = str[0].ToString();
                    string subject = str[1].ToString();
                    string newcourse = str[2].ToString();
                    string newSubject = str[3].ToString();
                    string divCode = str[4].ToString();
                    int result = 0;
                    if (course != "" && subject != "" && newcourse != "" && newSubject != "")
                    {
                        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                        string UserID = cookie.Values["UserID"];
                        
                       result = ProductController.CopySubject_Chapter_Topic_SubTopic_Module_LessonPlan(course, subject, newcourse, newSubject, UserID,divCode);
                        if (result == 1)
                        {                           
                            FillGrid();
                            Msg_Error.Visible = false;
                            Msg_Success.Visible = true;
                            lblSuccess.Text = "Subject Copied Successfully!!";
                            UpdatePanelMsgBox.Update();
                            return;
                        }
                        else
                        {
                            Msg_Error.Visible = true;
                            Msg_Success.Visible = false;
                            lblerror.Text = "Subject not Copied !!";
                            UpdatePanelMsgBox.Update();
                            return;
                        }
                    }
                    else
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "There are some problem between reference Subject, so subject can't be copy";
                        UpdatePanelMsgBox.Update();
                        return;
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
        }
        private void FillSubjectDetails(string PKey, string CommandName)
        {
            //row1.Visible = false;
            //row2.Visible = false;

            try
            {

                
               
                DataSet dsGrid = new DataSet();
                //dsGrid = ProductController.GetBoardDetails(string.Empty, PKey, flags);
                dsGrid = ProductController.GetSubject_ByCourse(string.Empty, string.Empty, lblslotid.Text.Trim(), "3");

                if (dsGrid.Tables[0].Rows.Count > 0)
                {
                    if (ddlDivisionName.SelectedIndex != 0)
                    {
                        ddlDivision.SelectedValue = ddlDivisionName.SelectedValue;
                    }
                    ddlCourseName.Enabled = true;

                    ddlReferencecoursename.Items.Clear();
                    ddlreferencesubjectname.Items.Clear();
                    BindAddCourseName();
                    TxtIcon.Text = "";
                    Txtfont.Text = "";
                    ddlCourseName.SelectedValue = dsGrid.Tables[0].Rows[0]["Course_Code"].ToString();
                    //ddlCategoryName.SelectedValue = dsGrid.Tables[0].Rows[0]["subject_display_name"].ToString();
                    txtSubjectDisplayName.Text = dsGrid.Tables[0].Rows[0]["subject_display_name"].ToString();
                    txtSubjectName.Text = dsGrid.Tables[0].Rows[0]["Subject_Name"].ToString();
                    FillDDL_Subjecticonname();
                    if( dsGrid.Tables[0].Rows[0]["Subject_Icon_Name"].ToString() !="")
                        ddlSubiconname.SelectedValue = dsGrid.Tables[0].Rows[0]["Subject_Icon_Name"].ToString();

                    if (dsGrid.Tables[0].Rows[0]["Subject_Icon_Colour"].ToString() != "")
                        TxtIcon.Text = dsGrid.Tables[0].Rows[0]["Subject_Icon_Colour"].ToString();
                    
                    if(dsGrid.Tables[0].Rows[0]["Icon_Font"].ToString() !="")
                        Txtfont.Text = dsGrid.Tables[0].Rows[0]["Icon_Font"].ToString();

                    txtsearchsubjectname.Enabled = true;
                    lblHeader_Add.Text = "Edit Subject Details";
                    txtSubjectSequenceNo.Text = dsGrid.Tables[0].Rows[0]["CourseSequenceNo"].ToString();
                    txtsearchsubjectname.Enabled = true;
                    txtSubjectDisplayName.Enabled = true;
                    txtSubjectSequenceNo.Enabled = true;
                    txtSubjectName.Enabled = true;
                    chkreference.Enabled = false;


                    if (Convert.ToInt32(dsGrid.Tables[0].Rows[0]["Is_Active"]) == 0)
                    {
                        chkActive.Checked = false;
                    }
                    else
                    {
                        chkActive.Checked = true;
                    }

                    if (Convert.ToInt32(dsGrid.Tables[0].Rows[0]["IsReference"]) == 0)
                    {
                        row3.Visible = false;
                        chkreference.Checked = false;
                        chkreference.Visible = true;
                        chkreference.Enabled = true;
                        ddlReferencecoursename.Enabled = true;
                        ddlreferencesubjectname.Enabled = true;
                    }
                    else
                    {
                        row3.Visible = true;
                        ddlReferencecoursename.Enabled = false;
                        ddlreferencesubjectname.Enabled = false;
                        // ddlCourseName.Enabled = false;
                        chkreference.Checked = true;
                        BindReferenceCourseName();

                        ddlReferencecoursename.Items.Remove(ddlReferencecoursename.Items.FindByValue(ddlCourseName.SelectedValue));

                        ddlReferencecoursename.SelectedValue = dsGrid.Tables[0].Rows[0]["CourseReferenceCode"].ToString();
                        DataSet dsrefsubject = new DataSet();
                        dsrefsubject = ProductController.GetSubject_ByCourse(ddlReferencecoursename.SelectedValue, string.Empty, string.Empty, "1");
                        BindDDL(ddlreferencesubjectname, dsrefsubject, "subject_name", "subject_code");
                        ddlreferencesubjectname.Items.Insert(0, "Select");
                        ddlreferencesubjectname.SelectedIndex = 0;
                        ddlreferencesubjectname.SelectedValue = dsGrid.Tables[0].Rows[0]["SubjectReferenceCode"].ToString();
                    }


                    BtnSaveAdd.Visible = false;
                    BtnSaveEdit.Visible = true;

                }

            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
                return;
            }
        }


        protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkreference.Visible = true;
            
        }
        protected void BtnSaveEdit_Click(object sender, EventArgs e)
        {

            if (txtSubjectName.Text == "")
            {
                Show_Error_Success_Box("E", "0095");
                txtSubjectName.Focus();
                return;
            }
            if (ddlCourseName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourseName.Focus();
                return;
            }
            //if (TxtIcon.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Please Add Icon Colour Code");
            //    TxtIcon.Focus();
            //    return;
            //}
            //if (Txtfont.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Please Add Icon Font");
            //    Txtfont.Focus();
            //    return;
            //}

            if (chkreference.Checked == true)
            {
                if (ddlReferencecoursename.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0093");
                    ddlReferencecoursename.Focus();
                    return;
                }
                if (ddlreferencesubjectname.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0094");
                    ddlreferencesubjectname.Focus();
                    return;
                }
            }

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int resultid = 0;
            string flag = "2";

            string ActiveFlag = "0";
            if (chkActive.Checked == true)
            {
                ActiveFlag = "1";
            }
            else
            {
                ActiveFlag = "0";
            }



            //resultid = ProductController.Insert_Update_Subject(txtSubjectDisplayName.Text.Trim(), string.Empty, string.Empty, UserID, string.Empty, ActiveFlag, flag, string.Empty, txtSubjectSequenceNo.Text, lblslotid.Text.Trim(),0);

            if (chkreference.Checked == false)
            {
                // resultid = ProductController.Insert_Update_Subject(txtSubjectName.Text, ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.Text, UserID, txtSubjectDisplayName.Text.Trim(), ActiveFlag, flag, string.Empty, string.Empty, string.Empty, 0, string.Empty);
                resultid = ProductController.Insert_UPdate_SubjectNEW(ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.ToString(), txtSubjectName.Text, "0", string.Empty, string.Empty, txtSubjectDisplayName.Text, txtSubjectSequenceNo.Text.Trim(), UserID, ActiveFlag, flag, TxtIcon.Text, Txtfont.Text, lblslotid.Text, ddlSubiconname.SelectedValue);

            }
            else
            {
                //resultid = ProductController.Insert_Update_Subject(txtSubjectName.Text, ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.Text, UserID, txtSubjectDisplayName.Text.Trim(), ActiveFlag, flag, ddlreferencesubjectname.SelectedValue, txtSubjectSequenceNo.Text.Trim(), ddlReferencecoursename.SelectedValue, 1, string.Empty);
                resultid = ProductController.Insert_UPdate_SubjectNEW(ddlCourseName.SelectedValue, ddlCourseName.SelectedItem.ToString(), txtSubjectName.Text, "1", ddlReferencecoursename.SelectedValue, ddlreferencesubjectname.SelectedValue, txtSubjectDisplayName.Text, txtSubjectSequenceNo.Text.Trim(), UserID, ActiveFlag, flag, TxtIcon.Text, Txtfont.Text, lblslotid.Text, ddlSubiconname.SelectedValue);
            }

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {

                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();
                Send_Subject_Details_LMS(ddlCourseName.SelectedValue, txtSubjectName.Text);

                ////

                FillGrid();
                return;

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
                BindDDL(ddlDivisionName, dsDivision, "Division_Name", "Division_Code");
                ddlDivisionName.Items.Insert(0, "Select");
                ddlDivisionName.SelectedIndex = 0;
                
                BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
                ddlDivision.Items.Insert(0, "Select");
                ddlDivision.SelectedIndex = 0;
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

        private void FillDDL_Subjecticonname()
        {

            try
            {

                Clear_Error_Success_Box();

                DataSet dssub = ProductController.GetAllActive_sub_icon_name(ddlCourseName.SelectedValue);
                BindDDL(ddlSubiconname, dssub, "Subject_Icon_Name", "Record_Number");
                ddlSubiconname.Items.Insert(0, "Select");
                ddlSubiconname.SelectedIndex = 0;
                //ddlSubiconname.SelectedValue = dssub.Tables[0].Rows[0]["Record_Number"].ToString();

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





        protected void ddlDivisionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCourseName_Search();
        }
        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageSubject.aspx");
        }

        private void FillGrid()
        {
            try
            {
                Clear_Error_Success_Box();
                string flag;
                flag = "2";
                DataSet dsdlsubject = new DataSet();
                dsdlsubject = ProductController.GetSubject_ByCourse(ddlsearchcoursename.SelectedValue, txtsearchsubjectname.Text.Trim(), string.Empty, flag);
                //dlSubject.DataSource = dsdlsubject;
                //dlSubject.DataBind();

                if (dsdlsubject != null)
                {
                    if (dsdlsubject.Tables.Count != 0)
                    {

                        dlSubject.DataSource = dsdlsubject;
                        dlSubject.DataBind();
                        lbltotalcount.Text = dsdlsubject.Tables[0].Rows.Count.ToString();
                        Clear_Error_Success_Box();
                    }
                    else
                    {
                        dlSubject.DataSource = null;
                        dlSubject.DataBind();
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    dlSubject.DataSource = null;
                    dlSubject.DataBind();
                    lbltotalcount.Text = "0";
                }
                ControlVisibility("Result");

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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCourseName.Items.Clear();
            if (ddlDivision.SelectedIndex == 0)
            {                
                ddlDivision.Focus();
                return;
            }
            BindAddCourseName();
            chkreference.Visible = true;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataList1.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Subject_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Subject</b></TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            DataList1.RenderControl(oHtmlTextWriter1);
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();

            DataList1.Visible = false;
        }
        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnuploadviexcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subject_Master.aspx");
        }


        private void Send_Subject_Details_LMS(string Coursecode, string Subjectname)
        {
            string Response_Status_Code = "";
            string Response_Return_Phrase = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            try
            {
                DataSet dsdetails = ProductController.GET_Subject_DETAILS(Coursecode, Subjectname);
                if (dsdetails.Tables[0].Rows.Count > 0)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(DBConnection.connStringLMS);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Subjectdetailsinsert = new Subjectlist();
                    Subjectdetailsinsert.SubjectId = dsdetails.Tables[0].Rows[0]["Record_Number"].ToString();
                    Subjectdetailsinsert.SubjectCode = dsdetails.Tables[0].Rows[0]["Subject_Code"].ToString();
                    Subjectdetailsinsert.SubjectReferenceCode = dsdetails.Tables[0].Rows[0]["SubjectReferenceCode"].ToString();
                    Subjectdetailsinsert.CourseReferenceCode = dsdetails.Tables[0].Rows[0]["CourseReferenceCode"].ToString();
                    Subjectdetailsinsert.SubjectNo = "1";
                    Subjectdetailsinsert.SubjectName = dsdetails.Tables[0].Rows[0]["Subject_Name"].ToString();
                    Subjectdetailsinsert.SubjectDisplayName = dsdetails.Tables[0].Rows[0]["Subject_Display_Name"].ToString();
                    Subjectdetailsinsert.CourseCode = dsdetails.Tables[0].Rows[0]["Course_Code"].ToString();
                    Subjectdetailsinsert.IsReference = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsReference"]);
                    Subjectdetailsinsert.subject_icon = dsdetails.Tables[0].Rows[0]["Icon_Font"].ToString();
                    Subjectdetailsinsert.subject_icon_colour = dsdetails.Tables[0].Rows[0]["Subject_Icon_Colour"].ToString();
                    Subjectdetailsinsert.Reference_Course = null;
                    Subjectdetailsinsert.Reference_Subject = null;
                    Subjectdetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                    Subjectdetailsinsert.CreatedBy = UserID;
                    Subjectdetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Modified_On"]);
                    Subjectdetailsinsert.ModifiedBy = UserID;
                    Subjectdetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Is_Active"]);
                    Subjectdetailsinsert.IsDeleted = false;

                    if (Subjectdetailsinsert.SubjectReferenceCode == "")
                    {
                        Subjectdetailsinsert.SubjectReferenceCode = dsdetails.Tables[0].Rows[0]["Subject_Code"].ToString();
                    }
                    if (Subjectdetailsinsert.CourseReferenceCode == "")
                    {
                        Subjectdetailsinsert.CourseReferenceCode = dsdetails.Tables[0].Rows[0]["Course_Code"].ToString();
                    }

                    var response = client.PostAsJsonAsync("subject/addUpdSubject", Subjectdetailsinsert).Result;

                    Response_Status_Code = response.StatusCode.ToString();
                    Response_Return_Phrase = response.ReasonPhrase;

                    if (response.StatusCode.ToString() == "OK")
                    {
                    }
                    else
                    {
                        // DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
                    }
                }



            }
            catch (Exception e)
            {
                //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Partner_Code, Response_Status_Code, Response_Return_Phrase, UserID);
            }
        }
        class Subjectlist
        {
            public string SubjectId { get; set; }
            public string SubjectCode { get; set; }
            public string SubjectReferenceCode { get; set; }
            public string CourseReferenceCode { get; set; }
            public string SubjectNo { get; set; }
            public string SubjectName { get; set; }
            public string  SubjectDisplayName { get; set; }
            public string CourseCode { get; set; }
            public Boolean IsReference { get; set; }
            public string subject_icon { get; set; }
            public string subject_icon_colour { get; set; }
            public string Reference_Course { get; set; }
            public string Reference_Subject { get; set;}
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime ModifiedOn { get; set; }
            public string ModifiedBy { get; set; }
            public Boolean IsActive { get; set; }
            public Boolean IsDeleted { get; set; }
        

        }
}