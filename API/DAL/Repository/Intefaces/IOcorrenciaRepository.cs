using DAL.DTOs;
using DAL.Models;
using DAL.Repository.Base;

namespace DAL.Repository.Intefaces
{
    public interface IOcorrenciaRepository : IRepository<Ocorrencia>
    {
        Task<IEnumerable<OcorreciaDto>> GetAll();
    }
}
