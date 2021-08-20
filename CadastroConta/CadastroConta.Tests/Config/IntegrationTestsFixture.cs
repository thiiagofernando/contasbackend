using CadastroConta.Api;
using CadastroConta.Api.ViewModels;
using CadastroConta.Business.Models;
using CadastroConta.Business.Services;
using CadastroConta.Data.Context;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;

namespace CadastroConta.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }
    public class IntegrationTestsFixture<TStartup> where TStartup : class
    {
        public readonly ContasFactory<TStartup> Factory;
        public readonly ContasDbContext Context;
        public HttpClient Client;
        public HttpClient ClientAuth;
        public TokenAcesso Token { get; set; }
        public int Prioridade { get; set; }

        public IntegrationTestsFixture()
        {
            Factory = new ContasFactory<TStartup>();
            Context = (ContasDbContext)Factory.Services.GetService(typeof(ContasDbContext));
            Client = Factory.CreateClient();
            ClientAuth = Factory.CreateClient();
            Autenticar();
            CriaUsuarioApi();
            CriarConta();
        }

        public async void Autenticar()
        {
            var login = new UsuarioLoginDto()
            {
                Login = "teste",
                Senha = "12345678"
            };
            var response = await Client.PostAsJsonAsync("/v1/Autenticacao/Login", login);
            response.EnsureSuccessStatusCodeOrResponseException();

            string conteudo = response.Content.ReadAsStringAsync().Result;
            Token = JsonConvert.DeserializeObject<TokenAcesso>(conteudo);
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token);
            ClientAuth.SetAuthorizeBearer(Token.Token);
        }

        private void CriaUsuarioApi()
        {
            var login = new UsuarioModel()
            {
                Login = "teste",
                NomeCompleto = "Teste",
                Senha = "12345678"
            };
            login.Senha = PasswordService.GeneratePassword(login.Senha);
            Context.usuario.Add(login);
            Context.SaveChanges();
        }
        private void CriarConta()
        {
            var novaSemAtrasoConta = new Business.Models.ContaModel()
            {
                Descricao = "Cartão Credito",
                Valor = 1300,
                DataPagamento = DateTime.Now,
            };
            //Context.conta.Add(novaSemAtrasoConta);
            // Context.SaveChanges();
        }

    }
}
