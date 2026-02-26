using System.Diagnostics;

namespace Mobify.DAL.Entities
{
    public class ProductVariants
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Storage { get; set; }
        public string RAM { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public List<ProductVariantPhoto> Photos { get; set; }
    }
}

