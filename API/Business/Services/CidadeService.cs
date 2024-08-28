using Business.Services.Interfaces;
using DAL.Models;
using DAL.Repository.Intefaces;

namespace Business.Services
{
    public class CidadeService : ICidadeService
    {
        private ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public async Task<IEnumerable<Cidade>> GetAll()
        {
            try
            {
                return await _cidadeRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Cadastrar(Cidade cidade)
        {
            try
            {
                await _cidadeRepository.Adicionar(cidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
