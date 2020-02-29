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
    public partial class Usuarios : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Usuarios()
        {
            InitializeComponent();
        }

        void Mostrar()
        {
            try
            {
                var usuarios = repository.ObtenerUsuarios();

                dataGridUsuarios.DataSource = usuarios;
                dataGridUsuarios.Columns[0].DisplayIndex = 22;

                dataGridUsuarios.Columns[1].HeaderText = "N";
                dataGridUsuarios.Columns[2].HeaderText = "Apellido";
                dataGridUsuarios.Columns[3].HeaderText = "Nombre";
                dataGridUsuarios.Columns[4].Visible = false;
                dataGridUsuarios.Columns[5].HeaderText = "Número Identificación";
                dataGridUsuarios.Columns[6].HeaderText = "Fecha Nacimiento";
                dataGridUsuarios.Columns[7].HeaderText = "Teléfono Fijo";
                dataGridUsuarios.Columns[8].HeaderText = "Teléfono Celular";
                dataGridUsuarios.Columns[9].HeaderText = "Email";
                dataGridUsuarios.Columns[10].Visible = false;
                dataGridUsuarios.Columns[11].Visible = false;
                dataGridUsuarios.Columns[12].Visible = false;
                dataGridUsuarios.Columns[13].Visible = false;
                dataGridUsuarios.Columns[14].Visible = false;
                dataGridUsuarios.Columns[15].Visible = false;
                dataGridUsuarios.Columns[16].Visible = false;
                dataGridUsuarios.Columns[17].Visible = false;
                dataGridUsuarios.Columns[18].Visible = false;
                dataGridUsuarios.Columns[19].Visible = false;
                dataGridUsuarios.Columns[20].Visible = false;
                dataGridUsuarios.Columns[21].Visible = false;
                dataGridUsuarios.Columns[22].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            panelUsuarios.Visible = false;
            Mostrar();
        }

        private void dataGridUsuarios_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

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
            txtUser.Text = "";
            dropBuscarPor.Text = "";
            dropEstado.Text = "";
            dropSupAdmin.Text = "";
            dropTipoIdenti.Text = "";
            tctClave.Text = "";
            panelUsuarios.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
            txtApellidos.Enabled = true;
            txtNombCli.Enabled = true;
            txtUser.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelUsuarios.Visible = false;
        }

        private usuario ObtenerDatosDelGridInsert()
        {
            usuario usuario = new usuario();

            usuario.apellido = txtApellidos.Text;
            usuario.direccion = txtDireccion.Text;
            usuario.email = txtEmail.Text;
            usuario.estado = dropEstado.Text;
            usuario.fecha_nacimiento = dateFecNac.Value;
            usuario.foto = lblFoto.Text;
            usuario.nombre = txtNombCli.Text;
            usuario.numero_identificacion = txtNumIden.Text;
            usuario.telefono_movil = txtCelular.Text;
            usuario.telefono_fijo = txtTeleFijo.Text;
            usuario.tipo_identificacion = dropTipoIdenti.Text;
            usuario.clave = tctClave.Text;
            usuario.usuario1 = txtUser.Text;
            usuario.es_superadmin = dropSupAdmin.Text;

            return usuario;

        }

        private usuario ObtenerDatosDelGridUpdate()
        {
            usuario usuario = new usuario();

            usuario.id = Convert.ToInt32(lblId.Text);
            usuario.apellido = txtApellidos.Text;
            usuario.direccion = txtDireccion.Text;
            usuario.email = txtEmail.Text;
            usuario.estado = dropEstado.Text;
            usuario.fecha_nacimiento = dateFecNac.Value;
            usuario.foto = lblFoto.Text;
            usuario.nombre = txtNombCli.Text;
            usuario.numero_identificacion = txtNumIden.Text;
            usuario.telefono_movil = txtCelular.Text;
            usuario.telefono_fijo = txtTeleFijo.Text;
            usuario.tipo_identificacion = dropTipoIdenti.Text;
            usuario.clave = tctClave.Text;
            usuario.usuario1 = txtUser.Text;
            usuario.es_superadmin = dropSupAdmin.Text;

            return usuario;

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioInsert = ObtenerDatosDelGridInsert();

                List<usuario> usuarioLogeado = repository.hacerLogin(Login.user, Login.password);
                usuarioInsert.cwi = usuarioLogeado[0].id;
                usuarioInsert.cwd = DateTime.Now;

                var isOK = repository.InsertUsuario(usuarioInsert);

                if (isOK)
                {
                    MessageBox.Show("Usuario Guardado");
                    panelUsuarios.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridUsuarios.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Usuario?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    if (dataGridUsuarios.SelectedCells[13].Value.ToString() == "admin")
                    {
                        MessageBox.Show("No puede eliminar al Admin");
                        return;
                    }
                    else
                    {
                        try
                        {
                            var eliminado = repository.EliminarUsuarios(Convert.ToInt32(dataGridUsuarios.SelectedCells[1].Value));

                            if (eliminado)
                            {
                                MessageBox.Show("Usuario eliminado");
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

        private void dataGridUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panelUsuarios.Visible = true;
            btnGuardar.Enabled = false;


            try
            {
                lblId.Text = dataGridUsuarios.SelectedCells[1].Value?.ToString();
                txtApellidos.Text = dataGridUsuarios.SelectedCells[2].Value?.ToString();
                txtCelular.Text = dataGridUsuarios.SelectedCells[8].Value?.ToString();
                txtDireccion.Text = dataGridUsuarios.SelectedCells[11].Value?.ToString();
                txtEmail.Text = dataGridUsuarios.SelectedCells[9].Value?.ToString();
                txtNombCli.Text = dataGridUsuarios.SelectedCells[3].Value?.ToString();
                txtNumIden.Text = dataGridUsuarios.SelectedCells[5].Value?.ToString();
                txtTeleFijo.Text = dataGridUsuarios.SelectedCells[7].Value?.ToString();
                txtUser.Text = dataGridUsuarios.SelectedCells[13].Value?.ToString();
                dropEstado.Text = dataGridUsuarios.SelectedCells[15].Value?.ToString();
                dropSupAdmin.Text = dataGridUsuarios.SelectedCells[12].Value?.ToString();
                dropTipoIdenti.Text = dataGridUsuarios.SelectedCells[4].Value?.ToString();
                dateFecNac.Value = Convert.ToDateTime(dataGridUsuarios.SelectedCells[6].Value);

                if (txtNombCli.Text == "Admin" || txtApellidos.Text == "Admin")
                {
                    txtApellidos.Enabled = false;
                    txtNombCli.Enabled = false;
                    txtUser.Enabled = false;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = ObtenerDatosDelGridUpdate();

                List<usuario> usuarioLogeado = repository.hacerLogin(Login.user, Login.password);
                usuario.cwi = usuarioLogeado[0].id;
                usuario.cwd = DateTime.Now;

                var isOk = repository.ModificarUsuarios(usuario);

                if (isOk)
                {
                    MessageBox.Show("Usuario Modificado con Éxito");
                    panelUsuarios.Visible = false;
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
