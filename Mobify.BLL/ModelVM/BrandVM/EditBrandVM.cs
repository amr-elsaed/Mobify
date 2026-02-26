namespace Mobify.BLL.ModelVM.BrandVM
{
    public class EditBrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? currentPhoto { get; set; }
        public IFormFile? formfile { get; set; }
    }
}
