using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {

        private readonly EmprestimoRepository _emprestimoRepository;

        public EmprestimoController(EmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }
        // POST api/<VeiculosController>
        [HttpPost("registrar-emprestimo-veiculos")]
        public async Task<IActionResult> RegistrarEmprestimo([FromBody] Emprestimo emprestimo)
        {
            var dados = await _emprestimoRepository.RegistrarEmprestimo(emprestimo.UsuarioId, emprestimo.LivroId);

            return Ok(new { mensagem = " Emprestimo registrado com Sucesso" });
        }

        [HttpPost("registrar-devolucao-livro")]
        public async Task<IActionResult> RegistrarDevolucao([FromBody] Emprestimo emprestimos)
        {

            var dados1 = await _emprestimoRepository.RegistrarDevolucao(emprestimos.LivroId);
            emprestimos.DataDevolucao = DateTime.Now;

            return Ok(new { mensagem = " Livro Devolvido" });
        }

        [HttpGet("historico-livros")]
        public async Task<IActionResult> ListarHistoricoEmprestimo()
        {
            var emprestimos = await _emprestimoRepository.ListarHistoricoEmprestimo();
            return Ok(emprestimos);
        }
    }
}

