using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace prjHeladeria.DAL
{
    public class DAL
    {
        private string cadena;
        private DataSet ds;
        private DataTable dt = new DataTable();
        private MySqlDataAdapter da = new MySqlDataAdapter();
        private MySqlCommand cm;
        private MySqlConnection cn;
        private MySqlTransaction transaccion;
        private string _Mensaje;
        public string _consulta = "";
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

        //declaracion del constructor para una nueva conexion
        public DAL()
        {
            establecerConexion();
            Open_Connection();
        }

        public void establecerConexion()
        {
            try
            {
                //Cadena Demo
                cadena = "data source=sql10.freesqldatabase.com; username =sql10300279; password=Mw84A6UpfP;database=sql10300279; SslMode=none";
                //cadena = "data source=MYSQL5016.site4now.net; username =db_a4240c_desa; password=rena4321;database=db_a4240c_doctor; SslMode=none";
                //Bryan db_a4240c_desarr rena5566
                //Mario db_a4240c_desa rena4321
                //Jorge a4240c_doctor  rena1234
                cn = new MySqlConnection(cadena);
            }
            catch (Exception f)
            {
                //MessageBox.Show("Error: \n" + f.Message);
            }
        }

        public string EjecutarProcedimiento(string procedimiento, object[] parametros)
        {
            try
            {
                cm = new MySqlCommand(procedimiento, cn);
                cm.CommandType = CommandType.StoredProcedure;
                foreach (object parametro in parametros)
                {
                    var param = cm.Parameters.Add((MySqlParameter)parametro);
                }
                string resultado = cm.ExecuteNonQuery().ToString();

                Close_Connection();
                return resultado;
            }
            catch (Exception ex)
            {
                Close_Connection();
                return ex.Message;
            }
        }

        public DataSet ConsultaGrid(string consulta)
        {
            try
            {
                da = new MySqlDataAdapter(consulta, cn);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error al consultar datos:" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
        }

        public DataTable RealizarConsulta(string procedimiento, object[] parametros)
        {
            try
            {
                dt = new DataTable();
                da = new MySqlDataAdapter(procedimiento, cn);
                ds = new DataSet();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                foreach (MySqlParameter parametro in parametros)
                {
                    da.SelectCommand.Parameters.Add((MySqlParameter)parametro);
                }
                da.Fill(ds);
                Close_Connection();
                return ds.Tables[0];
            }
            catch
            {
                Close_Connection();
                //throw;
                return null;
            }
        }
        public object Consultar(object contenedor, string select, string from, string where, string groupby, string orderby)
        {
            try
            {
                #region ArmarCadeaConexion;

                string _WhereAcumular = "";
                bool _resultado = true;
                if (select != "")
                {
                    _consulta += "SELECT " + select;
                    _resultado = true;
                }
                else
                {
                    _resultado = false;
                    _Mensaje = "Ingrese los campos a consultar";
                }
                if (from != "")
                {
                    _consulta += " FROM " + from;
                    _resultado = true;
                }
                else
                {
                    _resultado = false;
                    _Mensaje = "Ingrese la tabla o vista a consultar";
                }
                #endregion



                #region tipo entero;

                if (contenedor.GetType().Equals(typeof(int)))
                {

                    string[] SepararSentencias = where.Split(';');
                    //    cmd.Parameters.Add("@param1", colorList[0] / ToString());
                    foreach (string line in SepararSentencias)
                    {
                        //array for the measurement
                        string[] Datos = line.Split(',');
                        if (Datos.Length == 3)
                        {

                            if (Datos[0].ToString().ToUpper().Contains("OR "))
                            {
                                _WhereAcumular += Datos[0] + " " + Datos[1] + " " + Datos[2];
                            }
                            else if (Datos[1].ToString().ToUpper() == "LIKE")
                            {
                                _WhereAcumular += " AND  " + Datos[0] + " " + Datos[1] + " " + Datos[2];
                            }
                            else
                            {
                                _WhereAcumular += " AND  " + Datos[0] + Datos[1] + "@" + Datos[0].TrimStart();
                            }

                        }
                    }


                    _consulta += " WHERE 1=1 " + _WhereAcumular;
                    if (groupby != "")
                    {
                        _consulta += " GROUP BY " + groupby;
                    }
                    if (orderby != "")
                    {
                        _consulta += " ORDER BY " + orderby;
                    }

                    MySqlCommand comandos = new MySqlCommand(_consulta, cn);
                    foreach (string line in SepararSentencias)
                    {
                        string[] Datos = line.Split(',');
                        if (Datos.Length == 3)
                        {
                            if (Datos[1].ToString().ToUpper() != "LIKE")
                            {
                                comandos.Parameters.AddWithValue("@" + Datos[0].TrimStart(), Datos[2]);


                            }
                        }
                    }
                    MySqlDataReader rdr = comandos.ExecuteReader();




                    var datos = new DataTable();
                    datos.Load(rdr);
                    foreach (DataRow row in datos.Rows)
                    {
                        contenedor = Convert.ToInt16(row[0]);
                    }
                    rdr.Close();
                    rdr.Dispose();
                    Close_Connection();
                }
                else if (contenedor.GetType().Equals(typeof(DataTable)))
                {


                    string[] SepararSentencias = where.Split(';');
                    //    cmd.Parameters.Add("@param1", colorList[0] / ToString());
                    foreach (string line in SepararSentencias)
                    {
                        //array for the measurement
                        string[] Datos = line.Split(',');
                        if (Datos.Length == 3)
                        {

                            if (Datos[0].ToString().ToUpper().Contains("OR "))
                            {
                                _WhereAcumular += Datos[0] + " " + Datos[1] + " " + Datos[2];
                            }
                            else if (Datos[1].ToString().ToUpper() == "LIKE")
                            {
                                _WhereAcumular += " AND  " + Datos[0] + " " + Datos[1] + " " + Datos[2];
                            }
                            else if (Datos[1].ToString().ToUpper() == "IN")
                            {
                                string[] _in = Datos[2].Split('&');
                                int _inLen = _in.Length;
                                string _inDatos = "(";
                                for (int i = 0; i < _inLen; i++)
                                {
                                    if (i == 0) _inDatos += _in[i];
                                    else _inDatos += "," + _in[i];
                                }
                                _inDatos += ")";
                                _WhereAcumular += " AND  " + Datos[0] + " " + Datos[1] + " " + _inDatos;
                            }
                            else
                            {
                                _WhereAcumular += " AND  " + Datos[0] + Datos[1] + "@" + Datos[0].TrimStart();
                            }

                        }
                    }


                    _consulta += " WHERE 1=1 " + _WhereAcumular;
                    if (groupby != "")
                    {
                        _consulta += " GROUP BY " + groupby;
                    }
                    if (orderby != "")
                    {
                        _consulta += " ORDER BY " + orderby;
                    }

                    MySqlCommand comandos = new MySqlCommand(_consulta, cn);
                    foreach (string line in SepararSentencias)
                    {
                        string[] Datos = line.Split(',');
                        if (Datos.Length == 3)
                        {
                            if (Datos[1].ToString().ToUpper() != "LIKE" && Datos[1].ToString().ToUpper() != "IN")
                            {
                                DateTime tryDate = new DateTime();
                                if (DateTime.TryParse(Datos[2], out tryDate))
                                {
                                    Datos[2] = "STR_TO_DATE('" + Datos[2] + "','%d/%m/%Y')";
                                }
                                string[] nombreParametro = Datos[0].Split('#'); // Alias del parámetro
                                if (nombreParametro.Length == 2)
                                    comandos.Parameters.AddWithValue("@" + nombreParametro[1].TrimStart(), Datos[2]);
                                else
                                    comandos.Parameters.AddWithValue("@" + Datos[0].TrimStart(), Datos[2]);
                            }
                        }
                    }
                    MySqlDataReader rdr = comandos.ExecuteReader();


                    var datos = new DataTable();
                    datos.Load(rdr);
                    contenedor = datos;
                    rdr.Close();
                    rdr.Dispose();
                    Close_Connection();


                    #endregion
                }



                return contenedor;
            }
            catch (Exception e)
            {
                _Mensaje = e.Message;
                //MessageBox.Show("Error al consultar datos:" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close_Connection();
                return null;


            }
        }



        //Metodo para hacer una consulta a una tabla. Devuelve un DataSet que sirve para llenar un DataTable, por eso el metodo
        //se llama fill_DataSet
        public DataSet fill_DataSet(string sql)
        {
            try
            {
                da = new MySqlDataAdapter(sql, cn);
                ds = new DataSet();
                da.Fill(ds);
                Close_Connection();
                return ds;

            }
            catch (Exception e)
            {
                Close_Connection();
                //MessageBox.Show("Error al consultar datos:" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }

        }

        //Este es un metodo que ejecuta una consulta, que devuelve un numero entero, como por ejemplo count
        public int ejecutarComando(string sql)
        {
            cm = new MySqlCommand(sql, cn);
            try
            {
                cm.Connection.Open();
                int n = cm.ExecuteNonQuery();
                cm.Connection.Close();
                return n;

            }
            catch (Exception e)
            {
                //MessageBox.Show("Error al ejecutar el comando " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cm.Connection.Close();
                return -1;

            }

        }

        //----------------------------------------Transacciones--------------------------
        public DataSet fill_DataSet_UsingTransaction(string sql)
        {

            try
            {
                cm = new MySqlCommand(sql, cn);
                cm.CommandType = CommandType.Text;
                cm.Transaction = transaccion;
                if (cn.State == ConnectionState.Closed)
                { cn.Open(); }
                cm.ExecuteNonQuery();
                da = new MySqlDataAdapter(cm);
                ds = new DataSet();
                da.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }


        public int EjecutarComandoUsingTransaction(string sql)
        {
            cm = new MySqlCommand(sql, cn);
            cm.CommandType = CommandType.Text;
            cm.Transaction = transaccion;
            if (cn.State == ConnectionState.Closed)
            { cn.Open(); }
            return cm.ExecuteNonQuery();
        }

        public void Open_Connection()
        {
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
        }

        public void Close_Connection()
        {
            if (cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }
        }
        public void Begin_Transaction()
        {
            transaccion = cn.BeginTransaction();
        }

        public void Commit_Transaction()
        {
            transaccion.Commit();
        }
        public void Rollback_Transaction()
        {
            transaccion.Rollback();
        }

        public int GetValueOfQueryUsingTran(string sql)
        {
            cm = new MySqlCommand(sql, cn);
            cm.CommandType = CommandType.Text;
            cm.Transaction = transaccion;
            if (cn.State == ConnectionState.Closed)
            { cn.Open(); }

            return Convert.ToInt32(cm.ExecuteScalar());
        }
    }
}