using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Telephony.VritualNumberService.Data.Repositories
{
    public class Repository<T, TC> : IRepository<T, TC>
        where T : class
        where TC : DbContext, new()
    {
        private readonly TC _entities = new TC();

        public IQueryable<T> Get()
        {
            return _entities.Set<T>();
        }

        public void Save(T entity)
        {
            _entities.Set<T>().AddOrUpdate(entity);

            _entities.SaveChanges();
        }
    }
}