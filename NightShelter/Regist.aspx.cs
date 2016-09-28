using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NightShelter
{
    public partial class Regist : System.Web.UI.Page
    {
        protected string firData;
        protected string location;
        protected string message;
        protected string errMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            message = "";
            errMessage = "None";
            if (Page.IsPostBack)
            {
                return;
            }
            if (String.IsNullOrEmpty(Request.Form["firData"]))
            {
                Response.Redirect("Verify.aspx");
            }

            firData = Request.Form["firData"];
            int gender = -1;
            int fingerID = -1;
            Int32.TryParse(Request.Form["gender"], out gender);
            Int32.TryParse(Request.Form["fingerID"], out fingerID);
            location = Request.Form["location"].Trim();

            ViewState["firData"] = firData;
            ViewState["gender"] = gender;
            ViewState["fingerID"] = fingerID;
            ViewState["location"] = location;

            string uid = FingerPrint.verify(firData, gender.ToString(), fingerID.ToString());

            if(uid.Equals("Not Found"))
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showForm", "showForm();", true);
            }
            else
            {
                string connStr = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connStr);

                try
                {
                    conn.Open();
                    string storeProc = "getDetailsProc";
                    MySqlCommand cmd = new MySqlCommand(storeProc, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firData", uid);
                    cmd.Parameters.AddWithValue("lim", 1);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        FingerPrint.Details detail = new FingerPrint.Details();
                        detail.firstName = rdr[1] == System.DBNull.Value ? null : (string)rdr[1];
                        detail.lastName = rdr[2] == System.DBNull.Value ? null : (string)rdr[2];
                        detail.dob = rdr[3] == System.DBNull.Value ? null : rdr[3].ToString();
                        detail.paddress = rdr[4] == System.DBNull.Value ? null : (string)rdr[4];
                        detail.gender = gender.ToString();
                        detail.fingerID = fingerID.ToString();
                        detail.uid = uid;

                        DateTime dt = DateTime.ParseExact(detail.dob, "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        detail.dob = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                        FingerPrint.register(detail, location);
                        message = "successful";
                    }
                }
                catch(Exception ep)
                {
                    errMessage = ep.ToString().Replace('\n','~');
                }
                conn.Close();
                string url = "Verify.aspx?result={0}&err={1}";
                Response.Redirect(String.Format(url, message, errMessage));
            }
        }

        protected void getFormDetails(object sender, EventArgs args)
        {
            if (IsPostBack)
            {
                FingerPrint.Details detail = new FingerPrint.Details();
                if (ViewState["firData"] != null)
                {
                    firData = ViewState["firData"].ToString();
                }
                detail.uid = FingerPrint.createUIDFile(firData);
                detail.gender = ViewState["gender"].ToString();
                detail.fingerID = ViewState["fingerID"].ToString();
                location = ViewState["location"].ToString();

                detail.firstName = String.IsNullOrEmpty(f_firstName.Text) ? null : f_firstName.Text;
                detail.lastName = String.IsNullOrEmpty(f_lastName.Text) ? null : f_lastName.Text;
                detail.paddress = String.IsNullOrEmpty(f_paddress.Text) ? null : f_paddress.Text;
                detail.dob = String.IsNullOrEmpty(f_dob.Text) ? null : f_dob.Text;

                try
                {
                    FingerPrint.register(detail, location);
                    message = "successful";
                }
                catch(Exception e)
                {
                    errMessage = e.ToString().Replace('\n','~');
                }
                string url = "Verify.aspx?result={0}&err={1}";
                Response.Redirect(String.Format(url, message, errMessage));
            }
            else
            {
                Response.Redirect("Verify.aspx");
            }
        }

    }
}