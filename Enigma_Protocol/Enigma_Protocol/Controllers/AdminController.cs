using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enigma_Protocol.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }



        //Admin/ManagingProducts
        #region
        // GET: /Admin/Products
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: /Admin/CreateProduct
        public IActionResult CreateProduct()
        {
            return View();
        }

        // POST: /Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();

                // Optionally, create an inventory entry
                var inventory = new Inventory
                {
                    ProductID = model.Id,
                    QuantityAvailable = 0,
                    QuantityReserved = 0,
                    LastUpdated = DateTime.Now
                };
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();

                return RedirectToAction("Products");
            }
            return View(model);
        }

        // GET: /Admin/EditProduct/5
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: /Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Products");
            }
            return View(model);
        }

        // GET: /Admin/DeleteProduct/5
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: /Admin/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);

            // Optionally, remove the inventory entry
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductID == id);
            if (inventory != null) _context.Inventories.Remove(inventory);

            await _context.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        #endregion

        //Admin/ManagingOrders
        #region
        // GET: /Admin/Orders
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders.Include(o => o.User).ToListAsync();
            return View(orders);
        }

        // GET: /Admin/OrderDetails/5
        public async Task<IActionResult> OrderDetails(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        #endregion

        //Admin/ManagingReviews
        #region
        // GET: /Admin/Reviews
        public async Task<IActionResult> Reviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();
            return View(reviews);
        }

        // GET: /Admin/DeleteReview/5
        public async Task<IActionResult> DeleteReview(int? id)
        {
            if (id == null) return NotFound();

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return NotFound();

            return View(review);
        }

        // POST: /Admin/DeleteReview/5
        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Reviews");
        }

        // GET: /Admin/CommentReview/5
        public async Task<IActionResult> CommentReview(int? id)
        {
            if (id == null) return NotFound();

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return NotFound();

            var model = new CommentReviewViewModel
            {
                ReviewId = review.Id,
                OriginalComment = review.Comment,
                AdminComment = string.Empty
            };

            return View(model);
        }

        // POST: /Admin/CommentReview/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentReview(int id, CommentReviewViewModel model)
        {
            if (id != model.ReviewId) return NotFound();

            if (ModelState.IsValid)
            {
                var adminComment = new Review
                {
                    ProductId = model.ProductId,
                    UserId = /* Admin UserId */,
                    Comment = model.AdminComment,
                    CreatedAt = DateTime.Now
                };

                _context.Reviews.Add(adminComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Reviews");
            }
            return View(model);
        }

        #endregion


    }
}
