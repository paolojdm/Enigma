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
        public async Task<IActionResult> AddProduct(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Define the directory where the images will be stored
                    var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                    // Ensure the directory exists
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                    }

                    // Define a unique name for the file based on product name
                    var fileName = $"{Path.GetFileNameWithoutExtension(product.ProductName)}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(ImageFile.FileName)}";
                    var filePath = Path.Combine(imageDirectory, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Set the product's ImageUrl property
                    product.ImageUrl = $"/Images/{fileName}";
                }

                // Save the product to the database (assuming you have a method to add)
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("ProductList"); // Redirect to a page listing all products
            }

            return View("ProductForm", product); // If model validation fails, redisplay the form
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id, IFormFile ImageFile)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("ProductForm", product); // Pass the product to the form for editing
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Fetch the existing product from the database
                var existingProduct = await _context.Products.FindAsync(product.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                // If a new image is uploaded, process the new image file
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Define the directory where the images will be stored
                    var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                    // Ensure the directory exists
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                    }

                    // Define a unique name for the file based on product name
                    var fileName = $"{Path.GetFileNameWithoutExtension(product.ProductName)}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(ImageFile.FileName)}";
                    var filePath = Path.Combine(imageDirectory, fileName);

                    // Save the new image file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Delete the old image file if necessary (optional)
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Update the ImageUrl property with the new file path
                    existingProduct.ImageUrl = $"/Images/{fileName}";
                }

                // Update other product properties as needed
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.ProductType = product.ProductType;
                existingProduct.ProductDescription = product.ProductDescription;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction("ProductList"); // Redirect to a page listing all products
            }

            return View("ProductForm",product); // If model validation fails, redisplay the form
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Inventories) // Include inventories to be deleted
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                // Remove the associated inventory records
                _context.Inventories.RemoveRange(product.Inventories);

                // Remove the product itself
                _context.Products.Remove(product);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ProductList");
        }

    }
}
