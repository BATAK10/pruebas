using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class ProductoCatalogo : System.Web.UI.Page
    {
        public string _Mensaje = "";
        public DataTable dtDatos = new DataTable();
        Funciones CargarDatos = new Funciones();
        public string _usuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["usuario"] != null)
                {
                    _usuario = Request.Cookies["usuario"].Value;
                }

                if (_usuario == null || _usuario == "")
                {
                    dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_producto,nombre_producto, costo_producto, cantidad_producto, nombre_categoria_producto,CASE WHEN estado_producto= 1 THEN 'ACTIVO' WHEN estado_producto= 2 THEN 'INACTIVO' END AS estado_producto, id_foto, descripcion_producto", "producto pro inner join categoria_producto cap on pro.id_categoria_producto = cap.id_categoria_producto and cap.usuario = pro.usuario", "pro.usuario,=,perla", "", "");
                }
                else
                {
                    dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_producto,nombre_producto, costo_producto, cantidad_producto, nombre_categoria_producto,CASE WHEN estado_producto= 1 THEN 'ACTIVO' WHEN estado_producto= 2 THEN 'INACTIVO' END AS estado_producto, id_foto, descripcion_producto", "producto pro inner join categoria_producto cap on pro.id_categoria_producto = cap.id_categoria_producto and cap.usuario = pro.usuario", "pro.usuario,=," + _usuario, "", "");
                }

                if (Request.Cookies["HeladosEnCarrito"] == null)
                {
                    HttpCookie EventosAsignar = new HttpCookie("HeladosEnCarrito");
                    EventosAsignar.Value = "";
                    EventosAsignar.Expires = DateTime.Now.AddHours(3);
                    Response.Cookies.Add(EventosAsignar);
                }
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: " + ex.Message;
            }
        }
    }
}