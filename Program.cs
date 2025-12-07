using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PaneGEST_Blazor_MySQL.Components;
using PaneGEST_Blazor_MySQL.Models;
// Se o seu Context estiver na pasta Data, adicione: using PaneGEST_Blazor_MySQL.Data;

var builder = WebApplication.CreateBuilder(args);

// ==================================================
// 1. CONFIGURAÇÃO DOS SERVIÇOS (BUILDER)
// ==================================================

// Adicionar suporte para Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adicionar suporte para Controladores (Necessário para o Login seguro)
builder.Services.AddControllers();

// Adicionar serviço de Antiforgery (Obrigatório para formulários no .NET 8)
builder.Services.AddAntiforgery();

// Adicionar suporte para detetar estado de autenticação nas Views Blazor
builder.Services.AddCascadingAuthenticationState();

// Configuração da Autenticação por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Redireciona para aqui se não tiver acesso
        options.ExpireTimeSpan = TimeSpan.FromHours(8); // Duração do login
    });

// Configuração da Base de Dados MySQL
var connectionString = builder.Configuration.GetConnectionString("MySql");
builder.Services.AddDbContext<WwwpanetPaneGestContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

var app = builder.Build();

// ==================================================
// 2. CONFIGURAÇÃO DO PIPELINE (APP)
// ==================================================

// Configuração de erro para ambiente de produção
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- ZONA DE SEGURANÇA (A ORDEM É IMPORTANTE) ---
app.UseAuthentication(); // 1. Quem és tu?
app.UseAuthorization();  // 2. O que podes fazer?
app.UseAntiforgery();    // 3. Proteção de formulários
// ------------------------------------------------

// Mapear os Controladores (API de Login)
app.MapControllers();

// Mapear o Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
