using AutoMapper;
using DAL.Models;
using DAL.ViewModels;

namespace API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<OcorrenciaViewModel, Ocorrencia>().ReverseMap();
        }
    }
}