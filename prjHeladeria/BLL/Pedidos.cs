using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace prjHeladeria.BLL
{
    public class Pedidos
    {
        #region Propiedades        
        private string _ClientePedidoPor;
        public string ClientePedidoPor
        {
            get
            {
                return _ClientePedidoPor;
            }
            set
            {
                _ClientePedidoPor = value;
            }
        }
        private string _Mensaje;
        public string Mensaje
        {
            get
            {
                return _Mensaje;
            }
            set
            {
                _Mensaje = value;
            }
        }
        private int _TipoDeOperacion;
        public int TipoDeOperacion
        {
            get
            {
                return _TipoDeOperacion;
            }
            set
            {
                _TipoDeOperacion = value;
            }
        }
        private string _CodigoVenta;
        public string CodigoVenta
        {
            get
            {
                return _CodigoVenta;
            }
            set
            {
                _CodigoVenta = value;
            }
        }
        private string _CodigoProducto;
        public string CodigoProducto
        {
            get
            {
                return _CodigoProducto;
            }
            set
            {
                _CodigoProducto = value;
            }
        }
        private string _CodigoCategoriaProducto;
        public string CodigoCategoriaProducto
        {
            get
            {
                return _CodigoCategoriaProducto;
            }
            set
            {
                _CodigoCategoriaProducto = value;
            }
        }
        private string _FechaVenta;
        public string FechaVenta
        {
            get
            {
                return _FechaVenta;
            }
            set
            {
                _FechaVenta = value;
            }
        }
        private string _FechaEntregaVenta;
        public string FechaEntregaVenta
        {
            get
            {
                return _FechaEntregaVenta;
            }
            set
            {
                _FechaEntregaVenta = value;
            }
        }
        private string _CantidadVenta;
        public string CantidadVenta
        {
            get
            {
                return _CantidadVenta;
            }
            set
            {
                _CantidadVenta = value;
            }
        }
        private string _CostoTotalVenta;
        public string CostoTotalVenta
        {
            get
            {
                return _CostoTotalVenta;
            }
            set
            {
                _CostoTotalVenta = value;
            }
        }
        private string _CodigoClienteVenta;
        public string CodigoClienteVenta
        {
            get
            {
                return _CodigoClienteVenta;
            }
            set
            {
                _CodigoClienteVenta = value;
            }
        }
        private string _EstadoVenta;
        public string EstadoVenta
        {
            get
            {
                return _EstadoVenta;
            }
            set
            {
                _EstadoVenta = value;
            }
        }
        private string _Usuario;
        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
            }
        }
        private string _CantidadStockProducto;
        public string CantidadStockProducto
        {
            get
            {
                return _CantidadStockProducto;
            }
            set
            {
                _CantidadStockProducto = value;
            }
        }
        private DataTable _dtVentaDetalle = new DataTable();
        public DataTable dtVentaDetalle
        {
            get
            {
                return _dtVentaDetalle;
            }
            set
            {
                _dtVentaDetalle = value;
            }
        }
        #endregion
        #region Metodos
        public bool ValidarPedido()
        {
            bool _Resultado = true;
            try
            {
                _Mensaje = "";
                if (_TipoDeOperacion == 0)
                {
                    _Mensaje += "Ingrese el tipo de operacion ";
                    _Resultado = false;
                }
                if (_TipoDeOperacion == 3 || _TipoDeOperacion == 2) // Editar o Eliminar
                {
                    if (_CodigoVenta == "0")
                    {
                        _Mensaje += "Ingrese el código de venta";
                        _Resultado = false;
                    }
                }
                if (_CodigoProducto == "0" || _CodigoProducto == "")
                {
                    _Mensaje += "Ingrese el producto de venta. "; _Resultado = false;
                }

                DateTime fechaVenta = DateTime.Now;
                if (!DateTime.TryParse(_FechaVenta, out fechaVenta))
                {
                    _Mensaje += "Ingrese la fecha de venta. "; _Resultado = false;
                }

                if (_CantidadVenta == "0" || _CantidadVenta == "")
                {
                    _Mensaje += "Ingrese la cantidad de venta. "; _Resultado = false;
                }
                if (_CostoTotalVenta == "0" || _CostoTotalVenta == "")
                {
                    _Mensaje += "Ingrese el costo todal de venta. "; _Resultado = false;
                }
                if (_EstadoVenta == "0" || _EstadoVenta == "")
                {
                    _Mensaje += "Ingrese el estado de venta. "; _Resultado = false;
                }
                if (_Usuario == "")
                {
                    _Mensaje += "Ingrese el usuario. "; _Resultado = false;
                }
                if (_CantidadStockProducto == "0" || _CantidadStockProducto == "")
                {
                    _Mensaje += "Ingrese la cantidad en stock del producto"; _Resultado = false;
                }
                if (dtVentaDetalle.Rows.Count == 0)
                {
                    _Mensaje += "Ingrese detalle de la venta"; _Resultado = false;
                }
                Funciones _f = new Funciones();
                _CodigoClienteVenta = "0";
                _CodigoClienteVenta = _f.Consultar(int.Parse(_CodigoClienteVenta), "id_cliente", "cliente", "id_cliente,=," + _ClientePedidoPor, "", "").ToString();

            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }

        public bool OperarPedido()
        {
            bool _Resultado = true;

            _Resultado = ValidarPedido();
            if (_Resultado)
            {
                DAL.DAL _Conectar = new DAL.DAL();
                int resultadoQuery = 0;
                try
                {
                    if (_TipoDeOperacion == 1)
                    {
                        // Obtener código de categoria_producto siguiente
                        Funciones _f = new Funciones();
                        int codigoVenta = (int)_f.Consultar(int.Parse(_CodigoVenta), "ISNULL(max(id_venta),0) + 1", "venta", "", "", "");
                        resultadoQuery = _Conectar.ejecutarComando("insert into venta (id_venta,id_cliente_venta, fecha_venta, estado_venta, costo_total_venta, usuario, pedido_por, tipo_venta) values ("
                            + codigoVenta + ","
                            + _CodigoClienteVenta + ",'"
                            + _FechaVenta + "',"
                            + _EstadoVenta + ", "
                            + _CostoTotalVenta + ", '"
                            + _Usuario + "' , '"
                            + _ClientePedidoPor + "' , "
                            + "2)");
                        if (resultadoQuery == 1)
                        {
                            // Se agrega el detalle
                            int codigoVentaDetalle = 0;
                            foreach (DataRow _Detalle in dtVentaDetalle.Rows)
                            {
                                codigoVentaDetalle = (int)_f.Consultar(codigoVentaDetalle, "ISNULL(max(id_venta_detalle),0) + 1", "venta_detalle", "", "", "");
                                resultadoQuery = _Conectar.ejecutarComando("insert into venta_detalle (id_venta,id_venta_detalle, id_categoria_producto_venta, id_producto_venta, cantidad_venta, costo_total_venta, usuario) values ("
                                    + codigoVenta + ","
                                    + codigoVentaDetalle + ","
                                    + _Detalle["id_categoria_producto"] + ","
                                    + _Detalle["id_producto"] + ","
                                    + _Detalle["cantidad_producto"] + ","
                                    + _Detalle["costo_total"] + ", '"
                                    + _Usuario
                                    + "')");
                            }
                        }
                    }

                    if (_TipoDeOperacion == 3)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("delete from venta_detalle where id_venta = "
                            + _CodigoVenta);
                        if (resultadoQuery != 1)
                        {
                            resultadoQuery = _Conectar.ejecutarComando("delete from venta where id_venta = "
                                + _CodigoVenta);
                        }
                    }
                    if (resultadoQuery == 1)
                        _Resultado = true;
                    else
                    {
                        Mensaje = resultadoQuery.ToString();
                        _Resultado = false;
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                    _Resultado = false;
                }
            }
            else
                _Resultado = false;
            return _Resultado;
        }
        #endregion
    }
}