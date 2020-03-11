using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelos;

namespace JutasAgua.Ventanas
{
    public partial class Costp_Ta : Form
    {
        readonly RepositoryAgua repository = new RepositoryAgua();

        private int idTarifa;
        public Costp_Ta(int idTarifa)
        {
            this.idTarifa = idTarifa;
            InitializeComponent();
        }

        async void Mostrar()
        {
            var costos = await repository.ObtenerCostos(idTarifa);

            dataGridCosto.DataSource = costos;

            dataGridCosto.Columns[0].DisplayIndex = 14;

            dataGridCosto.Columns[1].HeaderText = "N";
            dataGridCosto.Columns[2].Visible = false;
            dataGridCosto.Columns[3].HeaderText = "Orden";
            dataGridCosto.Columns[4].HeaderText = "Nombre";
            dataGridCosto.Columns[5].HeaderText = "Desde(m3)";
            dataGridCosto.Columns[6].HeaderText = "Hasta(m3)";
            dataGridCosto.Columns[7].HeaderText = "Valor";
            dataGridCosto.Columns[8].HeaderText = "Base";
            dataGridCosto.Columns[9].HeaderText = "Multa";
            dataGridCosto.Columns[10].Visible = false;
            dataGridCosto.Columns[11].Visible = false;
            dataGridCosto.Columns[12].Visible = false;
            dataGridCosto.Columns[13].Visible = false;
        }

        private void Costp_Ta_Load(object sender, EventArgs e)
        {
            panelCosto.Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelCosto.Visible = true;
            btnGuardarCambios.Enabled = false;
            btnGuardar.Enabled = true;

            txtDesde.Text = "";
            txtHasta.Text = "";
            txtNombTa.Text = "";
            txtOrden.Text = "";
            txtValor.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelCosto.Visible = false;
        }


    }
}
