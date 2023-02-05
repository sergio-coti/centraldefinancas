using CentralDeFinancas.Presentation.Mvc.Models;
using CentralDeFinancas.Presentation.Mvc.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace CentralDeFinancas.Presentation.Mvc.Controllers
{
    public class AccountController : Controller
    {
        //GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, [FromServices] IntegrationService service)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model),
                        Encoding.UTF8, "application/json");

                    var response = await service.PostAsync("/Auth", content);
                    var result = JsonConvert.DeserializeObject<ResultContent>(response);

                    #region Autenticando o usuário

                    var user = JsonConvert.SerializeObject(result.Model);
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user) }, 
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Dashboard");

                    #endregion
                }
                catch (Exception e)
                {
                    var result = JsonConvert.DeserializeObject<ResultContent>(e.Message);
                    TempData["Mensagem"] = result.Message;
                }
            }

            return View();
        }

        //GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, [FromServices] IntegrationService service)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var request = new
                    {
                        model.Nome,
                        model.Email,
                        model.Senha
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(request),
                        Encoding.UTF8, "application/json");

                    var response = await service.PostAsync("/Usuarios", content);
                    var result = JsonConvert.DeserializeObject<ResultContent>(response);

                    TempData["Mensagem"] = result.Message;
                }
                catch(Exception e)
                {
                    var result = JsonConvert.DeserializeObject<ResultContent>(e.Message);
                    TempData["Mensagem"] = result.Message;
                }
            }

            return View();
        }
    }

    /// <summary>
    /// Classe auxiliar para deserializar a resposta obtida da API
    /// </summary>
    public class ResultContent
    {
        public string? Message { get; set; }
        public AccountModel? Model { get; set; }
    }

    /// <summary>
    /// Classe auxiliar para deserializar os dados do usuário autenticado
    /// </summary>
    public class AccountModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAcesso { get; set; }
        public string? AccessToken { get; set; }
    }
}
