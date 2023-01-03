
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
            
        }  
    }  
}