using Modelos;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace JutasAgua.Ventanas
{

    public partial class Articulos : Form
    {
        private readonly RepositoryAgua repository = new RepositoryAgua();

        public Articulos()
        {
            InitializeComponent();
        }

        private async void Mostrar()
        {
            try
            {
                var articulos = await repository.ObtenerArticulo();

                dataGridArticulo.DataSource = articulos;

                dataGridArticulo.Columns[0].DisplayIndex = 21;

                dataGridArticulo.Columns[1].HeaderText = "N";
                dataGridArticulo.Columns[2].Visible = false;
                dataGridArticulo.Columns[3].Visible = false;
                dataGridArticulo.Columns[4].Visible = false;
                dataGridArticulo.Columns[5].Visible = false;
                dataGridArticulo.Columns[6].Visible = false;
                dataGridArticulo.Columns[7].HeaderText = "Nombre";
                dataGridArticulo.Columns[8].Visible = false;
                dataGridArticulo.Columns[9].Visible = false;
                dataGridArticulo.Columns[10].Visible = false;
                dataGridArticulo.Columns[11].Visible = false;
                dataGridArticulo.Columns[12].Visible = false;
                dataGridArticulo.Columns[13].Visible = false;
                dataGridArticulo.Columns[14].Visible = false;
                dataGridArticulo.Columns[15].Visible = false;
                dataGridArticulo.Columns[16].Visible = false;
                dataGridArticulo.Columns[17].Visible = false;
                dataGridArticulo.Columns[18].Visible = false;
                dataGridArticulo.Columns[19].Visible = false;
                dataGridArticulo.Columns[20].Visible = false;
                dataGridArticulo.Columns[21].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Articulos_Load(object sender, EventArgs e)
        {
            panelArticulo.Visible = false;
            Mostrar();

            try
            {
                var grupos = await repository.ObtenerGrupo();

                foreach (var VARIABLE in grupos)
                {
                    dropGrupo.Items.Add(VARIABLE.nombre);

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            try
            {
                var marcas =  repository.ObtenerMarcas();

                foreach (var VARIABLE in marcas)
                {
                    dropMarca.Items.Add(VARIABLE.nombre);

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            try
            {
                var impuestos = await repository.ObtenerImpuesto();

                foreach (var VARIABLE in impuestos)
                {
                    dropImpuesto.Items.Add(VARIABLE.nombre + ". " + VARIABLE.porcentaje + "%");

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            try
            {
                var unidadMedidas = await repository.ObtenerMedida();

                foreach (var VARIABLE in unidadMedidas)
                {
                    dropMedida.Items.Add(VARIABLE.nombre);

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            panelArticulo.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardarCambios.Enabled = false;
            txtDescripcion.Text = "";
            txtNomComp.Text = "";
            txtNomCort.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtStockMax.Text = "";
            txtStockMin.Text = "";

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelArticulo.Visible = false;
        }

        private articulo ObtenerDatosInsert()
        {
            articulo articulo = new articulo();

            articulo.grupo_id = Convert.ToInt32(lblIdGrup.Text);
            articulo.clase = dropClase.Text;
            articulo.control_stock = dropControlStock.Text;
            articulo.descripcion = txtDescripcion.Text;
            articulo.fecha_creacion = dateFecCreac.Value;
            articulo.impuesto_id = Convert.ToInt32(lblIdImpuesto.Text);
            articulo.marca_id = Convert.ToInt32(lblIdMarca.Text);
            articulo.nombre_completo = txtNomComp.Text;
            articulo.nombre_corto = txtNomCort.Text;
            decimal resul;
            decimal.TryParse(txtPrecioCompra.Text, out resul);
            decimal resul2;
            decimal.TryParse(txtPrecioVenta.Text, out resul2);
            sbyte resul3;
            sbyte.TryParse(txtStockMax.Text, out resul3);
            articulo.precio_compra = resul;
            articulo.precio_venta = resul2;
            articulo.stock_actual = resul3;
            articulo.stock_minimo = Convert.ToSByte(txtStockMin.Text?? "0");
            articulo.unidad_medida_id = Convert.ToInt32(lblIdMedida.Text ?? "0");

            MemoryStream ms = new MemoryStream();
            picBoxArti.Image.Save(ms, picBoxArti.Image.RawFormat);

            articulo.foto = ms.GetBuffer();

            articulo.nombre_foto = lblIcono.Text;

            return articulo;

        }

        private articulo ObtenerDatosUpdate()
        {
            articulo articulo = new articulo();

            articulo.id = Convert.ToInt32(lblId.Text);
            articulo.grupo_id = Convert.ToInt32(lblIdGrup.Text);
            articulo.clase = dropClase.Text;
            articulo.control_stock = dropControlStock.Text;
            articulo.descripcion = txtDescripcion.Text;
            articulo.fecha_creacion = dateFecCreac.Value;
            articulo.impuesto_id = Convert.ToInt32(lblIdImpuesto.Text);
            articulo.marca_id = Convert.ToInt32(lblIdMarca.Text);
            articulo.nombre_completo = txtNomComp.Text;
            articulo.nombre_corto = txtNomCort.Text;
            decimal resul;
            decimal.TryParse(txtPrecioCompra.Text, out resul);
            decimal resul2;
            decimal.TryParse(txtPrecioVenta.Text, out resul2);
            sbyte resul3;
            sbyte.TryParse(txtStockMax.Text, out resul3);
            articulo.precio_compra = resul;
            articulo.precio_venta = resul2;
            articulo.stock_actual = resul3;
            articulo.stock_minimo = Convert.ToSByte(txtStockMin.Text ?? "0");
            articulo.unidad_medida_id = Convert.ToInt32(lblIdMedida.Text ?? "0");

            MemoryStream ms = new MemoryStream();
            picBoxArti.Image.Save(ms, picBoxArti.Image.RawFormat);

            articulo.foto = ms.GetBuffer();

            articulo.nombre_foto = lblIcono.Text;

            return articulo;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "Imagenes|*.jpg;*.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.Title = "Cargador de Imagenes";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picBoxArti.Image = null;
                picBoxArti.Image = new Bitmap(openFileDialog1.FileName);
                picBoxArti.SizeMode = PictureBoxSizeMode.Zoom;
                lblIcono.Text = Path.GetFileName(openFileDialog1.FileName);

            }
        }

        private void dataGridArticulo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelArticulo.Visible = true;
            btnGuardarCambios.Enabled = true;
            btnGuardar.Enabled = false;

            lblId.Text = dataGridArticulo.SelectedCells[1].Value?.ToString();
            lblIdGrup.Text = dataGridArticulo.SelectedCells[2].Value?.ToString();
            lblIdMedida.Text = dataGridArticulo.SelectedCells[3].Value?.ToString();
            lblIdImpuesto.Text = dataGridArticulo.SelectedCells[4].Value?.ToString();
            lblIdMarca.Text = dataGridArticulo.SelectedCells[5].Value?.ToString();
            dropClase.Text = dataGridArticulo.SelectedCells[6].Value?.ToString();
            txtNomComp.Text = dataGridArticulo.SelectedCells[7].Value?.ToString();
            txtNomCort.Text = dataGridArticulo.SelectedCells[8].Value?.ToString();
            txtDescripcion.Text = dataGridArticulo.SelectedCells[9].Value?.ToString();

            picBoxArti.BackgroundImage = null;
            byte[] b = (byte[])dataGridArticulo.SelectedCells[10].Value;
            MemoryStream ms = new MemoryStream(b);
            picBoxArti.Image = Image.FromStream(ms);
            picBoxArti.SizeMode = PictureBoxSizeMode.Zoom;

            lblIcono.Text = dataGridArticulo.SelectedCells[11].Value?.ToString();
            dateFecCreac.Value = (DateTime)dataGridArticulo.SelectedCells[12].Value;
            dropControlStock.Text = dataGridArticulo.SelectedCells[13].Value?.ToString();
            txtStockMin.Text = dataGridArticulo.SelectedCells[14].Value?.ToString();
            txtStockMax.Text = dataGridArticulo.SelectedCells[15].Value?.ToString();
            txtPrecioCompra.Text = dataGridArticulo.SelectedCells[16].Value?.ToString();
            txtPrecioVenta.Text = dataGridArticulo.SelectedCells[17].Value?.ToString();
            dropGrupo.Text = dataGridArticulo.SelectedCells[18].Value?.ToString();
            dropMedida.Text = dataGridArticulo.SelectedCells[19].Value?.ToString();
            dropImpuesto.Text = dataGridArticulo.SelectedCells[20].Value?.ToString() + "%";
            dropMarca.Text = dataGridArticulo.SelectedCells[21].Value?.ToString();
        }

        private void dataGridArticulo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private async void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (dropGrupo.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropGrupo, "Debe elegir un valor");
            }
            if (dropMedida.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropMedida, "Debe elegir un valor");
            }
            if (dropMarca.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropMarca, "Debe elegir un valor");
            }
            if (dropClase.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropClase, "Debe elegir un valor");
            }
            if (dropImpuesto.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropImpuesto, "Debe elegir un valor");
            }

            if (dropGrupo.Text == "SELECCIONE..." || dropMedida.Text == "SELECCIONE..." ||
                dropMarca.Text == "SELECCIONE..." || dropClase.Text == "SELECCIONE..." ||
                dropImpuesto.Text == "SELECCIONE...")
            {
                return;
            }


            try
            {
                var articulo = ObtenerDatosInsert();

                var isOk = await repository.InsertarArticulo(articulo);

                if (isOk)
                {
                    MessageBox.Show("Articulo Guardado");
                    panelArticulo.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private async void dropGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropGrupo, "");
            if (dropGrupo.Text.ToString() == "SELECCIONE...")
            {
                return;
            }

            try
            {
                var grupo = await repository.ObtenerGrupoPorNombre(dropGrupo.SelectedItem.ToString());

                lblIdGrup.Text = grupo.id.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private async void dropMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropMedida, "");
            if (dropMedida.Text.ToString() == "SELECCIONE...")
            {
                return;
            }

            try
            {
                var medida = await repository.ObtenerMedidaPorNombre(dropMedida.SelectedItem.ToString());

                lblIdMedida.Text = medida.id.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private async void dropImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropImpuesto, "");
            if (dropImpuesto.Text.ToString() == "SELECCIONE...")
            {
                return;
            }

            try
            {

                var nomImpuesto = dropImpuesto.SelectedItem.ToString();

                var impuestoCortado = nomImpuesto.Split('.');

                var impuesto = await repository.ObtenerImpuestoPorNombre(impuestoCortado[0]);

                lblIdImpuesto.Text = impuesto.id.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private async void dropMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropMarca, "");
            if (dropMarca.Text.ToString() == "SELECCIONE...")
            {
                return;
            }
            try
            {
                var marca = await repository.ObtenerMarcaPorNombre(dropMarca.SelectedItem.ToString());

                lblIdMarca.Text = marca.id.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dropClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(dropClase, "");
        }

        private async void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (dropGrupo.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropGrupo, "Debe elegir un valor");
            }
            if (dropMedida.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropMedida, "Debe elegir un valor");
            }
            if (dropMarca.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropMarca, "Debe elegir un valor");
            }
            if (dropClase.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropClase, "Debe elegir un valor");
            }
            if (dropImpuesto.Text == "SELECCIONE...")
            {
                errorProvider1.SetError(dropImpuesto, "Debe elegir un valor");
            }

            if (dropGrupo.Text == "SELECCIONE..." || dropMedida.Text == "SELECCIONE..." ||
                dropMarca.Text == "SELECCIONE..." || dropClase.Text == "SELECCIONE..." ||
                dropImpuesto.Text == "SELECCIONE...")
            {
                return;
            }


            try
            {
                var articulo = ObtenerDatosUpdate();

                var isOk = await repository.ModificarArticulo(articulo);

                if (isOk)
                {
                    MessageBox.Show("Articulo Modificado");
                    panelArticulo.Visible = false;
                    Mostrar();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("El tamaño de la Foto es muy grande, Elija una con menor tamaño");
            }
        }

        private async void dataGridArticulo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridArticulo.Columns["eliminar"].Index)
            {
                DialogResult result;

                result = MessageBox.Show("¿Realmente desea eliminar este Artículo?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        var isOk = await repository.EliminarArticulo(Convert.ToInt32(dataGridArticulo.SelectedCells[1].Value));

                        if (isOk)
                        {
                            MessageBox.Show("Artículo Eliminado con Éxito");
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
