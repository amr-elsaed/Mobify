namespace Mobify.DAL.Repo.Abstraction
{
    public interface IProductRepo
    {
        public Task<List<Product>> GetAll();
        public Task<Product?> GetById(int Id);
        public Task<Product?> GetByIdIncludePropAndPhotoes(int Id);
        public Task<Product?> GetByIdIncludePropAndPhotoesNoTraacking(int Id);
        public IQueryable<Product> Query();
        public Task Add(Product product);
        public Task Update(Product product);
        public Task Delete(int Id);
        public Task UpdateOffer(ProductOffer productOffer);
        public Task<ProductOffer> GetOffer(int Id);
    }
}
