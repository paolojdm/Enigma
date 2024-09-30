using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Enigma_Protocol.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> InventoryList()
        {
            var inventories = await _context.Inventories
                .Include(i => i.Product) // Include product details
                .ToListAsync();

            return View(inventories);
        }

        [HttpGet]
        public IActionResult AddInventory()
        {
            return View(new Inventory()); // Pass an empty Inventory model
        }

        [HttpPost]
        public async Task<IActionResult> AddInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("InventoryList");
            }

            return View(inventory); // Return to the form if not valid
        }

        // Action to edit inventory
        [HttpGet]
        public async Task<IActionResult> EditInventory(int productId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductID == productId);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                inventory.LastUpdated = DateTime.Now;
                _context.Inventories.Update(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("InventoryList"); // Ensure this action exists
            }
            return View(inventory); // Return the view if validation fails
        }

        [HttpGet]
        public async Task<IActionResult> DeleteInventory(int productId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductID == productId);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("InventoryList");
        }
    }
}