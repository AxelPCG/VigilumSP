using DAL.DTOs;
using DAL.ViewModels;

namespace Business.Services.Interfaces
{
    public interface IOcorrenciaService
    {
        Task<IEnumerable<OcorreciaDto>> GetAll();
        Task Salvar(OcorrenciaViewModel model);
    }
}
