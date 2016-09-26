using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Text;

namespace NightShelter
{
    public partial class FingerPrint
    {
        protected static string baseDirectory = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("."), "db");
        public static string verify(string firData, string gender, string fingerID)
        {
            string firTestData;
            if (String.IsNullOrEmpty(gender))
            {
                gender = "";
            }
            if (String.IsNullOrEmpty(fingerID))
            {
                fingerID = "";
            }
            string temp = "%{0}%";
            gender = String.Format(temp, gender);
            fingerID = String.Format(temp, fingerID);

            string connStr = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string storeProc = "searchProc";
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
                            //message = "Verification success !!!";
                            return (string)rdr[0];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Error Message
            }
            conn.Close();
            return "Not Found";
        }

        public static string getGender(int g)
        {
            return g == 1 ? "Male" : "Female";
        }

        public static string getFingerName(int fingerID)
        {
            string s;
            switch (fingerID)
            {
                case 1:
                case 6:
                    s = "{0} Thumb";
                    break;
                case 2:
                case 7:
                    s = "{0} Index Finger";
                    break;
                case 3:
                case 8:
                    s = "{0} Middle Finger";
                    break;
                case 4:
                case 9:
                    s = "{0} Ring Finger";
                    break;
                case 5:
                case 10:
                    s = "{0} Baby Finger";
                    break;
                default:
                    s = "Unknown";
                    break;
            }
            if (fingerID <= 5)
            {
                s = String.Format(s, "Right Hand");
            }
            else
            {
                s = String.Format(s, "Left Hand");
            }
            return s;
        }

        public static string getUID(int length,
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