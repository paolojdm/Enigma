﻿// <auto-generated />
using System;
using Enigma_Protocol.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enigma_Protocol.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241004151425_AddPuzzleDataDefault")]
    partial class AddPuzzleDataDefault
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Enigma_Protocol.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Carts", (string)null);
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityAvailable")
                        .HasColumnType("int");

                    b.Property<int>("QuantityReserved")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.ToTable("Inventory", (string)null);
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ShippingStatus")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Enigma_Protocol.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("Enigma_Protocol.Models.PlayerProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CurrentRoomTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Current_Lives_Puzzle")
                        .HasColumnType("int");

                    b.Property<int>("Current_Lives_Room")
                        .HasColumnType("int");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RoomStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SolvedPuzzles")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrentRoomId");

                    b.HasIndex("PlayerID");

                    b.ToTable("PlayerProgress", (string)null);
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Puzzle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Puzzles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Answer = "4359",
                            Question = "Enter the code",
                            RoomId = 1
                        },
                        new
                        {
                            Id = 2,
                            Answer = "true",
                            Question = "Reorder the image",
                            RoomId = 1
                        },
                        new
                        {
                            Id = 3,
                            Answer = "tenebre",
                            Question = "Enter the right word",
                            RoomId = 2
                        },
                        new
                        {
                            Id = 4,
                            Answer = "uscita",
                            Question = "Enter the right word",
                            RoomId = 3
                        });
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoomDescription")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Rooms", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoomDescription = "A misterious bedroom.",
                            RoomName = "Bedroom"
                        },
                        new
                        {
                            Id = 2,
                            RoomDescription = "A strange dining room.",
                            RoomName = "Dining room"
                        },
                        new
                        {
                            Id = 3,
                            RoomDescription = "An ancient armoury.",
                            RoomName = "Armoury"
                        });
                });

            modelBuilder.Entity("Enigma_Protocol.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CardCVC")
                        .HasColumnType("int");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardOwner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ShippingAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 10, 4, 17, 14, 25, 415, DateTimeKind.Local).AddTicks(5539),
                            Email = "testemail2@gmail.com",
                            IsAdmin = false,
                            PasswordHash = "AQAAAAIAAYagAAAAEMhU2fip+YkyWX1Lb9EePrwEBx3DN9pUTDdInCNp1otbhZIilQMpvs4RLsNyoif49w==",
                            UserName = "Presentazione_Utente1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 10, 4, 17, 14, 25, 415, DateTimeKind.Local).AddTicks(5591),
                            Email = "testemail3@gmail.com",
                            IsAdmin = true,
                            PasswordHash = "AQAAAAIAAYagAAAAEF6JuNnjowt+kv+JEecKS5+XYbBvS1E0jcz+iqWiv8HkrqSGGmmwa6QAyA1MN7ZmAQ==",
                            UserName = "Presentazione_Admin1"
                        });
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Cart", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enigma_Protocol.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Inventory", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.Product", "Product")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Order", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.OrderDetail", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enigma_Protocol.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.PlayerProgress", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.Room", "CurrentRoom")
                        .WithMany()
                        .HasForeignKey("CurrentRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enigma_Protocol.Models.User", "User")
                        .WithMany("playerProgresses")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentRoom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Puzzle", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.Room", "Room")
                        .WithMany("Puzzles")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Review", b =>
                {
                    b.HasOne("Enigma_Protocol.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enigma_Protocol.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Product", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.Room", b =>
                {
                    b.Navigation("Puzzles");
                });

            modelBuilder.Entity("Enigma_Protocol.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("playerProgresses");
                });
#pragma warning restore 612, 618
        }
    }
}
