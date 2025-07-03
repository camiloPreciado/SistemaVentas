using System;
using System.Configuration;
using System.Data.SqlClient;
using inventario.api.Models;

namespace inventario.api.Data
{
    public class InventarioRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public InventarioDto ObtenerPorProductoId(int productoId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Inventario WHERE ProductoId = @ProductoId", conn);
                cmd.Parameters.AddWithValue("@ProductoId", productoId);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new InventarioDto
                    {
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"]
                    };
                }
            }

            return null;
        }

        public InventarioDto ActualizarInventario(InventarioDto inventario)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var cmd = new SqlCommand(@"
                    IF EXISTS (SELECT 1 FROM Inventario WHERE ProductoId = @ProductoId)
                        UPDATE Inventario SET Cantidad = @Cantidad WHERE ProductoId = @ProductoId
                    ELSE
                        INSERT INTO Inventario (ProductoId, Cantidad) VALUES (@ProductoId, @Cantidad)
                ", conn);

                cmd.Parameters.AddWithValue("@ProductoId", inventario.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", inventario.Cantidad);

                cmd.ExecuteNonQuery();
            }

            return inventario;
        }
    }
}
