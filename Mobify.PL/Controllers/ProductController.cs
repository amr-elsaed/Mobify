using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobify.BLL.ModelVM.ProductVM;
using Mobify.BLL.ModelVM.ResponseResult;
using Mobify.BLL.Services.Abstraction;
using Mobify.BLL.Services.Implmentation;
using Mobify.DAL.Entities;
using System.Threading.Tasks;

namespace Mobify.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly IProductServices services;
        private readonly ICategoryService categoryServices ;
        private readonly IBrandServices brandServices;
        
        public ProductController(IProductServices services , ICategoryService categoryService , IBrandServices brandServices )
        {
            this.services = services;
            this.categoryServices = categoryService;
            this.brandServices = brandServices;
        }
        public async Task<IActionResult> Index([FromQuery] ProductQueryVM vm)
        {
            vm.PageSize = vm.PageSize <= 0 ? 10 : vm.PageSize > 100 ? 100 : vm.PageSize;
            vm.Page = vm.Page <= 0 ? 1 : vm.Page;

            var res = await services.GetAll(vm);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductsTablePartial", res);
            }

            var categories = await categoryServices.GetAll();
            var brands = await brandServices.GetAll();

            ViewBag.Categories = categories.result;
            ViewBag.Brands = brands.result;

            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryServices.GetAll();
            ViewBag.Categories = categories.result;
            var brands = await brandServices.GetAll();
            ViewBag.Brands = brands.result;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductVM vm)
        {
            if (ModelState.IsValid)
            {
                await services.Add(vm);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id)
        {
            var res = await services.GetForEdit(Id);
            var categories = await categoryServices.GetAll();
            ViewBag.Categories = categories.result;
            var brands = await brandServices.GetAll();
            ViewBag.Brands = brands.result;
            return View(res.result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(EditProductVM vm)
        {
            if (ModelState.IsValid)
            {
                await services.SaveEdit(vm);
                return RedirectToAction(nameof(Index));
            }

            var categories = await categoryServices.GetAll();
            ViewBag.Categories = categories.result;

            var brands = await brandServices.GetAll();
            ViewBag.Brands = brands.result;

            return View("Edit", vm);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await services.Delete(id);
            if (!res.IsHasErrorOrNot)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
      
        public async Task<IActionResult> UpdateOffer(int ProductId)
        {
            var res = await services.GetProductOffer(ProductId);
            return View(res.result);
        }
        public async Task<IActionResult> SaveUpdateOfferAsync(ProductOfferVM vm)
        {
            await services.UpdateOffer(vm);
            return RedirectToAction("Index");
        }
    }
}
