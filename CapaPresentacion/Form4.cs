using CapaEntidades;
using CapaNegocio;
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
    public partial class Form4 : Form
    {
        #region Variables Privadas
        private VentasBL ventasBL;
        #endregion

        #region Constructor
        public Form4()
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
                ventasBL = new VentasBL();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar componentes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVentas()
        {
            try
            {
                if (ventasDataSet4 == null || ventaTableAdapter == null)
                {
                    MessageBox.Show("Los componentes de datos no están inicializados correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ventasDataSet4.venta.Clear();
                ventaTableAdapter.Fill(ventasDataSet4.venta);

                if (ventasDataSet4.venta.Rows.Count == 0)
                {
                    MessageBox.Show("No hay ventas registradas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al cargar ventas: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? ObtenerIDVentaSeleccionada()
        {
            try
            {
                if (dataGridView1 == null)
                {
                    MessageBox.Show("DataGridView no inicializado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una venta", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                var celda = dataGridView1.SelectedRows[0].Cells["iDventaDataGridViewTextBoxColumn"];

                if (celda == null || celda.Value == null || celda.Value == DBNull.Value)
                {
                    MessageBox.Show("No se pudo obtener el ID de la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                if (!int.TryParse(celda.Value.ToString(), out int idVenta) || idVenta <= 0)
                {
                    MessageBox.Show("El ID de venta no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return idVenta;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("La columna especificada no existe: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID de venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region Eventos del Formulario
        private void Form4_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evento reservado para futuras funcionalidades
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Botón Productos - Abre el formulario de productos
            // Implementar navegación según sea necesario
        }
        #endregion

        #region Eventos de Botones de Acción
        private void button5_Click(object sender, EventArgs e)
        {
            // Botón Visualizar Reporte - Muestra el reporte de la venta seleccionada con todos los detalles
            VisualizarReporte();
        }
        #endregion

        #region Métodos de Negocio
        private void VisualizarReporte()
        {
            try
            {
                int? idVenta = ObtenerIDVentaSeleccionada();

                if (!idVenta.HasValue)
                {
                    return;
                }

                try
                {
                    // Crear y mostrar el reporte con los datos de la venta seleccionada
                    ReporteVenta reporte = new ReporteVenta(idVenta.Value);
                    
                    if (reporte == null)
                    {
                        MessageBox.Show("No se pudo crear el formulario de reporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    reporte.ShowDialog();

                    // Recargar datos después de cerrar el reporte
                    CargarVentas();
                }
                catch (System.IO.FileNotFoundException fnEx)
                {
                    MessageBox.Show("Archivo de reporte no encontrado: " + fnEx.Message, "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exReporte)
                {
                    MessageBox.Show("Error al abrir el reporte: " + exReporte.Message + "\n\nDetalles: " + exReporte.InnerException?.Message, 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la visualización del reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}

