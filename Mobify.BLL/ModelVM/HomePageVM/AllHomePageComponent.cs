
using System.Net;

namespace Mobify.BLL.ModelVM.HomePageVM
{
    public class AllHomePageComponent
    {
        public ProductCardQueryVM ProductCardQueryVM { get; set; } = new ProductCardQueryVM();
        public List<ProductCardVM> ProductsCardVM { get; set; }
        public List<BrandAndCountOfProduct> brandAndCountOfProducts { get; set; }
        public List<CategoryAndCountOfProduct> CategoryAndCountOfProducts { get; set; }

    }
}
