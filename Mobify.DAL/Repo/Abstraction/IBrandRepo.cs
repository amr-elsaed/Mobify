namespace Mobify.DAL.Repo.Abstraction
{
    public interface IBrandRepo
    {
        public Task<bool> Add(Brand brand);
        public Task<bool> Update(int Id ,Brand brand);
        public Task<string> Delete(int Id);
        public Task<List<Brand>> GetAll();
        public Task<List<Brand>> GetAllWithPhotoes();
        public Task<List<Brand>> GetAllWithPhotoesAndProduct();
        public Task<Brand> GetById(int Id);
        public IQueryable<Brand> Query();
    }
}
