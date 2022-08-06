using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empleados.Forms
{
    public partial class frmbusquedaEmpleado : Form
    {
        public frmbusquedaEmpleado()
        {
            InitializeComponent();
        }
        Clases.Conexion objconexion;
        SqlConnection Conexion;
        public DataTable dt = new DataTable();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter da = new SqlDataAdapter();

        private void frmbusquedaEmpleado_Load(object sender, EventArgs e)
        {
            objconexion = new Clases.Conexion();
            Conexion = new SqlConnection(objconexion.Conn());
            this.CancelButton = btnCancelar;
            cmd.Connection = Conexion;
            cmd.CommandText = "SELECT e.Clave_Emp, e.Nombre,e.ApPaterno,e.ApMaterno,e.FecNac,d.Puesto as Departamento,e.sueldo FROM Empleados e INNER JOIN Departamentos d ON e.Departamento = d.Puesto ";//sirve para buscar lo que contenga la expresion em id o nombre
            da.SelectCommand = cmd;
            dgBusqueda.DataSource = dt;
            llenarcboxDepa();

        }


        private void llenarcboxDepa()
        {
            DataTable dt = new DataTable();
            objconexion = new Clases.Conexion();
            Conexion = new SqlConnection(objconexion.Conn());
            Conexion.Open();
            string query = "SELECT * from Departamentos where Puesto >=1 order by Descripcion";
            //defino comando
            SqlCommand comando = new SqlCommand(query, Conexion);
            //defino mi adapter
            SqlDataAdapter da = new SqlDataAdapter(comando);
            //lleno el datatable
            da.Fill(dt);
            Conexion.Close();
            DataRow fila = dt.NewRow();
            fila["Descripcion"] = "Seleccione";
            dt.Rows.InsertAt(fila, 0);
            this.cboxsistema1.DataSource = dt;
            this.cboxsistema1.ValueMember = "Puesto";
            this.cboxsistema1.DisplayMember = "Descripcion";
            Conexion.Close();

        }
      


        private void txtExpresion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                buscar(txtExpresion.Text);
            }
        }
        
        public void buscar(string expresion)
        {
            try
            {
                objconexion = new Clases.Conexion();
                Conexion = new SqlConnection(objconexion.Conn());
                Conexion.Open();
                cmd.Connection = Conexion;
                cmd.Parameters.Clear();

                if (!string.IsNullOrEmpty(expresion))//se pone el ! para que devuelva falso y no true
                {

                    //cmd.CommandText = "SELECT e.Clave_Emp,e.Nombre,e.ApPaterno,e.ApMaterno,e.FecNac,d.Descripcion,e.sueldo FROM Empleados e INNER JOIN Departamentos d ON  e.Departamento = d.Puesto  WHERE e.Clave_Emp  LIKE '%' + '" + @expresion + "' + '%' OR e.Nombre LIKE '%' + '" + @expresion + "' + '%' e.ApPaterno LIKE '%' + '" + @expresion + "' + '%' OR e.ApMaterno LIKE '%' + '" + @expresion + "' + '%' e.FecNac LIKE '%' + '" + @expresion + "' + '%' OR d.Descripcion LIKE '%' + '" + @expresion + "' + '%' e.Sueldo LIKE '%' + '" + @expresion + "' + '%'"; //sirve para buscar lo que contenga la expresion em id o nombre

                    cmd.CommandText = " SELECT e.Clave_Emp, e.Nombre,e.ApPaterno,e.ApMaterno,e.FecNac,d.Descripcion,e.sueldo FROM Empleados e INNER JOIN Departamentos d ON e.Departamento = d.Puesto  WHERE e.Clave_Emp LIKE '%' + '" + @expresion + "' + '%' OR e.Nombre LIKE '%' + '" + @expresion + "' + '%'  OR e.ApPaterno LIKE '%' + '" + @expresion + "' + '%' OR e.ApMaterno LIKE '%' +  '" + @expresion + "' + '%' OR e.FecNac LIKE '%' + '" + @expresion + "' + '%' OR d.Descripcion LIKE '%' + '" + @expresion + "' + '%' OR e.Sueldo LIKE '%' + '" + @expresion + "' + '%''%'";//sirve para buscar lo que contenga la expresion em id o nombre
                    cmd.Parameters.AddWithValue("@expresion", expresion);
                }
                da.SelectCommand = cmd;//no se ejecute antes
                dgBusqueda.DataSource = dt;
                dt.Clear();
                da.Fill(dt);
            }
            catch (SqlException err)
            {

                MessageBox.Show("Error de base de datos: " + err.Message, "Error busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

        }

        private void cboxsistema1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscar(cboxsistema1.Text);

        }
    }
}
