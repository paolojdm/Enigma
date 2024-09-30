using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Enigma_Protocol.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProductList()
        {
            var products = await _context.Products
                .Include(p => p.Inventories)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult ProductForm()
        {
            return View(new Product()); // Pass an empty Product model for creating a new product
        }

        [HttpPost]
        //public async Task<IActionResult> AddProduct(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();

        //        // After saving the product, redirect to EditInventory for this product
        //        return RedirectToAction("EditInventory", new { productId = product.Id });
        //    }
        //    return View("ProductList", product); // Return to the product form if ModelState is not valid
        //}
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, int quantityAvailable, int quantityReserved)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // or use your preferred logging method
                }
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Create and add the inventory for the new product
                var inventory = new Inventory
                {
                    ProductID = product.Id,
                    QuantityAvailable = quantityAvailable,
                    QuantityReserved = quantityReserved,
                    LastUpdated = DateTime.Now
                };

                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductList"); // Redirect to the list after adding
            }

            // If there's an error, return the form with the current product
            return View("ProductForm", product); // Ensure you're passing the Product model
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("ProductForm", product); // Pass the product to the form for editing
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductList");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> EditInventory(int productId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductID == productId);
            if (inventory == null)
            {
                // If no inventory exists for the product, create one
                var product = await _context.Products.FindAsync(productId);
                if (product == null) return NotFound();

                inventory = new Inventory
                {
                    ProductID = product.Id,
                    QuantityAvailable = 0,
                    QuantityReserved = 0,
                    LastUpdated = DateTime.Now
                };

                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
            }

            return View(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> EditInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                inventory.LastUpdated = DateTime.Now;
                _context.Inventories.Update(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductList");
            }
            return View(inventory);
        }

        [HttpGet]
        public IActionResult AddInventory()
        {
            ViewBag.Products = _context.Products.ToList(); // Get the product list
            return View(new Inventory()); // Pass an empty Inventory model
        }
    }
}
