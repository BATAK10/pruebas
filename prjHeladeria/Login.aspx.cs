using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["usuario"] != null)
            {
                Request.Cookies["usuario"].Expires = DateTime.Now.AddDays(-1);
            }
            HttpCookie Usuario = new HttpCookie("usuario");
            Usuario.Value = txtUsuario.Value;
            Usuario.Expires = DateTime.Now.AddDays(365);
            Response.Cookies.Add(Usuario);
        }
        [WebMethod]
        public static string Ingresar(string usuario, string contraseña)
        {
            string _Mensaje = "";
            Funciones CargarDatos = new Funciones();
            DataTable dtDatos = new DataTable();
            try
            {
                dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "usuario, contraseña", "usuario", "usuario,=," + usuario + ";contraseña,=," + contraseña, "", "");
                if (dtDatos.Rows.Count == 0)
                {
                    _Mensaje = "Error: usuario o contraseña inválido";
                }
                else
                {
                    _Mensaje = "Acceso correcto";
                }
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: " + ex.Message;
            }
            return _Mensaje;
        }
    }
}