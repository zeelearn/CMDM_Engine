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
using System.Net.Http;
using LMSIntegration;
using System.Net.Http.Headers;

public partial class Config_Board : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
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

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }

    protected void dllecture_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");

            lblslotid.Text = e.CommandArgument.ToString();
            FillBoardDetails(lblslotid.Text, e.CommandName);
        }
    }
    private void FillBoardDetails(string PKey, string CommandName)
    {

        try
        {

            string e = "";
            string flags = "2";
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetBoardDetails(string.Empty, PKey, flags);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtboardname.Text = dsGrid.Tables[0].Rows[0]["Long_Description"].ToString();
                txtboarddisplayname.Text = dsGrid.Tables[0].Rows[0]["Short_Description"].ToString();
                txtboardshortname.Text = dsGrid.Tables[0].Rows[0]["Short_Name"].ToString();
                txtboarddescription.Text = dsGrid.Tables[0].Rows[0]["description"].ToString();
                txtboardsequenceno.Text = dsGrid.Tables[0].Rows[0]["INS_id"].ToString();


                if (dsGrid.Tables[0].Rows[0]["Is_Active"].ToString() == "1")
                {

                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                btnSave.Visible = false;
                BtnSaveEdit.Visible = true;
                lblHeader_Add.Text = "Edit Board Details";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;

        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;

        }


    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        txtboardname.Text = "";
        txtboarddescription.Text = "";
        txtboardsequenceno.Text = "";
        txtboardshortname.Text = "";
        txtboarddisplayname.Text = "";

        Clear_Error_Success_Box();
        ControlVisibility("Add");
        BtnSaveEdit.Visible = false;
        //ddlAcademicYear_add.Enabled = true;
        //ddlDivision_add.Enabled = true;
        btnSave.Visible = true;
        lblHeader_Add.Text = "Add Board Details";

    }
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        lblSuccess.Text = "";
        UpdatePanelMsgBox.Update();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Result");
        string flags = "1";
        string var = "";
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetBoardDetails(txtSearchboardname.Text + "%", string.Empty, flags);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                ddlBoard.DataSource = dsGrid;
                ddlBoard.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();
            }
            else
            {
                ddlBoard.DataSource = null;
                ddlBoard.DataBind();
                lbltotalcount.Text = "0";

                DataList1.DataSource = null;
                DataList1.DataBind();
            }
        }
        else
        {
            ddlBoard.DataSource = null;
            ddlBoard.DataBind();
            lbltotalcount.Text = "0";

            DataList1.DataSource = null;
            DataList1.DataBind();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtboardname.Text == "")
        {
            Show_Error_Success_Box("E", "0090");
            txtboarddescription.Focus();
            return;
        }
        if (txtboarddisplayname.Text == "")
        {
            Show_Error_Success_Box("E", "0091");
            txtboarddisplayname.Focus();
            return;
        }
        if (txtboardshortname.Text == "")
        {
            Show_Error_Success_Box("E", "00101");
            txtboardshortname.Focus();
            return;
        }
        if (txtboardname.Text == "")
        {
            Show_Error_Success_Box("E", "00102");
            txtboardname.Focus();
            return;
        }
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string flag = "1";

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        //resultid = ProductController.Insert_Board(txtboardname.Text.Trim(), txtboarddisplayname.Text.Trim(), txtboardshortname.Text, txtboarddescription.Text.Trim(), txtboardsequenceno.Text.Trim(), UserID, ActiveFlag, flag, string.Empty, "");

        //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
        //{
        //    Msg_Error.Visible = true;
        //    Msg_Success.Visible = false;
        //    lblerror.Text = "Board Name already Exists!!";
        //    UpdatePanelMsgBox.Update();

        //    return;

        //}
        //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
        //{

        //    Msg_Error.Visible = false;
        //    Msg_Success.Visible = true;

        //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
        //    string Return_Pkey_CRM = ms.CreateBoard(resultid.Tables[0].Rows[0]["Id"].ToString(), resultid.Tables[0].Rows[0]["ShortDesc"].ToString(), resultid.Tables[0].Rows[0]["BoardName"].ToString(), resultid.Tables[0].Rows[0]["ShortName"].ToString(), resultid.Tables[0].Rows[0]["Description"].ToString(), "", "", resultid.Tables[0].Rows[0]["IsActive"].ToString());
        //    string Id = resultid.Tables[0].Rows[0]["Id"].ToString();
        //    resultid = null;

        //    resultid = ProductController.Insert_Board(txtboardname.Text.Trim(), txtboarddisplayname.Text.Trim(), txtboardshortname.Text, txtboarddescription.Text.Trim(), txtboardsequenceno.Text.Trim(), UserID, ActiveFlag, "3", Id, Return_Pkey_CRM);

        //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
        //    {
        //        lblSuccess.Text = "Records Saved Successfully!!";
        //    }

        //    UpdatePanelMsgBox.Update();

        //    return;

        //}


        resultid = ProductController.Insert_Board(txtboardname.Text.Trim(), txtboarddisplayname.Text.Trim(), txtboardshortname.Text, txtboarddescription.Text.Trim(), txtboardsequenceno.Text.Trim(), UserID, ActiveFlag, flag, string.Empty);

        if (resultid == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Board Name already Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Send_Details_LMSBroad(txtboardname.Text.Trim());

            return;

        }

    }
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
        Clear_Error_Success_Box();
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        if (txtboardname.Text == "")
        {
            Show_Error_Success_Box("E", "0090");
            txtboarddescription.Focus();
            return;
        }
        if (txtboarddisplayname.Text == "")
        {
            Show_Error_Success_Box("E", "0091");
            txtboarddisplayname.Focus();
            return;
        }
        if (txtboardshortname.Text == "")
        {
            Show_Error_Success_Box("E", "00101");
            txtboardshortname.Focus();
            return;
        }
        if (txtboardname.Text == "")
        {
            Show_Error_Success_Box("E", "00102");
            txtboardname.Focus();
            return;
        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string flag = "2";

        int ActiveFlag = 0;
        if (chkActive.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        //resultid = ProductController.Insert_Board(txtboardname.Text.Trim(), txtboarddisplayname.Text.Trim(), txtboardshortname.Text, txtboarddescription.Text.Trim(), txtboardsequenceno.Text.Trim(), UserID, ActiveFlag, flag, lblslotid.Text.Trim(), "");

        //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
        //{
        //    Msg_Error.Visible = true;
        //    Msg_Success.Visible = false;
        //    lblerror.Text = "Board Name already Exists!!";
        //    UpdatePanelMsgBox.Update();

        //    return;

        //}
        //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
        //{

        //    Msg_Error.Visible = false;
        //    Msg_Success.Visible = true;

        //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
        //    string Return_Pkey_CRM = ms.CreateBoard(resultid.Tables[0].Rows[0]["Id"].ToString(), resultid.Tables[0].Rows[0]["ShortDesc"].ToString(), resultid.Tables[0].Rows[0]["BoardName"].ToString(), resultid.Tables[0].Rows[0]["ShortName"].ToString(), resultid.Tables[0].Rows[0]["Description"].ToString(), "", "", resultid.Tables[0].Rows[0]["IsActive"].ToString());
        //    resultid = null;

        //    resultid = ProductController.Insert_Board(txtboardname.Text.Trim(), txtboarddisplayname.Text.Trim(), txtboardshortname.Text, txtboarddescription.Text.Trim(), txtboardsequenceno.Text.Trim(), UserID, ActiveFlag, "3", lblslotid.Text.Trim(), Return_Pkey_CRM);

        //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
        //    {
        //        lblSuccess.Text = "Records Saved Successfully!!";
        //    }

        //    UpdatePanelMsgBox.Update();
        //    Fill_Grid();
        //    return;

        //}


        resultid = ProductController.Insert_Board(txtboardname.Text.Trim(), txtboarddisplayname.Text.Trim(), txtboardshortname.Text, txtboarddescription.Text.Trim(), txtboardsequenceno.Text.Trim(), UserID, ActiveFlag, flag, lblslotid.Text.Trim());

        if (resultid == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Board Name already Exists!!";
            UpdatePanelMsgBox.Update();

            return;

        }
        else if (resultid == 1)
        {

            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            lblSuccess.Text = "Records Saved Successfully!!";
            UpdatePanelMsgBox.Update();
            Send_Details_LMSBroad(txtboardname.Text.Trim());
            Fill_Grid();
            return;

        }


    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Config_Board.aspx");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Board_" + DateTime.Now + ".xls";
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
        DataList1.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
        DataList1.Visible = false;
    }

    private void Fill_Grid()
    {
        ControlVisibility("Result");
        string flags = "1";
        string var = "";
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetBoardDetails(txtSearchboardname.Text + "%", string.Empty, flags);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                ddlBoard.DataSource = dsGrid;
                ddlBoard.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                ddlBoard.DataSource = null;
                ddlBoard.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            ddlBoard.DataSource = null;
            ddlBoard.DataBind();
            lbltotalcount.Text = "0";
        }
    }


    private void Send_Details_LMSBroad(string Borad)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        try
        {
            DataSet dsdetails = ProductController.GET_Borad_DETAILS(Borad);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DBConnection.connStringLMS);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Boarddetailsinsert = new Boardlist();
                Boarddetailsinsert.BoardId = dsdetails.Tables[0].Rows[0]["ID"].ToString();
                Boarddetailsinsert.BoardCode = dsdetails.Tables[0].Rows[0]["ID"].ToString();
                Boarddetailsinsert.BoardName = dsdetails.Tables[0].Rows[0]["Long_Description"].ToString();
                Boarddetailsinsert.BoardDisplayName = dsdetails.Tables[0].Rows[0]["Short_Description"].ToString();
                Boarddetailsinsert.BoardShortName = dsdetails.Tables[0].Rows[0]["Short_Name"].ToString();
                Boarddetailsinsert.BoardDescription = dsdetails.Tables[0].Rows[0]["Description"].ToString();
                Boarddetailsinsert.BoardSequenceNo = 1;
                Boarddetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Boarddetailsinsert.CreatedBy = UserID;
                Boarddetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["Created_On"]);
                Boarddetailsinsert.ModifiedBy = UserID;
                Boarddetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Is_Active"]);
                Boarddetailsinsert.IsDeleted = false;


                var response = client.PostAsJsonAsync("Board/addUpdBoard", Boarddetailsinsert).Result;

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
    class Boardlist
    {
        public string BoardId { get; set; }
        public string BoardCode { get; set; }
        public string BoardName { get; set; }
        public string BoardDisplayName { get; set; }
        public string BoardShortName { get; set; }
        public string BoardDescription { get; set; }
        public int BoardSequenceNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public string BoardURLName{get; set;}
  



    }
}