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

            // Get cart items for the user
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserID == userId)
                .Select(c => new CartViewModel
                {
                    ProductName = c.Product.ProductName,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                })
                .ToListAsync();

            var checkoutModel = new CheckoutViewModel
            {
                User = user,
                CartItems = cartItems
            };

            return View(checkoutModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(User updatedUser)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(userId);

            // Get cart items for the user
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserID == userId)
                .Select(c => new CartViewModel
                {
                    ProductName = c.Product.ProductName,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                })
                .ToListAsync();

            // If user has no payment method saved, update payment information
            if (ModelState.IsValid && string.IsNullOrEmpty(user.CardNumber))
            {
                user.CardType = updatedUser.CardType;
                user.CardOwner = updatedUser.CardOwner;
                user.CardNumber = updatedUser.CardNumber;
                user.CardCVC = updatedUser.CardCVC;
                user.ExpirationDate = updatedUser.ExpirationDate;

                await _context.SaveChangesAsync();
            }

            // Pass both user and cart items to the view
            var checkoutModel = new CheckoutViewModel
            {
                User = user,
                CartItems = cartItems
            };

            return View(checkoutModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment()
        {
            // Logic for confirming existing payment method (if needed)
            return RedirectToAction("OrderConfirmation"); // Redirect to an order confirmation page
        }

        [HttpPost]
        public async Task<IActionResult> EditPaymentMethod(EditPaymentMethodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    // Update payment information
                    user.CardType = model.EditCardType;
                    user.CardOwner = model.EditCardOwner;
                    user.CardNumber = model.EditCardNumber;
                    user.CardCVC = int.Parse(model.EditCardCVC);
                    user.ExpirationDate = model.EditExpirationDate;

                    await _context.SaveChangesAsync();

                    // Optionally, redirect or return a success message
                    return RedirectToAction("Checkout"); // Or appropriate action
                }
            }

            // If we got this far, something failed; redisplay the form.
            return View(model); // Adjust based on your needs
        }

        public async Task<IActionResult> OrderConfirmation()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // You can retrieve the latest order for this user or any necessary information
            var lastOrder = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserID == userId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            return View(lastOrder); // Pass the order to the view for display
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);



            // Get cart items for the user
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserID == userId)
                .ToListAsync();

            // Check if cart is empty
            if (cartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty. Please add items before placing an order.");
                return RedirectToAction("Index"); // No items in the cart
            }

            // Get the user's shipping address
            var user = await _context.Users.FindAsync(userId); // Retrieve user to access shipping address
            var shippingAddress = user?.ShippingAddress;
            if (string.IsNullOrEmpty(shippingAddress))
            {
                ModelState.AddModelError(string.Empty, "Shipping address is required. Please update your profile.");
                return RedirectToAction("EditProfile", "Account"); // Redirect to profile edit if address is missing
            }

            // Create a new order
            var order = new Order
            {
                UserID = userId,
                OrderDate = DateTime.Now,
                ShippingAddress = shippingAddress, // Use the retrieved shipping address
                ShippingStatus = "Pending",
                TrackingNumber = Guid.NewGuid().ToString(), // Example tracking number
                UpdatedAt = DateTime.Now,
                TotalAmount = cartItems.Sum(c => c.Quantity * c.Product.Price), // Calculate total amount
                OrderDetails = new List<OrderDetail>()
            };

            // Create OrderDetails for each cart item
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            // Add the order to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear the cart after placing the order
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation"); // Redirect to order confirmation page
        }
    }
}