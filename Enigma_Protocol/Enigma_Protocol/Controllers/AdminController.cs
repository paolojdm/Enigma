using Enigma_Protocol.DB;
using Enigma_Protocol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;


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
                int v = 0;
                var adminUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                v = int.Parse(adminUserId!);
                var adminComment = new Review
                {
                    ProductId = model.ProductId,
                    UserId = v,
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
