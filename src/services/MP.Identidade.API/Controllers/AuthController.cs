﻿using EasyNetQ;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MP.Core.Integration;
using MP.Core.ObjetosDeDominio;
using MP.Identidade.API.Extensions;
using MP.Identidade.API.Models;
using MP.Identidade.API.RabbitMQSender;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MP.Identidade.API.Controllers
{
    
    [Route("api/v1/identidade")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSetting _appSetting;
        private readonly IRabbitMQMessageSender _rabbitMQSender;
       
        public AuthController(SignInManager<IdentityUser> signInManager, 
                                UserManager<IdentityUser> userManager,
                               IOptions<AppSetting> appSetting,
                               IRabbitMQMessageSender rabbitMQMessageSender
                               )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSetting = appSetting.Value;
            _rabbitMQSender = rabbitMQMessageSender;
           
        }


        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user,usuarioRegistro.Senha);
            if (result.Succeeded)
            {
                //integrar com fila do rabbitmq
                var criarCliente = new ClienteVo
                {
                    Nome = usuarioRegistro.Nome,
                    Email = usuarioRegistro.Email,
                    Cpf = new Cpf(usuarioRegistro.Cpf)

                };
                criarCliente.Nome = usuarioRegistro.Nome;

                _rabbitMQSender.SendMessage(criarCliente, "new-cliente-queue");
                return CustomResponse(await GerarJwt(usuarioRegistro.Email));
            }
            foreach (var erro in result.Errors)
            {
                AdicionarErroProcessamento(erro.Description);
            }
            return CustomResponse();  

        }        



        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if(result.Succeeded) return CustomResponse(await GerarJwt(usuarioLogin.Email));

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("usuário ou Senha incorretos");
            return CustomResponse();
        }


        
        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub,user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf,ToUnixEpochDate(DateTime.Now).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(DateTime.UtcNow).ToString(),ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSetting.Emissor,
                Audience = _appSetting.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSetting.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSetting.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }

            };
            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        
    }
}
