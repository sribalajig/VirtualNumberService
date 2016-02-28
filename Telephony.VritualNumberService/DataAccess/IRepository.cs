using System;
using System.Collections.Generic;

namespace Telephony.VritualNumberService.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Func<T, bool> predicate = null);

        void Add(T item);
    }
}
