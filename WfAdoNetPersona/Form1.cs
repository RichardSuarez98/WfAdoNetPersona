using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfAdoNetPersona
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cargarDatos();
            this.CargarComboBox();
            txtId.Enabled = false;
            btnInsertar.Text = "Insertar";

        }

        public void CargarComboBox()
        {
            using (DatosPersonalesEntities conn = new DatosPersonalesEntities())
            {
                dynamic lista = conn.pais.ToList();
                ///cmbPais.Text = "<Seleccione su País>";
                cmbPais.DataSource = lista;
                cmbPais.DisplayMember = "nombre";
                cmbPais.ValueMember = "id";
            }
            // comboBox1.Items.Add(lista);

        }

        public void cargarDatos()
        {
            using (DatosPersonalesEntities conn = new DatosPersonalesEntities())
            {
                dataGridView1.DataSource = conn.Personas.Select(x => new {
                    x.Id,
                    x.Nombre,
                    x.Apellido,
                    x.Edad,
                    x.FechaNacimiento,
                    x.Cedula,
                    Pais = x.pais1.nombre
                }).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DatosPersonalesEntities conn = new DatosPersonalesEntities())
            {
                try
                {
                    Personas per = new Personas();
                    per.Nombre = txtNombre.Text;
                    per.Apellido = txtApellido.Text;
                    per.Edad = Convert.ToInt32(txtEdad.Text);
                    per.FechaNacimiento = Convert.ToDateTime(txtNacimiento.Text);
                    per.Cedula = txtCedula.Text;
                    per.pais = (int?)cmbPais.SelectedValue;

                    if (dataGridView1.SelectedRows.Count==1)
                    {
                        int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                        per.Id = id;
                        conn.Entry(per).State=EntityState.Modified;
                        conn.SaveChanges();
                        MessageBox.Show("Exito al Modificar");
                        LimpiarPantalla();
                        cargarDatos();
                    }
                    else
                    {
                        conn.Personas.Add(per);
                        conn.SaveChanges();
                        MessageBox.Show("Exito al Guardar");
                        LimpiarPantalla();
                        cargarDatos();
                    }
                    
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        public void LimpiarPantalla()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtCedula.Clear();
            btnInsertar.Text = "Insertar";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                txtId.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                txtEdad.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                txtNacimiento.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                txtCedula.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                cmbPais.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
                btnInsertar.Text = "Modificar";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
            btnInsertar.Text = "Insertar";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (DatosPersonalesEntities conn = new DatosPersonalesEntities())
            {
                if (dataGridView1.SelectedRows.Count==1)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    var h = conn.Personas.Where(x => x.Id == id).FirstOrDefault();
                    conn.Personas.Remove(h);
                    conn.SaveChanges();
                    MessageBox.Show("Exito al Eliminar");
                    LimpiarPantalla();
                    cargarDatos();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (DatosPersonalesEntities conn = new DatosPersonalesEntities())
            {
                
                    dynamic lis = conn.Personas.Select(x=> new {
                        x.Id,
                        x.Nombre,
                        x.Apellido,
                        x.Edad,
                        x.FechaNacimiento,
                        x.Cedula,
                        Pais = x.pais1.nombre
                    }).Where(x=> x.Nombre==txtBuscarNombre.Text).ToList();
                    dataGridView1.DataSource = lis; 
                
            }

        }



    }
}
