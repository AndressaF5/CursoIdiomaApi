using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CursoIdiomaApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CursoIdiomaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsuariosController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioLogado = _userManager.FindByEmailAsync(usuario.Email).Result;
                if (_userManager.CheckPasswordAsync(usuarioLogado, usuario.Senha).Result)
                {

                    if (usuarioLogado != null)
                    {
                        _signInManager.SignInAsync(usuarioLogado, false);
                        return Ok(BuildToken(usuarioLogado));
                    }
                    else
                    {
                        return NotFound("Usuario não localizado!");
                    }
                }
                else
                {
                    throw new Exception("Usuário ou senha inválidos");
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("cadastrar")]
        public ActionResult Cadastrar([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = usuario.Email, Email = usuario.Email };

                var result = _userManager.CreateAsync(user, usuario.Senha).Result;
                if (result.Succeeded)
                {
                    return Ok(usuario);
                }
                else
                {
                    List<string> erros = new List<string>();
                    foreach (var erro in result.Errors)
                    {
                        erros.Add(erro.Description);
                    }

                    return UnprocessableEntity(erros);
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        public TokenUsuario BuildToken(ApplicationUser usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-curso-idioma"));
            var assinatura = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: exp,
                signingCredentials: assinatura
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var tokenUsuario = new TokenUsuario { Token = tokenString, Expiration = exp };

            return tokenUsuario;
        }
    }
}
