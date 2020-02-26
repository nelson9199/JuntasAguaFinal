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
        Medidores medidores = null;

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
            btnVerMedidor.Enabled = false;
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

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            medidores = new Medidores(Convert.ToInt32(lblId.Text));

            medidores.ShowDialog();

        }

        private void dataGridClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelClientes.Visible = true;
            btnVerMedidor.Enabled = true;
            try
            {
                lblId.Text = dataGridClientes.SelectedCells[1].Value.ToString();
                txtNombCli.Text = dataGridClientes.SelectedCells[2].Value.ToString();
                txtApellidos.Text = dataGridClientes.SelectedCells[3].Value.ToString();
                dateFecNac.Value = Convert.ToDateTime(dataGridClientes.SelectedCells[4].Value);
                dropTipoIdenti.Text = dataGridClientes.SelectedCells[5].Value.ToString();
                txtNumIden.Text = dataGridClientes.SelectedCells[6].Value.ToString();
                txtDireccion.Text = dataGridClientes.SelectedCells[7].Value.ToString();
                txtEmail.Text = dataGridClientes.SelectedCells[8].Value.ToString();

                lblFoto.Text = dataGridClientes.SelectedCells[9].Value != null ? lblFoto.Text = dataGridClientes.SelectedCells[9].Value.ToString() : "";
                txtTeleFijo.Text = dataGridClientes.SelectedCells[10].Value.ToString();
                txtCelular.Text = dataGridClientes.SelectedCells[11].Value.ToString();
                dropSexo.Text = dataGridClientes.SelectedCells[12].Value.ToString();
                dropEstado.Text = dataGridClientes.SelectedCells[13].Value.ToString();
                if (dataGridClientes.SelectedCells[14].Value != null)
                {
                    lblcci.Text = dataGridClientes.SelectedCells[14].Value.ToString();
                }
                else
                {
                    lblcci.Text = "";
                }

                lblccd.Text = dataGridClientes.SelectedCells[15].Value != null ? dataGridClientes.SelectedCells[15].Value.ToString() : "";
                lblcwi.Text = dataGridClientes.SelectedCells[16].Value != null ? dataGridClientes.SelectedCells[16].Value.ToString() : ""; ;
                lblcwd.Text = dataGridClientes.SelectedCells[17].Value != null ? dataGridClientes.SelectedCells[17].Value.ToString() : "";

            }
            catch
            {

            }
        }

        private cliente ObtenerDatosDelGrid()
        {
            cliente cliente = new cliente();

            cliente.apellido = txtApellidos.Text;
            cliente.direccion = txtDireccion.Text;
            cliente.email = txtEmail.Text;
            cliente.estado = dropEstado.Text;
            cliente.fecha_nacimiento = dateFecNac.Value;
            cliente.foto = lblFoto.Text;
            cliente.id = Convert.ToInt32(lblId.Text);
            cliente.nombre = txtNombCli.Text;
            cliente.numero_identificacion = txtNumIden.Text;
            cliente.sexo = dropSexo.Text;
            cliente.telefono_celular = txtCelular.Text;
            cliente.telefono_fijo = txtTeleFijo.Text;
            cliente.tipo_identificacion = dropTipoIdenti.Text;

            return cliente;

        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            var updatedCliente = ObtenerDatosDelGrid();

            List<usuario> usuario = repository.hacerLogin(Login.user, Login.password);
            updatedCliente.cwi = usuario[0].id;
            updatedCliente.cwd = DateTime.Now;

            try
            {
                var isOk = repository.ActualizarCliente(updatedCliente);

                if (isOk)
                {
                    MessageBox.Show("Datos Modificados con Éxito");
                    panelClientes.Visible = false;
                    Mostrar();
                }
                else
                {
                    MessageBox.Show("No se han podido modificar datos");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
