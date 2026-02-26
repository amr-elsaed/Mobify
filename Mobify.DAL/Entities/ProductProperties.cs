namespace Mobify.DAL.Entities
{
    public class ProductProperties
    {
        public int Id {  get; set; }
        public string Discription { get; set; }
        public bool IsAdvantage { get; set; }
        // Navigation Property
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
