﻿using System;
using System.Data.SqlClient;

namespace Ehsbha_SP
{
    public partial class addInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ehsbhaWebApp\Ehsbha_SP\Ehsbha_SP\App_Data\ehsbhaDB.mdf;Integrated Security=True");
                    conn.Open();
                    String name = "select facilityName from users where userId='" + Session["User"].ToString() + "'";
                    SqlCommand com = new SqlCommand(name, conn);
                    fName.Text = "Facility Name: " + Convert.ToString(com.ExecuteScalar());
                    conn.Close();
                    TimeSpan t = home.lastDate - DateTime.Now;
                    String countDown = "The remining time for VAT return form: " + string.Format("{0} Days, {1} Hours ", (t.Days + 1), (t.Hours + 1));
                    timer.Text = countDown;
                }
                catch (Exception ex)
                {
                    Response.Write("Erroe: " + ex);
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void setting_Click(object sender, EventArgs e)
        {
            Response.Redirect("setting.aspx");
        }

        protected void langueg_Click(object sender, EventArgs e)
        {
            Response.Redirect("addInvoiceArabic.aspx");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("startPage.aspx");
        }

        protected void homePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void addInvoicePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("addInvoice.aspx");
        }

        protected void formPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("form.aspx");
        }

        protected void summaryPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("summary.aspx");
        }

        protected void contactPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("contact.aspx");
        }

        protected void SaleInvoice_Click(object sender, EventArgs e)
        {
            
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ehsbhaWebApp\Ehsbha_SP\Ehsbha_SP\App_Data\ehsbhaDB.mdf;Integrated Security=True");
                
                string insert = "INSERT INTO sale VALUES(@userId,@date,@price,@classification,@adjasment)";

                SqlCommand com = new SqlCommand(insert, conn);

                com.Parameters.AddWithValue("@userId", Session["User"].ToString());
                com.Parameters.AddWithValue("@date", saleDate.Text);
                com.Parameters.AddWithValue("@price", saleValue.Text);
                com.Parameters.AddWithValue("@classification", saleClassification.SelectedValue);
                if (saleAdjasmentdjasment.Checked)
                {
                    com.Parameters.AddWithValue("@adjasment", "yes");

                }
                else
                {
                    com.Parameters.AddWithValue("@adjasment", "no");
                }
                conn.Open();
                com.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("addInvoice.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex);
            }
        }
        protected void PurchaseInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ehsbhaWebApp\Ehsbha_SP\Ehsbha_SP\App_Data\ehsbhaDB.mdf;Integrated Security=True");
                conn.Open();
                string insert = "insert into purchase values(@userId,@date,@price,@classification,@adjasment)";
                SqlCommand com = new SqlCommand(insert, conn);
                com.Parameters.AddWithValue("@userId", Session["User"].ToString());
                com.Parameters.AddWithValue("@date", purchaseDate.Text);
                com.Parameters.AddWithValue("@price", purchaseValue.Text);
                com.Parameters.AddWithValue("@classification", purchaseClassification.SelectedValue);

                if (PurchaseAdjasment.Checked)
                {
                    com.Parameters.AddWithValue("@adjasment", "yes");
                }
                else
                {
                    com.Parameters.AddWithValue("@adjasment", "no");
                }
                com.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("addInvoice.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex);
            }

        }

        protected void saleT_Click(object sender, EventArgs e)
        {
            Page.Validate("saleT");
            if (!Page.IsValid)
                return;
        }

        protected void purchaseT_Click(object sender, EventArgs e)
        {
            Page.Validate("purchaseT");
            if (!Page.IsValid)
                return;
        }
    }
}