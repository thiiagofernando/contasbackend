using CadastroConta.Api;
using CadastroConta.Api.ViewModels;
using CadastroConta.Tests.Config;
using System.Net;
using Xunit;

namespace CadastroConta.Tests.Autenticacao
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    [TestCaseOrderer("Contas.Tests.config.PriorityOrderer", "Contas.Tests")]
    public class AutenticacaoTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        public AutenticacaoTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }
        [Fact(DisplayName = "Gerando Token de Acesso Valido"), TestPriority(1)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void GerarTokenValidoParaAutenticacao()
        {
            var login = new UsuarioLoginDto()
            {
                Login = "teste",
                Senha = "12345678"
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/Login", login);
            response.EnsureSuccessStatusCodeOrResponseException();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Gerando Token de Acesso Invalido"), TestPriority(2)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void GerarTokenInalidoParaAutenticacao()
        {
            var login = new UsuarioLoginDto()
            {
                Login = "teste2",
                Senha = "12345678"
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/Login", login);
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact(DisplayName = "Criar nova conta de Usuário Valida"), TestPriority(3)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void CriarNovaContaDeUsuarioValido()
        {
            var login = new UsuarioDto()
            {
                Login = "teste3",
                NomeCompleto = "Teste 3",
                Senha = "12345678"
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/CriarNovoUsuario", login);
            response.EnsureSuccessStatusCodeOrResponseException();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Criar nova conta de Usuário Invalida"), TestPriority(4)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void CriarNovaContaDeUsuarioInvalido()
        {
            var login = new UsuarioDto()
            {
                Login = "teste3",
                Senha = null
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/CriarNovoUsuario", login);
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
