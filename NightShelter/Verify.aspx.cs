using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NightShelter
{
    public partial class Verify : System.Web.UI.Page
    {
        protected string message;
        protected string error;
        protected void Page_Load(object sender, EventArgs e)
        {
            message = "";
            error = "";
            if(Request.QueryString["result"] != null && Request.QueryString["err"] != null)
            {
                string res = Request.QueryString["result"];
                if (res.Equals("successful"))
                {
                    message = "1";
                } else
                {
                    message = "0";
                }
                string err = Request.QueryString["err"];
                if (!err.Equals("None"))
                {
                    error = err;
                }
            }
        }
    }
}