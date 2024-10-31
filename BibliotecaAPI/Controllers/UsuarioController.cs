using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class UsuariosController : ControllerBase
        {
            private readonly UsuarioRepository _usuariosRepository;

            public UsuariosController(UsuarioRepository usuariosRepository)
            {
                _usuariosRepository = usuariosRepository;
            }

            // Método para cadastrar um novo usuario
            [HttpPost("cadastrar-usuario")]
            public async Task<IActionResult> CadastrarUsuario([FromBody] Usuarios usuario)
            {

                await _usuariosRepository.CadastrarUsuario(usuario);
                return Ok(new { mensagem = "Usuario cadastrado com sucesso" });
            }


        }
    }

