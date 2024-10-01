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
        public IActionResult LetteraScrivania()
        {
            return View();
        }
        public IActionResult Baule()
        {
            return View();
        }
        public IActionResult Cassaforte()
        {
            return View();
        }


        
        
        //public string Code { get; set; }
       

        public IActionResult ValidaCode(string code)
        {
            
            string correctCode = "4359";

            if (code == correctCode)
            {
                return Redirect("/Puzzle/Puzzle");
            }
            else
            {
                return Redirect("/Puzzle/Cassaforte");
            }
        }
        public IActionResult Primopiatto()
        {
            return View();
        }
        public IActionResult Secondopiatto()
        {
            return View();
        }
        public IActionResult Correctword(string word)
        {
            string correctWord = "tenebre";

            if (word.ToLower() == correctWord.ToLower())
            {
                return Redirect("/Puzzle/Puzzle"); 
            }
            else
            {
                return Redirect("/Puzzle/Secondopiatto"); 
            }
        }
        public IActionResult LetteraVaso()
        {
            return View();
        }
        public IActionResult Spada()
        {
            return View();
        }
        public IActionResult Armor()
        {
            return View();
        }
        public IActionResult Scudo()
        {
            return View();
        }
        public IActionResult Correctwords(string word)
        {
            string correctWord = "uscita";

            if (word.ToLower() == correctWord.ToLower())
            {
                return Redirect("/Puzzle/Puzzle");
            }
            else
            {
                return Redirect("/Puzzle/Secondopiatto");
            }
        }
    }
}
