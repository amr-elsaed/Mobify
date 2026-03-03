namespace Mobify.BLL.ModelVM.ProductVM
{
    public class SaveEditVM
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
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public List<string> ExistingPhoto { get; set; } = new List<string>();
        public List<string> PhotoesToDelete { get; set; } = new List<string>();
        public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();
        public List<string> ProductNewPhotoes { get; set; } = new List<string>();
        public List<string> AdvProperties { get; set; } = new List<string>();
        public List<string> DisAdvProperties { get; set; } = new List<string>();
    }
}
