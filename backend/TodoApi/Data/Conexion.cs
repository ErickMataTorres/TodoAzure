using Microsoft.Data.SqlClient;

namespace TodoApi.Data
{
    public class Conexion
    {
        private readonly string _cadenaConexion;

        public Conexion(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("ToDoListDB")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión ToDoListDB.");
        }

        public SqlConnection CrearConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
