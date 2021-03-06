﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace prjHeladeria.BLL
{
    public class Ventas
    {
        #region Propiedades
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
        public bool ValidarVenta()
        {
            bool _Resultado = true;
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
            if (_CodigoCategoriaProducto == "0" || _CodigoCategoriaProducto == "")
            {
                _Mensaje += "Ingrese la categoría del producto de venta. "; _Resultado = false;
            }
            DateTime fechaVenta = DateTime.Now;
            if (!DateTime.TryParse(_FechaVenta, out fechaVenta))
            {
                _Mensaje += "Ingrese la fecha de venta. "; _Resultado = false;
            }
            DateTime fechaEntregaVenta = DateTime.Now;
            if (!DateTime.TryParse(_FechaEntregaVenta, out fechaEntregaVenta))
            {
                _Mensaje += "Ingrese la fecha de entrega. "; _Resultado = false;
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
            if (dtVentaDetalle.Rows.Count == 0 && _TipoDeOperacion!=3)
            {
                _Mensaje += "Ingrese detalle de la venta"; _Resultado = false;
            }
            return _Resultado;
        }

        public bool OperarVenta()
        {
            bool _Resultado = true;

            _Resultado = ValidarVenta();
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
                        resultadoQuery = _Conectar.ejecutarComando("insert into venta (id_venta,id_cliente_venta, fecha_venta, fecha_entrega_venta, estado_venta, costo_total_venta, usuario) values ("
                            + codigoVenta + ","
                            + _CodigoClienteVenta + ",'"
                            + _FechaVenta + "','"
                            + _FechaEntregaVenta + "',"
                            + _EstadoVenta + ", "
                            + _CostoTotalVenta + ", '"
                            + _Usuario
                            + "')");
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

                                // Se hace la descarga de stock del producto
                                if (resultadoQuery == 1)
                                {
                                    int cantidadStock = 0;
                                    cantidadStock = (int)_f.Consultar(cantidadStock, "cantidad_producto", "producto", "id_producto,=," + _Detalle["id_producto"], "", "");
                                    cantidadStock = cantidadStock - int.Parse(_Detalle["cantidad_producto"].ToString());
                                    resultadoQuery = _Conectar.ejecutarComando("update producto set cantidad_producto = "
                                + cantidadStock + " where id_producto = "
                                + _Detalle["id_producto"]);
                                }
                            }
                        }
                    }
                    if (_TipoDeOperacion == 2)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("update venta set id_cliente_venta = "
                            + _CodigoClienteVenta + ", fecha_entrega_venta='"
                            + _FechaEntregaVenta + "', estado_venta="
                            + _EstadoVenta + ", costo_total_venta="
                            + _CostoTotalVenta + ", tipo_venta="
                            + "NULL  where id_venta = "
                            + _CodigoVenta);
                        if (resultadoQuery == 1)
                        {
                            //Se eliminan los detalles
                            resultadoQuery = _Conectar.ejecutarComando("delete from venta_detalle where id_venta = "
                                + _CodigoVenta);
                            // Se agregan los nuevos detalles
                            Funciones _f = new Funciones();
                            if (resultadoQuery != -1)
                            {
                                int codigoVentaDetalle = 0;
                                foreach (DataRow _Detalle in dtVentaDetalle.Rows)
                                {
                                    codigoVentaDetalle = (int)_f.Consultar(codigoVentaDetalle, "ISNULL(max(id_venta_detalle),0) + 1", "venta_detalle", "", "", "");
                                    resultadoQuery = _Conectar.ejecutarComando("insert into venta_detalle (id_venta,id_venta_detalle, id_categoria_producto_venta, id_producto_venta, cantidad_venta, costo_total_venta, usuario) values ("
                                        + _CodigoVenta + ","
                                        + codigoVentaDetalle + ","
                                        + _Detalle["id_categoria_producto"] + ","
                                        + _Detalle["id_producto"] + ","
                                        + _Detalle["cantidad_producto"] + ","
                                        + _Detalle["costo_total"] + ", '"
                                        + _Usuario
                                        + "')");

                                    // Se hace la descarga de stock del producto
                                    if (resultadoQuery == 1)
                                    {
                                        int cantidadStock = 0;
                                        cantidadStock = (int)_f.Consultar(cantidadStock, "cantidad_producto", "producto", "id_producto,=," + _Detalle["id_producto"], "", "");
                                        cantidadStock = cantidadStock - int.Parse(_Detalle["cantidad_producto"].ToString());
                                        resultadoQuery = _Conectar.ejecutarComando("update producto set cantidad_producto = "
                                    + cantidadStock + " where id_producto = "
                                    + _Detalle["id_producto"]);
                                    }
                                }
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
        public bool CambiarEstadoVenta()
        {
            bool _Resultado = true;
            try
            {
                DAL.DAL _Conectar = new DAL.DAL();
                int resultadoQuery = 0;
                resultadoQuery = _Conectar.ejecutarComando("update venta set estado_venta = "
                           + _EstadoVenta + " where id_venta = "
                           + _CodigoVenta);
                if (resultadoQuery == 0)
                {
                    _Resultado = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        #endregion
    }
}