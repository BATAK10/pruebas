using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class Venta : System.Web.UI.Page
    {
        //Instancias
        private DataTable dtDatos = new DataTable();
        private DataTable dtEstado = new DataTable();
        private DataTable dtCliente = new DataTable();
        private DataTable dtCategoriaProducto = new DataTable();
        private DataTable dtProducto = new DataTable();
        public DataTable dtDatosDetalle = new DataTable();
        Funciones CargarDatos = new Funciones();
        //variables
        public string _MensajeDeError = "";
        public string _MensajeSatisfactorio = "";
        public string _Operacion = "";
        private string _CodigoVenta = "";
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
                if (Request.QueryString["idv"] != null)
                {
                    _CodigoVenta = Request.QueryString["idv"];
                }
                _Operacion = Request.QueryString["o"];
                if (!IsPostBack)
                {
                    // Traer los clientes
                    dtCliente = (DataTable)CargarDatos.Consultar(dtCliente, "id_cliente, nombre_cliente", "cliente", "usuario,=," + _usuario, "", "");
                    if (dtCliente.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcion = new ListItem("Seleccione cliente", "0");
                        cmbIdClienteVenta.DataSource = dtCliente;
                        cmbIdClienteVenta.DataValueField = "id_cliente";
                        cmbIdClienteVenta.DataTextField = "nombre_cliente";
                        cmbIdClienteVenta.DataBind();
                        cmbIdClienteVenta.Items.Insert(0, _PrimeraOpcion);
                    }
                    // Traer las categorias
                    dtCategoriaProducto = (DataTable)CargarDatos.Consultar(dtCategoriaProducto, "id_categoria_producto, nombre_categoria_producto", "categoria_producto", "usuario,=," + _usuario, "", "");
                    if (dtCategoriaProducto.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcion = new ListItem("Seleccione categoria", "0");
                        cmbIdCategoriaProductoVenta.DataSource = dtCategoriaProducto;
                        cmbIdCategoriaProductoVenta.DataValueField = "id_categoria_producto";
                        cmbIdCategoriaProductoVenta.DataTextField = "nombre_categoria_producto";
                        cmbIdCategoriaProductoVenta.DataBind();
                        cmbIdCategoriaProductoVenta.Items.Insert(0, _PrimeraOpcion);
                    }
                    txtFechaVenta2.Value = DateTime.Now.ToShortDateString();
                    if (_Operacion != "1")
                    {
                        //se carga la info
                        if (_CodigoVenta == "")
                        {
                            _MensajeDeError = "Parametro no encontrado";
                        }
                        else
                        {
                            // ven inner join categoria_producto cat on ven.id_categoria_producto_venta = cat.id_categoria_producto and ven.usuario = cat.usuario inner join producto pro on ven.id_producto_venta = pro.id_producto and ven.usuario = pro.usuario
                            dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_venta, id_cliente_venta, DATE_FORMAT(fecha_venta,'%d/%m/%Y') AS fecha_venta,DATE_FORMAT(fecha_entrega_venta,'%Y-%m-%d') as fecha_entrega_venta, costo_total_venta, estado_venta", "venta", "id_venta,=," + _CodigoVenta + ";usuario,=," + _usuario, "", "");
                            if (dtDatos.Rows.Count > 0)
                            {
                                txtIdVenta.Value = dtDatos.Rows[0]["id_venta"].ToString();
                                cmbIdClienteVenta.SelectedValue = dtDatos.Rows[0]["id_cliente_venta"].ToString();
                                txtFechaVenta2.Value = dtDatos.Rows[0]["fecha_venta"].ToString();
                                txtFechaEntregaVenta.Value = dtDatos.Rows[0]["fecha_entrega_venta"].ToString();
                                txtCostoTotalVenta.Value = dtDatos.Rows[0]["costo_total_venta"].ToString();
                                cmbEstadoVenta.Value = dtDatos.Rows[0]["estado_venta"].ToString();

                                // Cargar detalle de venta
                                dtDatosDetalle = (DataTable)CargarDatos.Consultar(dtDatosDetalle, "id_venta, id_venta_detalle,id_categoria_producto_venta,nombre_categoria_producto, id_producto_venta, nombre_producto,cantidad_venta, costo_total_venta", "venta_detalle ven inner join categoria_producto cat on ven.id_categoria_producto_venta = cat.id_categoria_producto inner join producto pro on ven.id_producto_venta = pro.id_producto ", "id_venta,=," + _CodigoVenta, "", "");
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
            txtIdVenta.Value = "";
            cmbIdCategoriaProductoVenta.SelectedIndex = 0;
            cmbIdClienteVenta.SelectedIndex = 0;
            cmbIdProductoVenta.SelectedIndex = 0;
            txtFechaVenta2.Value = "";
            txtFechaEntregaVenta.Value = "";
            txtCantidadVenta.Value = "";
            txtCostoTotalVenta.Value = "";
            cmbEstadoVenta.SelectedIndex = 0;
        }
        private void Inhabilitar()
        {
            txtIdVenta.Disabled = true;
            cmbIdClienteVenta.Enabled = false;
            cmbIdCategoriaProductoVenta.Disabled = true;
            cmbIdProductoVenta.Disabled = true;
            txtFechaVenta2.Disabled = true;
            txtFechaEntregaVenta.Disabled = true;
            txtCantidadVenta.Disabled = true;
            txtCostoTotalVenta.Disabled = true;
            cmbEstadoVenta.Disabled = true;
        }
        [WebMethod]
        public static string OperarVenta(string operacion, string usuario, string id_venta, string id_cliente_venta, string fecha_venta, string fecha_entrega_venta, string costo_total_venta, string estado_venta, string venta_detalle)
        {
            string _Mensaje = "";
            try
            {
                BLL.Ventas _Venta = new BLL.Ventas();
                string _Operacion = operacion;
                if (_Operacion != "")
                {
                    _Venta.CodigoVenta = "1";
                    _Venta.CodigoClienteVenta = id_cliente_venta;
                    _Venta.FechaVenta = DateTime.Parse(fecha_venta).ToString("yyyy/MM/dd");
                    _Venta.FechaEntregaVenta = DateTime.Parse(fecha_entrega_venta).ToString("yyyy/MM/dd");
                    _Venta.CostoTotalVenta = costo_total_venta;
                    _Venta.EstadoVenta = estado_venta;
                    _Venta.Usuario = usuario;
                    _Venta.dtVentaDetalle = JsonToDataTable(venta_detalle.Replace("\\", ""));
                    _Venta.TipoDeOperacion = int.Parse(_Operacion);

                    if (_Operacion != "1")
                    {
                        _Venta.CodigoVenta = id_venta;
                    }
                    if (_Venta.OperarVenta())
                    {

                        _Mensaje = "Operación exitosa";
                    }
                    else
                    {
                        _Mensaje = "Error: " + _Venta.Mensaje;
                    }
                }
            }
            catch (Exception ex)
            {
                _Mensaje = "Error: " + ex.Message;
            }
            return _Mensaje;
        }
        private static string DataTableToJson(DataTable dtDatos)
        {
            Dictionary<string, object> _Diccionario = new Dictionary<string, object>();
            object[] _ArregloDatos = new object[dtDatos.Rows.Count + 1];

            for (int i = 0; i <= dtDatos.Rows.Count - 1; i++)
            {
                _ArregloDatos[i] = dtDatos.Rows[i].ItemArray;
            }
            _Diccionario.Add("dato", _ArregloDatos);
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(_Diccionario);
        }
        private static DataTable JsonToDataTable(string json)
        {
            try
            {
                DataTable dtDatos = JsonConvert.DeserializeObject<DataTable>(json);
                return dtDatos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        public static object ObtenerProducto(string id_categoria_producto_venta, string usuario)
        {
            try
            {
                Funciones CargarDatos = new Funciones();
                DataTable dtProducto = new DataTable();
                dtProducto = (DataTable)CargarDatos.Consultar(dtProducto, "id_producto, nombre_producto", "producto", "id_categoria_producto,=," + id_categoria_producto_venta + ";usuario,=," + usuario, "", "");
                return DataTableToJson(dtProducto);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [WebMethod]
        public static string ObtenerPrecioYStock(string id_producto_venta, string usuario)
        {
            try
            {
                Funciones CargarDatos = new Funciones();
                DataTable dtDato = new DataTable();
                dtDato = (DataTable)CargarDatos.Consultar(dtDato, "costo_producto, cantidad_producto", "producto", "id_producto,=," + id_producto_venta + ";usuario,=," + usuario, "", "");
                return DataTableToJson(dtDato);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        protected void cmbIdCategoriaProductoVenta_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                // Traer los productos
                dtProducto = (DataTable)CargarDatos.Consultar(dtProducto, "id_producto, nombre_producto", "producto", "id_categoria_producto,=," + cmbIdCategoriaProductoVenta.Value + ";usuario,=," + _usuario, "", "");
                if (dtProducto.Rows.Count > 0)
                {
                    ListItem _PrimeraOpcionPaciente = new ListItem("Seleccione producto", "0");
                    cmbIdProductoVenta.DataSource = dtProducto;
                    cmbIdProductoVenta.DataValueField = "id_producto";
                    cmbIdProductoVenta.DataTextField = "nombre_producto";
                    cmbIdProductoVenta.DataBind();
                    cmbIdProductoVenta.Items.Insert(0, _PrimeraOpcionPaciente);
                }

            }
            catch (Exception ex)
            {
                _MensajeDeError = ex.Message;
            }
        }

        protected void cmbIdProductoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Traer costo por unidad del producto
                int costoUnidad = 0;
                costoUnidad = (int)CargarDatos.Consultar(costoUnidad, "costo_producto", "producto", "id_producto,=," + cmbIdProductoVenta.Value + ";usuario,=," + _usuario, "", "");
                if (costoUnidad > 0)
                {
                    txtCostoUnidad.Value = costoUnidad.ToString();
                }
                else
                    txtCostoUnidad.Value = "0";

            }
            catch (Exception ex)
            {
                _MensajeDeError = ex.Message;
            }

        }
    }
}