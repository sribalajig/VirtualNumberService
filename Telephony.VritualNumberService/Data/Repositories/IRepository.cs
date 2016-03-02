using System.Data.Entity;
using System.Linq;

namespace Telephony.VritualNumberService.Data.Repositories
{
    public interface IRepository<T, TC> where T : class where TC : DbContext
    {
        IQueryable<T> Get();

        void Save(T item);
    }
}
