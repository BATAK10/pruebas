using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class ProductoListado : System.Web.UI.Page
    {
        public string _Mensaje = "";
        public DataTable dtDatos = new DataTable();
        Funciones CargarDatos = new Funciones();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_producto,nombre_producto, costo_producto, cantidad_producto, nombre_categoria_producto,CASE WHEN estado_producto= 1 THEN 'ACTIVO' WHEN estado_producto= 2 THEN 'INACTIVO' END AS estado_producto", "producto pro inner join categoria_producto cap on pro.id_categoria_producto = cap.id_categoria_producto", "", "", "");
                dgvListadoProducto.DataSource = dtDatos;
                dgvListadoProducto.DataBind();
            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
            }
        }
    }
}