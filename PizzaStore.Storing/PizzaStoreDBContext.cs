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
        public virtual DbSet<Order1> Order1 { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Pizza1> Pizza1 { get; set; }
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

                entity.Property(e => e.CrustId)
                    .HasColumnName("CrustID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login", "User");

                entity.Property(e => e.LoginId)
                    .HasColumnName("LoginID")
                    .ValueGeneratedNever();

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

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOrdered)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Order__StoreID__72C60C4A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__UserID__71D1E811");
            });

            modelBuilder.Entity<Order1>(entity =>
            {
                entity.HasKey(e => e.StoreOrderId)
                    .HasName("PK__Order__A74CB93A906F38F9");

                entity.ToTable("Order", "Store");

                entity.Property(e => e.StoreOrderId)
                    .HasColumnName("StoreOrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Order1)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Order__OrderID__7A672E12");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Order1)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Order__StoreID__797309D9");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.HasKey(e => e.PizzaOrderId)
                    .HasName("PK__Pizza__EA09DF5D8686063B");

                entity.ToTable("Pizza", "Order");

                entity.Property(e => e.PizzaOrderId)
                    .HasColumnName("PizzaOrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Pizza__OrderID__75A278F5");

                entity.HasOne(d => d.PizzaNavigation)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__Pizza__PizzaID__76969D2E");
            });

            modelBuilder.Entity<Pizza1>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK__Pizza__0B6012FD0022EF31");

                entity.ToTable("Pizza", "Pizza");

                entity.Property(e => e.PizzaId)
                    .HasColumnName("PizzaID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CrustId).HasColumnName("CrustID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.Pizza1)
                    .HasForeignKey(d => d.CrustId)
                    .HasConstraintName("FK__Pizza__CrustID__4F7CD00D");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizza1)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK__Pizza__SizeID__5070F446");
            });

            modelBuilder.Entity<PizzaTopping>(entity =>
            {
                entity.ToTable("PizzaTopping", "Pizza");

                entity.Property(e => e.PizzaToppingId)
                    .HasColumnName("PizzaToppingID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaTopping)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__PizzaTopp__Pizza__7D439ABD");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.PizzaTopping)
                    .HasForeignKey(d => d.ToppingId)
                    .HasConstraintName("FK__PizzaTopp__Toppi__7E37BEF6");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size", "Pizza");

                entity.Property(e => e.SizeId)
                    .HasColumnName("SizeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "Store");

                entity.Property(e => e.StoreId)
                    .HasColumnName("StoreID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Store__LoginID__6B24EA82");
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

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.LoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__LoginID__68487DD7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
