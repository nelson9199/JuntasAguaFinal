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
    public partial class Medidores : Form
    {
        RepositoryAgua repository = new RepositoryAgua();

        int numCli;

        public Medidores(int numCli = 0)
        {
            InitializeComponent();

            this.numCli = numCli;

            if (this.numCli > 0)
            {
                Mostrar();
            }
        }

        private void Mostrar()
         {
            var medidores = repository.ObtenerMedidioresPorId(this.numCli);

                if(medidores[0].estado == "A")
                {
                    medidores[0].estado = "Activo";
                }
                else if(medidores[0].estado == "I")
                {
                    medidores[0].estado = "Activo";
                }

            dataGridMedidores.DataSource = medidores;

            dataGridMedidores.Columns[1].HeaderText = "N";
            dataGridMedidores.Columns[2].HeaderText = "Nombre";
            dataGridMedidores.Columns[3].HeaderText = "Barrio";
            dataGridMedidores.Columns[4].HeaderText = "Tarifa";
            dataGridMedidores.Columns[5].HeaderText = "Serie";
            dataGridMedidores.Columns[6].HeaderText = "Lectura Inicial";
            dataGridMedidores.Columns[7].HeaderText = "Estado";


        }


        private void dataGridMedidores_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void Medidores_Load_1(object sender, EventArgs e)
        {


        }
    }
}
