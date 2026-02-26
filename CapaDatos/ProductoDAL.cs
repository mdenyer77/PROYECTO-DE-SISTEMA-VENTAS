using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ProductoDAL
    {
        public List<Productos> Listar()
        {
            List<Productos> lista = new List<Productos>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_listar_productos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Productos
                    {
                        ID_Producto = Convert.ToInt32(dr["ID_Producto"]),
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Precio_Producto = Convert.ToDecimal(dr["Precio_Producto"]),
                        stock = Convert.ToInt32(dr["stock"]),
                        ID_categoria = Convert.ToInt32(dr["ID_categoria"]),
                        Nombre_categoria = dr["Nombre_categoria"].ToString()
                    });
                }
                dr.Close();
            }

            return lista;
        }

        public void Insertar(Productos producto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_productos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);
                cmd.Parameters.AddWithValue("@Precio_Producto", producto.Precio_Producto);
                cmd.Parameters.AddWithValue("@stock", producto.stock);
                cmd.Parameters.AddWithValue("@ID_categoria", producto.ID_categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Productos producto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_productos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Producto", producto.ID_Producto);
                cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);
                cmd.Parameters.AddWithValue("@Precio_Producto", producto.Precio_Producto);
                cmd.Parameters.AddWithValue("@stock", producto.stock);
                cmd.Parameters.AddWithValue("@ID_categoria", producto.ID_categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_desactivar_productos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Producto", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
