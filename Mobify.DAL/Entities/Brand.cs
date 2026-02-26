namespace Mobify.DAL.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public BrandPhoto BrandPhoto { get; set; } = new BrandPhoto();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
