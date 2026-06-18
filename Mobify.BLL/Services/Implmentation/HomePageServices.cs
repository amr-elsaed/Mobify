using Mobify.BLL.ModelVM.ResponseResult;
using Mobify.BLL.ModelVM.HomePageVM;
using System.Runtime.CompilerServices;

namespace Mobify.BLL.Services.Implmentation
{
    
    public class HomePageServices : IHomePageServices
    {
        private readonly IProductRepo repo;

        public HomePageServices(IProductRepo repo)
        {
            this.repo = repo;
        }
        public async Task<Response<List<ProductCardVM>>> GetProductsCard(ProductCardQueryVM vm)
        {
            vm.Search = vm.Search?.Trim();
            IQueryable<Product> items;
            if (vm.Sort == "asc")
            {
                items = repo.Query().OrderBy(x => x.Price).AsNoTracking();
            }
            else if (vm.Sort == "desc")
            {
                items = repo.Query().OrderByDescending(x => x.Price).AsNoTracking();
            }
            else
            {
                items = repo.Query().AsNoTracking();
            }
            if (vm.BrandId.HasValue)
            {
                items = items.Where(x=>x.BrandId ==  vm.BrandId);
            }
            if (vm.CategoryId.HasValue)
            {
                items = items.Where(x => x.CategoryId == vm.CategoryId);
            }
            if (vm.Price.HasValue)
            {
                decimal maxPrice = vm.Price.Value;

                items = items.Where(x =>
                    (x.productOffer != null && x.productOffer.NewPrice != 0
                        ? x.productOffer.NewPrice
                        : x.Price) <= maxPrice
                );
            }
            if (!string.IsNullOrEmpty(vm.Search))
            {
                items = items.Where(x => x.Name.ToLower().Contains(vm.Search.ToLower()));
            }
            var totalCount =await items.CountAsync();
            var res =await items.Select(x => new ProductCardVM
            {  
                Id = x.Id,
                CPU = x.CPU,
                BrandName = x.Brand.Name,
                OriginalPrice = x.Price,
                PtoductName = x.Name,
                RAM = x.RAM,
                PhotoURL = x.ProductPhotos.Select(p => p.PhotoUrl).FirstOrDefault() ?? "/images/no-image.png",
                OfferPrice = x.productOffer.NewPrice,
                OfferAsPrecentage = x.productOffer.Precentage,
                HasOffer = x.productOffer.HasOffer?x.productOffer.HasOffer:false,
                CategoryName = x.Category.Name,
            }).ToListAsync();
            return new Response<List<ProductCardVM>>(res, null, false);
        }
    }
}
