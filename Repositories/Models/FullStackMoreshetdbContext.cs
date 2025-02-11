using Microsoft.EntityFrameworkCore;

namespace Repositories.Models;

public partial class FullStackMoreshetdbContext : DbContext
{
    public FullStackMoreshetdbContext()
    {
    }

    public FullStackMoreshetdbContext(DbContextOptions<FullStackMoreshetdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ItemsForOrder> ItemsForOrders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:moreshetserver.database.windows.net,1433;Initial Catalog=moreshetdb;Persist Security Info=False;User ID=admn;Password=1qaz@WSX3edc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_User");
        });

        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.ToTable("CartProduct");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartProduct_Cart");

            entity.HasOne(d => d.Product).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartProduct_Product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ItemsForOrder>(entity =>
        {
            entity.ToTable("ItemsForOrder");

            entity.HasOne(d => d.Order).WithMany(p => p.ItemsForOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemsForOrder_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.ItemsForOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemsForOrder_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Notes).HasMaxLength(50);

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_Order_Cart");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Comment).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber1).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber2).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
