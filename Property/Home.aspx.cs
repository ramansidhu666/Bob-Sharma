using Property_cls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Property
{
    public partial class Home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.Cookies["UserEmail"] != null)
            {
                Response.Cookies["UserEmail"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Session["MySession"] = 0;
            }
          //  Response.Cookies["UserEmail"].Expires = DateTime.Now.AddDays(-1);
            //Session.Abandon();
            if (!IsPostBack)
            {
              int   userCount = Convert.ToInt32(HttpContext.Current.Session["MySession"]);

                if (userCount ==0 && Request.Cookies["UserEmail"] == null)
                {
                    //btnModal_Clicked(sender, e);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
               
            }

        }

        protected void btnSaveUserInfo_Clicked(object sender, EventArgs e)
        {
            string objuser;
            int userCount = 0;
            if (txtName.Value == "" && txtEmailID.Value == "" && txtPhone.Value == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error!", "alert('Fill all the details');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                HttpContext.Current.Session["MySession"] = 0;
              
            }
            else
            {
                cls_Property clsp = new cls_Property();
                var obj = clsp.Insert_UserInfo(txtName.Value, txtEmailID.Value, txtPhone.Value);
                string UserEmailId = ConfigurationManager.AppSettings["RegFromMailAddress"].ToString();
                string ToEmailId = ConfigurationManager.AppSettings["ToEmailID"].ToString();
                SendMailToAdmin(UserEmailId);
                SendMailToUser(UserEmailId);
                if (obj != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Congrts!", "alert('Your E-mail has been sent sucessfully - Thank You');", true);
                    objuser = "Success";
                    HttpContext.Current.Session["MySession"] = userCount + 1;
                    Response.Redirect("/Home.aspx");
                    //HttpCookie UserEmailcookies = new HttpCookie("UserEmail");
                    Response.Cookies["UserEmail"].Value = txtEmailID.Value;
                    
                    //UserEmailcookies.Value=Email;
                }
                else
                {
                    objuser = "Failure";
                }
            }

        }

        public void SendMailToAdmin(string UserEmailId)
        {
            MailMessage mail = new MailMessage();
            string ToEmailID = ConfigurationManager.AppSettings["ToEmailID"].ToString(); //From Email & To Email are same for admin
            //string ToEmailPassword = ConfigurationManager.AppSettings["ToEmailPassword"].ToString();
            string FromEmailID = ConfigurationManager.AppSettings["RegFromMailAddress"].ToString();
            string FromEmailPassword = ConfigurationManager.AppSettings["RegPassword"].ToString();
            string _Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();
            int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());
            Boolean _UseDefaultCredentials = false;
            Boolean _EnableSsl = true;
            mail.To.Add(ToEmailID);
            mail.From = new MailAddress(FromEmailID);
            mail.Subject = txtName.Value + " :  " + txtPhone.Value;
            string body = "";
            body = "<p>Person Name : " + txtName.Value + "</p>";
            body = body + "<p>Email ID : " + txtEmailID.Value + "</p>";
            body = body + "<p>Phone Number : " + txtPhone.Value + "</p>";
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _Host;
            smtp.Port = _Port;
            smtp.UseDefaultCredentials = _UseDefaultCredentials;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmailID, FromEmailPassword);// Enter senders User name and password
            smtp.EnableSsl = _EnableSsl;
            smtp.Send(mail);
        }
        public void SendMailToUser(string UserEmailId)
        {
            // Send mail.
            MailMessage mail = new MailMessage();

            string ToEmailID = txtEmailID.Value; //From Email & To Email are same for admin
            string FromEmailID = ConfigurationManager.AppSettings["RegFromMailAddress"].ToString();
            string FromEmailPassword = ConfigurationManager.AppSettings["RegPassword"].ToString();
            string _Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();
            int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());
            Boolean _UseDefaultCredentials = false;
            Boolean _EnableSsl = true;
            mail.To.Add(ToEmailID);
            mail.From = new MailAddress(FromEmailID);
            mail.Subject = "Bob Sharma";
            string body = "";
            body = "<p>Thanks for contacting us.</p>";
            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _Host;
            smtp.Port = _Port;
            smtp.UseDefaultCredentials = _UseDefaultCredentials;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmailID, FromEmailPassword);// Enter senders User name and password
            smtp.EnableSsl = _EnableSsl;
            smtp.Send(mail);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static String[] GetAutoCompleteData(string prefixText, int count, string contextKey)
        {
            List<String> itemNames = new List<String>();
            Property1.MLSDataWebServiceSoapClient ml = new Property1.MLSDataWebServiceSoapClient();
            DataTable dt = ml.GetAutoCompleteData(prefixText);
            foreach
                (DataRow dr in dt.Rows)
            {
                String item = dr["Province"].ToString();
                itemNames.Add(item);
            }
            string[] prefixTextArray = itemNames.ToArray();
            return prefixTextArray;
        }
    }
}