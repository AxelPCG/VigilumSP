using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly BancoAPIContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(BancoAPIContext db) 
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanes();
        }

        public async Task<int> SaveChanes()
        {
            return await Db.SaveChangesAsync();
        }

        //public async Task<IEnumerable<TEntity>> GetAll()
        //{
        //    return await DbSet.AsNoTracking().ToListAsync();
        //}

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
