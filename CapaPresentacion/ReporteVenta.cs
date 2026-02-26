using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class ReporteVenta : Form
    {
        private int idVentaSeleccionada = 0;
        private Detalle_VentasBL detalleVentasBL;

        public ReporteVenta()
        {
            InitializeComponent();
            detalleVentasBL = new Detalle_VentasBL();
        }

        public ReporteVenta(int idVenta)
        {
            InitializeComponent();
            idVentaSeleccionada = idVenta;
            detalleVentasBL = new Detalle_VentasBL();
        }

        private void ReporteVenta_Load(object sender, EventArgs e)
        {
            try
            {
                if (idVentaSeleccionada > 0)
                {
                    DataTable dtReporte = detalleVentasBL.ObtenerReporteVenta(idVentaSeleccionada);

                    if (dtReporte == null || dtReporte.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos disponibles para esta venta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.reportViewer1.LocalReport.DataSources.Clear();
                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = 
                        new Microsoft.Reporting.WinForms.ReportDataSource("VentasYFacturasReporte", dtReporte);
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    MessageBox.Show("No se especificó una venta para mostrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message + "\n\nDetalles: " + ex.InnerException?.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
