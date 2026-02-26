using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mobify.BLL.ModelVM.BrandVM;
using Mobify.BLL.Services.Abstraction;
using System.Threading.Tasks;

namespace Mobify.PL.Controllers
{
    public class BrandController : Controller
    {
        IBrandServices services;
        public BrandController(IBrandServices services)
        {
            this.services = services;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var AllBrand = await services.GetAll();
                if (!AllBrand.IsHasErrorOrNot)
                {
                    return View(AllBrand.result);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddAsync(AddBrandVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", vm);
            }
            var res = await services.Add(vm);
            if (res.IsHasErrorOrNot)
            {
                ModelState.AddModelError("", res.ErrorMessage ?? "An error occurred.");
                return View("Add",vm);
            };
            TempData["Success"] = "Category created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            var brand =await services.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand.result);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            if (Id != 0)
            {
                var res = await services.Delete(Id);
                if (!res.IsHasErrorOrNot)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error");
            }
            return View("Delete");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var res = await services.GetByIdForEdit(Id);
            if (!res.IsHasErrorOrNot)
            {
                return View(res.result);
            }
            else
            {
                return NotFound(res.ErrorMessage);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(EditBrandVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await services.SaveUpdate(vm);
                    return RedirectToAction(nameof(Index));
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
