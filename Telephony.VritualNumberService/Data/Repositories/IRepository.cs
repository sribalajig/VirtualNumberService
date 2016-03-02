using System.Data.Entity;
using System.Linq;

namespace Telephony.VritualNumberService.Data.Repositories
{
    public interface IRepository<T, C> where T : class where C : DbContext
    {
        IQueryable<T> Get();

        void Add(T item);
    }
}
