namespace Mobify.DAL.Entities
{
    public class ProductVariantPhoto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }

        // Navigation Property
        public int ProductVariantId { get; set; }
        public ProductVariants ProductVariant { get; set; }
    }
}
