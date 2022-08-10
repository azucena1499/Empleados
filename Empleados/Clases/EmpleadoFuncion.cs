using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Clases
{
    public class EmpleadoFuncion
    {
        private Clases.Conexion objconexion;
        private SqlConnection Conexion;
        public EmpleadoFuncion()
        {
            objconexion = new Clases.Conexion();
            Conexion = new SqlConnection(objconexion.Conn());
        }
        
        public bool guardar(string clave, string nombre, string paterno, string materno, DateTime fecha, string departamento, string sueldo, string estatus)
        {

            try
            {
               

                //se abre la conexion
                Conexion.Open();
                string query = "insert into Empleados values(@Clave_Emp,@Nombre,@ApPaterno,@apMaterno,@FecNac,@Departamento,@sueldo,@estatus)";  //este es para insetar,se hace la conexion el campo y esl paramet                                                                                                            //asigno a comando el sqlcommand
                SqlCommand comando = new SqlCommand(query, Conexion);
                //inicializo cualquier parametrodefinido anteriormente
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@Clave_Emp", int.Parse(clave));
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@ApPaterno", paterno);
                comando.Parameters.AddWithValue("@FecNac", fecha);
                comando.Parameters.AddWithValue("@sueldo", sueldo);
                comando.Parameters.AddWithValue("@Departamento", int.Parse(departamento));
                comando.Parameters.AddWithValue("@ApMaterno", materno);
                comando.Parameters.AddWithValue("@estatus", 1);

                comando.ExecuteNonQuery();
                Conexion.Close();
                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;

            }
        }
        public bool modificar(string clave, string nombre, string paterno, string materno, DateTime fecha, string departamento, string sueldo, string estatus)
        {

            try
            {

                //se abre la conexion
                Conexion.Open();
                string query = "update Empleados set Nombre=@Nombre,ApPaterno=@ApPaterno,ApMaterno=@ApMaterno,FecNac=@FecNac,Departamento=@Departamento,sueldo=@sueldo,estatus=@estatus where Clave_Emp=@Clave_Emp";  //este es para modificar,se hace la conexion el campo y esl paramet                                                                                                            //asigno a comando el sqlcommand
                SqlCommand comando = new SqlCommand(query, Conexion);
                //inicializo cualquier parametrodefinido anteriormente
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@Clave_Emp", int.Parse(clave));
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@ApPaterno", paterno);
                comando.Parameters.AddWithValue("@FecNac", fecha);
                comando.Parameters.AddWithValue("@sueldo", sueldo);
                comando.Parameters.AddWithValue("@Departamento", int.Parse(departamento));
                comando.Parameters.AddWithValue("@ApMaterno", materno);
                comando.Parameters.AddWithValue("@estatus", 1);


                comando.ExecuteNonQuery();
                Conexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;

            }


        }

    }
}
