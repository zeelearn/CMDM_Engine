using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using ShoppingCart.BL;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Post_Data_CRM_Manual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            ddlsearchfunction.Items.Insert(0, "Select");
            ddlsearchfunction.Items.Insert(1, "Country");
            ddlsearchfunction.Items.Insert(2, "State");
            ddlsearchfunction.Items.Insert(3, "City");
            ddlsearchfunction.Items.Insert(4, "Location");
            ddlsearchfunction.Items.Insert(5, "Company");
            ddlsearchfunction.Items.Insert(6, "Division");
            ddlsearchfunction.Items.Insert(7, "Center");
            ddlsearchfunction.Items.Insert(8, "Academic Year");
            ddlsearchfunction.Items.Insert(9, "Course Category");
            ddlsearchfunction.Items.Insert(10, "Institute");
            ddlsearchfunction.Items.Insert(11, "Board");
            ddlsearchfunction.Items.Insert(12, "Medium");
            ddlsearchfunction.Items.Insert(13, "Course");
            ddlsearchfunction.Items.Insert(14, "Product Type");
            ddlsearchfunction.Items.Insert(15, "Product Sub Type");
            ddlsearchfunction.Items.Insert(16, "Robomate Product");
            ddlsearchfunction.Items.Insert(17, "Contact Source");
            ddlsearchfunction.Items.Insert(18, "Contact Type");
            ddlsearchfunction.Items.Insert(19, "Customer Type");

        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {

            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //DivAddPanel.Visible = false;
            //BtnAdd.Visible = true;
            //txtsearchconceptname.Focus();
        }
        else if (Mode == "Result")
        {

            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //DivAddPanel.Visible = false;
            //BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            //DivEditPanel.Visible = false;
            //DivAddPanel.Visible = true;
            //DivResultPanel.Visible = false;
            //DivSearchPanel.Visible = false;
            //BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = false;
            //txtconceptname.Focus();
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlsearchfunction.SelectedIndex = 0;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        try
        {
            if (ddlsearchfunction.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Function");
                ddlsearchfunction.Focus();
                return;
            }

            dlGridReport1.DataSource = null;
            dlGridReport1.DataBind();
            dlGridReport1.Columns.Clear();

            DataSet dsGrid = null;
            dsGrid = ProductController.GET_CRM_DETAILS_POST(ddlsearchfunction.SelectedIndex);
            int ColCnt = 0;
            foreach (DataColumn col in dsGrid.Tables[0].Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                BoundField bfield = new BoundField();

                //Initalize the DataField value.
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                if (ColCnt == 0)
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    bfield.ItemStyle.Width = Unit.Pixel(390); //"390";
                }
                else
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                }

                //Add the newly created bound field to the GridView.
                dlGridReport1.Columns.Add(bfield);
                ColCnt = ColCnt + 1;
            }

            //Initialize the DataSource

            dlGridReport1.DataSource = dsGrid.Tables[0];

            //Bind the datatable with the GridView.
            dlGridReport1.DataBind();

            lbltotalcount.Text = Convert.ToString(dlGridReport1.Rows.Count);
            if (Convert.ToInt32(lbltotalcount.Text) > 0)
            {
                ControlVisibility("Result");
            }
            else
            {
                ControlVisibility("Search");
                Show_Error_Success_Box("E", "No Records Found");

            }
        }

        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            DataTable ResultId = new DataTable("ResultId");

            //Add columns to DataTable.
            foreach (TableCell cell in dlGridReport1.HeaderRow.Cells)
            {
                ResultId.Columns.Add(cell.Text);
            }

            //Loop through the GridView and copy rows.
            foreach (GridViewRow row in dlGridReport1.Rows)
            {
                ResultId.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    ResultId.Rows[row.RowIndex][i] = row.Cells[i].Text;
                }
            }

            //country
            if (ddlsearchfunction.SelectedIndex == 1)
            {

                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCountry(ResultId.Rows[i]["Countrycode"].ToString(), ResultId.Rows[i]["Countryname"].ToString(), ResultId.Rows[i]["IsActive"].ToString());
                    DataSet rs = null;
                    rs = ProductController.Insert_Update_Country(ResultId.Rows[i]["Countrycode"].ToString(), ResultId.Rows[i]["Countryname"].ToString(), "", 0, "3", Return_Pkey_CRM);

                }

            }

            //state
            if (ddlsearchfunction.SelectedIndex == 2)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateState(ResultId.Rows[i]["StateCode"].ToString(), ResultId.Rows[i]["StateName"].ToString(), ResultId.Rows[i]["Country"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Insert_Update_State(ResultId.Rows[i]["StateCode"].ToString(), "", "", "", 0, "3", Return_Pkey_CRM);
                }
            }

            //city
            if (ddlsearchfunction.SelectedIndex == 3)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCity(ResultId.Rows[i]["CityCode"].ToString(), ResultId.Rows[i]["City"].ToString(), ResultId.Rows[i]["State"].ToString(), ResultId.Rows[i]["Country"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Insert_Update_City("", "", "", "", 0, "3", ResultId.Rows[i]["CityCode"].ToString(), Return_Pkey_CRM);
                }
            }


            //location
            if (ddlsearchfunction.SelectedIndex == 4)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateLocation(ResultId.Rows[i]["LocationCode"].ToString(), ResultId.Rows[i]["LocationName"].ToString(), ResultId.Rows[i]["Country"].ToString(), ResultId.Rows[i]["State"].ToString(), ResultId.Rows[i]["City"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Insert_Update_Location(ResultId.Rows[i]["LocationCode"].ToString(), "", "", "", "", "", 0, "3", Return_Pkey_CRM);
                }
            }

            //company
            if (ddlsearchfunction.SelectedIndex == 5)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCompany(ResultId.Rows[i]["Company_Code"].ToString(), ResultId.Rows[i]["Company_Name"].ToString(), ResultId.Rows[i]["Company_Desc"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Update_Return_Pkey_CRM(1, ResultId.Rows[i]["Company_Code"].ToString(), Return_Pkey_CRM);

                }

            }

            //division
            if (ddlsearchfunction.SelectedIndex == 6)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateDivision(ResultId.Rows[i]["DivisionCode"].ToString(), ResultId.Rows[i]["DivisionShortDesc"].ToString(), ResultId.Rows[i]["DivisionLongDesc"].ToString(), ResultId.Rows[i]["RDV"].ToString(), ResultId.Rows[i]["CompanyCode"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Insert_Update_Division(ResultId.Rows[i]["DivisionCode"].ToString(), "", "", 0, "", "3", Return_Pkey_CRM);

                }

            }

            //center
            if (ddlsearchfunction.SelectedIndex == 7)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCenter(ResultId.Rows[i]["ApplicationID"].ToString(), ResultId.Rows[i]["SourceCenterCode"].ToString(), ResultId.Rows[i]["SourceCenterName"].ToString(), ResultId.Rows[i]["TargetApplicationId"].ToString(), ResultId.Rows[i]["TargetCenterCode"].ToString(), ResultId.Rows[i]["TargetCenterName"].ToString(), ResultId.Rows[i]["SourceDivisionCode"].ToString(), ResultId.Rows[i]["CenterShortName"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Insert_Update_Center("", "", "", "", "", "", ResultId.Rows[i]["SourceCenterCode"].ToString(), "", "", "", "", "", 0, "", "3", "", "", "", Return_Pkey_CRM);
                }
            }


            //academic year
            if (ddlsearchfunction.SelectedIndex == 8)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateAcademiYear(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["Description"].ToString());

                    DataSet rs = null;
                    rs = ProductController.InsertUpdateAcademic_Year(ResultId.Rows[i]["Id"].ToString(), "", 0, "", 3, Return_Pkey_CRM);
                }
            }

            //course category
            if (ddlsearchfunction.SelectedIndex == 9)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCourseCategory(ResultId.Rows[i]["CCID"].ToString(), ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["CourseCategoryName"].ToString(), ResultId.Rows[i]["CourseCategoryDisplayName"].ToString(), ResultId.Rows[i]["CourseCategoryDescription"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.InsertUpdateCourseCategory(ResultId.Rows[i]["CCID"].ToString(), "", 0, "", "", "", "", "3", Return_Pkey_CRM);
                }
            }

            //institute
            if (ddlsearchfunction.SelectedIndex == 10)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateInstitute(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString());
                    DataSet rs = null;
                    rs = ProductController.Insert_Update_ConfigTables("012", ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString(), UserID, "", "", Return_Pkey_CRM);
                }
            }

            //board
            if (ddlsearchfunction.SelectedIndex == 11)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateBoard(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["ShortDesc"].ToString(), ResultId.Rows[i]["BoardName"].ToString(), ResultId.Rows[i]["ShortName"].ToString(), ResultId.Rows[i]["Description"].ToString(), "", "", ResultId.Rows[i]["IsActive"].ToString());
                    DataSet rs = null;
                    rs = ProductController.Insert_Board("", "", "", "", "", UserID, 0, "3", ResultId.Rows[i]["Id"].ToString(), Return_Pkey_CRM);

                }
            }

            //medium
            if (ddlsearchfunction.SelectedIndex == 12)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateMedium(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["MediumName"].ToString(), ResultId.Rows[i]["DisplayName"].ToString(), ResultId.Rows[i]["ShortName"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString());
                    DataSet rs = null;
                    rs = ProductController.InsertUpdateMedium(ResultId.Rows[i]["Id"].ToString(), "", 0, "", "", "", "", "3", Return_Pkey_CRM);

                }
            }

            //course
            if (ddlsearchfunction.SelectedIndex == 13)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCourse(ResultId.Rows[i]["CourseCode"].ToString(), ResultId.Rows[i]["CourseName"].ToString(), ResultId.Rows[i]["RecordNumber"].ToString(), ResultId.Rows[i]["DivisionCode"].ToString(), ResultId.Rows[i]["Medium"].ToString(), ResultId.Rows[i]["CourseDisplayName"].ToString(), ResultId.Rows[i]["CourseShortName"].ToString(), ResultId.Rows[i]["CourseDescription"].ToString(), ResultId.Rows[i]["IsOnline"].ToString(), ResultId.Rows[i]["IsActive"].ToString(), ResultId.Rows[i]["CourseCategory"].ToString(), ResultId.Rows[i]["Board"].ToString());
                    DataSet rs = null;
                    rs = ProductController.InsertUpdateCourseDetail(ResultId.Rows[i]["CourseCode"].ToString(), "", "", "", "", "", "", "", "", "", 0, UserID, "3", 0, Return_Pkey_CRM);
                    
                }
            
            }

            //product type
            if (ddlsearchfunction.SelectedIndex == 14)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateProductType(ResultId.Rows[i]["Record_Id"].ToString(), ResultId.Rows[i]["Product_Type"].ToString(), ResultId.Rows[i]["Product_Description"].ToString(), ResultId.Rows[i]["Is_Deleted"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Update_Return_Pkey_CRM(2, ResultId.Rows[i]["Record_Id"].ToString(), Return_Pkey_CRM);

                }
            }

            //product sub type
            if (ddlsearchfunction.SelectedIndex == 15)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateProductSubType(ResultId.Rows[i]["Record_Id"].ToString(), ResultId.Rows[i]["Product_Type_Record_Id"].ToString(), ResultId.Rows[i]["Product_Subtype"].ToString(), ResultId.Rows[i]["Product_Subtype_Description"].ToString(), ResultId.Rows[i]["Is_Deleted"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Update_Return_Pkey_CRM(3, ResultId.Rows[i]["Record_Id"].ToString(), Return_Pkey_CRM);
                }

            }

            //robomate product
            if (ddlsearchfunction.SelectedIndex == 16)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateProduct(ResultId.Rows[i]["ProductCode"].ToString(), ResultId.Rows[i]["ProductName"].ToString(), ResultId.Rows[i]["DeviceSKU"].ToString(),
                        ResultId.Rows[i]["ProductDescription"].ToString(), ResultId.Rows[i]["ProductCategoryId"].ToString(), "",
                        ResultId.Rows[i]["CourseCategoryId"].ToString(), ResultId.Rows[i]["ProductTypeId"].ToString(), ResultId.Rows[i]["ProductSubTypeId"].ToString(),
                        ResultId.Rows[i]["AcadYear"].ToString(), ResultId.Rows[i]["CourseCode"].ToString(), ResultId.Rows[i]["BoardCode"].ToString(),
                        ResultId.Rows[i]["MediumCode"].ToString(), ResultId.Rows[i]["DivisionCode"].ToString(), ResultId.Rows[i]["IsDeleted"].ToString(),
                        ResultId.Rows[i]["IsActive"].ToString(), ResultId.Rows[i]["FromDate"].ToString(), ResultId.Rows[i]["ToDate"].ToString());


                    DataSet rs = null;
                    rs = ProductController.Update_Return_Pkey_CRM(4, ResultId.Rows[i]["ProductCode"].ToString(), Return_Pkey_CRM);
                }
            }


            //contatc source
            if (ddlsearchfunction.SelectedIndex == 17)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateContactSource(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString());

                    DataSet rs = null;
                    rs = ProductController.Insert_Update_ConfigTables("017", ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString(), UserID, "", "", Return_Pkey_CRM);
                }
            }

            //contact type
            if (ddlsearchfunction.SelectedIndex == 18)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCustomerType(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString());
                    DataSet rs = null;
                    rs = ProductController.Update_Return_Pkey_CRM(5, ResultId.Rows[i]["Id"].ToString(), Return_Pkey_CRM);
                }
            }

            //customer type
            if (ddlsearchfunction.SelectedIndex == 19)
            {
                for (int i = 0; i < ResultId.Rows.Count; i++)
                {
                    ServiceReference1.MastersClient ms = new ServiceReference1.MastersClient();
                    string Return_Pkey_CRM = ms.CreateCustomerType(ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString());
                    DataSet rs = null;
                    rs = ProductController.Insert_Update_ConfigTables("022", ResultId.Rows[i]["Id"].ToString(), ResultId.Rows[i]["Description"].ToString(), ResultId.Rows[i]["IsActive"].ToString(), UserID, "", "", Return_Pkey_CRM);
                }
            }

            BtnSearch_Click(sender, e);
        }

        catch (Exception ex)
        {
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btn_cose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
}