using Mobify.BLL.ModelVM.HomePageVM;

namespace Mobify.BLL.Services.Abstraction
{
    public interface IHomePageServices
    {
        public Task<Response<List<ProductCardVM>>> GetProductsCard(ProductCardQueryVM vm);
    }
}
