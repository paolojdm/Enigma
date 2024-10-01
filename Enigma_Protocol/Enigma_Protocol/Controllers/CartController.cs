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

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            // Get the current user ID (assuming you're using Identity)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Find the cart item for the current user
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

            if (cartItem == null)
            {
                return NotFound(); // Item not found in the cart
            }

            // Remove the item from the cart
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirect to the cart index after removing
        }

        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            // Get the current user ID (assuming you're using Identity)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Find the cart item for the current user
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

            if (cartItem == null)
            {
                return NotFound(); // Item not found in the cart
            }

            // Update the quantity
            if (quantity <= 0)
            {
                // If quantity is 0 or less, remove the item from the cart
                _context.Carts.Remove(cartItem);
            }
            else
            {
                // Otherwise, update the quantity
                cartItem.Quantity = quantity;
                _context.Carts.Update(cartItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirect to the cart index after updating
        }
        // Add other actions like Create, Edit, Delete as needed

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(userId);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _context.Users.FindAsync(userId);

                // Update user payment information
                user.CardType = updatedUser.CardType;
                user.CardOwner = updatedUser.CardOwner;
                user.CardNumber = updatedUser.CardNumber;
                user.CardCVC = updatedUser.CardCVC;
                user.ExpirationDate = updatedUser.ExpirationDate;

                await _context.SaveChangesAsync();

                // Redirect to order confirmation or cart index
                return RedirectToAction("Index"); // You can change this to an order confirmation view
            }

            return View(updatedUser);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment()
        {
            // Logic for confirming the existing payment method (if needed)
            // Redirect to a confirmation page or cart index
            return RedirectToAction("Index");
        }




    }
}