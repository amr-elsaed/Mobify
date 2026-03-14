using System.Linq.Expressions;

namespace Mobify.DAL.Repo.Abstraction
{
    public interface ICategoryRepo
    {
        Task<bool> Add(Category category);
        Task<bool> EditAsync(Category category);
        Task<List<Category>> GetAll(Expression<Func<Category,bool>>? Filter = null);
        Task<bool> Delete(int id);
        Task<Category?> GetById(int id);
        Task<Category?> GetByName(string name);
        public IQueryable<Category> Query();
    }
}
