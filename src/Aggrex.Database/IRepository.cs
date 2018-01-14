using System;
using System.Collections;
using System.Collections.Generic;
using Aggrex.Common.BitSharp;
using LiteDB;

namespace Aggrex.Database
{
    public interface IRepository<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(UInt256 id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        T FindOneById(UInt256 id);
    }
}
