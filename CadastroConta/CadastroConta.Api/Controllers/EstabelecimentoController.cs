using System;
using CadastroConta.Api.ViewModels;
using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroConta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstabelecimentoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AuthenticatedUser _user;
        private readonly IEstabelecimentoRepository _repository;

        public EstabelecimentoController(IEstabelecimentoRepository repository, AuthenticatedUser user,
            IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _user = user;
            _repository = repository;
        }

        [HttpGet]
        [Route("listar")]
        [Authorize]
        public IActionResult ListarEstabelecimentos()
        {
            try
            {
                var logado = _usuarioRepository.ObterUsuarioPorLogin(_user.UsuarioLogado.Name);
                if(logado == null)
                    return BadRequest(new { message = "Não foi possível obter  a lista de contas" });
                var lista = _repository.ListaEstabelecimento(logado.Id);
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível obter  a lista de contas" });
            }
        }

        [HttpPost]
        [Route("novo")]
        [Authorize]
        public IActionResult NovoEstabelecimento([FromBody] EstabelecimentoDto estabelecimentoDto)
        {
            if(!ModelState.IsValid)
                return NotFound(estabelecimentoDto);
            try
            {
                var est = new EstabelecimentoModel()
                {
                    Descricao = estabelecimentoDto.Descricao,
                    UsuarioId = estabelecimentoDto.UsuarioId
                };
                _repository.Adicionar(est);
                return Ok(est);
            }
            catch (Exception )
            {
                return BadRequest(new { message = "Não foi possível cadastrar o estabelecimento" });
            }
        }
    }
}