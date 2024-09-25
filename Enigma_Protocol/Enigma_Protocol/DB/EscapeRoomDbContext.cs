using Enigma_Protocol.Models;
using Microsoft.EntityFrameworkCore;

namespace Enigma_Protocol.DB
{
    public class EscapeRoomDbContext : DbContext
    {

        public EscapeRoomDbContext(DbContextOptions<EscapeRoomDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<PlayerProgress> PlayerProgresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetails> OrdersDetails { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relazione uno-a-molti: User -> Orders
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne()
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Carts)
                .WithOne()
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione uno-a-molti: Order -> OrderDetails
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrdersDetails)
                .WithOne()
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione uno-a-molti: Room -> Puzzles
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Puzzles)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione molti-a-uno: PlayerProgress -> Room
            modelBuilder.Entity<PlayerProgress>()
                .HasOne(pp => pp.CurrentRoom)
                .WithMany()
                .HasForeignKey(pp => pp.CurrentRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazione molti-a-molti: PlayerProgress -> Puzzles (SolvedPuzzles)
            modelBuilder.Entity<PlayerProgress>()
                .HasMany(pp => pp.SolvedPuzzles)
                .WithMany(p => p.SolvedByPlayers); // Devi aggiungere questa proprietà di navigazione a Puzzle

            // Relazione molti-a-uno: Cart -> User
            modelBuilder.Entity<Cart>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Inventory)
                .WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
