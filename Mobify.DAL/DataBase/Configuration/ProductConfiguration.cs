
namespace Mobify.DAL.DataBase.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Description).HasColumnType("VARCHAR").IsRequired();
            builder.Property(p => p.CPU).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Screen).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Battary).HasMaxLength(250).IsRequired();
            builder.Property(p => p.StockQuantity).HasColumnType("INT").IsRequired();
            builder.Property(p => p.Price).HasColumnType("DECIMAL").IsRequired();
            builder.HasOne(p=>p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
            builder.HasOne(p=>p.Brand).WithMany(p => p.Products).HasForeignKey(p => p.BrandId);
            builder.HasMany(p=>p.ProductPhotos).WithOne(ph=>ph.Product).HasForeignKey(p=> p.ProductId);
            builder.HasMany(p=>p.ProductProperties).WithOne(ph=>ph.Product).HasForeignKey(p=> p.ProductId);
        }
    }
}
