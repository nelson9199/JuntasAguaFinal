﻿using Modelos;
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
    public partial class Empresas : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        public Empresas()
        {
            InitializeComponent();
        }

        private void Mostrar()
        {
            try
            {

                var empresa = repository.obtenerEmpresas();

                if (empresa.Count > 0)
                {
                    switch (empresa[0].facturacion)
                    {
                        case "F":
                            empresa[0].facturacion = "Facturación";
                            break;
                        case "R":
                            empresa[0].facturacion = "Recibo";
                            break;
                    }
                }

                dataGridEmpresas.DataSource = empresa;

                dataGridEmpresas.Columns[0].Visible = false;
                dataGridEmpresas.Columns[1].HeaderText = "Nombre";
                dataGridEmpresas.Columns[2].HeaderText = "Email";
                dataGridEmpresas.Columns[3].HeaderText = "Dirección";
                dataGridEmpresas.Columns[4].Visible = false;
                dataGridEmpresas.Columns[5].HeaderText = "Teléfono Fijo";
                dataGridEmpresas.Columns[6].HeaderText = "Telefono Móvil";
                dataGridEmpresas.Columns[7].HeaderText = "Recaudador";
                dataGridEmpresas.Columns[8].HeaderText = "RUC";
                dataGridEmpresas.Columns[9].HeaderText = "Facturación";
                dataGridEmpresas.Columns[10].Visible = false;
                dataGridEmpresas.Columns[11].Visible = false;
                dataGridEmpresas.Columns[12].Visible = false;
                dataGridEmpresas.Columns[13].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Empresas_Load(object sender, EventArgs e)
        {
            Mostrar();
            panelEmpresa.Visible = false;
        }

        private void dataGridEmpresas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridEmpresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelEmpresa.Visible = true;

            lblId.Text = dataGridEmpresas.SelectedCells[0].Value?.ToString();
            txtNombreEmpre.Text = dataGridEmpresas.SelectedCells[1]?.Value.ToString();
            txtEmail.Text = dataGridEmpresas.SelectedCells[2].Value?.ToString();
            txtDirecc.Text = dataGridEmpresas.SelectedCells[3].Value?.ToString();
            lblLogo.Text = dataGridEmpresas.SelectedCells[4].Value?.ToString();
            txtTelefonoFijo.Text = dataGridEmpresas.SelectedCells[5].Value?.ToString();
            txtTelefonoCel.Text = dataGridEmpresas.SelectedCells[6].Value?.ToString();
            txtRecaudador.Text = dataGridEmpresas.SelectedCells[7].Value?.ToString();
            txtRuc.Text = dataGridEmpresas.SelectedCells[8].Value?.ToString();

            dropFactu.Text = dataGridEmpresas.SelectedCells[9].Value == null ? "" : dataGridEmpresas.SelectedCells[9].Value.ToString();
            lblcci.Text = dataGridEmpresas.SelectedCells[10].Value == null ? "" : dataGridEmpresas.SelectedCells[10].Value.ToString();
            lblccd.Text = dataGridEmpresas.SelectedCells[11].Value == null ? "" : dataGridEmpresas.SelectedCells[11].Value.ToString();
            lblcwi.Text = dataGridEmpresas.SelectedCells[12].Value == null ? "" : dataGridEmpresas.SelectedCells[12].Value.ToString();
            lblcwd.Text = dataGridEmpresas.SelectedCells[13].Value == null ? "" : dataGridEmpresas.SelectedCells[13].Value.ToString();

        }

        private empresa ObtenerEmpresas()
        {
            empresa empresa = new empresa();
            empresa.direccion = txtDirecc.Text;
            empresa.email = txtEmail.Text;
            empresa.id = Convert.ToInt32(lblId.Text);
            empresa.logo = lblLogo.Text;
            empresa.nombre = txtNombreEmpre.Text;
            empresa.recaudador = txtRecaudador.Text;
            empresa.ruc = txtRuc.Text;
            empresa.telefono_fijo = txtTelefonoFijo.Text;
            empresa.telefono_movil = txtTelefonoCel.Text;

            if (dropFactu.Text == "FACTURA")
            {
                empresa.facturacion = "F";
            }
            else if (dropFactu.Text == "RECIBO")
            {
                empresa.facturacion = "R";
            }


            return empresa;

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelEmpresa.Visible = false;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                empresa updateEmpresa = ObtenerEmpresas();

            //llenar el cwi y cwd cuando se editan datos
            List<usuario> usuario = repository.hacerLogin(Login.user, Login.password);
            updateEmpresa.cwd = DateTime.Now;
            updateEmpresa.cwi = usuario[0].id;

            var isModfy = repository.ModificarEmpresa(updateEmpresa);

            if (isModfy)
            {
                MessageBox.Show("Cambios realizados con éxito");
                panelEmpresa.Visible = false;
                Mostrar();

            }
            else
            {
                MessageBox.Show("No se Modificarion datos");
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
