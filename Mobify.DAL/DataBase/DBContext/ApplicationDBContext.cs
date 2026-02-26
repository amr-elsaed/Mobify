using Mobify.DAL.DataBase.Configuration;

namespace Mobify.DAL.DataBase.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Brand> Brands {  get; set; }
        public DbSet<BrandPhoto> BrandPhotos {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<ProductPhoto> ProductPhotos {  get; set; }
        public DbSet<ProductProperties> ProductProperties {  get; set; }
        public DbSet<ProductVariantPhoto> productVariantPhotos {  get; set; }
        public DbSet<ProductVariants> ProductVariants {  get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
