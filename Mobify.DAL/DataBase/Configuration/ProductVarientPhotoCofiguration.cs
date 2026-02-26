
namespace Mobify.DAL.DataBase.Configuration
{
    internal class ProductVarientPhotoCofiguration : IEntityTypeConfiguration<ProductVariantPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductVariantPhoto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ProductVariant).WithMany(x => x.Photos).HasForeignKey(x => x.ProductVariantId);
        }
    }
}
