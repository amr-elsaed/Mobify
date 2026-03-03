using Mobify.BLL.ModelVM.ProductVM;

namespace Mobify.BLL.Services.Abstraction
{
    public interface IProductServices
    {
        public Task<Response<bool>> Add(AddProductVM vm);
        public Task<Response<EditProductVM>> GetForEdit(int Id);
        public Task<PagedResult<ShowProductVM>> GetAll(ProductQueryVM vm);
    }
}
