namespace Mobify.DAL.DataBase.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.HasMany(c=>c.Products).WithOne(c=>c.Category).HasForeignKey(p=>p.CategoryId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
