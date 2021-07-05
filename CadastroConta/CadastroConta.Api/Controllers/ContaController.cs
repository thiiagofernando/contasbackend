using CadastroConta.Api.ViewModels;
using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CadastroConta.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _repository;
        public ContaController(IContaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("CadastrarNovaConta")]
        [Authorize]
        public ActionResult CadastrarNovaConta([FromBody] ContaRequestViewModel conta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound(conta);
                }
                int diasAtraso = _repository.CalcularDiasEmAtraso(conta.DataVencimento, conta.DataPagamento);
                decimal valorCorrigido = _repository.CalcularMultaEJuros(diasAtraso, conta.ValorOriginal);
                var novaconta = new Conta
                {
                    Nome = conta.Nome,
                    DiasEmAtraso = diasAtraso,
                    ValorOriginal = conta.ValorOriginal,
                    ValorCorrigido = valorCorrigido,
                    DataPagamento = conta.DataPagamento,
                    DataVencimento = conta.DataVencimento,
                    PagamentoRealizado = false
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
        [Route("ListarContasCadastradas")]
        [Authorize]
        public ActionResult<List<ContaResponseViewModel>> ListarContasCadastradas()
        {
            try
            {
                var lista = _repository.ListarTodasAsContas()
                                       .Select(p => new ContaResponseViewModel
                                       {
                                           Nome = p.Nome,
                                           ValorOriginal = p.ValorOriginal.ToString("N2"),
                                           ValorCorrigido = p.ValorCorrigido.ToString("N2"),
                                           DiasEmAtraso = p.DiasEmAtraso,
                                           DataPagamento = p.DataPagamento.ToString("dd/MM/yyyy HH:mm:ss"),
                                           PagamentoRealizado = p.PagamentoRealizado == true ? "Sim" : "Não"
                                       });
                return Ok(lista);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível obter  a lista de contas" });
            }
        }
        [HttpGet]
        [Route("ObterContaCadastrada/{id}")]
        [Authorize]
        public IActionResult ObterContaCadastrada(Guid id)
        {
            try
            {
                var conta = _repository.ObterPorId(id);
                return Ok(conta);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível obter  a conta selecionada" });
            }
        }
        [HttpPut]
        [Route("AtualizarContaCadastrada")]
        [Authorize]
        public IActionResult AtualizarConta([FromBody] ContaRequestViewModel conta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound(conta);
                }
                int diasAtraso = _repository.CalcularDiasEmAtraso(conta.DataVencimento, conta.DataPagamento);
                decimal valorCorrigido = _repository.CalcularMultaEJuros(diasAtraso, conta.ValorOriginal);
                var atualizarConta = new Conta
                {
                    Id = (Guid)conta.Id,
                    Nome = conta.Nome,
                    DiasEmAtraso = diasAtraso,
                    ValorOriginal = conta.ValorOriginal,
                    ValorCorrigido = valorCorrigido,
                    DataPagamento = conta.DataPagamento,
                    DataVencimento = conta.DataVencimento,
                    PagamentoRealizado = conta.PagamentoRealizado == 1 ? true : false
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
        [Route("ExcluirContaCadastrada/{id}")]
        [Authorize]
        public IActionResult ExcluirConta(Guid id)
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
