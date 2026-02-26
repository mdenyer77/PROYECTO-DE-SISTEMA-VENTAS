using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CategoriaDAL
    {
        public List<Categorias> Listar()
        {
            List<Categorias> lista = new List<Categorias>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_listar_categoria", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categorias
                        {
                            ID_categoria = dr["ID_categoria"] != DBNull.Value ? Convert.ToInt32(dr["ID_categoria"]) : 0,
                            Nombre_Categoria = dr["Nombre_Categoria"]?.ToString(),
                            Descripcion = dr["Descripcion"]?.ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public void Insertar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Crear_Categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NOMBRE_CAT", categoria.Nombre_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_categoria", categoria.ID_categoria);
                cmd.Parameters.AddWithValue("@Nombre_categoria", categoria.Nombre_Categoria);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_desactivar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_categoria", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
