using CadastroConta.Api.ViewModels;
using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CadastroConta.Business.Services;

namespace CadastroConta.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AuthenticatedUser _user;
        public ContaController(IContaRepository repository,AuthenticatedUser user,IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _user = user;
        }

        [HttpPost]
        [Route("cadastrar")]
        [Authorize]
        public ActionResult CadastrarNovaConta([FromBody] ContaDto conta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound(conta);
                }
                if (conta.PagamentoRealizado == true && conta.DataPagamento == null)
                    conta.DataPagamento = DateTime.Now;
                
                var novaconta = new ContaModel
                {
                    Descricao = conta.Descricao,
                    Valor = conta.Valor,
                    DataPagamento = conta.DataPagamento,
                    EstabelecimentoId = conta.EstabelecimentoId,
                    UsuarioId = conta.UsuarioId,
                    PagamentoRealizado = conta.PagamentoRealizado
                };
                _repository.Adicionar(novaconta);
                return Ok(novaconta);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar a conta" });
            }
        }

        [HttpGet]
        [Route("listartodos")]
        [Authorize]
        public ActionResult<List<ContaDto>> ListarContasCadastradas()
        {
            try
            {
                var logado = _usuarioRepository.ObterUsuarioPorLogin(_user.UsuarioLogado.Name);
                if (logado == null)
                    return BadRequest(new { message = "Não foi possível obter  a lista de contas" });
                
                var lista = _repository.ListarTodasAsContas(logado.Id)
                                       .Select(p => new ContaDto
                                       {
                                           Id = p.Id,
                                           Descricao = p.Descricao,
                                           Valor = p.Valor,
                                           EstabelecimentoId = p.EstabelecimentoId,
                                           UsuarioId = p.UsuarioId,
                                           DescricaoEstabelecimento = p.Estabelecimento?.Descricao,
                                           DataPagamento = p.DataPagamento,
                                           PagamentoRealizado = p.PagamentoRealizado
                                       }).OrderBy(x => x.Id);
                return Ok(lista);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível obter  a lista de contas" });
            }
        }
        [HttpGet]
        [Route("obterporcodigo/{id}")]
        [Authorize]
        public IActionResult ObterContaCadastrada(int id)
        {
            try
            {
                var cont = _repository.ObterPorId(id);
                return Ok(cont);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível obter  a conta selecionada" });
            }
        }
        [HttpPut]
        [Route("atualizar")]
        [Authorize]
        public IActionResult AtualizarConta([FromBody] ContaDto conta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound(conta);
                }
                if (conta.PagamentoRealizado == true && conta.DataPagamento == null)
                    conta.DataPagamento = DateTime.Now;
                
                var atualizarConta = new ContaModel
                {
                    Id = conta.Id,
                    Descricao = conta.Descricao,
                    Valor = conta.Valor,
                    DataPagamento = conta.DataPagamento,
                    EstabelecimentoId = conta.EstabelecimentoId,
                    UsuarioId = conta.UsuarioId,
                    PagamentoRealizado = conta.PagamentoRealizado 
                };
                _repository.Atualizar(atualizarConta);
                return Ok(atualizarConta);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a conta" });
            }
        }

        [HttpDelete]
        [Route("excluir/{id}")]
        [Authorize]
        public IActionResult ExcluirConta(int id)
        {
            try
            {
                _repository.Excluir(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível excluir  a conta selecionada" });
            }
        }
    }
}
