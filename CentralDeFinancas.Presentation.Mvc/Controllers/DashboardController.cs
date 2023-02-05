using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeFinancas.Presentation.Mvc.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        //GET: /Dashboard/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
