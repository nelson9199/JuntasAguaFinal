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
    public partial class Barrio : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Barrio()
        {
            InitializeComponent();

            panelBarrio.Visible = false;
        }

        barrio ObtenerBarrioInsert()
        {
            barrio barrio = new barrio();

            barrio.nombreB = txtNomBarrio.Text;

            return barrio;
        }

        barrio ObtenerBarrioUpdate()
        {
            barrio barrio = new barrio();

            barrio.nombreB = txtNomBarrio.Text;

            barrio.id = Convert.ToInt32(lblId.Text);

            return barrio;
        }


        void Mostrar()
        {
            try
            {
                var barrios = repository.ObtenerBarrios();

                dataGridBarrio.DataSource = barrios;

                dataGridBarrio.Columns[0].DisplayIndex = 8;

                dataGridBarrio.Columns[1].HeaderText = "N";
                dataGridBarrio.Columns[3].HeaderText = "Nombre";
                dataGridBarrio.Columns[4].Visible = false;
                dataGridBarrio.Columns[5].Visible = false;
                dataGridBarrio.Columns[6].Visible = false;
                dataGridBarrio.Columns[7].Visible = false;
                dataGridBarrio.Columns[8].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelBarrio.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = true;
            txtNomBarrio.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelBarrio.Visible = false;
        }

        private void Barrio_LocationChanged(object sender, EventArgs e)
        {

        }

        private void Barrio_Load(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void dataGridBarrio_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioInsert = ObtenerBarrioInsert();

                var isOk = repository.InsertarBarrio(usuarioInsert);

                if (isOk)
                {
                    MessageBox.Show("Barrio Guardado");
                    panelBarrio.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridBarrio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridBarrio.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Barrio?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOK = repository.EliminarBarrio(Convert.ToInt32(dataGridBarrio.SelectedCells[1].Value));

                        if (isOK)
                        {
                            MessageBox.Show("Barrio Eliminado");
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

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioUpdate = ObtenerBarrioUpdate();

                var isOk = repository.ModificarBarrio(usuarioUpdate);

                if (isOk)
                {
                    MessageBox.Show("Barrio Modificado");
                    panelBarrio.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridBarrio_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelBarrio.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            lblId.Text = dataGridBarrio.SelectedCells[1].Value?.ToString();
            txtNomBarrio.Text = dataGridBarrio.SelectedCells[2].Value?.ToString();
        }
    }
}
