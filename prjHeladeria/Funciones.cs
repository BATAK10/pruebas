using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using prjHeladeria.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace prjHeladeria
{
    public class Funciones
    {
        private string _Permisos;
        public string Permisos
        {
            get
            {
                return _Permisos;
            }
            set
            {
                _Permisos = value;
            }
        }

        public string getSortDirectionString(SortDirection sortDirection)
        {

            try
            {
                if (sortDirection == SortDirection.Ascending)
                {
                    return "ASC";
                }
                else
                {
                    return "DESC";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ObtenerPermisos(int Usuario)
        {
            Seguridad _Seguridad = new Seguridad();
            _Seguridad.CodigoUsuario = Usuario;
            _Permisos = _Seguridad.ObtenerPermisos();
            return _Permisos;
        }
        public bool InsertarBitacora(int codigoEmpleado, int registroAfectado, string PaginaAfectada, int AccionRealizada, string TablaAfectada)
        {
            bool _Resultado = true;
            if (_Resultado)
            {
                DAL.DAL conectar = new DAL.DAL();
                string resultadoQuery = "-";
                string mensaje = "";
                try
                {
                    resultadoQuery = conectar.EjecutarProcedimiento("SP_BITACORA",
                                                new object[] { new MySqlParameter("?TIPO_OPERACION", 1),
                                                 new MySqlParameter("?CODIGO_BITACORA_L", 0  ),
                                                new MySqlParameter("?CODIGO_EMPLEADO", codigoEmpleado  ),
                                                new MySqlParameter("?REGISTRO_AFECTADO",registroAfectado ),
                                                new MySqlParameter("?PAGINA_AFECTADA",PaginaAfectada),
                                                new MySqlParameter("?FECHA_BITACORA", DateTime.Now ),
                                                new MySqlParameter("?TABLA_AFECTADA", TablaAfectada),
                                                new MySqlParameter("?ACCION_REALIZADA", AccionRealizada)
                                                });
                    string dato = resultadoQuery.ToString();
                    if (dato == "1")
                        _Resultado = true;
                    else
                    {
                        mensaje = dato;
                        _Resultado = false;
                    }
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }

            }
            else
                _Resultado = false;
            return _Resultado;
        }



        public DataSet Listar(string consulta)
        {
            DAL.DAL conectar = new DAL.DAL();
            DataSet Buscar = new DataSet();
            try
            {
                Buscar = conectar.ConsultaGrid(consulta);
            }
            catch
            {
                throw;
            }
            return Buscar;
        }
        public object Consultar(int contenedor, string select, string from, string where, string groupby, string orderby)
        {

            DAL.DAL conectar = new DAL.DAL();

            try
            {
                contenedor = Convert.ToInt16(conectar.Consultar(contenedor, select, from, where, groupby, orderby));
                return contenedor;
            }
            catch
            {
                return null;
            }

        }
        public object Consultar(DataTable contenedor, string select, string from, string where, string groupby, string orderby)
        {

            DAL.DAL conectar = new DAL.DAL();
            try
            {
                contenedor = (DataTable)conectar.Consultar(contenedor, select, from, where, groupby, orderby);
                if (contenedor == null)
                    contenedor = new DataTable();
                return contenedor;
            }
            catch
            {
                return null;
                throw;
            }

        }

        public static string DataTableToJson(DataTable dtDatos)
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
        public static DataTable JsonToDataTable(string json)
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
    }
}