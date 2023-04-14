
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using BVPortalAPIPrime.Models;

namespace BVPortalAPIPrime.Services
{
    public class AutoMapperProfile : Profile  
    {  
        public AutoMapperProfile()  
        {   
            CreateMap<AssetType, AssetTypeDTO>().ReverseMap();
            CreateMap<AssetAllocation, AssetAllocationDTO>().ReverseMap();
            CreateMap<Asset, AssetDTO>()
                .ForMember(dest => dest.Type, source => source.MapFrom(source => source.AssetType.Name))
                .ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<Leave, LeaveDTO>()
                .ForMember(dest => dest.FullName, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ForMember(dest => dest.LeaveType, source => source.MapFrom(source => source.LeaveType.Type))
                .ReverseMap();
            CreateMap<Candidate, CandidateDTO>()
                .ForMember(dest => dest.ReferByName, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ForMember(dest => dest.JobName, source => source.MapFrom(source => source.Openjobs.JobName))
                .ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<ClientTerm, ClientTermDTO>()
                .ForMember(dest => dest.ClientId, source => source.MapFrom(source => source.Client.ClientName))
                .ReverseMap();
            CreateMap<ClientTermHistory, ClientTermHistoryDTO>()
                .ForMember(dest => dest.ClientId, source => source.MapFrom(source => source.Client.ClientName))
                .ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<EmpClientPerHour, EmpClientPerHourDTO>()
                .ForMember(dest => dest.Employee, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ForMember(dest => dest.ClientId, source => source.MapFrom(source => source.Client.ClientName))
                .ReverseMap();
            CreateMap<EmpClientPerHourHistory, EmpClientPerHourHistoryDTO>()
                .ForMember(dest => dest.Employee, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ForMember(dest => dest.ClientId, source => source.MapFrom(source => source.Client.ClientName))
                .ReverseMap();
            CreateMap<EmployeeBasicInfo, EmployeeBasicInfoDTO>().ReverseMap();
            CreateMap<EmployeeContact, EmployeeContactDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<HolidayMaster, HolidayMasterDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<InvoiceProduct, InvoiceProductDTO>()
                .ForMember(dest => dest.InvoiceId, source => source.MapFrom(source => source.Invoice.InvoiceNumber))
                .ReverseMap();
            CreateMap<Openjobs, OpenjobsDTO>().ReverseMap();
            CreateMap<PaymentOption, PaymentOptionDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProjectAssignment, ProjectAssignmentDTO>()
                .ForMember(dest => dest.ProjectName, source => source.MapFrom(source => source.Project.ProjectName))
                .ForMember(dest => dest.EmployeeName, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ReverseMap();
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ClientName, source => source.MapFrom(source => source.Client.ClientName))
                .ReverseMap();
            CreateMap<ReferList, ReferListDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
             CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Employee, source => source.MapFrom(source => source.Employee.LastName+", "+source.Employee.FirstName))
                .ReverseMap();
        }  
    }  
}