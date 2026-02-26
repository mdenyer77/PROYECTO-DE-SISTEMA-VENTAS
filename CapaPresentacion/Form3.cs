using CapaEntidades;
using CapaNegocio;
using Capa.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Form3 : Form
    {
        private ProductosBL productosBL;
        private CategoriaBL categoriaBL;
        private VentasBL ventasBL;
        private ClienteBL clienteBL;
        private bool componentesInicializados = false;

        public Form3()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;  // 👈 AGREGA ESTA LÍNEA
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            try
            {
                productosBL = new ProductosBL();
                categoriaBL = new CategoriaBL();
                ventasBL = new VentasBL();
                clienteBL = new ClienteBL();
                componentesInicializados = true;
            }
            catch (Exception ex)
            {
                componentesInicializados = false;
                MessageBox.Show("Error crítico: No se pudieron inicializar los componentes de negocio.\n" + ex.Message, 
                    "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
             

            if (!componentesInicializados)
            {
                MessageBox.Show("No es posible continuar. Los componentes de negocio no están disponibles.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                CargarComboBoxes();
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboBoxes()
        {
            try
            {
                // Cargar clientes en comboBox1
                if (comboBox1 != null)
                {
                    try
                    {
                        var clientes = clienteBL.Listar();
                        
                        if (clientes == null || clientes.Count == 0)
                        {
                            MessageBox.Show("No hay clientes disponibles en la base de datos\n\nVerifica:\n1. La tabla 'cliente' tiene registros\n2. El procedimiento 'sp_ListarClientes' existe\n3. La conexión a BD es correcta", 
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comboBox1.DataSource = new List<Cliente>();
                            comboBox1.Enabled = false;
                        }
                        else
                        {
                            comboBox1.DataSource = clientes;
                            comboBox1.DisplayMember = "Nombre";
                            comboBox1.ValueMember = "ID_cliente";
                            comboBox1.Enabled = true;
                            MessageBox.Show($"✅ Se cargaron {clientes.Count} clientes correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception exClientes)
                    {
                        MessageBox.Show($"❌ Error al cargar clientes:\n\n{exClientes.Message}\n\nDetalles:\n{exClientes.InnerException?.Message}", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox1.Enabled = false;
                    }
                }

                // Cargar productos en comboBox2
                if (comboBox2 != null)
                {
                    try
                    {
                        var productos = productosBL.Listar();
                        
                        if (productos == null || productos.Count == 0)
                        {
                            MessageBox.Show("No hay productos disponibles en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comboBox2.DataSource = new List<Productos>();
                        }
                        else
                        {
                            comboBox2.DataSource = productos;
                            comboBox2.DisplayMember = "Nombre_Producto";
                            comboBox2.ValueMember = "ID_Producto";
                        }
                    }
                    catch (Exception exProductos)
                    {
                        MessageBox.Show($"Error al cargar productos: {exProductos.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Cargar categorías en comboBox3
                if (comboBox3 != null)
                {
                    try
                    {
                        var categorias = categoriaBL.Listar();
                        
                        if (categorias == null || categorias.Count == 0)
                        {
                            MessageBox.Show("No hay categorías disponibles en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comboBox3.DataSource = new List<Categorias>();
                        }
                        else
                        {
                            comboBox3.DataSource = categorias;
                            comboBox3.DisplayMember = "Nombre_Categoria";
                            comboBox3.ValueMember = "ID_categoria";
                        }
                    }
                    catch (Exception exCategorias)
                    {
                        MessageBox.Show($"Error al cargar categorías: {exCategorias.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error de conexión al cargar ComboBox: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al cargar los ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVentas()
        {
            var ventas = ventasBL.ListarVentas();
            dataGridView1.DataSource = ventas;

            // 🔥 OCULTAR IDS
            if (dataGridView1.Columns.Contains("ID_venta"))
                dataGridView1.Columns["ID_venta"].Visible = false;

            if (dataGridView1.Columns.Contains("ID_cliente"))
                dataGridView1.Columns["ID_cliente"].Visible = false;

            // Si también existe Estado_venta
            if (dataGridView1.Columns.Contains("Estado_venta"))
                dataGridView1.Columns["Estado_venta"].Visible = false;

            // Encabezados bonitos
            if (dataGridView1.Columns.Contains("Nombre"))
                dataGridView1.Columns["Nombre"].HeaderText = "Cliente";

            if (dataGridView1.Columns.Contains("Nombre_Producto"))
                dataGridView1.Columns["Nombre_Producto"].HeaderText = "Producto";

            if (dataGridView1.Columns.Contains("Fecha_venta"))
                dataGridView1.Columns["Fecha_venta"].HeaderText = "Fecha";

            if (dataGridView1.Columns.Contains("Total_general"))
                dataGridView1.Columns["Total_general"].HeaderText = "Total";
        }

        private bool ValidarComboBoxes(params ComboBox[] comboBoxes)
        {
            foreach (var comboBox in comboBoxes)
            {
                if (comboBox == null)
                {
                    MessageBox.Show("Control ComboBox no inicializado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!comboBox.Enabled || comboBox.Items.Count == 0)
                {
                    return false;
                }

                if (comboBox.SelectedIndex == -1 || comboBox.SelectedItem == null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidarCantidad(string cantidad)
        {
            if (string.IsNullOrWhiteSpace(cantidad))
            {
                MessageBox.Show("La cantidad no puede estar vacía", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(cantidad, out int cantidadNum) || cantidadNum <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número mayor a cero", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Botón Crear - Crea una nueva venta
            try
            {
                if (!componentesInicializados)
                {
                    MessageBox.Show("Los componentes de negocio no están disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validación especial para comboBox1
                if (comboBox1 == null || !comboBox1.Enabled)
                {
                    MessageBox.Show("Los clientes no están disponibles en este momento", 
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un cliente válido", 
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarComboBoxes(comboBox2))
                {
                    MessageBox.Show("Debe seleccionar un producto válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBox2.Items.Count == 0)
                {
                    MessageBox.Show("No hay productos disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear la venta con los datos seleccionados
                int idCliente = 0;
                if (int.TryParse(comboBox1.SelectedValue?.ToString() ?? "0", out int clienteId))
                {
                    idCliente = clienteId;
                }

                if (idCliente == 0)
                {
                    MessageBox.Show("Error: No se pudo obtener el ID del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Ventas venta = new Ventas
                {
                    ID_cliente = idCliente,
                    Fecha_venta = DateTime.Now,
                    Total_general = 0,
                    Estado_venta = true
                };

                ventasBL.InsertarVenta(venta);
                MessageBox.Show("Nueva venta creada exitosamente para el cliente: " + comboBox1.Text, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarVentas();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al crear venta: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Botón Añadir - Añade un producto a la venta seleccionada
            try
            {
                if (!componentesInicializados)
                {
                    MessageBox.Show("Los componentes de negocio no están disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener la venta seleccionada
                int? idVenta = ObtenerIDVentaSeleccionada();
                if (!idVenta.HasValue)
                {
                    return;
                }

                if (comboBox2.Items.Count == 0)
                {
                    MessageBox.Show("No hay productos disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ValidarComboBoxes(comboBox2))
                {
                    MessageBox.Show("Debe seleccionar un producto válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCantidad(textBox1.Text))
                {
                    return;
                }

                if (comboBox2.SelectedValue == null || !int.TryParse(comboBox2.SelectedValue.ToString(), out int idProducto))
                {
                    MessageBox.Show("Error al obtener el ID del producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int cantidad = int.Parse(textBox1.Text);

                // Obtener el precio del producto
                var producto = productosBL.Listar().FirstOrDefault(p => p.ID_Producto == idProducto);
                if (producto == null)
                {
                    MessageBox.Show("No se pudo obtener la información del producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insertar el detalle de venta
                Detalle_Venta detalle = new Detalle_Venta
                {
                    ID_venta = idVenta.Value,
                    ID_producto = idProducto,
                    Cantidad = cantidad,
                    Precio = producto.Precio_Producto
                };

                var detalleVentasBL = new Detalle_VentasBL();
                detalleVentasBL.InsertarDetalle(detalle);
                MessageBox.Show($"Producto '{comboBox2.Text}' (Cantidad: {cantidad}) añadido a la venta exitosamente", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                CargarVentas();
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Error al convertir datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Botón A Facturar - Abre el reporte
            try
            {
                int? idVenta = ObtenerIDVentaSeleccionada();
                
                if (!idVenta.HasValue)
                {
                    MessageBox.Show("Debe seleccionar una venta para facturar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    ReporteVenta reporte = new ReporteVenta(idVenta.Value);
                    if (reporte == null)
                    {
                        MessageBox.Show("No se pudo crear el formulario de reporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    reporte.ShowDialog();
                }
                catch (FileNotFoundException fnEx)
                {
                    MessageBox.Show("Archivo de reporte no encontrado: " + fnEx.Message, "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exReporte)
                {
                    MessageBox.Show("Error al abrir el reporte: " + exReporte.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la facturación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            // Botón Eliminar - Desactiva una venta seleccionada
            try
            {
                if (!componentesInicializados)
                {
                    MessageBox.Show("Los componentes de negocio no están disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? idVenta = ObtenerIDVentaSeleccionada();
                
                if (!idVenta.HasValue)
                {
                    return;
                }

                if (MessageBox.Show("¿Está seguro que desea eliminar la venta ID: " + idVenta + "?", 
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                ventasBL.DesactivarVenta(idVenta.Value);
                
                MessageBox.Show("Venta eliminada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarVentas();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error SQL al eliminar venta: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            // Botón Actualizar - Actualiza la venta seleccionada
            try
            {
                if (!componentesInicializados)
                {
                    MessageBox.Show("Los componentes de negocio no están disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? idVenta = ObtenerIDVentaSeleccionada();
                
                if (!idVenta.HasValue)
                {
                    return;
                }

                MessageBox.Show("Funcionalidad de actualización pendiente de implementar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
