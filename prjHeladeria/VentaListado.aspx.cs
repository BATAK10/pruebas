using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class VentaListado : System.Web.UI.Page
    {
        public string _Mensaje = "";
        public DataTable dtDatos = new DataTable();
        public string _usuario = "";
        Funciones CargarDatos = new Funciones();
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

                dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_venta, nombre_cliente, fecha_venta, fecha_entrega_venta, costo_total_venta,CASE WHEN estado_venta= 1 THEN 'Pendiente' WHEN estado_venta= 2 THEN 'Entregado' END AS estado_venta", "venta ven inner join cliente cli on ven.id_cliente_venta = cli.id_cliente and ven.usuario = cli.usuario", "ven.usuario,=," + _usuario, "", "");
                dgvListadoVenta.DataSource = dtDatos;
                dgvListadoVenta.DataBind();
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: " + ex.Message;
            }
        }
    }
}