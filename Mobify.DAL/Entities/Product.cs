namespace Mobify.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CPU { get; set; }
        public string Screen { get; set; }
        public string Camera { get; set; }
        public string Battary { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public string RAM { get; set; }
        

        // Navigation Property
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<ProductPhoto> ProductPhotos { get; set; } = new List<ProductPhoto>();
        public List<ProductProperties> ProductProperties { get; set; } = new List<ProductProperties>();
        public ProductOffer productOffer { get; set; }

    }
}
