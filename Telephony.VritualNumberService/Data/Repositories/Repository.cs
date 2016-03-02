using System.Data.Entity;
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

        public void Save(T item)
        {
           _entities.Set<T>().Add(item);
        }
    }
}