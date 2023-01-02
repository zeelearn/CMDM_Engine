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
public partial class Assign_RFID_To_Employee : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            //Page_Validation();
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
            
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            
        }
        
        Clear_Error_Success_Box();
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

    

    #endregion

    #region Events
   

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlEmployeeTYpe.SelectedValue = "0";
        Clear_Error_Success_Box();

    }

    

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {

        Clear_Error_Success_Box();
        FillGrid();
    }



    private void FillGrid()
    {
        int flag = 0;
        flag = Convert.ToInt32(ddlEmployeeTYpe.SelectedValue);

        if (flag == 0)
        {
            Show_Error_Success_Box("E", "Select Employee Type !!");
            return;
        }
        else
        {
            ControlVisibility("Result");
            if (flag == 1)
            {
                BtnSave.Visible = true;
            }
            else if (flag == 2)
            {
                BtnSave.Visible = false;

            }


            DataSet dsGrid = ProductController.GetAllUser_ForRFID(flag);
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {

                    dlGridDisplay.DataSource = dsGrid;
                    dlGridDisplay.DataBind();
                    lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
                }
                else
                {
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();
                    lbltotalcount.Text = "0";
                    BtnSave.Visible = false;
                }
            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = "0";
                BtnSave.Visible = false;
            }

        }
    }



    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        Clear_Error_Success_Box();
        try
        {
            if (e.CommandName == "Assign")
            {

                int ResultId = 0;
              ResultId = ProductController.Assig_Employee_RFID(e.CommandArgument.ToString());
              if (ResultId == 1)
              {
                  FillGrid();
                  Show_Error_Success_Box("S", "Employee RFID created successfully");
                  return;
              }
              else
              {
                  Show_Error_Success_Box("E", "Employee RFID not created");
                  return;
              }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E",ex.ToString());
            return;
        }
        
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        
        Clear_Error_Success_Box();
        try
        {
            bool flag = false;
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                TextBox txtEmp_RFID = (TextBox)dtlItem.FindControl("txtEmp_RFID");
                if (txtEmp_RFID.Text.Trim() != "")
                {
                    flag = true;
                    break;
                }

            }
            if (flag)
            {
                foreach (DataListItem dtlItem in dlGridDisplay.Items)
                {

                    int ResultId = 0;
                    TextBox txtEmp_RFID = (TextBox)dtlItem.FindControl("txtEmp_RFID");
                    Label lblUser_Code = (Label)dtlItem.FindControl("lblUser_Code");
                    HtmlAnchor lbl_DLError = (HtmlAnchor)dtlItem.FindControl("lbl_DLError");
                    Panel icon = (Panel)dtlItem.FindControl("icon");

                    lbl_DLError.Title = "";
                    icon.Visible = false;
                    if (txtEmp_RFID.Text.Trim() != "")
                    {
                        ResultId = ProductController.Update_Employee_RFID(lblUser_Code.Text.Trim(), txtEmp_RFID.Text.Trim());
                        if (ResultId == -1)
                        {
                            lbl_DLError.Title = "This Employee RFID is already assigned to other employee";
                            icon.Visible = true;
                            txtEmp_RFID.Focus();

                        }
                    }

                }
            }
            else
            {
                Show_Error_Success_Box("E", "Enter atleast one employee RFID !!");
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }
    
    #endregion
        
}