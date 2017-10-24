using System;
using System.Collections;
using System.Collections.Generic;

namespace Aggrex.Database
{
    public interface IRepository<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        T FindOne(Func<T, bool> predicate);
    }
}
