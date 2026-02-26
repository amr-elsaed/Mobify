namespace Mobify.DAL.DataBase.Configuration
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(b => b.Id);
            builder.HasMany(b=>b.Products).WithOne(p=>p.Brand).HasForeignKey(p=>p.BrandId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(b => b.BrandPhoto).WithOne(ph => ph.Brand).HasForeignKey<BrandPhoto>(ph => ph.BrandId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
