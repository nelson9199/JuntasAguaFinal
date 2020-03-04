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

        bool buscarPorNombre = false;
        bool buscarPorCedula = false;
        bool buscarPorDireccion = false;



        public Clientes()
        {
            InitializeComponent();
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtApellidos.Text = "";
            txtBuscar.Text = "";
            txtCelular.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtNombCli.Text = "";
            txtNumIden.Text = "";
            txtTeleFijo.Text = "";
            dropBuscarPor.Text = "";
            dropEstado.Text = "";
            dropTipoIdenti.Text = "";
            dropSexo.Text = "";

            panelClientes.Visible = true;
            btnGuardarCambios.Enabled = false;
            btnGuardar.Enabled = true;
            btnVerMedidor.Enabled = false;
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

        List<cliente> MostrarDatosOk(List<cliente> clientes)
        {
            for (int i = 0; i < clientes.Count; i++)
            {
                switch (clientes[i].tipo_identificacion)
                {
                    case "C":
                        clientes[i].tipo_identificacion = "CÈDULA";
                        break;
                    case "P":
                        clientes[i].tipo_identificacion = "PASAPORTE";
                        break;
                    case "R":
                        clientes[i].tipo_identificacion = "RUC";
                        break;

                }

                switch (clientes[i].sexo)
                {
                    case "F":
                        clientes[i].sexo = "Femenino";
                        break;
                    case "M":
                        clientes[i].sexo = "Masculino";
                        break;
                }

                switch (clientes[i].estado)
                {
                    case "A":
                        clientes[i].estado = "Activo";
                        break;
                    case "I":
                        clientes[i].sexo = "Inactivo";
                        break;
                }
            }

            return clientes;
        }

        private void Mostrar()
        {
            try
            {

                var clientes = repository.OtenerClientes();

                dataGridClientes.DataSource = MostrarDatosOk(clientes); ;

                dataGridClientes.Columns[0].DisplayIndex = 19;

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
            medidores = new Medidores(Convert.ToInt32(lblId.Text), txtNombCli.Text);

            medidores.ShowDialog();

        }

        private void dataGridClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelClientes.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;
            btnVerMedidor.Enabled = true;
            try
            {
                lblId.Text = dataGridClientes.SelectedCells[1].Value?.ToString();
                txtNombCli.Text = dataGridClientes.SelectedCells[2].Value?.ToString();
                txtApellidos.Text = dataGridClientes.SelectedCells[3].Value?.ToString();
                dateFecNac.Value = Convert.ToDateTime(dataGridClientes.SelectedCells[4].Value);
                dropTipoIdenti.Text = dataGridClientes.SelectedCells[5].Value?.ToString();
                txtNumIden.Text = dataGridClientes.SelectedCells[6].Value?.ToString();
                txtDireccion.Text = dataGridClientes.SelectedCells[7]?.Value.ToString();
                txtEmail.Text = dataGridClientes.SelectedCells[8]?.Value.ToString();

                lblFoto.Text = dataGridClientes.SelectedCells[9].Value != null ? lblFoto.Text = dataGridClientes.SelectedCells[9].Value.ToString() : "";
                txtTeleFijo.Text = dataGridClientes.SelectedCells[10].Value?.ToString();
                txtCelular.Text = dataGridClientes.SelectedCells[11].Value?.ToString();
                dropSexo.Text = dataGridClientes.SelectedCells[12].Value?.ToString();
                dropEstado.Text = dataGridClientes.SelectedCells[13].Value?.ToString();
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

            cliente.fecha_nacimiento = dateFecNac.Value;
            cliente.foto = lblFoto.Text;
            cliente.id = Convert.ToInt32(lblId.Text);
            cliente.nombre = txtNombCli.Text;
            cliente.numero_identificacion = txtNumIden.Text;
            cliente.telefono_celular = txtCelular.Text;
            cliente.telefono_fijo = txtTeleFijo.Text;
            if (dropTipoIdenti.Items.Contains("CÉDULA"))
            {
                cliente.tipo_identificacion = "C";
            }
            else if (dropTipoIdenti.Items.Contains("PASAPORTE"))
            {
                cliente.tipo_identificacion = "P";
            }
            else if (dropTipoIdenti.Items.Contains("RUC"))
            {
                cliente.tipo_identificacion = "R";
            }

            if (dropEstado.Items.Contains("ACTIVO"))
            {
                cliente.estado = "A";
            }
            else if (dropEstado.Items.Contains("INACTIVO"))
            {
                cliente.estado = "I";
            }


            if (dropSexo.Items.Contains("MASCULINO"))
            {
                cliente.sexo = "M";
            }
            else if (dropSexo.Items.Contains("FEMENINO"))
            {
                cliente.sexo = "F";
            }



            return cliente;

        }

        private cliente ObtenerDatosDelGridInsert()
        {
            cliente cliente = new cliente();

            cliente.apellido = txtApellidos.Text;
            cliente.direccion = txtDireccion.Text;
            cliente.email = txtEmail.Text;
            cliente.estado = dropEstado.Text;
            cliente.fecha_nacimiento = dateFecNac.Value;
            cliente.foto = lblFoto.Text;
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

            //Validaciones de formulario
            if (txtNombCli.Text == "")
            {
                errorProvider1.SetError(txtNombCli, "Debe llenar este campo");
            }
            if (txtApellidos.Text == "")
            {
                errorProvider1.SetError(txtApellidos, "Debe llenar este campo");
            }
            if (txtNumIden.Text == "")
            {
                errorProvider1.SetError(txtNumIden, "Debe llenar este campo");
            }
            if (dropTipoIdenti.Text == null)
            {
                errorProvider1.SetError(dropTipoIdenti, "Debe escoger un tipo de Identificacìon");

            }
            if (txtNombCli.Text == "" || txtApellidos.Text == "" || txtNumIden.Text == "" || dropTipoIdenti.Text == null)
            {
                return;
            }
            else
            {
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

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validaciones de formulario
            if (txtNombCli.Text == "")
            {
                errorProvider1.SetError(txtNombCli, "Debe llenar este campo");
            }
            if (txtApellidos.Text == "")
            {
                errorProvider1.SetError(txtApellidos, "Debe llenar este campo");
            }
            if (txtNumIden.Text == "")
            {
                errorProvider1.SetError(txtNumIden, "Debe llenar este campo");
            }
            if (dropTipoIdenti.Text == null)
            {
                errorProvider1.SetError(dropTipoIdenti, "Debe escoger un tipo de Identificacìon");

            }
            if (txtNombCli.Text == "" || txtApellidos.Text == "" || txtNumIden.Text == "" || dropTipoIdenti.Text == null)
            {
                return;
            }
            else
            {
                try
                {
                    var cliente = ObtenerDatosDelGridInsert();
                    List<usuario> usuario = repository.hacerLogin(Login.user, Login.password);
                    cliente.cci = usuario[0].id;
                    cliente.ccd = DateTime.Now;

                    switch (cliente.tipo_identificacion)
                    {
                        case "CÉDULA":
                            cliente.tipo_identificacion = "C";
                            break;
                        case "PASAPORTE":
                            cliente.tipo_identificacion = "P";
                            break;
                        case "RUC":
                            cliente.tipo_identificacion = "R";
                            break;
                    }

                    switch (cliente.sexo)
                    {
                        case "FEMENINO":
                            cliente.sexo = "F";
                            break;
                        case "MASCULINO":
                            cliente.sexo = "M";
                            break;
                    }

                    switch (cliente.estado)
                    {
                        case "ACTIVO":
                            cliente.estado = "A";
                            break;
                        case "INACTIVO":
                            cliente.estado = "I";
                            break;
                    }

                    var isOk = repository.InsertarCliente(cliente);

                    if (isOk)
                    {
                        MessageBox.Show("Cliente Guardado con Èxito");
                        panelClientes.Visible = false;
                        Mostrar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron guardar los datos");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void txtNombCli_TextChange(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNombCli, "");
        }

        private void txtApellidos_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtApellidos, "");
        }

        private void dropTipoIdenti_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropTipoIdenti, "");
        }

        private void txtNumIden_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNumIden, "");
        }

        private void dataGridClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridClientes.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Cliente?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        List<ConsultaMedidores> hayMedidor = repository.ObtenerMedidioresPorId(Convert.ToInt32(dataGridClientes.SelectedCells[1].Value));

                        if (hayMedidor.Count > 0)
                        {
                            var medidorBorrado = repository.EliminarMedidor(Convert.ToInt32(dataGridClientes.SelectedCells[1].Value));

                            if (medidorBorrado)
                            {
                                var isOk = repository.EliminarCliente(Convert.ToInt32(dataGridClientes.SelectedCells[1].Value));

                                if (isOk)
                                {
                                    MessageBox.Show("Cliente Eliminado con Éxito");
                                    Mostrar();
                                }
                                else
                                {
                                    MessageBox.Show("No se puede eliminar");
                                }

                            }
                        }
                        else
                        {
                            var isOk = repository.EliminarCliente(Convert.ToInt32(dataGridClientes.SelectedCells[1].Value));

                            if (isOk)
                            {
                                MessageBox.Show("Cliente Eliminado con Éxito");
                                Mostrar();
                            }
                            else
                            {
                                MessageBox.Show("No se puede eliminar");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    return;
                }
            }


        }

        private void dropBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropBuscarPor.SelectedItem.ToString() == "Nombre")
            {
                txtBuscar.Enabled = true;
                buscarPorNombre = true;
                buscarPorCedula = false;
                buscarPorDireccion = false;

            }
            else if (dropBuscarPor.SelectedItem.ToString() == "Cedula")
            {
                txtBuscar.Enabled = true;
                buscarPorNombre = false;
                buscarPorCedula = true;
                buscarPorDireccion = false;
            }
            else if (dropBuscarPor.SelectedItem.ToString() == "Direccion")
            {
                txtBuscar.Enabled = true;
                buscarPorNombre = false;
                buscarPorCedula = false;
                buscarPorDireccion = true;
            }
            else if (dropBuscarPor.SelectedItem.ToString() == "")
            {
                txtBuscar.Enabled = false;
                buscarPorNombre = false;
                buscarPorCedula = false;
                buscarPorDireccion = false;
                txtBuscar.Text = "";
                Mostrar();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (buscarPorNombre == true)
            {
                try
                {
                    var clientes = repository.BuscarClientePorNombre(txtBuscar.Text);

                    dataGridClientes.DataSource = MostrarDatosOk(clientes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (buscarPorCedula == true)
            {
                try
                {
                    var clientes = repository.BuscarClientePorCedula(txtBuscar.Text);

                    dataGridClientes.DataSource = MostrarDatosOk(clientes); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (buscarPorDireccion == true)
            {
                try
                {
                    var clientes = repository.BuscarClientePorDireccion(txtBuscar.Text);

                    dataGridClientes.DataSource = MostrarDatosOk(clientes); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }


}
