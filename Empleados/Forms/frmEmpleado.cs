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
using Empleados.Clases;

namespace Empleados.Forms
{
    public partial class frmEmpleado : Form
    {
        Clases.Conexion objconexion;
        private EmpleadoFuncion empleado;
        SqlConnection Conexion;
        private Clases.Conexion objConexionPrincipal;
        private SqlConnection cone;
        int existe = 0;
        int estatus;

        public frmEmpleado(string clave, string nombre, string paterno, string materno, string fecha, string departamento, string sueldo, string estatus)
        {

            InitializeComponent();
            iniciarCboxDepa();
            txtclave.Text = clave;
            txtnombre.Text = nombre;
            txtxape.Text = paterno;
            txtapeMat.Text = materno;
            dtpFecha.Text = fecha;
            cboxDepartamento.Text=departamento;
            txtSueldo.Text = sueldo;
            txtclave.Enabled = false;
            toolEliminar.Enabled = true;
            existe = 1;
            cboxEmpleado.Text = estatus;

            empleado = new EmpleadoFuncion();

        }
        public frmEmpleado()
        {
            maximo();
            iniciarCboxDepa();
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
                    
                   
                        bool resultado = empleado.guardar(txtclave.Text, txtnombre.Text, txtxape.Text, txtapeMat.Text, dtpFecha.Value, cboxDepartamento.SelectedValue.ToString(), txtSueldo.Text,cboxEmpleado.SelectedIndex.ToString() /*"1"*/ /*cboxEmpleado.SelectedValue.ToString()*/);
                        if (resultado)
                        {
                            BorrarMensaje();
                            MessageBox.Show("Empleado guardada con éxito", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiar();


                        }
                    
                    else
                    {
                        Validar();

                        MessageBox.Show("No se pudo guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    //txtClave.Clear();
                }
                if (existe == 1)
                {
                    bool resultado = empleado.modificar(txtclave.Text, txtnombre.Text, txtxape.Text, txtapeMat.Text, dtpFecha.Value, cboxDepartamento.SelectedValue.ToString(), txtSueldo.Text, cboxEmpleado.SelectedIndex.ToString()/*"1"*/);
                    if (resultado)
                    {
                        BorrarMensaje();
                        MessageBox.Show("consulta modificada con éxito", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mostrarDatos();

                    }
                    else
                    {
                        Validar();
                        MessageBox.Show("No se pudo modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
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
                    comando.Parameters.AddWithValue("@estatus", this.cboxEmpleado.SelectedIndex);


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
                        cboxEmpleado.Enabled = true;
                        cboxEmpleado.Enabled = true;
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
                        txtapeMat.Text = leer["ApPaterno"].ToString();
                        estatus = int.Parse(leer["estatus"].ToString());

                        if (estatus == 1)
                        {
                            cboxEmpleado.SelectedIndex = estatus - 1;
                        }
                        else
                        {
                            cboxEmpleado.SelectedIndex = estatus + 1;
                        }
                        //if (estatus == 0)
                        //{
                        //    MessageBox.Show("Empleado dado de baja", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    toolEliminar.Enabled = false;
                        //    toolGuardar.Enabled = false;
                        //    cboxEmpleado.Enabled = false;
                        //    txtnombre.Enabled = false;
                        //    txtclave.Enabled = false;
                        //    txtSueldo.Enabled = false;
                        //    cboxDepartamento.Enabled = false;
                        //    txtapeMat.Enabled = false;
                        //    txtxape.Enabled = false;
                        //    txtclave.Enabled = true;

                        //    //limpiar();


                        //}
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
            

        }
        private void iniciarCboxDepa()
        {
            cboxEmpleado.Text = "seleccione";
            cboxEmpleado.Items.Add("Activo");
            cboxEmpleado.Items.Add("Baja");
            llenarcboxDepa();
        }
        public void mostrarDatos()
        {
            objconexion = new Clases.Conexion();
            Conexion = new SqlConnection(objconexion.Conn());
            //se abre el contenido
            Conexion.Open();
            string query = "SELECT * from Empleados where estatus=1";
            SqlCommand comando = new SqlCommand(query, Conexion);
            //defino mi adapter
            SqlDataAdapter da = new SqlDataAdapter(comando);
            Conexion.Close();
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
            //frmbusquedaEmpleado frm = new frmbusquedaEmpleado();

            //frm.ShowDialog();


            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    txtclave.Focus();
            //    txtclave.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[0].Value.ToString();
            //    txtnombre.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[2].Value.ToString();
            //    txtxape.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[3].Value.ToString();
            //    txtapeMat.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[4].Value.ToString();

            //    dtpFecha.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[5].Value.ToString();
            //    //cboxEmpleado.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[9].Value.ToString();

            //    cboxDepartamento.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[6].Value.ToString();
            //    txtSueldo.Text = frm.dgBusqueda.Rows[frm.dgBusqueda.CurrentRow.Index].Cells[7].Value.ToString();

            //    existe = 1;
            //    txtclave.Enabled = false;
            //    toolEliminar.Enabled = true;
            //}
            //else
            //{
            //    existe = 0;

            
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
            cboxEmpleado.Enabled = false;
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
                string query = "update Empleados set estatus=0 where Clave_Emp=@Clave_Emp";
                
                SqlCommand comando = new SqlCommand(query, Conexion);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@Clave_Emp", txtclave.Text);
                if (MessageBox.Show("Empleado sera eliminada,está seguro?", "Eliminar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Empleado Eliminado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
                Conexion.Close();
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

        public bool Validar()
        {
            bool ok = true;
            if(txtnombre.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtnombre, "Ingrese nombre");
            }
            if (txtapeMat.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtapeMat, "Ingrese apellido materno");
            }
            if (txtxape.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtxape, "Ingrese apellido paterno");
            }
            if (txtSueldo.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtSueldo, "Ingrese sueldo");
            }
            if (cboxDepartamento.SelectedValue.ToString() == "")
            {
                ok = false;
                errorProvider1.SetError(cboxDepartamento, "Ingrese departamento");
            }
            if (dtpFecha.Value.ToString() == "")
            {
                ok = false;
                errorProvider1.SetError(dtpFecha, "Ingrese departamento");
            }
            return ok = true;
            

        }
        private void BorrarMensaje()
        {
            errorProvider1.SetError(txtnombre, "");
            errorProvider1.SetError(cboxDepartamento, "");
            errorProvider1.SetError(txtSueldo, "");
            errorProvider1.SetError(dtpFecha, "");
            errorProvider1.SetError(txtxape, "");
            errorProvider1.SetError(txtapeMat, "");

        }
    }
    
}
