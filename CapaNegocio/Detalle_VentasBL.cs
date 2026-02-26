using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Detalle_VentasBL
    {
        Detalle_VentasDAL detalleDAL = new Detalle_VentasDAL();

        public void InsertarDetalle(Detalle_Venta detalle)
        {
            detalleDAL.Insertar(detalle);
        }

        public void ActualizarDetalle(Detalle_Venta detalle)
        {
            detalleDAL.Actualizar(detalle);
        }

        public void DesactivarDetalle(int idDetalle)
        {
            detalleDAL.Desactivar(idDetalle);
        }

        public List<Detalle_Venta> ListarDetalle()
        {
            return detalleDAL.Listar();
        }

        public DataTable ObtenerReporteVenta(int idVenta)
        {
            return detalleDAL.ObtenerReporteVenta(idVenta);
        }
    }
}
