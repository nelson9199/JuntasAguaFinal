using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JutasAgua.Ventanas
{
    public partial class Clientes : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Clientes()
        {
            InitializeComponent();
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelClientes.Visible = true;
            btnGuardarCambios.Enabled = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelClientes.Visible = false;

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            Mostrar();
            txtBuscar.Enabled = false;
            panelClientes.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mostrar()
        {
            try
            {

                var empresa = repository.OtenerClientes();

                dataGridClientes.DataSource = empresa;

                dataGridClientes.Columns[1].Visible = false;
                dataGridClientes.Columns[2].HeaderText = "Nombre";
                dataGridClientes.Columns[3].HeaderText = "Apellido";
                dataGridClientes.Columns[4].HeaderText = "Fecha Nacimiento"; 
                dataGridClientes.Columns[5].HeaderText = "Tipo Identificación";
                dataGridClientes.Columns[6].HeaderText = "Número Identificacíon";
                dataGridClientes.Columns[7].HeaderText = "Dirección";
                dataGridClientes.Columns[8].HeaderText = "Email";
                dataGridClientes.Columns[9].Visible = false;
                dataGridClientes.Columns[10].HeaderText = "Telefono Fijo";
                dataGridClientes.Columns[11].HeaderText = "Telefono Celular";
                dataGridClientes.Columns[12].HeaderText = "Sexo";
                dataGridClientes.Columns[13].HeaderText = "Estado";
                dataGridClientes.Columns[14].Visible = false;
                dataGridClientes.Columns[15].Visible = false;
                dataGridClientes.Columns[16].Visible = false;
                dataGridClientes.Columns[17].Visible = false;
                dataGridClientes.Columns[18].Visible = false;
                dataGridClientes.Columns[19].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridClientes_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
