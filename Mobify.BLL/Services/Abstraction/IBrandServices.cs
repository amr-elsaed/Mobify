using Mobify.BLL.ModelVM.BrandVM;

namespace Mobify.BLL.Services.Abstraction
{
    public interface IBrandServices
    {
        public Task<Response<string>> Add(AddBrandVM vm);
        public Task<Response<List<ShowBrandVM>>> GetAll();
        public Task<Response<string>> Delete(int Id);
        public Task<Response<ShowBrandVM>> GetById(int Id);
        public Task<Response<EditBrandVM>> GetByIdForEdit(int Id);
        public Task<Response<string>>SaveUpdate(EditBrandVM vm);
    }
}
