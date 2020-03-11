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
    public partial class Medidores : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        int numCli;
        string nombCli;

        public Medidores(int numCli = 0, string nombCli = "")
        {
            InitializeComponent();

            panelMedidior.Visible = false;

            this.numCli = numCli;
            this.nombCli = nombCli;

            if (this.numCli > 0)
            {
                Mostrar();
                lblMedidorUser.Text = nombCli;
            }
        }

        medidor ObtenarDatosInsert()
        {
            medidor medidor = new medidor();

            medidor.barrio_id = Convert.ToInt32(lblIdBarrio.Text);
            medidor.cliente_id = numCli;
            medidor.fecha_ingreso = dateFecIngreso.Value;
            medidor.fecha_retiro = dateFecRetiro.Value;
            medidor.lectura_inicial = txtLecturaInicial.Text;
            medidor.marca_id = Convert.ToInt32(lblIdMarca.Text);
            medidor.nombreM = txtNomMedi.Text;
            medidor.serie = txtSerie.Text;
            medidor.tarifa_id = Convert.ToInt32(lblIdTarifa.Text);

            if (dropEstado.Text == "ACTIVO")
            {
                medidor.estado = "A";

            }
            else if (dropEstado.Text == "INACTIVO")
            {
                medidor.estado = "I";
            }


            return medidor;
        }

        medidor ObtenarDatosUpdate()
        {
            medidor medidor = new medidor();

            medidor.id = Convert.ToInt32(lblId.Text);
            medidor.barrio_id = Convert.ToInt32(lblIdBarrio.Text);
            medidor.cliente_id = numCli;
            if (dropEstado.Text == "ACTIVO")
            {
                medidor.estado = "A";

            }
            else if (dropEstado.Text == "INACTIVO")
            {
                medidor.estado = "I";
            }

            medidor.fecha_ingreso = dateFecIngreso.Value;
            medidor.fecha_retiro = dateFecRetiro.Value;
            medidor.lectura_inicial = txtLecturaInicial.Text;
            medidor.marca_id = Convert.ToInt32(lblIdMarca.Text);
            medidor.nombreM = txtNomMedi.Text;
            medidor.serie = txtSerie.Text;
            medidor.tarifa_id = Convert.ToInt32(lblIdTarifa.Text);

            return medidor;
        }

        private void Mostrar()
        {
            var medidores = repository.ObtenerMedidioresPorId(this.numCli);


            if (medidores.Count > 0)
            {
                foreach (var medidor in medidores)
                {
                    if (medidor.estado == "A")
                    {
                        medidor.estado = "Activo";
                    }
                    else if (medidor.estado == "I")
                    {
                        medidor.estado = "Inactivo";
                    }

                }

                dataGridMedidores.DataSource = medidores;

                dataGridMedidores.Columns[0].DisplayIndex = 13;

                dataGridMedidores.Columns[1].HeaderText = "N";
                dataGridMedidores.Columns[2].HeaderText = "Nombre";
                dataGridMedidores.Columns[3].HeaderText = "Barrio";
                dataGridMedidores.Columns[4].HeaderText = "Tarifa";
                dataGridMedidores.Columns[5].HeaderText = "Serie";
                dataGridMedidores.Columns[6].HeaderText = "Lectura Inicial";
                dataGridMedidores.Columns[7].HeaderText = "Estado";
                dataGridMedidores.Columns[8].Visible = false;
                dataGridMedidores.Columns[9].Visible = false;
                dataGridMedidores.Columns[10].Visible = false;
                dataGridMedidores.Columns[11].Visible = false;
                dataGridMedidores.Columns[12].Visible = false;
                dataGridMedidores.Columns[13].Visible = false;

            }

        }


        private void dataGridMedidores_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void Medidores_Load_1(object sender, EventArgs e)
        {
            try
            {
                var barrios = repository.ObtenerBarrios();

                foreach (var barrio in barrios)
                {
                    dropBarrio.Items.Add(barrio.nombreB);
                }

                var tarifas = repository.ObtenerTarifas();

                foreach (var tarifa in tarifas)
                {
                    dropTarifa.Items.Add(tarifa.nombreT);
                }


                var marcas = repository.ObtenerMarcas();

                foreach (var marca in marcas)
                {
                    dropMarca.Items.Add(marca.nombre);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelMedidior.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
            txtBuscar.Text = "";
            txtLecturaInicial.Text = "";
            txtNomMedi.Text = "";
            txtSerie.Text = "";

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelMedidior.Visible = false;
        }

        private async void dropBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropBarrio, "");
            if (dropBarrio.SelectedItem.ToString() == "Seleccione...")
            {
                return;
            }
            try
            {
                var barrio = await repository.ObtenerBarrioPorNombre(dropBarrio.SelectedItem.ToString());

                lblIdBarrio.Text = barrio.id.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void dropTarifa_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropTarifa, "");
            if (dropTarifa.SelectedItem.ToString() == "Seleccione...")
            {
                return;
            }
            try
            {
                var tarifa = await repository.ObtenerTarifaPorNombre(dropTarifa.SelectedItem.ToString());

                lblIdTarifa.Text = tarifa.id.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void dropMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropMarca, "");

            if (dropMarca.SelectedItem.ToString() == "Seleccione...")
            {
                return;
            }
            try
            {
                var marca = await repository.ObtenerMarcaPorNombre(dropMarca.SelectedItem.ToString());

                lblIdMarca.Text = marca.id.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dropMarca.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropMarca, "Debe seleccionar una Marca");
            }
            if (dropTarifa.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropTarifa, "Debe seleccionar una Tarifa");
            }
            if (dropBarrio.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropBarrio, "Debe seleccionar un Barrio");
            }
            if (dropMarca.Text == "SELECCIONE..." || dropTarifa.Text == "SELECCIONE..." || dropBarrio.Text == "SELECCIONE...")
            {
                return;
            }

            try
            {
                var medidor = ObtenarDatosInsert();

                var idOk = repository.InsertarMedidor(medidor);

                if (idOk)
                {
                    MessageBox.Show("Medidor Guardado");
                    panelMedidior.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridMedidores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelMedidior.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            lblId.Text = dataGridMedidores.SelectedCells[1].Value?.ToString();
            lblIdBarrio.Text = dataGridMedidores.SelectedCells[8].Value?.ToString();
            lblIdTarifa.Text = dataGridMedidores.SelectedCells[10].Value?.ToString();
            lblIdMarca.Text = dataGridMedidores.SelectedCells[9].Value?.ToString();
            txtNomMedi.Text = dataGridMedidores.SelectedCells[2].Value?.ToString();
            txtSerie.Text = dataGridMedidores.SelectedCells[5].Value?.ToString();
            dropEstado.Text = dataGridMedidores.SelectedCells[7].Value?.ToString();
            dateFecIngreso.Value = Convert.ToDateTime(dataGridMedidores.SelectedCells[11].Value);
            dateFecRetiro.Value = Convert.ToDateTime(dataGridMedidores.SelectedCells[12].Value);
            dropBarrio.Text = dataGridMedidores.SelectedCells[3].Value?.ToString();
            dropTarifa.Text = dataGridMedidores.SelectedCells[4].Value?.ToString();
            dropMarca.Text = dataGridMedidores.SelectedCells[13].Value?.ToString();
            txtLecturaInicial.Text = dataGridMedidores.SelectedCells[6].Value?.ToString();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (dropMarca.SelectedItem?.ToString() == "Seleccione...")
            {
                errorProvider1.SetError(dropMarca, "Debe seleccionar una Marca");
            }
            if (dropTarifa.SelectedItem?.ToString() == "Seleccione...")
            {
                errorProvider1.SetError(dropTarifa, "Debe seleccionar una Tarifa");
            }
            if (dropBarrio.SelectedItem?.ToString() == "Seleccione...")
            {
                errorProvider1.SetError(dropBarrio, "Debe seleccionar un Barrio");
            }
            if (dropMarca.SelectedItem?.ToString() == "Seleccione..." || dropTarifa.SelectedItem?.ToString() == "Seleccione..." || dropBarrio.SelectedItem?.ToString() == "Seleccione...")
            {
                return;
            }

            try
            {
                var medidor = ObtenarDatosUpdate();

                var idOk = repository.ActualizarMedidior(medidor);

                if (idOk)
                {
                    MessageBox.Show("Medidor Actualizado");
                    panelMedidior.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dropEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridMedidores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridMedidores.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Medidor?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOk = repository.EliminarMedidor(Convert.ToInt32(dataGridMedidores.SelectedCells[1].Value));

                        if (isOk)
                        {
                            MessageBox.Show("Medidor Eliminado con Éxito");
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
