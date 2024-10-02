using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Timers;

namespace Enigma_Protocol.Controllers
{
    public class PuzzleController : Controller
    {
        private static System.Timers.Timer _timer;
        private readonly ApplicationDbContext _context; // Adjust to your DbContext name

        public PuzzleController(ApplicationDbContext context)
        {
            _context = context;

            _timer = new System.Timers.Timer(1000); // Update every second

            _timer.Elapsed += UpdateRoomTime;
            _timer.AutoReset = true;
        }

        #region GPT methods

        public PuzzleController()
        {

        }

        // Method to initialize puzzle
        public IActionResult Cassaforte()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            var playerProgress = GetPlayerProgress();
            playerProgress.RoomStartTime = DateTime.Now;

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoom.Id)
            };

            _timer.Start();
            return View(viewModel);
        }

        // Method to validate the code entered for puzzle
        public IActionResult ValidaCode(string code)
        {
            var playerProgress = GetPlayerProgress();

            // Stop the timer
            _timer.Stop();

            // Update room time one last time
            playerProgress.CurrentRoomTime = DateTime.Now;

            if ((playerProgress.CurrentRoomTime - playerProgress.RoomStartTime).TotalMinutes > 10)
            {
                // Fail scenario
                ViewBag.Message = "You Failed! Time exceeded 10 minutes.";
                return RedirectToAction("Fail");
            }

            // Validate the puzzle
            string correctCode = "4359";
            if (code == correctCode)
            {
                playerProgress.SolvedPuzzles++;
                // Proceed to the next puzzle or room
                return RedirectToAction("PuzzleSolved");
            }
            else
            {
                // Code incorrect, retry
                return RedirectToAction("Cassaforte");
            }
        }

        private void UpdateRoomTime(object sender, ElapsedEventArgs e)
        {
            var playerProgress = GetPlayerProgress();
            playerProgress.CurrentRoomTime = DateTime.Now;
            // You could save this to the database periodically if needed
        }

        private PlayerProgress GetPlayerProgress()
        {
            // Fetch player progress from database or session

            // Get the user ID from the claims
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Fetch the PlayerProgress object for the current user
            var playerProgress = _context.PlayerProgresses
                .Include(p => p.CurrentRoom)  // Include navigation property for the room
                .Include(p => p.User)         // Include navigation property for the user
                .FirstOrDefault(p => p.PlayerID == userId);

            return playerProgress;
        }

        private Puzzle GetCurrentPuzzle(int roomId)
        {
            // Fetch current puzzle for the room
            return new Puzzle { Question = "Enter the correct code", RoomId = roomId };
        }

        public IActionResult PuzzleSolved()
        {
            return View("PuzzleSolved");
        }

        public IActionResult Fail()
        {
            return View("Fail");
        }




        #endregion  GPT methods
        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Puzzle()
        {
            var playerProgress = GetPlayerProgress(); //metodo che prende l'oggetto

            if (playerProgress == null)
            {
                return NotFound("Player progress not found.");
            } //semplice check

            // Create the ViewModel and populate its properties
            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,  // The current room of the player
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId), // Fetch the current puzzle
            };
            return View(viewModel);
            //Quando l'utente passa da un stanza all'altra, la roomId deve cambiare, quindi prima bisgona assegnare un id ad ogni stanza (aggiungerle al db in base)
        }
        public IActionResult LetteraScrivania()
        {
            return View();
        }
        public IActionResult Baule()
        {
            return View();
        }
        //public IActionResult Cassaforte()
        //{
        //    return View();
        //} //Deprecated

        //public string Code { get; set; }


        //public IActionResult ValidaCode(string code)
        //{

        //    string correctCode = "4359";

        //    if (code == correctCode)
        //    {
        //        return Redirect("/Puzzle/Puzzle");
        //    }
        //    else
        //    {
        //        return Redirect("/Puzzle/Cassaforte");
        //    }
        //} //Deprecated
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
