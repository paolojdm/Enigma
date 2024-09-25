using Enigma_Protocol.Models;
using Microsoft.EntityFrameworkCore;

namespace Enigma_Protocol.DB
{
    public class ApplicationDbContext : DbContext
    { 
    // DbSets for each entity
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Puzzle> Puzzles { get; set; }
    public DbSet<PlayerProgress> PlayerProgresses { get; set; }

    // Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.ShippingAddress).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.ProductDescription).HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.ProductType).IsRequired().HasMaxLength(255);
        });

        // Configure Inventory entity
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.QuantityAvailable).IsRequired();
            entity.Property(e => e.QuantityReserved).IsRequired();
            entity.Property(e => e.LastUpdated).IsRequired();

            // Foreign key to Products
            entity.HasOne(e => e.Product)
                  .WithMany(p => p.Inventories)
                  .HasForeignKey(e => e.ProductID);
        });

        // Configure Cart entity
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Carts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();

            // Explicitly configure the foreign key for Inventory
            entity.HasOne(e => e.Inventory)
                    .WithMany()  // If Inventory does not have a navigation property for Cart
                    .HasForeignKey(e => e.InventoryID)  // Use only InventoryID as the FK
                    .OnDelete(DeleteBehavior.Cascade);  // Cascade delete behavior

            // Explicitly configure the foreign key for User
            entity.HasOne(e => e.User)
                    .WithMany(u => u.Carts)  // Assuming User has a collection of Carts
                    .HasForeignKey(e => e.UserID)
                    .OnDelete(DeleteBehavior.Cascade);
        });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderDate).IsRequired();
            entity.Property(e => e.TotalAmount).IsRequired();
            entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(255);
            entity.Property(e => e.ShippingStatus).HasMaxLength(255);
            entity.Property(e => e.TrackingNumber).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).IsRequired();

            // Foreign key to User
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Orders)
                  .HasForeignKey(e => e.UserID);
        });

        // Configure OrderDetail entity
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetails");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.UnitPrice).IsRequired();

            // Foreign key to Order
            entity.HasOne(e => e.Order)
                  .WithMany(o => o.OrderDetails)
                  .HasForeignKey(e => e.OrderID);

            // Foreign key to Product
            entity.HasOne(e => e.Product)
                  .WithMany(p => p.OrderDetails)
                  .HasForeignKey(e => e.ProductId);
        });

        // Configure Room entity
        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Rooms");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RoomName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.RoomDescription).IsRequired().HasColumnType("NVARCHAR(MAX)");
        });

        // Configure Puzzle entity
        modelBuilder.Entity<Puzzle>(entity =>
        {
            entity.ToTable("Puzzles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired().HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.Answer).IsRequired().HasColumnType("NVARCHAR(MAX)");

            // Foreign key to Room
            entity.HasOne(e => e.Room)
                  .WithMany(r => r.Puzzles)
                  .HasForeignKey(e => e.RoomId);
        });

        // Configure PlayerProgress entity
        modelBuilder.Entity<PlayerProgress>(entity =>
        {
            entity.ToTable("PlayerProgress");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PlayerName).IsRequired().HasMaxLength(255);

            // Foreign key to Room
            entity.HasOne(e => e.CurrentRoom)
                  .WithMany()
                  .HasForeignKey(e => e.CurrentRoomId);
        });

        base.OnModelCreating(modelBuilder);
    }
    }
}
