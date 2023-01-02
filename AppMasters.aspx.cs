using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatusMaster.aspx");
    }
    protected void btnAddressType_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddressType.aspx");
    }
    protected void btnVendorMaster_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VendorMaster.aspx");
    }
    protected void btnNendorAddress_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VendorAddress.aspx");
    }
    protected void btnDesignation_Master_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DesignationMaster.aspx");
    }
    protected void btnDivision_Responsible_person_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DivisionResponsibleperson.aspx");
    }
    protected void btnAgreement_Type_Master_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AgreementTypeMaster.aspx");
    }
    protected void btnPeriod_Master_Click(object sender, EventArgs e)
    {
       Response.Redirect("~/PeriodMaster.aspx");
    }
    protected void btnPayment_Type_Master_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PaymentTypeMaster.aspx");
    }
    protected void btnTerms_Of_Payment_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TermsOfPayment.aspx");
    }
    protected void btnCentre_Master_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CentreMaster.aspx");
    }
    protected void btnPower_Backup_Type_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PowerBackupType.aspx");
    }
    protected void btnProperty_Document_Type_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PropertyDocumentType.aspx");
    }
    protected void btnPremises_Type_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PremisesType.aspx");
    }
    //protected void btnVerticalMaster_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/VerticalMaster.aspx");
    //}
    protected void btnDocReferenceMasrer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DocReferenceMaster.aspx");
    }
    protected void btnVendorTypeMaster_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VendorTypeMaster.aspx");
    }
    protected void btnPaymentMode_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PaymentMode.aspx");
    }
}