using Microsoft.AspNetCore.Http;
namespace Mobify.BLL.ModelVM.BrandVM
{
    public class AddBrandVM
    {
        public string Name { get; set; }        
        public IFormFile formFile { get; set; }
    }
}
