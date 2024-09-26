using Microsoft.AspNetCore.Mvc;

namespace Enigma_Protocol.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
