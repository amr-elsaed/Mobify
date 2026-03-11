
namespace Mobify.DAL.Repo.Implmentation
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDBContext context;
        public ProductRepo(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Product>> GetAll()
        {
            var res = await context.Products.AsNoTracking().ToListAsync();
            return res;
        }
        public async Task<Product?> GetById(int Id)
        {
            return await context.Products.FindAsync(Id);
        }
        public async Task<Product?> GetByIdIncludePropAndPhotoesNoTraacking(int Id)
        {
            return await context.Products.Include(x=>x.ProductProperties).Include(c=>c.ProductPhotos).AsNoTracking().FirstOrDefaultAsync(x=>x.Id == Id);
        }
        public async Task<Product?> GetByIdIncludePropAndPhotoes(int Id)
        {
            return await context.Products.Include(x => x.ProductProperties).Include(c => c.ProductPhotos).FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task Add(Product product)
        {
            var res = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }
        public async Task Delete(int Id)
        {
            var affected = await context.Products.Where(x => x.Id == Id).ExecuteDeleteAsync();
            if(affected == 0)
            {
                throw new KeyNotFoundException("Product Id Not Found");
            }
        }
        public async Task Update(Product product)
        {
            var existingProduct = await context.Products
            .Include(p => p.ProductPhotos)
            .Include(p => p.ProductProperties)
            .FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct == null)
                return;
            // Update scalar properties
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.CPU = product.CPU;
            existingProduct.Screen = product.Screen;
            existingProduct.Camera = product.Camera;
            existingProduct.Battary = product.Battary;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Price = product.Price;
            existingProduct.Color = product.Color;
            existingProduct.Storage = product.Storage;
            existingProduct.RAM = product.RAM;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.BrandId = product.BrandId;
            existingProduct.ProductProperties.Clear();
            foreach (var item in product.ProductProperties)
            {
                existingProduct.ProductProperties.Add(item);
            }
            existingProduct.ProductPhotos = product.ProductPhotos;
            await context.SaveChangesAsync();

        }
        public IQueryable<Product> Query()
        {
            return context.Products;
        }
    }
}
