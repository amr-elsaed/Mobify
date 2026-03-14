using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobify.DAL.Entities
{
    public class ProductOffer
    {
        public bool HasOffer { get; set;}
        public decimal NewPrice { get; set;}
        public int Precentage { get; set;}
        [Key]
        public int ProductId { get; set;}
        [ForeignKey("ProductId")]
        public Product Product { get; set;}
    }
}
