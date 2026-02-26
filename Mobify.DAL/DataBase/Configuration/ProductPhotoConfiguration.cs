
namespace Mobify.DAL.DataBase.Configuration
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.ToTable("ProductPhotoes");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.PhotoUrl).IsRequired().HasMaxLength(255);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductPhotos).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
