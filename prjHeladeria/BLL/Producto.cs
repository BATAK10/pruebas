using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjHeladeria.BLL
{
    public class Producto
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
        private int _CodigoProducto;
        public int CodigoProducto
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
        private string _NombreProducto;
        public string NombreProducto
        {
            get
            {
                return _NombreProducto;
            }
            set
            {
                _NombreProducto = value;
            }
        }
        private string _CostoProducto;
        public string CostoProducto
        {
            get
            {
                return _CostoProducto;
            }
            set
            {
                _CostoProducto = value;
            }
        }
        private string _EstadoProducto;
        public string EstadoProducto
        {
            get
            {
                return _EstadoProducto;
            }
            set
            {
                _EstadoProducto = value;
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
        private string _CantidadStock;
        public string CantidadStock
        {
            get
            {
                return _CantidadStock;
            }
            set
            {
                _CantidadStock = value;
            }
        }

        #endregion
        #region Metodos
        public bool ValidarProducto()
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
                if (_CodigoProducto == 0)
                {
                    _Mensaje += "Ingrese el código de producto";
                    _Resultado = false;
                }
            }
            if (_NombreProducto == "")
            {
                _Mensaje += "Ingrese el nombre de producto. "; _Resultado = false;
            }
            if (_EstadoProducto == "0" || _EstadoProducto == "")
            {
                _Mensaje += "Ingrese el estado de producto. "; _Resultado = false;
            }
            double _Costo = 0;
            if (!double.TryParse(_CostoProducto, out _Costo))
            {
                _Mensaje += "Ingrese el costo del producto con un valor numérico. "; _Resultado = false;
            }
            else
            {
                if (_Costo == 0)
                {
                    _Mensaje += "Ingrese el costo del producto con un valor mayor a 0. "; _Resultado = false;
                }
            }
            if (_CodigoCategoriaProducto == "0" || _CodigoCategoriaProducto == "")
            {
                _Mensaje += "Ingrese el cógido de categoría de producto. "; _Resultado = false;
            }
            return _Resultado;
        }

        public bool OperarProducto()
        {
            bool _Resultado = true;

            _Resultado = ValidarProducto();
            if (_Resultado)
            {
                DAL.DAL _Conectar = new DAL.DAL();
                int resultadoQuery = 0;

                try
                {
                    if (_TipoDeOperacion == 1)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("insert into producto values ("
                            + _CodigoProducto + ","
                            + _NombreProducto + ","
                            + _CostoProducto + ","
                            + _CantidadStock+ ","
                            + _CodigoCategoriaProducto + ","
                            + _EstadoProducto);
                    }
                    if (_TipoDeOperacion == 2)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("update producto set nombre_producto = '"
                            + _NombreProducto + "', costo_producto='"
                            + _CostoProducto + "', cantidad_stock='"
                            + _CantidadStock + "', codigo_categoria_producto='"
                            + _CodigoCategoriaProducto + "', estado_producto='"
                            + _EstadoProducto + " where id_producto = "
                            + _CodigoProducto);
                    }
                    if (_TipoDeOperacion == 3)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("delete from producto where id_producto = "
                            + _CodigoProducto);
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
                }
            }
            else
                _Resultado = false;
            return _Resultado;
        }
        #endregion
    }
}