using DAL.Models;

namespace Business.Services.Interfaces
{
    public interface ICidadeService
    {
        Task<IEnumerable<Cidade>> GetAll();
        Task Cadastrar(Cidade cidade);
    }
}
