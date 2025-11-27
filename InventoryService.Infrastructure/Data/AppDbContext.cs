using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; } = null!;
		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<Material> Materials { get; set; } = null!;
		public DbSet<Supplier> Suppliers { get; set; } = null!;
		public DbSet<ProductMaterial> ProductMaterials { get; set; } = null!;
		public DbSet<ProductVariant> ProductVariants { get; set; } = null!;
		public DbSet<Warehouse> Warehouses { get; set; } = null!;
		public DbSet<StockItem> StockItems { get; set; } = null!;
		public DbSet<InventoryTransaction> InventoryTransactions { get; set; } = null!;
		public DbSet<BomItem> BomItems { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Product
			modelBuilder.Entity<Product>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(200);
				b.Property(x => x.SKU).IsRequired().HasMaxLength(50);
				b.HasIndex(x => x.SKU).IsUnique();
				b.Property(x => x.Price).HasColumnType("decimal(18,2)");
				b.Property(x => x.Cost).HasColumnType("decimal(18,2)");
				b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
				b.Property(x => x.LastUpdated).HasDefaultValueSql("GETUTCDATE()");
				b.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
			});

			// Category (self ref)
			modelBuilder.Entity<Category>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(100);
				b.HasOne(x => x.ParentCategory).WithMany(x => x.Children).HasForeignKey(x => x.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
			});

			// Material
			modelBuilder.Entity<Material>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(100);
				b.Property(x => x.UnitCost).HasColumnType("decimal(18,4)");
				b.HasOne(x => x.Supplier).WithMany(s => s.Materials).HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.SetNull);
			});

			// Supplier
			modelBuilder.Entity<Supplier>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(200);
			});

			// ProductMaterial (join)
			modelBuilder.Entity<ProductMaterial>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
				b.Property(x => x.WasteFactor).HasColumnType("decimal(18,4)");
				b.HasOne(x => x.Product).WithMany(p => p.Materials).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
				b.HasOne(x => x.Material).WithMany(m => m.ProductMaterials).HasForeignKey(x => x.MaterialId).OnDelete(DeleteBehavior.Cascade);
				b.HasIndex(x => new { x.ProductId, x.MaterialId }).IsUnique(false);
			});

			// ProductVariant
			modelBuilder.Entity<ProductVariant>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(100);
				b.Property(x => x.SKU).HasMaxLength(50);
				b.Property(x => x.AdditionalCost).HasColumnType("decimal(18,2)");
				b.HasOne(x => x.Product).WithMany(p => p.Variants).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
				b.HasIndex(x => new { x.ProductId, x.SKU }).IsUnique(false);
			});

			// Warehouse
			modelBuilder.Entity<Warehouse>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(150);
			});

			// StockItem
			modelBuilder.Entity<StockItem>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
				b.Property(x => x.ReservedQuantity).HasColumnType("decimal(18,4)");
				b.Property(x => x.AverageCost).HasColumnType("decimal(18,4)");
				b.HasOne(x => x.ProductVariant).WithMany(v => v.StockItems).HasForeignKey(x => x.ProductVariantId).OnDelete(DeleteBehavior.Cascade);
				b.HasOne(x => x.Warehouse).WithMany(w => w.StockItems).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Cascade);
				b.HasIndex(x => new { x.ProductVariantId, x.WarehouseId }).IsUnique();
			});

			// InventoryTransaction
			modelBuilder.Entity<InventoryTransaction>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
				b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
				b.HasOne(x => x.StockItem).WithMany().HasForeignKey(x => x.StockItemId).OnDelete(DeleteBehavior.SetNull);
			});

			// BomItem
			modelBuilder.Entity<BomItem>(b =>
			{
				b.HasKey(x => x.Id);
				b.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
				b.Property(x => x.ScrapFactor).HasColumnType("decimal(18,4)");
				b.HasOne(x => x.Product).WithMany(p => p.BomItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
				b.HasOne(x => x.ComponentProduct).WithMany().HasForeignKey(x => x.ComponentProductId).OnDelete(DeleteBehavior.Restrict);
				b.HasOne(x => x.ComponentMaterial).WithMany().HasForeignKey(x => x.ComponentMaterialId).OnDelete(DeleteBehavior.Restrict);
			});

			// Invoice
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);

                entity.HasMany(i => i.Materials)
                      .WithOne(m => m.Invoice)
                      .HasForeignKey(m => m.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
		}
	}
}