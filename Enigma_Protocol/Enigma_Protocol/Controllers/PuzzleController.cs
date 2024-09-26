using Microsoft.AspNetCore.Mvc;

namespace Enigma_Protocol.Controllers
{
    public class PuzzleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Puzzle()
        {
            return View();
        }
    }
}
