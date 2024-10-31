using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class LivrosRepository
    {
        private readonly string _connectionString;

        public LivrosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        // Método para cadastrar um livro
        public async Task<int> CadastrarLivro(Livros livro)
        {
            var sql = "INSERT INTO Livros (Titulo, Autor, AnoPublicacao, Genero, Disponivel) " +
                      "VALUES (@Titulo, @Autor, @AnoPublicacao, @Genero, @Disponivel);";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, livro);
            }
        }

        // Método para listar todos os livros
        public async Task<IEnumerable<Livros>> ListarTodosLivros()
        {
            var sql = "SELECT * FROM Livros;";
            using (var conn = Connection)
            {
                return await conn.QueryAsync<Livros>(sql);
            }
        }

        // Método para atualizar um livro
        public async Task<int> AtualizarLivro(int id, Livros livro)
        {
            var sql = "UPDATE Livros SET Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao, Genero = @Genero " +
                      "WHERE Id = @Id;";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, livro);
            }
        }

        // Método para excluir um livro
        public async Task<int> ExcluirLivro(int livroId)
        {
            var sql = "DELETE FROM Livros WHERE Id = @Id AND Disponivel = true;";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = livroId });
            }
        }
    }
}
