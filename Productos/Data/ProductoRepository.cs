using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using productos.api.Models;

namespace productos.api.Data
{
    public class ProductoRepository
    {
        //private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        string _connectionString = Environment.GetEnvironmentVariable("CONEXION_SQL");

        public IEnumerable<ProductoDto> ObtenerTodos()
        {
            var lista = new List<ProductoDto>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Productos", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new ProductoDto
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Precio = (int)reader["Precio"],
                        Descripcion = reader["Descripcion"]?.ToString()
                    });
                }
            }

            return lista;
        }

        public ProductoDto ObtenerPorId(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Productos WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new ProductoDto
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Precio = (int)reader["Precio"],
                        Descripcion = reader["Descripcion"]?.ToString()
                    };
                }
            }

            return null;
        }

        public ProductoDto Crear(ProductoDto producto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Productos (Nombre, Precio, Descripcion) OUTPUT INSERTED.Id VALUES (@Nombre, @Precio, @Descripcion)",
                    conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);

                producto.Id = (int)cmd.ExecuteScalar();
            }

            return producto;
        }
    }
}
