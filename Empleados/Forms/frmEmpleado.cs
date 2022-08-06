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
    public partial class frmEmpleado : Form
    {
        Clases.Conexion objconexion;
        SqlConnection Conexion;
        private Clases.Conexion objConexionPrincipal;
        private SqlConnection cone;
        int existe = 0;
        public frmEmpleado()
        {
            InitializeComponent();
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

            this.cboxDepartamento.DataSource = dt;
            this.cboxDepartamento.ValueMember = "Puesto";
            this.cboxDepartamento.DisplayMember = "Descripcion";
            Conexion.Close();

        }


        private void toolGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (existe == 0)
                {
                    objconexion = new Clases.Conexion();
                    Conexion = new SqlConnection(objconexion.Conn());

                    //se abre la conexion
                    Conexion.Open();
                    string query = "insert into Empleados values(@Clave_Emp,@Nombre,@ApPaterno,@apMaterno,@FecNac,@Departamento,@sueldo)";  //este es para insetar,se hace la conexion el campo y esl paramet                                                                                                            //asigno a comando el sqlcommand
                    SqlCommand comando = new SqlCommand(query, Conexion);
                    //inicializo cualquier parametrodefinido anteriormente
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@Clave_Emp", txtclave.Text);
                    comando.Parameters.AddWithValue("@Nombre", txtnombre.Text);
                    comando.Parameters.AddWithValue("@ApPaterno", txtxape.Text);
                    comando.Parameters.AddWithValue("@FecNac", dtpFecha.Value.Date);
                    comando.Parameters.AddWithValue("@sueldo", txtSueldo.Text);
                    comando.Parameters.AddWithValue("@Departamento", cboxDepartamento.SelectedValue);
                    comando.Parameters.AddWithValue("@ApMaterno", txtapeMat.Text);
                    comando.ExecuteNonQuery();
                    Conexion.Close();

                    MessageBox.Show("Empleado guardada con éxito", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtClave.Clear();
                }
                if (existe == 1)
                {
                    objconexion = new Clases.Conexion();
                    Conexion = new SqlConnection(objconexion.Conn());
                    //se abre la conexion
                    Conexion.Open();
                    string query = "update Empleados set Nombre=@Nombre,ApPaterno=@ApPaterno,ApMaterno=@ApMaterno,FecNac=@FecNac,Departamento=@Departamento,sueldo=@sueldo where Clave_Emp=@Clave_Emp";  //este es para modificar,se hace la conexion el campo y esl paramet                                                                                                            //asigno a comando el sqlcommand
                    SqlCommand comando = new SqlCommand(query, Conexion);
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@Clave_Emp", int.Parse(txtclave.Text));
                    comando.Parameters.AddWithValue("@Nombre", txtnombre.Text);
                    comando.Parameters.AddWithValue("@ApPaterno", txtxape.Text);
                    comando.Parameters.AddWithValue("@FecNac", dtpFecha.Value.Date);
                    comando.Parameters.AddWithValue("@sueldo", txtSueldo.Text);
                    comando.Parameters.AddWithValue("@Departamento", cboxDepartamento.SelectedIndex);
                    comando.Parameters.AddWithValue("@ApMaterno", txtapeMat.Text);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("consulta modificada con éxito", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
            }
            catch (SqlException err)
            {
                MessageBox.Show("Error en consulta: " + err.Message, "Error Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtclave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                //chechando que no sea valor nulo o blanco
                if (string.IsNullOrEmpty(txtclave.Text))
                {
                    MessageBox.Show("Error:No se permiten nulos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtclave.Enabled = true;
                    txtclave.Clear();
                    txtclave.Focus();
                }
                else
                {
                    //no fue nulo

                    objconexion = new Clases.Conexion();
                    Conexion = new SqlConnection(objconexion.Conn());
                    //se abre la conexion
                    Conexion.Open();
                    string query = "select * from Empleados where Clave_Emp=@Clave_Emp";
                    //asigno a comando el sqlcommand
                    SqlCommand comando = new SqlCommand(query, Conexion);
                    //inicializo cualquier parametrodefinido anteriormente
                    comando.Parameters.Clear();

                    comando.Parameters.AddWithValue("@Clave_Emp", this.txtclave.Text);
                    comando.Parameters.AddWithValue("@Nombre", this.txtnombre.Text);
                    comando.Parameters.AddWithValue("@ApPaterno", this.txtxape.Text);
                    comando.Parameters.AddWithValue("@Sueldo", this.txtSueldo.Text);
                    comando.Parameters.AddWithValue("@Departamento", this.cboxDepartamento.Text);
                    comando.Parameters.AddWithValue("@FecNac", this.dtpFecha.Text);
                    comando.Parameters.AddWithValue("@ApMaterno", this.txtapeMat.Text);


                    SqlDataReader leer = comando.ExecuteReader();
                    if (leer.Read())
                    {
                        //inicializo la variable 1 para que el programa sepa que existe
                        existe = 1;
                        txtnombre.Enabled = true;
                        txtxape.Enabled = true;
                        txtSueldo.Enabled = true;
                        txtapeMat.Enabled = true;
                        cboxDepartamento.Enabled = true;
                        txtnombre.Focus();
                        txtclave.Enabled = false;
                        toolGuardar.Enabled = true;
                        toolEliminar.Enabled = true;

                        //igualo los campos o columnas al txtnombre
                        txtnombre.Text = leer["Nombre"].ToString();
                        txtxape.Text = leer["ApMaterno"].ToString();
                        txtSueldo.Text = leer["Sueldo"].ToString();
                        txtclave.Text = leer["Clave_Emp"].ToString();
                        dtpFecha.Text = leer["FecNac"].ToString();
                        cboxDepartamento.Text = leer["Departamento"].ToString();
                        txtapeMat.Text = leer["Cl_apemat"].ToString();


                    }
                }
            else
            {
                //si lavariable existe vale 0 y se usara insert
                existe = 0;
                if (MessageBox.Show("Empleado no registrado.¿desea agregar un nuevo Empleado?", "No existe", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    txtnombre.Enabled = true;
                    txtnombre.Focus();
                }
            }
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {

            llenarcboxDepa();
            //maximo();
        }

        private void maximo()
        {
            objconexion = new Clases.Conexion();
            Conexion = new SqlConnection(objconexion.Conn());
            Conexion.Open();
            string query = "SELECT max(Clave_Emp)+1 as ultimo from Empleados";
            SqlCommand comando = new SqlCommand(query, Conexion);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read())
            {
                txtclave.Text = leer["ultimo"].ToString();
                if (txtclave.Text == "")
                {
                    txtclave.Text = "1";
                }
            }
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmbusquedaEmpleado frm = new frmbusquedaEmpleado();

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                txtclave.Focus();
                txtclave.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[0].Value.ToString();
                txtnombre.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[1].Value.ToString();
                txtxape.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[2].Value.ToString();
                txtapeMat.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[3].Value.ToString();

                dtpFecha.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[4].Value.ToString();

                cboxDepartamento.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[5].Value.ToString();
                txtSueldo.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[6].Value.ToString();

                existe = 1;
                txtclave.Enabled = false;
                toolEliminar.Enabled = true;
            }
            else
            {
                existe = 0;

            }
        }
        private void limpiar()
        {
            txtSueldo.Clear();
            txtnombre.Clear();
            txtclave.Enabled = true;
            txtclave.Clear();
            txtclave.Focus();
            txtxape.Clear();
            txtapeMat.Clear();
            cboxDepartamento.SelectedIndex = 0;
            toolEliminar.Enabled = false;
        }
        private void toolEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                objconexion = new Clases.Conexion();
                Conexion = new SqlConnection(objconexion.Conn());
                //se abre el contenido
                Conexion.Open();
                string query = "DELETE from Empleados where Clave_Emp=" + txtclave.Text;
                SqlCommand comando = new SqlCommand(query, Conexion);
                comando.Parameters.Clear();
                if (MessageBox.Show("Empleado sera eliminada,está seguro?", "Eliminar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Empleado Eliminada", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }

            }
            catch (SqlException err)
            {
                MessageBox.Show("Error en consulta: " + err.Message, "Error Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
           
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
    
}
