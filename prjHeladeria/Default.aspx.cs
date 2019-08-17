using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class Default : System.Web.UI.Page
    {
        public string usuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Request.Cookies["usuario"].Value == "" || Request.Cookies["usuario"].Value == null)
                    Response.Redirect("Login.aspx");
                else
                    usuario = Request.Cookies["usuario"].Value;
            }
        }
    }
}