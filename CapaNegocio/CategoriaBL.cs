using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Negocio
{
    public class CategoriaBL
    {
        private CategoriaDAL dal = new CategoriaDAL();

        public List<Categorias> Listar()
        {
            return dal.Listar();
        }

        public void Agregar(Categorias categoria)
        {
            if (categoria.ID_categoria == 0)
                dal.Insertar(categoria);
            else
                dal.Actualizar(categoria);
        }

        public void Actualizar(Categorias categoria)
        {
            if (categoria.ID_categoria <= 0)
                throw new ArgumentException("El ID de categoría debe ser válido para actualizar");
            
            dal.Actualizar(categoria);
        }

        public void Eliminar(int id)
        {
            dal.Desactivar(id);
        }
    }
}

