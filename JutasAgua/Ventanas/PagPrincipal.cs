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
    public partial class PagPrincipal : Form
    {
        Empresas formEmpresas = null;
        Clientes formClientes = null;
        Usuarios formUsuarios = null;
        public PagPrincipal()
        {
            InitializeComponent();
        }

        private void PagPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void PagPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //ABRIR FROMA EMPRESAS
        private void empresasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formEmpresas == null)
            {
                formEmpresas = new Empresas();
                formEmpresas.MdiParent = this;
                formEmpresas.FormClosed += new FormClosedEventHandler(CerrarFormaEmpresas);
                formEmpresas.Show();
                formEmpresas.WindowState = FormWindowState.Maximized;
            }
            else
            {
                formEmpresas.Activate();
            }
        }

        private void CerrarFormaEmpresas(object sender, FormClosedEventArgs e)
        {
            formEmpresas = null;
        }

        //Abrir Forma Clientes
        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (formClientes == null)
            {
                formClientes = new Clientes();
                formClientes.MdiParent = this;
                formClientes.FormClosed += new FormClosedEventHandler(CerrarFormaClientes);
                formClientes.Show();
                formClientes.WindowState = FormWindowState.Maximized;

            }
            else
            {
                formClientes.Activate();
            }
        }

        private void CerrarFormaClientes(object sender, FormClosedEventArgs e)
        {
            formClientes = null;
        }

        private void perdilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //abrir ventana usuarios
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formUsuarios == null)
            {
                formUsuarios = new Usuarios();
                formUsuarios.MdiParent = this;
                formUsuarios.FormClosed += new FormClosedEventHandler(CerrarFormaUsuarios);
                formUsuarios.Show();
                formUsuarios.WindowState = FormWindowState.Maximized;

            }
            else
            {
                formUsuarios.Activate();
            }
        }

        private void CerrarFormaUsuarios(object sender, FormClosedEventArgs e)
        {
            formUsuarios = null;
        }
    }
}
