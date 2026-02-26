namespace Mobify.DAL.DataBase.Configuration
{
    public class BrandPhotoConfiguration : IEntityTypeConfiguration<BrandPhoto>
    {
        public void Configure(EntityTypeBuilder<BrandPhoto> builder)
        {
            builder.ToTable("BrandPhotoes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PhotoUrl).HasMaxLength(255);
            builder.HasOne(ph => ph.Brand).WithOne(b => b.BrandPhoto).HasForeignKey<BrandPhoto>(ph => ph.BrandId);
        }
    }
}
