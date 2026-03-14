using AutoMapper;
using Mobify.BLL.ModelVM.HomePageVM;
using Mobify.BLL.Services.Abstraction;

namespace Mobify.BLL.Services.Implmentation
{
    public class CategoryServices : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepo repo;
        public CategoryServices(ICategoryRepo _repo,IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;   
        }

        public async Task<Response<string>> Add(CategoryVM categoryVM)
        {
            try
            {
                var res = mapper.Map<Category>(categoryVM);
                await repo.Add(res);
                return new Response<string>("Added Successfully", null, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> Delete(int id)
        {
            try
            {
                var category = await repo.GetById(id);
                if (category != null)
                {
                    bool res = await repo.Delete(category.Id);
                    if (res)
                    {
                        return new Response<string>("Deleted Successfully", null, false);
                    }
                }
                return new Response<string>("Not Deleted", "an error happened", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<CategoryVM>>> GetAll()
        {
            try
            {
                var res = await repo.GetAll();
                var resVM = mapper.Map<List<CategoryVM>>(res);
                return new Response<List<CategoryVM>>(resVM, null, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<CategoryVM>> GetById(int id)
        {
            try
            {
                var category = await repo.GetById(id);
                if (category != null)
                {
                    var vm = mapper.Map<CategoryVM>(category);
                    return new Response<CategoryVM>(vm, null, false);
                }
                return new Response<CategoryVM>(null, "Category not found", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<CategoryAndCountOfProduct>>> GetCategoryAndCountOfProduct()
        {
            var res = await repo.Query().Select(x => new CategoryAndCountOfProduct()
            {
                Id = x.Id,
                Name = x.Name,
                CountOfProduct = x.Products.Count()
            }).ToListAsync();
            return new Response<List<CategoryAndCountOfProduct>>(res, null, false);
        }

        public async Task<Response<string>> Update(int id, CategoryVM vm)
        {
            try
            {
                var category = await repo.GetById(id);
                if (category == null)
                {
                    return new Response<string>("Category not found", "Category not found", true);
                }
                category.Name = vm.Name;
                bool res = await repo.EditAsync(category);
                if (res)
                {
                    return new Response<string>("Updated Successfully",null, false);
                }
                return new Response<string>("Can't update the category","an error happen in repo",true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
