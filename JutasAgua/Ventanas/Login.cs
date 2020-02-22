using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JutasAgua.Ventanas;
using Modelos;
namespace JutasAgua
{
    public partial class Login : Form
    {
        RepositoryAgua repository = new RepositoryAgua();
        PagPrincipal pagPrincipal;
       
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string password = txtPassword.Text;

            try
            {
                var datos = repository.hacerLogin(user,password);

                if(datos.Count > 0)
                {
                    MessageBox.Show("Acceso Exitoso");
                    this.Hide();
                    pagPrincipal = new PagPrincipal();
                    pagPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña Incorrectos");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
