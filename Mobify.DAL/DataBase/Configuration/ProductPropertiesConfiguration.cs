
namespace Mobify.DAL.DataBase.Configuration
{
    public class ProductPropertiesConfiguration : IEntityTypeConfiguration<ProductProperties>
    {
        public void Configure(EntityTypeBuilder<ProductProperties> builder)
        {
            builder.ToTable("ProductProperties");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Discription).HasColumnType("VARCHAR").IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductProperties).HasForeignKey(x => x.ProductId);
        }
    }
}
