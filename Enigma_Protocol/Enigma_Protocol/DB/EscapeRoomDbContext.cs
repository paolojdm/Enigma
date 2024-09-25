using Enigma_Protocol.Models;
using Microsoft.EntityFrameworkCore;

namespace Enigma_Protocol.DB
{
    public class EscapeRoomDbContext : DbContext
    {
        public EscapeRoomDbContext(DbContextOptions<EscapeRoomDbContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<PlayerProgress> PlayerProgresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; } // Include Inventory as well

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Inventory to Product relationship
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Inventories) // Assuming Product has a collection of Inventories
                .HasForeignKey(i => i.ProductID);

            // Inventory to Cart relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Inventory)
                .WithMany(i => i.Carts)
                .HasForeignKey(c => c.InventoryID);

            // Define the primary key for Inventory
            modelBuilder.Entity<Inventory>()
                .HasKey(i => i.InventoryId);

            // Define the primary key for OrderDetails
            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => od.OrderDetailId);

            // Define relationships for OrderDetails
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrdersDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

            // User to Orders relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Carts relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Carts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Order to OrderDetails relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrdersDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Room to Puzzles relationship
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Puzzles)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // PlayerProgress to Room relationship (many-to-one)
            modelBuilder.Entity<PlayerProgress>()
                .HasOne(pp => pp.CurrentRoom)
                .WithMany(r => r.PlayerProgresses) // Add a collection in Room if necessary
                .HasForeignKey(pp => pp.CurrentRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // PlayerProgress to Puzzles (many-to-many)
            modelBuilder.Entity<PlayerProgressPuzzle>()
                .HasKey(pp => new { pp.PlayerProgressId, pp.PuzzleId });

            modelBuilder.Entity<PlayerProgressPuzzle>()
                .HasOne(pp => pp.PlayerProgress)
                .WithMany(pp => (IEnumerable<PlayerProgressPuzzle>)pp.SolvedPuzzles)
                .HasForeignKey(pp => pp.PlayerProgressId);

            modelBuilder.Entity<PlayerProgressPuzzle>()
                .HasOne(pp => pp.Puzzle)
                .WithMany(p => (IEnumerable<PlayerProgressPuzzle>)p.SolvedByPlayers)
                .HasForeignKey(pp => pp.PuzzleId);

            // Cart to User relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Product to Inventory relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Inventory)
                .WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductID);

            // Product to OrderDetails relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
