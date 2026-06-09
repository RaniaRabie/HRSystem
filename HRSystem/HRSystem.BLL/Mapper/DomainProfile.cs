using HRSystem.BLL.ModelVM.Department;
using HRSystem.BLL.ModelVM.Position;
using HRSystem.DAL.Models.Entities;

namespace HRSystem.BLL.Mapper
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<DepartmentFormVM, Department>().ReverseMap();
            CreateMap<PositionFormVM, Position>().ReverseMap();
        }
    }
}
