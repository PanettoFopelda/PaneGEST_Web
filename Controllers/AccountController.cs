using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaneGEST_Blazor_MySQL.Models; // Confirme se o namespace está certo
using System.Security.Claims;

namespace PaneGEST_Blazor_MySQL.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly WwwpanetPaneGestContext _context;

        public AccountController(WwwpanetPaneGestContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            // 1. Verificar na Base de Dados
            var user = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Nome == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                // Login falhou: volta para a página de login com erro
                return Redirect("/login?erro=true");
            }

            // 2. Criar a Identidade do Utilizador (Claims)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, "Admin") // Pode ajustar consoante a sua tabela
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantém o login mesmo se fechar o browser
            };

            // 3. Criar o Cookie Oficialmente
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // 4. Sucesso: Vai para o Dashboard
            return Redirect("/dashboard");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // 1. Apaga o Cookie de autenticação (Logout efetivo)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Redireciona para o site externo
            return Redirect("https://www.panetto.pt");
        }

    }
}
