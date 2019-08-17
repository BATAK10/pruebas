using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class Master : System.Web.UI.MasterPage
    {
        public string _NombreUsuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["usuario"] != null)
                {
                    if (Request.Cookies["usuario"].Value != "")
                        _NombreUsuario = Request.Cookies["usuario"].Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}