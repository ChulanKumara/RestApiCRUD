using Atmoz.EmissionBreakdownApi.Models;
using AutoMapper;
using EmissionBreakdownApi.DTOs;

namespace EmissionBreakdownApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        // Map Entity to DTO and vice versa for data transfers in both ways
        public AutoMapperProfiles()
        {
            CreateMap<EmissionBreakdownRowDTO, EmissionBreakdownRow>();
            CreateMap<EmissionBreakdownRow, EmissionBreakdownRowDTO>();

            CreateMap<EmissionCategoryDTO, EmissionCategory>();
            CreateMap<EmissionCategory, EmissionCategoryDTO>();

            CreateMap<EmissionSubCategoryDTO, EmissionSubCategory>();
            CreateMap<EmissionSubCategory, EmissionSubCategoryDTO>();

            CreateMap<EmissionBreakdownQueryParametersDTO, EmissionBreakdownQueryParameters>();
            CreateMap<EmissionBreakdownQueryParameters, EmissionBreakdownQueryParametersDTO>();

            CreateMap<CreatedEmissionBreakdownRowDTO, CreatedEmissionBreakdownRow>();
            CreateMap<CreatedEmissionBreakdownRow, CreatedEmissionBreakdownRowDTO>();
        }
    }
}
