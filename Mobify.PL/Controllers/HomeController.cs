using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobify.BLL.ModelVM.HomePageVM;
using Mobify.BLL.Services.Abstraction;
using Mobify.BLL.Services.Implmentation;
using Mobify.PL.Models;

namespace Mobify.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomePageServices services;
        private readonly ICategoryService categoryService;

        public IBrandServices brandServices { get; }

        public HomeController(IHomePageServices services , IBrandServices brandServices , ICategoryService categoryService)
        {
            this.services = services;
            this.brandServices = brandServices;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index([FromQuery] AllHomePageComponent vm)
        {
            vm.ProductCardQueryVM.PageSize = vm.ProductCardQueryVM.PageSize <= 0 ? 10 : vm.ProductCardQueryVM.PageSize > 100 ? 100 : vm.ProductCardQueryVM.PageSize;
            vm.ProductCardQueryVM.Page = vm.ProductCardQueryVM.Page <= 0 ? 1 : vm.ProductCardQueryVM.Page;
            var res =await services.GetProductsCard(vm.ProductCardQueryVM);
            if(!res.IsHasErrorOrNot)
            {
                vm.ProductsCardVM = res.result;
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_ProductsGrid", vm);

            var Brands =await brandServices.GetBrandsAndCountOfProduct();
            var Categories = await categoryService.GetCategoryAndCountOfProduct();
            vm.brandAndCountOfProducts = Brands.result;
            vm.CategoryAndCountOfProducts = Categories.result;
            vm.ProductCardQueryVM = vm.ProductCardQueryVM;
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
