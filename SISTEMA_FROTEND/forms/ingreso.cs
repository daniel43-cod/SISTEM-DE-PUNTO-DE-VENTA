using SISTEMA_FROTEND.DTOs;
using SISTEMA_FROTEND.helpers;
using SISTEMA_FROTEND.models;
using SISTEMA_FROTEND.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SISTEMA_FROTEND.presentacion
{
    public partial class ingreso : Form
    {
        public ingreso()
        {
            InitializeComponent();
            ConfigurarComboCategoria();
            timer1.Interval = 3000; // 3000 ms = 3 segundos

            texproducto.KeyDown += EnterComoTab;
            texdescripcion.KeyDown += EnterComoTab;
            texcodigobarras.KeyDown += EnterComoTab;
            texcantidad.KeyDown += EnterComoTab;
            texminima.KeyDown += EnterComoTab;
            texpreciocompra.KeyDown += EnterComoTab;
            texpreciomin.KeyDown += EnterComoTab;
            texpreciomayor.KeyDown += EnterComoTab;
           
        }
        //metodo para moverse entre los campos de texto con el enter
        private void EnterComoTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(
                    (Control)sender,
                    true,
                    true,
                    true,
                    true);

                e.SuppressKeyPress = true;
            }
        }

        private void ConfigurarComboCategoria()
        {
            comcategoria.DropDownStyle = ComboBoxStyle.DropDown;
            comcategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comcategoria.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private string rutaimagen = "";
        private readonly CategoriaServices _categoriaServices = new CategoriaServices();
        private List<CategoriaDto> _categorias = new();



        private readonly ProductoService _service = new();

        private async void button2_Click(object sender, EventArgs e)
        {

            if (comcategoria.Text == "")
            {
                MostrarError(labcategoria, "Debe ingresar la categoría del producto");
                return;
            }

            if (texcodigobarras.Text == "")
            {
                MostrarError(labcodigobarras, "Ingresa el código de barras");
                return;
            }

            if (texproducto.Text == "")
            {
                MostrarError(labproducto, "Ingresa el nombre del producto");
                return;
            }





            if (texpreciocompra.Text == "")
            {
                MostrarError(labpreciocompra, "Ingresa el precio");
                return;
            }

            if (!decimal.TryParse(texpreciocompra.Text, out _))
            {
                MostrarError(labpreciocompra, "Ingresa un preci3 válido");
                return;
            }

            //1

            if (!string.IsNullOrWhiteSpace(texpreciomin.Text) &&
                !int.TryParse(texpreciomin.Text, out _))
            {
                MostrarError(labpreciomenor, "Ingrese un número válido");
                return;
            }

            //2

            if (!string.IsNullOrWhiteSpace(texpreciomayor.Text) &&
                !int.TryParse(texpreciomayor.Text, out _))
            {
                MostrarError(labpreciomayor, "Ingrese un número válido");
                return;
            }

            if (!string.IsNullOrWhiteSpace(texminima.Text) &&
                !int.TryParse(texminima.Text, out _))
            {
                MostrarError(labexistenciamin, "Ingrese un número válido");
                return;
            }



            decimal preciomayor = Convert.ToDecimal(texpreciomayor.Text);
            decimal preciomenor = Convert.ToDecimal(texpreciomin.Text);

            Productos producto = new Productos



            {
                nombre = texproducto.Text,
                descripcion = texdescripcion.Text,
                stock = Convert.ToInt32(texcantidad.Text),
                stock_minimo = Convert.ToInt32(texminima.Text),
                precio_compra = Convert.ToDecimal(texpreciocompra.Text),
                codigo_barra = texcodigobarras.Text,
                id_categoria = Convert.ToInt32(comcategoria.SelectedValue),




            };


            string error = await _service.CrearProducto(producto, preciomayor, preciomenor, rutaimagen);

            if (error != null)
            {
                MessageBox.Show($"Error al registrar el producto: {error}");
            }
            else
            {
                MessageBox.Show("Producto registrado correctamente.");
                limpiar();
            }

        }


        private void limpiarlabels()
        {
            labcategoria.Text = "";
            labproducto.Text = "";
            labdescripcion.Text = "";
            labcantidad.Text = "";
            labexistenciamin.Text = "";
            labpreciomayor.Text = "";
            labpreciocompra.Text = "";
            labcodigobarras.Text = "";
            labpreciomayor.Text = "";
            labpreciomenor.Text = "";


        }

        private void MostrarError(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;
            lbl.ForeColor = Color.Red;

            timer1.Stop();
            timer1.Tag = lbl;
            timer1.Start();
        }

        private async void ingreso_Load(object sender, EventArgs e)
        {
            await CargarCategoriasAsync();
            lblUsuario.Text = $"Usuario: {Sesion.Nombre}";

        }

        //ingresar la imagen
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "Imagenes|*.jpg;*.jpeg;*.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                rutaimagen = openFile.FileName;

                picimagen.Image = Image.FromFile(rutaimagen);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private async void FrmProductos_Load(object sender, EventArgs e)
        {
            await CargarCategoriasAsync();
        }

        private async Task CargarCategoriasAsync()
        {
            try
            {
                _categorias = await _categoriaServices.GetCategoriasAsync();

                // _categorias.Insert(0, new CategoriaDto { Id = 0, Nombre = "" }); // fila vacía

                comcategoria.DataSource = null;
                comcategoria.DataSource = _categorias;
                comcategoria.DisplayMember = "Nombre";
                comcategoria.ValueMember = "Id";
                comcategoria.SelectedIndex = 0;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"No se pudo conectar con la API: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void texproducto_TextChanged(object sender, EventArgs e)
        {

        }

        private void limpiar()
        {
            comcategoria.Text = string.Empty;
            texproducto.Text = string.Empty;
            texdescripcion.Text = string.Empty;
            texcantidad.Text = string.Empty;
            texcodigobarras.Text = string.Empty;
            texminima.Text = string.Empty;
            texpreciocompra.Text = string.Empty;
            picimagen.Image = null;
            texcantidad.Text = string.Empty;
            texpreciomayor.Text = string.Empty;
            texpreciomin.Text = string.Empty;

        }

        private void bulimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarlabels();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Tag is Label lbl)
            {
                lbl.Text = "";
            }

            timer1.Stop();
        }

        private void picimagen_Click(object sender, EventArgs e)
        {

        }

        private void butsalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
