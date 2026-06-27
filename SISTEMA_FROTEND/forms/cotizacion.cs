using SISTEMA_FROTEND.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SISTEMA_FROTEND.presentacion
{
    public partial class cotizacion : Form
    {
        public cotizacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btncotizar_Click(object sender, EventArgs e)
        {

        }

        private void cotizacion_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuario: {Sesion.Nombre}";

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
