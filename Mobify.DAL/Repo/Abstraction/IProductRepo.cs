namespace Mobify.DAL.Repo.Abstraction
{
    public interface IProductRepo
    {

        public Task<List<Product>> GetAll();
        public Task<Product?> GetById(int Id);
        public IQueryable<Product> Query();

        public Task Add(Product product);

        public Task Delete(int Id);


    }
}
