namespace Mobify.BLL.ModelVM.ProductVM
{
    public class ShowProductVM
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
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ProductPhotoURL { get; set; } 
        public bool IsActive { get; set; }
    }
}
