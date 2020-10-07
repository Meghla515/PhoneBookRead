using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadPersistence.Repository.GenericRepository
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll(string cql, params object[] queryParams);
        IEnumerable<T> GetAll();
        void Execute(string query);
        IEnumerable<T> GetByCql(string cql, params object[] queryParams);
        void Delete(T entity);
        T Get(string cql, params object[] args);
        T Insert(T entity);
        void Update(T entity);
    }
}
