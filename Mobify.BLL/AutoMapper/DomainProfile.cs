using AutoMapper;
using Mobify.BLL.ModelVM.CategoryVM;

namespace Mobify.BLL.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryVM,Category>().ReverseMap();
        }
    }
}
