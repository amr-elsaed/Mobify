using Mobify.DAL.DataBase.DBContext;
using Mobify.DAL.Repo.Abstraction;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mobify.DAL.Repo.Implmentation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDBContext context;
        public CategoryRepo(ApplicationDBContext _context)
        {
            context = _context;            
        }
        public async Task<bool> Add(Category category)
        {
            try
            {
                var res =await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                if(res.Entity.Id > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var res =await GetById(id);
                if (res != null)
                {
                    context.Remove(res);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EditAsync(Category category)
        {
            try
            {
                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>>? Filter = null)
        {
            try
            {
                if(Filter == null)
                {
                    var res =await context.Categories.AsNoTracking().ToListAsync();
                    return res;
                }
                else
                {
                    var res =await context.Categories.Where(Filter).AsNoTracking().ToListAsync();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category?> GetById(int id)
        {
            try
            {
                var res = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Category?> GetByName(string name)
        {
            try
            {
                var res = await context.Categories.FirstOrDefaultAsync(x => x.Name == name);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
