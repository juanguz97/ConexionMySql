using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ConexionMySql.Clases
{
    class ConexionMySql
    {
        //Parámetros de conexión a la base de datos MySQL.
        private static string servidor_bd = "localhost";
        private static string usuario_bd = "usuario";
        private static string contrasena_bd = "contraseña";
        private static string puerto_bd = "3306";
        private static string bd_bd = "base_de_datos";
        //Método privado genérico, para cualquier consulta la ejecuté.
        private static List<DataRow> RealizarConexionMySql(string miServidor,string miBD,string miUsuario,string miContrasena,string miPuerto,string miConsulta)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", miServidor, miPuerto, miUsuario, miContrasena, miBD, "none"));
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(miConsulta, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0].AsEnumerable().ToList();              
            }
            catch (MySqlException)
            {
                return null;
            }
        }
        //Método privado genérico, para cualquier procedimiento almacenado lo ejecuté, tenga o no parámetros.
        private static List<DataRow> RealizarConexionMySqlProcedientoAlmacenado(string miServidor, string miBD, string miUsuario, string miContrasena, string miPuerto, string miConsulta, List<(String, Object)> Parametros)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", miServidor, miPuerto, miUsuario, miContrasena, miBD, "none"));
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(miConsulta, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Parametros != null)
                {
                    foreach ((String, Object) miParametro in Parametros)
                    {
                        cmd.Parameters.AddWithValue(miParametro.Item1, miParametro.Item2);
                    }
                }
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0].AsEnumerable().ToList();
            }
            catch (MySqlException)
            {
                return null;
            }
        }
        //Mediante este método se obtienen los datos, como lista de datarow y se reutiliza el método RealizarConexionMySql para todas las consultas.
        public static List<DataRow> EjecutarConsulta(string miConsulta)
        {
            return RealizarConexionMySql(servidor_bd, bd_bd, usuario_bd, contrasena_bd, puerto_bd, miConsulta);
        }
        //Mediante este método se obtienen los datos, como lista de datarow y se reutiliza el método RealizarConexionMySqlProcedientoAlmacenado para todas los procedimientos almacenados.
        //La colección de parámetros para el procedimiento almacenado se maneja mediante una lista de tuplas de string y object para que pueda recibir cualquier valor la cantidad de veces necesaria.
        //Si no tiene parámetros, la consulta se envia null en la lista de parámetros.
        public static List<DataRow> EjecutarProcedimiento(string miProcedimiento, List<(String, Object)> miListaDeParametros)
        {
            return RealizarConexionMySqlProcedientoAlmacenado(servidor_bd, bd_bd, usuario_bd, contrasena_bd, puerto_bd, miProcedimiento, miListaDeParametros);
        }
    }
}
