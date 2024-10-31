using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivrosRepository _livrosRepository;
        private object livro;

        public LivrosController(LivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

        // Método para listar todos os livros
        [HttpGet("listar-livros")]
        public async Task<IActionResult> ListarLivros()
        {
            var livros = await _livrosRepository.ListarTodosLivros();
            return Ok(livros);
        }

        // Método para cadastrar um novo livro
        [HttpPost("cadastrar-livro")]
        public async Task<IActionResult> CadastrarLivro([FromBody] Livros livro)
        {
            if (livro == null)
            {
                return BadRequest(new { mensagem = "Dados do livro são inválidos" });
            }

            await _livrosRepository.CadastrarLivro(livro);
            return Ok(new { mensagem = "Livro cadastrado com sucesso" });
        }

        // Método para atualizar informações de um livro
        [HttpPut("atualizar-livro/{id}")]
        public async Task<IActionResult> AtualizarLivro(int id, [FromBody] Livros livroAtualizado)
        {
            int v = await _livrosRepository.AtualizarLivro(id, livroAtualizado);
            if (livro == null)
            {
                return  BadRequest(new { mensagem = "Livro não encontrado" });
            }
            return Ok(new { mensagem = "Livro atualizado com sucesso" });
        }


        // Método para excluir um livro
        [HttpDelete("excluir-livro/{id}")]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            var resultado = await _livrosRepository.ExcluirLivro(id);
            if (resultado == 0) // ou o que você usar para indicar que o livro não foi encontrado
            {
                return NotFound(new { mensagem = "Livro não encontrado" });
            }

            return Ok(new { mensagem = "Livro excluído com sucesso" });
        }
    }
}
