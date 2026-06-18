namespace Mobify.BLL.ModelVM.ProductDetailsVM
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CPU { get; set; }
        public string Screen { get; set; }
        public string Camera { get; set; }
        public string Battary { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public string RAM { get; set; }
        public bool HasOffer = false;
        public int? Precentage { get; set; }
        public decimal? OfferPrice { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public List<string> ProductPhotos { get; set; }
        public List<string> AdvProductProperties { get; set; }
        public List<string> DisAdvProductProperties { get; set; }
    }
}
