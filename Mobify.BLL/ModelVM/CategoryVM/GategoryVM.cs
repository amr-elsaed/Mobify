using System.ComponentModel.DataAnnotations;

namespace Mobify.BLL.ModelVM.CategoryVM
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
