using DAL.Models;
using DAL.Repository.Base;
using DAL.Repository.Intefaces;

namespace DAL.Repository
{
    public class CidadeRepository : Repository<Cidade>, ICidadeRepository
    {
        public BancoAPIContext _db { get; set; }

        public CidadeRepository(BancoAPIContext db) : base(db)
        {
            _db = db;
        }
    }
}