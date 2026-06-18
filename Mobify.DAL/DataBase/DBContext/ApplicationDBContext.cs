using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Mobify.DAL.DataBase.DBContext
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Brand> Brands {  get; set; }
        public DbSet<BrandPhoto> BrandPhotos {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<ProductPhoto> ProductPhotos {  get; set; }
        public DbSet<ProductOffer> ProductOffers {  get; set; }
        public DbSet<ProductProperties> ProductProperties {  get; set; }
        
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
