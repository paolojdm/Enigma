using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;

namespace Enigma_Protocol.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context; // Adjust to your DbContext name

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var carts = await _context.Carts
                                       .Include(c => c.User)
                                       .Include(c => c.Inventory)
                                       .Select(c => new CartViewModel
                                       {
                                           Id = c.Id,
                                           UserID = c.UserID,
                                           InventoryID = c.InventoryID,
                                           CreatedAt = c.CreatedAt,
                                           Quantity = c.Quantity,
                                           UserName = c.User.UserName, // Assuming User is a navigation property
                                           ProductName = c.Inventory.Product.ProductName // Assuming Inventory has Product
                                       })
                                       .ToListAsync();

            return View(carts);
        }

        // Add other actions like Create, Edit, Delete as needed
    }
}
