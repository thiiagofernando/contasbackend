using CadastroConta.Api.ViewModels;
using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroConta.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        public AutenticacaoController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Autenticar([FromBody] UsuarioLoginViewModel usuario)
        {
            try
            {
                string hash = PasswordService.GeneratePassword(usuario.Senha);
                Usuario user = _repository.ObterUsuarioPorLoginESenha(username: usuario.Login, senha: hash);

                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = GerarToken(user);
                return Ok(new
                {
                    user = user.Login,
                    nameUser = user.NomeCompleto,
                    authenticated = true,
                    created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"),
                    token = token,
                    message = "OK"
                });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível realizar o login" });
            }
        }

        [HttpPost]
        [Route("CriarNovoUsuario")]
        [AllowAnonymous]
        public ActionResult<UsuarioViewModel> CriarNovoUsuario([FromBody] UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existeLogin = _repository.ObterUsuarioPorLogin(usuario.Login);
                if(existeLogin != null)
                    return BadRequest(new { message = "Não foi possível criar o usuário informe um login diferente" });

                var novaoUsuario = new Usuario
                {
                    Login = usuario.Login,
                    NomeCompleto = usuario.NomeCompleto,
                    Senha = PasswordService.GeneratePassword(usuario.Senha)
                };
                _repository.GravarNovoUsuario(novaoUsuario);
                _repository.SaveChanges();
                return usuario;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
        }

        private static string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(3600);
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login.ToString()),
                }),
                NotBefore = dataCriacao,
                Audience = "ApiContas",
                Expires = dataExpiracao,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
