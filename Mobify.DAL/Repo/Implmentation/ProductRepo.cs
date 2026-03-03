
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

        public IQueryable<Product> Query()
        {
            return context.Products;
        }
    }
}
