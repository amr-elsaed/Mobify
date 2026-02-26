namespace Mobify.DAL.DataBase.Configuration
{
    public class ProductVarientsCofiguration : IEntityTypeConfiguration<ProductVariants>
    {

        public void Configure(EntityTypeBuilder<ProductVariants> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Color).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Storage).IsRequired().HasMaxLength(25);
            builder.Property(x => x.RAM).IsRequired().HasMaxLength(25);
            builder.HasMany(x => x.Photos).WithOne(x => x.ProductVariant).HasForeignKey(x => x.ProductVariantId);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductVariants).HasForeignKey(x => x.ProductId);
        }
    }
}
