using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Detalle_VentasDAL  
    {
        public List<Detalle_Venta> Listar()
        {
            List<Detalle_Venta> lista = new List<Detalle_Venta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_listar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Detalle_Venta
                    {
                        ID_detalle_venta = Convert.ToInt32(dr["ID_detalle_venta"]),
                        ID_venta = Convert.ToInt32(dr["ID_venta"]),
                        ID_producto = Convert.ToInt32(dr["ID_producto"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Precio = Convert.ToDecimal(dr["Precio"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    });
                }
            }

            return lista;
        }

    public DataTable ObtenerReporteVenta(int idVenta)
    {
        DataTable dt = new DataTable();

        using (SqlConnection cn = Conexion.ObtenerConexion())
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT 
                    c.ID_cliente,
                    c.Nombre AS Nombre_Cliente,
                    p.ID_Producto AS ID_producto,
                    p.Nombre_Producto AS Nombre_Producto,
                    dv.Cantidad,
                    dv.Precio,
                    (dv.Cantidad * dv.Precio) AS Sub_Total,
                    v.Total_general
                FROM Detalle_Venta dv
                INNER JOIN venta v ON dv.ID_venta = v.ID_venta
                INNER JOIN cliente c ON v.ID_cliente = c.ID_cliente
                INNER JOIN productos p ON dv.ID_producto = p.ID_Producto
                WHERE v.ID_venta = @ID_venta
                ORDER BY dv.ID_detalle_venta", cn);
            
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID_venta", idVenta);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
        }

        return dt;
    }

        public void Insertar(Detalle_Venta detalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_venta", detalle.ID_venta);
                cmd.Parameters.AddWithValue("@ID_producto", detalle.ID_producto);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@Precio", detalle.Precio);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Detalle_Venta detalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_detalle_venta", detalle.ID_detalle_venta);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@Precio", detalle.Precio);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Desactivar(int idDetalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_desactivar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_detalle_venta", idDetalle);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
