using Microsoft.AspNetCore.Mvc;

namespace Enigma_Protocol.Controllers
{
    public class PuzzleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Lettera()
        {
            return RedirectToAction("LetteraScrivania");
        }

        public IActionResult LetteraScrivania()
        {
            return View();
        }
        public IActionResult MostraBaule()
        {
            return RedirectToAction("Baule");
        }
        public IActionResult Baule()
        {
            return View();
        }
        public IActionResult MostraCassaforte()
        {
            return RedirectToAction("Cassaforte");
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
        public IActionResult Puzzle()
        {
            return View();
        }
        
        public IActionResult MostraPrimopiatto()
        {
            return RedirectToAction("Primopiatto");
        }
        public IActionResult Primopiatto()
        {
            return View();
        }
        public IActionResult MostraSecondopiatto()
        {
            return RedirectToAction("Secondopiatto");
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
                return Redirect("/Puzzle/Room3"); 
            }
            else
            {
                return Redirect("/Puzzle/Secondopiatto"); 
            }
        }
        public IActionResult MostraLetteravaso()
        {
            return RedirectToAction("Letteravaso");
        }
        public IActionResult Letteravaso()
        {
            return View();
        }
        public IActionResult TrovaSpada()
        {
            return RedirectToAction("Spada");
        }
        public IActionResult Spada()
        {
            return View();
        }
        public IActionResult MostraArmatura()
        {
            return RedirectToAction("Armor");
        }
        public IActionResult Armor()
        {
            return View();
        }
        public IActionResult MostraScudo()
        {
            return RedirectToAction("Scudo");
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
                return Redirect("/PrologoeFinale/Fine");
            }
            else
            {
                return Redirect("/Puzzle/Scudo");
            }
        }
        public IActionResult Room1()
        {
            return View();
        }
        public IActionResult Room2()
        {
            return View();
        }
        public IActionResult Room3()
        {
            return View();
        }
    }
}
