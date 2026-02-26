using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class VentasDAL
    {
        public List<Ventas> Listar()
        {
            List<Ventas> lista = new List<Ventas>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_listar_ventas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Ventas
                    {
                        ID_venta = Convert.ToInt32(dr["ID_venta"]),
                        Fecha_venta = Convert.ToDateTime(dr["Fecha_venta"]),
                        ID_cliente = Convert.ToInt32(dr["ID_cliente"]),
                        Total_general = Convert.ToDecimal(dr["Total_general"]),
                        Estado_venta = Convert.ToBoolean(dr["Estado_venta"]),
                        Nombre = dr["Nombre_Cliente"].ToString(),
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),

                    });
                }
                dr.Close();
            }

            return lista;
        }

        public DataTable ListarVentasConCliente()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarVentasConCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }

        public void Insertar(Ventas venta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_cliente", venta.ID_cliente);
                cmd.Parameters.AddWithValue("@Total_general", venta.Total_general);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Ventas venta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_venta", venta.ID_venta);
                cmd.Parameters.AddWithValue("@ID_cliente", venta.ID_cliente);
                cmd.Parameters.AddWithValue("@Total_general", venta.Total_general);
                cmd.Parameters.AddWithValue("@Estado_venta", venta.Estado_venta);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int idVenta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_desactivar_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_venta", idVenta);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
