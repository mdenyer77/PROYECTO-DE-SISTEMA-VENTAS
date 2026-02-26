using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Menu : Form
    {
        #region Constructor
        public Menu()
        {
            InitializeComponent();
            ConfigurarBotones();
        }
        #endregion

        #region Métodos Privados
        private void ConfigurarBotones()
        {
            // Configurar los textos y eventos de los botones del menú
            button1.Text = "PRODUCTO";
            button1.Click += Button1_Click;

            button3.Text = "VENTA";
            button3.Click += Button3_Click;

            button2.Text = "FACTURACION";
            button2.Click += Button2_Click;

            button4.Text = "CATEGORIA";
            button4.Click += Button4_Click;
        }
        #endregion

        #region Eventos de Botones
        private void Button1_Click(object sender, EventArgs e)
        {
            // Abre el formulario de Productos (Form2)
            AbrirFormulario(new Form2(), "Gestión de Productos");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // Abre el formulario de Ventas (Form3)
            AbrirFormulario(new Form3(), "Gestión de Ventas");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Abre el formulario de Facturación (Form4)
            AbrirFormulario(new Form4(), "Facturación");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // Abre el formulario de Categorías (Form1)
            AbrirFormulario(new Form1(), "Gestión de Categorías");
        }
        #endregion

        #region Métodos de Navegación
        private void AbrirFormulario(Form formulario, string titulo)
        {
            try
            {
                if (formulario != null)
                {
                    formulario.Text = titulo;
                    formulario.Show();
                    this.Hide();

                    // Cuando se cierre el formulario, mostrar el menú nuevamente
                    formulario.FormClosed += (s, e) =>
                    {
                        this.Show();
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}

