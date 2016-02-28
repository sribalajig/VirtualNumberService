using System;
using System.Collections.Generic;
using System.Linq;

namespace Telephony.VritualNumberService.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        public IEnumerable<T> Get(Func<T, bool> predicate = null)
        {
            return Enumerable.Empty<T>();
        }

        public void Add(T item)
        {
        }
    }
}