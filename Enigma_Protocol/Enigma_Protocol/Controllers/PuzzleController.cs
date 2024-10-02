using Enigma_Protocol.DB;
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
        private readonly ApplicationDbContext _context; // Adjust to your DbContext name

        public PuzzleController(ApplicationDbContext context)
        {
            _context = context;

            _timer = new System.Timers.Timer(1000); // Update every second
            _timer.Elapsed += UpdateRoomTime;
            _timer.AutoReset = true;
        }

        private PlayerProgress GetOrCreatePlayerProgress(int userId)
        {
            var playerProgress = _context.PlayerProgresses
                .Include(p => p.CurrentRoom)
                .Include(p => p.User)
                .FirstOrDefault(p => p.PlayerID == userId);

            if (playerProgress == null)
            {
                // Create new PlayerProgress if it doesn't exist
                playerProgress = new PlayerProgress
                {
                    PlayerID = userId,
                    CurrentRoomId = 1, // Set initial room
                    SolvedPuzzles = 0,
                    Current_Lives_Room = 3,
                    Current_Lives_Puzzle = 3,
                    RoomStartTime = DateTime.Now,
                    CurrentRoomTime = DateTime.Now
                };

                _context.PlayerProgresses.Add(playerProgress);
                _context.SaveChanges();
            }

            return playerProgress;
        }

        private PlayerProgress GetPlayerProgress()
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var playerProgress = _context.PlayerProgresses
                .Include(p => p.CurrentRoom)
                .Include(p => p.User)
                .FirstOrDefault(p => p.PlayerID == userId);

            return playerProgress;
        }

        private Puzzle GetCurrentPuzzle(int roomId)
        {
            // Fetch current puzzle for the room
            return new Puzzle { Question = "Enter the correct code", RoomId = roomId };
        }

        private void UpdateRoomTime(object sender, ElapsedEventArgs e)
        {
            var playerProgress = GetPlayerProgress();
            playerProgress.CurrentRoomTime = DateTime.Now;
            // Optionally save this periodically if needed
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Puzzle()
        {
            var playerProgress = GetPlayerProgress();
            if (playerProgress == null)
            {
                return NotFound("Player progress not found.");
            }

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId),
            };

            return View(viewModel);
        }

        // Room 1 Action
        public IActionResult Room1()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var playerProgress = GetOrCreatePlayerProgress(userId);

            // Set the current room to Room 1
            playerProgress.CurrentRoomId = 1;
            _context.SaveChanges();

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Room 2 Action
        public IActionResult Room2()
        {
            var playerProgress = GetPlayerProgress();

            // Update the current room to Room 2
            playerProgress.CurrentRoomId = 2;
            _context.SaveChanges();  // Save the progress update to the database

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Baule Room Action
        public IActionResult Baule()
        {
            var playerProgress = GetPlayerProgress();

            // Update the current room to Baule (assuming Baule has a unique room ID, say 3)
            playerProgress.CurrentRoomId = 3;
            _context.SaveChanges();  // Save progress

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Lettera Scrivania Action
        public IActionResult LetteraScrivania()
        {
            var playerProgress = GetPlayerProgress();

            // Update the current room (assign a room ID for LetteraScrivania)
            playerProgress.CurrentRoomId = 4; // Example ID for LetteraScrivania
            _context.SaveChanges();

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Spada Room Action
        public IActionResult Spada()
        {
            var playerProgress = GetPlayerProgress();

            // Update the current room for Spada
            playerProgress.CurrentRoomId = 5; // Example ID for Spada
            _context.SaveChanges();

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Armor Room Action
        public IActionResult Armor()
        {
            var playerProgress = GetPlayerProgress();

            // Update the current room for Armor
            playerProgress.CurrentRoomId = 6; // Example ID for Armor
            _context.SaveChanges();

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Scudo Room Action
        public IActionResult Scudo()
        {
            var playerProgress = GetPlayerProgress();

            // Update the current room for Scudo
            playerProgress.CurrentRoomId = 7; // Example ID for Scudo
            _context.SaveChanges();

            var viewModel = new CurrentRoomViewModel
            {
                PlayerProgress = playerProgress,
                CurrentRoom = playerProgress.CurrentRoom,
                CurrentPuzzle = GetCurrentPuzzle(playerProgress.CurrentRoomId)
            };

            return View(viewModel);
        }

        // Correct Word Validation (Secondopiatto)
        public IActionResult Correctwords(string word)
        {
            string correctWord = "uscita";
            var playerProgress = GetPlayerProgress();

            if (word.ToLower() == correctWord.ToLower())
            {
                // Move to the next room, update progress
                playerProgress.CurrentRoomId = 8;  // Example ID for next room
                _context.SaveChanges();

                return RedirectToAction("Room2"); // Go to the next room
            }
            else
            {
                // Stay in the current room
                return RedirectToAction("Secondopiatto");
            }
        }
    }
}
