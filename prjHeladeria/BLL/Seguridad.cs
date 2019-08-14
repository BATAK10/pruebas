using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace prjHeladeria.BLL
{
    public class Seguridad
    {
        Funciones CargarDatos = new Funciones();
        #region Propiedades
        public DataTable _dtPermisos = new DataTable();
        public string Nombres;
        public string Apellidos;

        public int CodigoEmpleado;
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


        private int _CodigoUsuario;
        public int CodigoUsuario
        {
            get
            {
                return _CodigoUsuario;
            }
            set
            {
                _CodigoUsuario = value;
            }
        }
        private string _Contraseña;
        public string Contraseña
        {
            get
            {
                return _Contraseña;
            }
            set
            {
                _Contraseña = value;
            }
        }
        private string _Tabla;
        public string Tabla
        {
            get
            {
                return Tabla;
            }
            set
            {
                _Tabla = value;
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
        #endregion
        #region Metodos

        public bool ValidarUsuario()
        {
            bool _Resultado = true;
            _Mensaje = "";



            if (_CodigoUsuario == 0)
            { _Mensaje += "Ingrese el usuario"; _Resultado = false; }

            return _Resultado;
        }
        public bool VerificarPermisos()
        {
            bool _Resultado = true;
            string where = " CODIGO_USUARIO,=," + _CodigoUsuario + ";";
            where += "DESCRIPCION,=," + _Tabla + ";";

            _dtPermisos = (DataTable)CargarDatos.Consultar(_dtPermisos, "CODIGO_PERMISO_DISPONIBLE,CODIGO_ACCION", "V_PERMISOS", where, "", "CODIGO_PERMISO_DISPONIBLE ASC");
            return _Resultado;
        }

        public string ObtenerPermisos()
        {
            string _Resultado = "";
            string where = " CODIGO_USUARIO,=," + _CodigoUsuario + ";";


            _dtPermisos = (DataTable)CargarDatos.Consultar(_dtPermisos, "CODIGO_PERMISO_DISPONIBLE,CODIGO_ACCION", "V_PERMISOS", where, "", "CODIGO_PERMISO_DISPONIBLE ASC");
            foreach (DataRow row in _dtPermisos.Rows)
            {
                _Resultado += row["CODIGO_PERMISO_DISPONIBLE"].ToString() + ";";
            }
            return _Resultado;
        }
        #endregion
    }
}