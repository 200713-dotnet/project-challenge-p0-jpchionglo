using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaStore.Storing
{
    public partial class PizzaStoreDBContext : DbContext
    {
        public PizzaStoreDBContext()
        {
        }

        public PizzaStoreDBContext(DbContextOptions<PizzaStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crust> Crust { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderJunction> OrderJunction { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<PizzaJunction> PizzaJunction { get; set; }
        public virtual DbSet<PizzaTopping> PizzaTopping { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Topping> Topping { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=PizzaStoreDB;user id=sa;password=Password123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crust>(entity =>
            {
                entity.ToTable("Crust", "Pizza");

                entity.Property(e => e.CrustId).HasColumnName("CrustID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login", "User");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(259);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.DateOrdered)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Order__StoreID__55F4C372");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__UserID__55009F39");
            });

            modelBuilder.Entity<OrderJunction>(entity =>
            {
                entity.HasKey(e => e.StoreOrderId)
                    .HasName("PK__OrderJun__A74CB93A57BF3209");

                entity.ToTable("OrderJunction", "Store");

                entity.Property(e => e.StoreOrderId).HasColumnName("StoreOrderID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderJunction)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderJunc__Order__5D95E53A");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.OrderJunction)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__OrderJunc__Store__5CA1C101");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "Pizza");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.CrustId).HasColumnName("CrustID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.CrustId)
                    .HasConstraintName("FK__Pizza__CrustID__4A8310C6");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK__Pizza__SizeID__4B7734FF");
            });

            modelBuilder.Entity<PizzaJunction>(entity =>
            {
                entity.HasKey(e => e.PizzaOrderId)
                    .HasName("PK__PizzaJun__EA09DF5D3E874A08");

                entity.ToTable("PizzaJunction", "Order");

                entity.Property(e => e.PizzaOrderId).HasColumnName("PizzaOrderID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PizzaJunction)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__PizzaJunc__Order__58D1301D");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaJunction)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__PizzaJunc__Pizza__59C55456");
            });

            modelBuilder.Entity<PizzaTopping>(entity =>
            {
                entity.ToTable("PizzaTopping", "Pizza");

                entity.Property(e => e.PizzaToppingId).HasColumnName("PizzaToppingID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaTopping)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__PizzaTopp__Pizza__4E53A1AA");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.PizzaTopping)
                    .HasForeignKey(d => d.ToppingId)
                    .HasConstraintName("FK__PizzaTopp__Toppi__4F47C5E3");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size", "Pizza");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "Store");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Store__LoginID__5224328E");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.ToTable("Topping", "Pizza");

                entity.Property(e => e.ToppingId)
                    .HasColumnName("ToppingID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(250);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(250);

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.LoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__LoginID__42E1EEFE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
