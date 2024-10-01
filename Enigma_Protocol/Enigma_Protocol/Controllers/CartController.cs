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
            // Get the current user ID (assuming you're using Identity)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Fetch the cart items for the current user
            var cartItems = await _context.Carts
                .Where(c => c.UserID == userId)
                .Include(c => c.Product) // Ensure you're including the product details from the inventory
                .Select(c => new CartViewModel
                {
                    Id = c.ProductID,
                    ProductName = c.Product.ProductName,
                    Quantity = c.Quantity,
                    Price = c.Product.Price,
                    TotalPrice = c.Quantity * c.Product.Price
                })
                .ToListAsync();

            return View(cartItems);  // Ensure that the View expects IEnumerable<CartViewModel>
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            // Get the current user ID (assuming you're using Identity)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Check if the product exists
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound(); // Product doesn't exist
            }

            // Check if the user already has this product in the cart
            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

            if (existingCartItem != null)
            {
                // If the product is already in the cart, increase the quantity
                existingCartItem.Quantity++;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                // Otherwise, add a new item to the cart
                var cartItem = new Cart
                {
                    UserID = userId,
                    ProductID = productId,
                    Quantity = 1,
                    CreatedAt = DateTime.Now
                };

                _context.Carts.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Catalog"); // Redirect to cart index after adding
        }

        // Add other actions like Create, Edit, Delete as needed
    }
}