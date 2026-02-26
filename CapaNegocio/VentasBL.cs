using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class VentasBL
    {
        VentasDAL ventasDAL = new VentasDAL();

        public void InsertarVenta(Ventas venta)
        {
            ventasDAL.Insertar(venta);
        }

        public void ActualizarVenta(Ventas venta)
        {
            ventasDAL.Actualizar(venta);
        }

        public void DesactivarVenta(int idVenta)
        {
            ventasDAL.Desactivar(idVenta);
        }

        public List<Ventas> ListarVentas()
        {
            return ventasDAL.Listar();
        }
    }
}
