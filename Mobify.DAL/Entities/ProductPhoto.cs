namespace Mobify.DAL.Entities
{
    public class ProductPhoto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }

        // Navigation Property
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
