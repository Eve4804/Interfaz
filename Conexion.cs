using System;
using Npgsql;

namespace Interfaz
{
    public class Conexion
    {
        private readonly string _connectionString;

        // Constructor: inicializa la cadena de conexión
        public Conexion()
        {
            _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=Moduloerp";
        }

        // Método para obtener una conexión abierta
        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                Console.WriteLine("Conexión exitosa a PostgreSQL 🎉");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
                throw;
            }
            return conn;
        }

        // Ejemplo de consulta simple
        public void ProbarConexion()
        {
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand("SELECT version();", conn))
            {
                var version = cmd.ExecuteScalar();
                Console.WriteLine($"Versión de PostgreSQL: {version}");
            }
        }
    }
}