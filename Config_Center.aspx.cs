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

public partial class Config_Center : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page_Validation();
            FillDDL_Country();
            FillDDL_Division();
            FillDDL_Tax();
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

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        lblSuccess.Text = "";
        UpdatePanelMsgBox.Update();
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
            DivEditPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivEditPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            BtnSaveEdit.Visible = false;
            btnSave.Visible = true;

        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivEditPanel.Visible = true;
            BtnSaveEdit.Visible = true;
            btnSave.Visible = false;
        }

        else if (Mode == "EditClose")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            DivEditPanel.Visible = false;

        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }
        Clear_Error_Success_Box();

        ControlVisibility("Result");

        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetCenter(txtsearchcentername.Text.Trim(), ddlDivision.SelectedValue, 1);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlcenter.DataSource = dsGrid;
                dlcenter.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                DataList1.DataSource = dsGrid;
                DataList1.DataBind();
            }
            else
            {
                dlcenter.DataSource = null;
                dlcenter.DataBind();
                lbltotalcount.Text = "0";

                DataList1.DataSource = null;
                DataList1.DataBind();
            }
        }
        else
        {
            dlcenter.DataSource = null;
            dlcenter.DataBind();
            lbltotalcount.Text = "0";

            DataList1.DataSource = null;
            DataList1.DataBind();
        }

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        lblHeader_Add.Text = "Create New Center";
        clear_edit_addfiled();
        FillDDL_Country();
        FillDDL_Division();
        BtnSaveEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Config_Center.aspx");
    }
    protected void btnClear_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("Config_Center.aspx");
    }
    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {

        try
        {
            Clear_Error_Success_Box();
            if (txteditcentername.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Center ");
                txteditcentername.Focus();
                return;
            }

            if (ddlZone_edit.SelectedValue == "Select Zone")
            {
                Show_Error_Success_Box("E", "Select Zone");
                ddlZone_edit.Focus();
                return;
            }


            int count = 0;
            foreach (char c in Txteditaddress1.Text)
            {
                //if (c == charToCount)
                {
                    count++;
                }
            }
            if (count > 60)
            {
                Show_Error_Success_Box("E", "Address1 Character is More Then 60");
                return;
            }


            int count1 = 0;
            foreach (char c in Txteditaddress2.Text)
            {
                //if (c == charToCount)
                {
                    count1++;
                }
            }
            if (count1 > 60)
            {
                Show_Error_Success_Box("E", "Address2 Character is More Then 60");
                return;
            }

            int count2 = 0;
            foreach (char c in Txteditaddress3.Text)
            {
                //if (c == charToCount)
                {
                    count2++;
                }
            }
            if (count2 > 60)
            {
                Show_Error_Success_Box("E", "Address3 Character is More Then 60");
                return;
            }


            int count3 = 0;
            foreach (char c in Txteditbuilding.Text)
            {
                //if (c == charToCount)
                {
                    count3++;
                }
            }
            if (count3 > 60)
            {
                Show_Error_Success_Box("E", "Building Character is More Then 60");
                return;
            }



            int count4 = 0;
            foreach (char c in Txteditroom.Text)
            {
                //if (c == charToCount)
                {
                    count4++;
                }
            }
            if (count4 > 60)
            {
                Show_Error_Success_Box("E", "Room Character is More Then 60");
                return;
            }


            int count5 = 0;
            foreach (char c in Txteditflr.Text)
            {
                //if (c == charToCount)
                {
                    count5++;
                }
            }
            if (count5 > 60)
            {
                Show_Error_Success_Box("E", "Floor Character is More Then 60");
                return;
            }


           int SelCntCen = 0;

            foreach (DataListItem dtlItem in taxediet2.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lbltaxcode = (Label)dtlItem.FindControl("lbltaxcode");

                if (chkitemck.Checked == true)
                {
                    SelCntCen = SelCntCen + 1;

                }

            }
            if (SelCntCen == 0)
            {
                Show_Error_Success_Box("E", "Select Tax");
                return;
            }


            //if (txteditbuilding.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Building ");
            //    txteditbuilding.Focus();
            //    return;
            //}
            //if (txteditroom.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Room");
            //    txteditroom.Focus();
            //    return;
            //}
            //if (txteditfloor.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Floor");
            //    txteditfloor.Focus();
            //    return;
            //}
            //if (txteditarea.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Area");
            //    txteditarea.Focus();
            //    return;
            //}

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            int resultid = 0;


            int ActiveFlag = 0;
            if (chactiveedit.Checked == true)
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }

            //resultid = ProductController.Insert_Update_Center(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txteditcentercode.Text.Trim(), txteditcentername.Text, string.Empty, string.Empty, string.Empty, string.Empty, ActiveFlag, UserID, "2", string.Empty, ddlZone_edit.SelectedValue.ToString().Trim(), txtcenshrtname_edit.Text.Trim(), "");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Center Code already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateCenter(resultid.Tables[0].Rows[0]["ApplicationID"].ToString(), resultid.Tables[0].Rows[0]["SourceCenterCode"].ToString(), resultid.Tables[0].Rows[0]["SourceCenterName"].ToString(), resultid.Tables[0].Rows[0]["TargetApplicationId"].ToString(), resultid.Tables[0].Rows[0]["TargetCenterCode"].ToString(), resultid.Tables[0].Rows[0]["TargetCenterName"].ToString(), resultid.Tables[0].Rows[0]["SourceDivisionCode"].ToString(), resultid.Tables[0].Rows[0]["CenterShortName"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());

            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Center(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txteditcentercode.Text.Trim(), txteditcentername.Text, string.Empty, string.Empty, string.Empty, string.Empty, ActiveFlag, UserID, "3", string.Empty, ddlZone_edit.SelectedValue.ToString().Trim(), txtcenshrtname_edit.Text.Trim(), Return_Pkey_CRM);
            //    if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //    {

            //        lblSuccess.Text = "Records Saved Successfully!!";
            //        UpdatePanelMsgBox.Update();

            //        ControlVisibility("Result");

            //        DataSet dsGrid = new DataSet();
            //        dsGrid = ProductController.GetCenter(txtsearchcentername.Text.Trim(), ddlDivisionName.SelectedValue, 1);
            //        if (dsGrid != null)
            //        {
            //            if (dsGrid.Tables.Count != 0)
            //            {

            //                dlcenter.DataSource = dsGrid;
            //                dlcenter.DataBind();
            //                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            //            }
            //            else
            //            {
            //                dlcenter.DataSource = null;
            //                dlcenter.DataBind();
            //                lbltotalcount.Text = "0";
            //            }
            //        }
            //        else
            //        {
            //            dlcenter.DataSource = null;
            //            dlcenter.DataBind();
            //            lbltotalcount.Text = "0";
            //        }

            //        lblSuccess.Text = "Records Saved Successfully!!";
            //        UpdatePanelMsgBox.Update();

            //        return;

            //    }
            //}


            resultid = ProductController.Insert_Update_Center(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txteditcentercode.Text.Trim(), txteditcentername.Text, Txteditbuilding.Text, Txteditroom.Text, Txteditflr.Text, string.Empty, ActiveFlag, UserID, "2", string.Empty, ddlZone_edit.SelectedValue.ToString().Trim(), txtcenshrtname_edit.Text.Trim(), Txteditaddress1.Text.Trim(), Txteditaddress2.Text, Txteditaddress3.Text,Txteditcin.Text, Txteditgstin.Text);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Center Code already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {
                SAVETAXDETAILSEDIT();
                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();

                ControlVisibility("Result");

                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.GetCenter(txteditcentername.Text.Trim(), txtdivisioncode.Text, 1);
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {

                        dlcenter.DataSource = dsGrid;
                        dlcenter.DataBind();
                        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        dlcenter.DataSource = null;
                        dlcenter.DataBind();
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    dlcenter.DataSource = null;
                    dlcenter.DataBind();
                    lbltotalcount.Text = "0";
                }

                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();

                return;

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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
        try
        {
            Clear_Error_Success_Box();
            if (ddlCountry.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Country");
                ddlCountry.Focus();
                return;
            }
            if (ddlState.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select State");
                ddlState.Focus();
                return;
            }
            if (ddlCity.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select City");
                ddlCity.Focus();
                return;
            }
            if (ddllocation.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Location");
                ddllocation.Focus();
                return;
            }

            if (ddlZone.SelectedValue == "Select Zone")
            {
                Show_Error_Success_Box("E", "Select Zone");
                ddlZone.Focus();
                return;
            }


            if (ddlDivisionName.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivisionName.Focus();
                return;
            }
            if (txtcentercode.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Center Code");
                txtcentercode.Focus();
                return;
            }
            if (txtcentername.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Center Name");
                txtcentername.Focus();
                return;
            }


            if (txadd1.Text == "")
            {
                Show_Error_Success_Box("E", "Enter Center Address");
                txadd1.Focus();
                return;
            }

            if (TXTCIN.Text == "")
            {
                Show_Error_Success_Box("E", "Enter CIN Number");
                TXTCIN.Focus();
                return;
            }

            if (TXTGSTIN.Text == "")
            {
                Show_Error_Success_Box("E", "Enter GSTIN Number");
                TXTGSTIN.Focus();
                return;
            }


            int count = 0;
            foreach (char c in txadd1.Text)
            {
                //if (c == charToCount)
                {
                    count++;
                }
            }
            if (count > 60)
            {
                Show_Error_Success_Box("E", "Address1 Character is More Then 60");
                return;
            }


            int count1 = 0;
            foreach (char c in txtadd2.Text)
            {
                //if (c == charToCount)
                {
                    count1++;
                }
            }
            if (count1 > 60)
            {
                Show_Error_Success_Box("E", "Address2 Character is More Then 60");
                return;
            }

            int count2 = 0;
            foreach (char c in txtadd3.Text)
            {
                //if (c == charToCount)
                {
                    count2++;
                }
            }
            if (count2 > 60)
            {
                Show_Error_Success_Box("E", "Address3 Character is More Then 60");
                return;
            }


            int count3 = 0;
            foreach (char c in txtbuilding.Text)
            {
                //if (c == charToCount)
                {
                    count3++;
                }
            }
            if (count3 > 60)
            {
                Show_Error_Success_Box("E", "Building Character is More Then 60");
                return;
            }



            int count4 = 0;
            foreach (char c in txtroom.Text)
            {
                //if (c == charToCount)
                {
                    count4++;
                }
            }
            if (count4 > 60)
            {
                Show_Error_Success_Box("E", "Room Character is More Then 60");
                return;
            }


            int count5 = 0;
            foreach (char c in txtflr.Text)
            {
                //if (c == charToCount)
                {
                    count5++;
                }
            }
            if (count5 > 60)
            {
                Show_Error_Success_Box("E", "Floor Character is More Then 60");
                return;
            }

            int SelCntCen = 0;

            foreach (DataListItem dtlItem in dsTAX.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lbltaxcode = (Label)dtlItem.FindControl("lbltaxcode");

                if (chkitemck.Checked == true)
                {
                    SelCntCen = SelCntCen + 1;
                    
                }

             }
            if (SelCntCen == 0)
            {
                Show_Error_Success_Box("E", "Select Tax");
                return;
            }







            //if (txtbuilding.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Building Name");
            //    txtbuilding.Focus();
            //    return;
            //}
            //if (txtfloor.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Floor Name");
            //    txtfloor.Focus();
            //    return;
            //}
            //if (txtAddress.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Address");
            //    txtAddress.Focus();
            //    return;
            //}
            //if (txtRoom.Text == "")
            //{
            //    Show_Error_Success_Box("E", "Enter Room");
            //    txtRoom.Focus();
            //    return;
            //}

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

            //resultid = ProductController.Insert_Update_Center(ddlCountry.SelectedValue, ddlCity.SelectedValue, ddlCity.SelectedItem.Text, ddllocation.SelectedValue, ddllocation.SelectedItem.Text, ddlDivisionName.SelectedValue, txtcentercode.Text, txtcentername.Text, "", "", "", "", ActiveFlag, UserID, "1", ddlState.SelectedValue.ToString(), ddlZone.SelectedValue.ToString().Trim(), txtcenshort.Text.Trim(),"");

            //if (resultid.Tables[0].Rows[0]["Status"].ToString() == "-2")
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "Center Code already Exists!!";
            //    UpdatePanelMsgBox.Update();

            //    return;

            //}
            //else if (resultid.Tables[0].Rows[0]["Status"].ToString() == "1")
            //{

            //    Msg_Error.Visible = false;
            //    Msg_Success.Visible = true;

            //    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
            //    string Return_Pkey_CRM = ms.CreateCenter(resultid.Tables[0].Rows[0]["ApplicationID"].ToString(), resultid.Tables[0].Rows[0]["SourceCenterCode"].ToString(), resultid.Tables[0].Rows[0]["SourceCenterName"].ToString(), resultid.Tables[0].Rows[0]["TargetApplicationId"].ToString(), resultid.Tables[0].Rows[0]["TargetCenterCode"].ToString(), resultid.Tables[0].Rows[0]["TargetCenterName"].ToString(), resultid.Tables[0].Rows[0]["SourceDivisionCode"].ToString(), resultid.Tables[0].Rows[0]["CenterShortName"].ToString(), resultid.Tables[0].Rows[0]["IsActive"].ToString());

            //    resultid = null;
            //    resultid = ProductController.Insert_Update_Center(ddlCountry.SelectedValue, ddlCity.SelectedValue, ddlCity.SelectedItem.Text, ddllocation.SelectedValue, ddllocation.SelectedItem.Text, ddlDivisionName.SelectedValue, txtcentercode.Text, txtcentername.Text, "", "", "", "", ActiveFlag, UserID, "3", ddlState.SelectedValue.ToString(), ddlZone.SelectedValue.ToString().Trim(), txtcenshort.Text.Trim(), Return_Pkey_CRM);

            //    lblSuccess.Text = "Records Saved Successfully!!";
            //    UpdatePanelMsgBox.Update();
            //    clear_edit_addfiled();
            //    ///
            //    ControlVisibility("Result");

            //    DataSet dsGrid = new DataSet();
            //    dsGrid = ProductController.GetCenter(txtsearchcentername.Text.Trim(), ddlDivisionName.SelectedValue, 1);
            //    if (dsGrid != null)
            //    {
            //        if (dsGrid.Tables.Count != 0)
            //        {

            //            dlcenter.DataSource = dsGrid;
            //            dlcenter.DataBind();
            //            lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            //        }
            //        else
            //        {
            //            dlcenter.DataSource = null;
            //            dlcenter.DataBind();
            //            lbltotalcount.Text = "0";
            //        }
            //    }
            //    else
            //    {
            //        dlcenter.DataSource = null;
            //        dlcenter.DataBind();
            //        lbltotalcount.Text = "0";
            //    }

            //    return;

            //}

            resultid = ProductController.Insert_Update_Center(ddlCountry.SelectedValue, ddlCity.SelectedValue, ddlCity.SelectedItem.Text, ddllocation.SelectedValue, ddllocation.SelectedItem.Text, ddlDivisionName.SelectedValue, txtcentercode.Text, txtcentername.Text, txtbuilding.Text, txtroom.Text, txtflr.Text, "", ActiveFlag, UserID, "1", ddlState.SelectedValue.ToString(), ddlZone.SelectedValue.ToString().Trim(), txtcenshort.Text.Trim(), txadd1.Text.Trim(), txtadd2.Text, txtadd3.Text, TXTCIN.Text, TXTGSTIN.Text);

            if (resultid == -2)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Center Code already Exists!!";
                UpdatePanelMsgBox.Update();

                return;

            }
            else if (resultid == 1)
            {
                SAVETAXDETAILS();
                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                lblSuccess.Text = "Records Saved Successfully!!";
                UpdatePanelMsgBox.Update();
                clear_edit_addfiled();
                ///
                ControlVisibility("Result");

                DataSet dsGrid = new DataSet();
                dsGrid = ProductController.GetCenter(txtcenshrtname_edit.Text.Trim(), ddlDivisionName.SelectedValue, 1);
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {

                        dlcenter.DataSource = dsGrid;
                        dlcenter.DataBind();
                        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        dlcenter.DataSource = null;
                        dlcenter.DataBind();
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    dlcenter.DataSource = null;
                    dlcenter.DataBind();
                    lbltotalcount.Text = "0";
                }

                return;

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Center_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Center(s)</b></TD></TR>");
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
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_State();
        Clear_Error_Success_Box();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_City();
        Clear_Error_Success_Box();
    }
    private void FillDDL_State()
    {
        string Country_Code = null;
        Country_Code = ddlCountry.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlState, dsState, "State_Name", "State_Code");
        ddlState.Items.Insert(0, "Select");
        ddlState.SelectedIndex = 0;


    }



    private void FillDDL_Centre()
    {
        //dlCentre_Add.DataSource = null;
        //dlCentre_Add.DataBind();

        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];


        //DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
        //dlCentre_Add.DataSource = dsCentre;
        //dlCentre_Add.DataBind();
    }
    private void FillDDL_Country()
    {

        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlCountry, dsDivision, "Country_Name", "Country_Code");
        ddlCountry.Items.Insert(0, "Select");
        ddlCountry.SelectedIndex = 0;

    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void FillDDL_City()
    {
        string State_Code = null;
        State_Code = ddlState.SelectedValue;

        DataSet dsCity = ProductController.GetAllActiveCity(State_Code);
        BindDDL(ddlCity, dsCity, "City_Name", "City_Code");
        ddlCity.Items.Insert(0, "Select");
        ddlCity.SelectedIndex = 0;
    }


    private void FillDDL_Location()
    {
        DataSet dsCity = ProductController.GetallLocationbycity(ddlCity.SelectedValue.Trim());
        BindDDL(ddllocation, dsCity, "Location_Name", "Location_Code");
        ddllocation.Items.Insert(0, "Select");
        ddllocation.SelectedIndex = 0;
    }


    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Location();
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
    protected void dlcenter_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Edit");
            clear_edit_addfiled();
            lblslotid.Text = e.CommandArgument.ToString();

            FillCenterDetails(lblslotid.Text, e.CommandName);
        }
    }
    private void FillCenterDetails(string PKey, string CommandName)
    {

        try
        {

            string div_code = "";
            int zcon = 0;
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetCenter(lblslotid.Text.Trim(), "", 2);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {

                txtCountry_edit.Text = dsGrid.Tables[0].Rows[0]["Country_Name"].ToString();
                txtState_edit.Text = dsGrid.Tables[0].Rows[0]["State_Name"].ToString();
                txtCity_edit.Text = dsGrid.Tables[0].Rows[0]["City_Name"].ToString();
                txtLocation_edit.Text = dsGrid.Tables[0].Rows[0]["Location_Name"].ToString();
                txtDivision.Text = dsGrid.Tables[0].Rows[0]["Source_Division_ShortDesc"].ToString();
                div_code = dsGrid.Tables[0].Rows[0]["Source_Division_Code"].ToString();
                txtdivisioncode.Text = dsGrid.Tables[0].Rows[0]["Source_Division_Code"].ToString();
                txtdivisioncode.Visible = false;
                txtcenshrtname_edit.Text = dsGrid.Tables[0].Rows[0]["Short_Source_Center_Name"].ToString();
                FillDDL_Zone_edit(div_code);
                zcon = dsGrid.Tables[0].Rows[0]["Zone_Code"].ToString().Length;

                Txteditflr.Text = dsGrid.Tables[0].Rows[0]["Floor"].ToString();
                Txteditbuilding.Text = dsGrid.Tables[0].Rows[0]["Building"].ToString();
                Txteditroom.Text = dsGrid.Tables[0].Rows[0]["Room"].ToString();
                Txteditaddress1.Text = dsGrid.Tables[0].Rows[0]["Address_1"].ToString();
                Txteditaddress2.Text = dsGrid.Tables[0].Rows[0]["Address_2"].ToString();
                Txteditaddress3.Text = dsGrid.Tables[0].Rows[0]["Address_3"].ToString();
                Txteditcin.Text = dsGrid.Tables[0].Rows[0]["CIN_No"].ToString();
                Txteditgstin.Text = dsGrid.Tables[0].Rows[0]["GST_No"].ToString();

                if (zcon == 0)
                {
                    ddlZone_edit.SelectedIndex = 0;
                }
                else
                {
                    ddlZone_edit.SelectedValue = dsGrid.Tables[0].Rows[0]["Zone_Code"].ToString();
                }
                //ddlZone_edit.SelectedValue = dsGrid.Tables[0].Rows[0]["Zone_Code"].ToString();

                txteditcentercode.Text = dsGrid.Tables[0].Rows[0]["source_center_code"].ToString();
                txteditcentername.Text = dsGrid.Tables[0].Rows[0]["source_center_name"].ToString();
                //txteditbuilding.Text = dsGrid.Tables[0].Rows[0]["building"].ToString();
                //txteditroom.Text = dsGrid.Tables[0].Rows[0]["room"].ToString();
                //txteditfloor.Text = dsGrid.Tables[0].Rows[0]["floor"].ToString();
                //txteditarea.Text = dsGrid.Tables[0].Rows[0]["Address_1"].ToString();

                if (dsGrid.Tables[0].Rows[0]["Is_Active"].ToString() == "1")
                {

                    chactiveedit.Checked = true;
                }
                else
                {
                    chactiveedit.Checked = false;
                }


        //        foreach (DataListItem TextBox in dsTAX.Items)
        //{

        //    CheckBox chkitemck = (CheckBox)TextBox.FindControl("chkCheck");
        //    Label lbltaxcode = (Label)TextBox.FindControl("lbltaxcode");
                FillDDL_Taxedit();
                for (int cnt = 0; cnt <= dsGrid.Tables[1].Rows.Count - 1; cnt++)
                {

                    foreach (DataListItem dtlItem in taxediet2.Items)
                    {

                        CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                        Label lbltaxcode = (Label)dtlItem.FindControl("lbltaxcode");
                        if (Convert.ToString(lbltaxcode.Text).Trim() == Convert.ToString(dsGrid.Tables[1].Rows[cnt]["Taxcode"]).Trim())
                        {
                            chkitemck.Checked = true;
                            break;
                        }
                    }

                }



                btnSave.Visible = false;
                BtnSaveEdit.Visible = true;
                lblHeader_Add.Text = "Edit Board Details";
                divtaxedit1.Visible = true;
                Label7.Text = " TAX Details ";

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void btnClear_Edit_Click(object sender, EventArgs e)
    {
        ControlVisibility("EditClose");
        Clear_Error_Success_Box();
    }
    protected void ddlDivisionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Zone();
    }

    private void FillDDL_Zone()
    {
        DataSet dsCity = ProductController.GetallZonebyDivision(ddlDivisionName.SelectedValue.Trim(), 1);
        BindDDL(ddlZone, dsCity, "Zone_Name", "Zone_Code");
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }
    private void FillDDL_Tax()
    {
        DataSet ds = ProductController.GetallZonebyDivision("",4);

        dsTAX.DataSource = ds;
        dsTAX.DataBind();
        divtax.Visible = true;
        Label24.Text = " TAX Details ";
    }
    private void FillDDL_Taxedit()
    {
        DataSet ds = ProductController.GetallZonebyDivision("", 4);

        taxediet2.DataSource = ds;
        taxediet2.DataBind();
        divtaxedit1.Visible = true;
        Label7.Text = " TAX Details ";
    }

    


    private void FillDDL_Zone_edit(string div_code)
    {
        DataSet dsCity = ProductController.GetallZonebyDivision(div_code, 1);
        BindDDL(ddlZone_edit, dsCity, "Zone_Name", "Zone_Code");
        ddlZone_edit.Items.Insert(0, "Select Zone");
        ddlZone_edit.SelectedIndex = 0;
    }

    private void clear_edit_addfiled()
    {
        ddlCountry.Items.Clear();
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        ddllocation.Items.Clear();
        ddlDivisionName.Items.Clear();
        ddlZone.Items.Clear();
        txtcentercode.Text = "";
        txtcentername.Text = "";


        txtCountry_edit.Text = "";
        txtState_edit.Text = "";
        txtCity_edit.Text = "";
        txtLocation_edit.Text = "";
        txtDivision.Text = "";
        txteditcentercode.Text = "";
        txteditcentername.Text = "";
        txtcenshrtname_edit.Text = "";
        ddlZone_edit.Items.Clear();
    }



    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string taxcode = "";

            CheckBox s = sender as CheckBox;
            //Set checked status of hidden check box to items in grid

            foreach (DataListItem dtlItem in dsTAX.Items)
            {

                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lbltaxcode = (Label)dtlItem.FindControl("lbltaxcode");
                chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {

                    taxcode = taxcode + lbltaxcode.Text + ",";
                }
                else
                {

                }
            }

            //DataSet ds = ProductController.Get_subject_forSap1("2", ddlDivisionAdd.SelectedValue, subjectgroupcode, txtCoursePeriod.Value);

            //dlSubjects1.DataSource = ds;
            //dlSubjects1.DataBind();
            //divsubjects.Visible = true;
            //Label22.Text = " Add Subject Price ";
            //FillRegistrationtab();

            //Clear_Error_Success_Box();
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
            string taxcode = "";
            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dsTAX.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                Label lbltaxcode = (Label)dtlItem.FindControl("lbltaxcode");
                //chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {
                    taxcode = taxcode + lbltaxcode.Text + ",";
                }
                else
                {

                }

                //{

                //    //subjectgroupcode = subjectgroupcode + lblSubgrCode.Text + ",";
                //    DataSet ds = ProductController.Get_subject_forSap1("2", ddlDivisionAdd.SelectedValue, subjectgroupcode, txtCoursePeriod.Value);
                //    dlSubjects1.DataSource = ds;
                //    dlSubjects1.DataBind();
                //    divsubjects.Visible = true;
                //    Label22.Text = " Add Subject Price ";
                //    FillRegistrationtab();

                //}

            }




            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }


    protected void SAVETAXDETAILS()
    {
        
        
        
        string txtAmount = string.Empty;
        string Regcode = string.Empty;
        string VouchrName = string.Empty;
        string matcode = string.Empty;
        string txtedate = string.Empty;

        string ResultId = "0";
       
        string divisioncode = "";
            divisioncode=ddlDivisionName.SelectedValue;
            string center = "";
            center = txtcentercode.Text;

        foreach (DataListItem TextBox in dsTAX.Items)
        {

            CheckBox chkitemck = (CheckBox)TextBox.FindControl("chkCheck");
            Label lbltaxcode = (Label)TextBox.FindControl("lbltaxcode");
            if (chkitemck.Checked == true)
            {

                Label lalvoutype = (Label)TextBox.FindControl("lbltaxcode");
                Label lbltaxvalue = (Label)TextBox.FindControl("lbltaxvalue");
                Label lalsadte = (Label)TextBox.FindControl("lalsadte");
                Label laleadte = (Label)TextBox.FindControl("laleadte");

                DataSet DS= ProductController.InsertTaxmaster(divisioncode, center, lalvoutype.Text, lalsadte.Text, laleadte.Text, lbltaxvalue.Text);
            }


        }




    }
    protected void SAVETAXDETAILSEDIT()
    {



        string txtAmount = string.Empty;
        string Regcode = string.Empty;
        string VouchrName = string.Empty;
        string matcode = string.Empty;
        string txtedate = string.Empty;

        string ResultId = "0";

        string divisioncode = "";
        divisioncode = ddlDivisionName.SelectedValue;
        string center = "";
        center = txtcentercode.Text;

        foreach (DataListItem TextBox in taxediet2.Items)
        {

            CheckBox chkitemck = (CheckBox)TextBox.FindControl("chkCheck");
            Label lbltaxcode = (Label)TextBox.FindControl("lbltaxcode");
            if (chkitemck.Checked == true)
            {

                Label lalvoutype = (Label)TextBox.FindControl("lbltaxcode");
                Label lbltaxvalue = (Label)TextBox.FindControl("lbltaxvalue");
                Label lalsadte = (Label)TextBox.FindControl("lalsadte");
                Label laleadte = (Label)TextBox.FindControl("laleadte");

                DataSet DS = ProductController.InsertTaxmaster(txtdivisioncode.Text, txteditcentercode.Text.Trim(), lalvoutype.Text, lalsadte.Text, laleadte.Text, lbltaxvalue.Text);
            }


        }




    }

  
}