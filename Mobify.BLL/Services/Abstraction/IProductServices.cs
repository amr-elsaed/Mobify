using Mobify.BLL.ModelVM.ProductVM;

namespace Mobify.BLL.Services.Abstraction
{
    public interface IProductServices
    {
        public Task<Response<bool>> Add(AddProductVM vm);
        public Task<Response<EditProductVM>> GetForEdit(int Id);
        public Task<PagedResult<ShowProductVM>> GetAll(ProductQueryVM vm);
        public Task<Response<bool>> Delete(int Id);
        public Task<Response<string>> SaveEdit(EditProductVM vm);
        public Task<Response<bool>> UpdateOffer(ProductOfferVM productOffer);
        public Task<Response<ProductOfferVM>> GetProductOffer(int Id);
        public Task<Response<ProductPriceVM>> GetProoductPriceById(int Id);
    }
}
