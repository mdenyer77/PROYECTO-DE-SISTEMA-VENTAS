using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ClienteDAL
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        ID_cliente = Convert.ToInt32(dr["ID_cliente"]),
                        Nombre = dr["Nombre"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString()
                    });
                }
                dr.Close();
            }

            return lista;
        }

        public void Insertar(Cliente cliente)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre ?? "");
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion ?? "");
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? "");
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo ?? "");

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Cliente cliente)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_cliente", cliente.ID_cliente);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre ?? "");
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion ?? "");
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? "");
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo ?? "");

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_cliente", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
