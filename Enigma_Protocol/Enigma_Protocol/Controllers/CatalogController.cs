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
            var catalogItems = await _context.Inventories
                .Include(i => i.Product)
                .Select(i => new CatalogViewModel
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.ProductName,
                    ProductDescription = i.Product.ProductDescription,
                    Price = i.Product.Price,
                    ProductType = i.Product.ProductType,
                    QuantityAvailable = i.QuantityAvailable
                })
                .ToListAsync();

            return View(catalogItems);
        }
    }
}
