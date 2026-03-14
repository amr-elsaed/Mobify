using Mobify.DAL.DataBase.DBContext;
using Mobify.DAL.Repo.Abstraction;
using System.Linq;

namespace Mobify.DAL.Repo.Implmentation
{
    public class BrandRepo : IBrandRepo
    {
        private readonly ApplicationDBContext context;
        public BrandRepo(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(Brand brand)
        {
            try
            {
                var res = await context.Brands.AddAsync(brand);
                await context.SaveChangesAsync();
                if(res.Entity.Id > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }
        //return photoURL to delete in server
        public async Task<string> Delete(int Id)
        {
            try
            {
                var url = await context.BrandPhotos.FirstOrDefaultAsync(x => x.BrandId == Id);
                var res = await context.Brands.Where(x => x.Id == Id).ExecuteDeleteAsync();
                if (res > 0)
                {
                    return url.PhotoUrl;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Brand>> GetAll()
        {
            try
            {
                return await context.Brands.AsNoTracking().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Brand>> GetAllWithPhotoes()
        {
            try
            {
                return await context.Brands.Include(x=>x.BrandPhoto).AsNoTracking().ToListAsync();
            }
            catch
            {
                throw ;
            }
        }
        public async Task<List<Brand>> GetAllWithPhotoesAndProduct()
        {
            try
            {
                return await context.Brands.AsNoTracking().Include(x=>x.BrandPhoto).Include(x=>x.Products).ToListAsync();
            }
            catch 
            {
                throw;
            }
        }
        public async Task<Brand> GetById(int Id)
        {
            try
            {
                var res =await context.Brands.Include(x=>x.BrandPhoto).AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                if(res != null)
                {
                    return res;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(int Id ,Brand brand)
        {
            try
            {
                var res = await context.Brands.Include(x=>x.BrandPhoto).FirstOrDefaultAsync(x => x.Id == Id);
                res.Name = brand.Name;
                if (brand.BrandPhoto.PhotoUrl != null)
                {
                    res.BrandPhoto.PhotoUrl = brand.BrandPhoto.PhotoUrl;
                }
                await context.SaveChangesAsync(); 
                return true;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Brand> Query()
        {
            return context.Brands;
        }
    
    }
}
