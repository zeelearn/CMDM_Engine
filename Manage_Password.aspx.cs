using System;
using ShoppingCart.BL;
using System.Web;



public partial class Manage_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        if (string.IsNullOrEmpty(txtoldPassword.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter Old Password");
            txtoldPassword.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtNewPassword1.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Enter New Password");
            txtNewPassword1.Focus();
            return;
        }


        if (string.IsNullOrEmpty(txtNewPassword2.Text.Trim()))
        {
            Show_Error_Success_Box("E", "Retype New Password");
            txtNewPassword2.Focus();
            return;
        }

        string oldPass = txtoldPassword.Text.Trim();
        string newpass = txtNewPassword1.Text.Trim();
        string retypepass = txtNewPassword2.Text.Trim();
        string ResultId = "";
        string ResultId1 = "";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        if (UserID == "")
            Response.Redirect("login.aspx");


        ResultId = ProductController.Change_Password_Chk(UserID, oldPass, 1);

        //Close the Add Panel and go to Search Grid
        if (ResultId == "1")
        {
            if (newpass == retypepass)
            {
                ResultId1 = ProductController.Change_Password_Chk(UserID, newpass, 2);

                if (ResultId1=="1")
                {
                    Show_Error_Success_Box("S", "Password Change Successful");
                    Response.Redirect("login.aspx");
                }
            }
            else
            {
                Show_Error_Success_Box("E", "Password dosen't match");
                txtNewPassword2.Focus();
                return;
            }
        }
        else if (ResultId == "0")
        {
            Show_Error_Success_Box("E", "Enter Correct Old Password");
            txtNewPassword2.Focus();
            return;
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}