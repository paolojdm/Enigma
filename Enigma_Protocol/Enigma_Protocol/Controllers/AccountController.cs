using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Enigma_Protocol.Models;
using System.Security.Claims;
using System;
using Enigma_Protocol.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Enigma_Protocol.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        #region LOgin
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // This will clear the authentication cookie
            await HttpContext.SignOutAsync("MyCookieAuth");  // Use your custom scheme

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    // Create authentication cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    // Add role claim
                    string role = user.IsAdmin ? "Admin" : "User";
                    claims.Add(new Claim(ClaimTypes.Role, role));

                    var claimsIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,  // Set persistence based on Remember Me
                        ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null // Optional: Set expiration if needed
                    };

                    await HttpContext.SignInAsync(principal, authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        #endregion Login

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return View(model);
                }

                // Check if passwords match
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("Password", "Passwords do not match");
                    return View(model);
                }

                // Create new user object
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    CreatedAt = DateTime.Now
                };

                // Hash the password
                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

                // Save user to database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to login page after successful registration
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        #endregion Register

        #region MyAccount

        [Authorize] // Ensure only logged-in users can access
        public async Task<IActionResult> MyAccount()
        {
            // Get the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users
                                     .Include(u => u.playerProgresses) // Assuming PlayerProgress is a related entity
                                     .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }
            // Fetch the PlayerProfile by matching PlayerName to UserName


            var playerProgress = await _context.PlayerProgresses
                                    .Include(p => p.User) // Ensure User is included
                                    .FirstOrDefaultAsync(p => p.User.Id == user.Id);

            // Fetch solved puzzles from PlayerProgress (example statistic)
            var solvedPuzzlesCount = 0;
            if (playerProgress != null)
            {
                solvedPuzzlesCount = playerProgress.SolvedPuzzles;
            }

            var model = new MyAccountViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                ShippingAddress = user.ShippingAddress,
                CreatedAt = user.CreatedAt,
                SolvedPuzzlesCount = solvedPuzzlesCount
            };

            return View(model);
        }

        #endregion MyAccount

        #region EditProfile

        // GET: /Account/EditProfile
        public async Task<IActionResult> EditProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); // Get the logged-in user's ID
            var user = await _context.Users.FindAsync(userId); // Fetch the user from the database

            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            // Initialize the EditProfileViewModel with the user's current data
            var model = new EditProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                ShippingAddress = user.ShippingAddress,
                CardType = user.CardType,
                CardOwner = user.CardOwner,
                CardNumber = user.CardNumber,
                CardCVC = user.CardCVC,
                ExpirationDate = user.ExpirationDate
            };

            return View(model); // Pass the view model to the view
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View(model);
                }
                else
                {

                    // Update user profile information
                    user.ShippingAddress = model.ShippingAddress;

                    // Update payment information
                    user.CardType = model.CardType;
                    user.CardOwner = model.CardOwner;
                    user.CardNumber = model.CardNumber;
                    user.CardCVC = model.CardCVC;
                    user.ExpirationDate = model.ExpirationDate;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception
                        Console.WriteLine(ex.Message);
                    }

                    // Redirect to MyAccount page after successful edit
                    return RedirectToAction("MyAccount", "Account");
                }
            }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Or use a logger
                }
                return View(model);
            }
            return View(model);
        }
        #endregion EditProfile
    }//end public class
}
