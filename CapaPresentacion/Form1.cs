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
    public partial class Form1 : Form
    {
        #region Variables Privadas
        private CategoriaBL categoriaBL;
        #endregion

        #region Constructor
        public Form1()
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
            button1.Click += Button1_Click;
            button1.Text = "Ingresar";

            button2.Click += Button2_Click;
            button2.Text = "Eliminar";
        }

        private void CargarCategorias()
        { 

            try
            {
                if (ventasDataSet1 == null || categoriaTableAdapter == null)
                {
                    MessageBox.Show("Los componentes de datos no están inicializados correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ventasDataSet1.categoria.Clear();
                categoriaTableAdapter.Fill(ventasDataSet1.categoria);

                if (ventasDataSet1.categoria.Rows.Count == 0)
                {
                    MessageBox.Show("No hay categorías registradas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al cargar categorías: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("El nombre de la categoría no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("La descripción de la categoría no puede estar vacía", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private int? ObtenerCategoriaSeleccionada()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una categoría para eliminar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                var celda = dataGridView1.SelectedRows[0].Cells["iDcategoriaDataGridViewTextBoxColumn"];

                if (celda == null || celda.Value == null || celda.Value == DBNull.Value)
                {
                    MessageBox.Show("No se pudo obtener el ID de la categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                if (!int.TryParse(celda.Value.ToString(), out int idCategoria) || idCategoria <= 0)
                {
                    MessageBox.Show("El ID de la categoría no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return idCategoria;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la categoría seleccionada: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region Eventos del Formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            {
                CargarCategorias();
                // Agregar evento para cargar datos al seleccionar una fila
                dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var row = dataGridView1.SelectedRows[0];
                    textBox1.Text = row.Cells["nombrecategoriaDataGridViewTextBoxColumn"]?.Value?.ToString() ?? "";
                    textBox2.Text = row.Cells["descripcionDataGridViewTextBoxColumn"]?.Value?.ToString() ?? "";
                    button1.Text = "Actualizar";
                }
                else
                {
                    button1.Text = "Ingresar";
                }
            }
            catch (Exception ex)
            {
                // Silenciar errores de selección
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Evento reservado
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Evento reservado
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Evento reservado
        }
        #endregion

        #region Eventos de Botones
        private void Button1_Click(object sender, EventArgs e)
        {
            // Botón Ingresar - Agrega una nueva categoría
            IngresarCategoria();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Botón Eliminar - Elimina la categoría seleccionada
            EliminarCategoria();
        }
        #endregion

        #region Métodos de Negocio
        private void IngresarCategoria()
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }

                // Verificar si es actualización o inserción
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var row = dataGridView1.SelectedRows[0];
                    int idCategoria = Convert.ToInt32(row.Cells["iDcategoriaDataGridViewTextBoxColumn"]?.Value ?? 0);

                    if (idCategoria > 0)
                    {
                        // Actualizar categoría existente
                        ActualizarCategoria(idCategoria);
                        return;
                    }
                }

                // Insertar nueva categoría
                Categorias categoria = new Categorias
                {
                    ID_categoria = 0,
                    Nombre_Categoria = textBox1.Text.Trim(),
                    Descripcion = textBox2.Text.Trim(),
                    Estado = true
                };

                categoriaBL.Agregar(categoria);
                MessageBox.Show("Categoría agregada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarCategorias();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al agregar categoría: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarCategoria(int idCategoria)
        {
            try
            {
                if (idCategoria <= 0)
                {
                    MessageBox.Show("ID de categoría inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Categorias categoria = new Categorias
                {
                    ID_categoria = idCategoria,
                    Nombre_Categoria = textBox1.Text.Trim(),
                    Descripcion = textBox2.Text.Trim(),
                    Estado = true
                };

                // Usar el método actualizar directamente de DAL, no el método Agregar de BL
                // que puede causar que se actualicen todas las categorías
                categoriaBL.Actualizar(categoria);
                MessageBox.Show("Categoría actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarCategorias();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al actualizar categoría: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarCategoria()
        {
            try
            {
                int? idCategoria = ObtenerCategoriaSeleccionada();

                if (!idCategoria.HasValue)
                {
                    return;
                }

                if (MessageBox.Show("¿Está seguro que desea eliminar esta categoría?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                categoriaBL.Eliminar(idCategoria.Value);
                MessageBox.Show("Categoría eliminada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarCategorias();
                LimpiarCampos();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al eliminar categoría: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Columns["ID_categoria"].Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

