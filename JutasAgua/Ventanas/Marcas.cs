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
    public partial class Marcas : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Marcas()
        {
            InitializeComponent();

            bunifuShadowPanel1.Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
            txtNombBarrio.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = false;
        }

        private marca ObtenerMarca()
        {
            marca marca = new marca();

            marca.nombre = txtNombBarrio.Text;

            return marca;
        }

        private marca ObtenerMarcaUpdate()
        {
            marca marca = new marca();

            marca.id = Convert.ToInt32(lblId.Text);
            marca.nombre = txtNombBarrio.Text;

            return marca;
        }

        void Mostar()
        {
            try
            {
                var marcas = repository.ObtenerMarcas();

                dataGirdMarca.DataSource = marcas;

                dataGirdMarca.Columns[0].DisplayIndex = 4;

                dataGirdMarca.Columns[1].HeaderText = "N";
                dataGirdMarca.Columns[2].HeaderText = "Nombre";
                dataGirdMarca.Columns[3].Visible = false;
                dataGirdMarca.Columns[4].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Marcas_Load(object sender, EventArgs e)
        {
            Mostar();
        }

        private void dataGirdMarca_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var marca = ObtenerMarca();

                var isOk = repository.InsertarMarca(marca);

                if (isOk)
                {
                    MessageBox.Show("Marca Guardada");
                    bunifuShadowPanel1.Visible = false;
                    Mostar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var marca = ObtenerMarcaUpdate();

                var isOk = repository.ModificarMarca(marca);

                if (isOk)
                {
                    MessageBox.Show("Marca Actualizada");
                    bunifuShadowPanel1.Visible = false;
                    Mostar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGirdMarca_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGuardar.Enabled = false;
            bunifuShadowPanel1.Visible = true;
            btnGuardarCambios.Enabled = true;
            txtNombBarrio.Text = dataGirdMarca.SelectedCells[2].Value?.ToString();
            lblId.Text = dataGirdMarca.SelectedCells[1].Value?.ToString();
        }

        private void dataGirdMarca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGirdMarca.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar esta Marca?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isEliminated = repository.EliminarMarca(Convert.ToInt32(dataGirdMarca.SelectedCells[1].Value));

                        if (isEliminated)
                        {
                            MessageBox.Show("Marca eliminada");
                            Mostar();
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
