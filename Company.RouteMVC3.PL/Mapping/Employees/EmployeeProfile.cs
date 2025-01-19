using AutoMapper;
using Company.RouteMVC3.DAL.Models;
using Company.RouteMVC3.PL.ViewModels.Employees;
namespace Company.RouteMVC3.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
