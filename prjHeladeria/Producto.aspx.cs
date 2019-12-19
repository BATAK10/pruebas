using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class Producto : System.Web.UI.Page
    {
        //Instancias
        private DataTable dtDatos = new DataTable();
        private DataTable dtEstado = new DataTable();
        private DataTable dtCategoriaProducto = new DataTable();
        Funciones CargarDatos = new Funciones();
        //variables
        public string _MensajeDeError = "";
        public string _MensajeSatisfactorio = "";
        public string _Operacion = "";
        private string _CodigoProducto = "";
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

                if (Request.QueryString["idp"] != null)
                {
                    _CodigoProducto = Request.QueryString["idp"];
                }
                _Operacion = Request.QueryString["o"];
                if (!IsPostBack)
                {
                    // Traer las categorias
                    dtCategoriaProducto = (DataTable)CargarDatos.Consultar(dtCategoriaProducto, "id_categoria_producto, nombre_categoria_producto", "categoria_producto", "usuario,=," + _usuario, "", "");
                    if (dtCategoriaProducto.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcionPaciente = new ListItem("Seleccione categoria", "0");
                        cmbIdCategoriaProducto.DataSource = dtCategoriaProducto;
                        cmbIdCategoriaProducto.DataValueField = "id_categoria_producto";
                        cmbIdCategoriaProducto.DataTextField = "nombre_categoria_producto";
                        cmbIdCategoriaProducto.DataBind();
                        cmbIdCategoriaProducto.Items.Insert(0, _PrimeraOpcionPaciente);
                    }

                    if (_Operacion != "1")
                    {
                        //se carga la info
                        if (_CodigoProducto == "")
                        {
                            _MensajeDeError = "Parametro no encontrado";
                        }
                        else
                        {
                            dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_producto, nombre_producto, costo_producto, cantidad_producto, id_categoria_producto, estado_producto,id_foto, descripcion_producto", "producto", "id_producto,=," + _CodigoProducto + ";usuario,=," + _usuario, "", "");
                            if (dtDatos.Rows.Count > 0)
                            {
                                txtIdProducto.Value = dtDatos.Rows[0]["id_producto"].ToString();
                                txtNombreProducto.Value = dtDatos.Rows[0]["nombre_producto"].ToString();
                                txtCostoProducto.Value = dtDatos.Rows[0]["costo_producto"].ToString();
                                txtCantidadProducto.Value = dtDatos.Rows[0]["cantidad_producto"].ToString();
                                txtDescripcionProducto.Value = dtDatos.Rows[0]["descripcion_producto"].ToString();
                                cmbIdCategoriaProducto.Value = dtDatos.Rows[0]["id_categoria_producto"].ToString();
                                cmbEstadoProducto.Value = dtDatos.Rows[0]["estado_producto"].ToString();
                                imgPrevistaFotografia.ImageUrl = dtDatos.Rows[0]["id_foto"].ToString(); // @"data:image/png;base64," + 

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
            txtIdProducto.Value = "";
            txtNombreProducto.Value = "";
            txtCostoProducto.Value = "";
            txtCantidadProducto.Value = "";
            cmbIdCategoriaProducto.SelectedIndex = 0;
            cmbEstadoProducto.SelectedIndex = 0;
        }
        private void Inhabilitar()
        {
            txtIdProducto.Disabled = true;
            txtNombreProducto.Disabled = true;
            txtCostoProducto.Disabled = true;
            txtCantidadProducto.Disabled = true;
            cmbIdCategoriaProducto.Disabled = true;
            cmbEstadoProducto.Disabled = true;
        }
        [WebMethod]
        public static string OperarProducto(string operacion, string usuario, string id_producto, string nombre_producto, string costo_producto, string cantidad_producto, string id_categoria_producto, string estado_producto, string imagen_producto, string descripcion_producto)
        {
            string _Mensaje = "";
            try
            {
                BLL.Producto _Producto = new BLL.Producto();
                string _Operacion = operacion;
                if (_Operacion != "")
                {
                    _Producto.CodigoProducto = "1";
                    _Producto.NombreProducto = nombre_producto;
                    _Producto.CostoProducto = costo_producto;
                    _Producto.CantidadStock = cantidad_producto;
                    _Producto.CodigoCategoriaProducto = id_categoria_producto;
                    _Producto.EstadoProducto = estado_producto;
                    _Producto.DescripcionProducto = descripcion_producto;
                    _Producto.Usuario = usuario;
                    _Producto.TipoDeOperacion = int.Parse(_Operacion);

                    if (_Operacion != "1")
                    {
                        _Producto.CodigoProducto = id_producto;
                    }
                    if (imagen_producto != null)
                    {
                        //var bytes = Convert.FromBase64String(imagen_producto);
                        //using (var imageFile = new FileStream(HttpContext.Current.Server.MapPath("/images"), FileMode.Open, FileAccess.Read))
                        //{
                        //    imageFile.Write(bytes, 0, bytes.Length);
                        //    imageFile.Flush();
                        //}
                        _Producto.Imagen = imagen_producto;
                    }
                    if (_Producto.OperarProducto())
                    {
                        _Mensaje = "Operación exitosa";
                    }
                    else
                    {
                        _Mensaje = "Error: " + _Producto.Mensaje;
                    }
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