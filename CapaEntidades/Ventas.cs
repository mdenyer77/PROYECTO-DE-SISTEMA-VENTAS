using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Ventas
    {
        public int ID_venta { get; set; }
        public DateTime Fecha_venta { get; set; }
        public int ID_cliente { get; set; }
        public decimal Total_general { get; set; }
        public bool Estado_venta { get; set; }
        public string Nombre { get; set; }
        public string Nombre_Producto { get; set; }
    }
}
