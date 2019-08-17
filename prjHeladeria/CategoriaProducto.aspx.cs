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
    public partial class CategoriaProducto : System.Web.UI.Page
    {
        // Tipos de operaciones 1 insertar 2 modificar 3 eliminar 4 consultar
        //Instancias
        private DataTable dtDatos = new DataTable();
        private DataTable dtEstado = new DataTable();
        Funciones CargarDatos = new Funciones();        
        //variables
        public string _MensajeDeError = "";
        public string _MensajeSatisfactorio = "";
        public string _Operacion = "";
        private string _CodigoCategoriaProducto = "";
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
                if (Request.QueryString["icp"] != null)
                {
                    _CodigoCategoriaProducto = Request.QueryString["icp"];
                }
                _Operacion = Request.QueryString["o"];
                if (!IsPostBack)
                {
                    if (_Operacion != "1")
                    {
                        //se carga la info
                        if (_CodigoCategoriaProducto == "")
                        {
                            _MensajeDeError = "Parametro no encontrado";
                        }
                        else
                        {
                            dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_categoria_producto, nombre_categoria_producto, estado_categoria_producto", "categoria_producto", "id_categoria_producto,=," + _CodigoCategoriaProducto+ ";usuario,=," + _usuario, "", "");
                            if (dtDatos.Rows.Count > 0)
                            {
                                txtIdCategoriaProducto.Value = dtDatos.Rows[0]["id_categoria_producto"].ToString();
                                txtNombreCategoriaProducto.Value = dtDatos.Rows[0]["nombre_categoria_producto"].ToString();
                                cmbEstadoCategoriaProducto.Value = dtDatos.Rows[0]["estado_categoria_producto"].ToString();
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
            txtIdCategoriaProducto.Value = "";
            txtNombreCategoriaProducto.Value = "";
            cmbEstadoCategoriaProducto.SelectedIndex = 0;
        }
        private void Inhabilitar()
        {
            txtIdCategoriaProducto.Disabled = true;
            txtNombreCategoriaProducto.Disabled = true;
            cmbEstadoCategoriaProducto.Disabled = true;
        }
        [WebMethod]
        public static string OperarCategoriaProducto(string operacion, string usuario, string id_categoria_producto, string nombre_categoria_producto, string estado_categoria_producto)
        {
            string _Mensaje = "";
            try
            {
                BLL.CategoriaProducto _CategoriaProducto = new BLL.CategoriaProducto();
                string _Operacion = operacion;
                if (_Operacion != "")
                {
                    _CategoriaProducto.CodigoCategoriaProducto = "1";
                    _CategoriaProducto.NombreCategoriaProducto = nombre_categoria_producto;
                    _CategoriaProducto.EstadoCategoriaProducto = estado_categoria_producto;
                    _CategoriaProducto.Usuario = usuario;
                    _CategoriaProducto.TipoDeOperacion = int.Parse(_Operacion);

                    if (_Operacion != "1")
                    {
                        _CategoriaProducto.CodigoCategoriaProducto = id_categoria_producto;
                    }
                    if (_CategoriaProducto.OperarCategoriaProducto())
                    {

                        _Mensaje = "Operación exitosa";
                    }
                    else
                    {
                        _Mensaje = "Error: " + _CategoriaProducto.Mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: "+ex.Message;
            }
            return _Mensaje;
        }
    }
}