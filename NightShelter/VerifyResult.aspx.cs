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
        protected string baseDirectory = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("."), "db");

        protected void Page_Load(object sender, EventArgs e)
        {
            //test_connection(); return;

            string firstName = "";
            string lastName = "";
            string dateOfBirth = "";
            string permanentAddress = "";
            int gender = -1;
            int fingerID = -1;

            firstName = Request.Form["fname"].Trim();
            lastName = Request.Form["lname"].Trim();
            dateOfBirth = Request.Form["dob"].Trim();
            permanentAddress = Request.Form["pAddress"].Trim();

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

            firstName = String.IsNullOrEmpty(firstName) ? null : firstName;
            lastName = String.IsNullOrEmpty(firstName) ? null : lastName;
            permanentAddress = String.IsNullOrEmpty(firstName) ? null : permanentAddress;

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

            string uid = firDataExists(firData, gender, fingerID);


            if (uid.Equals("Not Found"))
            {
                uid = RandomString(32);
                string fileToCreate = System.IO.Path.Combine(baseDirectory, uid + ".fir");
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
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
            }


        }

        private string firDataExists(string firData, int gender, int fingerID)
        {
            string firTestData;


            string connStr = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            string storeProc = "verifyProc";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(storeProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@fingerID", fingerID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    string firFileName = rdr[0] + ".fir";
                    string filePath = System.IO.Path.Combine(baseDirectory, firFileName);
                    if (!System.IO.File.Exists(filePath))
                        continue;
                    using (System.IO.FileStream fs = System.IO.File.OpenRead(filePath))
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                        string fUserID = sr.ReadLine();
                        string fFirDataSize = sr.ReadLine();
                        firTestData = sr.ReadLine();
                        sr.Close();
                    }
                    Type BioBSPCOMM = Type.GetTypeFromProgID("BioBSPCOMM.BioBSP");
                    object BioBSP = Activator.CreateInstance(BioBSPCOMM);
                    object[] parameter = new object[2];
                    parameter[0] = firData;
                    parameter[1] = firTestData;
                    BioBSPCOMM.InvokeMember("VerifyMatch", System.Reflection.BindingFlags.InvokeMethod, null, BioBSP, parameter);
                    string errorCode = BioBSPCOMM.GetProperty("ErrorCode").GetValue(BioBSP, null).ToString();
                    if (errorCode.Equals("0"))
                    {
                        string matchingResult = BioBSPCOMM.GetProperty("MatchingResult").GetValue(BioBSP, null).ToString();
                        if (matchingResult.Equals("0"))
                        {
                            //message = "Matching failed ! Verification failed !";
                        }
                        else
                        {
                            message = "Verification success !!!";
                            return (string)rdr[0];
                        }
                    }
                }
                message = "Verification Failed!";
                conn.Close();
            }
            catch (Exception e)
            {
                errMessage = e.ToString();
            }
            return "Not Found";
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
                string ran = RandomString(32);
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

        string RandomString(int length,
                            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars))
                throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }
    }
}
