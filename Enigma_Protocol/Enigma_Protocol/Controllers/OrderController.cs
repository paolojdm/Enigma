using Enigma_Protocol.Controllers;
using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Enigma_Protocol.Controllers
{


    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> OrderList()
        {
            // Get the current user ID (assuming you're using Identity)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Fetch the orders for the current user
            var orders = await _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product) // Include product details for each order detail
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    ShippingStatus = o.ShippingStatus,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel
                    {
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return View(orders);  // Ensure the view expects IEnumerable<OrderViewModel>
        }
    }
}