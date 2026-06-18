using Mobify.BLL.ModelVM.ProductDetailsVM;

namespace Mobify.BLL.Services.Implmentation
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IProductRepo repo;

        public ProductDetailsService(IProductRepo repo)
        {
            this.repo = repo;
        }
        public async Task<Response<ProductDetailsVM>> GetProductDetailsByIdAsync(int Id)
        {
            var res = await repo.Query().Where(x => x.Id == Id).Select(x => new ProductDetailsVM
            {
                Id = x.Id,
                Name = x.Name,
                Battary = x.Battary,
                Camera = x.Camera,
                CPU = x.CPU,
                Color = x.Color,
                Description = x.Description,
                Price = x.Price,
                RAM = x.RAM,
                Screen = x.Screen,
                ProductPhotos = x.ProductPhotos.Select(x => x.PhotoUrl).ToList(),
                Storage = x.Storage,
                BrandName = x.Brand.Name,
                CategoryName = x.Category.Name,
                AdvProductProperties = x.ProductProperties.Where(x => x.IsAdvantage == true).Select(x => x.Discription).ToList(),
                DisAdvProductProperties = x.ProductProperties.Where(x => x.IsAdvantage == false).Select(x => x.Discription).ToList(),
                HasOffer = x.productOffer != null ? x.productOffer.HasOffer : false,
                OfferPrice = x.productOffer != null ? x.productOffer.NewPrice : null,
                Precentage = x.productOffer != null ? x.productOffer.Precentage : null
            }).FirstOrDefaultAsync();
            return new Response<ProductDetailsVM>(res, null, false);
        }
    }
}
