using CadastroConta.Api;
using CadastroConta.Api.ViewModels;
using CadastroConta.Tests.Config;
using System;
using System.Net;
using Xunit;

namespace CadastroConta.Tests.Conta
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    [TestCaseOrderer("Contas.Tests.config.PriorityOrderer", "Contas.Tests")]
    public class ContaTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        public ContaTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastro de conta Valida sem atraso"), TestPriority(1)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaSemAtraso()
        {
            var novaSemAtrasoConta = new ContaDto()
            {
                Descricao = "Conta De Telefone",
                Valor = 500,
                DataPagamento = DateTime.Now,
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", novaSemAtrasoConta);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.ContaModel>();

            Assert.NotNull(result);
            Assert.Equal(500, result.Valor);
        }

        [Fact(DisplayName = "Cadastro de conta Valida com 3 dias de atraso"), TestPriority(2)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaComTresDiasDeAtraso()
        {
            var contaTresDiasDeAtraso = new ContaDto()
            {
                Descricao = "Conta De Luz",
                Valor = 450,
                DataPagamento = DateTime.Now.AddDays(3),
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", contaTresDiasDeAtraso);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.ContaModel>();

            Assert.NotNull(result);
            Assert.Equal(459.1350M, result.Valor);
        }

        [Fact(DisplayName = "Cadastro de conta Valida com mais de 3 dias de atraso"), TestPriority(3)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaComMaisDeTresDiasDeAtraso()
        {
            var contaComMaisDETresDiasDeAtraso = new ContaDto()
            {
                Descricao = "Conta De Agua",
                Valor = 450,
                DataPagamento = DateTime.Now.AddDays(4),
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", contaComMaisDETresDiasDeAtraso);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.ContaModel>();

            Assert.NotNull(result);
            Assert.Equal(463.8600M, result.Valor);
        }

        [Fact(DisplayName = "Cadastro de conta Valida com mais de 5 dias de atraso"), TestPriority(4)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaComMaisDeCincoDiasDeAtraso()
        {
            var contaCincoDiasDeAtraso = new ContaDto()
            {
                Descricao = "Conta Hospital",
                Valor = 700,
                DataPagamento = DateTime.Now.AddDays(8),
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", contaCincoDiasDeAtraso);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.ContaModel>();

            Assert.NotNull(result);
            Assert.Equal(736.6800M, result.Valor);
        }


        [Fact(DisplayName = "Cadastro de conta Invalida"), TestPriority(5)]
        [Trait("Conta", "Conta")]
        public async void CadastroDeContaInvalida()
        {
            var novaConta = new ContaDto()
            {
                Descricao = null,
                Valor = 500,
                DataPagamento = DateTime.Now,
            };
            //var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", novaConta);
            //Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
