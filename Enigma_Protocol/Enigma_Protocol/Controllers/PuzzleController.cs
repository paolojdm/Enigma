using Enigma_Protocol.DB;
using Enigma_Protocol.Migrations;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Timers;

namespace Enigma_Protocol.Controllers
{
    public class PuzzleController : Controller
    {
        private static System.Timers.Timer _timer;
        private readonly ApplicationDbContext _context;

        public PuzzleController(ApplicationDbContext context)
        {
            _context = context;

            // Initialize the timer to update every second
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += UpdateRoomTime; // Event handler for timer ticks
            _timer.AutoReset = true; // Auto-reset the timer
        }

        // Dispose to stop the timer when the controller is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Stop();
                _timer.Dispose(); // Clean up the timer resources
            }
            base.Dispose(disposing);
        }

        // Method to initialize the Cassaforte puzzle
        public async Task<IActionResult> Cassaforte()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var playerProgress = await GetPlayerProgressAsync(userId);
            if (playerProgress == null)
            {
                return NotFound("Player progress not found.");
            }

            playerProgress.RoomStartTime = DateTime.Now; // Record the start time

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoom.Id)
            };

            _timer.Start(); // Start the timer
            return View(viewModel); // Return the view with the view model
        }

        // Method to validate the entered code for the puzzle
        public async Task<IActionResult> ValidaCode(string code) //Only for the safe code in the Room1
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var playerProgress = await GetPlayerProgressAsync(userId);

            string correctCode = "4359"; // Correct code

            if (code == correctCode)
            {
                playerProgress.SolvedPuzzles++; // Increment solved puzzles count
                _context.PlayerProgresses.Update(playerProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction("Puzzle"); // Proceed to the next puzzle
            }
            else
            {
                return RedirectToAction("Cassaforte"); // Incorrect code, retry
            }
        }

        // Method to update room time every second
        private void UpdateRoomTime(object sender, ElapsedEventArgs e)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var playerProgress = GetPlayerProgressAsync(userId).Result; // Retrieve player progress

            if (playerProgress != null)
            {
                playerProgress.CurrentRoomTime = DateTime.Now; // Update current room time
                // Optionally save the updated time to the database
                _context.PlayerProgresses.Update(playerProgress);
                _context.SaveChangesAsync(); // Save changes asynchronously
            }
        }


        #region GetOBJECT for Views

        private async Task<PlayerProgress> GetPlayerProgressAsync(int userId)
        {
            var pp = await _context.PlayerProgresses
                .Include(p => p.CurrentRoom)  // Include navigation property for the room
                .Include(p => p.User)         // Include navigation property for the user
                .FirstOrDefaultAsync(p => p.PlayerID == userId); //cerco un player progress gia' esistente

            if (pp == null)
            {
                pp = new PlayerProgress
                {
                    CurrentRoomId = 1,
                    SolvedPuzzles = 0,
                    PlayerID = userId,
                    Current_Lives_Room = 3,
                    Current_Lives_Puzzle = 3,
                    RoomStartTime = DateTime.Now,
                    CurrentRoomTime = DateTime.Now
                }; // creo oggetto player progress per darlo a Room1

                _context.PlayerProgresses.Add(pp);
                await _context.SaveChangesAsync(); // Save changes asynchronously
            }

            Console.WriteLine($"\n\n\n\nplayerProgress \n {pp.Id}\n" +
                $"{pp.CurrentRoomId}\n" +
                $"{pp.SolvedPuzzles}\n" +
                $"{pp.PlayerID}\n" +
                $"{pp.Current_Lives_Room}\n" +
                $"{pp.Current_Lives_Puzzle}\n" +
                $"{pp.RoomStartTime}\n" +
                $"{pp.CurrentRoomTime}\n\n\n\n\n\n");

            return pp;
        }

        // Method to get the current puzzle for the room

        private async Task<Puzzle> GetCurrentPuzzleAsync(int roomId)
        {
            // Query the database to get the puzzle for the specified room
            var puzzle = await _context.Puzzles
                .FirstOrDefaultAsync(p => p.RoomId == roomId);

            if (puzzle == null)
            {
                // Handle case where no puzzle exists for the room
                // You can either return null or throw an exception
                return null;
            }

            return puzzle;
        }
        private Puzzle GetCurrentPuzzle(int roomId)
        {
            

            //var puzzle_new = new Puzzle
            //{
            //    //public int Id
            //    //public string Question
            //    //public string Answer
            //    //public int RoomId
            //    Question = "Inserisci il codice",
            //    Answer = "4359",
            //    RoomId = 1
            //};

            //_context.Puzzles.Add(puzzle_new);
            // _context.SaveChangesAsync(); // Save changes asynchronously
            return null; // Placeholder puzzle
        }



        #endregion GetOBJ for Views

        // Asynchronous method to get player progress from the database

        public IActionResult ImageReorderPuzzle()
        {
            return View(); // Load the Image Reorder Puzzle View
        }
        public IActionResult PuzzleSolved()
        {
            return View("PuzzleSolved"); // Return the solved puzzle view
        }

        public IActionResult Fail()
        {
            return View("Fail"); // Return the fail view
        }

        public IActionResult Index()
        {
            return View(); // Return the index view
        }

        // Method to load the puzzle view
        public async Task<IActionResult> Puzzle()
        {
            //var playerProgress = await GetPlayerProgressAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            //if (playerProgress == null)
            //{
            //    return NotFound("Player progress not found."); // Handle null player progress
            //}

            //var viewModel = new CurrentRoomViewModel
            //{
            //    PlayerProgress = playerProgress,
            //    CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId), // Fetch current puzzle
            //};

            return View(); // Return the puzzle view with the view model
        }


        #region View cshtml dei puzzle
        // Other action methods for additional puzzles (you can customize these as needed)
        public IActionResult LetteraScrivania()
        {
            return View(); // Return the Lettera Scrivania view
        }

        public IActionResult Baule()
        {
            return View(); // Return the Baule view
        }

        public IActionResult Primopiatto()
        {
            return View(); // Return the Primo Piatto view
        }

        public IActionResult Secondopiatto()
        {
            return View(); // Return the Secondo Piatto view
        }

        public IActionResult LetteraVaso()
        {
            return View(); // Return the Lettera Vaso view
        }

        public IActionResult Spada()
        {
            return View(); // Return the Spada view
        }

        public IActionResult Armor()
        {
            return View(); // Return the Armor view
        }

        public IActionResult Scudo()
        {
            return View(); // Return the Scudo view
        }


        #endregion View cshtml dei puzzle

        // Method to validate a word entered by the user
        public IActionResult Correctword(string word)
        {
            string correctWord = "tenebre"; // Correct word

            if (word.ToLower() == correctWord.ToLower())
            {
                return Redirect("/Puzzle/Room3"); // Correct word, proceed
            }
            else
            {
                return Redirect("/Puzzle/Secondopiatto"); // Incorrect word, retry
            }
        }

        // Method to validate a second word
        public IActionResult Correctwords(string word)
        {
            string correctWord = "uscita"; // Correct word

            if (word.ToLower() == correctWord.ToLower())
            {
                return Redirect("/PrologoeFinale/Fine"); // Correct word, proceed
            }
            else
            {
                return Redirect("/Puzzle/Secondopiatto"); // Incorrect word, retry
            }
        }


        #region RoomsViews
        public async Task<IActionResult> CompleteImagePuzzle()
        {
            // Logic to check if the puzzle is solved (this can also be done in JS, but handled here for confirmation)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var playerProgress = await GetPlayerProgressAsync(userId);

            playerProgress.CurrentRoomId = 2; // Move to Room 2
            _context.PlayerProgresses.Update(playerProgress);
            await _context.SaveChangesAsync();

            return RedirectToAction("EnterRoom", "Puzzle", new { roomId = 2 });
        }
        public async Task<IActionResult> EnterRoom(int roomId)
        {
            var playerProgress = await GetPlayerProgressAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            if (playerProgress == null)
            {
                return NotFound("Player progress not found.");
            }
            var currentPuzzle = await GetCurrentPuzzleAsync(playerProgress.CurrentRoomId); // Fetch current puzzle from DB
            // Determine which puzzle or resources are relevant for this room
            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentPuzzle = currentPuzzle, // Load the appropriate puzzle
            };



            // You can customize the background music or jumpscare per room
            switch (roomId)
            {
                case 1:
                    viewModel.RoomBackgroundMusic = "/Music/Prologo.mp3";
                    viewModel.RoomImage = "/Images/giphy.gif";
                    return View("Room1", viewModel); // Render Room1 view
                case 2:
                    viewModel.RoomBackgroundMusic = "/Music/Prova_Scream.mp3";
                    viewModel.RoomImage = "/Images/jumpscare02.gif";
                    return View("Room2", viewModel); // Render Room2 view
                case 3:
                    viewModel.RoomBackgroundMusic = "/Music/Fantasma.mp3";
                    viewModel.RoomImage = "/Images/ghost.gif";
                    return View("Room3", viewModel); // Render Room3 view
                default:
                    return NotFound("Room not found.");
            }
        }

        #endregion RoomsViews
    }
}
