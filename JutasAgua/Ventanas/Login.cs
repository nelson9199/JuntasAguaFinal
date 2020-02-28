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

        public static string user;
        public static string password;

        public Login()
        {
            InitializeComponent();
        }

        private void txtPassword_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string password = txtPassword.Text;
            Login.user = txtUser.Text;
            Login.password = txtPassword.Text;


            var isOk = repository.ComprobarLogin(user, password);

            if (isOk)
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



        }
    }
}
