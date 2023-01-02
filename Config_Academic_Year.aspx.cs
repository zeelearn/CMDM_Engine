//using Microsoft.VisualBasic;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using ShoppingCart.BL;
//using System.Web.UI.WebControls;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web;
//using System.Net.Http;
//using LMSIntegration;
//using System.Net.Http.Headers;
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
using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;

public partial class Config_Academic_Year : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
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

      #endregion

    #region Methods 
   
    /// <summary>
    /// Manage Control Visibility
    /// </summary>
    /// <param name="Mode"></param>
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }
        Clear_Error_Success_Box();
    }
       
    /// <summary>
    /// Fill Academic Year Details
    /// </summary>
    /// <param name="PKey"></param>
    private void FillAcademicYearDetails(string PKey)
    {
        try
        {

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            DataSet dsAcademic = ProductController.GetAcademic_YearByPkey("", PKey, 1);
            if (dsAcademic != null)
            {
                if (dsAcademic.Tables.Count != 0)
                {
                    if (dsAcademic.Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToInt32(dsAcademic.Tables[0].Rows[0]["Is_Active"]) == 0)
                        {
                            chkActiveFlag.Checked = false;
                        }
                        else
                        {
                            chkActiveFlag.Checked = true;
                        }

                        txtAcademicYearCode.Text = dsAcademic.Tables[0].Rows[0]["ID"].ToString();
                        txtAcademicYearDec.Text = dsAcademic.Tables[0].Rows[0]["Description"].ToString();
                        

                    }
                }

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
      
    /// <summary>
    /// Clear Error Message
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    /// <summary>
    /// Show Error/Success Massage 
    /// </summary>
    /// <param name="BoxType"></param>
    /// <param name="Error_Code"></param>
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

    /// <summary>
    /// Clear Add Panel
    /// </summary>
    private void Clear_AddPanel()
    {
        
        txtAcademicYearCode.Text = "";
        txtAcademicYearDec.Text = "";
        chkActiveFlag.Checked = false;
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
        
    }
    
    /// <summary>
    /// Check Validation Control
    /// </summary>
    /// <returns></returns>
    private bool ValidationControl()
    {
        bool flag = true;

        if (string.IsNullOrEmpty(txtAcademicYearCode.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Academic Year Code can not be blank");
            txtAcademicYearCode.Focus();
            flag = false;
            return flag;
        }
        if (string.IsNullOrEmpty(txtAcademicYearDec.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Academic Year can not be blank");
            txtAcademicYearDec.Focus();
            flag = false;
            return flag;
            
        }
        return flag;

    }

    #endregion

    #region Events

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
        Clear_AddPanel();

        lblHeader_Add.Text = "Create New Academic Year";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;

    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
    }

    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
    {
        try
        {


            Clear_Error_Success_Box();

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            int ActiveFlag = 0;
            if (chkActiveFlag.Checked == true)
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }
            //DataSet ResultId = null;
            //if (ValidationControl())
            //{
                

            //    ResultId = ProductController.InsertUpdateAcademic_Year(txtAcademicYearCode.Text.Trim(), txtAcademicYearDec.Text.Trim(), ActiveFlag, CreatedBy, 2,"");

            //    ////Close the Add Panel and go to Search Grid
            //    if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
            //    {
            //        Show_Error_Success_Box("E", "Academic Year already exist");
            //        txtAcademicYearCode.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //        string Return_Pkey_CRM = ms.CreateAcademiYear(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["IsActive"].ToString());
                    
            //        ResultId = null;
            //        ResultId = ProductController.InsertUpdateAcademic_Year(txtAcademicYearCode.Text.Trim(), txtAcademicYearDec.Text.Trim(), ActiveFlag, CreatedBy,3,Return_Pkey_CRM );

                   
            //        UpdatePanelMsgBox.Update();
            //        ControlVisibility("Result");
            //        BtnSearch_Click(sender, e);
            //        Show_Error_Success_Box("S", "0000");
            //        Clear_AddPanel();
            //    }
            //}


            if (ValidationControl())
            {
                int ResultId = 0;

                //ResultId = ProductController.InsertUpdateAcademic_Year(txtAcademicYearCode.Text.Trim(), txtAcademicYearDec.Text.Trim(), ActiveFlag, CreatedBy, 2);

                ////Close the Add Panel and go to Search Grid
                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Academic Year already exist");
                    txtAcademicYearCode.Focus();
                    return;
                }
                else
                {
                    ControlVisibility("Result");
                    BtnSearch_Click(sender, e);
                    Show_Error_Success_Box("S", "0000");
                    Clear_AddPanel();
                    //Send_Details_LMS_edit(txtAcademicYearCode.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString()); 
        }
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Academic_Year_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Academic Year</b></TD></TR>");
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
        txtSearchAcademicYear.Text = "";

    }

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {

    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        
        
        string AcdYear = null;
        if (string.IsNullOrEmpty(txtSearchAcademicYear.Text.Trim()))
        {
            AcdYear = "";
        }
        else
        {
            AcdYear =   txtSearchAcademicYear.Text.Trim();
        }

        ControlVisibility("Result");

        DataSet dsGrid = ProductController.GetAcademic_YearByPkey(AcdYear,"" ,3);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();

                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
        }
        else
        {
            lbltotalcount.Text = "0";
        }
        
        
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            ControlVisibility("Add");
            BtnSave.Visible = false;
            BtnSaveEdit.Visible = true;

            lblPKey_Edit.Text = e.CommandArgument.ToString();
            lblHeader_Add.Text = "Edit Academic Year";

            FillAcademicYearDetails(e.CommandArgument.ToString());
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            //Validation


            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            int ActiveFlag = 0;
            if (chkActiveFlag.Checked == true)
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }


            //if (ValidationControl())
            //{
            //    DataSet ResultId = null;

            //    ResultId = ProductController.InsertUpdateAcademic_Year(txtAcademicYearCode.Text, txtAcademicYearDec.Text.Trim(), ActiveFlag, CreatedBy, 1,"");


            //    if (ResultId.Tables[0].Rows[0]["Status"].ToString() == "-1")
            //    {
            //        Show_Error_Success_Box("E", "Academic Year already exist");
            //        txtAcademicYearCode.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //        string Return_Pkey_CRM = ms.CreateAcademiYear(ResultId.Tables[0].Rows[0]["Id"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString(), ResultId.Tables[0].Rows[0]["Description"].ToString());
                    
            //        ResultId = null;
            //        ResultId = ProductController.InsertUpdateAcademic_Year(txtAcademicYearCode.Text.Trim(), txtAcademicYearDec.Text.Trim(), ActiveFlag, CreatedBy, 3, Return_Pkey_CRM);
            //        ControlVisibility("Result");
            //        BtnSearch_Click(sender, e);
            //        Show_Error_Success_Box("S", "0000");
            //        Clear_AddPanel();
            //    }
            //}

            if (ValidationControl())
            {
                int ResultId = 0;

                ResultId = ProductController.InsertUpdateAcademic_Year(txtAcademicYearCode.Text, txtAcademicYearDec.Text.Trim(), ActiveFlag, CreatedBy, 1);


                if (ResultId == -1)
                {
                    Show_Error_Success_Box("E", "Academic Year already exist");
                    txtAcademicYearCode.Focus();
                    return;
                }
                else
                {
                    Send_Details_LMS(txtAcademicYearCode.Text);
                    ControlVisibility("Result");
                    BtnSearch_Click(sender, e);
                    Show_Error_Success_Box("S", "0000");
                    Clear_AddPanel();
                   
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    public Config_Academic_Year()
    {
        Load += Page_Load;
    }


    private void Send_Details_LMS(string Acadyear)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        try
        {
            DataSet dsdetails = ProductController.GET_Acadyear_DETAILS(Acadyear);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DBConnection.connStringLMS);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Acaddetailsinsert = new Acaddetailsinsert();
                Acaddetailsinsert.AcademicYearCode = dsdetails.Tables[0].Rows[0]["ID"].ToString();
                Acaddetailsinsert.AcademicYearName = dsdetails.Tables[0].Rows[0]["ID"].ToString();
                Acaddetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Acaddetailsinsert.CreatedBy = UserID;
                Acaddetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Acaddetailsinsert.ModifiedBy = UserID;
                Acaddetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Is_Active"]);
                Acaddetailsinsert.IsDeleted = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Is_Active"]);
                Acaddetailsinsert.AcademicYearSequenceNo = 1;

                var response = client.PostAsJsonAsync("AcademicYear/addUpdAcademicYear", Acaddetailsinsert).Result;

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
    class Acaddetailsinsert
    {
        public string AcademicYearCode { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public int AcademicYearSequenceNo { get; set; }


    }



    //private void Send_Details_LMS()
    //{
       
      
    //    string Response_Status_Code = "";
    //    string Response_Return_Phrase = "";
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    string UserID = cookie.Values["UserID"];

    //    string checked_flag2 = "";

    //    if (chkActiveFlag.Checked == true)
    //    {
    //        checked_flag2 = "true";
    //    }
    //    else
    //    {
    //        checked_flag2= "False";
    //    }
    //    try
    //    {
           
    //            HttpClient client = new HttpClient();
    //            client.BaseAddress = new Uri(DBConnection.connStringLMS);
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            var Acadyeardetailsinsert = new AcadyearDetails();
    //            Acadyeardetailsinsert.AcademicYearCode = txtAcademicYearDec.Text.Trim();
    //            Acadyeardetailsinsert.AcademicYearName = txtAcademicYearDec.Text.Trim();
    //            Acadyeardetailsinsert.AcademicYearSequenceNo = "";
    //            Acadyeardetailsinsert.CreatedOn = DateTime.Now;
    //            Acadyeardetailsinsert.CreatedBy = UserID;
    //            Acadyeardetailsinsert.IsActive = Convert.ToBoolean(checked_flag2);
    //            //Acadyeardetailsinsert.IsDeleted = Convert.ToBoolean(chkActiveFlag.Value);

    //            var response = client.PostAsJsonAsync("/AcademicYear/addUpdAcademicYear", Acadyeardetailsinsert).Result;

    //            Response_Status_Code = response.StatusCode.ToString();
    //            Response_Return_Phrase = response.ReasonPhrase;

    //            if (response.StatusCode.ToString() == "OK")
    //            {

    //                //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, 1, academicYear, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
                 
    //            }
    //            else
    //            {
    //                //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, academicYear, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
    //            }
         



    //    }
    //    catch (Exception e)
    //    {
    //        DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, "", Response_Status_Code, Response_Return_Phrase, UserID);
    //    }
    //}
    //class AcadyearDetails
    //{
    //    public string AcademicYearCode { get; set; }
    //    public string AcademicYearName { get; set; }
    //    public string AcademicYearSequenceNo { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public Boolean IsActive { get; set; }
    //    public Boolean IsDeleted { get; set; }


    //}


    //private void Send_Details_LMS_edit(string academicYear)
    //{


    //    string Response_Status_Code = "";
    //    string Response_Return_Phrase = "";
    //    string checked_flag1 = "";

    //    if (chkActiveFlag.Checked == true)
    //    {
    //        checked_flag1 = "true";
    //    }
    //    else
    //    {
    //        checked_flag1 = "False";
    //    }
    //    HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
    //    string UserID = cookie.Values["UserID"];
    //    try
    //    {

    //        HttpClient client = new HttpClient();
    //        client.BaseAddress = new Uri(DBConnection.connStringLMS);
    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //        var AcadyeardetailsEdit = new AcadyearDetailsedit();
    //        AcadyeardetailsEdit.AcademicYearCode1 = lblPKey_Edit.Text.Trim();
    //        AcadyeardetailsEdit.AcademicYearName1 = lblPKey_Edit.Text.Trim();
    //        AcadyeardetailsEdit.AcademicYearSequenceNo1 = "1";
    //        AcadyeardetailsEdit.ModifiedOn1 = DateTime.Now;
    //        AcadyeardetailsEdit.ModifiedBy1 = UserID;
    //        AcadyeardetailsEdit.IsActive1 = Convert.ToBoolean(checked_flag1);
    //        //Acadyeardetailsinsert.IsDeleted = Convert.ToBoolean(chkActiveFlag.Value);

    //        var response = client.PostAsJsonAsync("/AcademicYear/addUpdAcademicYear", AcadyeardetailsEdit).Result;

    //        Response_Status_Code = response.StatusCode.ToString();
    //        Response_Return_Phrase = response.ReasonPhrase;

    //        if (response.StatusCode.ToString() == "OK")
    //        {

    //            //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, 1, academicYear, response.StatusCode.ToString(), response.ReasonPhrase, UserID);

    //        }
    //        else
    //        {
    //            //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, academicYear, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
    //        }




    //    }
    //    catch (Exception e)
    //    {
    //        //DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, academicYear, Response_Status_Code, Response_Return_Phrase, UserID);
    //    }
    //}
    //class AcadyearDetailsedit
    //{
    //    public string AcademicYearCode1 { get; set; }
    //    public string AcademicYearName1{ get; set; }
    //    public string AcademicYearSequenceNo1 { get; set; }
    //    public DateTime ModifiedOn1 { get; set; }
    //    public string ModifiedBy1 { get; set; }
    //    public Boolean IsActive1 { get; set; }
    //    public Boolean IsDeleted1 { get; set; }


    //}

    #endregion
}