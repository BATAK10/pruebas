using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class CategoriaProductoListado : System.Web.UI.Page
    {
        public string _Mensaje = "";
        public DataTable dtDatos = new DataTable();
        Funciones CargarDatos = new Funciones();
        public string _usuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        _usuario = Request.Cookies["usuario"].Value;
                }
                dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_categoria_producto,nombre_categoria_producto,CASE WHEN estado_categoria_producto= 1 THEN 'ACTIVO' WHEN estado_categoria_producto= 2 THEN 'INACTIVO' END AS estado_categoria_producto", "categoria_producto", "usuario,=," + _usuario, "", "");
                dgvListadoCategoriaProducto.DataSource = dtDatos;
                dgvListadoCategoriaProducto.DataBind();
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: "+ex.Message;
            }
        }
    }
}