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
    public partial class Unidades_Medida : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Unidades_Medida()
        {
            InitializeComponent();

        }

        private void Unidades_Medida_Load(object sender, EventArgs e)
        {
            panelMedida.Visible = false;
            Mostrar();
        }

        private async void Mostrar()
        {
            try
            {
                var medidas = await repository.ObtenerMedida();

                dataGridMedida.DataSource = medidas;

                dataGridMedida.Columns[0].DisplayIndex = 4;

                dataGridMedida.Columns[1].HeaderText = "N";
                dataGridMedida.Columns[2].HeaderText = "Nombre";
                dataGridMedida.Columns[3].HeaderText = "Prefijo";
                dataGridMedida.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;

            panelMedida.Visible = true;
            txtPrefijo.Text = "";
            txtNombre.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelMedida.Visible = false;
        }

        unidad_medida ObntenerDatosInsert()
        {
            unidad_medida medida = new unidad_medida();

            medida.nombre = txtNombre.Text;
            medida.prefijo = txtPrefijo.Text;

            return medida;
        }
        unidad_medida ObntenerDatosUpdate()
        {
            unidad_medida medida = new unidad_medida();
            medida.id = Convert.ToInt32(lblId.Text);
            medida.nombre = txtNombre.Text;
            medida.prefijo = txtPrefijo.Text;

            return medida;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "Debe llenar este campo");

                return;
            }

            try
            {
                var medida = ObntenerDatosInsert();

                var isOk = await repository.InsertarMedida(medida);

                if (isOk)
                {
                    MessageBox.Show("Unidad de Medida Guardada");
                    panelMedida.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNombre, "");
        }

        private void dataGridMedida_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private async void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "Debe llenar este campo");

                return;
            }

            try
            {
                var medida = ObntenerDatosUpdate();

                var isOk = await repository.ModificarMedida(medida);

                if (isOk)
                {
                    MessageBox.Show("Unidad de Medida Actualizada");
                    panelMedida.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridMedida_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridMedida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelMedida.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            lblId.Text = dataGridMedida.SelectedCells[1].Value?.ToString();
            txtNombre.Text = dataGridMedida.SelectedCells[2].Value?.ToString();
            txtPrefijo.Text = dataGridMedida.SelectedCells[3].Value?.ToString();
        }

        private async void dataGridMedida_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridMedida.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar esta Unidad de Medida?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOk = await repository.EliminarMedida(Convert.ToInt32(dataGridMedida.SelectedCells[1].Value));

                        if (isOk)
                        {
                            MessageBox.Show("Unidad de Medida Eliminada");
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
    }
}
