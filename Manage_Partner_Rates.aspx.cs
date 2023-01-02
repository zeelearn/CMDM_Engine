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

partial class Manage_Partner_Rates : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDL_Activity();
            ddlPartner.Items.Insert(0, "Select");
            ddlPartner.SelectedIndex = 0;
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
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = True
        }
        Clear_Error_Success_Box();
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_Division()
    {
        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID , Company_Code, "", "", "2", DBname);
        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;
        //DataSet dsDivision = null;
        //OrderDataService.OrderDataServiceSoapClient client = new OrderDataService.OrderDataServiceSoapClient();
        //dsDivision = client.GetCompany_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        //if (dsDivision != null)
        //{
        //    if (dsDivision.Tables.Count != 0)
        //    {
        //        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //    }
        //    else
        //    {
                
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //    }
            
        //}
        //else
        //{

        //    ddlDivision.Items.Insert(0, "Select");
        //    ddlDivision.SelectedIndex = 0;
        //}

        

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;
    }
    
    private void FillDL_Activity()
    {
        DataSet dsGrid = ProductController.GetAllActiveActivity("", "2");
        BindDDL(ddlActivityType, dsGrid, "Activity_Name", "Activity_ID");
        ddlActivityType.Items.Insert(0, "Select");
        ddlActivityType.SelectedIndex = 0;
    }

    private void FillDDL_Partner()
    {
        DataSet dsPartner = ProductController.Get_Partner_Rates(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlActivityType.SelectedValue, "","", 1);
        BindDDL(ddlPartner, dsPartner, "PartnerName", "Partner_Code");
        ddlPartner.Items.Insert(0, "Select");
        ddlPartner.SelectedIndex = 0;
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void BindTableDDL(DropDownList ddl, DataTable ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillGrid_PartnerRates()
    {
        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcadYear.Focus();
            return;
        }

        if (ddlActivityType.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Activity Type");
            ddlAcadYear.Focus();
            return;
        }

        if (ddlPartner.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Partner");
            ddlPartner.Focus();
            return;
        }

        if (ddlActivityType.SelectedValue == "01")//if Activity type is Classroom Lecture Delivery
        {

            ControlVisibility("Result");
            DataSet dsGrid = ProductController.Get_Partner_Rates(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlActivityType.SelectedValue, ddlPartner.SelectedValue,"", 2);

            //Copy dsGrid content from DataSet to DataTable
            DataTable dtGrid = null;
            dtGrid = dsGrid.Tables[0];

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
            }
            else
            {
                btnExport.Visible = false;
            }

            //Add 1 Blank records
            dtGrid.Rows.Add("", ddlPartner.SelectedItem.ToString(), ddlPartner.SelectedValue, ddlAcadYear.SelectedValue, "", "", "", "", "", "", "","","0", 0, 1); 

            dlGridDisplay.DataSource = dtGrid;
            //dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();
            
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {                
                Label lblPartnerRateCode = (Label)dtlItem.FindControl("lblPartnerRateCode");
                if (lblPartnerRateCode.Text == "")
                {
                    DropDownList ddlSubject = (DropDownList)dtlItem.FindControl("ddlSubject");
                    ddlSubject.Visible = true;
                    break;
                }

            }

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            lblActivityType_Result.Text = ddlActivityType.SelectedItem.ToString();
            lblPartner_Result.Text = ddlPartner.SelectedItem.ToString();
            lbltotalcount.Text = Convert.ToString(Convert.ToInt16(dsGrid.Tables[0].Rows.Count.ToString()) - 1);

        }//End if (ddlActivityType.SelectedValue == "01") //if Activity type is Classroom Lecture Delivery
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid_PartnerRates();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Partner();
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Partner();
    }

    protected void ddlActivityType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Partner();
    }
    

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        DropDownList ddlCourse = (DropDownList)e.Item.FindControl("ddlCourse");
        DropDownList ddlSubject = (DropDownList)e.Item.FindControl("ddlSubject");
        DropDownList ddlPaymentMode = (DropDownList)e.Item.FindControl("ddlPaymentMode");
        System.Web.UI.HtmlControls.HtmlInputText txtPartnerRatePeriod = (System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtPartnerRatePeriod");
        TextBox txtPartnerRate =(TextBox)e.Item.FindControl("txtPartnerRate");
        
        Label lblDLCourseName = (Label)e.Item.FindControl("lblDLCourseName");
        Label lblDLSubjectName = (Label)e.Item.FindControl("lblDLSubjectName");
        Label lblDLPaymentMode = (Label)e.Item.FindControl("lblDLPaymentMode");
        Label lblDLRatePeriod = (Label)e.Item.FindControl("lblDLRatePeriod");        
        Label lblDLPartnerRate = (Label)e.Item.FindControl("lblDLPartnerRate");        
                
        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");                
        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

        if (e.CommandName == "Edit")
        {
            ddlCourse.Visible = true;
            ddlSubject.Visible = true;
            ddlPaymentMode.Visible = true;
            txtPartnerRatePeriod.Visible = true;
            txtPartnerRate.Visible = true;

            lblDLCourseName.Visible = false;
            lblDLSubjectName.Visible = false;
            lblDLPaymentMode.Visible = false;
            lblDLRatePeriod.Visible = false;
            lblDLPartnerRate.Visible = false;

            lnkDLEdit.Visible = false;
            lnkDLSave.Visible = true;
            icon_Error.Visible = false;
        }
        else if (e.CommandName == "Save")
        {
            //Validation
            if (txtPartnerRatePeriod.Value == "")
            {
                lbl_DLError.Title = "Enter Partner Rate Period";
                icon_Error.Visible = true;
                return;
            }

            if (ddlCourse.SelectedIndex == 0)
            {
                lbl_DLError.Title = "Select Course";
                icon_Error.Visible = true;
                ddlCourse.Focus();
                return;
            }

            if (ddlSubject.SelectedIndex == 0)
            {
                lbl_DLError.Title = "Subject";
                icon_Error.Visible = true;
                ddlSubject.Focus();
                return;
            }

            if (ddlPaymentMode.SelectedIndex == 0)
            {
                lbl_DLError.Title = "Enter Payment Mode";
                icon_Error.Visible = true;
                ddlPaymentMode.Focus();
                return;
            }

            if ((txtPartnerRate.Text.Trim() == "") || (txtPartnerRate.Text.Trim() == "0"))
            {
                lbl_DLError.Title = "Enter Partner Rate";
                icon_Error.Visible = true;
                txtPartnerRate.Focus();
                return;
            }

            //Saving part
            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];                        

            string PartnerRateCodeForEdit = "";
            PartnerRateCodeForEdit = e.CommandArgument.ToString();
            
            string PartnerRatePeriod="",PartnerRate_SDate="",PartnerRate_EDate="";
            PartnerRatePeriod = txtPartnerRatePeriod.Value;
            PartnerRate_SDate = PartnerRatePeriod.Substring(0, 10);
            PartnerRate_EDate = PartnerRatePeriod.Substring(13, 10);

            DataSet ds = ProductController.Insert_Update_Partner_Rates(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlActivityType.SelectedValue, ddlPartner.SelectedValue, ddlCourse.SelectedValue, ddlSubject.SelectedValue, ddlPaymentMode.SelectedValue, PartnerRate_SDate, PartnerRate_EDate, PartnerRateCodeForEdit,txtPartnerRate.Text, UserID, 1);

            if (ds.Tables[0].Rows[0]["ErrorSaveId"].ToString() == "-1")//if any error comes
            {
                lbl_DLError.Title = ds.Tables[0].Rows[0]["ErrorSaveMessage"].ToString();
                icon_Error.Visible = true;
                return;
            }
            else
            {
                icon_Error.Visible = false;
            }

            ////Change look
            //ddlCourse.Visible = false;
            //ddlSubject.Visible = false;
            //ddlPaymentMode.Visible = false;
            //txtPartnerRatePeriod.Visible = false;


            //lblDLCourseName.Visible = true;
            //lblDL.Visible = true;
            //lblDLLectureCnt.Visible = true;
            //lblDLLectureMin.Visible = true;
            //lblDLStatus.Visible = true;
            //lblDLChapterDisplayName.Visible = true;


            lnkDLEdit.Visible = true;
            lnkDLSave.Visible = false;

            FillGrid_PartnerRates();
        }

    }

    public Manage_Partner_Rates()
    {
        Load += Page_Load;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Partner Rates_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Partner Rates</b></TD></TR><TR><TD Colspan='2'><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Acad Year : " + ddlAcadYear.SelectedItem.ToString() + "</b></TD><TD Colspan='3'><b>Activity Type : " + ddlActivityType.SelectedItem.ToString() + "</b></TD></TR><TR><TD Colspan='2'><b>Partner : " + ddlPartner.SelectedItem.ToString() + "</b></TD><TD Colspan='5'></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridExport.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

        dlGridExport.Visible = false;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlActivityType.SelectedIndex = 0;
        ddlPartner.Items.Clear();
        ddlPartner.Items.Insert(0, "Select");
    }

    


    private bool IsNumeric(object value)
    {
        try
        {
            int i = Convert.ToInt32(value.ToString());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }


    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlCourse = (DropDownList)row.FindControl("ddlCourse");
        DropDownList ddlSubject = (DropDownList)row.FindControl("ddlSubject");

        if (!string.IsNullOrEmpty(ddlCourse.SelectedValue))
        {
            //Bind Subject 
            DataSet dsGrid = ProductController.Get_Partner_Rates(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlActivityType.SelectedValue, ddlPartner.SelectedValue, ddlCourse.SelectedValue, 4);
            ddlSubject.DataSource = dsGrid;
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "Subject_Code";
            ddlSubject.DataBind();

            ddlSubject.Items.Insert(0, "Select");
            ddlSubject.SelectedIndex = 0;  
        }

        //DropDownList ddl = sender as DropDownList;

        //foreach (DataListItem dtlItem in dlGridDisplay.Items)        
        //{
        //    //Finding Dropdown control  
        //    Control ctrl = dtlItem.FindControl("ddlCourse") as DropDownList;
        //    if (ctrl != null)
        //    {
        //        DropDownList ddl1 = (DropDownList)ctrl;
        //        //Comparing ClientID of the dropdown with sender
        //        if (ddl.ClientID == ddl1.ClientID)
        //        {
        //            Control ctrl1 = dtlItem.FindControl("ddlSubject") as DropDownList;
        //            DropDownList ddlSubject = (DropDownList)ctrl1;
        //            //DropDownList ddlSubject = dtlItem.FindControl("ddlSubject") as DropDownList;
        //            //Bind Subject 
        //            DataSet dsGrid = ProductController.Get_Partner_Rates(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlActivityType.SelectedValue, ddlPartner.SelectedValue, ddl1.SelectedValue, 4);
        //            //BindTableDDL(ddlSubject, dsGrid.Tables[0], "SubjectName", "Subject_Code");
        //            ddlSubject.DataSource = dsGrid.Tables[0];
        //            ddlSubject.DataBind();

        //            ddlSubject.Items.Insert(0, "Select");
        //            ddlSubject.SelectedIndex = 0;       

        //            break;
        //        }
        //    }
        //}
    }

    protected void dlGridDisplay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) | (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DropDownList ddlCourse = (DropDownList)e.Item.FindControl("ddlCourse");
            Label lblDLCourseCode = (Label)e.Item.FindControl("lblDLCourseCode");
            DropDownList ddlSubject = (DropDownList)e.Item.FindControl("ddlSubject");
            Label lblDLSubjectCode = (Label)e.Item.FindControl("lblDLSubjectCode");
            DropDownList ddlPaymentMode = (DropDownList)e.Item.FindControl("ddlPaymentMode");
            Label lblDLPaymentModeCode = (Label)e.Item.FindControl("lblDLPaymentModeCode");

            //Bind Course 
            DataSet dsGrid = ProductController.Get_Partner_Rates(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlActivityType.SelectedValue, ddlPartner.SelectedValue, lblDLCourseCode.Text, 3);
            BindTableDDL(ddlCourse, dsGrid.Tables[0], "CourseName", "Course_Code");
            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;
            BindTableDDL(ddlSubject, dsGrid.Tables[1], "SubjectName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            ddlSubject.SelectedIndex = 0;
            BindTableDDL(ddlPaymentMode, dsGrid.Tables[2], "PayMode", "PaymodeCode");
            ddlPaymentMode.Items.Insert(0, "Select");
            ddlPaymentMode.SelectedIndex = 0;

            if (lblDLCourseCode.Text != "")//If Course is Entered 
            {
                ddlCourse.SelectedValue = lblDLCourseCode.Text;
                if (lblDLSubjectCode.Text != "")
                {
                    ddlSubject.SelectedValue = lblDLSubjectCode.Text;
                }
            }

            if (lblDLPaymentModeCode.Text != "")
            {
                ddlPaymentMode.SelectedValue = lblDLPaymentModeCode.Text;
            }
        }
    }
}
