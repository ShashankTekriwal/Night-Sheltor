using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;

namespace NightShelter
{
    public partial class VerifyResult : System.Web.UI.Page
    {
        protected string message;
        protected string errMessage;
        //protected string baseDirectory = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("."), "db");

        protected void Page_Load(object sender, EventArgs e)
        {
            //test_connection(); return;

            if(String.IsNullOrEmpty(Request.Form["firData"]))
            {
                Response.Redirect("Verify.aspx");
            }

            errMessage = "None";
            message = "";


            string firstName = "";
            string lastName = "";
            string dateOfBirth = "";
            string permanentAddress = "";
            int gender = -1;
            int fingerID = -1;

            firstName = String.IsNullOrEmpty(Request.Form["fname"]) ? null : Request.Form["fname"].Trim();
            lastName = String.IsNullOrEmpty(Request.Form["lname"]) ? null : Request.Form["lname"].Trim();
            permanentAddress = String.IsNullOrEmpty(Request.Form["paddress"]) ? null : Request.Form["paddress"].Trim();
            dateOfBirth = String.IsNullOrEmpty(Request.Form["dob"]) ? null : Request.Form["dob"].Trim();


            if (Int32.TryParse(Request.Form["gender"], out gender)) { }
            else
            {
                //Generate error
            }

            if (Int32.TryParse(Request.Form["fingerID"], out fingerID)) { }
            else
            {
                //Generate error
            }
            string firData = Request.Form["firData"];
            string location = Request.Form["location"];

            

            DateTime? dateOfBirthF;
            if (String.IsNullOrEmpty(dateOfBirth))
            {
                dateOfBirthF = null;
            } else
            {
                dateOfBirthF = DateTime.ParseExact(dateOfBirth, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            string connStr = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);

            string uid = FingerPrint.verify(firData, gender.ToString(), fingerID.ToString());


            if (uid.Equals("Not Found"))
            {
                uid = FingerPrint.getUID(32);
                string fileToCreate = System.IO.Path.Combine(FingerPrint.baseDirectory, uid + ".fir");
                if (!System.IO.File.Exists(fileToCreate))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(fileToCreate))
                    {
                        int firDataSize = firData.Length;
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                        sw.WriteLine(uid);
                        sw.WriteLine(firDataSize);
                        sw.WriteLine(firData);
                        sw.Close();
                    }
                }
            }

            try
            {
                string storeProc = "registerProc";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(storeProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", firstName);
                cmd.Parameters.AddWithValue("@lname", lastName);
                cmd.Parameters.AddWithValue("@dob", dateOfBirthF);
                cmd.Parameters.AddWithValue("@paddress", permanentAddress);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@fingerID", fingerID);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@firDataPath", uid);
                cmd.ExecuteNonQuery();
                conn.Close();
                message = "successful";
            }
            catch (Exception ex)
            {
                message = "unsuccessful";
                errMessage = ex.ToString().Replace('\n','~');
            }
            string url = String.Format("Verify.aspx?result={0}&err={1}", message, errMessage);
            Response.Redirect(url);
        }

        void test_connection()
        {
            //string connStr = "server=localhost;user=root;database=test;port=3306;";
            string connStr = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string ran = FingerPrint.getUID(32);
                string storeProc = "verifyProc";
                MySqlCommand cmd = new MySqlCommand(storeProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@gender", 1);
                cmd.Parameters.AddWithValue("@fingerID", 1);
                MySqlDataReader rdr = cmd.ExecuteReader();
                message = "";
                while (rdr.Read())
                {
                    message += rdr[0] + "<br>";
                }
                // Perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
