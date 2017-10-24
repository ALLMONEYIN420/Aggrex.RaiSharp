using System;
using System.Collections.Generic;
using Aggrex.Configuration;
using LiteDB;

namespace Aggrex.Database.LiteDB
{
    public class LiteDBRepository<T> : IRepository<T> where T : IDataModel
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
            _liteRepository.Insert(item);
        }

        public void Update(T item)
        {
            _liteRepository.Update(item);
        }

        public void Delete(int id)
        {
            _liteRepository.Delete<T>((item) => item.Id == id , typeof(T).Name);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _liteRepository.Query<T>().Where(x => predicate(x)).ToEnumerable();
        }

        public T FindOne(Func<T, bool> predicate)
        {
            return _liteRepository.FirstOrDefault<T>(x => predicate(x));
        }
    }
}
