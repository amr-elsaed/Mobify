namespace Mobify.BLL.Services.Implmentation
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepo repo;
        public ProductServices(IProductRepo repo )
        {
            this.repo = repo;
        }
        public async Task<Response<bool>> Add(AddProductVM vm)
        {
            Product p = new Product();
            p.Name = vm.Name;
            p.Description = vm.Description;
            p.CPU = vm.CPU;
            p.Screen = vm.Screen;
            p.Camera = vm.Camera;
            p.Battary = vm.Battary;
            p.StockQuantity = vm.StockQuantity;
            p.Price = vm.Price;
            p.RAM = vm.RAM;
            p.Color = vm.Color;
            p.Storage = vm.Storage;
            p.CategoryId = vm.CategoryId;
            p.BrandId = vm.BrandId;
            foreach (var item in vm.AdvProperties)
            {
                ProductProperties properties = new ProductProperties();
                properties.Discription = item;
                properties.IsAdvantage = true;
                p.ProductProperties.Add(properties);
            }
            foreach (var item in vm.DisAdvProperties)
            {
                ProductProperties properties = new ProductProperties();
                properties.Discription = item;
                properties.IsAdvantage = false;
                p.ProductProperties.Add(properties);
            }
            foreach (var item in vm.FormFiles)
            {
                ProductPhoto productPhoto = new ProductPhoto();
                var url = Files.UploadFile("ProductPhotoes", item);
                productPhoto.PhotoUrl = url;
                p.ProductPhotos.Add(productPhoto);
            }
            await repo.Add(p);
            return new Response<bool>(true, null, false);
        }
        public async Task<Response<bool>> Delete(int Id)
        {
            await repo.Delete(Id);
            return new Response<bool>(true, null, false);
        }
        public async Task<Response<EditProductVM>> GetForEdit(int Id)
        {
            var res = await repo.Query().AsNoTracking().Where(p => p.Id == Id).Select(x => new EditProductVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CPU = x.CPU,
                Battary = x.Battary,
                Camera = x.Camera,
                Price = x.Price,
                RAM = x.RAM,
                Color = x.Color,
                Storage = x.Storage,
                StockQuantity = x.StockQuantity,
                Screen = x.Screen,
                CategoryId = x.CategoryId,
                BrandId = x.BrandId,
                ExistingPhoto = x.ProductPhotos.Select(x => x.PhotoUrl).ToList(),
                AdvProperties = x.ProductProperties.Where(x => x.IsAdvantage).Select(x => x.Discription).ToList(),
                DisAdvProperties = x.ProductProperties.Where(x => !x.IsAdvantage).Select(x => x.Discription).ToList(),

            }).FirstOrDefaultAsync();
            return new Response<EditProductVM>(res, null, false);
        }
        public async Task<Response<string>> SaveEdit(EditProductVM vm)
        {
            var res = await repo.GetByIdIncludePropAndPhotoesNoTraacking(vm.Id);

            if (res == null)
                return new Response<string>("Product Not Found", null, true);
            // =========================
            // Update Basic Info
            // =========================
            res.Name = vm.Name;
            res.Description = vm.Description;
            res.CPU = vm.CPU;
            res.Screen = vm.Screen;
            res.Camera = vm.Camera;
            res.Battary = vm.Battary;
            res.StockQuantity = vm.StockQuantity;
            res.RAM = vm.RAM;
            res.Price = vm.Price;
            res.Color = vm.Color;
            res.Storage = vm.Storage;
            res.CategoryId = vm.CategoryId;
            res.BrandId = vm.BrandId;
            // =========================
            // Delete Photos
            // =========================
            foreach (var photoURL in vm.PhotoesToDelete ?? new List<string>())
            {
                Files.RemoveFile("ProductPhotoes", photoURL);

                var existPhoto = res.ProductPhotos
                    .FirstOrDefault(x => x.PhotoUrl == photoURL);

                if (existPhoto != null)
                    res.ProductPhotos.Remove(existPhoto);
            }
            // =========================
            // Add New Photos
            // =========================
            foreach (var photo in vm.FormFiles ?? new List<IFormFile>())
            {
                var url = Files.UploadFile("ProductPhotoes", photo);

                res.ProductPhotos.Add(new ProductPhoto
                {
                    PhotoUrl = url
                });
            }
            // =========================
            // Update Properties
            // =========================
            res.ProductProperties.Clear();

            foreach (var adv in vm.AdvProperties ?? new List<string>())
            {
                res.ProductProperties.Add(new ProductProperties
                {
                    Discription = adv,
                    IsAdvantage = true
                });
            }

            foreach (var dis in vm.DisAdvProperties ?? new List<string>())
            {
                res.ProductProperties.Add(new ProductProperties
                {
                    Discription = dis,
                    IsAdvantage = false
                });
            }

            await repo.Update(res);

            return new Response<string>("Updated Successfully", null, false);
        }
        public async Task<PagedResult<ShowProductVM>> GetAll(ProductQueryVM vm)
        {
            vm.PageSize = Math.Min(vm.PageSize, 50);
            IQueryable<Product> items;
            vm.Search = vm.Search?.Trim();

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

            if (vm.CategoryId.HasValue)
            {
                items = items.Where(x => x.CategoryId == vm.CategoryId);
            }
            if (vm.BrandId.HasValue)
            {
                items = items.Where(x => x.BrandId == vm.BrandId);
            }
            if (!string.IsNullOrEmpty(vm.Search))
            {
                items = items.Where(x => x.Name.ToLower().Contains(vm.Search.ToLower()));
            }
            var totalCount = await items.CountAsync();

            var ResItems = await items.Select(x => new ShowProductVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CPU = x.CPU,
                Screen = x.Screen,
                Camera = x.Camera,
                Battary = x.Battary,
                StockQuantity = x.StockQuantity,
                Price = x.Price,
                Color = x.Color,
                Storage = x.Storage,
                RAM = x.RAM,
                CategoryName = x.Category.Name,
                BrandName = x.Brand.Name,
                ProductPhotoURL = x.ProductPhotos.OrderBy(n => n.Id).Take(1).Select(c => c.PhotoUrl).FirstOrDefault(),

            }).Skip((vm.Page - 1) * vm.PageSize).Take(vm.PageSize).ToListAsync();
            return new PagedResult<ShowProductVM>()
            {
                Items = ResItems,
                TotalItems = totalCount,
                Page = vm.Page,
                PageSize = vm.PageSize
            };
        }
    }
}
