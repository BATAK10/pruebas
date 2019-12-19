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
        public string _MensajeDeError = "";
        public string _MensajeSatisfactorio = "";
        public DataTable dtDatos = new DataTable();
        public string _usuario = "";
        Funciones CargarDatos = new Funciones();
        BLL.Ventas _Venta = new BLL.Ventas();
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

                dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_venta, nombre_cliente, fecha_venta, fecha_entrega_venta, costo_total_venta,CASE WHEN estado_venta= 1 THEN 'Pendiente' WHEN estado_venta= 2 THEN 'Entregado' END AS estado_venta", "venta ven inner join cliente cli on ven.id_cliente_venta = cli.id_cliente and ven.usuario = cli.usuario", "ven.usuario,=," + _usuario, "", "id_venta");
                dgvListadoVenta.DataSource = dtDatos;
                dgvListadoVenta.DataBind();
            }
            catch (Exception ex)
            {
                _MensajeDeError = "Error: " + ex.Message;
            }
        }

        protected void btnEntregado_Click(object sender, EventArgs e)
        {

        }

        protected void btnPendiente_Click(object sender, EventArgs e)
        {
        }

        protected void dgvListadoVenta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                try
                {
                    string id_venta = dgvListadoVenta.Rows[e.NewEditIndex].Cells[0].Text;
                    _Venta.CodigoVenta = id_venta;
                    _Venta.EstadoVenta = "2";
                    if (_Venta.CambiarEstadoVenta())
                    {
                        _MensajeSatisfactorio = "Estado modificado con éxito";
                        Response.Redirect("VentaListado.aspx");
                    }
                    else
                        _MensajeDeError = "Error al modificar el estado.";
                }
                catch (Exception ex)
                {
                    _MensajeDeError = "Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                _MensajeDeError = "Error: " + ex.Message;
            }
        }

        protected void dgvListadoVenta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                try
                {
                    string id_venta = dgvListadoVenta.Rows[e.RowIndex].Cells[0].Text;
                    _Venta.CodigoVenta = id_venta;
                    _Venta.EstadoVenta = "1";
                    if (_Venta.CambiarEstadoVenta())
                    {
                        _MensajeSatisfactorio = "Estado modificado con éxito";
                        Response.Redirect("VentaListado.aspx");
                    }
                    else
                        _MensajeDeError = "Error al modificar el estado.";
                }
                catch (Exception ex)
                {
                    _MensajeDeError = "Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                _MensajeDeError = "Error: " + ex.Message;
            }
        }
    }
}