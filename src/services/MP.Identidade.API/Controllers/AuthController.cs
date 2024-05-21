﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MP.Identidade.API.Models;

namespace MP.Identidade.API.Controllers
{
    [Route("api/v1/identidade")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user,usuarioRegistro.Senha);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            return BadRequest();  

        }
        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioRegistro usuarioLogin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if(result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}