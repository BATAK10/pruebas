using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class frmClientesListado : System.Web.UI.Page
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
                dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_cliente,nombre_cliente,apellido_cliente,telefono_cliente,direccion_cliente, CASE WHEN estado_cliente = 1 THEN 'ACTIVO' WHEN estado_cliente = 2 THEN 'INACTIVO' END AS estado_cliente", "cliente", "usuario,=," + _usuario, "", "id_cliente");
                dgvListadoClientes.DataSource = dtDatos;
                dgvListadoClientes.DataBind();
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: "+ex.Message;
            }
        }
    }
}