using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
   public class Detalle_Venta
    {
        public int ID_detalle_venta { get; set; }
        public int ID_venta { get; set; }
        public int ID_producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }
}
