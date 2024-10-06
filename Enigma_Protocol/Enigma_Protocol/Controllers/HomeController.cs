using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Enigma_Protocol.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
           
            
                // Safely get the user ID
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        
                if (userIdClaim != null)
                {
                    var userId = int.Parse(userIdClaim.Value);

                var user = await _context.Users
                  .Include(u => u.Orders)
                      .ThenInclude(o => o.OrderDetails)
                          .ThenInclude(od => od.Product)
                  .FirstOrDefaultAsync(u => u.Id == userId);


                if (user != null)
                    {
                        return View(user);
                    }
                    else
                    {
                        // Handle the case where the user is not found in the database
                        _logger.LogError($"User with ID {userId} not found.");
                        return NotFound("User not found.");
                    }
                }
                else
                {
                    User user = new User();
                    return View(user);
                }
        


        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
