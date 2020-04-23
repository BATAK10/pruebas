using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public partial class CarritoDeCompras : System.Web.UI.Page
    {
        public string _Mensaje = "";
        public DataTable dtDatos = new DataTable();
        Funciones CargarDatos = new Funciones();
        public string _CostoTotal = "0";
        public string _Operacion = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["HeladosEnCarrito"] != null)
                {
                    string idProductos = Request.Cookies["HeladosEnCarrito"].Value;
                    string[] arregloProductos = idProductos.Split(',');
                    string idProductosIn = "";
                    for (int i = 0; i < arregloProductos.Length; i++)
                    {
                        if (idProductosIn == "")
                        {
                            idProductosIn = arregloProductos[i].Split(':')[0];
                        }
                        else
                        {
                            idProductosIn += "&" + arregloProductos[i].Split(':')[0];
                        }
                    }
                    if (idProductosIn != "")
                    {
                        dtDatos = (DataTable)CargarDatos.Consultar(dtDatos, "id_producto,nombre_producto, costo_producto, cantidad_producto,pro.id_categoria_producto, nombre_categoria_producto, id_foto, descripcion_producto", "producto pro inner join categoria_producto cap on pro.id_categoria_producto = cap.id_categoria_producto", "pro.id_producto,in," + idProductosIn, "", "");
                        if (dtDatos.Rows.Count > 0)
                        {
                            _CostoTotal = dtDatos.Compute("SUM(costo_producto)", "").ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [WebMethod]
        public static string OperarPedido(string operacion, string id_cliente_venta, string costo_total_venta, string estado_venta, string venta_detalle)
        {
            string _Mensaje = "";
            try
            {
                BLL.Pedidos _Pedido = new BLL.Pedidos();
                string _Operacion = operacion;
                if (_Operacion != "")
                {
                    _Pedido.CodigoVenta = "1";
                    _Pedido.ClientePedidoPor = id_cliente_venta;
                    _Pedido.FechaVenta = DateTime.Now.ToString("yyyy/MM/dd");
                    _Pedido.CostoTotalVenta = costo_total_venta;
                    _Pedido.EstadoVenta = estado_venta;
                    _Pedido.Usuario = "perla";
                    _Pedido.dtVentaDetalle = Funciones.JsonToDataTable(venta_detalle.Replace("\\", ""));
                    _Pedido.TipoDeOperacion = int.Parse(_Operacion);

                    if (_Pedido.OperarPedido())
                    {

                        _Mensaje = "Operación exitosa";
                    }
                    else
                    {
                        _Mensaje = "Error: " + _Pedido.Mensaje;
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