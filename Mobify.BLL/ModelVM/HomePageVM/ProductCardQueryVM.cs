namespace Mobify.BLL.ModelVM.HomePageVM
{
    public class ProductCardQueryVM
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? Price { get; set; } 
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Sort { get; set; }

    }
}
