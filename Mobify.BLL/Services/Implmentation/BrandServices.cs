using Mobify.BLL.Helper;
using Mobify.BLL.ModelVM.BrandVM;

namespace Mobify.BLL.Services.Implmentation
{
    public class BrandServices : IBrandServices
    {
        IBrandRepo repo;
        public BrandServices(IBrandRepo repo)
        {
            this.repo = repo;
        }
        public async Task<Response<string>> Add(AddBrandVM vm)
        {
            try
            {
                Brand brand = new Brand();
                brand.Name = vm.Name;
                string URL = Files.UploadFile("BrandPhotoes", vm.formFile);
                brand.BrandPhoto.PhotoUrl = URL;
                await repo.Add(brand);
                return new Response<string>("Added Successfully", null, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> Delete(int Id)
        {
            try
            {
                string res = await repo.Delete(Id);
                string delImg = Files.RemoveFile("BrandPhotoes",res);
                if (res!=null)
                {
                    return new Response<string>("Deleted Successfully" , null , false);
                }
                return new Response<string>("not Deleted", "error happen in repo", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<Response<List<ShowBrandVM>>> GetAll()
        {
            try
            {
                var res = await repo.GetAllWithPhotoes();
                List<ShowBrandVM> resOfVM = new List<ShowBrandVM>();

                foreach (var item in res)
                {
                    resOfVM.Add(new ShowBrandVM() {Id = item.Id , Name = item.Name, imgURL = item.BrandPhoto.PhotoUrl });
                }
                return new Response<List<ShowBrandVM>>(resOfVM, null, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<ShowBrandVM>> GetById(int Id)
        {
            try
            {
                var res =await repo.GetById(Id);
                if(res != null)
                {
                    ShowBrandVM vm = new ShowBrandVM() { Id = res.Id ,Name = res.Name , imgURL = res.BrandPhoto.PhotoUrl};
                    return new Response<ShowBrandVM>(vm, null, false);
                }
                return new Response<ShowBrandVM>(null, null, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<Response<EditBrandVM>>GetByIdForEdit(int Id)
        {
            try
            {
                var res = await repo.GetById(Id);
                if (res != null)
                {
                    EditBrandVM vm = new EditBrandVM() { Id = res.Id, Name = res.Name, currentPhoto = res.BrandPhoto.PhotoUrl };
                    return new Response<EditBrandVM>(vm, null, false);
                }
                return new Response<EditBrandVM>(null, null, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> SaveUpdate(EditBrandVM vm)
        {
            try
            {
                Brand b = new Brand();
                if (vm.formfile != null)
                {
                    string DelOldPhoto = Files.RemoveFile("BrandPhotoes", vm.currentPhoto);
                    string url = Files.UploadFile("BrandPhotoes",vm.formfile);
                    b.BrandPhoto.PhotoUrl = url;
                }
                b.Id = vm.Id;
                b.Name = vm.Name;
                await repo.Update(vm.Id, b);
                return new Response<string>("Updated Succesfully",null,false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
