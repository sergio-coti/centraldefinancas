using CentralDeFinancas.Presentation.Mvc.Hubs;
using CentralDeFinancas.Presentation.Mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuração da política de autenticação
builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

//habilitar o signalr no projeto
builder.Services.AddSignalR();

//injeção de dependência
var apiUrl = builder.Configuration.GetValue<string>("apiUrl");
builder.Services.AddTransient(map => new IntegrationService(apiUrl));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//Configurar os hubs criados no projeto
app.MapHub<MainHub>("/mainhub");

app.Run();
