using Microsoft.AspNetCore.Mvc;
using Mobify.BLL.ModelVM.CategoryVM;
using Mobify.BLL.Services.Abstraction;
namespace Mobify.PL.Controllers
{
    public class CategoryController : Controller
    {
        #region injection
        private readonly ICategoryService service;
        public CategoryController(ICategoryService _service)
        {
            service = _service;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var res = await service.GetAll();
            if (res.IsHasErrorOrNot)
            {
                return View("Error");
            }
            return View(res.result);
        }
#endregion
        
        #region AddCategory
        // GET: Category/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmAdd(CategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var res = await service.Add(vm);
            if (res.IsHasErrorOrNot)
            {
                ModelState.AddModelError("", res.ErrorMessage ?? "An error occurred.");
                return View(vm);
            }
            TempData["Success"] = "Category created successfully!";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region EditCategory
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var res = await service.GetById(id);
            if (res.IsHasErrorOrNot || res.result == null)
            {
                return NotFound();
            }
            return View(res.result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var res = await service.Update(id, vm);
            if (res.IsHasErrorOrNot)
            {
                ModelState.AddModelError("", res.ErrorMessage ?? "An error occurred.");
                return View(vm);
            }
            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region DeleteCategory
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await service.GetById(id);
            if (res.IsHasErrorOrNot || res.result == null)
            {
                return NotFound();
            }
            return View(res.result);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await service.Delete(id);
            if (res.IsHasErrorOrNot)
            {
                TempData["Error"] = res.ErrorMessage ?? "An error occurred while deleting.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
#endregion
    
    }
}
