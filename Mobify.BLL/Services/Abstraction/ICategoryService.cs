namespace Mobify.BLL.Services.Abstraction
{
    public interface ICategoryService
    {
        public Task<Response<string>> Add(CategoryVM categoryVM);
        public Task<Response<string>> Update(int id, CategoryVM vm);
        public Task<Response<string>> Delete(int id);
        public Task<Response<List<CategoryVM>>> GetAll();
        public Task<Response<CategoryVM>> GetById(int id);
    }
}
