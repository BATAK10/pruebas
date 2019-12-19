using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjHeladeria.BLL
{
    public class Clientes
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
        private string _CodigoCliente;
        public string CodigoCliente
        {
            get
            {
                return _CodigoCliente;
            }
            set
            {
                _CodigoCliente = value;
            }
        }
        private string _NombreCliente;
        public string NombreCliente
        {
            get
            {
                return _NombreCliente;
            }
            set
            {
                _NombreCliente = value;
            }
        }
        private string _ApellidoCliente;
        public string ApellidoCliente
        {
            get
            {
                return _ApellidoCliente;
            }
            set
            {
                _ApellidoCliente = value;
            }
        }
        private string _TelefonoCliente;
        public string TelefonoCliente
        {
            get
            {
                return _TelefonoCliente;
            }
            set
            {
                _TelefonoCliente = value;
            }
        }
        private string _DireccionCliente;
        public string DireccionCliente
        {
            get
            {
                return _DireccionCliente;
            }
            set
            {
                _DireccionCliente = value;
            }
        }
        private string _EstadoCliente;
        public string EstadoCliente
        {
            get
            {
                return _EstadoCliente;
            }
            set
            {
                _EstadoCliente = value;
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
        #endregion
        #region Metodos
        public bool ValidarCliente()
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
                if (_CodigoCliente == "0")
                {
                    _Mensaje += "Ingrese el código de cliente";
                    _Resultado = false;
                }
            }
            if (_NombreCliente == "")
            {
                _Mensaje += "Ingrese el nombre de cliente. "; _Resultado = false;
            }
            if (_EstadoCliente == "0" || _EstadoCliente == "")
            {
                _Mensaje += "Ingrese el estado de cliente. "; _Resultado = false;
            }
            if (_ApellidoCliente == "")
            {
                _Mensaje += "Ingrese el apellido de cliente. "; _Resultado = false;
            }
            if (_TelefonoCliente == "")
            {
                _Mensaje += "Ingrese el teléfono de cliente. "; _Resultado = false;
            }
            else
            {
                int _TelefonoD = 0;
                if (!int.TryParse(_TelefonoCliente, out _TelefonoD))
                {
                    _Mensaje += "Ingrese el teléfono con valores numéricos. "; _Resultado = false;
                }
                else
                {
                    if (_TelefonoCliente.Length != 8)
                    {
                        _Mensaje += "Ingrese el teléfono con 8 dígitos. "; _Resultado = false;
                    }
                }
            }
            if (_Usuario == "")
            {
                _Mensaje += "Ingrese el usuario. "; _Resultado = false;
            }
            return _Resultado;
        }

        public bool OperarCliente()
        {
            bool _Resultado = true;

            _Resultado = ValidarCliente();
            if (_Resultado)
            {
                DAL.DAL _Conectar = new DAL.DAL();
                int resultadoQuery = 0;

                try
                {
                    if (_TipoDeOperacion == 1)
                    {
                        // Obtener código de cliente siguiente
                        Funciones _f = new Funciones();
                        int codigoCliente = (int)_f.Consultar(int.Parse(_CodigoCliente), "ISNULL(max(id_cliente),0) + 1", "cliente", "", "", "");
                        resultadoQuery = _Conectar.ejecutarComando("insert into cliente (id_cliente, nombre_cliente, apellido_cliente, telefono_cliente, direccion_cliente, estado_cliente, usuario) values ("
                            + codigoCliente + ","
                            + "'" + _NombreCliente + "' ,"
                            + "'" + _ApellidoCliente + "',"
                            + _TelefonoCliente + ","
                            + "'" + _DireccionCliente + "',"
                            + _EstadoCliente
                            + ",'" + _Usuario
                            + "')");
                    }
                    if (_TipoDeOperacion == 2)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("update cliente set nombre_cliente = '" +
                            _NombreCliente + "', apellido_cliente='"
                            + _ApellidoCliente + "', telefono_cliente = '"
                            + _TelefonoCliente + "', direccion_cliente = '"
                            + _DireccionCliente + "', estado_cliente = "
                            + _EstadoCliente + " where id_cliente = "
                            + _CodigoCliente);
                    }
                    if (_TipoDeOperacion == 3)
                    {
                        resultadoQuery = _Conectar.ejecutarComando("delete from cliente  where id_cliente = "
                            + _CodigoCliente);
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