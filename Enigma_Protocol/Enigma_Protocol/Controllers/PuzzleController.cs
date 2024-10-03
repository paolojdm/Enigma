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
        public async Task<IActionResult> ValidaCode(string code)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var playerProgress = await GetPlayerProgressAsync(userId);

            // Stop the timer
            _timer.Stop();

            // Update room time one last time
            playerProgress.CurrentRoomTime = DateTime.Now;

            //// Check if time limit exceeded
            //if ((playerProgress.CurrentRoomTime - playerProgress.RoomStartTime).TotalMinutes > 10)
            //{
            //    ViewBag.Message = "You Failed! Time exceeded 10 minutes.";
            //    return RedirectToAction("Fail"); // Redirect to fail view
            //}

            // Validate the puzzle
            string correctCode = "4359"; // Correct code
            if (code == correctCode)
            {
                playerProgress.SolvedPuzzles++; // Increment solved puzzles count
                _context.PlayerProgresses.Update(playerProgress); // Update player progress in DB
                await _context.SaveChangesAsync(); // Save changes to DB
                return RedirectToAction("Puzzle"); // Redirect to solved puzzle view
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

        // Asynchronous method to get player progress from the database
        private async Task<PlayerProgress> GetPlayerProgressAsync(int userId)
        {

            var pp = await _context.PlayerProgresses
                .Include(p => p.CurrentRoom)  // Include navigation property for the room
                .Include(p => p.User)         // Include navigation property for the user
                .FirstOrDefaultAsync(p => p.PlayerID == userId);


            if (pp == null)
            {
                
                    var max_id = await _context.PlayerProgresses
                        .Select(pp => pp.Id)
                        .DefaultIfEmpty(0)  // Return 0 if the list is empty
                        .ToListAsync();      // Execute the query and get the list

                    int maxIdValue = max_id.Max();  // Now safely get the maximum value from the list

                Console.WriteLine($"\n\n\n\n\nID OF PLAYER PROGRESS = {maxIdValue}\n\n\n\n\n");
                    pp = new PlayerProgress
                    {
                        Id = maxIdValue + 1,
                        CurrentRoomId = 1,
                        SolvedPuzzles = 0,
                        PlayerID = userId,
                        Current_Lives_Room = 3,
                        Current_Lives_Puzzle = 3,
                        RoomStartTime = DateTime.Now,
                        CurrentRoomTime = DateTime.Now
                    };

                    _context.PlayerProgresses.Add(pp);
                    await _context.SaveChangesAsync(); // Save changes asynchronously
                    return pp;
            }
                else
                    return pp;
        }

        // Method to get the current puzzle for the room
        private Puzzle GetCurrentPuzzle(int roomId)
        {
            

            return new Puzzle { Question = "Enter the correct code", RoomId = roomId }; // Placeholder puzzle
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
            var playerProgress = await GetPlayerProgressAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            if (playerProgress == null)
            {
                return NotFound("Player progress not found."); // Handle null player progress
            }

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId), // Fetch current puzzle
            };

            return View(viewModel); // Return the puzzle view with the view model
        }

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

        // Methods for the different rooms
        public async Task<IActionResult> Room1()
        {
            var playerProgress = await GetPlayerProgressAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            if (playerProgress == null)
            {
                return NotFound("Player progress not found."); // Handle null player progress
            }

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId), // Fetch current puzzle
            };

            return View(viewModel); // Return the room view with the view model
        }

        public IActionResult Room2()
        {
            return View(); // Return Room 2 view
        }

        public IActionResult Room3()
        {
            return View(); // Return Room 3 view
        }
    }
}
