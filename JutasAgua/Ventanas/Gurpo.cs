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
    public partial class Gurpo : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Gurpo()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Load(object sender, EventArgs e)
        {

        }

        private void Gurpo_Load(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = false;
            Mostrar();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            bunifuShadowPanel1.Visible = false;
        }

        async void Mostrar()
        {
            try
            {
                var grupos = repository.ObtenerGrupo();

                dataGirdGrupo.DataSource = await grupos;

                dataGirdGrupo.Columns[0].DisplayIndex = 4;

                dataGirdGrupo.Columns[1].HeaderText = "N";
                dataGirdGrupo.Columns[2].HeaderText = "Nombre";
                dataGirdGrupo.Columns[3].HeaderText = "Código";
                dataGirdGrupo.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGirdGrupo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        grupo ObtenerGrupoInsert()
        {
            grupo grupo = new grupo();

            grupo.codigo = txtCodigo.Text;
            grupo.nombre = txtNombre.Text;

            return grupo;
        }
        grupo ObtenerGrupoUpdate()
        {
            grupo grupo = new grupo();

            grupo.id = Convert.ToInt32(lblId.Text);
            grupo.codigo = txtCodigo.Text;
            grupo.nombre = txtNombre.Text;

            return grupo;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var grupo = ObtenerGrupoInsert();

                var isOk = await repository.InsertarGrupo(grupo);

                if (isOk)
                {
                    MessageBox.Show("Grupo Guardado");
                    bunifuShadowPanel1.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGirdGrupo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuShadowPanel1.Visible = true;
            btnGuardar.Enabled = false;
            btnGuardarCambios.Enabled = true;

            lblId.Text = dataGirdGrupo.SelectedCells[1].Value?.ToString();
            txtNombre.Text = dataGirdGrupo.SelectedCells[2].Value?.ToString();
            txtCodigo.Text = dataGirdGrupo.SelectedCells[3].Value?.ToString();
        }

        private async void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var grupo = ObtenerGrupoUpdate();

                var isOk = await repository.ModificarGrupo(grupo);

                if (isOk)
                {
                    MessageBox.Show("Grupo Actualizado");
                    bunifuShadowPanel1.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void dataGirdGrupo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGirdGrupo.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Grupo?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOk = await repository.EliminarGrupo(Convert.ToInt32(dataGirdGrupo.SelectedCells[1].Value));

                        if (isOk)
                        {
                            MessageBox.Show("Grupo Eliminada");
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
