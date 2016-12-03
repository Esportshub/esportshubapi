using System.Threading.Tasks;

namespace RestfulApi.App.Models.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> SaveAsync();

        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        bool Save();
    }
}