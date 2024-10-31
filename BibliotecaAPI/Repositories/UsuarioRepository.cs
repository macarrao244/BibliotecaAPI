using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> CadastrarUsuario(Usuarios usuario)
        {
            var sql = "INSERT INTO Usuarios (Nome, Email) VALUES (@Nome, @Email);";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, usuario);
            }
        }

    }
}
