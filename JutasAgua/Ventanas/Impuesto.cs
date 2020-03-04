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
    public partial class Impuesto : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Impuesto()
        {
            InitializeComponent();
        }

        private void dataGirdGrupo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Impuesto_Load(object sender, EventArgs e)
        {
            panelImpuesto.Visible = false;
            Mostrar();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = true;
            panelImpuesto.Visible = true;

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelImpuesto.Visible = false;
        }

        async void Mostrar()
        {

            try
            {
                var impuestos = repository.ObtenerImpuesto();

                dataGridImpuesto.DataSource = await impuestos;

                dataGridImpuesto.Columns[0].DisplayIndex = 4;

                dataGridImpuesto.Columns[1].HeaderText = "N";
                dataGridImpuesto.Columns[2].HeaderText = "Nombre";
                dataGridImpuesto.Columns[3].HeaderText = "Porcentaje";
                dataGridImpuesto.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        impuesto ObtenerDatosInsert()
        {
            impuesto impuesto = new impuesto();

            impuesto.porcentaje = Convert.ToSByte(txtPorcent.Text);
            impuesto.nombre = txtNombre.Text;

            return impuesto;
        }

        impuesto ObtenerDatosUpdate()
        {
            impuesto impuesto = new impuesto();

            impuesto.id = Convert.ToInt32(lblId.Text);
            impuesto.porcentaje = Convert.ToSByte(txtPorcent.Text);
            impuesto.nombre = txtNombre.Text;

            return impuesto;
        }

        private void dataGridImpuesto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelImpuesto.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            lblId.Text = dataGridImpuesto.SelectedCells[1].Value?.ToString();
            txtNombre.Text = dataGridImpuesto.SelectedCells[2].Value?.ToString();
            txtPorcent.Text = dataGridImpuesto.SelectedCells[3].Value?.ToString();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var impuesto = ObtenerDatosInsert();

                var isOk = await repository.InsertarImpuesto(impuesto);

                if (isOk)
                {
                    MessageBox.Show("Impuesto Guardado");
                    panelImpuesto.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var impuesto = ObtenerDatosUpdate();

                var isOk = await repository.ModificarImpuesto(impuesto);

                if (isOk)
                {
                    MessageBox.Show("Impuesto Actualizado");
                    panelImpuesto.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void dataGridImpuesto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridImpuesto.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Impuesto?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOk = await repository.EliminarImpuesto(Convert.ToInt32(dataGridImpuesto.SelectedCells[1].Value));

                        if (isOk)
                        {
                            MessageBox.Show("Impuesto Eliminado");
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

        private void dataGridImpuesto_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
