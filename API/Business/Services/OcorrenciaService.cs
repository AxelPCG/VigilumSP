using AutoMapper;
using Business.Services.Interfaces;
using DAL.DTOs;
using DAL.Models;
using DAL.Repository.Intefaces;
using DAL.ViewModels;

namespace Business.Services
{
    public class OcorrenciaService : IOcorrenciaService
    {
        private readonly IOcorrenciaRepository _repository;
        private readonly IMapper _mapper;

        public OcorrenciaService(IOcorrenciaRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OcorreciaDto>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Salvar(OcorrenciaViewModel model)
        {
            try
            {
                model.Concordo_Termos_Condicoes = model.Concordo ? 'S' : 'N';


                var ocorrencia = _mapper.Map<Ocorrencia>(model);
                await _repository.Adicionar(ocorrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
