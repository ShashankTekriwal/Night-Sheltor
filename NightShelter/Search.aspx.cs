using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NightShelter
{
    public partial class Search : System.Web.UI.Page
    {
        protected string result;
        protected string detailJson;
        protected void Page_Load(object sender, EventArgs e)
        {
            //result = "";
        }
        protected void search(object sender, EventArgs e)
        {
            string gender = "";
            string fingerID = "";

            gender = f_gender.Text;
            fingerID = f_fingerID.Text;
            string firData = f_firData.Value;

            string uid = FingerPrint.verify(firData, gender, fingerID);
            if (uid.Equals("Not Found"))
            {
                result = "Finger Print Not Found!";
                resultDiv.InnerHtml = result;
            } else
            {
                result = "Finger Print Found!";
                showHistory(uid);
                resultDiv.InnerHtml = detailJson;
            }
        }

        private void showHistory(string uid)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            Dictionary<string, string> details = new Dictionary<string, string>();

            FingerPrint.Details detail = new FingerPrint.Details();

            try
            {
                conn.Open();
                string storeProc = "getDetailsProc";
                MySqlCommand cmd = new MySqlCommand(storeProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firData", uid);
                cmd.Parameters.AddWithValue("lim", Int32.MaxValue);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //rdr[1] fName; rdr[2] lName; rdr[3] dob; rdr[4] paddress;
                //rdr[5] gender; rdr[6]fingerID; rdr[7] dateTime; rdr[8] location;
                //rdr[0] sno; rdr[9] firDataPath;
                bool temp = true;
                while (rdr.Read())
                {
                    if (temp)
                    {
                        if (rdr[1] == System.DBNull.Value)
                        {
                            detail.firstName = "";
                        }else
                        {
                            detail.firstName = (string)rdr[1];
                        }
                        detail.lastName = rdr[2] == System.DBNull.Value ? "" : (string)rdr[2];
                        detail.dob = rdr[3] == System.DBNull.Value ? "" : rdr[3].ToString();
                        detail.paddress = rdr[4] == System.DBNull.Value ? "" : (string)rdr[4];
                        detail.gender = FingerPrint.getGender((int)rdr[5]);
                        detail.fingerID = FingerPrint.getFingerName((int)rdr[6]);
                        temp = false;
                    }
                    string t1 = rdr[7].ToString();
                    string t2 = (string)rdr[8];
                    detail.places.Add(t1, t2);
                }
                detailJson = JsonConvert.SerializeObject(detail);
                string funcName = String.Format("showDetails('{0}');", detailJson);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Javascript", funcName, true);
            } catch(Exception e)
            {
                resultDiv.InnerHtml = e.ToString();
            }
        }
    }
}