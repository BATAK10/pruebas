using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjHeladeria.BLL
{
    public class CategoriaProducto
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
        private string _NombreCategoriaProducto;
        public string NombreCategoriaProducto
        {
            get
            {
                return _NombreCategoriaProducto;
            }
            set
            {
                _NombreCategoriaProducto = value;
            }
        }        
        private string _EstadoCategoriaProducto;
        public string EstadoCategoriaProducto
        {
            get
            {
                return _EstadoCategoriaProducto;
            }
            set
            {
                _EstadoCategoriaProducto = value;
            }
        }
        #endregion
        #region Metodos
        public bool ValidarCategoriaProducto()
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
                if (_CodigoCategoriaProducto == "0")
                {
                    _Mensaje += "Ingrese el código de categoría producto";
                    _Resultado = false;
                }
            }
            if (_NombreCategoriaProducto == "")
            {
                _Mensaje += "Ingrese el nombre de categoría producto. "; _Resultado = false;
            }
            if (_EstadoCategoriaProducto == "0" || _EstadoCategoriaProducto == "")
            {
                _Mensaje += "Ingrese el estado de categoría producto. "; _Resultado = false;
            }
            return _Resultado;
        }

        public bool OperarCategoriaProducto()
        {
            bool _Resultado = true;

            _Resultado = ValidarCategoriaProducto();
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
                        int codigoCategoriaProducto= (int)_f.Consultar(int.Parse(_CodigoCategoriaProducto), "IFNULL(max(id_categoria_producto),0) + 1", "categoria_producto", "", "", "");
                        resultadoQuery = _Conectar.ejecutarComando("insert into categoria_producto values ("
                            + codigoCategoriaProducto + ","
                            + "'"+_NombreCategoriaProducto + "',"                            
                            + _EstadoCategoriaProducto
                            +")");
                    }
                    if (_TipoDeOperacion == 2)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("update categoria_producto set nombre_categoria_producto = '" +
                            _NombreCategoriaProducto + "', estado_categoria_producto='"                                                        
                            + _EstadoCategoriaProducto + "' where id_categoria_producto = "
                            + _CodigoCategoriaProducto);
                    }
                    if (_TipoDeOperacion == 3)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("delete from categoria_producto where id_categoria_producto = "
                            + _CodigoCategoriaProducto);
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