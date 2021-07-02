﻿using Contas.API.ViewModels;
using Contas.Business.Interfaces;
using Contas.Business.Models;
using Contas.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult<dynamic> Autenticar([FromBody] UsuarioViewModel usuario)
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
                Audience = "API Contas",
                Expires = dataExpiracao,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
