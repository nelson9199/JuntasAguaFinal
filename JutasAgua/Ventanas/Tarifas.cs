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
    public partial class Tarifas : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Tarifas()
        {
            InitializeComponent();
        }

        private void Tarifas_Load(object sender, EventArgs e)
        {
            panelTarifa.Visible = false;
            Mostrar();
        }

        void Mostrar()
        {
            try
            {
                var tarifas = repository.ObtenerTarifas();

                dataGridTarifa.DataSource = tarifas;

                dataGridTarifa.Columns[0].DisplayIndex = 11;

                dataGridTarifa.Columns[1].HeaderText = "N";
                dataGridTarifa.Columns[2].HeaderText = "Orden";
                dataGridTarifa.Columns[3].HeaderText = "Nombre";
                dataGridTarifa.Columns[4].HeaderText = "Descripcion";
                dataGridTarifa.Columns[9].HeaderText = "acumulativa";

                dataGridTarifa.Columns[5].Visible = false;
                dataGridTarifa.Columns[6].Visible = false;
                dataGridTarifa.Columns[7].Visible = false;
                dataGridTarifa.Columns[8].Visible = false;
                dataGridTarifa.Columns[10].Visible = false;
                dataGridTarifa.Columns[11].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelTarifa.Visible = true;
            btnGuardarCambios.Enabled = false;
            btnGuardar.Enabled = true;
            txtDescripcion.Text = "";
            txtNombTa.Text = "";
            txtOrden.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelTarifa.Visible = false;
        }

        private void dataGridTarifa_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        tarifa ObtenerTarifaInsert()
        {
            tarifa tarifa = new tarifa();

            tarifa.nombreT = txtNombTa.Text;
            tarifa.acumulativa = dropAcumu.Text;
            tarifa.descripcion = txtDescripcion.Text;
            try
            {
                tarifa.orden = Convert.ToInt32(txtOrden.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese un valor numérico");
            }

            return tarifa;
        }

        tarifa ObtenerTarifaUpdate()
        {
            tarifa tarifa = new tarifa();

            tarifa.id = Convert.ToInt32(lblId.Text);
            tarifa.nombreT = txtNombTa.Text;
            tarifa.acumulativa = dropAcumu.Text;
            tarifa.descripcion = txtDescripcion.Text;
            try
            {
                tarifa.orden = Convert.ToInt32(txtOrden.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese un valor numérico");
            }

            return tarifa;
        }

        private void dataGridTarifa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelTarifa.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            txtDescripcion.Text = dataGridTarifa.SelectedCells[4].Value?.ToString();
            txtNombTa.Text = dataGridTarifa.SelectedCells[3].Value?.ToString();
            txtOrden.Text = dataGridTarifa.SelectedCells[2].Value?.ToString();
            lblId.Text = dataGridTarifa.SelectedCells[1].Value?.ToString();
            dropAcumu.Text = dataGridTarifa.SelectedCells[9].Value?.ToString();
        }

        private void dataGridTarifa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridTarifa.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Cliente?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {

                        var isOK = repository.EliminarTarifa(Convert.ToInt32(dataGridTarifa.SelectedCells[1].Value));

                        if (isOK)
                        {
                            MessageBox.Show("Tarifa Eliminada");
                            panelTarifa.Visible = false;
                            Mostrar();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtOrden.Text == "")
            {
                errorProvider1.SetError(txtOrden, "Debe llenar este campo");

                return;
            }

            try
            {
                var tarifa = ObtenerTarifaInsert();

                var isOK = repository.InsertarTarifa(tarifa);

                if (isOK)
                {
                    MessageBox.Show("Tarifa guardada");
                    panelTarifa.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtOrden_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtOrden, "");
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtOrden.Text == "")
            {
                errorProvider1.SetError(txtOrden, "Debe llenar este campo");

                return;
            }

            try
            {
                var tarifa = ObtenerTarifaUpdate();

                var isOK = repository.ModificarTarifa(tarifa);

                if (isOK)
                {
                    MessageBox.Show("Tarifa Actualizada");
                    panelTarifa.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
