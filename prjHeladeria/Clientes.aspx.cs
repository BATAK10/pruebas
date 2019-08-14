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
    public partial class Clientes : System.Web.UI.Page
    {
        // Tipos de operaciones 1 insertar 2 modificar 3 eliminar 4 consultar
        //Instancias
        private DataTable dtDatos = new DataTable();
        private DataTable dtEstado = new DataTable();
        Funciones CargarDatos = new Funciones();
        BLL.Clientes _Cliente = new BLL.Clientes();
        //variables
        public string _MensajeDeError = "";
        public string _MensajeSatisfactorio = "";
        public string _Operacion = "";
        private string _CodigoCliente = "";
        public Byte[] _ByteImagen = null;
        public bool _MostrarImagen = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string Usuario = Request.Cookies["CodigoUsuario"].Value.ToString();
                //string Permisos = Request.Cookies["Permisos"].Value;
                //string Permiso = "1";
                //if (Permisos.Contains(Permiso + ".") != true)
                //{
                //    Response.Redirect("ErrorPermisos.aspx");
                //}

                if (Request.QueryString["idc"] != null)
                {
                    _CodigoCliente = Request.QueryString["idc"];
                }
                _Operacion = Request.QueryString["o"];
                if (!IsPostBack)
                {
                    if (_Operacion != "1")
                    {
                        //se carga la info
                        if (_CodigoCliente == "")
                        {
                            _MensajeDeError = "Parametro no encontrado";
                        }
                        else
                        {
                            dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_cliente, nombre_cliente, apellido_cliente, telefono_cliente, direccion_cliente, estado_cliente", "cliente", "id_cliente,=," + _CodigoCliente, "", "");
                            if (dtDatos.Rows.Count > 0)
                            {
                                txtIdCliente.Value = dtDatos.Rows[0]["id_cliente"].ToString();
                                //ObtenerImagen
                                txtNombreCliente.Value = dtDatos.Rows[0]["nombre_cliente"].ToString();
                                txtApellidoCliente.Value = dtDatos.Rows[0]["apellido_cliente"].ToString();
                                txtTelefonoCliente.Value = dtDatos.Rows[0]["telefono_cliente"].ToString();
                                txtDireccionCliente.Value = dtDatos.Rows[0]["direccion_cliente"].ToString();
                                cmbEstadoCliente.Value = dtDatos.Rows[0]["estado_cliente"].ToString();
                            }
                            else
                            {
                                _MensajeDeError = "Datos no encontrados";
                            }
                        }
                    }
                    // se bloque todo porque es consulta
                    if (_Operacion == "4")
                    {
                        Inhabilitar();
                    }
                    // para eliminar se bloquean los campos menos botones
                    if (_Operacion == "3")
                    {
                        Inhabilitar();
                    }
                }
            }
            catch (Exception ex)
            {
                _MensajeDeError = ex.Message;
            }
        }

        private void limpiar()
        {
            txtIdCliente.Value = "";
            txtNombreCliente.Value = "";
            txtApellidoCliente.Value = "";
            txtTelefonoCliente.Value = "";
            txtDireccionCliente.Value = "";
            cmbEstadoCliente.SelectedIndex = 0;
        }
        private void Inhabilitar()
        {
            txtIdCliente.Disabled = true;
            txtNombreCliente.Disabled = true;
            txtApellidoCliente.Disabled = true;
            txtTelefonoCliente.Disabled = true;
            txtDireccionCliente.Disabled = true;
            cmbEstadoCliente.Disabled = true;
        }
        [WebMethod]
        public static string OperarCliente(string operacion, string id_cliente, string nombre_cliente, string apellido_cliente, string telefono_cliente, string direccion_cliente, string estado_cliente)
        {
            string _Mensaje = "";
            try
            {
                BLL.Clientes _Cliente = new BLL.Clientes();
                string _Operacion = operacion;
                if (_Operacion != "")
                {
                    _Cliente.CodigoCliente = "1";
                    _Cliente.NombreCliente = nombre_cliente;
                    _Cliente.ApellidoCliente = apellido_cliente;
                    _Cliente.TelefonoCliente = telefono_cliente;
                    _Cliente.DireccionCliente = direccion_cliente;
                    _Cliente.EstadoCliente = estado_cliente;
                    _Cliente.TipoDeOperacion = int.Parse(_Operacion);

                    if (_Operacion != "1")
                    {
                        _Cliente.CodigoCliente = id_cliente;
                    }                    
                    if (_Cliente.OperarCliente())
                    {

                        _Mensaje = "Operación exitosa";
                    }
                    else
                    {
                        _Mensaje = "Error: " + _Cliente.Mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
            }
            return _Mensaje;
        }
    }
}