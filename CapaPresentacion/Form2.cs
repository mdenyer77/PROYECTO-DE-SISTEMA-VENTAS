using CapaEntidades;
using CapaNegocio;
using Capa.Negocio;
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
    public partial class Form2 : Form
    {
        #region Variables Privadas
        private ProductosBL productosBL;
        private CategoriaBL categoriaBL;
        #endregion

        #region Constructor
        public Form2()
        {
            InitializeComponent();
            InicializarComponentes();
        }
        #endregion

        #region Métodos Privados
        private void InicializarComponentes()
        {
            try
            {
                productosBL = new ProductosBL();
                categoriaBL = new CategoriaBL();
                ConfigurarBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar componentes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarBotones()
        {
            button4.Click += Button4_Click;
            button4.Text = "Agregar";

            button2.Click += Button2_Click;
            button2.Text = "Eliminar";
        }

        private void CargarProductos()
{
    try
    {
        var productos = productosBL.Listar();

        if (productos != null && productos.Count > 0)
        {
            productosBindingSource.DataSource = productos;

            //  OCULTAR COLUMNAS
            if (dataGridView1.Columns["ID_Producto"] != null)
                dataGridView1.Columns["ID_Producto"].Visible = false;

            if (dataGridView1.Columns["ID_categoria"] != null)
                dataGridView1.Columns["ID_categoria"].Visible = false;
        }
        else
        {
            productosBindingSource.DataSource = null;
            MessageBox.Show("No hay productos registrados", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al cargar los productos: " + ex.Message,
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void CargarCategorias()
        {
            try
            {
                var categorias = categoriaBL.Listar();
                
                if (categorias != null && categorias.Count > 0)
                {
                    comboBox1.DataSource = categorias;
                    comboBox1.DisplayMember = "Nombre_Categoria";
                    comboBox1.ValueMember = "ID_categoria";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioProducto.Text))
            {
                MessageBox.Show("El precio del producto no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrecioProducto.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("El precio debe ser un número válido mayor a cero", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("El stock no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoría", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombreProducto.Clear();
            txtPrecioProducto.Clear();
            txtStock.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private int? ObtenerProductoSeleccionado()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un producto para eliminar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                var celda = dataGridView1.SelectedRows[0].Cells["iDProductoDataGridViewTextBoxColumn"];

                if (celda == null || celda.Value == null || celda.Value == DBNull.Value)
                {
                    MessageBox.Show("No se pudo obtener el ID del producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                if (!int.TryParse(celda.Value.ToString(), out int idProducto) || idProducto <= 0)
                {
                    MessageBox.Show("El ID del producto no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return idProducto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el producto seleccionado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region Eventos del Formulario
        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            try
            {
                CargarProductos();
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Evento reservado
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            // Evento reservado
        }
        #endregion

        #region Eventos de Botones
        private void Button4_Click(object sender, EventArgs e)
        {
            // Botón Agregar - Agrega un nuevo producto
            AgregarProducto();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Botón Eliminar - Elimina el producto seleccionado
            EliminarProducto();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Botón Agregar Categoría
            AgregarCategoria();
        }
        #endregion

        #region Métodos de Negocio
        private void AgregarProducto()
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }

                Productos producto = new Productos
                {
                    ID_Producto = 0,
                    Nombre_Producto = txtNombreProducto.Text.Trim(),
                    Precio_Producto = Convert.ToDecimal(txtPrecioProducto.Text),
                    stock = Convert.ToInt32(txtStock.Text),
                    ID_categoria = Convert.ToInt32(comboBox1.SelectedValue)
                };

                productosBL.Agregar(producto);
                MessageBox.Show("Producto agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarProductos();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al agregar producto: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarProducto()
        {
            try
            {
                int? idProducto = ObtenerProductoSeleccionado();

                if (!idProducto.HasValue)
                {
                    return;
                }

                if (MessageBox.Show("¿Está seguro que desea eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                productosBL.Eliminar(idProducto.Value);
                MessageBox.Show("Producto eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarProductos();
                LimpiarCampos();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al eliminar producto: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void button4_Click_1(object sender, EventArgs e)
        {
            AgregarProducto();
        }

        private void AgregarCategoria()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("El nombre de la categoría no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Categorias categoria = new Categorias
                {
                    ID_categoria = 0,
                    Nombre_Categoria = textBox2.Text.Trim()
                };

                categoriaBL.Agregar(categoria);
                MessageBox.Show("Categoría agregada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox2.Clear();
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        } 


    }
}
