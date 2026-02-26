namespace Mobify.DAL.Entities
{
    public class BrandPhoto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }

        // Navigation Property
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
