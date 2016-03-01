using System;
using System.Collections.Generic;

namespace Telephony.VritualNumberService.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Func<T, bool> predicate = null);

        void Add(T item);
    }
}
