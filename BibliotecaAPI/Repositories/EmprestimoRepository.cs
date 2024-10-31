using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class EmprestimoRepository
    {
        private readonly string _connectionString;

        public EmprestimoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> RegistrarEmprestimo(int usuarioId, int livroId)
        {
    
            
                var sql = "INSERT INTO Emprestimos (LivroId, UsuarioId,DataEmprestimo,DataDevolucao) " +
                "VALUES (@LivroId, @UsuarioId,@DataEmprestimo,@DataDevolucao);";

            DateTime dataDevolucao = DateTime.Now.AddDays(14);
                using (var conn = Connection)
                {
                    return await conn.ExecuteAsync(sql, new { LivroId = livroId, UsuarioId= usuarioId, DataEmprestimo= DateTime.Now, DataDevolucao = dataDevolucao });
                }
            
        }


        public async Task<int> RegistrarDevolucao(int livroId)
        {
            using (var conn = Connection)
            {
                using (var transaction = conn.BeginTransaction())
                {
                    var sqlDevolucao = "UPDATE Emprestimos SET DataDevolucao = NOW() WHERE LivroId = @LivroId AND DataDevolucao IS NULL";
                    await conn.ExecuteScalarAsync(sqlDevolucao, new { LivroId = livroId }, transaction);

                    var sqlAtualizarLivro = "UPDATE Livros SET Disponivel = true WHERE Id = @LivroId;";
                    await conn.ExecuteAsync(sqlAtualizarLivro, new { LivroId = livroId }, transaction);

                    transaction.Commit();

                    return livroId;
                }
            }
        }
        public async Task<IEnumerable<Emprestimo>> ListarHistoricoEmprestimo()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Emprestimos emp WHERE emp.DataEmprestimo IS NOT NULL";
                return await conn.QueryAsync<Emprestimo>(sql);
            }

        }




    }
}
