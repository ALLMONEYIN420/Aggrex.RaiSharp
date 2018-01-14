using System;
using System.Collections.Generic;
using Aggrex.Common.BitSharp;
using Aggrex.Configuration;
using LiteDB;

namespace Aggrex.Database.LiteDB
{
    public class LiteDBRepository<T> : IRepository<T>
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly LiteRepository _liteRepository;
        public LiteDBRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
            _liteRepository = new LiteRepository(_databaseSettings.FolderPath);
        }

        public void Insert(T item)
        {
            var s = _liteRepository.Insert(item);
        }

        public void Update(T item)
        {
            _liteRepository.Update(item);
        }

        public void Delete(UInt256 id)
        {
            _liteRepository.Database.GetCollection<T>().Delete(id.ToByteArray());
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _liteRepository.Query<T>().Where(x => predicate(x)).ToEnumerable();
        }

        public T FindOneById(UInt256 id)
        {
            return _liteRepository.Database.GetCollection<T>().FindById(id.ToByteArray());
        }
    }
}
