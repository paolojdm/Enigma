using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Enigma_Protocol.Models;
using System.Security.Claims;
using System;
using Enigma_Protocol.DB;

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
            await HttpContext.SignOutAsync();
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

                    var claimsIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(principal);

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

       

        //public async Task<IActionResult> Register(User user, string password)//deprecated
        //{
        //    var passwordHasher = new PasswordHasher<User>();
        //    user.PasswordHash = passwordHasher.HashPassword(user, password);

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Login", "Account");
        //}

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
    }//end public class
}
