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
    public partial class Multas : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Multas()
        {
            InitializeComponent();
        }

        private void Multas_Load(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = false;
            Mostrar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
            txtEspera.Text = "";
            txtPorcen.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = false;
        }

        multa_retraso ObtenerDatosInsert()
        {
            multa_retraso multa = new multa_retraso();

            multa.porcentaje_pago = Convert.ToDecimal(txtPorcen.Text);
            multa.tiempo_espera = Convert.ToInt32(txtEspera.Text);

            return multa;
        }
        multa_retraso ObtenerDatosUpdate()
        {
            multa_retraso multa = new multa_retraso();

            multa.id = Convert.ToInt32(lblId.Text);
            multa.porcentaje_pago = Convert.ToDecimal(txtPorcen.Text);
            multa.tiempo_espera = Convert.ToInt32(txtEspera.Text);

            return multa;
        }


        public async void Mostrar()
        {
            try
            {
                var multas = await repository.ObtenerMultas();

                dataGridMultas.DataSource = multas;

                dataGridMultas.Columns[0].DisplayIndex = 6;

                dataGridMultas.Columns[1].HeaderText = "N";
                dataGridMultas.Columns[2].HeaderText = "Tiempo espera en meses";
                dataGridMultas.Columns[3].HeaderText = "Porcentaje Multas";
                dataGridMultas.Columns[4].Visible = false;
                dataGridMultas.Columns[5].Visible = false;
                dataGridMultas.Columns[6].Visible = false;
                dataGridMultas.Columns[7].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtEspera.Text == "")
            {
                errorProvider1.SetError(txtEspera, "");
            }
            if (txtPorcen.Text == "")
            {
                errorProvider1.SetError(txtPorcen, "");
            }

            if (txtEspera.Text == "" || txtPorcen.Text == "")
            {
                return;
            }

            try
            {
                var multa = ObtenerDatosInsert();

                var isOK = repository.InsertarMultas(multa);

                if (isOK)
                {
                    MessageBox.Show("Multa Guardada");
                    bunifuShadowPanel1.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtEspera.Text == "")
            {
                errorProvider1.SetError(txtEspera, "");
            }
            if (txtPorcen.Text == "")
            {
                errorProvider1.SetError(txtPorcen, "");
            }

            if (txtEspera.Text == "" || txtPorcen.Text == "")
            {
                return;
            }

            //try
            //{
            var multa = ObtenerDatosUpdate();

            var isOK = repository.ModificarMulta(multa);

            if (isOK)
            {
                MessageBox.Show("Multa Actualizada");
                bunifuShadowPanel1.Visible = false;
                Mostrar();
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dataGridMultas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuShadowPanel1.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            lblId.Text = dataGridMultas.SelectedCells[1].Value?.ToString();
            txtEspera.Text = dataGridMultas.SelectedCells[2].Value?.ToString();
            txtPorcen.Text = dataGridMultas.SelectedCells[3].Value?.ToString();
        }

        private void dataGridMultas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridMultas.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar esta Multa?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOk = repository.EliminarMulta(Convert.ToInt32(dataGridMultas.SelectedCells[1].Value));

                        if (isOk)
                        {
                            MessageBox.Show("Multa Eliminada");
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
