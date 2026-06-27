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

namespace SISTEMA_FROTEND.forms
{
    public partial class frmproductos : Form
    {
        public frmproductos()
        {
            InitializeComponent();
        }

        private CancellationTokenSource _cts;



        ProductoService _service = new ProductoService();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void frmproductos_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuario: {Sesion.Nombre}";

            var productos = await _service.ListarProducto();
            dataProductos.DataSource = productos;
            dataProductos.Columns["id_producto"].Visible = false; // aquí, una sola vez


            dataProductos.DataSource = productos;
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (_actualizandolista)
            {
                return;
            }

            if (combuscar.SelectedItem is Productos productoSeleccionado)
            {
                var listaUnico = new List<Productos> { productoSeleccionado };
                dataProductos.DataSource = listaUnico;
                dataProductos.Columns["id_producto"].Visible = false;

            }


        }
        private bool _actualizandolista = false;
        private async void combuscar_TextChanged(object sender, EventArgs e)
        {
            if (_actualizandolista)
            {
                return;
            }

            string texto = combuscar.Text;



            //metodo que muestra todo el catalogo si el usuario va borrando el buscador combuscar
            if (string.IsNullOrWhiteSpace(texto))
            {
                await CargarTodosLosProductos();
                return;
            }

            if (texto.Length  <=10)
            {
                return;
            }


            // Cancela cualquier búsqueda anterior que todavía esté esperando
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var tokenActual = _cts.Token;

            List<Productos> productos;
            try
            {
                productos = await _service.BuscarProducto(texto);
            }
            catch (Exception)
            {
                return; // si falla la consulta, no sigas
            }

            // Si mientras esperábamos, el usuario ya escribió algo más, ignora este resultado viejo
            if (tokenActual.IsCancellationRequested)
            {
                return;
            }


            _actualizandolista = true;

            combuscar.DataSource = productos;
            combuscar.DisplayMember = "nombre";
            combuscar.ValueMember = "id_producto";
            /*combuscar.Text = texto;
            combuscar.SelectionStart = texto.Length;*/
            combuscar.SelectedIndex = -1;//quita la seleccion automatica del primer elemento
            combuscar.Text = string.Empty;//LIMIPA EL COMBUSCAR
            combuscar.DroppedDown = true;

            _actualizandolista = false;
        }

        private void dataProductos_SelectionChanged(object sender, EventArgs e)
        {

        }

        //metodo para mostrar todos los productos si el usuario no tiene nada escrito en el combuscar
        private async void combuscar_Enter(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(combuscar.Text))
            {
                await CargarTodosLosProductos();
            }
        }

        private async Task CargarTodosLosProductos()
        {
            var productos = await _service.ListarProducto(); // el método que ya tienes para listar todo

            _actualizandolista = true;

            combuscar.DataSource = productos;
            combuscar.DisplayMember = "nombre";
            combuscar.ValueMember = "id_producto";
            combuscar.DroppedDown = true;

            _actualizandolista = false;
        }
    }
}
