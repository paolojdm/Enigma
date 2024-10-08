using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Enigma_Protocol.Controllers
{
    public class Room3Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Room3Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Room3()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Retrieve or initialize player progress for Room 3
            var playerProgress = await _context.PlayerProgresses
                .Include(pp => pp.User)
                .FirstOrDefaultAsync(pp => pp.PlayerID == userId);

            if (playerProgress == null)
            {
                playerProgress = new PlayerProgress
                {
                    PlayerID = userId,
                    Current_Lives_Room = 3,
                    SolvedPuzzles = 0,
                    CurrentRoomId = 3
                };
                _context.PlayerProgresses.Add(playerProgress);
            }
            else
            {
                playerProgress.Current_Lives_Room = 3;
                playerProgress.CurrentRoomId = 3;
                _context.PlayerProgresses.Update(playerProgress);
            }

            await _context.SaveChangesAsync();
            ViewBag.CurrentLives = playerProgress.Current_Lives_Room;

            return View(playerProgress);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCode([FromBody] CodeSubmissionModel submission)
        {
            Console.WriteLine($"\n\n\n\nROOM3 - Validating the code...\n\n\n\n");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var playerProgress = await _context.PlayerProgresses
                                        .Include(p => p.User)
                                        .FirstOrDefaultAsync(p => p.PlayerID == int.Parse(userId));

            if (playerProgress == null)
                return Json(new { correct = false, message = "Player progress not found.", livesRemaining = 0 });

            var puzzle = await _context.Puzzles.FirstOrDefaultAsync(p => p.RoomId == 3); // Room 3 specific puzzle
            if (puzzle == null)
                return Json(new { correct = false, message = "Puzzle not found.", livesRemaining = 0 });



            if (string.Equals(submission.Code.Trim(), puzzle.Answer.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                playerProgress.SolvedPuzzles += 1;
                await _context.SaveChangesAsync();

                return Json(new { correct = true, nextRoom = false, message = "Congratulations! You've completed the escape room!" });
            }
            else
            {
                playerProgress.Current_Lives_Room -= 1;

                if (playerProgress.Current_Lives_Room <= 0)
                {
                    await _context.SaveChangesAsync();
                    return Json(new { correct = false, message = "Incorrect! No lives remaining.", livesRemaining = 0, redirectUrl = "/Puzzle/Fail" });
                }

                await _context.SaveChangesAsync();
                return Json(new { correct = false, message = $"Incorrect! Lives remaining: {playerProgress.Current_Lives_Room}", livesRemaining = playerProgress.Current_Lives_Room });
            }
        }
    }
}
