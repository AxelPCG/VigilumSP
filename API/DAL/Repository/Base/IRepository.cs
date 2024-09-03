namespace DAL.Repository.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        //Task<IEnumerable<TEntity>> GetAll();
        Task Adicionar(TEntity entity);
        Task<int> SaveChanes();
    }
}
