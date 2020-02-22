﻿using System;
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
            if(formEmpresas == null)
            {
                formEmpresas = new Empresas();
                formEmpresas.MdiParent = this;
                formEmpresas.FormClosed += new FormClosedEventHandler(CerrarFormaEmpresas);
                formEmpresas.Show();
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
    }
}