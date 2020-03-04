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
        Marcas formMarcas = null;
        Barrio formBarrio = null;
        Tarifas tarifas = null;
        Multas multas = null;
        Gurpo gurpo = null;
        Impuesto impuesto = null;

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

        //Abrir from Marcas
        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formMarcas == null)
            {
                formMarcas = new Marcas();
                formMarcas.MdiParent = this;
                formMarcas.FormClosed += new FormClosedEventHandler(CerrarFormaMarcas);
                formMarcas.Show();
                formMarcas.WindowState = FormWindowState.Normal;

            }
            else
            {
                formMarcas.Activate();
            }
        }


        private void CerrarFormaMarcas(object sender, FormClosedEventArgs e)
        {
            formMarcas = null;
        }

        private void barrioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formBarrio == null)
            {
                formBarrio = new Barrio();
                formBarrio.MdiParent = this;
                formBarrio.FormClosed += new FormClosedEventHandler(CerrarFormaBarrio);
                formBarrio.Show();
                formBarrio.WindowState = FormWindowState.Normal;

            }
            else
            {
                formBarrio.Activate();
            }
        }


        private void CerrarFormaBarrio(object sender, FormClosedEventArgs e)
        {
            formBarrio = null;
        }

        //abrir ventana tarifas
        private void tarifasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tarifas == null)
            {
                tarifas = new Tarifas();
                tarifas.MdiParent = this;
                tarifas.FormClosed += new FormClosedEventHandler(CerrarFormaTarifas);
                tarifas.Show();
            }
            else
            {
                tarifas.Activate();
            }
        }

        private void CerrarFormaTarifas(object sender, FormClosedEventArgs e)
        {
            tarifas = null;
        }

        private void multasRetrasoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (multas == null)
            {
                multas = new Multas();
                multas.MdiParent = this;
                multas.FormClosed += new FormClosedEventHandler(CerrarFormaMultas);
                multas.Show();
            }
            else
            {
                multas.Activate();
            }
        }

        private void CerrarFormaMultas(object sender, FormClosedEventArgs e)
        {
            multas = null;
        }

        private void grupoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gurpo == null)
            {
                gurpo = new Gurpo();
                gurpo.MdiParent = this;
                gurpo.FormClosed += new FormClosedEventHandler(CerrarFormaGrupo);
                gurpo.Show();
            }
            else
            {
                gurpo.Activate();
            }
        }

        private void CerrarFormaGrupo(object sender, FormClosedEventArgs e)
        {
            gurpo = null;
        }

        private void impuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (impuesto == null)
            {
                impuesto = new Impuesto();
                impuesto.MdiParent = this;
                impuesto.FormClosed += new FormClosedEventHandler(CerrarFormaImpuesto);
                impuesto.Show();
            }
            else
            {
                impuesto.Activate();
            }
        }
        private void CerrarFormaImpuesto(object sender, FormClosedEventArgs e)
        {
            impuesto = null;
        }

    }
}
