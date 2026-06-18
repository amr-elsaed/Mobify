using Microsoft.AspNetCore.Mvc;
using Mobify.BLL.Services.Abstraction;
using Mobify.BLL.Services.Implmentation;
using System.Threading.Tasks;

namespace Mobify.PL.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailsService servics;

        public ProductDetailsController(IProductDetailsService servics)
        {
            this.servics = servics;
        }

        public async Task<IActionResult> GetProductDetails(int Id)
        {
            var product =await servics.GetProductDetailsByIdAsync(Id);
            return View(product.result);
        }
    }
}
