using Mobify.BLL.ModelVM.ProductDetailsVM; 
namespace Mobify.BLL.Services.Abstraction
{
    public interface IProductDetailsService
    {
        public Task<Response<ProductDetailsVM>> GetProductDetailsByIdAsync(int Id);
    }
}
