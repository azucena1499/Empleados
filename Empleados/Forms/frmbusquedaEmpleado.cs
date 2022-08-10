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
            //cmd.Connection = Conexion;
            //cmd.CommandText = "SELECT e.Clave_Emp, e.Nombre,e.ApPaterno,e.ApMaterno,e.FecNac,d.Puesto as Departamento,e.sueldo,e.estatus FROM Empleados e INNER JOIN Departamentos d ON e.Departamento = d.Puesto WHERE  e.estatus=1 ";//sirve para buscar lo que contenga la expresion em id o nombre
            //da.SelectCommand = cmd;
            //dgBusqueda.DataSource = dt;
            llenarcboxDepa();
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //dgBusqueda.Columns.Add(btn);
            //btn.HeaderText = "Click Data";
            //btn.Text = "Modificar";
            //btn.Name = "btn";
            //btn.UseColumnTextForButtonValue = true;
            buscar("");

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
        public DataGridViewButtonColumn GB = new DataGridViewButtonColumn();

        public void buscar(string expresion)
        {
            try
            {
                objconexion = new Clases.Conexion();
                Conexion = new SqlConnection(objconexion.Conn());
                Conexion.Open();
                cmd.Connection = Conexion;
                cmd.Parameters.Clear();

                if (!string.IsNullOrEmpty(expresion))
                {


                    cmd.CommandText = " SELECT e.Clave_Emp,CONCAT(e.Nombre,' ',e.ApPaterno,' ',e.ApMaterno) as nombre_completo, e.Nombre,e.ApPaterno,e.ApMaterno,e.FecNac,d.Descripcion,e.sueldo,e.estatus FROM Empleados e INNER JOIN Departamentos d ON e.Departamento = d.Puesto  WHERE e.estatus=1 AND  (e.Clave_Emp LIKE '%' + '" + @expresion + "' + '%' OR e.Nombre LIKE '%' + '" + @expresion + "' + '%'  OR e.ApPaterno LIKE '%' + '" + @expresion + "' + '%' OR e.ApMaterno LIKE '%' +  '" + @expresion + "' + '%' OR e.FecNac LIKE '%' + '" + @expresion + "' + '%' OR d.Descripcion LIKE '%' + '" + @expresion + "' + '%' OR e.Sueldo LIKE '%' + '" + @expresion + "' + '%' OR e.estatus LIKE '%' + '" + @expresion + "' + '%')";//sirve para buscar lo que contenga la expresion em id o nombre
                    cmd.Parameters.AddWithValue("@expresion", expresion);
                }
                else
                {
                    cmd.CommandText = " SELECT e.Clave_Emp,CONCAT(e.Nombre,' ',e.ApPaterno,' ',e.ApMaterno) as nombre_completo, e.Nombre,e.ApPaterno,e.ApMaterno, e.FecNac,d.Descripcion,e.sueldo,e.estatus FROM Empleados e INNER JOIN Departamentos d ON e.Departamento = d.Puesto WHERE  e.estatus=1";
                }
                dt.Clear();
                da.SelectCommand = cmd;//no se ejecute antes
                //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                //dgBusqueda.Columns.Add(btn);
                //btn.HeaderText = "Click Data";
                //btn.Text = "edit";
                //btn.Name = "btn";
                //btn.UseColumnTextForButtonValue = true;
                dgBusqueda.DataSource = dt;
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
            string clave =dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[0].Value.ToString();
            string nombre = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[2].Value.ToString();
            string paterno = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[3].Value.ToString();
            string materno = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[4].Value.ToString();
            string fecha = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[5].Value.ToString();
            string departamento = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[6].Value.ToString();
            string sueldo = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[7].Value.ToString();
            string estatus = dgBusqueda.Rows[dgBusqueda.CurrentRow.Index].Cells[8].Value.ToString();


            frmEmpleado frm = new frmEmpleado(clave,nombre,paterno,materno,fecha,departamento,sueldo,estatus);
            frm.ShowDialog();


           

            


        }

        private void cboxsistema1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscar(cboxsistema1.Text);

        }

        private void dgBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
