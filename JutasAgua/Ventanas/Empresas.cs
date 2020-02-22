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
    public partial class Empresas : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Empresas()
        {
            InitializeComponent();
        }

        private void Mostrar()
        {
            try
            {

                var empresa = repository.obtenerEmpresas();

                dataGridEmpresas.DataSource = empresa;

                dataGridEmpresas.Columns[0].Visible = false;
                dataGridEmpresas.Columns[1].HeaderText = "Nombre";
                dataGridEmpresas.Columns[2].HeaderText = "Email";
                dataGridEmpresas.Columns[3].HeaderText = "Dirección";
                dataGridEmpresas.Columns[4].Visible = false;
                dataGridEmpresas.Columns[5].HeaderText = "Teléfono Fijo";
                dataGridEmpresas.Columns[6].HeaderText = "Telefono Móvil";
                dataGridEmpresas.Columns[7].HeaderText = "Recaudador";
                dataGridEmpresas.Columns[8].HeaderText = "RUC";
                dataGridEmpresas.Columns[9].Visible = false;
                dataGridEmpresas.Columns[10].Visible = false;
                dataGridEmpresas.Columns[11].Visible = false;
                dataGridEmpresas.Columns[12].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Empresas_Load(object sender, EventArgs e)
        {
            Mostrar();
            panelEmpresa.Visible = false;
        }
    }
}
