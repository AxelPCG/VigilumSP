using DAL.DTOs;
using DAL.Models;
using DAL.Repository.Base;
using DAL.Repository.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class OcorrenciaRepository : Repository<Ocorrencia>, IOcorrenciaRepository
    {
        private readonly BancoAPIContext _db;

        public OcorrenciaRepository(BancoAPIContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<OcorreciaDto>> GetAll()
        {
            return _db.Ocorrencias
                .AsNoTracking()
                .Select(x => new OcorreciaDto()
                {
                    Primeiro_Nome = x.Primeiro_Nome,
                    Sobrenome = x.Sobrenome,
                    Email = x.Email,
                    Telefone = x.Telefone,
                    Descricao = x.Descricao,
                    Mensagem = x.Mensagem,
                    Assunto = x.Assunto,
                    Concordo_Termos_Condicoes = x.Concordo_Termos_Condicoes
                }).ToList();
        }
    }
}
