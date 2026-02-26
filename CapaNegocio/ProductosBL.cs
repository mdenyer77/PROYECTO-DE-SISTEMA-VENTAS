using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductosBL
    {
        private ProductoDAL dal = new ProductoDAL();

        
        public List<Productos> Listar()
        {
            return dal.Listar();
        }

         
        public void Agregar(Productos producto)
        {
            if (producto.ID_Producto == 0)
                dal.Insertar(producto);
            else
                dal.Actualizar(producto);
        }

        
        public void Eliminar(int id)
        {
            dal.Desactivar(id);
        }
    }  
}
