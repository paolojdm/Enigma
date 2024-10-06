using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Enigma_Protocol.Models;
using Enigma_Protocol.DB;
using System.Security.Claims;

namespace Enigma_Protocol.Controllers
{
    [Authorize]
    public class PuzzleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PuzzleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Room1
        public async Task<IActionResult> Room1()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Attempt to fetch existing PlayerProgress
            var playerProgress = await _context.PlayerProgresses
                .Include(pp => pp.User)
                .FirstOrDefaultAsync(pp => pp.PlayerID == userId);

            if (playerProgress == null)
            {
                // Create a new PlayerProgress entry for the user
                playerProgress = new PlayerProgress
                {
                    PlayerID = userId,
                    Current_Lives_Room = 3,
                    SolvedPuzzles = 0
                };
                _context.PlayerProgresses.Add(playerProgress);
            }
            else
            {
                // Update existing PlayerProgress with new values
                playerProgress.Current_Lives_Room = 3;

                _context.PlayerProgresses.Update(playerProgress); // Ensure to mark it as updated
            }

            await _context.SaveChangesAsync(); // Save changes to the database

            // Pass the current lives to the view
            ViewBag.CurrentLives = playerProgress.Current_Lives_Room;

            return View(playerProgress);
        }

        // POST: Validate Puzzle Answer
        [HttpPost]
        public async Task<IActionResult> ValidatePuzzleAnswer(int puzzleId, string answer)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _context.Users
                         .Include(u => u.playerProgresses) // Assuming PlayerProgress is a related entity
                         .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));


            var playerProgress = await _context.PlayerProgresses
                                    .Include(p => p.User) // Ensure User is included
                                    .FirstOrDefaultAsync(p => p.User.Id == user.Id);

            if (playerProgress == null)
            {
                return BadRequest("Player progress not found.");
            }

            var puzzle = await _context.Puzzles.FindAsync(puzzleId);

            if (puzzle == null)
            {
                return NotFound("Puzzle not found.");
            }

            if (answer == puzzle.Answer.ToString()) // Check if the answer matches
            {
                // Correct answer logic
                playerProgress.SolvedPuzzles += 1; // Increment solved puzzles count
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Correct! Proceed to the next puzzle." });
            }
            else
            {
                // Incorrect answer logic
                playerProgress.Current_Lives_Room -= 1; // Subtract a life

                if (playerProgress.Current_Lives_Room <= 0)
                {
                    await _context.SaveChangesAsync();
                    return Json(new { success = false, message = "Incorrect! No lives remaining.", redirectUrl = "/Puzzle/Fail" });
                }

                await _context.SaveChangesAsync();
                return Json(new { success = false, message = $"Incorrect! Lives remaining: {playerProgress.Current_Lives_Room}" });
            }
        }

        // POST: Validate Code for the Safe Puzzle
        [HttpPost]
        public async Task<IActionResult> ValidateCode([FromBody] CodeSubmissionModel submission)
        {
            try
            {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found.");
                }
                var user = await _context.Users
                             .Include(u => u.playerProgresses) // Assuming PlayerProgress is a related entity
                             .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
                if (user == null)
                {
                    return BadRequest("User not found.");
                }


                var playerProgress = await _context.PlayerProgresses
                                        .Include(p => p.User) // Ensure User is included
                                        .FirstOrDefaultAsync(p => p.User.Id == user.Id);

                if (playerProgress == null)
                {
                    return BadRequest("Player progress not found.");
                }

                var puzzle = await _context.Puzzles.FindAsync(1); // Assuming ID 1 is the safe puzzle
                if (puzzle == null)
                {
                    return NotFound("Puzzle not found.");
                }

                var correctCode = puzzle.Answer; // Assuming the correct answer is stored in the 'Answer' field

                Console.WriteLine($"\n\n\n\n\n\n\n\t\tCORRECT CODE VALUE: {correctCode}\n\n\n\n\n\n\n\n");

                // Validate code
                if (string.Equals(submission.Code.Trim(), correctCode.Trim(), StringComparison.OrdinalIgnoreCase))// Check if the code matches
                {
                    playerProgress.SolvedPuzzles += 1; // Increment solved puzzles count
                    await _context.SaveChangesAsync();

                    return Json(new { correct = true });
                }
                else
                {
                    playerProgress.Current_Lives_Room -= 1; // Subtract a life

                    if (playerProgress.Current_Lives_Room <= 0)
                    {
                        await _context.SaveChangesAsync();
                        return Json(new { correct = false, livesRemaining = 0 });
                    }

                    await _context.SaveChangesAsync();
                    return Json(new { correct = false, livesRemaining = playerProgress.Current_Lives_Room, message = $"Submitted code {submission.Code} does not match {correctCode}" });
                }
            }
            catch (Exception ex)
            {
                // Log the error (you could log this to a file or a logging service)
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return 500 error with the error message
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: Fail
        public IActionResult Fail()
        {
            return View();
        }
        // Example method for transitioning to the next room
        public async Task<IActionResult> NextRoom()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Attempt to fetch existing PlayerProgress
            var playerProgress = await _context.PlayerProgresses
                .Include(pp => pp.User)
                .FirstOrDefaultAsync(pp => pp.PlayerID == userId);


            // Logic to set up next room
            return View("Transition",playerProgress); // This will return the Transition.cshtml view
        }
    }
}