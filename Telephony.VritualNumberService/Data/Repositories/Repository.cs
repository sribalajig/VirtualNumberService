using System;
using System.Collections.Generic;
using System.Linq;
using Telephony.VritualNumberService.Data.Persistence;

namespace Telephony.VritualNumberService.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        public IEnumerable<T> Get(Func<T, bool> predicate = null)
        {
            using (var databaseContext = new VirtualNumberContext())
            {
                if (predicate != null)
                    return databaseContext.Set<T>()
                        .Where(predicate).ToList();

                return databaseContext.Set<T>().ToList();
            }
        }

        public void Add(T item)
        {
        }
    }
}