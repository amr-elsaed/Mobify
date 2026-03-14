namespace Mobify.BLL.ModelVM.HomePageVM
{
    public class ProductCardVM
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string PtoductName { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? OfferAsPrecentage { get; set; }
        public string PhotoURL {  get; set; }
        public decimal? OfferPriice { get; set; }
        public bool HasOffer { get; set; }
        public string CategoryName { get; set; }
    }
}
