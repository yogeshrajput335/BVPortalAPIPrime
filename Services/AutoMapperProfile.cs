
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;

namespace BVPortalAPIPrime.Services
{
    public class AutoMapperProfile : Profile  
    {  
        public AutoMapperProfile()  
        {   
            CreateMap<AssetType, AssetTypeDTO>().ReverseMap();
            CreateMap<Asset, AssetDTO>()
                .ForMember(dest => dest.Type, source => source.MapFrom(source => source.AssetType.Name))
                .ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<Leave, LeaveDTO>()
                .ForMember(dest => dest.FullName, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ForMember(dest => dest.LeaveType, source => source.MapFrom(source => source.LeaveType.Type))
                .ReverseMap();
            
        }  
    }  
}