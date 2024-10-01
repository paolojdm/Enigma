using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enigma_Protocol.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var catalogItems = await _context.Products
                .Select(p => new CatalogViewModel
                {
                    ImageUrl = p.ImageUrl,
                    ProductId = p.Id,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    ProductType = p.ProductType,
                    QuantityAvailable = p.Inventories
                        .Any() ? p.Inventories.Sum(i => i.QuantityAvailable) : 0 // Calculate total quantity available from inventories
                })
                .ToListAsync();

            return View(catalogItems);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.Inventories
                .Include(i => i.Product)
                .Where(i => i.Product.Id == id)
                .Select(i => new ProductDetailsViewModel
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.ProductName,
                    ProductDescription = i.Product.ProductDescription,
                    Price = i.Product.Price,
                    ProductType = i.Product.ProductType,
                    QuantityAvailable = i.QuantityAvailable,
                    ImageUrl = "/Images/Designer1.jpg"  // You can change this dynamically later
                }).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
