using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Negocio
{
    public class ClienteBL
    {
        private ClienteDAL dal = new ClienteDAL();

        public List<Cliente> Listar()
        {
            try
            {
                return dal.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al listar clientes: " + ex.Message);
            }
        }

        public void Agregar(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new ArgumentNullException("cliente", "El cliente no puede ser nulo");
                }

                if (string.IsNullOrWhiteSpace(cliente.Nombre))
                {
                    throw new ArgumentException("El nombre del cliente es requerido");
                }

                if (cliente.ID_cliente == 0)
                    dal.Insertar(cliente);
                else
                    dal.Actualizar(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al agregar cliente: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID del cliente debe ser mayor a 0");
                }

                dal.Desactivar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al eliminar cliente: " + ex.Message);
            }
        }
    }
}
