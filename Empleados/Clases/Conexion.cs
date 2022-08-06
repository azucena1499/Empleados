using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Clases
{
    class Conexion
    {
        public string Conn()
        {
            //Defino  la cadena de conexion para para uso en el proyecto 
            string conexion = (@"Data Source=DESKTOP-4CSQT06;Initial Catalog=Empleados;Integrated Security=True");
            return conexion;
        }
    }
}
